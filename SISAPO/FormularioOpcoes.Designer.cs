namespace SISAPO
{
    partial class FormularioOpcoes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioOpcoes));
            this.LblTituloFormulario = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxExibirItensJaEntregues = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageObjetosAguardandoRetirada = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButtonEnderecoSROWebsro2oCampo2 = new System.Windows.Forms.RadioButton();
            this.radioButtonEnderecoAppCampo2 = new System.Windows.Forms.RadioButton();
            this.TxtEnderecoSROEspecificoObjeto = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.BtnAtualizarEnderecoSROObjetoEspecifico = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonEnderecoApp = new System.Windows.Forms.RadioButton();
            this.radioButtonEnderecoSROWebsro2 = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtEnderecoSRO = new System.Windows.Forms.TextBox();
            this.BtnAtualizarEnderecoSRO = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.BtnMarcarTodosAtualizados = new System.Windows.Forms.Button();
            this.BtnRequererVerificacaoDeObjetosJaEntregues = new System.Windows.Forms.Button();
            this.tabPageExibirItensJaEntregues = new System.Windows.Forms.TabPage();
            this.checkBoxExibirObjetosEmCaixaPostal = new System.Windows.Forms.CheckBox();
            this.tabPageConfiguracoesAgencia = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae = new System.Windows.Forms.CheckBox();
            this.checkBoxACCAgenciaComunitaria = new System.Windows.Forms.CheckBox();
            this.checkBoxGerarQRCodePLRNaLdi = new System.Windows.Forms.CheckBox();
            this.comboBoxUFAgenciaLocal = new System.Windows.Forms.ComboBox();
            this.bindingSourceTabelaConfiguracoesSistema = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetConfiguracoes = new SISAPO.DataSetConfiguracoes();
            this.comboBoxSupEst = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LblNomeAgencia = new System.Windows.Forms.Label();
            this.BtnBuscarDadosAgenciaCodigoInformado = new System.Windows.Forms.Button();
            this.BtnAtualizarConfiguracoesAgencia = new System.Windows.Forms.Button();
            this.txtHorarioFuncionamentoAgenciaLocal = new System.Windows.Forms.TextBox();
            this.txtEnderecoAgencia = new System.Windows.Forms.TextBox();
            this.txtCepUnidade = new System.Windows.Forms.TextBox();
            this.txtTelefoneAgenciaLocal = new System.Windows.Forms.TextBox();
            this.txtCidadeAgenciaLocal = new System.Windows.Forms.TextBox();
            this.txtNomeAgencia = new System.Windows.Forms.TextBox();
            this.tabPageBackup = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnTornarBancoVazio = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnRestaurarBackup = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.BtnSalvarBackup = new System.Windows.Forms.Button();
            this.labelResultadoFolderBackup = new System.Windows.Forms.Label();
            this.BtnBuscarEnderecoParaBackup = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabelaConfiguracoesSistemaTableAdapter = new SISAPO.DataSetConfiguracoesTableAdapters.TabelaConfiguracoesSistemaTableAdapter();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageObjetosAguardandoRetirada.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageExibirItensJaEntregues.SuspendLayout();
            this.tabPageConfiguracoesAgencia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTabelaConfiguracoesSistema)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetConfiguracoes)).BeginInit();
            this.tabPageBackup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblTituloFormulario
            // 
            this.LblTituloFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTituloFormulario.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTituloFormulario.Location = new System.Drawing.Point(0, 0);
            this.LblTituloFormulario.Name = "LblTituloFormulario";
            this.LblTituloFormulario.Size = new System.Drawing.Size(739, 51);
            this.LblTituloFormulario.TabIndex = 0;
            this.LblTituloFormulario.Text = "Opções do sistema";
            this.LblTituloFormulario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.LblTituloFormulario);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(739, 51);
            this.panel1.TabIndex = 0;
            // 
            // checkBoxExibirItensJaEntregues
            // 
            this.checkBoxExibirItensJaEntregues.AutoSize = true;
            this.checkBoxExibirItensJaEntregues.Location = new System.Drawing.Point(8, 38);
            this.checkBoxExibirItensJaEntregues.Name = "checkBoxExibirItensJaEntregues";
            this.checkBoxExibirItensJaEntregues.Size = new System.Drawing.Size(211, 17);
            this.checkBoxExibirItensJaEntregues.TabIndex = 1;
            this.checkBoxExibirItensJaEntregues.Text = "Exibir Objetos já entregues na pesquisa";
            this.checkBoxExibirItensJaEntregues.UseVisualStyleBackColor = true;
            this.checkBoxExibirItensJaEntregues.CheckedChanged += new System.EventHandler(this.checkBoxExibirItensJaEntregues_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageObjetosAguardandoRetirada);
            this.tabControl1.Controls.Add(this.tabPageExibirItensJaEntregues);
            this.tabControl1.Controls.Add(this.tabPageConfiguracoesAgencia);
            this.tabControl1.Controls.Add(this.tabPageBackup);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(739, 431);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageObjetosAguardandoRetirada
            // 
            this.tabPageObjetosAguardandoRetirada.Controls.Add(this.groupBox5);
            this.tabPageObjetosAguardandoRetirada.Controls.Add(this.groupBox1);
            this.tabPageObjetosAguardandoRetirada.Controls.Add(this.BtnMarcarTodosAtualizados);
            this.tabPageObjetosAguardandoRetirada.Controls.Add(this.BtnRequererVerificacaoDeObjetosJaEntregues);
            this.tabPageObjetosAguardandoRetirada.Location = new System.Drawing.Point(4, 22);
            this.tabPageObjetosAguardandoRetirada.Name = "tabPageObjetosAguardandoRetirada";
            this.tabPageObjetosAguardandoRetirada.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageObjetosAguardandoRetirada.Size = new System.Drawing.Size(731, 405);
            this.tabPageObjetosAguardandoRetirada.TabIndex = 0;
            this.tabPageObjetosAguardandoRetirada.Text = "Objetos aguardando retirada";
            this.tabPageObjetosAguardandoRetirada.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.radioButtonEnderecoSROWebsro2oCampo2);
            this.groupBox5.Controls.Add(this.radioButtonEnderecoAppCampo2);
            this.groupBox5.Controls.Add(this.TxtEnderecoSROEspecificoObjeto);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.BtnAtualizarEnderecoSROObjetoEspecifico);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Location = new System.Drawing.Point(8, 183);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(715, 119);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Endereço SRO para um objeto específico";
            // 
            // radioButtonEnderecoSROWebsro2oCampo2
            // 
            this.radioButtonEnderecoSROWebsro2oCampo2.AutoSize = true;
            this.radioButtonEnderecoSROWebsro2oCampo2.Location = new System.Drawing.Point(24, 19);
            this.radioButtonEnderecoSROWebsro2oCampo2.Name = "radioButtonEnderecoSROWebsro2oCampo2";
            this.radioButtonEnderecoSROWebsro2oCampo2.Size = new System.Drawing.Size(124, 17);
            this.radioButtonEnderecoSROWebsro2oCampo2.TabIndex = 0;
            this.radioButtonEnderecoSROWebsro2oCampo2.TabStop = true;
            this.radioButtonEnderecoSROWebsro2oCampo2.Text = "Endereço \"websro2\"";
            this.radioButtonEnderecoSROWebsro2oCampo2.UseVisualStyleBackColor = true;
            this.radioButtonEnderecoSROWebsro2oCampo2.CheckedChanged += new System.EventHandler(this.radioButtonEnderecoSROWebsro2oCampo2_CheckedChanged);
            // 
            // radioButtonEnderecoAppCampo2
            // 
            this.radioButtonEnderecoAppCampo2.AutoSize = true;
            this.radioButtonEnderecoAppCampo2.Location = new System.Drawing.Point(180, 18);
            this.radioButtonEnderecoAppCampo2.Name = "radioButtonEnderecoAppCampo2";
            this.radioButtonEnderecoAppCampo2.Size = new System.Drawing.Size(102, 17);
            this.radioButtonEnderecoAppCampo2.TabIndex = 1;
            this.radioButtonEnderecoAppCampo2.TabStop = true;
            this.radioButtonEnderecoAppCampo2.Text = "Endereço \"app\"";
            this.radioButtonEnderecoAppCampo2.UseVisualStyleBackColor = true;
            this.radioButtonEnderecoAppCampo2.CheckedChanged += new System.EventHandler(this.radioButtonEnderecoAppCampo2_CheckedChanged);
            // 
            // TxtEnderecoSROEspecificoObjeto
            // 
            this.TxtEnderecoSROEspecificoObjeto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtEnderecoSROEspecificoObjeto.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSourceTabelaConfiguracoesSistema, "EnderecoSROPorObjeto", true));
            this.TxtEnderecoSROEspecificoObjeto.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTabelaConfiguracoesSistema, "EnderecoSROPorObjeto", true));
            this.TxtEnderecoSROEspecificoObjeto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEnderecoSROEspecificoObjeto.Location = new System.Drawing.Point(6, 83);
            this.TxtEnderecoSROEspecificoObjeto.Name = "TxtEnderecoSROEspecificoObjeto";
            this.TxtEnderecoSROEspecificoObjeto.Size = new System.Drawing.Size(584, 26);
            this.TxtEnderecoSROEspecificoObjeto.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(304, 16);
            this.label9.TabIndex = 6;
            this.label9.Text = "Endereço SRO para um objeto específico";
            // 
            // BtnAtualizarEnderecoSROObjetoEspecifico
            // 
            this.BtnAtualizarEnderecoSROObjetoEspecifico.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAtualizarEnderecoSROObjetoEspecifico.Location = new System.Drawing.Point(596, 83);
            this.BtnAtualizarEnderecoSROObjetoEspecifico.Name = "BtnAtualizarEnderecoSROObjetoEspecifico";
            this.BtnAtualizarEnderecoSROObjetoEspecifico.Size = new System.Drawing.Size(113, 28);
            this.BtnAtualizarEnderecoSROObjetoEspecifico.TabIndex = 9;
            this.BtnAtualizarEnderecoSROObjetoEspecifico.Text = "Atualizar";
            this.BtnAtualizarEnderecoSROObjetoEspecifico.UseVisualStyleBackColor = true;
            this.BtnAtualizarEnderecoSROObjetoEspecifico.Click += new System.EventHandler(this.BtnAtualizarEnderecoSROObjetoEspecifico_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(530, 16);
            this.label11.TabIndex = 7;
            this.label11.Text = "Exemplo: http://websro2/rastreamento/sro?opcao=PESQUISA&objetos=QB378038055BR";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButtonEnderecoApp);
            this.groupBox1.Controls.Add(this.radioButtonEnderecoSROWebsro2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.TxtEnderecoSRO);
            this.groupBox1.Controls.Add(this.BtnAtualizarEnderecoSRO);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(8, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(715, 119);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Endereço SRO";
            // 
            // radioButtonEnderecoApp
            // 
            this.radioButtonEnderecoApp.AutoSize = true;
            this.radioButtonEnderecoApp.Location = new System.Drawing.Point(180, 19);
            this.radioButtonEnderecoApp.Name = "radioButtonEnderecoApp";
            this.radioButtonEnderecoApp.Size = new System.Drawing.Size(102, 17);
            this.radioButtonEnderecoApp.TabIndex = 1;
            this.radioButtonEnderecoApp.TabStop = true;
            this.radioButtonEnderecoApp.Text = "Endereço \"app\"";
            this.radioButtonEnderecoApp.UseVisualStyleBackColor = true;
            this.radioButtonEnderecoApp.CheckedChanged += new System.EventHandler(this.radioButtonEnderecoApp_CheckedChanged);
            // 
            // radioButtonEnderecoSROWebsro2
            // 
            this.radioButtonEnderecoSROWebsro2.AutoSize = true;
            this.radioButtonEnderecoSROWebsro2.Location = new System.Drawing.Point(24, 20);
            this.radioButtonEnderecoSROWebsro2.Name = "radioButtonEnderecoSROWebsro2";
            this.radioButtonEnderecoSROWebsro2.Size = new System.Drawing.Size(124, 17);
            this.radioButtonEnderecoSROWebsro2.TabIndex = 0;
            this.radioButtonEnderecoSROWebsro2.TabStop = true;
            this.radioButtonEnderecoSROWebsro2.Text = "Endereço \"websro2\"";
            this.radioButtonEnderecoSROWebsro2.UseVisualStyleBackColor = true;
            this.radioButtonEnderecoSROWebsro2.CheckedChanged += new System.EventHandler(this.radioButtonEnderecoSROWebsro2_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(256, 16);
            this.label10.TabIndex = 3;
            this.label10.Text = "Exemplo: http://websro2/rastreamento/sro";
            // 
            // TxtEnderecoSRO
            // 
            this.TxtEnderecoSRO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtEnderecoSRO.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSourceTabelaConfiguracoesSistema, "EnderecoSRO", true));
            this.TxtEnderecoSRO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTabelaConfiguracoesSistema, "EnderecoSRO", true));
            this.TxtEnderecoSRO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEnderecoSRO.Location = new System.Drawing.Point(6, 83);
            this.TxtEnderecoSRO.Name = "TxtEnderecoSRO";
            this.TxtEnderecoSRO.Size = new System.Drawing.Size(584, 26);
            this.TxtEnderecoSRO.TabIndex = 4;
            // 
            // BtnAtualizarEnderecoSRO
            // 
            this.BtnAtualizarEnderecoSRO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAtualizarEnderecoSRO.Location = new System.Drawing.Point(596, 83);
            this.BtnAtualizarEnderecoSRO.Name = "BtnAtualizarEnderecoSRO";
            this.BtnAtualizarEnderecoSRO.Size = new System.Drawing.Size(113, 28);
            this.BtnAtualizarEnderecoSRO.TabIndex = 5;
            this.BtnAtualizarEnderecoSRO.Text = "Atualizar";
            this.BtnAtualizarEnderecoSRO.UseVisualStyleBackColor = true;
            this.BtnAtualizarEnderecoSRO.Click += new System.EventHandler(this.BtnAtualizarEnderecoSRO_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Endereço SRO";
            // 
            // BtnMarcarTodosAtualizados
            // 
            this.BtnMarcarTodosAtualizados.Location = new System.Drawing.Point(188, 15);
            this.BtnMarcarTodosAtualizados.Name = "BtnMarcarTodosAtualizados";
            this.BtnMarcarTodosAtualizados.Size = new System.Drawing.Size(174, 23);
            this.BtnMarcarTodosAtualizados.TabIndex = 1;
            this.BtnMarcarTodosAtualizados.Text = "&Marcar todos Atualizados";
            this.BtnMarcarTodosAtualizados.UseVisualStyleBackColor = true;
            this.BtnMarcarTodosAtualizados.Click += new System.EventHandler(this.BtnMarcarTodosAtualizados_Click);
            // 
            // BtnRequererVerificacaoDeObjetosJaEntregues
            // 
            this.BtnRequererVerificacaoDeObjetosJaEntregues.Location = new System.Drawing.Point(8, 15);
            this.BtnRequererVerificacaoDeObjetosJaEntregues.Name = "BtnRequererVerificacaoDeObjetosJaEntregues";
            this.BtnRequererVerificacaoDeObjetosJaEntregues.Size = new System.Drawing.Size(174, 23);
            this.BtnRequererVerificacaoDeObjetosJaEntregues.TabIndex = 0;
            this.BtnRequererVerificacaoDeObjetosJaEntregues.Text = "&Solicitar verificação de Objetos ainda não Entregues?";
            this.BtnRequererVerificacaoDeObjetosJaEntregues.UseVisualStyleBackColor = true;
            this.BtnRequererVerificacaoDeObjetosJaEntregues.Click += new System.EventHandler(this.BtnRequererVerificacaoDeObjetosJaEntregues_Click);
            // 
            // tabPageExibirItensJaEntregues
            // 
            this.tabPageExibirItensJaEntregues.Controls.Add(this.checkBoxExibirObjetosEmCaixaPostal);
            this.tabPageExibirItensJaEntregues.Controls.Add(this.checkBoxExibirItensJaEntregues);
            this.tabPageExibirItensJaEntregues.Location = new System.Drawing.Point(4, 22);
            this.tabPageExibirItensJaEntregues.Name = "tabPageExibirItensJaEntregues";
            this.tabPageExibirItensJaEntregues.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExibirItensJaEntregues.Size = new System.Drawing.Size(731, 405);
            this.tabPageExibirItensJaEntregues.TabIndex = 1;
            this.tabPageExibirItensJaEntregues.Text = "Consulta de objetos aguardando retirada";
            this.tabPageExibirItensJaEntregues.UseVisualStyleBackColor = true;
            // 
            // checkBoxExibirObjetosEmCaixaPostal
            // 
            this.checkBoxExibirObjetosEmCaixaPostal.AutoSize = true;
            this.checkBoxExibirObjetosEmCaixaPostal.Location = new System.Drawing.Point(8, 15);
            this.checkBoxExibirObjetosEmCaixaPostal.Name = "checkBoxExibirObjetosEmCaixaPostal";
            this.checkBoxExibirObjetosEmCaixaPostal.Size = new System.Drawing.Size(228, 17);
            this.checkBoxExibirObjetosEmCaixaPostal.TabIndex = 0;
            this.checkBoxExibirObjetosEmCaixaPostal.Text = "Exibir Objetos em Caixa Postal na pesquisa";
            this.checkBoxExibirObjetosEmCaixaPostal.UseVisualStyleBackColor = true;
            this.checkBoxExibirObjetosEmCaixaPostal.CheckedChanged += new System.EventHandler(this.checkBoxExibirObjetosEmCaixaPostal_CheckedChanged);
            // 
            // tabPageConfiguracoesAgencia
            // 
            this.tabPageConfiguracoesAgencia.Controls.Add(this.pictureBox1);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.label12);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.checkBoxACCAgenciaComunitaria);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.checkBoxGerarQRCodePLRNaLdi);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.comboBoxUFAgenciaLocal);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.comboBoxSupEst);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.label7);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.label1);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.label5);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.label3);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.label2);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.label6);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.label4);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.LblNomeAgencia);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.BtnBuscarDadosAgenciaCodigoInformado);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.BtnAtualizarConfiguracoesAgencia);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.txtHorarioFuncionamentoAgenciaLocal);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.txtEnderecoAgencia);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.txtCepUnidade);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.txtTelefoneAgenciaLocal);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.txtCidadeAgenciaLocal);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.txtNomeAgencia);
            this.tabPageConfiguracoesAgencia.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfiguracoesAgencia.Name = "tabPageConfiguracoesAgencia";
            this.tabPageConfiguracoesAgencia.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfiguracoesAgencia.Size = new System.Drawing.Size(731, 405);
            this.tabPageConfiguracoesAgencia.TabIndex = 2;
            this.tabPageConfiguracoesAgencia.Text = "Configurações da agência";
            this.tabPageConfiguracoesAgencia.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SISAPO.Properties.Resources.seta_diagonal;
            this.pictureBox1.Location = new System.Drawing.Point(25, 277);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 19);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DarkRed;
            this.label12.Location = new System.Drawing.Point(46, 221);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(347, 16);
            this.label12.TabIndex = 17;
            this.label12.Text = "(Obs.: Disponível apenas quando gerado 1 LDI por folha)";
            // 
            // checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae
            // 
            this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae.AutoSize = true;
            this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceTabelaConfiguracoesSistema, "ReceberObjetosViaQRCodePLRDaAgenciaMae", true));
            this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.bindingSourceTabelaConfiguracoesSistema, "ReceberObjetosViaQRCodePLRDaAgenciaMae", true));
            this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae.Location = new System.Drawing.Point(56, 277);
            this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae.Name = "checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae";
            this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae.Size = new System.Drawing.Size(629, 24);
            this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae.TabIndex = 19;
            this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae.Text = "Receber objetos via QR Code PLR (Pré Lista de Remessa) de agência mãe";
            this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae.UseVisualStyleBackColor = true;
            this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae.CheckedChanged += new System.EventHandler(this.checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae_CheckedChanged);
            // 
            // checkBoxACCAgenciaComunitaria
            // 
            this.checkBoxACCAgenciaComunitaria.AutoSize = true;
            this.checkBoxACCAgenciaComunitaria.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceTabelaConfiguracoesSistema, "ACCAgenciaComunitaria", true));
            this.checkBoxACCAgenciaComunitaria.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.bindingSourceTabelaConfiguracoesSistema, "ACCAgenciaComunitaria", true));
            this.checkBoxACCAgenciaComunitaria.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxACCAgenciaComunitaria.Location = new System.Drawing.Point(25, 247);
            this.checkBoxACCAgenciaComunitaria.Name = "checkBoxACCAgenciaComunitaria";
            this.checkBoxACCAgenciaComunitaria.Size = new System.Drawing.Size(343, 24);
            this.checkBoxACCAgenciaComunitaria.TabIndex = 18;
            this.checkBoxACCAgenciaComunitaria.Text = "ACC - Agência de Correios Comunitária";
            this.checkBoxACCAgenciaComunitaria.UseVisualStyleBackColor = true;
            this.checkBoxACCAgenciaComunitaria.CheckedChanged += new System.EventHandler(this.checkBoxACCAgenciaComunitaria_CheckedChanged);
            // 
            // checkBoxGerarQRCodePLRNaLdi
            // 
            this.checkBoxGerarQRCodePLRNaLdi.AutoSize = true;
            this.checkBoxGerarQRCodePLRNaLdi.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceTabelaConfiguracoesSistema, "GerarQRCodePLRNaLdi", true));
            this.checkBoxGerarQRCodePLRNaLdi.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.bindingSourceTabelaConfiguracoesSistema, "GerarQRCodePLRNaLdi", true));
            this.checkBoxGerarQRCodePLRNaLdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxGerarQRCodePLRNaLdi.Location = new System.Drawing.Point(25, 202);
            this.checkBoxGerarQRCodePLRNaLdi.Name = "checkBoxGerarQRCodePLRNaLdi";
            this.checkBoxGerarQRCodePLRNaLdi.Size = new System.Drawing.Size(443, 24);
            this.checkBoxGerarQRCodePLRNaLdi.TabIndex = 16;
            this.checkBoxGerarQRCodePLRNaLdi.Text = "Gerar QR Code PLR (Pré Lista de Remessa) na LDI";
            this.checkBoxGerarQRCodePLRNaLdi.UseVisualStyleBackColor = true;
            this.checkBoxGerarQRCodePLRNaLdi.CheckedChanged += new System.EventHandler(this.checkBoxGerarQRCodePLRNaLdi_CheckedChanged);
            // 
            // comboBoxUFAgenciaLocal
            // 
            this.comboBoxUFAgenciaLocal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxUFAgenciaLocal.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTabelaConfiguracoesSistema, "UFAgenciaLocal", true));
            this.comboBoxUFAgenciaLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxUFAgenciaLocal.FormattingEnabled = true;
            this.comboBoxUFAgenciaLocal.Items.AddRange(new object[] {
            "AC",
            "AL",
            "AP",
            "AM",
            "BA",
            "CE",
            "DF",
            "ES",
            "GO",
            "MA",
            "MT",
            "MS",
            "MG",
            "PA",
            "PB",
            "PR",
            "PE",
            "PI",
            "RJ",
            "RN",
            "RS",
            "RO",
            "RR",
            "SC",
            "SP",
            "SE",
            "TO"});
            this.comboBoxUFAgenciaLocal.Location = new System.Drawing.Point(437, 112);
            this.comboBoxUFAgenciaLocal.Name = "comboBoxUFAgenciaLocal";
            this.comboBoxUFAgenciaLocal.Size = new System.Drawing.Size(49, 28);
            this.comboBoxUFAgenciaLocal.TabIndex = 11;
            // 
            // bindingSourceTabelaConfiguracoesSistema
            // 
            this.bindingSourceTabelaConfiguracoesSistema.DataMember = "TabelaConfiguracoesSistema";
            this.bindingSourceTabelaConfiguracoesSistema.DataSource = this.dataSetConfiguracoes;
            // 
            // dataSetConfiguracoes
            // 
            this.dataSetConfiguracoes.DataSetName = "DataSetConfiguracoes";
            this.dataSetConfiguracoes.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // comboBoxSupEst
            // 
            this.comboBoxSupEst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSupEst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSupEst.FormattingEnabled = true;
            this.comboBoxSupEst.Items.AddRange(new object[] {
            "AC",
            "AL",
            "AP",
            "AM",
            "BA",
            "CE",
            "DF",
            "ES",
            "GO",
            "MA",
            "MT",
            "MS",
            "MG",
            "PA",
            "PB",
            "PR",
            "PE",
            "PI",
            "RJ",
            "RN",
            "RS",
            "RO",
            "RR",
            "SC",
            "SP",
            "SE",
            "TO"});
            this.comboBoxSupEst.Location = new System.Drawing.Point(546, 23);
            this.comboBoxSupEst.Name = "comboBoxSupEst";
            this.comboBoxSupEst.Size = new System.Drawing.Size(49, 28);
            this.comboBoxSupEst.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(236, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "Horário de funcionamento da agência:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Endereço da agência:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(436, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Estado:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(601, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "CEP Unidade:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(545, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sup. Est.:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(491, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Telefone:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Cidade:";
            // 
            // LblNomeAgencia
            // 
            this.LblNomeAgencia.AutoSize = true;
            this.LblNomeAgencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNomeAgencia.Location = new System.Drawing.Point(7, 8);
            this.LblNomeAgencia.Name = "LblNomeAgencia";
            this.LblNomeAgencia.Size = new System.Drawing.Size(119, 16);
            this.LblNomeAgencia.TabIndex = 0;
            this.LblNomeAgencia.Text = "Nome da agência:";
            // 
            // BtnBuscarDadosAgenciaCodigoInformado
            // 
            this.BtnBuscarDadosAgenciaCodigoInformado.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBuscarDadosAgenciaCodigoInformado.Location = new System.Drawing.Point(492, 310);
            this.BtnBuscarDadosAgenciaCodigoInformado.Name = "BtnBuscarDadosAgenciaCodigoInformado";
            this.BtnBuscarDadosAgenciaCodigoInformado.Size = new System.Drawing.Size(231, 87);
            this.BtnBuscarDadosAgenciaCodigoInformado.TabIndex = 21;
            this.BtnBuscarDadosAgenciaCodigoInformado.Text = "Buscar dados da Agência por um código informado";
            this.BtnBuscarDadosAgenciaCodigoInformado.UseVisualStyleBackColor = true;
            this.BtnBuscarDadosAgenciaCodigoInformado.Click += new System.EventHandler(this.BtnBuscarDadosAgenciaCodigoInformado_Click);
            // 
            // BtnAtualizarConfiguracoesAgencia
            // 
            this.BtnAtualizarConfiguracoesAgencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAtualizarConfiguracoesAgencia.Location = new System.Drawing.Point(8, 310);
            this.BtnAtualizarConfiguracoesAgencia.Name = "BtnAtualizarConfiguracoesAgencia";
            this.BtnAtualizarConfiguracoesAgencia.Size = new System.Drawing.Size(235, 87);
            this.BtnAtualizarConfiguracoesAgencia.TabIndex = 20;
            this.BtnAtualizarConfiguracoesAgencia.Text = "Atualizar / Gravar alterações";
            this.BtnAtualizarConfiguracoesAgencia.UseVisualStyleBackColor = true;
            this.BtnAtualizarConfiguracoesAgencia.Click += new System.EventHandler(this.BtnAtualizarConfiguracoesAgencia_Click);
            // 
            // txtHorarioFuncionamentoAgenciaLocal
            // 
            this.txtHorarioFuncionamentoAgenciaLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHorarioFuncionamentoAgenciaLocal.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSourceTabelaConfiguracoesSistema, "HorarioFuncionamentoAgenciaLocal", true));
            this.txtHorarioFuncionamentoAgenciaLocal.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTabelaConfiguracoesSistema, "HorarioFuncionamentoAgenciaLocal", true));
            this.txtHorarioFuncionamentoAgenciaLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHorarioFuncionamentoAgenciaLocal.Location = new System.Drawing.Point(8, 161);
            this.txtHorarioFuncionamentoAgenciaLocal.Name = "txtHorarioFuncionamentoAgenciaLocal";
            this.txtHorarioFuncionamentoAgenciaLocal.Size = new System.Drawing.Size(714, 26);
            this.txtHorarioFuncionamentoAgenciaLocal.TabIndex = 15;
            this.txtHorarioFuncionamentoAgenciaLocal.Text = "09:00hs às 12:00hs / 14:00hs às 17:00hs";
            // 
            // txtEnderecoAgencia
            // 
            this.txtEnderecoAgencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnderecoAgencia.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSourceTabelaConfiguracoesSistema, "EnderecoAgenciaLocal", true));
            this.txtEnderecoAgencia.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTabelaConfiguracoesSistema, "EnderecoAgenciaLocal", true));
            this.txtEnderecoAgencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEnderecoAgencia.Location = new System.Drawing.Point(9, 68);
            this.txtEnderecoAgencia.Name = "txtEnderecoAgencia";
            this.txtEnderecoAgencia.Size = new System.Drawing.Size(714, 26);
            this.txtEnderecoAgencia.TabIndex = 7;
            // 
            // txtCepUnidade
            // 
            this.txtCepUnidade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCepUnidade.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSourceTabelaConfiguracoesSistema, "CepUnidade", true));
            this.txtCepUnidade.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTabelaConfiguracoesSistema, "CepUnidade", true));
            this.txtCepUnidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCepUnidade.Location = new System.Drawing.Point(600, 24);
            this.txtCepUnidade.Name = "txtCepUnidade";
            this.txtCepUnidade.Size = new System.Drawing.Size(123, 26);
            this.txtCepUnidade.TabIndex = 5;
            // 
            // txtTelefoneAgenciaLocal
            // 
            this.txtTelefoneAgenciaLocal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTelefoneAgenciaLocal.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSourceTabelaConfiguracoesSistema, "TelefoneAgenciaLocal", true));
            this.txtTelefoneAgenciaLocal.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTabelaConfiguracoesSistema, "TelefoneAgenciaLocal", true));
            this.txtTelefoneAgenciaLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefoneAgenciaLocal.Location = new System.Drawing.Point(492, 113);
            this.txtTelefoneAgenciaLocal.Name = "txtTelefoneAgenciaLocal";
            this.txtTelefoneAgenciaLocal.Size = new System.Drawing.Size(231, 26);
            this.txtTelefoneAgenciaLocal.TabIndex = 13;
            // 
            // txtCidadeAgenciaLocal
            // 
            this.txtCidadeAgenciaLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCidadeAgenciaLocal.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSourceTabelaConfiguracoesSistema, "CidadeAgenciaLocal", true));
            this.txtCidadeAgenciaLocal.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTabelaConfiguracoesSistema, "CidadeAgenciaLocal", true));
            this.txtCidadeAgenciaLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCidadeAgenciaLocal.Location = new System.Drawing.Point(8, 114);
            this.txtCidadeAgenciaLocal.Name = "txtCidadeAgenciaLocal";
            this.txtCidadeAgenciaLocal.Size = new System.Drawing.Size(423, 26);
            this.txtCidadeAgenciaLocal.TabIndex = 9;
            // 
            // txtNomeAgencia
            // 
            this.txtNomeAgencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNomeAgencia.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSourceTabelaConfiguracoesSistema, "NomeAgenciaLocal", true));
            this.txtNomeAgencia.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTabelaConfiguracoesSistema, "NomeAgenciaLocal", true));
            this.txtNomeAgencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeAgencia.Location = new System.Drawing.Point(8, 24);
            this.txtNomeAgencia.Name = "txtNomeAgencia";
            this.txtNomeAgencia.Size = new System.Drawing.Size(532, 26);
            this.txtNomeAgencia.TabIndex = 1;
            // 
            // tabPageBackup
            // 
            this.tabPageBackup.Controls.Add(this.groupBox2);
            this.tabPageBackup.Controls.Add(this.groupBox4);
            this.tabPageBackup.Controls.Add(this.groupBox3);
            this.tabPageBackup.Location = new System.Drawing.Point(4, 22);
            this.tabPageBackup.Name = "tabPageBackup";
            this.tabPageBackup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBackup.Size = new System.Drawing.Size(731, 405);
            this.tabPageBackup.TabIndex = 3;
            this.tabPageBackup.Text = "Configurações de Backup";
            this.tabPageBackup.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnTornarBancoVazio);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(725, 94);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Limpar banco de dados";
            // 
            // BtnTornarBancoVazio
            // 
            this.BtnTornarBancoVazio.Location = new System.Drawing.Point(10, 26);
            this.BtnTornarBancoVazio.Name = "BtnTornarBancoVazio";
            this.BtnTornarBancoVazio.Size = new System.Drawing.Size(202, 28);
            this.BtnTornarBancoVazio.TabIndex = 3;
            this.BtnTornarBancoVazio.Text = "Limpar Banco / Tornar Banco Vazio";
            this.BtnTornarBancoVazio.UseVisualStyleBackColor = true;
            this.BtnTornarBancoVazio.Click += new System.EventHandler(this.BtnTornarBancoVazio_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.BtnRestaurarBackup);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 114);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(725, 94);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Restaurar Backup";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(536, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Teste de Usuario";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnRestaurarBackup
            // 
            this.BtnRestaurarBackup.Location = new System.Drawing.Point(10, 27);
            this.BtnRestaurarBackup.Name = "BtnRestaurarBackup";
            this.BtnRestaurarBackup.Size = new System.Drawing.Size(202, 28);
            this.BtnRestaurarBackup.TabIndex = 3;
            this.BtnRestaurarBackup.Text = "Restaurar backup";
            this.BtnRestaurarBackup.UseVisualStyleBackColor = true;
            this.BtnRestaurarBackup.Click += new System.EventHandler(this.BtnRestaurarBackup_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.BtnSalvarBackup);
            this.groupBox3.Controls.Add(this.labelResultadoFolderBackup);
            this.groupBox3.Controls.Add(this.BtnBuscarEnderecoParaBackup);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(725, 111);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dados de backup";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(8, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(365, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "Selecione o endereço desejado para backup";
            // 
            // BtnSalvarBackup
            // 
            this.BtnSalvarBackup.Location = new System.Drawing.Point(129, 19);
            this.BtnSalvarBackup.Name = "BtnSalvarBackup";
            this.BtnSalvarBackup.Size = new System.Drawing.Size(113, 28);
            this.BtnSalvarBackup.TabIndex = 2;
            this.BtnSalvarBackup.Text = "Salvar Backup";
            this.BtnSalvarBackup.UseVisualStyleBackColor = true;
            this.BtnSalvarBackup.Click += new System.EventHandler(this.BtnSalvarBackup_Click);
            // 
            // labelResultadoFolderBackup
            // 
            this.labelResultadoFolderBackup.AutoSize = true;
            this.labelResultadoFolderBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResultadoFolderBackup.ForeColor = System.Drawing.Color.DarkRed;
            this.labelResultadoFolderBackup.Location = new System.Drawing.Point(8, 68);
            this.labelResultadoFolderBackup.Name = "labelResultadoFolderBackup";
            this.labelResultadoFolderBackup.Size = new System.Drawing.Size(0, 20);
            this.labelResultadoFolderBackup.TabIndex = 4;
            // 
            // BtnBuscarEnderecoParaBackup
            // 
            this.BtnBuscarEnderecoParaBackup.Location = new System.Drawing.Point(10, 19);
            this.BtnBuscarEnderecoParaBackup.Name = "BtnBuscarEnderecoParaBackup";
            this.BtnBuscarEnderecoParaBackup.Size = new System.Drawing.Size(113, 28);
            this.BtnBuscarEnderecoParaBackup.TabIndex = 2;
            this.BtnBuscarEnderecoParaBackup.Text = "Buscar Endereço";
            this.BtnBuscarEnderecoParaBackup.UseVisualStyleBackColor = true;
            this.BtnBuscarEnderecoParaBackup.Click += new System.EventHandler(this.BtnBuscarEnderecoParaBackup_Click);
            // 
            // tabelaConfiguracoesSistemaTableAdapter
            // 
            this.tabelaConfiguracoesSistemaTableAdapter.ClearBeforeFill = true;
            // 
            // FormularioOpcoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 482);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormularioOpcoes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opções do sitema";
            this.Load += new System.EventHandler(this.FormularioOpcoes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormularioOpcoes_KeyDown);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageObjetosAguardandoRetirada.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageExibirItensJaEntregues.ResumeLayout(false);
            this.tabPageExibirItensJaEntregues.PerformLayout();
            this.tabPageConfiguracoesAgencia.ResumeLayout(false);
            this.tabPageConfiguracoesAgencia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTabelaConfiguracoesSistema)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetConfiguracoes)).EndInit();
            this.tabPageBackup.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblTituloFormulario;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxExibirItensJaEntregues;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageObjetosAguardandoRetirada;
        private System.Windows.Forms.TabPage tabPageExibirItensJaEntregues;
        private System.Windows.Forms.TabPage tabPageConfiguracoesAgencia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblNomeAgencia;
        private System.Windows.Forms.Button BtnAtualizarConfiguracoesAgencia;
        private System.Windows.Forms.TextBox txtEnderecoAgencia;
        private System.Windows.Forms.TextBox txtNomeAgencia;
        private System.Windows.Forms.BindingSource bindingSourceTabelaConfiguracoesSistema;
        private DataSetConfiguracoes dataSetConfiguracoes;
        private DataSetConfiguracoesTableAdapters.TabelaConfiguracoesSistemaTableAdapter tabelaConfiguracoesSistemaTableAdapter;
        private System.Windows.Forms.Button BtnMarcarTodosAtualizados;
        private System.Windows.Forms.Button BtnRequererVerificacaoDeObjetosJaEntregues;
        private System.Windows.Forms.CheckBox checkBoxExibirObjetosEmCaixaPostal;
        private System.Windows.Forms.ComboBox comboBoxSupEst;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCepUnidade;
        private System.Windows.Forms.ComboBox comboBoxUFAgenciaLocal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHorarioFuncionamentoAgenciaLocal;
        private System.Windows.Forms.TextBox txtTelefoneAgenciaLocal;
        private System.Windows.Forms.TextBox txtCidadeAgenciaLocal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtEnderecoSRO;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TxtEnderecoSROEspecificoObjeto;
        private System.Windows.Forms.Button BtnAtualizarEnderecoSROObjetoEspecifico;
        private System.Windows.Forms.Button BtnAtualizarEnderecoSRO;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonEnderecoApp;
        private System.Windows.Forms.RadioButton radioButtonEnderecoSROWebsro2;
        private System.Windows.Forms.TabPage tabPageBackup;
        private System.Windows.Forms.Button BtnSalvarBackup;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button BtnTornarBancoVazio;
        private System.Windows.Forms.Button BtnBuscarEnderecoParaBackup;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label labelResultadoFolderBackup;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BtnRestaurarBackup;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButtonEnderecoAppCampo2;
        private System.Windows.Forms.RadioButton radioButtonEnderecoSROWebsro2oCampo2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnBuscarDadosAgenciaCodigoInformado;
        private System.Windows.Forms.CheckBox checkBoxGerarQRCodePLRNaLdi;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkBoxReceberObjetosViaQRCodePLRDaAgenciaMae;
        private System.Windows.Forms.CheckBox checkBoxACCAgenciaComunitaria;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}