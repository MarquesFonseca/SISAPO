namespace SISAPO
{
    partial class FormularioTiposPostais
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioTiposPostais));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Servico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sigla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoClassificacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrazoDestinoCaidaPedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrazoDestinoCaixaPostal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrazoRemetenteCaidaPedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrazoRemetenteCaixaPostal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataAlteracao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acoesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.acoesAlterarPrazoTodosSelecionadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.alterarItemToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LbnQuantidadeRegistros = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxTipoClassificacao = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnAlterarPrazosTodosSelecionados = new System.Windows.Forms.Button();
            this.BtnAdicionarNovosTiposPostais = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.acoesContextMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Servico,
            this.Sigla,
            this.Descricao,
            this.TipoClassificacao,
            this.PrazoDestinoCaidaPedida,
            this.PrazoDestinoCaixaPostal,
            this.PrazoRemetenteCaidaPedida,
            this.PrazoRemetenteCaixaPostal,
            this.DataAlteracao});
            this.dataGridView1.ContextMenuStrip = this.acoesContextMenuStrip;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowHeadersWidth = 30;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(757, 291);
            this.dataGridView1.TabIndex = 0;
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "Codigo";
            this.Codigo.Frozen = true;
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Visible = false;
            // 
            // Servico
            // 
            this.Servico.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Servico.DataPropertyName = "Servico";
            this.Servico.Frozen = true;
            this.Servico.HeaderText = "Tipo serviço";
            this.Servico.Name = "Servico";
            this.Servico.ReadOnly = true;
            this.Servico.Width = 90;
            // 
            // Sigla
            // 
            this.Sigla.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Sigla.DataPropertyName = "Sigla";
            this.Sigla.Frozen = true;
            this.Sigla.HeaderText = "Sigla";
            this.Sigla.Name = "Sigla";
            this.Sigla.ReadOnly = true;
            this.Sigla.Width = 55;
            // 
            // Descricao
            // 
            this.Descricao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Descricao.DataPropertyName = "Descricao";
            this.Descricao.HeaderText = "Descrição";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            // 
            // TipoClassificacao
            // 
            this.TipoClassificacao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.TipoClassificacao.DataPropertyName = "TipoClassificacao";
            this.TipoClassificacao.HeaderText = "Tipo de classificação";
            this.TipoClassificacao.Name = "TipoClassificacao";
            this.TipoClassificacao.ReadOnly = true;
            this.TipoClassificacao.Width = 132;
            // 
            // PrazoDestinoCaidaPedida
            // 
            this.PrazoDestinoCaidaPedida.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PrazoDestinoCaidaPedida.DataPropertyName = "PrazoDestinoCaidaPedida";
            this.PrazoDestinoCaidaPedida.HeaderText = "Prazo destinatário";
            this.PrazoDestinoCaidaPedida.Name = "PrazoDestinoCaidaPedida";
            this.PrazoDestinoCaidaPedida.ReadOnly = true;
            this.PrazoDestinoCaidaPedida.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PrazoDestinoCaidaPedida.Width = 116;
            // 
            // PrazoDestinoCaixaPostal
            // 
            this.PrazoDestinoCaixaPostal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PrazoDestinoCaixaPostal.DataPropertyName = "PrazoDestinoCaixaPostal";
            this.PrazoDestinoCaixaPostal.HeaderText = "Prazo destinatário caixa postal";
            this.PrazoDestinoCaixaPostal.Name = "PrazoDestinoCaixaPostal";
            this.PrazoDestinoCaixaPostal.ReadOnly = true;
            this.PrazoDestinoCaixaPostal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PrazoDestinoCaixaPostal.Width = 175;
            // 
            // PrazoRemetenteCaidaPedida
            // 
            this.PrazoRemetenteCaidaPedida.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PrazoRemetenteCaidaPedida.DataPropertyName = "PrazoRemetenteCaidaPedida";
            this.PrazoRemetenteCaidaPedida.HeaderText = "Prazo remetente";
            this.PrazoRemetenteCaidaPedida.Name = "PrazoRemetenteCaidaPedida";
            this.PrazoRemetenteCaidaPedida.ReadOnly = true;
            this.PrazoRemetenteCaidaPedida.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PrazoRemetenteCaidaPedida.Width = 109;
            // 
            // PrazoRemetenteCaixaPostal
            // 
            this.PrazoRemetenteCaixaPostal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PrazoRemetenteCaixaPostal.DataPropertyName = "PrazoRemetenteCaixaPostal";
            this.PrazoRemetenteCaixaPostal.HeaderText = "Prazo remetente caixa postal";
            this.PrazoRemetenteCaixaPostal.Name = "PrazoRemetenteCaixaPostal";
            this.PrazoRemetenteCaixaPostal.ReadOnly = true;
            this.PrazoRemetenteCaixaPostal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PrazoRemetenteCaixaPostal.Width = 168;
            // 
            // DataAlteracao
            // 
            this.DataAlteracao.DataPropertyName = "DataAlteracao";
            this.DataAlteracao.HeaderText = "Última alteração";
            this.DataAlteracao.Name = "DataAlteracao";
            this.DataAlteracao.ReadOnly = true;
            this.DataAlteracao.Visible = false;
            // 
            // acoesContextMenuStrip
            // 
            this.acoesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acoesAlterarPrazoTodosSelecionadosToolStripMenuItem,
            this.toolStripSeparator2,
            this.alterarItemToolStripMenuItem1});
            this.acoesContextMenuStrip.Name = "contextMenuStripImprimirListaEntrega";
            this.acoesContextMenuStrip.Size = new System.Drawing.Size(262, 54);
            // 
            // acoesAlterarPrazoTodosSelecionadosToolStripMenuItem
            // 
            this.acoesAlterarPrazoTodosSelecionadosToolStripMenuItem.Image = global::SISAPO.Properties.Resources.if_BT_printer_905556;
            this.acoesAlterarPrazoTodosSelecionadosToolStripMenuItem.Name = "acoesAlterarPrazoTodosSelecionadosToolStripMenuItem";
            this.acoesAlterarPrazoTodosSelecionadosToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.acoesAlterarPrazoTodosSelecionadosToolStripMenuItem.Text = "Alterar prazo de todos selecionados";
            this.acoesAlterarPrazoTodosSelecionadosToolStripMenuItem.Click += new System.EventHandler(this.acoesAlterarPrazoTodosSelecionadosToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(258, 6);
            // 
            // alterarItemToolStripMenuItem1
            // 
            this.alterarItemToolStripMenuItem1.Image = global::SISAPO.Properties.Resources.CadastroObjetos;
            this.alterarItemToolStripMenuItem1.Name = "alterarItemToolStripMenuItem1";
            this.alterarItemToolStripMenuItem1.Size = new System.Drawing.Size(261, 22);
            this.alterarItemToolStripMenuItem1.Text = "CCCCCCCCCCC";
            this.alterarItemToolStripMenuItem1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.LbnQuantidadeRegistros);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.linkLabel2);
            this.groupBox1.Controls.Add(this.TxtPesquisa);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(763, 82);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(279, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Quantidade de registros:";
            // 
            // LbnQuantidadeRegistros
            // 
            this.LbnQuantidadeRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LbnQuantidadeRegistros.AutoSize = true;
            this.LbnQuantidadeRegistros.BackColor = System.Drawing.SystemColors.Control;
            this.LbnQuantidadeRegistros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LbnQuantidadeRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbnQuantidadeRegistros.ForeColor = System.Drawing.Color.Red;
            this.LbnQuantidadeRegistros.Location = new System.Drawing.Point(454, 18);
            this.LbnQuantidadeRegistros.Name = "LbnQuantidadeRegistros";
            this.LbnQuantidadeRegistros.Size = new System.Drawing.Size(19, 20);
            this.LbnQuantidadeRegistros.TabIndex = 4;
            this.LbnQuantidadeRegistros.Text = "0";
            this.LbnQuantidadeRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleDescription = "Limpar";
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Tag = "Limpar";
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(35, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Campo de pesquisa";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(35, 9);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(71, 13);
            this.linkLabel2.TabIndex = 0;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Limpar - [Esc]";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // TxtPesquisa
            // 
            this.TxtPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPesquisa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPesquisa.Location = new System.Drawing.Point(6, 39);
            this.TxtPesquisa.Name = "TxtPesquisa";
            this.TxtPesquisa.Size = new System.Drawing.Size(492, 29);
            this.TxtPesquisa.TabIndex = 2;
            this.TxtPesquisa.TextChanged += new System.EventHandler(this.TxtPesquisa_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.comboBoxTipoClassificacao);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(504, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 60);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "*Filtro por classificação";
            // 
            // comboBoxTipoClassificacao
            // 
            this.comboBoxTipoClassificacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipoClassificacao.FormattingEnabled = true;
            this.comboBoxTipoClassificacao.Items.AddRange(new object[] {
            "TODOS",
            "PAC",
            "SEDEX",
            "DIVERSOS",
            "SEM CLASSIFICAÇÃO"});
            this.comboBoxTipoClassificacao.Location = new System.Drawing.Point(6, 30);
            this.comboBoxTipoClassificacao.Name = "comboBoxTipoClassificacao";
            this.comboBoxTipoClassificacao.Size = new System.Drawing.Size(240, 24);
            this.comboBoxTipoClassificacao.TabIndex = 0;
            this.comboBoxTipoClassificacao.SelectedValueChanged += new System.EventHandler(this.comboBoxTipoClassificacao_SelectedValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BtnAlterarPrazosTodosSelecionados);
            this.groupBox3.Controls.Add(this.BtnAdicionarNovosTiposPostais);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(763, 70);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // BtnAlterarPrazosTodosSelecionados
            // 
            this.BtnAlterarPrazosTodosSelecionados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.BtnAlterarPrazosTodosSelecionados.Location = new System.Drawing.Point(12, 12);
            this.BtnAlterarPrazosTodosSelecionados.Name = "BtnAlterarPrazosTodosSelecionados";
            this.BtnAlterarPrazosTodosSelecionados.Size = new System.Drawing.Size(192, 53);
            this.BtnAlterarPrazosTodosSelecionados.TabIndex = 0;
            this.BtnAlterarPrazosTodosSelecionados.Text = "Alterar prazo (Selecionados)";
            this.BtnAlterarPrazosTodosSelecionados.UseVisualStyleBackColor = true;
            this.BtnAlterarPrazosTodosSelecionados.Click += new System.EventHandler(this.BtnAlterarPrazosTodosSelecionados_Click);
            // 
            // BtnAdicionarNovosTiposPostais
            // 
            this.BtnAdicionarNovosTiposPostais.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAdicionarNovosTiposPostais.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdicionarNovosTiposPostais.Image = global::SISAPO.Properties.Resources.icons8_colar_26;
            this.BtnAdicionarNovosTiposPostais.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnAdicionarNovosTiposPostais.Location = new System.Drawing.Point(499, 12);
            this.BtnAdicionarNovosTiposPostais.Name = "BtnAdicionarNovosTiposPostais";
            this.BtnAdicionarNovosTiposPostais.Size = new System.Drawing.Size(252, 53);
            this.BtnAdicionarNovosTiposPostais.TabIndex = 1;
            this.BtnAdicionarNovosTiposPostais.Text = "&Adicionar novos registros";
            this.BtnAdicionarNovosTiposPostais.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnAdicionarNovosTiposPostais.UseVisualStyleBackColor = true;
            this.BtnAdicionarNovosTiposPostais.Click += new System.EventHandler(this.BtnAdicionarNovosTiposPostais_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 152);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(763, 310);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // FormularioTiposPostais
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 462);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormularioTiposPostais";
            this.Text = "Formulário gerenciamento de Tipos Postais";
            this.Load += new System.EventHandler(this.FormularioTiposPostais_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormularioTiposPostais_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.acoesContextMenuStrip.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxTipoClassificacao;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button BtnAdicionarNovosTiposPostais;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LbnQuantidadeRegistros;
        private System.Windows.Forms.ContextMenuStrip acoesContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem acoesAlterarPrazoTodosSelecionadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem alterarItemToolStripMenuItem1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Servico;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sigla;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoClassificacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrazoDestinoCaidaPedida;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrazoDestinoCaixaPostal;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrazoRemetenteCaidaPedida;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrazoRemetenteCaixaPostal;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataAlteracao;
        private System.Windows.Forms.Button BtnAlterarPrazosTodosSelecionados;
    }
}