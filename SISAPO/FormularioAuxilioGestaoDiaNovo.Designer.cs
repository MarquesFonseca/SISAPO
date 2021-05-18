namespace SISAPO
{
    partial class FormularioAuxilioGestaoDiaNovo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioAuxilioGestaoDiaNovo));
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnImprimirListaAtual = new System.Windows.Forms.Button();
            this.BtnRetornaTodosNaoEntregues = new System.Windows.Forms.Button();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPageExibicaoTodosAindaNaoEntregues = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPageExibicaoColarConteudoJaCopiado = new System.Windows.Forms.TabPage();
            this.BtnColarConteudoJaCopiado = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FiltrarPorPrazosAVENCERCheckBox = new System.Windows.Forms.CheckBox();
            this.FiltrarPorPrazosVENCENDOHOJECheckBox = new System.Windows.Forms.CheckBox();
            this.FiltrarPorPrazosVENCIDOSCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FiltrarPorClassificacaoDIVERSOSCheckBox = new System.Windows.Forms.CheckBox();
            this.FiltrarPorClassificacaoSEDEXCheckBox = new System.Windows.Forms.CheckBox();
            this.FiltrarPorClassificacaoPACCCheckBox = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.LbnQuantidadeRegistros = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CodigoLdi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataLancamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtdDiasCorridos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoClassificacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoObjeto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sigla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrazoTipoClassificacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusPrazo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataVencimento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtdDiasVencidos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceObjetosNaoEntregues = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPageExibicaoTodosAindaNaoEntregues.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceObjetosNaoEntregues)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BtnImprimirListaAtual);
            this.panel3.Controls.Add(this.BtnRetornaTodosNaoEntregues);
            this.panel3.Controls.Add(this.tabControl3);
            this.panel3.Controls.Add(this.BtnColarConteudoJaCopiado);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(771, 77);
            this.panel3.TabIndex = 0;
            // 
            // BtnImprimirListaAtual
            // 
            this.BtnImprimirListaAtual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnImprimirListaAtual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnImprimirListaAtual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImprimirListaAtual.Image = global::SISAPO.Properties.Resources.impressão_26;
            this.BtnImprimirListaAtual.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnImprimirListaAtual.Location = new System.Drawing.Point(572, 34);
            this.BtnImprimirListaAtual.Name = "BtnImprimirListaAtual";
            this.BtnImprimirListaAtual.Size = new System.Drawing.Size(196, 38);
            this.BtnImprimirListaAtual.TabIndex = 2;
            this.BtnImprimirListaAtual.Text = "&Imprimir lista atual";
            this.BtnImprimirListaAtual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnImprimirListaAtual.UseVisualStyleBackColor = true;
            this.BtnImprimirListaAtual.Click += new System.EventHandler(this.BtnImprimirListaAtual_Click);
            // 
            // BtnRetornaTodosNaoEntregues
            // 
            this.BtnRetornaTodosNaoEntregues.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRetornaTodosNaoEntregues.Image = global::SISAPO.Properties.Resources.icons8_colar_26;
            this.BtnRetornaTodosNaoEntregues.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnRetornaTodosNaoEntregues.Location = new System.Drawing.Point(21, 35);
            this.BtnRetornaTodosNaoEntregues.Name = "BtnRetornaTodosNaoEntregues";
            this.BtnRetornaTodosNaoEntregues.Size = new System.Drawing.Size(329, 38);
            this.BtnRetornaTodosNaoEntregues.TabIndex = 1;
            this.BtnRetornaTodosNaoEntregues.Text = "Buscar todos ainda não entregues";
            this.BtnRetornaTodosNaoEntregues.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnRetornaTodosNaoEntregues.UseVisualStyleBackColor = true;
            this.BtnRetornaTodosNaoEntregues.Click += new System.EventHandler(this.BtnRetornaTodosNaoEntregues_Click);
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPageExibicaoTodosAindaNaoEntregues);
            this.tabControl3.Controls.Add(this.tabPageExibicaoColarConteudoJaCopiado);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(771, 30);
            this.tabControl3.TabIndex = 0;
            this.tabControl3.SelectedIndexChanged += new System.EventHandler(this.tabControl3_SelectedIndexChanged);
            // 
            // tabPageExibicaoTodosAindaNaoEntregues
            // 
            this.tabPageExibicaoTodosAindaNaoEntregues.Controls.Add(this.label5);
            this.tabPageExibicaoTodosAindaNaoEntregues.Location = new System.Drawing.Point(4, 29);
            this.tabPageExibicaoTodosAindaNaoEntregues.Name = "tabPageExibicaoTodosAindaNaoEntregues";
            this.tabPageExibicaoTodosAindaNaoEntregues.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExibicaoTodosAindaNaoEntregues.Size = new System.Drawing.Size(763, 0);
            this.tabPageExibicaoTodosAindaNaoEntregues.TabIndex = 0;
            this.tabPageExibicaoTodosAindaNaoEntregues.Text = "Exibir todos objetos ainda não entregues";
            this.tabPageExibicaoTodosAindaNaoEntregues.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.label5.Size = new System.Drawing.Size(757, 0);
            this.label5.TabIndex = 0;
            // 
            // tabPageExibicaoColarConteudoJaCopiado
            // 
            this.tabPageExibicaoColarConteudoJaCopiado.Location = new System.Drawing.Point(4, 29);
            this.tabPageExibicaoColarConteudoJaCopiado.Name = "tabPageExibicaoColarConteudoJaCopiado";
            this.tabPageExibicaoColarConteudoJaCopiado.Size = new System.Drawing.Size(763, 0);
            this.tabPageExibicaoColarConteudoJaCopiado.TabIndex = 1;
            this.tabPageExibicaoColarConteudoJaCopiado.Text = "Exibir itens copiado no SRO";
            this.tabPageExibicaoColarConteudoJaCopiado.UseVisualStyleBackColor = true;
            // 
            // BtnColarConteudoJaCopiado
            // 
            this.BtnColarConteudoJaCopiado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnColarConteudoJaCopiado.Image = global::SISAPO.Properties.Resources.icons8_colar_26;
            this.BtnColarConteudoJaCopiado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnColarConteudoJaCopiado.Location = new System.Drawing.Point(21, 36);
            this.BtnColarConteudoJaCopiado.Name = "BtnColarConteudoJaCopiado";
            this.BtnColarConteudoJaCopiado.Size = new System.Drawing.Size(329, 38);
            this.BtnColarConteudoJaCopiado.TabIndex = 0;
            this.BtnColarConteudoJaCopiado.Text = "&Colar conteúdo já copiado";
            this.BtnColarConteudoJaCopiado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnColarConteudoJaCopiado.UseVisualStyleBackColor = true;
            this.BtnColarConteudoJaCopiado.Click += new System.EventHandler(this.BtnColarConteudoJaCopiado_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.FiltrarPorPrazosAVENCERCheckBox);
            this.groupBox2.Controls.Add(this.FiltrarPorPrazosVENCENDOHOJECheckBox);
            this.groupBox2.Controls.Add(this.FiltrarPorPrazosVENCIDOSCheckBox);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 119);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtrar por prazos";
            // 
            // FiltrarPorPrazosAVENCERCheckBox
            // 
            this.FiltrarPorPrazosAVENCERCheckBox.AutoSize = true;
            this.FiltrarPorPrazosAVENCERCheckBox.Enabled = false;
            this.FiltrarPorPrazosAVENCERCheckBox.Location = new System.Drawing.Point(14, 89);
            this.FiltrarPorPrazosAVENCERCheckBox.Name = "FiltrarPorPrazosAVENCERCheckBox";
            this.FiltrarPorPrazosAVENCERCheckBox.Size = new System.Drawing.Size(102, 24);
            this.FiltrarPorPrazosAVENCERCheckBox.TabIndex = 2;
            this.FiltrarPorPrazosAVENCERCheckBox.Text = "A Vencer";
            this.FiltrarPorPrazosAVENCERCheckBox.UseVisualStyleBackColor = true;
            this.FiltrarPorPrazosAVENCERCheckBox.CheckedChanged += new System.EventHandler(this.FiltrarPorPrazosAVENCERCheckBox_CheckedChanged);
            // 
            // FiltrarPorPrazosVENCENDOHOJECheckBox
            // 
            this.FiltrarPorPrazosVENCENDOHOJECheckBox.AutoSize = true;
            this.FiltrarPorPrazosVENCENDOHOJECheckBox.Enabled = false;
            this.FiltrarPorPrazosVENCENDOHOJECheckBox.Location = new System.Drawing.Point(14, 59);
            this.FiltrarPorPrazosVENCENDOHOJECheckBox.Name = "FiltrarPorPrazosVENCENDOHOJECheckBox";
            this.FiltrarPorPrazosVENCENDOHOJECheckBox.Size = new System.Drawing.Size(151, 24);
            this.FiltrarPorPrazosVENCENDOHOJECheckBox.TabIndex = 1;
            this.FiltrarPorPrazosVENCENDOHOJECheckBox.Text = "Vencendo Hoje";
            this.FiltrarPorPrazosVENCENDOHOJECheckBox.UseVisualStyleBackColor = true;
            this.FiltrarPorPrazosVENCENDOHOJECheckBox.CheckedChanged += new System.EventHandler(this.FiltrarPorPrazosVENCENDOHOJECheckBox_CheckedChanged);
            // 
            // FiltrarPorPrazosVENCIDOSCheckBox
            // 
            this.FiltrarPorPrazosVENCIDOSCheckBox.AutoSize = true;
            this.FiltrarPorPrazosVENCIDOSCheckBox.Enabled = false;
            this.FiltrarPorPrazosVENCIDOSCheckBox.Location = new System.Drawing.Point(14, 29);
            this.FiltrarPorPrazosVENCIDOSCheckBox.Name = "FiltrarPorPrazosVENCIDOSCheckBox";
            this.FiltrarPorPrazosVENCIDOSCheckBox.Size = new System.Drawing.Size(102, 24);
            this.FiltrarPorPrazosVENCIDOSCheckBox.TabIndex = 0;
            this.FiltrarPorPrazosVENCIDOSCheckBox.Text = "Vencidos";
            this.FiltrarPorPrazosVENCIDOSCheckBox.UseVisualStyleBackColor = true;
            this.FiltrarPorPrazosVENCIDOSCheckBox.CheckedChanged += new System.EventHandler(this.FiltrarPorPrazosVENCIDOSCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.FiltrarPorClassificacaoDIVERSOSCheckBox);
            this.groupBox1.Controls.Add(this.FiltrarPorClassificacaoSEDEXCheckBox);
            this.groupBox1.Controls.Add(this.FiltrarPorClassificacaoPACCCheckBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(4, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 119);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtrar por classificação";
            // 
            // FiltrarPorClassificacaoDIVERSOSCheckBox
            // 
            this.FiltrarPorClassificacaoDIVERSOSCheckBox.AutoSize = true;
            this.FiltrarPorClassificacaoDIVERSOSCheckBox.Enabled = false;
            this.FiltrarPorClassificacaoDIVERSOSCheckBox.Location = new System.Drawing.Point(15, 89);
            this.FiltrarPorClassificacaoDIVERSOSCheckBox.Name = "FiltrarPorClassificacaoDIVERSOSCheckBox";
            this.FiltrarPorClassificacaoDIVERSOSCheckBox.Size = new System.Drawing.Size(97, 24);
            this.FiltrarPorClassificacaoDIVERSOSCheckBox.TabIndex = 2;
            this.FiltrarPorClassificacaoDIVERSOSCheckBox.Text = "Diversos";
            this.FiltrarPorClassificacaoDIVERSOSCheckBox.UseVisualStyleBackColor = true;
            this.FiltrarPorClassificacaoDIVERSOSCheckBox.CheckedChanged += new System.EventHandler(this.FiltrarPorClassificacaoDIVERSOSCheckBox_CheckedChanged);
            // 
            // FiltrarPorClassificacaoSEDEXCheckBox
            // 
            this.FiltrarPorClassificacaoSEDEXCheckBox.AutoSize = true;
            this.FiltrarPorClassificacaoSEDEXCheckBox.Enabled = false;
            this.FiltrarPorClassificacaoSEDEXCheckBox.Location = new System.Drawing.Point(15, 59);
            this.FiltrarPorClassificacaoSEDEXCheckBox.Name = "FiltrarPorClassificacaoSEDEXCheckBox";
            this.FiltrarPorClassificacaoSEDEXCheckBox.Size = new System.Drawing.Size(78, 24);
            this.FiltrarPorClassificacaoSEDEXCheckBox.TabIndex = 1;
            this.FiltrarPorClassificacaoSEDEXCheckBox.Text = "Sedex";
            this.FiltrarPorClassificacaoSEDEXCheckBox.UseVisualStyleBackColor = true;
            this.FiltrarPorClassificacaoSEDEXCheckBox.CheckedChanged += new System.EventHandler(this.FiltrarPorClassificacaoSEDEXCheckBox_CheckedChanged);
            // 
            // FiltrarPorClassificacaoPACCCheckBox
            // 
            this.FiltrarPorClassificacaoPACCCheckBox.AutoSize = true;
            this.FiltrarPorClassificacaoPACCCheckBox.Enabled = false;
            this.FiltrarPorClassificacaoPACCCheckBox.Location = new System.Drawing.Point(15, 29);
            this.FiltrarPorClassificacaoPACCCheckBox.Name = "FiltrarPorClassificacaoPACCCheckBox";
            this.FiltrarPorClassificacaoPACCCheckBox.Size = new System.Drawing.Size(58, 24);
            this.FiltrarPorClassificacaoPACCCheckBox.TabIndex = 0;
            this.FiltrarPorClassificacaoPACCCheckBox.Text = "Pac";
            this.FiltrarPorClassificacaoPACCCheckBox.UseVisualStyleBackColor = true;
            this.FiltrarPorClassificacaoPACCCheckBox.CheckedChanged += new System.EventHandler(this.FiltrarPorClassificacaoPACCCheckBox_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.LbnQuantidadeRegistros);
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 411);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(771, 51);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(3, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quantidade de registros:";
            // 
            // LbnQuantidadeRegistros
            // 
            this.LbnQuantidadeRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LbnQuantidadeRegistros.AutoSize = true;
            this.LbnQuantidadeRegistros.BackColor = System.Drawing.SystemColors.Control;
            this.LbnQuantidadeRegistros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LbnQuantidadeRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbnQuantidadeRegistros.ForeColor = System.Drawing.Color.Red;
            this.LbnQuantidadeRegistros.Location = new System.Drawing.Point(210, 22);
            this.LbnQuantidadeRegistros.Name = "LbnQuantidadeRegistros";
            this.LbnQuantidadeRegistros.Size = new System.Drawing.Size(19, 20);
            this.LbnQuantidadeRegistros.TabIndex = 1;
            this.LbnQuantidadeRegistros.Text = "0";
            this.LbnQuantidadeRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Location = new System.Drawing.Point(590, 11);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(175, 31);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "&Fechar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            this.CodigoLdi,
            this.DataLancamento,
            this.QtdDiasCorridos,
            this.TipoClassificacao,
            this.CodigoObjeto,
            this.Sigla,
            this.PrazoTipoClassificacao,
            this.NomeCliente,
            this.StatusPrazo,
            this.DataVencimento,
            this.QtdDiasVencidos});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 77);
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
            this.dataGridView1.Size = new System.Drawing.Size(550, 334);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // CodigoLdi
            // 
            this.CodigoLdi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CodigoLdi.DataPropertyName = "CodigoLdi";
            this.CodigoLdi.HeaderText = "Código Ldi";
            this.CodigoLdi.Name = "CodigoLdi";
            this.CodigoLdi.ReadOnly = true;
            this.CodigoLdi.Width = 82;
            // 
            // DataLancamento
            // 
            this.DataLancamento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DataLancamento.DataPropertyName = "DataLancamento";
            this.DataLancamento.HeaderText = "Data Lançamento";
            this.DataLancamento.Name = "DataLancamento";
            this.DataLancamento.ReadOnly = true;
            this.DataLancamento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataLancamento.Width = 98;
            // 
            // QtdDiasCorridos
            // 
            this.QtdDiasCorridos.DataPropertyName = "QtdDiasCorridos";
            this.QtdDiasCorridos.HeaderText = "Qtd Dias Corridos";
            this.QtdDiasCorridos.Name = "QtdDiasCorridos";
            this.QtdDiasCorridos.ReadOnly = true;
            this.QtdDiasCorridos.Width = 95;
            // 
            // TipoClassificacao
            // 
            this.TipoClassificacao.DataPropertyName = "TipoClassificacao";
            this.TipoClassificacao.HeaderText = "Tipo Classificação";
            this.TipoClassificacao.Name = "TipoClassificacao";
            this.TipoClassificacao.ReadOnly = true;
            // 
            // CodigoObjeto
            // 
            this.CodigoObjeto.DataPropertyName = "CodigoObjeto";
            this.CodigoObjeto.HeaderText = "Código Objeto";
            this.CodigoObjeto.Name = "CodigoObjeto";
            this.CodigoObjeto.ReadOnly = true;
            this.CodigoObjeto.Width = 99;
            // 
            // Sigla
            // 
            this.Sigla.DataPropertyName = "Sigla";
            this.Sigla.HeaderText = "Sigla";
            this.Sigla.Name = "Sigla";
            this.Sigla.ReadOnly = true;
            this.Sigla.Width = 40;
            // 
            // PrazoTipoClassificacao
            // 
            this.PrazoTipoClassificacao.DataPropertyName = "PrazoTipoClassificacao";
            this.PrazoTipoClassificacao.HeaderText = "Prazo Tipo Class.";
            this.PrazoTipoClassificacao.Name = "PrazoTipoClassificacao";
            this.PrazoTipoClassificacao.ReadOnly = true;
            // 
            // NomeCliente
            // 
            this.NomeCliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NomeCliente.DataPropertyName = "NomeCliente";
            this.NomeCliente.HeaderText = "Nome Cliente";
            this.NomeCliente.MinimumWidth = 20;
            this.NomeCliente.Name = "NomeCliente";
            this.NomeCliente.ReadOnly = true;
            this.NomeCliente.Width = 95;
            // 
            // StatusPrazo
            // 
            this.StatusPrazo.DataPropertyName = "StatusPrazo";
            this.StatusPrazo.HeaderText = "Status";
            this.StatusPrazo.Name = "StatusPrazo";
            this.StatusPrazo.ReadOnly = true;
            this.StatusPrazo.Width = 120;
            // 
            // DataVencimento
            // 
            this.DataVencimento.DataPropertyName = "DataVencimento";
            this.DataVencimento.HeaderText = "Data Vencimento";
            this.DataVencimento.Name = "DataVencimento";
            this.DataVencimento.ReadOnly = true;
            this.DataVencimento.Width = 98;
            // 
            // QtdDiasVencidos
            // 
            this.QtdDiasVencidos.DataPropertyName = "QtdDiasVencidos";
            this.QtdDiasVencidos.HeaderText = "Qtd Dias Vencidos";
            this.QtdDiasVencidos.Name = "QtdDiasVencidos";
            this.QtdDiasVencidos.ReadOnly = true;
            this.QtdDiasVencidos.Width = 110;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(550, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(221, 334);
            this.panel1.TabIndex = 1;
            // 
            // FormularioAuxilioGestaoDiaNovo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 462);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "FormularioAuxilioGestaoDiaNovo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulário de auxílio à gestão do dia";
            this.Load += new System.EventHandler(this.FormularioAuxilioGestaoDia_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ObjetosComPrazoGuardaVencido_KeyDown);
            this.panel3.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPageExibicaoTodosAindaNaoEntregues.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceObjetosNaoEntregues)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BtnColarConteudoJaCopiado;
        private System.Windows.Forms.Button BtnImprimirListaAtual;
        private System.Windows.Forms.Button BtnRetornaTodosNaoEntregues;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource bindingSourceObjetosNaoEntregues;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LbnQuantidadeRegistros;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox FiltrarPorPrazosAVENCERCheckBox;
        private System.Windows.Forms.CheckBox FiltrarPorPrazosVENCENDOHOJECheckBox;
        private System.Windows.Forms.CheckBox FiltrarPorPrazosVENCIDOSCheckBox;
        private System.Windows.Forms.CheckBox FiltrarPorClassificacaoDIVERSOSCheckBox;
        private System.Windows.Forms.CheckBox FiltrarPorClassificacaoSEDEXCheckBox;
        private System.Windows.Forms.CheckBox FiltrarPorClassificacaoPACCCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoLdi;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataLancamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtdDiasCorridos;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoClassificacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoObjeto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sigla;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrazoTipoClassificacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusPrazo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataVencimento;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtdDiasVencidos;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPageExibicaoTodosAindaNaoEntregues;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPageExibicaoColarConteudoJaCopiado;
        private System.Windows.Forms.Panel panel1;
    }
}