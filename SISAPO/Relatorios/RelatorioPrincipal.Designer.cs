namespace SISAPO
{
    partial class RelatorioPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RelatorioPrincipal));
            this.BtnTiposPostaisMaisUtilizados = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelDataInicial = new System.Windows.Forms.Label();
            this.DataInicialQtdLancamentos = new System.Windows.Forms.DateTimePicker();
            this.DataFinalQtdLancamentos = new System.Windows.Forms.DateTimePicker();
            this.labelDataFinal = new System.Windows.Forms.Label();
            this.BtnQtdDeLancamentos = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.DataInicialTiposPostaisMaisUtilizados = new System.Windows.Forms.DateTimePicker();
            this.DataFinalTiposPostaisMaisUtilizados = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnTiposPostaisMaisUtilizados
            // 
            this.BtnTiposPostaisMaisUtilizados.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTiposPostaisMaisUtilizados.Location = new System.Drawing.Point(19, 135);
            this.BtnTiposPostaisMaisUtilizados.Name = "BtnTiposPostaisMaisUtilizados";
            this.BtnTiposPostaisMaisUtilizados.Size = new System.Drawing.Size(223, 37);
            this.BtnTiposPostaisMaisUtilizados.TabIndex = 1;
            this.BtnTiposPostaisMaisUtilizados.Text = "Exibir relatório";
            this.BtnTiposPostaisMaisUtilizados.UseVisualStyleBackColor = true;
            this.BtnTiposPostaisMaisUtilizados.Click += new System.EventHandler(this.BtnTiposPostaisMaisUtilizados_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(710, 457);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.DataInicialTiposPostaisMaisUtilizados);
            this.tabPage1.Controls.Add(this.DataFinalTiposPostaisMaisUtilizados);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.BtnTiposPostaisMaisUtilizados);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(702, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tipos postais mais utilizados";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.BtnQtdDeLancamentos);
            this.tabPage2.Controls.Add(this.labelDataInicial);
            this.tabPage2.Controls.Add(this.DataInicialQtdLancamentos);
            this.tabPage2.Controls.Add(this.DataFinalQtdLancamentos);
            this.tabPage2.Controls.Add(this.labelDataFinal);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(702, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Qtd. de lançamentos";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelDataInicial
            // 
            this.labelDataInicial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDataInicial.AutoSize = true;
            this.labelDataInicial.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelDataInicial.Location = new System.Drawing.Point(14, 70);
            this.labelDataInicial.Name = "labelDataInicial";
            this.labelDataInicial.Size = new System.Drawing.Size(100, 20);
            this.labelDataInicial.TabIndex = 14;
            this.labelDataInicial.Text = "Data Inicial";
            // 
            // DataInicialQtdLancamentos
            // 
            this.DataInicialQtdLancamentos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DataInicialQtdLancamentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataInicialQtdLancamentos.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DataInicialQtdLancamentos.Location = new System.Drawing.Point(18, 93);
            this.DataInicialQtdLancamentos.Name = "DataInicialQtdLancamentos";
            this.DataInicialQtdLancamentos.Size = new System.Drawing.Size(103, 27);
            this.DataInicialQtdLancamentos.TabIndex = 15;
            // 
            // DataFinalQtdLancamentos
            // 
            this.DataFinalQtdLancamentos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DataFinalQtdLancamentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataFinalQtdLancamentos.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DataFinalQtdLancamentos.Location = new System.Drawing.Point(138, 93);
            this.DataFinalQtdLancamentos.Name = "DataFinalQtdLancamentos";
            this.DataFinalQtdLancamentos.Size = new System.Drawing.Size(103, 27);
            this.DataFinalQtdLancamentos.TabIndex = 17;
            // 
            // labelDataFinal
            // 
            this.labelDataFinal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDataFinal.AutoSize = true;
            this.labelDataFinal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelDataFinal.Location = new System.Drawing.Point(134, 71);
            this.labelDataFinal.Name = "labelDataFinal";
            this.labelDataFinal.Size = new System.Drawing.Size(87, 20);
            this.labelDataFinal.TabIndex = 16;
            this.labelDataFinal.Text = "Data final";
            // 
            // BtnQtdDeLancamentos
            // 
            this.BtnQtdDeLancamentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnQtdDeLancamentos.Location = new System.Drawing.Point(18, 138);
            this.BtnQtdDeLancamentos.Name = "BtnQtdDeLancamentos";
            this.BtnQtdDeLancamentos.Size = new System.Drawing.Size(223, 37);
            this.BtnQtdDeLancamentos.TabIndex = 18;
            this.BtnQtdDeLancamentos.Text = "Exibir relatório";
            this.BtnQtdDeLancamentos.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(696, 53);
            this.label2.TabIndex = 20;
            this.label2.Text = "Utilize o filtro abaixo para ter um período especifico e verificar os tipos posta" +
    "is mais utilizados.";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 53);
            this.panel1.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(15, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "Data Inicial";
            // 
            // DataInicialTiposPostaisMaisUtilizados
            // 
            this.DataInicialTiposPostaisMaisUtilizados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DataInicialTiposPostaisMaisUtilizados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataInicialTiposPostaisMaisUtilizados.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DataInicialTiposPostaisMaisUtilizados.Location = new System.Drawing.Point(19, 92);
            this.DataInicialTiposPostaisMaisUtilizados.Name = "DataInicialTiposPostaisMaisUtilizados";
            this.DataInicialTiposPostaisMaisUtilizados.Size = new System.Drawing.Size(103, 27);
            this.DataInicialTiposPostaisMaisUtilizados.TabIndex = 23;
            // 
            // DataFinalTiposPostaisMaisUtilizados
            // 
            this.DataFinalTiposPostaisMaisUtilizados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DataFinalTiposPostaisMaisUtilizados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataFinalTiposPostaisMaisUtilizados.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DataFinalTiposPostaisMaisUtilizados.Location = new System.Drawing.Point(139, 92);
            this.DataFinalTiposPostaisMaisUtilizados.Name = "DataFinalTiposPostaisMaisUtilizados";
            this.DataFinalTiposPostaisMaisUtilizados.Size = new System.Drawing.Size(103, 27);
            this.DataFinalTiposPostaisMaisUtilizados.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(135, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 20);
            this.label4.TabIndex = 24;
            this.label4.Text = "Data final";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(696, 53);
            this.panel3.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(696, 53);
            this.label5.TabIndex = 20;
            this.label5.Text = "Utilize o filtro abaixo para ter um período especifico e verificar as quantidades" +
    " de lançamento por dia.";
            // 
            // RelatorioPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 457);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RelatorioPrincipal";
            this.Text = "RelatorioTeste";
            this.Load += new System.EventHandler(this.RelatorioPrincipal_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnTiposPostaisMaisUtilizados;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DateTimePicker DataInicialTiposPostaisMaisUtilizados;
        public System.Windows.Forms.DateTimePicker DataFinalTiposPostaisMaisUtilizados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnQtdDeLancamentos;
        private System.Windows.Forms.Label labelDataInicial;
        public System.Windows.Forms.DateTimePicker DataInicialQtdLancamentos;
        public System.Windows.Forms.DateTimePicker DataFinalQtdLancamentos;
        private System.Windows.Forms.Label labelDataFinal;
    }
}