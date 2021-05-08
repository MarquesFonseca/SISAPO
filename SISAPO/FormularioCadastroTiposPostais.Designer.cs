namespace SISAPO
{
    partial class FormularioCadastroTiposPostais
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioCadastroTiposPostais));
            this.BtnGravar = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.BtnColarConteudoJaCopiado = new System.Windows.Forms.Button();
            this.LblMensagem = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Servico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataLancamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataModificacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Situacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrazoDestinoCaixaPostal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrazoRemetenteCaidaPedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrazoRemetenteCaixaPostal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoClassificacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataAlteracao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnFechar = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageListaObjetosImportados = new System.Windows.Forms.TabPage();
            this.LblQuantidadeImportados = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageListaObjetosImportados.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnGravar
            // 
            this.BtnGravar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnGravar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGravar.Location = new System.Drawing.Point(531, 425);
            this.BtnGravar.Name = "BtnGravar";
            this.BtnGravar.Size = new System.Drawing.Size(126, 26);
            this.BtnGravar.TabIndex = 9;
            this.BtnGravar.Text = "&Importar lista";
            this.BtnGravar.UseVisualStyleBackColor = true;
            this.BtnGravar.Click += new System.EventHandler(this.BtnGravar_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 426);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(411, 24);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 7;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // BtnColarConteudoJaCopiado
            // 
            this.BtnColarConteudoJaCopiado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnColarConteudoJaCopiado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnColarConteudoJaCopiado.Image = global::SISAPO.Properties.Resources.icons8_colar_26;
            this.BtnColarConteudoJaCopiado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnColarConteudoJaCopiado.Location = new System.Drawing.Point(499, 12);
            this.BtnColarConteudoJaCopiado.Name = "BtnColarConteudoJaCopiado";
            this.BtnColarConteudoJaCopiado.Size = new System.Drawing.Size(252, 53);
            this.BtnColarConteudoJaCopiado.TabIndex = 3;
            this.BtnColarConteudoJaCopiado.Text = "&Colar conteúdo já copiado";
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
            this.label2.Location = new System.Drawing.Point(12, 405);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 6;
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelar.Location = new System.Drawing.Point(426, 425);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(103, 26);
            this.BtnCancelar.TabIndex = 8;
            this.BtnCancelar.Text = "&Cancelar";
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
            this.Servico,
            this.DataLancamento,
            this.DataModificacao,
            this.Situacao,
            this.PrazoDestinoCaixaPostal,
            this.PrazoRemetenteCaidaPedida,
            this.PrazoRemetenteCaixaPostal,
            this.TipoClassificacao,
            this.DataAlteracao});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(725, 288);
            this.dataGridView1.TabIndex = 1;
            // 
            // Servico
            // 
            this.Servico.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Servico.DataPropertyName = "Servico";
            this.Servico.HeaderText = "Tipo Serviço";
            this.Servico.Name = "Servico";
            this.Servico.ReadOnly = true;
            this.Servico.Width = 101;
            // 
            // DataLancamento
            // 
            this.DataLancamento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DataLancamento.DataPropertyName = "Sigla";
            this.DataLancamento.HeaderText = "Sigla";
            this.DataLancamento.Name = "DataLancamento";
            this.DataLancamento.ReadOnly = true;
            this.DataLancamento.Width = 64;
            // 
            // DataModificacao
            // 
            this.DataModificacao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DataModificacao.DataPropertyName = "Descricao";
            this.DataModificacao.HeaderText = "Descrição postal";
            this.DataModificacao.Name = "DataModificacao";
            this.DataModificacao.ReadOnly = true;
            // 
            // Situacao
            // 
            this.Situacao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Situacao.DataPropertyName = "PrazoDestinoCaidaPedida";
            this.Situacao.HeaderText = "Prazo Destino Caída Pedida";
            this.Situacao.Name = "Situacao";
            this.Situacao.ReadOnly = true;
            this.Situacao.Visible = false;
            // 
            // PrazoDestinoCaixaPostal
            // 
            this.PrazoDestinoCaixaPostal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.PrazoDestinoCaixaPostal.DataPropertyName = "PrazoDestinoCaixaPostal";
            this.PrazoDestinoCaixaPostal.HeaderText = "Prazo Destino Caixa Postal";
            this.PrazoDestinoCaixaPostal.Name = "PrazoDestinoCaixaPostal";
            this.PrazoDestinoCaixaPostal.ReadOnly = true;
            this.PrazoDestinoCaixaPostal.Visible = false;
            // 
            // PrazoRemetenteCaidaPedida
            // 
            this.PrazoRemetenteCaidaPedida.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.PrazoRemetenteCaidaPedida.DataPropertyName = "PrazoRemetenteCaidaPedida";
            this.PrazoRemetenteCaidaPedida.HeaderText = "Prazo Remetente Caida Pedida";
            this.PrazoRemetenteCaidaPedida.Name = "PrazoRemetenteCaidaPedida";
            this.PrazoRemetenteCaidaPedida.ReadOnly = true;
            this.PrazoRemetenteCaidaPedida.Visible = false;
            // 
            // PrazoRemetenteCaixaPostal
            // 
            this.PrazoRemetenteCaixaPostal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.PrazoRemetenteCaixaPostal.DataPropertyName = "PrazoRemetenteCaixaPostal";
            this.PrazoRemetenteCaixaPostal.HeaderText = "Prazo Remetente Caixa Postal";
            this.PrazoRemetenteCaixaPostal.Name = "PrazoRemetenteCaixaPostal";
            this.PrazoRemetenteCaixaPostal.ReadOnly = true;
            this.PrazoRemetenteCaixaPostal.Visible = false;
            // 
            // TipoClassificacao
            // 
            this.TipoClassificacao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.TipoClassificacao.DataPropertyName = "TipoClassificacao";
            this.TipoClassificacao.HeaderText = "Tipo Classificacao";
            this.TipoClassificacao.Name = "TipoClassificacao";
            this.TipoClassificacao.ReadOnly = true;
            this.TipoClassificacao.Visible = false;
            // 
            // DataAlteracao
            // 
            this.DataAlteracao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DataAlteracao.DataPropertyName = "DataAlteracao";
            this.DataAlteracao.HeaderText = "Data Alteração";
            this.DataAlteracao.Name = "DataAlteracao";
            this.DataAlteracao.ReadOnly = true;
            this.DataAlteracao.Visible = false;
            // 
            // BtnFechar
            // 
            this.BtnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFechar.Location = new System.Drawing.Point(659, 425);
            this.BtnFechar.Name = "BtnFechar";
            this.BtnFechar.Size = new System.Drawing.Size(92, 26);
            this.BtnFechar.TabIndex = 10;
            this.BtnFechar.Text = "&Fechar";
            this.BtnFechar.UseVisualStyleBackColor = true;
            this.BtnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageListaObjetosImportados);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 71);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(739, 331);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPageListaObjetosImportados
            // 
            this.tabPageListaObjetosImportados.Controls.Add(this.dataGridView1);
            this.tabPageListaObjetosImportados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageListaObjetosImportados.Location = new System.Drawing.Point(4, 33);
            this.tabPageListaObjetosImportados.Name = "tabPageListaObjetosImportados";
            this.tabPageListaObjetosImportados.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageListaObjetosImportados.Size = new System.Drawing.Size(731, 294);
            this.tabPageListaObjetosImportados.TabIndex = 0;
            this.tabPageListaObjetosImportados.Text = "Listados para importar";
            this.tabPageListaObjetosImportados.UseVisualStyleBackColor = true;
            // 
            // LblQuantidadeImportados
            // 
            this.LblQuantidadeImportados.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblQuantidadeImportados.ForeColor = System.Drawing.Color.Blue;
            this.LblQuantidadeImportados.Location = new System.Drawing.Point(9, 40);
            this.LblQuantidadeImportados.Name = "LblQuantidadeImportados";
            this.LblQuantidadeImportados.Size = new System.Drawing.Size(459, 28);
            this.LblQuantidadeImportados.TabIndex = 0;
            this.LblQuantidadeImportados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormularioCadastroTiposPostais
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 462);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.BtnColarConteudoJaCopiado);
            this.Controls.Add(this.LblQuantidadeImportados);
            this.Controls.Add(this.LblMensagem);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.BtnFechar);
            this.Controls.Add(this.BtnGravar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormularioCadastroTiposPostais";
            this.ShowInTaskbar = false;
            this.Text = "Importar Novos Tipos Postais";
            this.Load += new System.EventHandler(this.FormularioCadastroTiposPostais_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormularioCadastroObjetos_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageListaObjetosImportados.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Servico;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataLancamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataModificacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Situacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrazoDestinoCaixaPostal;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrazoRemetenteCaidaPedida;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrazoRemetenteCaixaPostal;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoClassificacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataAlteracao;
    }
}

