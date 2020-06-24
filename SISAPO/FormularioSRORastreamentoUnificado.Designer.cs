namespace SISAPO
{
    partial class FormularioSRORastreamentoUnificado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioSRORastreamentoUnificado));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtEnderecoUrl = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 43);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(739, 419);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.webBrowser1_PreviewKeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TxtEnderecoUrl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(739, 43);
            this.panel1.TabIndex = 1;
            // 
            // TxtEnderecoUrl
            // 
            this.TxtEnderecoUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtEnderecoUrl.BackColor = System.Drawing.SystemColors.Control;
            this.TxtEnderecoUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEnderecoUrl.ForeColor = System.Drawing.Color.Maroon;
            this.TxtEnderecoUrl.Location = new System.Drawing.Point(12, 6);
            this.TxtEnderecoUrl.Name = "TxtEnderecoUrl";
            this.TxtEnderecoUrl.ReadOnly = true;
            this.TxtEnderecoUrl.Size = new System.Drawing.Size(715, 26);
            this.TxtEnderecoUrl.TabIndex = 23;
            this.TxtEnderecoUrl.TabStop = false;
            // 
            // FormularioSRORastreamentoUnificado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(739, 462);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormularioSRORastreamentoUnificado";
            this.ShowInTaskbar = false;
            this.Text = "SRO - Rastreamento Unificado";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormularioSRORastreamentoUnificado_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormularioSRORastreamentoUnificado_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtEnderecoUrl;
    }
}