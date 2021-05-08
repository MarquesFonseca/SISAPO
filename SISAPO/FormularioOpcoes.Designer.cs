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
            this.BtnMarcarTodosAtualizados = new System.Windows.Forms.Button();
            this.BtnRequererVerificacaoDeObjetosJaEntregues = new System.Windows.Forms.Button();
            this.tabPageExibirItensJaEntregues = new System.Windows.Forms.TabPage();
            this.checkBoxExibirObjetosEmCaixaPostal = new System.Windows.Forms.CheckBox();
            this.tabPageConfiguracoesAgencia = new System.Windows.Forms.TabPage();
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
            this.BtnAtualizarConfiguracoesAgencia = new System.Windows.Forms.Button();
            this.txtHorarioFuncionamentoAgenciaLocal = new System.Windows.Forms.TextBox();
            this.txtEnderecoAgencia = new System.Windows.Forms.TextBox();
            this.txtCepUnidade = new System.Windows.Forms.TextBox();
            this.txtTelefoneAgenciaLocal = new System.Windows.Forms.TextBox();
            this.txtCidadeAgenciaLocal = new System.Windows.Forms.TextBox();
            this.txtNomeAgencia = new System.Windows.Forms.TextBox();
            this.tabelaConfiguracoesSistemaTableAdapter = new SISAPO.DataSetConfiguracoesTableAdapters.TabelaConfiguracoesSistemaTableAdapter();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageObjetosAguardandoRetirada.SuspendLayout();
            this.tabPageExibirItensJaEntregues.SuspendLayout();
            this.tabPageConfiguracoesAgencia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTabelaConfiguracoesSistema)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetConfiguracoes)).BeginInit();
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
            this.checkBoxExibirItensJaEntregues.TabIndex = 0;
            this.checkBoxExibirItensJaEntregues.Text = "Exibir Objetos já entregues na pesquisa";
            this.checkBoxExibirItensJaEntregues.UseVisualStyleBackColor = true;
            this.checkBoxExibirItensJaEntregues.CheckedChanged += new System.EventHandler(this.checkBoxExibirItensJaEntregues_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageObjetosAguardandoRetirada);
            this.tabControl1.Controls.Add(this.tabPageExibirItensJaEntregues);
            this.tabControl1.Controls.Add(this.tabPageConfiguracoesAgencia);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(739, 411);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageObjetosAguardandoRetirada
            // 
            this.tabPageObjetosAguardandoRetirada.Controls.Add(this.BtnMarcarTodosAtualizados);
            this.tabPageObjetosAguardandoRetirada.Controls.Add(this.BtnRequererVerificacaoDeObjetosJaEntregues);
            this.tabPageObjetosAguardandoRetirada.Location = new System.Drawing.Point(4, 22);
            this.tabPageObjetosAguardandoRetirada.Name = "tabPageObjetosAguardandoRetirada";
            this.tabPageObjetosAguardandoRetirada.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageObjetosAguardandoRetirada.Size = new System.Drawing.Size(731, 385);
            this.tabPageObjetosAguardandoRetirada.TabIndex = 0;
            this.tabPageObjetosAguardandoRetirada.Text = "Objetos aguardando retirada";
            this.tabPageObjetosAguardandoRetirada.UseVisualStyleBackColor = true;
            // 
            // BtnMarcarTodosAtualizados
            // 
            this.BtnMarcarTodosAtualizados.Location = new System.Drawing.Point(8, 60);
            this.BtnMarcarTodosAtualizados.Name = "BtnMarcarTodosAtualizados";
            this.BtnMarcarTodosAtualizados.Size = new System.Drawing.Size(174, 23);
            this.BtnMarcarTodosAtualizados.TabIndex = 2;
            this.BtnMarcarTodosAtualizados.Text = "&Marcar todos Atualizados";
            this.BtnMarcarTodosAtualizados.UseVisualStyleBackColor = true;
            this.BtnMarcarTodosAtualizados.Click += new System.EventHandler(this.BtnMarcarTodosAtualizados_Click);
            // 
            // BtnRequererVerificacaoDeObjetosJaEntregues
            // 
            this.BtnRequererVerificacaoDeObjetosJaEntregues.Location = new System.Drawing.Point(8, 15);
            this.BtnRequererVerificacaoDeObjetosJaEntregues.Name = "BtnRequererVerificacaoDeObjetosJaEntregues";
            this.BtnRequererVerificacaoDeObjetosJaEntregues.Size = new System.Drawing.Size(174, 23);
            this.BtnRequererVerificacaoDeObjetosJaEntregues.TabIndex = 2;
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
            this.tabPageExibirItensJaEntregues.Size = new System.Drawing.Size(731, 385);
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
            this.tabPageConfiguracoesAgencia.Size = new System.Drawing.Size(731, 385);
            this.tabPageConfiguracoesAgencia.TabIndex = 2;
            this.tabPageConfiguracoesAgencia.Text = "Configurações da agência";
            this.tabPageConfiguracoesAgencia.UseVisualStyleBackColor = true;
            // 
            // comboBoxUFAgenciaLocal
            // 
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
            // BtnAtualizarConfiguracoesAgencia
            // 
            this.BtnAtualizarConfiguracoesAgencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAtualizarConfiguracoesAgencia.Location = new System.Drawing.Point(8, 211);
            this.BtnAtualizarConfiguracoesAgencia.Name = "BtnAtualizarConfiguracoesAgencia";
            this.BtnAtualizarConfiguracoesAgencia.Size = new System.Drawing.Size(143, 39);
            this.BtnAtualizarConfiguracoesAgencia.TabIndex = 16;
            this.BtnAtualizarConfiguracoesAgencia.Text = "Atualizar";
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
            this.txtCepUnidade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.txtTelefoneAgenciaLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            // tabelaConfiguracoesSistemaTableAdapter
            // 
            this.tabelaConfiguracoesSistemaTableAdapter.ClearBeforeFill = true;
            // 
            // FormularioOpcoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 462);
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
            this.tabPageExibirItensJaEntregues.ResumeLayout(false);
            this.tabPageExibirItensJaEntregues.PerformLayout();
            this.tabPageConfiguracoesAgencia.ResumeLayout(false);
            this.tabPageConfiguracoesAgencia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTabelaConfiguracoesSistema)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetConfiguracoes)).EndInit();
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
    }
}