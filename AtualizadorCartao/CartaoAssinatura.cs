using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtualizadorCartao
{
    class CartaoAssinatura
    {
        private String nrCartao;

        public String NrCartao
        {
            get { return nrCartao; }
            set { nrCartao = value; }
        }
        private DateTime dtCadastro;

        public DateTime DtCadastro
        {
            get { return dtCadastro; }
            set { dtCadastro = value; }
        }
        private String nmCartao;

        public String NmCartao
        {
            get { return nmCartao; }
            set { nmCartao = value; }
        }
        private String nrCPF;

        public String NrCPF
        {
            get { return nrCPF; }
            set { nrCPF = value; }
        }
        private String dsEndereco;

        public String DsEndereco
        {
            get { return dsEndereco; }
            set { dsEndereco = value; }
        }
        private String dsBairro;

        public String DsBairro
        {
            get { return dsBairro; }
            set { dsBairro = value; }
        }
        private String nmCidade;

        public String NmCidade
        {
            get { return nmCidade; }
            set { nmCidade = value; }
        }
        private String nrCEP;

        public String NrCEP
        {
            get { return nrCEP; }
            set { nrCEP = value; }
        }
        private String sgUF;

        public String SgUF
        {
            get { return sgUF; }
            set { sgUF = value; }
        }
        private Nullable<DateTime> dtNascimento;

        public Nullable<DateTime> DtNascimento
        {
            get { return dtNascimento; }
            set { dtNascimento = value; }
        }


        private String nrRG;

        public String NrRG
        {
            get { return nrRG; }
            set { nrRG = value; }
        }
        private Nullable<DateTime> dtExpedicao;

        public Nullable<DateTime> DtExpedicao
        {
            get { return dtExpedicao; }
            set { dtExpedicao = value; }
        }


        private byte[] biRGFrente;

        public byte[] BiRGFrente
        {
            get { return biRGFrente; }
            set { biRGFrente = value; }
        }
        private byte[] biRGVerso;

        public byte[] BiRGVerso
        {
            get { return biRGVerso; }
            set { biRGVerso = value; }
        }
        private String dsOrgaoEmissor;

        public String DsOrgaoEmissor
        {
            get { return dsOrgaoEmissor; }
            set { dsOrgaoEmissor = value; }
        }
        private String nrFones;

        public String NrFones
        {
            get { return nrFones; }
            set { nrFones = value; }
        }
        private char tpCartao;

        public char TpCartao
        {
            get { return tpCartao; }
            set { tpCartao = value; }
        }

        private DateTime dtRenovacao;

        public DateTime DtRenovacao
        {
            get { return dtRenovacao; }
            set { dtRenovacao = value; }
        }


        private byte[] biFotoCartao;

        public byte[] BiFotoCartao
        {
            get { return biFotoCartao; }
            set { biFotoCartao = value; }
        }

        private int idCartorio;

        public int IdCartorio
        {
            get { return idCartorio; }
            set { idCartorio = value; }
        }

        private int cdTipoRG;

        public int CdTipoRG
        {
            get { return cdTipoRG; }
            set { cdTipoRG = value; }
        }
        private string dsObservacao;

        public string DsObservacao
        {
            get { return dsObservacao; }
            set { dsObservacao = value; }
        }

        private string dsEmail;

        public string DsEmail
        {
            get { return dsEmail; }
            set { dsEmail = value; }
        }
    }
}
