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
            this.groupBoxQuantidadeFolhas = new System.Windows.Forms.GroupBox();
            this.checkBoxImprimirUmPorFolha = new System.Windows.Forms.CheckBox();
            this.checkBoxImprimirVariosPorFolha = new System.Windows.Forms.CheckBox();
            this.groupBoxTipoOrdenacao = new System.Windows.Forms.GroupBox();
            this.checkBoxOrdenacaoPorNomeDestinatario = new System.Windows.Forms.CheckBox();
            this.checkBoxOrdenacaoPorDataLancamento = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBoxQuantidadeFolhas.SuspendLayout();
            this.groupBoxTipoOrdenacao.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBoxQuantidadeFolhas);
            this.groupBox1.Controls.Add(this.groupBoxTipoOrdenacao);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(684, 362);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBoxQuantidadeFolhas
            // 
            this.groupBoxQuantidadeFolhas.Controls.Add(this.checkBoxImprimirUmPorFolha);
            this.groupBoxQuantidadeFolhas.Controls.Add(this.checkBoxImprimirVariosPorFolha);
            this.groupBoxQuantidadeFolhas.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxQuantidadeFolhas.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBoxQuantidadeFolhas.Location = new System.Drawing.Point(6, 143);
            this.groupBoxQuantidadeFolhas.Name = "groupBoxQuantidadeFolhas";
            this.groupBoxQuantidadeFolhas.Size = new System.Drawing.Size(530, 100);
            this.groupBoxQuantidadeFolhas.TabIndex = 1;
            this.groupBoxQuantidadeFolhas.TabStop = false;
            this.groupBoxQuantidadeFolhas.Text = "Quantidade por folha:";
            // 
            // checkBoxImprimirUmPorFolha
            // 
            this.checkBoxImprimirUmPorFolha.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxImprimirUmPorFolha.AutoSize = true;
            this.checkBoxImprimirUmPorFolha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxImprimirUmPorFolha.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxImprimirUmPorFolha.Location = new System.Drawing.Point(17, 46);
            this.checkBoxImprimirUmPorFolha.Name = "checkBoxImprimirUmPorFolha";
            this.checkBoxImprimirUmPorFolha.Size = new System.Drawing.Size(204, 39);
            this.checkBoxImprimirUmPorFolha.TabIndex = 0;
            this.checkBoxImprimirUmPorFolha.Text = "1 item por folha";
            this.checkBoxImprimirUmPorFolha.UseVisualStyleBackColor = true;
            this.checkBoxImprimirUmPorFolha.CheckedChanged += new System.EventHandler(this.checkBoxImprimirUmPorFolha_CheckedChanged);
            // 
            // checkBoxImprimirVariosPorFolha
            // 
            this.checkBoxImprimirVariosPorFolha.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxImprimirVariosPorFolha.AutoSize = true;
            this.checkBoxImprimirVariosPorFolha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxImprimirVariosPorFolha.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxImprimirVariosPorFolha.Location = new System.Drawing.Point(237, 46);
            this.checkBoxImprimirVariosPorFolha.Name = "checkBoxImprimirVariosPorFolha";
            this.checkBoxImprimirVariosPorFolha.Size = new System.Drawing.Size(270, 39);
            this.checkBoxImprimirVariosPorFolha.TabIndex = 0;
            this.checkBoxImprimirVariosPorFolha.Text = "Vários itens por folha";
            this.checkBoxImprimirVariosPorFolha.UseVisualStyleBackColor = true;
            this.checkBoxImprimirVariosPorFolha.CheckedChanged += new System.EventHandler(this.checkBoxImprimirVariosPorFolha_CheckedChanged);
            // 
            // groupBoxTipoOrdenacao
            // 
            this.groupBoxTipoOrdenacao.Controls.Add(this.checkBoxOrdenacaoPorNomeDestinatario);
            this.groupBoxTipoOrdenacao.Controls.Add(this.checkBoxOrdenacaoPorDataLancamento);
            this.groupBoxTipoOrdenacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTipoOrdenacao.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBoxTipoOrdenacao.Location = new System.Drawing.Point(6, 19);
            this.groupBoxTipoOrdenacao.Name = "groupBoxTipoOrdenacao";
            this.groupBoxTipoOrdenacao.Size = new System.Drawing.Size(668, 100);
            this.groupBoxTipoOrdenacao.TabIndex = 1;
            this.groupBoxTipoOrdenacao.TabStop = false;
            this.groupBoxTipoOrdenacao.Text = "Tipos de ordenação:";
            // 
            // checkBoxOrdenacaoPorNomeDestinatario
            // 
            this.checkBoxOrdenacaoPorNomeDestinatario.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxOrdenacaoPorNomeDestinatario.AutoSize = true;
            this.checkBoxOrdenacaoPorNomeDestinatario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxOrdenacaoPorNomeDestinatario.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxOrdenacaoPorNomeDestinatario.Location = new System.Drawing.Point(17, 43);
            this.checkBoxOrdenacaoPorNomeDestinatario.Name = "checkBoxOrdenacaoPorNomeDestinatario";
            this.checkBoxOrdenacaoPorNomeDestinatario.Size = new System.Drawing.Size(317, 39);
            this.checkBoxOrdenacaoPorNomeDestinatario.TabIndex = 0;
            this.checkBoxOrdenacaoPorNomeDestinatario.Text = "Por nome do destinatário";
            this.checkBoxOrdenacaoPorNomeDestinatario.UseVisualStyleBackColor = true;
            this.checkBoxOrdenacaoPorNomeDestinatario.CheckedChanged += new System.EventHandler(this.checkBoxOrdenacaoPorNomeDestinatario_CheckedChanged);
            // 
            // checkBoxOrdenacaoPorDataLancamento
            // 
            this.checkBoxOrdenacaoPorDataLancamento.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxOrdenacaoPorDataLancamento.AutoSize = true;
            this.checkBoxOrdenacaoPorDataLancamento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxOrdenacaoPorDataLancamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxOrdenacaoPorDataLancamento.Location = new System.Drawing.Point(350, 43);
            this.checkBoxOrdenacaoPorDataLancamento.Name = "checkBoxOrdenacaoPorDataLancamento";
            this.checkBoxOrdenacaoPorDataLancamento.Size = new System.Drawing.Size(299, 39);
            this.checkBoxOrdenacaoPorDataLancamento.TabIndex = 0;
            this.checkBoxOrdenacaoPorDataLancamento.Text = "Por data de lançamento";
            this.checkBoxOrdenacaoPorDataLancamento.UseVisualStyleBackColor = true;
            this.checkBoxOrdenacaoPorDataLancamento.CheckedChanged += new System.EventHandler(this.checkBoxOrdenacaoPorDataLancamento_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.btnAlterar);
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 311);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(684, 51);
            this.panel2.TabIndex = 2;
            // 
            // btnAlterar
            // 
            this.btnAlterar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAlterar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlterar.Location = new System.Drawing.Point(501, 6);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(175, 39);
            this.btnAlterar.TabIndex = 1;
            this.btnAlterar.Text = "Confirmar";
            this.btnAlterar.UseVisualStyleBackColor = true;
            this.btnAlterar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(6, 6);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(175, 39);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FormularioImpressaoEntregaObjetosOpcoesImpressao2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 362);
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
            this.groupBoxQuantidadeFolhas.ResumeLayout(false);
            this.groupBoxQuantidadeFolhas.PerformLayout();
            this.groupBoxTipoOrdenacao.ResumeLayout(false);
            this.groupBoxTipoOrdenacao.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.CheckBox checkBoxImprimirUmPorFolha;
        private System.Windows.Forms.CheckBox checkBoxImprimirVariosPorFolha;
        private System.Windows.Forms.GroupBox groupBoxQuantidadeFolhas;
        private System.Windows.Forms.GroupBox groupBoxTipoOrdenacao;
        private System.Windows.Forms.CheckBox checkBoxOrdenacaoPorNomeDestinatario;
        private System.Windows.Forms.CheckBox checkBoxOrdenacaoPorDataLancamento;
    }
}