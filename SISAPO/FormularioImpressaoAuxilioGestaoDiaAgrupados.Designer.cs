﻿namespace SISAPO
{
    partial class FormularioImpressaoAuxilioGestaoDiaAgrupados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioImpressaoAuxilioGestaoDiaAgrupados));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnImprimirPagina = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 51);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(939, 611);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnImprimirPagina);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 51);
            this.panel1.TabIndex = 2;
            // 
            // BtnImprimirPagina
            // 
            this.BtnImprimirPagina.BackgroundImage = global::SISAPO.Properties.Resources.if_BT_printer_905556;
            this.BtnImprimirPagina.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnImprimirPagina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnImprimirPagina.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnImprimirPagina.Location = new System.Drawing.Point(0, 0);
            this.BtnImprimirPagina.Name = "BtnImprimirPagina";
            this.BtnImprimirPagina.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.BtnImprimirPagina.Size = new System.Drawing.Size(939, 51);
            this.BtnImprimirPagina.TabIndex = 0;
            this.BtnImprimirPagina.Tag = "Imprimir lista";
            this.BtnImprimirPagina.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnImprimirPagina.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnImprimirPagina.UseVisualStyleBackColor = true;
            this.BtnImprimirPagina.Click += new System.EventHandler(this.BtnImprimirPagina_Click);
            // 
            // FormularioImpressaoAuxilioGestaoDiaAgrupados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(939, 662);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "FormularioImpressaoAuxilioGestaoDiaAgrupados";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulário impressão auxílio gestão do dia";
            this.Load += new System.EventHandler(this.FormularioImpressaoAuxilioGestaoDiaAgrupados_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormularioSRORastreamentoUnificado_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnImprimirPagina;

    }
}