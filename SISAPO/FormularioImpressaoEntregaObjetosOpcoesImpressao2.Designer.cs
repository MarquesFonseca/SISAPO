namespace SISAPO
{
    partial class FormularioImpressaoEntregaObjetosOpcoesImpressao2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioImpressaoEntregaObjetosOpcoesImpressao2));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl4 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.checkBoxImprimirVariosPorFolha = new System.Windows.Forms.RadioButton();
            this.checkBoxImprimirUmPorFolha = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBoxOrdemCrescenteDescrecente = new System.Windows.Forms.ComboBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.comboBoxTipoOrdenacao = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tabControl4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl4);
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.tabControl2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(778, 362);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tabControl4
            // 
            this.tabControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl4.Controls.Add(this.tabPage5);
            this.tabControl4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl4.Location = new System.Drawing.Point(28, 157);
            this.tabControl4.Name = "tabControl4";
            this.tabControl4.SelectedIndex = 0;
            this.tabControl4.Size = new System.Drawing.Size(718, 102);
            this.tabControl4.TabIndex = 2;
            this.tabControl4.TabStop = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.checkBoxImprimirVariosPorFolha);
            this.tabPage5.Controls.Add(this.checkBoxImprimirUmPorFolha);
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(710, 69);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Escolha quantos itens por folha";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // checkBoxImprimirVariosPorFolha
            // 
            this.checkBoxImprimirVariosPorFolha.AutoSize = true;
            this.checkBoxImprimirVariosPorFolha.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxImprimirVariosPorFolha.Location = new System.Drawing.Point(247, 22);
            this.checkBoxImprimirVariosPorFolha.Name = "checkBoxImprimirVariosPorFolha";
            this.checkBoxImprimirVariosPorFolha.Size = new System.Drawing.Size(197, 29);
            this.checkBoxImprimirVariosPorFolha.TabIndex = 1;
            this.checkBoxImprimirVariosPorFolha.Text = "Vários por folha";
            this.checkBoxImprimirVariosPorFolha.UseVisualStyleBackColor = true;
            this.checkBoxImprimirVariosPorFolha.CheckedChanged += new System.EventHandler(this.checkBoxImprimirVariosPorFolha_CheckedChanged);
            // 
            // checkBoxImprimirUmPorFolha
            // 
            this.checkBoxImprimirUmPorFolha.AutoSize = true;
            this.checkBoxImprimirUmPorFolha.Checked = true;
            this.checkBoxImprimirUmPorFolha.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxImprimirUmPorFolha.Location = new System.Drawing.Point(28, 22);
            this.checkBoxImprimirUmPorFolha.Name = "checkBoxImprimirUmPorFolha";
            this.checkBoxImprimirUmPorFolha.Size = new System.Drawing.Size(164, 29);
            this.checkBoxImprimirUmPorFolha.TabIndex = 0;
            this.checkBoxImprimirUmPorFolha.TabStop = true;
            this.checkBoxImprimirUmPorFolha.Text = "Um por folha";
            this.checkBoxImprimirUmPorFolha.UseVisualStyleBackColor = true;
            this.checkBoxImprimirUmPorFolha.CheckedChanged += new System.EventHandler(this.checkBoxImprimirUmPorFolha_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(390, 30);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(356, 102);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboBoxOrdemCrescenteDescrecente);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(348, 69);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Escolha uma ordem";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBoxOrdemCrescenteDescrecente
            // 
            this.comboBoxOrdemCrescenteDescrecente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrdemCrescenteDescrecente.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxOrdemCrescenteDescrecente.ForeColor = System.Drawing.Color.Red;
            this.comboBoxOrdemCrescenteDescrecente.FormattingEnabled = true;
            this.comboBoxOrdemCrescenteDescrecente.Items.AddRange(new object[] {
            "Crescente      [ A-Z ]",
            "Descrescente [ Z-A ]"});
            this.comboBoxOrdemCrescenteDescrecente.Location = new System.Drawing.Point(16, 24);
            this.comboBoxOrdemCrescenteDescrecente.Name = "comboBoxOrdemCrescenteDescrecente";
            this.comboBoxOrdemCrescenteDescrecente.Size = new System.Drawing.Size(311, 33);
            this.comboBoxOrdemCrescenteDescrecente.TabIndex = 0;
            this.comboBoxOrdemCrescenteDescrecente.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrdemCrescenteDescrecente_SelectedIndexChanged);
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl2.Location = new System.Drawing.Point(28, 30);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(356, 102);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.comboBoxTipoOrdenacao);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(348, 69);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Escolha um tipo de ordenação";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // comboBoxTipoOrdenacao
            // 
            this.comboBoxTipoOrdenacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipoOrdenacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTipoOrdenacao.ForeColor = System.Drawing.Color.Red;
            this.comboBoxTipoOrdenacao.FormattingEnabled = true;
            this.comboBoxTipoOrdenacao.Items.AddRange(new object[] {
            "Nome do cliente",
            "Data de lançamento"});
            this.comboBoxTipoOrdenacao.Location = new System.Drawing.Point(16, 24);
            this.comboBoxTipoOrdenacao.Name = "comboBoxTipoOrdenacao";
            this.comboBoxTipoOrdenacao.Size = new System.Drawing.Size(311, 33);
            this.comboBoxTipoOrdenacao.TabIndex = 0;
            this.comboBoxTipoOrdenacao.SelectedIndexChanged += new System.EventHandler(this.comboBoxTipoOrdenacao_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.btnAlterar);
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 311);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(778, 51);
            this.panel2.TabIndex = 1;
            // 
            // btnAlterar
            // 
            this.btnAlterar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAlterar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlterar.Location = new System.Drawing.Point(6, 6);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(267, 39);
            this.btnAlterar.TabIndex = 0;
            this.btnAlterar.Text = "[Enter] - &Confirmar";
            this.btnAlterar.UseVisualStyleBackColor = true;
            this.btnAlterar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(505, 6);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(267, 39);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "[Esc] - Cancela&r";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FormularioImpressaoEntregaObjetosOpcoesImpressao2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 362);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(526, 401);
            this.Name = "FormularioImpressaoEntregaObjetosOpcoesImpressao2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selecione as opções desejadas para impressão";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormularioImpressaoEntregaObjetosOpcoesImpressao2_FormClosed);
            this.Load += new System.EventHandler(this.FormularioImpressaoEntregaObjetosOpcoesImpressao2_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormularioImpressaoEntregaObjetosOpcoesImpressao2_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.tabControl4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TabControl tabControl4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.RadioButton checkBoxImprimirVariosPorFolha;
        private System.Windows.Forms.RadioButton checkBoxImprimirUmPorFolha;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox comboBoxOrdemCrescenteDescrecente;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox comboBoxTipoOrdenacao;
    }
}