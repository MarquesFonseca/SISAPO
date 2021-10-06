namespace SISAPO
{
    partial class FormularioCadastroObjetos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioCadastroObjetos));
            this.BtnGravar = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.BtnColarConteudoJaCopiado = new System.Windows.Forms.Button();
            this.LblMensagem = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CodigoObjeto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataLancamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataModificacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Situacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnFechar = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageListaObjetosImportados = new System.Windows.Forms.TabPage();
            this.LblQuantidadeImportados = new System.Windows.Forms.Label();
            this.BtnAdicionarItem = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnLimparListaAtual = new System.Windows.Forms.Button();
            this.BtnVerObjetosNaoAtualizados = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3Botoes = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageListaObjetosImportados.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel3Botoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnGravar
            // 
            this.BtnGravar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnGravar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGravar.Location = new System.Drawing.Point(4, 199);
            this.BtnGravar.Name = "BtnGravar";
            this.BtnGravar.Size = new System.Drawing.Size(190, 54);
            this.BtnGravar.TabIndex = 3;
            this.BtnGravar.Text = "&Iniciar importação da lista atual";
            this.BtnGravar.UseVisualStyleBackColor = true;
            this.BtnGravar.Click += new System.EventHandler(this.BtnGravar_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 29);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(399, 36);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // BtnColarConteudoJaCopiado
            // 
            this.BtnColarConteudoJaCopiado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnColarConteudoJaCopiado.Image = global::SISAPO.Properties.Resources.icons8_colar_26;
            this.BtnColarConteudoJaCopiado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnColarConteudoJaCopiado.Location = new System.Drawing.Point(12, 33);
            this.BtnColarConteudoJaCopiado.Name = "BtnColarConteudoJaCopiado";
            this.BtnColarConteudoJaCopiado.Size = new System.Drawing.Size(325, 53);
            this.BtnColarConteudoJaCopiado.TabIndex = 1;
            this.BtnColarConteudoJaCopiado.Text = "&Colar conteúdo já copiado do SRO";
            this.BtnColarConteudoJaCopiado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnColarConteudoJaCopiado.UseVisualStyleBackColor = true;
            this.BtnColarConteudoJaCopiado.Click += new System.EventHandler(this.BtnColarConteudoJaCopiado_Click);
            // 
            // LblMensagem
            // 
            this.LblMensagem.AutoSize = true;
            this.LblMensagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensagem.ForeColor = System.Drawing.Color.Red;
            this.LblMensagem.Location = new System.Drawing.Point(9, 12);
            this.LblMensagem.Name = "LblMensagem";
            this.LblMensagem.Size = new System.Drawing.Size(459, 18);
            this.LblMensagem.TabIndex = 0;
            this.LblMensagem.Text = "Certifique-se que o conteúdo na caixa de texto é o mesmo desejado!";
            this.LblMensagem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(12, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 0;
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelar.Location = new System.Drawing.Point(417, 28);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(208, 38);
            this.BtnCancelar.TabIndex = 2;
            this.BtnCancelar.Text = "&Cancelar carregamento";
            this.BtnCancelar.UseVisualStyleBackColor = true;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoObjeto,
            this.DataLancamento,
            this.DataModificacao,
            this.Situacao,
            this.Comentario});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(353, 214);
            this.dataGridView1.TabIndex = 0;
            // 
            // CodigoObjeto
            // 
            this.CodigoObjeto.DataPropertyName = "CodigoObjeto";
            this.CodigoObjeto.HeaderText = "Código Objeto";
            this.CodigoObjeto.Name = "CodigoObjeto";
            this.CodigoObjeto.ReadOnly = true;
            // 
            // DataLancamento
            // 
            this.DataLancamento.DataPropertyName = "DataLancamento";
            this.DataLancamento.HeaderText = "Data lançamento";
            this.DataLancamento.Name = "DataLancamento";
            this.DataLancamento.ReadOnly = true;
            // 
            // DataModificacao
            // 
            this.DataModificacao.DataPropertyName = "DataModificacao";
            this.DataModificacao.HeaderText = "Data modificação";
            this.DataModificacao.Name = "DataModificacao";
            this.DataModificacao.ReadOnly = true;
            // 
            // Situacao
            // 
            this.Situacao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Situacao.DataPropertyName = "Situacao";
            this.Situacao.HeaderText = "Situação baixa";
            this.Situacao.Name = "Situacao";
            this.Situacao.ReadOnly = true;
            // 
            // Comentario
            // 
            this.Comentario.DataPropertyName = "Comentario";
            this.Comentario.HeaderText = "Comentário";
            this.Comentario.Name = "Comentario";
            this.Comentario.ReadOnly = true;
            // 
            // BtnFechar
            // 
            this.BtnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFechar.Location = new System.Drawing.Point(631, 28);
            this.BtnFechar.Name = "BtnFechar";
            this.BtnFechar.Size = new System.Drawing.Size(116, 38);
            this.BtnFechar.TabIndex = 3;
            this.BtnFechar.Text = "&Fechar Tela";
            this.BtnFechar.UseVisualStyleBackColor = true;
            this.BtnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageListaObjetosImportados);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(367, 257);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageListaObjetosImportados
            // 
            this.tabPageListaObjetosImportados.Controls.Add(this.dataGridView1);
            this.tabPageListaObjetosImportados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageListaObjetosImportados.Location = new System.Drawing.Point(4, 33);
            this.tabPageListaObjetosImportados.Name = "tabPageListaObjetosImportados";
            this.tabPageListaObjetosImportados.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageListaObjetosImportados.Size = new System.Drawing.Size(359, 220);
            this.tabPageListaObjetosImportados.TabIndex = 0;
            this.tabPageListaObjetosImportados.Text = "Lista para importação";
            this.tabPageListaObjetosImportados.UseVisualStyleBackColor = true;
            // 
            // LblQuantidadeImportados
            // 
            this.LblQuantidadeImportados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblQuantidadeImportados.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblQuantidadeImportados.ForeColor = System.Drawing.Color.Blue;
            this.LblQuantidadeImportados.Location = new System.Drawing.Point(11, 89);
            this.LblQuantidadeImportados.Name = "LblQuantidadeImportados";
            this.LblQuantidadeImportados.Size = new System.Drawing.Size(733, 28);
            this.LblQuantidadeImportados.TabIndex = 2;
            this.LblQuantidadeImportados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnAdicionarItem
            // 
            this.BtnAdicionarItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAdicionarItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdicionarItem.Location = new System.Drawing.Point(4, 93);
            this.BtnAdicionarItem.Name = "BtnAdicionarItem";
            this.BtnAdicionarItem.Size = new System.Drawing.Size(190, 54);
            this.BtnAdicionarItem.TabIndex = 1;
            this.BtnAdicionarItem.Text = "&Adicionar Item à lista";
            this.BtnAdicionarItem.UseVisualStyleBackColor = true;
            this.BtnAdicionarItem.Click += new System.EventHandler(this.BtnAdicionarItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.BtnFechar);
            this.panel1.Controls.Add(this.BtnCancelar);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 384);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(763, 78);
            this.panel1.TabIndex = 4;
            // 
            // BtnLimparListaAtual
            // 
            this.BtnLimparListaAtual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnLimparListaAtual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLimparListaAtual.Location = new System.Drawing.Point(4, 33);
            this.BtnLimparListaAtual.Name = "BtnLimparListaAtual";
            this.BtnLimparListaAtual.Size = new System.Drawing.Size(190, 54);
            this.BtnLimparListaAtual.TabIndex = 0;
            this.BtnLimparListaAtual.Text = "&Limpar lista atual";
            this.BtnLimparListaAtual.UseVisualStyleBackColor = true;
            this.BtnLimparListaAtual.Click += new System.EventHandler(this.BtnLimparListaAtual_Click);
            // 
            // BtnVerObjetosNaoAtualizados
            // 
            this.BtnVerObjetosNaoAtualizados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnVerObjetosNaoAtualizados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVerObjetosNaoAtualizados.Location = new System.Drawing.Point(4, 153);
            this.BtnVerObjetosNaoAtualizados.Name = "BtnVerObjetosNaoAtualizados";
            this.BtnVerObjetosNaoAtualizados.Size = new System.Drawing.Size(190, 54);
            this.BtnVerObjetosNaoAtualizados.TabIndex = 2;
            this.BtnVerObjetosNaoAtualizados.Text = "&Ver objetos não buscados";
            this.BtnVerObjetosNaoAtualizados.UseVisualStyleBackColor = true;
            this.BtnVerObjetosNaoAtualizados.Click += new System.EventHandler(this.BtnVerObjetosNaoAtualizados_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(4, 121);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(557, 257);
            this.panel2.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tabControl1);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(557, 257);
            this.panel4.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.tabControl2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(367, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(190, 257);
            this.panel3.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(6, 231);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 22);
            this.button1.TabIndex = 1;
            this.button1.Text = "Fechar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl2.Location = new System.Drawing.Point(2, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(185, 232);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView2);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(177, 195);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Não buscados";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView2.Size = new System.Drawing.Size(171, 189);
            this.dataGridView2.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "CodigoObjeto";
            this.dataGridViewTextBoxColumn1.HeaderText = "Código Objeto";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // panel3Botoes
            // 
            this.panel3Botoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3Botoes.Controls.Add(this.BtnLimparListaAtual);
            this.panel3Botoes.Controls.Add(this.BtnAdicionarItem);
            this.panel3Botoes.Controls.Add(this.BtnVerObjetosNaoAtualizados);
            this.panel3Botoes.Controls.Add(this.BtnGravar);
            this.panel3Botoes.Location = new System.Drawing.Point(560, 121);
            this.panel3Botoes.Name = "panel3Botoes";
            this.panel3Botoes.Size = new System.Drawing.Size(200, 257);
            this.panel3Botoes.TabIndex = 3;
            // 
            // FormularioCadastroObjetos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 462);
            this.Controls.Add(this.panel3Botoes);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnColarConteudoJaCopiado);
            this.Controls.Add(this.LblQuantidadeImportados);
            this.Controls.Add(this.LblMensagem);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormularioCadastroObjetos";
            this.Text = "Importar Novo(s) Objeto(s)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormularioCadastroObjetos_FormClosed);
            this.Load += new System.EventHandler(this.FormularioCadastroObjetos_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormularioCadastroObjetos_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageListaObjetosImportados.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel3Botoes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnGravar;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button BtnColarConteudoJaCopiado;
        private System.Windows.Forms.Label LblMensagem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BtnFechar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageListaObjetosImportados;
        private System.Windows.Forms.Label LblQuantidadeImportados;
        private System.Windows.Forms.Button BtnAdicionarItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoObjeto;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataLancamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataModificacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Situacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comentario;
        private System.Windows.Forms.Button BtnLimparListaAtual;
        private System.Windows.Forms.Button BtnVerObjetosNaoAtualizados;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3Botoes;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}

