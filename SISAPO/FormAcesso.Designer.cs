namespace SISAPO
{
    partial class FormAcesso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAcesso));
            this.BtnAcesso = new System.Windows.Forms.Button();
            this.TxtUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtSenha = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnAcesso
            // 
            this.BtnAcesso.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAcesso.Location = new System.Drawing.Point(56, 168);
            this.BtnAcesso.Name = "BtnAcesso";
            this.BtnAcesso.Size = new System.Drawing.Size(109, 30);
            this.BtnAcesso.TabIndex = 5;
            this.BtnAcesso.Text = "&Acessar";
            this.BtnAcesso.UseVisualStyleBackColor = true;
            this.BtnAcesso.Click += new System.EventHandler(this.BtnAcesso_Click);
            // 
            // TxtUsuario
            // 
            this.TxtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUsuario.Location = new System.Drawing.Point(56, 68);
            this.TxtUsuario.Name = "TxtUsuario";
            this.TxtUsuario.Size = new System.Drawing.Size(238, 29);
            this.TxtUsuario.TabIndex = 2;
            this.TxtUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUsuario_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Usuário:";
            // 
            // TxtSenha
            // 
            this.TxtSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSenha.Location = new System.Drawing.Point(56, 123);
            this.TxtSenha.Name = "TxtSenha";
            this.TxtSenha.PasswordChar = '#';
            this.TxtSenha.Size = new System.Drawing.Size(238, 29);
            this.TxtSenha.TabIndex = 4;
            this.TxtSenha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSenha_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(53, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Senha:";
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelar.Location = new System.Drawing.Point(185, 168);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(109, 30);
            this.BtnCancelar.TabIndex = 6;
            this.BtnCancelar.Text = "&Cancelar";
            this.BtnCancelar.UseVisualStyleBackColor = true;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 31);
            this.label3.TabIndex = 0;
            this.label3.Text = "Formulário de acesso";
            // 
            // FormAcesso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(351, 223);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtSenha);
            this.Controls.Add(this.TxtUsuario);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.BtnAcesso);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormAcesso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormAcesso_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormAcesso_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtUsuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtSenha;
        private System.Windows.Forms.Button BtnAcesso;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.Label label3;
    }
}