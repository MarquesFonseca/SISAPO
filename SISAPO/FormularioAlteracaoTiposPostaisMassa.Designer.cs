namespace SISAPO
{
    partial class FormularioAlteracaoTiposPostaisMassa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioAlteracaoTiposPostaisMassa));
            this.tabControlPrincipal = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.PrazoDestinatarioCaixaPostalUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.PrazoDestinatarioCaidaPedidaUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.PrazoRemetenteCaixaPostalUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.PrazoRemetenteCaidaPedidaUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxTipoClassificacao = new System.Windows.Forms.ComboBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnGravar = new System.Windows.Forms.Button();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.tabControl4 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.DataInicial_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.radioButtonAPartirDoDia = new System.Windows.Forms.RadioButton();
            this.radioButtonTodos = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.LblProgresso1 = new System.Windows.Forms.Label();
            this.LblProgresso2 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.tabControlPrincipal.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrazoDestinatarioCaixaPostalUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrazoDestinatarioCaidaPedidaUpDown)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrazoRemetenteCaixaPostalUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrazoRemetenteCaidaPedidaUpDown)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabControl4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlPrincipal
            // 
            this.tabControlPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlPrincipal.Controls.Add(this.tabPage1);
            this.tabControlPrincipal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlPrincipal.Location = new System.Drawing.Point(12, 110);
            this.tabControlPrincipal.Name = "tabControlPrincipal";
            this.tabControlPrincipal.SelectedIndex = 0;
            this.tabControlPrincipal.Size = new System.Drawing.Size(360, 155);
            this.tabControlPrincipal.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.PrazoDestinatarioCaixaPostalUpDown);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.PrazoDestinatarioCaidaPedidaUpDown);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(352, 122);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ao Destinatário";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // PrazoDestinatarioCaixaPostalUpDown
            // 
            this.PrazoDestinatarioCaixaPostalUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrazoDestinatarioCaixaPostalUpDown.Location = new System.Drawing.Point(16, 82);
            this.PrazoDestinatarioCaixaPostalUpDown.Name = "PrazoDestinatarioCaixaPostalUpDown";
            this.PrazoDestinatarioCaixaPostalUpDown.Size = new System.Drawing.Size(146, 29);
            this.PrazoDestinatarioCaixaPostalUpDown.TabIndex = 3;
            this.PrazoDestinatarioCaixaPostalUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.PrazoDestinatarioCaixaPostalUpDown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PrazoDestinatarioCaixaPostalUpDown_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label1.Location = new System.Drawing.Point(13, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Prazo ao destinatário de caixa postal";
            // 
            // PrazoDestinatarioCaidaPedidaUpDown
            // 
            this.PrazoDestinatarioCaidaPedidaUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrazoDestinatarioCaidaPedidaUpDown.Location = new System.Drawing.Point(16, 29);
            this.PrazoDestinatarioCaidaPedidaUpDown.Name = "PrazoDestinatarioCaidaPedidaUpDown";
            this.PrazoDestinatarioCaidaPedidaUpDown.Size = new System.Drawing.Size(146, 29);
            this.PrazoDestinatarioCaidaPedidaUpDown.TabIndex = 1;
            this.PrazoDestinatarioCaidaPedidaUpDown.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.PrazoDestinatarioCaidaPedidaUpDown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PrazoDestinatarioCaidaPedidaUpDown_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label2.Location = new System.Drawing.Point(13, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Prazo ao destinatário";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(378, 110);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(360, 155);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.PrazoRemetenteCaixaPostalUpDown);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.PrazoRemetenteCaidaPedidaUpDown);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(352, 122);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Ao Remetente";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // PrazoRemetenteCaixaPostalUpDown
            // 
            this.PrazoRemetenteCaixaPostalUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrazoRemetenteCaixaPostalUpDown.Location = new System.Drawing.Point(16, 82);
            this.PrazoRemetenteCaixaPostalUpDown.Name = "PrazoRemetenteCaixaPostalUpDown";
            this.PrazoRemetenteCaixaPostalUpDown.Size = new System.Drawing.Size(146, 29);
            this.PrazoRemetenteCaixaPostalUpDown.TabIndex = 3;
            this.PrazoRemetenteCaixaPostalUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.PrazoRemetenteCaixaPostalUpDown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PrazoRemetenteCaixaPostalUpDown_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label3.Location = new System.Drawing.Point(13, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Prazo ao remetente de caixa postal";
            // 
            // PrazoRemetenteCaidaPedidaUpDown
            // 
            this.PrazoRemetenteCaidaPedidaUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrazoRemetenteCaidaPedidaUpDown.Location = new System.Drawing.Point(16, 29);
            this.PrazoRemetenteCaidaPedidaUpDown.Name = "PrazoRemetenteCaidaPedidaUpDown";
            this.PrazoRemetenteCaidaPedidaUpDown.Size = new System.Drawing.Size(146, 29);
            this.PrazoRemetenteCaidaPedidaUpDown.TabIndex = 1;
            this.PrazoRemetenteCaidaPedidaUpDown.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.PrazoRemetenteCaidaPedidaUpDown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PrazoRemetenteCaidaPedidaUpDown_MouseClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label4.Location = new System.Drawing.Point(13, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Prazo ao remetente";
            // 
            // comboBoxTipoClassificacao
            // 
            this.comboBoxTipoClassificacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipoClassificacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.comboBoxTipoClassificacao.FormattingEnabled = true;
            this.comboBoxTipoClassificacao.Items.AddRange(new object[] {
            "PAC",
            "SEDEX",
            "DIVERSOS"});
            this.comboBoxTipoClassificacao.Location = new System.Drawing.Point(16, 24);
            this.comboBoxTipoClassificacao.Name = "comboBoxTipoClassificacao";
            this.comboBoxTipoClassificacao.Size = new System.Drawing.Size(311, 26);
            this.comboBoxTipoClassificacao.TabIndex = 0;
            this.comboBoxTipoClassificacao.SelectedIndexChanged += new System.EventHandler(this.comboBoxTipoClassificacao_SelectedIndexChanged);
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl2.Location = new System.Drawing.Point(12, 271);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(356, 102);
            this.tabControl2.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.comboBoxTipoClassificacao);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(348, 69);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Selecione uma Classificação";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl3.Controls.Add(this.tabPage4);
            this.tabControl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl3.Location = new System.Drawing.Point(12, 6);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(726, 98);
            this.tabControl3.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(718, 65);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Atenção!";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.label5.Size = new System.Drawing.Size(712, 59);
            this.label5.TabIndex = 0;
            // 
            // BtnGravar
            // 
            this.BtnGravar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnGravar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGravar.Location = new System.Drawing.Point(16, 498);
            this.BtnGravar.Name = "BtnGravar";
            this.BtnGravar.Size = new System.Drawing.Size(127, 37);
            this.BtnGravar.TabIndex = 4;
            this.BtnGravar.Text = "[F5] &Gravar";
            this.BtnGravar.UseVisualStyleBackColor = true;
            this.BtnGravar.Click += new System.EventHandler(this.BtnGravar_Click);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelar.Location = new System.Drawing.Point(579, 498);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(152, 37);
            this.BtnCancelar.TabIndex = 5;
            this.BtnCancelar.Text = "[ESC] &Cancelar";
            this.BtnCancelar.UseVisualStyleBackColor = true;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // tabControl4
            // 
            this.tabControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl4.Controls.Add(this.tabPage5);
            this.tabControl4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl4.Location = new System.Drawing.Point(378, 271);
            this.tabControl4.Name = "tabControl4";
            this.tabControl4.SelectedIndex = 0;
            this.tabControl4.Size = new System.Drawing.Size(356, 102);
            this.tabControl4.TabIndex = 3;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.DataInicial_dateTimePicker);
            this.tabPage5.Controls.Add(this.radioButtonAPartirDoDia);
            this.tabPage5.Controls.Add(this.radioButtonTodos);
            this.tabPage5.Controls.Add(this.label6);
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(348, 69);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Sobre Não Entregues";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // DataInicial_dateTimePicker
            // 
            this.DataInicial_dateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DataInicial_dateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataInicial_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DataInicial_dateTimePicker.Location = new System.Drawing.Point(239, 32);
            this.DataInicial_dateTimePicker.Name = "DataInicial_dateTimePicker";
            this.DataInicial_dateTimePicker.Size = new System.Drawing.Size(103, 27);
            this.DataInicial_dateTimePicker.TabIndex = 12;
            // 
            // radioButtonAPartirDoDia
            // 
            this.radioButtonAPartirDoDia.AutoSize = true;
            this.radioButtonAPartirDoDia.Location = new System.Drawing.Point(96, 33);
            this.radioButtonAPartirDoDia.Name = "radioButtonAPartirDoDia";
            this.radioButtonAPartirDoDia.Size = new System.Drawing.Size(145, 24);
            this.radioButtonAPartirDoDia.TabIndex = 13;
            this.radioButtonAPartirDoDia.Text = "A partir do dia:";
            this.radioButtonAPartirDoDia.UseVisualStyleBackColor = true;
            this.radioButtonAPartirDoDia.CheckedChanged += new System.EventHandler(this.radioButtonAPartirDoDia_CheckedChanged);
            // 
            // radioButtonTodos
            // 
            this.radioButtonTodos.AutoSize = true;
            this.radioButtonTodos.Checked = true;
            this.radioButtonTodos.Location = new System.Drawing.Point(9, 33);
            this.radioButtonTodos.Name = "radioButtonTodos";
            this.radioButtonTodos.Size = new System.Drawing.Size(76, 24);
            this.radioButtonTodos.TabIndex = 13;
            this.radioButtonTodos.TabStop = true;
            this.radioButtonTodos.Text = "Todos";
            this.radioButtonTodos.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label6.Location = new System.Drawing.Point(6, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(325, 18);
            this.label6.TabIndex = 2;
            this.label6.Text = "Para redefinir prazos dos objetos não entregues";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(16, 395);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(714, 34);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 12;
            // 
            // LblProgresso1
            // 
            this.LblProgresso1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblProgresso1.AutoSize = true;
            this.LblProgresso1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblProgresso1.ForeColor = System.Drawing.Color.Maroon;
            this.LblProgresso1.Location = new System.Drawing.Point(12, 370);
            this.LblProgresso1.Name = "LblProgresso1";
            this.LblProgresso1.Size = new System.Drawing.Size(0, 24);
            this.LblProgresso1.TabIndex = 11;
            // 
            // LblProgresso2
            // 
            this.LblProgresso2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblProgresso2.AutoSize = true;
            this.LblProgresso2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblProgresso2.ForeColor = System.Drawing.Color.Maroon;
            this.LblProgresso2.Location = new System.Drawing.Point(12, 432);
            this.LblProgresso2.Name = "LblProgresso2";
            this.LblProgresso2.Size = new System.Drawing.Size(0, 24);
            this.LblProgresso2.TabIndex = 11;
            // 
            // progressBar2
            // 
            this.progressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar2.Location = new System.Drawing.Point(16, 456);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(714, 34);
            this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar2.TabIndex = 12;
            // 
            // FormularioAlteracaoTiposPostaisMassa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 539);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.LblProgresso2);
            this.Controls.Add(this.LblProgresso1);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.BtnGravar);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tabControl3);
            this.Controls.Add(this.tabControl4);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControlPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormularioAlteracaoTiposPostaisMassa";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulário redefinição de prazos";
            this.Load += new System.EventHandler(this.FormularioAlteracaoTiposPostaisMassa_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormularioAlteracaoTiposPostaisMassa_KeyDown);
            this.tabControlPrincipal.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrazoDestinatarioCaixaPostalUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrazoDestinatarioCaidaPedidaUpDown)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrazoRemetenteCaixaPostalUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrazoRemetenteCaidaPedidaUpDown)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabControl4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlPrincipal;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PrazoDestinatarioCaixaPostalUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown PrazoDestinatarioCaidaPedidaUpDown;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.NumericUpDown PrazoRemetenteCaixaPostalUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown PrazoRemetenteCaidaPedidaUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxTipoClassificacao;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnGravar;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.TabControl tabControl4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.DateTimePicker DataInicial_dateTimePicker;
        private System.Windows.Forms.RadioButton radioButtonAPartirDoDia;
        private System.Windows.Forms.RadioButton radioButtonTodos;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label LblProgresso1;
        private System.Windows.Forms.Label LblProgresso2;
        private System.Windows.Forms.ProgressBar progressBar2;
    }
}