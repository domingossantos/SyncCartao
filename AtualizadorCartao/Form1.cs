using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Mail;
using System.Net;


namespace AtualizadorCartao
{
    public partial class PrincipalForm : Form
    {
        private Data.Conexao conLocal;
        private Data.Conexao conRemoto;
        private String pathDirApp;
        public String dirDiaLog;
        private String dirLog;
        private String filePit;
        private int vezes = 0;
        public bool chControle = false;
        private int numCartaoAtualizado;
        private int numCartaoNovo;
        private int numAssinaturas;

        public PrincipalForm()
        {
            InitializeComponent();
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.BalloonTipText = "Atualizador de Cartão";
            pathDirApp = Application.StartupPath;
            dirLog = pathDirApp + @"\log";
            filePit = dirLog + @"\processo.pit";
            notifyIcon1.Visible = false;
            numCartaoAtualizado = 0;
            numCartaoNovo = 0;
            numAssinaturas = 0;
        }

        private void PrincipalForm_Load(object sender, EventArgs e)
        {
            conLocal = new Data.Conexao(Properties.Settings.Default.local);
            conRemoto = new Data.Conexao(Properties.Settings.Default.remoto);
            txDataInicio.Text = Properties.Settings.Default.cfgDataInicio;
            txDataFim.Text = Properties.Settings.Default.cfgDataFim;
            Refresh();
            //timer1.Enabled = true;
        }

        private void verificaCartaoParaCopia()
        {
            String sql = "select top 50 nrCartao,nmCartao from tblCartaoAssinatura where stCopia = 'N' order by dtCadastro desc, dtRenovacao desc";


            toolStripStatusLabel4.Text = DateTime.Now.ToLongDateString() + " as " + DateTime.Now.ToLongTimeString();
            conLocal.abreBanco();
            dataGridView1.DataSource = conLocal.retornarDataSet(sql);
            conLocal.fechaBanco();
        }

        /*
         * Verifica se o cartão existr na base remota
         * Se existir verifica a data de renovação
         * 
         * Retorno: 0 - Não Existe
         *          1 - Existe e deve ser atualizada
         *          2 - Existe mas não precisa atualizar
         */ 
        private int consultaCartaoRemoto(String nrCartao,DateTime dataCadastro, DateTime dataRenovacao) {
            String sql = "select nrCartao,dtRenovacao,dtCadastro from tblCartaoAssinatura where nrCartao = '" + nrCartao.PadLeft(7, '0') + "'";
            conRemoto.abreBanco();

            SqlDataReader dado = conRemoto.retornoDataReader(sql);
            int retorno = 0;
            if (dado.HasRows)
            {
                dado.Read();
                if (!dado["dtRenovacao"].ToString().Equals(""))
                {
                    DateTime dataLinhaRenovacao = Convert.ToDateTime(dado["dtRenovacao"].ToString());
                    if (dataLinhaRenovacao <= dataRenovacao)
                    {
                        retorno = 1;
                    }
                    else
                    {
                        retorno = 2;
                    }
                }
                else {
                    retorno = 2;
                }
            }
            conRemoto.fechaBanco();
            return retorno;
        }

        private CartaoAssinatura getCartaoLocal(String nrCartao) {
            CartaoAssinatura _cartao = new CartaoAssinatura();

            conLocal.abreBanco();
            String sql = "select * from tblCartaoAssinatura where nrCartao = '"+nrCartao+"'";

            SqlDataReader dados = conLocal.retornoDataReader(sql);

            if (dados.HasRows) {
                dados.Read();
                //Convert.ToInt32(dr["nrSelo"].ToString());
                _cartao.NrCartao = dados["nrCartao"].ToString();
                if(!dados["idCartorio"].ToString().Equals(""))
                    _cartao.IdCartorio = Convert.ToInt32(dados["idCartorio"].ToString());

                if(!dados["dtCadastro"].ToString().Equals(""))
                    _cartao.DtCadastro = DateTime.Parse(dados["dtCadastro"].ToString());
                
                _cartao.NmCartao = dados["nmCartao"].ToString();
                _cartao.NrCPF = dados["nrCPF"].ToString();
                _cartao.DsEndereco = dados["dsEndereco"].ToString();
                _cartao.DsBairro = dados["dsBairro"].ToString();
                _cartao.NmCidade = dados["nmCidade"].ToString();
                _cartao.NrCEP = dados["nrCEP"].ToString();
                _cartao.SgUF = dados["sgUF"].ToString();

                if(!dados["dtNascimento"].ToString().Equals(""))
                    _cartao.DtNascimento = DateTime.Parse(dados["dtNascimento"].ToString());

                _cartao.NrRG = dados["nrRG"].ToString();

                if(!dados["dtExpRG"].ToString().Equals(""))
                    _cartao.DtExpedicao = DateTime.Parse(dados["dtExpRG"].ToString());

                _cartao.DsOrgaoEmissor = dados["dsOrgaoExpRG"].ToString();
                _cartao.NrFones = dados["nrFones"].ToString();

                if (!dados["tpCartao"].ToString().Equals(""))
                {
                    _cartao.TpCartao = Convert.ToChar(dados["tpCartao"].ToString()[0]);
                }
                else { _cartao.TpCartao = 'C'; }


                if (!dados["dtRenovacao"].ToString().Equals(""))
                    _cartao.DtRenovacao = DateTime.Parse(dados["dtRenovacao"].ToString());

                if (!dados["cdTipoRG"].ToString().Equals(""))
                    _cartao.CdTipoRG = Convert.ToInt32(dados["cdTipoRG"].ToString());

                _cartao.DsObservacao = dados["dsObservacao"].ToString();
                _cartao.DsEmail = dados["dsEmail"].ToString();

            }

            dados.Close();
            return _cartao;
        }
        private bool atualizaCartaoRemoto(CartaoAssinatura cartao) {
            bool retorno = false;

            try
            {


                string sql = "UPDATE tblCartaoAssinatura SET ";
                sql += "nmCartao = @nmCartao ";
                sql += ",nrCPF = @nrCPF ";
                sql += ",dsEndereco = @dsEndereco ";
                sql += ",dsBairro = @dsBairro ";
                sql += ",nmCidade = @nmCidade ";
                sql += ",nrCEP = @nrCEP ";
                sql += ",sgUF = @sgUF ";
                sql += ",dtNascimento = @dtNascimento ";
                sql += ",dtRenovacao = @dtRenovacao ";
                sql += ",nrRG = @nrRG ";
                sql += ",dtExpRG = @dtExpRG ";
                sql += ",dsOrgaoExpRG = @dsOrgaoExpRG ";
                sql += ",nrFones = @nrFones ";
                sql += ",tpCartao = @tpCartao ";
                sql += ",idCartorio = @idCartorio ";
                sql += ",dsObservacao = @dsObservacao ";
                sql += ",dsEmail = @dsEmail ";
                sql += "WHERE nrCartao = @nrCartao";

                conRemoto.abreBanco();

                conRemoto.cmd.CommandText = sql;
                conRemoto.cmd.Parameters.AddWithValue("@nmCartao", cartao.NmCartao);
                conRemoto.cmd.Parameters.AddWithValue("@nrcartao", cartao.NrCartao.PadLeft(7, '0'));
                conRemoto.cmd.Parameters.AddWithValue("@nrCPF", cartao.NrCPF);
                conRemoto.cmd.Parameters.AddWithValue("@dsEndereco", cartao.DsEndereco);
                conRemoto.cmd.Parameters.AddWithValue("@dsBairro", cartao.DsBairro);
                conRemoto.cmd.Parameters.AddWithValue("@nmCidade", cartao.NmCidade);
                conRemoto.cmd.Parameters.AddWithValue("@nrCEP", cartao.NrCEP);
                conRemoto.cmd.Parameters.AddWithValue("@sgUF", cartao.SgUF);
                if (cartao.DtNascimento != null)
                    conRemoto.cmd.Parameters.AddWithValue("@dtNascimento", cartao.DtNascimento);
                else
                    conRemoto.cmd.Parameters.AddWithValue("@dtNascimento", DBNull.Value);

                if (cartao.DtRenovacao != null)
                    conRemoto.cmd.Parameters.AddWithValue("@dtRenovacao", cartao.DtRenovacao);
                else
                    conRemoto.cmd.Parameters.AddWithValue("@dtRenovacao", DBNull.Value);

                conRemoto.cmd.Parameters.AddWithValue("@nrRG", cartao.NrRG);
                if (cartao.DtExpedicao != null)
                    conRemoto.cmd.Parameters.AddWithValue("@dtExpRG", cartao.DtExpedicao);
                else
                    conRemoto.cmd.Parameters.AddWithValue("@dtExpRG", DBNull.Value);

                conRemoto.cmd.Parameters.AddWithValue("@dsOrgaoExpRG", cartao.DsOrgaoEmissor);
                conRemoto.cmd.Parameters.AddWithValue("@nrFones", cartao.NrFones);
                conRemoto.cmd.Parameters.AddWithValue("@tpCartao", cartao.TpCartao);

                conRemoto.cmd.Parameters.AddWithValue("@idCartorio", cartao.IdCartorio);
                conRemoto.cmd.Parameters.AddWithValue("@dsObservacao", cartao.DsObservacao);
                conRemoto.cmd.Parameters.AddWithValue("@dsEmail", cartao.DsEmail);
                

                conRemoto.cmd.ExecuteNonQuery();
                
                retorno = true;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao inserir cartão " + ex.Message);
            }
            finally {
                conRemoto.cmd.Parameters.Clear();
                conRemoto.fechaBanco();
            }
            

            return retorno;
        }
        private bool gravaCartaoRemoto(CartaoAssinatura cartao)
        {
            bool retorno = false;
            try
            {
                string sql = "INSERT INTO tblCartaoAssinatura ";
                sql += "(nrCartao,dtCadastro,nmCartao,nrCPF,dsEndereco ";
                sql += ",dsBairro,nmCidade,nrCEP,sgUF,dtNascimento,nrRG ";
                sql += ",dtExpRG,dsOrgaoExpRG,nrFones,tpCartao,idCartorio,dsObservacao,dsEmail)VALUES ";
                sql += "(@nrCartao,@dtCadastro,@nmCartao,@nrCPF ,@dsEndereco ";
                sql += ",@dsBairro,@nmCidade,@nrCEP,@sgUF,@dtNascimento,@nrRG ";
                sql += ",@dtExpRG,@dsOrgaoExpRG,@nrFones, @tpCartao,@idCartorio,@dsObservacao,@dsEmail)";
                conRemoto.abreBanco();

                conRemoto.cmd.CommandText = sql;
                conRemoto.cmd.Parameters.AddWithValue("@nrcartao", cartao.NrCartao.PadLeft(7, '0'));
                conRemoto.cmd.Parameters.AddWithValue("@dtCadastro", cartao.DtCadastro);
                conRemoto.cmd.Parameters.AddWithValue("@nmCartao", cartao.NmCartao);
                conRemoto.cmd.Parameters.AddWithValue("@nrCPF", cartao.NrCPF);
                conRemoto.cmd.Parameters.AddWithValue("@dsEndereco", cartao.DsEndereco);
                conRemoto.cmd.Parameters.AddWithValue("@dsBairro", cartao.DsBairro);
                conRemoto.cmd.Parameters.AddWithValue("@nmCidade", cartao.NmCidade);
                conRemoto.cmd.Parameters.AddWithValue("@nrCEP", cartao.NrCEP);
                conRemoto.cmd.Parameters.AddWithValue("@sgUF", cartao.SgUF);

                if (cartao.DtNascimento != null)
                    conRemoto.cmd.Parameters.AddWithValue("@dtNascimento", cartao.DtNascimento);
                else
                    conRemoto.cmd.Parameters.AddWithValue("@dtNascimento", DBNull.Value);


                conRemoto.cmd.Parameters.AddWithValue("@nrRG", cartao.NrRG);

                if (cartao.DtExpedicao != null)
                    conRemoto.cmd.Parameters.AddWithValue("@dtExpRG", cartao.DtExpedicao);
                else
                    conRemoto.cmd.Parameters.AddWithValue("@dtExpRG", DBNull.Value);

                conRemoto.cmd.Parameters.AddWithValue("@dsOrgaoExpRG", cartao.DsOrgaoEmissor);
                conRemoto.cmd.Parameters.AddWithValue("@nrFones", cartao.NrFones);
                conRemoto.cmd.Parameters.AddWithValue("@tpCartao", cartao.TpCartao);
                conRemoto.cmd.Parameters.AddWithValue("@idCartorio", cartao.IdCartorio);
                conRemoto.cmd.Parameters.AddWithValue("@dsObservacao", cartao.DsObservacao);
                conRemoto.cmd.Parameters.AddWithValue("@dsEmail", cartao.DsEmail);

                conRemoto.cmd.ExecuteNonQuery();
                
                retorno = true;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao inserir cartão " + ex.Message);
            }
            finally
            {
                conRemoto.cmd.Parameters.Clear();
                conRemoto.fechaBanco();
            }

            return retorno;
        }

        private bool consultaImagemRemoto(String nrCartao, DateTime dtAssinatura) {
            bool retorno = false;

            String sql = "select count(*) as qtd from tblAssinaturas ";
            sql += " where nrCartao = @nrCartao and dtAssinatura = @dtAssinatura";


            try
            {
                conRemoto.abreBanco();
                conRemoto.cmd.CommandText = sql;
                conRemoto.cmd.Parameters.AddWithValue("@nrCartao", nrCartao);

                if (DateTime.Parse("01/01/1900 00:00:00") > dtAssinatura)
                {
                    conRemoto.cmd.Parameters.AddWithValue("@dtAssinatura", DateTime.Parse("01/01/1900 00:00:00"));
                }
                else {
                    conRemoto.cmd.Parameters.AddWithValue("@dtAssinatura", dtAssinatura);
                }
                SqlDataReader dr = conRemoto.cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    if (Convert.ToInt16(dr["qtd"].ToString()) > 0) {
                        retorno = true;
                    }
                }
               
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao ler assinatura " + ex.Message);
            }
            finally {
                conRemoto.cmd.Parameters.Clear();
                conRemoto.fechaBanco();
            }

            return retorno;
        }

        private Assinatura getAssinaturaLocal(String nrCartao) { 
            Assinatura _assinatura = new Assinatura();

            String sql = "select * from tblAssinaturas where stCopia <> 'G'";
            sql += " and  nrCartao = '" + nrCartao + "' and dtAssinatura = ";
            sql += " (select MAX(dtAssinatura) from tblAssinaturas ";
            sql += " where nrCartao = '" + nrCartao + "' )";

            try
            {
                conLocal.abreBanco();
                SqlDataReader dr = conLocal.retornoDataReader(sql);

                if (dr.HasRows)
                {
                    dr.Read();

                    _assinatura.BiAssinatura = (byte[])dr["biAssinatura"];
                    _assinatura.DtAssnatura = DateTime.Parse(dr["dtAssinatura"].ToString());
                    _assinatura.NrCartao = dr["nrCartao"].ToString();

                    if (!dr["nrTamanhoImagem"].ToString().Equals(""))
                    {
                        _assinatura.NrTamanhoImagem = Convert.ToInt32(dr["nrTamanhoImagem"].ToString());
                    }
                    else
                    {
                        _assinatura.NrTamanhoImagem = 0;
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao ler assinatura " + ex.Message);
            }
            finally
            {
                conLocal.cmd.Parameters.Clear();
                conLocal.fechaBanco();
            }

            return _assinatura;
        }
        private bool gravaImagemRemoto(Assinatura assinatura) {
            bool retorno = false;
            try
            {

                if (DateTime.Parse("01/01/1900 00:00:00") < assinatura.DtAssnatura)
                {

                    string sql = "insert into tblAssinaturas (dtAssinatura, biAssinatura, nrCartao) values (@dtAssinatura,@biAssinatura,@nrCartao)";
                    conRemoto.abreBanco();
                    conRemoto.cmd.CommandText = sql;
                    conRemoto.cmd.Parameters.AddWithValue("@dtAssinatura", assinatura.DtAssnatura);
                    conRemoto.cmd.Parameters.AddWithValue("@biAssinatura", assinatura.BiAssinatura);
                    conRemoto.cmd.Parameters.AddWithValue("@nrCartao", assinatura.NrCartao);

                    conRemoto.cmd.ExecuteNonQuery();
                }
                
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro incluir assinaturas " + ex.Message);
            }
            finally {
                conRemoto.cmd.Parameters.Clear();
                conRemoto.fechaBanco();
            }

            return retorno;
        }

        private void atualizaStatusCartao(String nrCartao) {
            String sql = "update tblCartaoAssinatura set stCopia = 'S' where nrCartao = '" + nrCartao + "'";
            conLocal.abreBanco();
            conLocal.executaSemRetorno(sql);
            conLocal.fechaBanco();
        }

        // Verifica se há um processo de copia ativo
        private bool getProcessoAtivo() {
            bool retorno = false;
            try
            {
               

                if (!Directory.Exists(dirLog))
                {
                    Directory.CreateDirectory(dirLog);
                }

                if (!File.Exists(filePit))
                {
                    File.Create(filePit).Close();
                    
                    StreamWriter arquivo = new StreamWriter(filePit);
                    arquivo.Write("Data do processo: " + DateTime.Now.ToLongDateString() + " "+DateTime.Now.ToLongTimeString());
                    arquivo.Close();
                }
                else
                {
                    retorno = true;
                }
            }
            catch (Exception ex) { 
                MessageBox.Show("Erro: "+ex.Message);
            }
            
            return retorno;
        }

        private void maximizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }

        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(notifyIcon1.Visible == true)
                notifyIcon1.Visible = false;
            this.Close();
        }

        private void PrincipalForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState) {
                notifyIcon1.Visible = true;
                Hide();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            String strDataInicio = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day+ " "+ txDataInicio.Text;
            String strDataFim = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " + txDataFim.Text;

            DateTime dataInicio = Convert.ToDateTime(strDataInicio);
            DateTime dataFim = Convert.ToDateTime(strDataFim);

            if (dataInicio <= DateTime.Now)
            {

                if (!getProcessoAtivo())
                {
                    verificaCartaoParaCopia();
                    String nrCartao = "";

                    if (dataGridView1.RowCount > 1)
                    {


                        dirDiaLog = dirLog + @"\" + DateTime.Now.ToShortDateString().Replace("/", "_");
                        if (!Directory.Exists(dirDiaLog))
                        {
                            Directory.CreateDirectory(dirDiaLog);
                        }

                        String arquivoLog = dirDiaLog + @"\log-" + DateTime.Now.ToShortDateString().Replace("/", "-") + DateTime.Now.ToLongTimeString().Replace(":", "-") + ".txt";
                        if (!File.Exists(arquivoLog))
                        {
                            File.Create(arquivoLog).Close();
                        }

                        StreamWriter arquivo = new StreamWriter(arquivoLog);
                        arquivo.WriteLine("############### COPIA DE ASSINATURAS ###############");
                        arquivo.WriteLine("DATA: " + DateTime.Now.ToShortDateString());
                        arquivo.WriteLine("HORA: " + DateTime.Now.ToShortTimeString());
                        arquivo.WriteLine("##################### CARTOES ######################");


                        timer1.Enabled = false;
                        try
                        {
                            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                            {

                                nrCartao = this.dataGridView1[0, i].Value.ToString();
                                arquivo.WriteLine("Cartão No." + nrCartao);
                                CartaoAssinatura oCartao = getCartaoLocal(nrCartao);
                                int ckCartaoRemoto = consultaCartaoRemoto(nrCartao, oCartao.DtCadastro, oCartao.DtRenovacao);
                                if (ckCartaoRemoto == 1)
                                {
                                    arquivo.WriteLine("Cartão Existe.");
                                    atualizaCartaoRemoto(oCartao);
                                    arquivo.WriteLine("Cartão Atualizado.");
                                    numCartaoAtualizado++;
                                }
                                else if (ckCartaoRemoto == 0)
                                {
                                    arquivo.WriteLine("Cartão Não Existe.");
                                    gravaCartaoRemoto(oCartao);
                                    numCartaoNovo++;
                                    arquivo.WriteLine("Cartão Copiado.");
                                }
                                else
                                {
                                    arquivo.WriteLine("Cartão já atualizado.");
                                }

                                Assinatura oAssinatura = getAssinaturaLocal(nrCartao);

                                if (!consultaImagemRemoto(nrCartao, oAssinatura.DtAssnatura))
                                {
                                    gravaImagemRemoto(oAssinatura);
                                    numAssinaturas++;
                                    arquivo.WriteLine("Assinatura Copiada.");
                                }
                                else
                                {
                                    arquivo.WriteLine("Assinatura ja existe.");
                                }


                                atualizaStatusCartao(nrCartao);

                            }

                        }
                        catch (Exception ex)
                        {
                            arquivo.WriteLine("Erro no processo: " + ex.Message);
                        }
                        finally
                        {
                            vezes++;
                            lbCiclos.Text = vezes.ToString();
                            Refresh();
                            arquivo.Close();
                            timer1.Enabled = true;
                            File.Delete(filePit);
                        }
                    }
                    else {
                        File.Delete(filePit);
                        escreveResumo();
                        enviaEmail();
                        this.Close();
                    }

                }

                if (dataFim <= DateTime.Now)
                {
                    timer1.Enabled = false;
                    Properties.Settings.Default.cfgDataInicio = txDataInicio.Text;
                    Properties.Settings.Default.cfgDataFim = txDataFim.Text;
                    Properties.Settings.Default.Save();
                    File.Delete(filePit);
                    escreveResumo();

                    enviaEmail();
                    this.Close();
                }
            }

            
        }

        public void escreveResumo() {
            String arquivoResumo = dirDiaLog + @"\ResumoLog-" + DateTime.Now.ToShortDateString().Replace("/", "-") + DateTime.Now.ToLongTimeString().Replace(":", "-") + ".txt";
            if (!File.Exists(arquivoResumo))
            {
                File.Create(arquivoResumo).Close();
            }

            StreamWriter arquivo = new StreamWriter(arquivoResumo);
            arquivo.WriteLine("############### RESUMO SINCRONISMO ###############");
            arquivo.WriteLine("DATA SINCRONISMO: " + DateTime.Now.ToShortDateString());
            arquivo.WriteLine("Cadastros Novos: " + numCartaoNovo);
            arquivo.WriteLine("Cadastros Atualizados: " + numCartaoAtualizado);
            arquivo.WriteLine("Assinaturas Incluidas: " + numAssinaturas);
            arquivo.Close();

        }

        public void enviaEmail() {
            /*
            try
            {
                SmtpClient mail = new SmtpClient("smtp.google.com",587);

                mail.EnableSsl = true;
                mail.Credentials = new NetworkCredential("domsantos@gmail.com", "D0m1ng02@santos");
                mail.UseDefaultCredentials = true;

                MailMessage mensagem = new MailMessage();
                mensagem.Sender = new MailAddress("domsantos@gmail.com", "Apps");
                mensagem.From = new MailAddress("domsantos@gmail.com", "Apps");
                mensagem.To.Add(new MailAddress("domsantos@hotmail.com", "Domingos"));
                mensagem.Subject = "Relatório de sincronismo de " + Properties.Settings.Default.origem + " dia " + DateTime.Now.ToShortDateString();
                
                StringBuilder msg = new StringBuilder();

                msg.Append("Resumo do sincronismo\n");
                msg.Append("Numero de Cadastro Novos: " + numCartaoNovo.ToString() + "\n");
                msg.Append("Numero de Cadastro Atualizados: " + numCartaoAtualizado.ToString() + "\n");
                msg.Append("Numero de Assinaturas enviadas: " + numAssinaturas.ToString() + "\n");
                msg.Append("\n\n\n");
                msg.Append("Total de registros: " + (numCartaoAtualizado + numCartaoNovo) + "\n");
                msg.Append("\n\n\n");
                msg.Append("SGC");
                mensagem.Body = msg.ToString();
                mensagem.IsBodyHtml = false;
                mail.Send(mensagem);

            }
            catch(SmtpException ex){
                MessageBox.Show(ex.StackTrace);
            }
            finally { 
                
            }
             * */
        }

        private void btnControle_Click(object sender, EventArgs e)
        {
            if (btnControle.Text == "Iniciar")
            {
                btnControle.Text = "Terminar";
                this.Refresh();
                timer1.Enabled = true;
                txDataInicio.Enabled = false;
                txDataFim.Enabled = false;
                
                return;
            } 

            if (btnControle.Text == "Terminar")
            {
                btnControle.Text = "Iniciar";
                Refresh();
                txDataInicio.Enabled = true;
                txDataFim.Enabled = true;
                return;
            }

            
        }

        
    }
}
