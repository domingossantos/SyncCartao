namespace AtualizadorCartao
{
    partial class PrincipalForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrincipalForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txDataFim = new System.Windows.Forms.MaskedTextBox();
            this.txDataInicio = new System.Windows.Forms.MaskedTextBox();
            this.btnControle = new System.Windows.Forms.Button();
            this.Termino = new System.Windows.Forms.Label();
            this.Inicio = new System.Windows.Forms.Label();
            this.lbCiclos = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.maximizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 377);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(777, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel1.Text = "Versão 1.0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel2.Text = "-";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(95, 17);
            this.toolStripStatusLabel3.Text = "Ultima Verificação:";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(19, 17);
            this.toolStripStatusLabel4.Text = "...";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txDataFim);
            this.panel1.Controls.Add(this.txDataInicio);
            this.panel1.Controls.Add(this.btnControle);
            this.panel1.Controls.Add(this.Termino);
            this.panel1.Controls.Add(this.Inicio);
            this.panel1.Controls.Add(this.lbCiclos);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(525, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 377);
            this.panel1.TabIndex = 1;
            // 
            // txDataFim
            // 
            this.txDataFim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txDataFim.Location = new System.Drawing.Point(12, 192);
            this.txDataFim.Mask = "00:00";
            this.txDataFim.Name = "txDataFim";
            this.txDataFim.Size = new System.Drawing.Size(85, 26);
            this.txDataFim.TabIndex = 8;
            this.txDataFim.ValidatingType = typeof(System.DateTime);
            // 
            // txDataInicio
            // 
            this.txDataInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txDataInicio.Location = new System.Drawing.Point(12, 124);
            this.txDataInicio.Mask = "00:00";
            this.txDataInicio.Name = "txDataInicio";
            this.txDataInicio.Size = new System.Drawing.Size(85, 26);
            this.txDataInicio.TabIndex = 7;
            // 
            // btnControle
            // 
            this.btnControle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnControle.Location = new System.Drawing.Point(11, 239);
            this.btnControle.Name = "btnControle";
            this.btnControle.Size = new System.Drawing.Size(109, 40);
            this.btnControle.TabIndex = 6;
            this.btnControle.Text = "Iniciar";
            this.btnControle.UseVisualStyleBackColor = true;
            this.btnControle.Click += new System.EventHandler(this.btnControle_Click);
            // 
            // Termino
            // 
            this.Termino.AutoSize = true;
            this.Termino.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Termino.Location = new System.Drawing.Point(8, 169);
            this.Termino.Name = "Termino";
            this.Termino.Size = new System.Drawing.Size(144, 20);
            this.Termino.TabIndex = 5;
            this.Termino.Text = "Horário de Término";
            // 
            // Inicio
            // 
            this.Inicio.AutoSize = true;
            this.Inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Inicio.Location = new System.Drawing.Point(7, 101);
            this.Inicio.Name = "Inicio";
            this.Inicio.Size = new System.Drawing.Size(124, 20);
            this.Inicio.TabIndex = 4;
            this.Inicio.Text = "Horário de Inicio";
            // 
            // lbCiclos
            // 
            this.lbCiclos.AutoSize = true;
            this.lbCiclos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCiclos.Location = new System.Drawing.Point(6, 36);
            this.lbCiclos.Name = "lbCiclos";
            this.lbCiclos.Size = new System.Drawing.Size(21, 24);
            this.lbCiclos.TabIndex = 1;
            this.lbCiclos.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nº de ciclos de atualização:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(525, 377);
            this.dataGridView1.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.maximizarToolStripMenuItem,
            this.fecharToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(122, 48);
            // 
            // maximizarToolStripMenuItem
            // 
            this.maximizarToolStripMenuItem.Name = "maximizarToolStripMenuItem";
            this.maximizarToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.maximizarToolStripMenuItem.Text = "Maximizar";
            this.maximizarToolStripMenuItem.Click += new System.EventHandler(this.maximizarToolStripMenuItem_Click);
            // 
            // fecharToolStripMenuItem
            // 
            this.fecharToolStripMenuItem.Name = "fecharToolStripMenuItem";
            this.fecharToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.fecharToolStripMenuItem.Text = "Fechar";
            this.fecharToolStripMenuItem.Click += new System.EventHandler(this.fecharToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PrincipalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 399);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PrincipalForm";
            this.Text = "Atualizador de Cartão";
            this.Load += new System.EventHandler(this.PrincipalForm_Load);
            this.Resize += new System.EventHandler(this.PrincipalForm_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem maximizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fecharToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCiclos;
        private System.Windows.Forms.Label Termino;
        private System.Windows.Forms.Label Inicio;
        private System.Windows.Forms.Button btnControle;
        private System.Windows.Forms.MaskedTextBox txDataFim;
        private System.Windows.Forms.MaskedTextBox txDataInicio;
    }
}

