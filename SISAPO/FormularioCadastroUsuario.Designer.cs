namespace SISAPO
{
    partial class FormularioCadastroUsuario
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
            System.Windows.Forms.Label nomeCompletoUsuarioLabel;
            System.Windows.Forms.Label cPFUsuarioLabel;
            System.Windows.Forms.Label matriculaUsuarioLabel;
            System.Windows.Forms.Label emailCorporativoUsuarioLabel;
            System.Windows.Forms.Label emailAlternativoUsuarioLabel;
            System.Windows.Forms.Label loginUsuarioLabel;
            System.Windows.Forms.Label senhaUsuarioLabel;
            System.Windows.Forms.Label codigoLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioCadastroUsuario));
            this.panel1 = new System.Windows.Forms.Panel();
            this.codigoTextBox = new System.Windows.Forms.TextBox();
            this.bindingNavigatorTabelaUsuario = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.nomeCompletoUsuarioTextBox = new System.Windows.Forms.TextBox();
            this.cPFUsuarioTextBox = new System.Windows.Forms.TextBox();
            this.matriculaUsuarioTextBox = new System.Windows.Forms.TextBox();
            this.emailCorporativoUsuarioTextBox = new System.Windows.Forms.TextBox();
            this.emailAlternativoUsuarioTextBox = new System.Windows.Forms.TextBox();
            this.loginUsuarioTextBox = new System.Windows.Forms.TextBox();
            this.senhaUsuarioTextBox = new System.Windows.Forms.TextBox();
            this.loginAtivoCheckBox = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabelaUsuarioDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeCompletoUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPFUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatriculaUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmailCorporativoUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmailAlternativoUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsuarioCriacaoCadastro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoginUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SenhaUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoginAtivo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DataAlteracao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabelaUsuarioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetTabelaUsuario = new SISAPO.DataSetTabelaUsuario();
            this.tabelaUsuarioTableAdapter = new SISAPO.DataSetTabelaUsuarioTableAdapters.TabelaUsuarioTableAdapter();
            this.tableAdapterManager = new SISAPO.DataSetTabelaUsuarioTableAdapters.TableAdapterManager();
            nomeCompletoUsuarioLabel = new System.Windows.Forms.Label();
            cPFUsuarioLabel = new System.Windows.Forms.Label();
            matriculaUsuarioLabel = new System.Windows.Forms.Label();
            emailCorporativoUsuarioLabel = new System.Windows.Forms.Label();
            emailAlternativoUsuarioLabel = new System.Windows.Forms.Label();
            loginUsuarioLabel = new System.Windows.Forms.Label();
            senhaUsuarioLabel = new System.Windows.Forms.Label();
            codigoLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorTabelaUsuario)).BeginInit();
            this.bindingNavigatorTabelaUsuario.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabelaUsuarioDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabelaUsuarioBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetTabelaUsuario)).BeginInit();
            this.SuspendLayout();
            // 
            // nomeCompletoUsuarioLabel
            // 
            nomeCompletoUsuarioLabel.AutoSize = true;
            nomeCompletoUsuarioLabel.Location = new System.Drawing.Point(112, 22);
            nomeCompletoUsuarioLabel.Name = "nomeCompletoUsuarioLabel";
            nomeCompletoUsuarioLabel.Size = new System.Drawing.Size(82, 13);
            nomeCompletoUsuarioLabel.TabIndex = 2;
            nomeCompletoUsuarioLabel.Text = "Nome Completo";
            // 
            // cPFUsuarioLabel
            // 
            cPFUsuarioLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            cPFUsuarioLabel.AutoSize = true;
            cPFUsuarioLabel.Location = new System.Drawing.Point(444, 22);
            cPFUsuarioLabel.Name = "cPFUsuarioLabel";
            cPFUsuarioLabel.Size = new System.Drawing.Size(27, 13);
            cPFUsuarioLabel.TabIndex = 4;
            cPFUsuarioLabel.Text = "CPF";
            // 
            // matriculaUsuarioLabel
            // 
            matriculaUsuarioLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            matriculaUsuarioLabel.AutoSize = true;
            matriculaUsuarioLabel.Location = new System.Drawing.Point(580, 22);
            matriculaUsuarioLabel.Name = "matriculaUsuarioLabel";
            matriculaUsuarioLabel.Size = new System.Drawing.Size(52, 13);
            matriculaUsuarioLabel.TabIndex = 6;
            matriculaUsuarioLabel.Text = "Matrícula";
            // 
            // emailCorporativoUsuarioLabel
            // 
            emailCorporativoUsuarioLabel.AutoSize = true;
            emailCorporativoUsuarioLabel.Location = new System.Drawing.Point(6, 73);
            emailCorporativoUsuarioLabel.Name = "emailCorporativoUsuarioLabel";
            emailCorporativoUsuarioLabel.Size = new System.Drawing.Size(131, 13);
            emailCorporativoUsuarioLabel.TabIndex = 8;
            emailCorporativoUsuarioLabel.Text = "Email Corporativo Usuario:";
            // 
            // emailAlternativoUsuarioLabel
            // 
            emailAlternativoUsuarioLabel.AutoSize = true;
            emailAlternativoUsuarioLabel.Location = new System.Drawing.Point(339, 73);
            emailAlternativoUsuarioLabel.Name = "emailAlternativoUsuarioLabel";
            emailAlternativoUsuarioLabel.Size = new System.Drawing.Size(127, 13);
            emailAlternativoUsuarioLabel.TabIndex = 10;
            emailAlternativoUsuarioLabel.Text = "Email Alternativo Usuario:";
            // 
            // loginUsuarioLabel
            // 
            loginUsuarioLabel.AutoSize = true;
            loginUsuarioLabel.Location = new System.Drawing.Point(6, 125);
            loginUsuarioLabel.Name = "loginUsuarioLabel";
            loginUsuarioLabel.Size = new System.Drawing.Size(75, 13);
            loginUsuarioLabel.TabIndex = 12;
            loginUsuarioLabel.Text = "Login Usuario:";
            // 
            // senhaUsuarioLabel
            // 
            senhaUsuarioLabel.AutoSize = true;
            senhaUsuarioLabel.Location = new System.Drawing.Point(212, 125);
            senhaUsuarioLabel.Name = "senhaUsuarioLabel";
            senhaUsuarioLabel.Size = new System.Drawing.Size(80, 13);
            senhaUsuarioLabel.TabIndex = 14;
            senhaUsuarioLabel.Text = "Senha Usuario:";
            // 
            // codigoLabel
            // 
            codigoLabel.AutoSize = true;
            codigoLabel.Location = new System.Drawing.Point(6, 22);
            codigoLabel.Name = "codigoLabel";
            codigoLabel.Size = new System.Drawing.Size(40, 13);
            codigoLabel.TabIndex = 0;
            codigoLabel.Text = "Código";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bindingNavigatorTabelaUsuario);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(720, 38);
            this.panel1.TabIndex = 0;
            // 
            // codigoTextBox
            // 
            this.codigoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tabelaUsuarioBindingSource, "Codigo", true));
            this.codigoTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codigoTextBox.Location = new System.Drawing.Point(7, 38);
            this.codigoTextBox.Name = "codigoTextBox";
            this.codigoTextBox.ReadOnly = true;
            this.codigoTextBox.Size = new System.Drawing.Size(100, 26);
            this.codigoTextBox.TabIndex = 1;
            // 
            // bindingNavigatorTabelaUsuario
            // 
            this.bindingNavigatorTabelaUsuario.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigatorTabelaUsuario.AllowMerge = false;
            this.bindingNavigatorTabelaUsuario.BindingSource = this.tabelaUsuarioBindingSource;
            this.bindingNavigatorTabelaUsuario.CountItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigatorTabelaUsuario.CountItemFormat = "Primeiro registro";
            this.bindingNavigatorTabelaUsuario.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigatorTabelaUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bindingNavigatorTabelaUsuario.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.toolStripButton1,
            this.bindingNavigatorDeleteItem,
            this.toolStripButtonSave});
            this.bindingNavigatorTabelaUsuario.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigatorTabelaUsuario.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigatorTabelaUsuario.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigatorTabelaUsuario.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigatorTabelaUsuario.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigatorTabelaUsuario.Name = "bindingNavigatorTabelaUsuario";
            this.bindingNavigatorTabelaUsuario.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigatorTabelaUsuario.Size = new System.Drawing.Size(720, 38);
            this.bindingNavigatorTabelaUsuario.TabIndex = 0;
            this.bindingNavigatorTabelaUsuario.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 35);
            this.bindingNavigatorAddNewItem.Text = "Adicionar novo";
            this.bindingNavigatorAddNewItem.ToolTipText = "Adicionar novo - Preencha os campos e clique em \"Gravar\"";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 35);
            this.bindingNavigatorMoveFirstItem.Text = "Primeiro registro";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 35);
            this.bindingNavigatorDeleteItem.Text = "Remover";
            this.bindingNavigatorDeleteItem.ToolTipText = "Remover o registro atual";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.bindingNavigatorDeleteItem_Click);
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 35);
            this.bindingNavigatorMovePreviousItem.Text = "Anterior";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 38);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(30, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 35);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 35);
            this.bindingNavigatorMoveNextItem.Text = "Próximo";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 35);
            this.bindingNavigatorMoveLastItem.Text = "Último registro";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 35);
            this.toolStripButton1.Text = "Alterar";
            this.toolStripButton1.ToolTipText = "Alterar - Preencha os campos e clique em \"Gravar\"";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 35);
            this.toolStripButtonSave.Text = "Gravar";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // nomeCompletoUsuarioTextBox
            // 
            this.nomeCompletoUsuarioTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nomeCompletoUsuarioTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.nomeCompletoUsuarioTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tabelaUsuarioBindingSource, "NomeCompletoUsuario", true));
            this.nomeCompletoUsuarioTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nomeCompletoUsuarioTextBox.Location = new System.Drawing.Point(113, 38);
            this.nomeCompletoUsuarioTextBox.Name = "nomeCompletoUsuarioTextBox";
            this.nomeCompletoUsuarioTextBox.Size = new System.Drawing.Size(326, 26);
            this.nomeCompletoUsuarioTextBox.TabIndex = 3;
            // 
            // cPFUsuarioTextBox
            // 
            this.cPFUsuarioTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cPFUsuarioTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cPFUsuarioTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tabelaUsuarioBindingSource, "CPFUsuario", true));
            this.cPFUsuarioTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cPFUsuarioTextBox.Location = new System.Drawing.Point(445, 38);
            this.cPFUsuarioTextBox.Name = "cPFUsuarioTextBox";
            this.cPFUsuarioTextBox.Size = new System.Drawing.Size(130, 26);
            this.cPFUsuarioTextBox.TabIndex = 5;
            // 
            // matriculaUsuarioTextBox
            // 
            this.matriculaUsuarioTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.matriculaUsuarioTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.matriculaUsuarioTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tabelaUsuarioBindingSource, "MatriculaUsuario", true));
            this.matriculaUsuarioTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matriculaUsuarioTextBox.Location = new System.Drawing.Point(581, 38);
            this.matriculaUsuarioTextBox.Name = "matriculaUsuarioTextBox";
            this.matriculaUsuarioTextBox.Size = new System.Drawing.Size(129, 26);
            this.matriculaUsuarioTextBox.TabIndex = 7;
            // 
            // emailCorporativoUsuarioTextBox
            // 
            this.emailCorporativoUsuarioTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.emailCorporativoUsuarioTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tabelaUsuarioBindingSource, "EmailCorporativoUsuario", true));
            this.emailCorporativoUsuarioTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailCorporativoUsuarioTextBox.Location = new System.Drawing.Point(7, 89);
            this.emailCorporativoUsuarioTextBox.Name = "emailCorporativoUsuarioTextBox";
            this.emailCorporativoUsuarioTextBox.Size = new System.Drawing.Size(326, 26);
            this.emailCorporativoUsuarioTextBox.TabIndex = 9;
            // 
            // emailAlternativoUsuarioTextBox
            // 
            this.emailAlternativoUsuarioTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emailAlternativoUsuarioTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.emailAlternativoUsuarioTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tabelaUsuarioBindingSource, "EmailAlternativoUsuario", true));
            this.emailAlternativoUsuarioTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailAlternativoUsuarioTextBox.Location = new System.Drawing.Point(339, 89);
            this.emailAlternativoUsuarioTextBox.Name = "emailAlternativoUsuarioTextBox";
            this.emailAlternativoUsuarioTextBox.Size = new System.Drawing.Size(371, 26);
            this.emailAlternativoUsuarioTextBox.TabIndex = 11;
            // 
            // loginUsuarioTextBox
            // 
            this.loginUsuarioTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tabelaUsuarioBindingSource, "LoginUsuario", true));
            this.loginUsuarioTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginUsuarioTextBox.Location = new System.Drawing.Point(7, 141);
            this.loginUsuarioTextBox.Name = "loginUsuarioTextBox";
            this.loginUsuarioTextBox.Size = new System.Drawing.Size(200, 26);
            this.loginUsuarioTextBox.TabIndex = 13;
            // 
            // senhaUsuarioTextBox
            // 
            this.senhaUsuarioTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tabelaUsuarioBindingSource, "SenhaUsuario", true));
            this.senhaUsuarioTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.senhaUsuarioTextBox.Location = new System.Drawing.Point(213, 141);
            this.senhaUsuarioTextBox.Name = "senhaUsuarioTextBox";
            this.senhaUsuarioTextBox.PasswordChar = '#';
            this.senhaUsuarioTextBox.Size = new System.Drawing.Size(200, 26);
            this.senhaUsuarioTextBox.TabIndex = 15;
            this.senhaUsuarioTextBox.UseSystemPasswordChar = true;
            // 
            // loginAtivoCheckBox
            // 
            this.loginAtivoCheckBox.AutoSize = true;
            this.loginAtivoCheckBox.Checked = true;
            this.loginAtivoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.loginAtivoCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginAtivoCheckBox.Location = new System.Drawing.Point(445, 143);
            this.loginAtivoCheckBox.Name = "loginAtivoCheckBox";
            this.loginAtivoCheckBox.Size = new System.Drawing.Size(122, 24);
            this.loginAtivoCheckBox.TabIndex = 16;
            this.loginAtivoCheckBox.Text = "Usuário Ativo";
            this.loginAtivoCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 355);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(720, 63);
            this.panel2.TabIndex = 2;
            // 
            // tabelaUsuarioDataGridView
            // 
            this.tabelaUsuarioDataGridView.AllowUserToAddRows = false;
            this.tabelaUsuarioDataGridView.AllowUserToDeleteRows = false;
            this.tabelaUsuarioDataGridView.AutoGenerateColumns = false;
            this.tabelaUsuarioDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabelaUsuarioDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.NomeCompletoUsuario,
            this.CPFUsuario,
            this.MatriculaUsuario,
            this.EmailCorporativoUsuario,
            this.EmailAlternativoUsuario,
            this.UsuarioCriacaoCadastro,
            this.LoginUsuario,
            this.SenhaUsuario,
            this.LoginAtivo,
            this.DataAlteracao});
            this.tabelaUsuarioDataGridView.DataSource = this.tabelaUsuarioBindingSource;
            this.tabelaUsuarioDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabelaUsuarioDataGridView.Location = new System.Drawing.Point(0, 218);
            this.tabelaUsuarioDataGridView.Name = "tabelaUsuarioDataGridView";
            this.tabelaUsuarioDataGridView.ReadOnly = true;
            this.tabelaUsuarioDataGridView.RowHeadersVisible = false;
            this.tabelaUsuarioDataGridView.Size = new System.Drawing.Size(720, 137);
            this.tabelaUsuarioDataGridView.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(codigoLabel);
            this.groupBox1.Controls.Add(this.loginAtivoCheckBox);
            this.groupBox1.Controls.Add(this.codigoTextBox);
            this.groupBox1.Controls.Add(this.senhaUsuarioTextBox);
            this.groupBox1.Controls.Add(senhaUsuarioLabel);
            this.groupBox1.Controls.Add(nomeCompletoUsuarioLabel);
            this.groupBox1.Controls.Add(this.loginUsuarioTextBox);
            this.groupBox1.Controls.Add(this.nomeCompletoUsuarioTextBox);
            this.groupBox1.Controls.Add(loginUsuarioLabel);
            this.groupBox1.Controls.Add(cPFUsuarioLabel);
            this.groupBox1.Controls.Add(this.emailAlternativoUsuarioTextBox);
            this.groupBox1.Controls.Add(this.cPFUsuarioTextBox);
            this.groupBox1.Controls.Add(emailAlternativoUsuarioLabel);
            this.groupBox1.Controls.Add(matriculaUsuarioLabel);
            this.groupBox1.Controls.Add(this.emailCorporativoUsuarioTextBox);
            this.groupBox1.Controls.Add(this.matriculaUsuarioTextBox);
            this.groupBox1.Controls.Add(emailCorporativoUsuarioLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(720, 180);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados do usuário";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(576, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "Fechar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Codigo
            // 
            this.Codigo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Codigo.DataPropertyName = "Codigo";
            this.Codigo.HeaderText = "Código";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 65;
            // 
            // NomeCompletoUsuario
            // 
            this.NomeCompletoUsuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NomeCompletoUsuario.DataPropertyName = "NomeCompletoUsuario";
            this.NomeCompletoUsuario.HeaderText = "Nome completo";
            this.NomeCompletoUsuario.Name = "NomeCompletoUsuario";
            this.NomeCompletoUsuario.ReadOnly = true;
            // 
            // CPFUsuario
            // 
            this.CPFUsuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CPFUsuario.DataPropertyName = "CPFUsuario";
            this.CPFUsuario.HeaderText = "CPF";
            this.CPFUsuario.Name = "CPFUsuario";
            this.CPFUsuario.ReadOnly = true;
            this.CPFUsuario.Width = 52;
            // 
            // MatriculaUsuario
            // 
            this.MatriculaUsuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MatriculaUsuario.DataPropertyName = "MatriculaUsuario";
            this.MatriculaUsuario.HeaderText = "Matrícula";
            this.MatriculaUsuario.Name = "MatriculaUsuario";
            this.MatriculaUsuario.ReadOnly = true;
            this.MatriculaUsuario.Width = 77;
            // 
            // EmailCorporativoUsuario
            // 
            this.EmailCorporativoUsuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.EmailCorporativoUsuario.DataPropertyName = "EmailCorporativoUsuario";
            this.EmailCorporativoUsuario.HeaderText = "E-mail corporativo";
            this.EmailCorporativoUsuario.Name = "EmailCorporativoUsuario";
            this.EmailCorporativoUsuario.ReadOnly = true;
            this.EmailCorporativoUsuario.Visible = false;
            this.EmailCorporativoUsuario.Width = 116;
            // 
            // EmailAlternativoUsuario
            // 
            this.EmailAlternativoUsuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.EmailAlternativoUsuario.DataPropertyName = "EmailAlternativoUsuario";
            this.EmailAlternativoUsuario.HeaderText = "E-mail alternativo";
            this.EmailAlternativoUsuario.Name = "EmailAlternativoUsuario";
            this.EmailAlternativoUsuario.ReadOnly = true;
            this.EmailAlternativoUsuario.Visible = false;
            this.EmailAlternativoUsuario.Width = 112;
            // 
            // UsuarioCriacaoCadastro
            // 
            this.UsuarioCriacaoCadastro.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.UsuarioCriacaoCadastro.DataPropertyName = "UsuarioCriacaoCadastro";
            this.UsuarioCriacaoCadastro.HeaderText = "Usuário cadastro";
            this.UsuarioCriacaoCadastro.Name = "UsuarioCriacaoCadastro";
            this.UsuarioCriacaoCadastro.ReadOnly = true;
            this.UsuarioCriacaoCadastro.Visible = false;
            this.UsuarioCriacaoCadastro.Width = 112;
            // 
            // LoginUsuario
            // 
            this.LoginUsuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.LoginUsuario.DataPropertyName = "LoginUsuario";
            this.LoginUsuario.HeaderText = "Login";
            this.LoginUsuario.Name = "LoginUsuario";
            this.LoginUsuario.ReadOnly = true;
            this.LoginUsuario.Width = 58;
            // 
            // SenhaUsuario
            // 
            this.SenhaUsuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.SenhaUsuario.DataPropertyName = "SenhaUsuario";
            this.SenhaUsuario.HeaderText = "Senha";
            this.SenhaUsuario.Name = "SenhaUsuario";
            this.SenhaUsuario.ReadOnly = true;
            this.SenhaUsuario.Visible = false;
            this.SenhaUsuario.Width = 63;
            // 
            // LoginAtivo
            // 
            this.LoginAtivo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LoginAtivo.DataPropertyName = "LoginAtivo";
            this.LoginAtivo.FalseValue = "Inativo";
            this.LoginAtivo.HeaderText = "Status";
            this.LoginAtivo.IndeterminateValue = "-";
            this.LoginAtivo.Name = "LoginAtivo";
            this.LoginAtivo.ReadOnly = true;
            this.LoginAtivo.TrueValue = "Ativo";
            this.LoginAtivo.Width = 43;
            // 
            // DataAlteracao
            // 
            this.DataAlteracao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DataAlteracao.DataPropertyName = "DataAlteracao";
            this.DataAlteracao.HeaderText = "Modificação";
            this.DataAlteracao.Name = "DataAlteracao";
            this.DataAlteracao.ReadOnly = true;
            this.DataAlteracao.Width = 90;
            // 
            // tabelaUsuarioBindingSource
            // 
            this.tabelaUsuarioBindingSource.DataMember = "TabelaUsuario";
            this.tabelaUsuarioBindingSource.DataSource = this.dataSetTabelaUsuario;
            this.tabelaUsuarioBindingSource.CurrentChanged += new System.EventHandler(this.tabelaUsuarioBindingSource_CurrentChanged);
            // 
            // dataSetTabelaUsuario
            // 
            this.dataSetTabelaUsuario.DataSetName = "DataSetTabelaUsuario";
            this.dataSetTabelaUsuario.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabelaUsuarioTableAdapter
            // 
            this.tabelaUsuarioTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.TabelaUsuarioTableAdapter = this.tabelaUsuarioTableAdapter;
            this.tableAdapterManager.UpdateOrder = SISAPO.DataSetTabelaUsuarioTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // FormularioCadastroUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 418);
            this.Controls.Add(this.tabelaUsuarioDataGridView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormularioCadastroUsuario";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulário cadastro de usuário";
            this.Load += new System.EventHandler(this.FormularioCadastroUsuario_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormularioCadastroUsuario_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorTabelaUsuario)).EndInit();
            this.bindingNavigatorTabelaUsuario.ResumeLayout(false);
            this.bindingNavigatorTabelaUsuario.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabelaUsuarioDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabelaUsuarioBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetTabelaUsuario)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DataSetTabelaUsuario dataSetTabelaUsuario;
        private System.Windows.Forms.BindingSource tabelaUsuarioBindingSource;
        private DataSetTabelaUsuarioTableAdapters.TabelaUsuarioTableAdapter tabelaUsuarioTableAdapter;
        private DataSetTabelaUsuarioTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator bindingNavigatorTabelaUsuario;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.TextBox nomeCompletoUsuarioTextBox;
        private System.Windows.Forms.TextBox cPFUsuarioTextBox;
        private System.Windows.Forms.TextBox matriculaUsuarioTextBox;
        private System.Windows.Forms.TextBox emailCorporativoUsuarioTextBox;
        private System.Windows.Forms.TextBox emailAlternativoUsuarioTextBox;
        private System.Windows.Forms.TextBox loginUsuarioTextBox;
        private System.Windows.Forms.TextBox senhaUsuarioTextBox;
        private System.Windows.Forms.CheckBox loginAtivoCheckBox;
        private System.Windows.Forms.DataGridView tabelaUsuarioDataGridView;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.TextBox codigoTextBox;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeCompletoUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPFUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatriculaUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmailCorporativoUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmailAlternativoUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsuarioCriacaoCadastro;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoginUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn SenhaUsuario;
        private System.Windows.Forms.DataGridViewCheckBoxColumn LoginAtivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataAlteracao;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}