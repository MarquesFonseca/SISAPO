namespace SISAPO
{
    partial class FormularioImpressaoEntregaObjetosOpcoesImpressao
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
            this.LblTituloFormulario = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxIncluirItensCaixaPostal = new System.Windows.Forms.CheckBox();
            this.checkBoxIncluirItensJaEntregues = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblTituloFormulario
            // 
            this.LblTituloFormulario.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTituloFormulario.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTituloFormulario.Location = new System.Drawing.Point(0, 0);
            this.LblTituloFormulario.Name = "LblTituloFormulario";
            this.LblTituloFormulario.Size = new System.Drawing.Size(520, 51);
            this.LblTituloFormulario.TabIndex = 0;
            this.LblTituloFormulario.Text = "Opções de impresão";
            this.LblTituloFormulario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.LblTituloFormulario);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(520, 51);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxIncluirItensCaixaPostal);
            this.groupBox1.Controls.Add(this.checkBoxIncluirItensJaEntregues);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(520, 321);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // checkBoxIncluirItensCaixaPostal
            // 
            this.checkBoxIncluirItensCaixaPostal.AutoSize = true;
            this.checkBoxIncluirItensCaixaPostal.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxIncluirItensCaixaPostal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxIncluirItensCaixaPostal.Location = new System.Drawing.Point(86, 96);
            this.checkBoxIncluirItensCaixaPostal.Name = "checkBoxIncluirItensCaixaPostal";
            this.checkBoxIncluirItensCaixaPostal.Size = new System.Drawing.Size(367, 34);
            this.checkBoxIncluirItensCaixaPostal.TabIndex = 0;
            this.checkBoxIncluirItensCaixaPostal.Text = "Incluir itens de caixa postal?";
            this.checkBoxIncluirItensCaixaPostal.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncluirItensJaEntregues
            // 
            this.checkBoxIncluirItensJaEntregues.AutoSize = true;
            this.checkBoxIncluirItensJaEntregues.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxIncluirItensJaEntregues.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxIncluirItensJaEntregues.Location = new System.Drawing.Point(86, 33);
            this.checkBoxIncluirItensJaEntregues.Name = "checkBoxIncluirItensJaEntregues";
            this.checkBoxIncluirItensJaEntregues.Size = new System.Drawing.Size(337, 34);
            this.checkBoxIncluirItensJaEntregues.TabIndex = 0;
            this.checkBoxIncluirItensJaEntregues.Text = "Incluir itens já entregues?";
            this.checkBoxIncluirItensJaEntregues.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.btnAlterar);
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 321);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(520, 51);
            this.panel2.TabIndex = 2;
            // 
            // btnAlterar
            // 
            this.btnAlterar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAlterar.Location = new System.Drawing.Point(337, 11);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(175, 31);
            this.btnAlterar.TabIndex = 1;
            this.btnAlterar.Text = "&Confirmar";
            this.btnAlterar.UseVisualStyleBackColor = true;
            this.btnAlterar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(156, 11);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(175, 31);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FormularioImpressaoEntregaObjetosOpcoesImpressao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 372);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(526, 401);
            this.Name = "FormularioImpressaoEntregaObjetosOpcoesImpressao";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selecione as opções desejadas para impressão";
            this.Load += new System.EventHandler(this.FormularioImpressaoEntregaObjetosOpcoesImpressao_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblTituloFormulario;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.CheckBox checkBoxIncluirItensJaEntregues;
        private System.Windows.Forms.CheckBox checkBoxIncluirItensCaixaPostal;
    }
}