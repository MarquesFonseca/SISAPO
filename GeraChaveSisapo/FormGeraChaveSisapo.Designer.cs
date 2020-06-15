namespace GeraChaveSisapo
{
    partial class FormGeraChaveSisapo
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
            this.BtnCopiarFechar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtAcesso = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnGerarAcesso = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Data_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // BtnCopiarFechar
            // 
            this.BtnCopiarFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCopiarFechar.Location = new System.Drawing.Point(185, 172);
            this.BtnCopiarFechar.Name = "BtnCopiarFechar";
            this.BtnCopiarFechar.Size = new System.Drawing.Size(146, 30);
            this.BtnCopiarFechar.TabIndex = 6;
            this.BtnCopiarFechar.Text = "&Copiar / Fechar";
            this.BtnCopiarFechar.UseVisualStyleBackColor = true;
            this.BtnCopiarFechar.Click += new System.EventHandler(this.BtnCopiarFechar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Informe uma data:";
            // 
            // TxtAcesso
            // 
            this.TxtAcesso.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAcesso.Location = new System.Drawing.Point(21, 124);
            this.TxtAcesso.Name = "TxtAcesso";
            this.TxtAcesso.Size = new System.Drawing.Size(310, 29);
            this.TxtAcesso.TabIndex = 5;
            this.TxtAcesso.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAcesso_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Acesso";
            // 
            // BtnGerarAcesso
            // 
            this.BtnGerarAcesso.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGerarAcesso.Location = new System.Drawing.Point(185, 72);
            this.BtnGerarAcesso.Name = "BtnGerarAcesso";
            this.BtnGerarAcesso.Size = new System.Drawing.Size(146, 30);
            this.BtnGerarAcesso.TabIndex = 3;
            this.BtnGerarAcesso.Text = "&Gerar acesso";
            this.BtnGerarAcesso.UseVisualStyleBackColor = true;
            this.BtnGerarAcesso.Click += new System.EventHandler(this.BtnGerarAcesso_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(320, 31);
            this.label3.TabIndex = 0;
            this.label3.Text = "Formulário gera acesso";
            // 
            // Data_dateTimePicker
            // 
            this.Data_dateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Data_dateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Data_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Data_dateTimePicker.Location = new System.Drawing.Point(21, 72);
            this.Data_dateTimePicker.Name = "Data_dateTimePicker";
            this.Data_dateTimePicker.Size = new System.Drawing.Size(155, 29);
            this.Data_dateTimePicker.TabIndex = 2;
            // 
            // FormGeraChaveSisapo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(351, 223);
            this.Controls.Add(this.Data_dateTimePicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtAcesso);
            this.Controls.Add(this.BtnGerarAcesso);
            this.Controls.Add(this.BtnCopiarFechar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "FormGeraChaveSisapo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormGeraChaveSisapo";
            this.Load += new System.EventHandler(this.FormGeraChaveSisapo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGeraChaveSisapo_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnCopiarFechar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtAcesso;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnGerarAcesso;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DateTimePicker Data_dateTimePicker;
    }
}

