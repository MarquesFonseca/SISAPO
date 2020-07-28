namespace SISAPO
{
    partial class FormularioAuxilioGestaoDia
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioAuxilioGestaoDia));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CodigoLdi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoObjeto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataLancamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiasCorridos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnImprimirListaAtual = new System.Windows.Forms.Button();
            this.BtnRetornaTodosNaoEntregues = new System.Windows.Forms.Button();
            this.BtnColarConteudoJaCopiado = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LblQuantidade = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageNumeroLDI = new System.Windows.Forms.TabPage();
            this.tabPageObjeto = new System.Windows.Forms.TabPage();
            this.tabPageNomeCliente = new System.Windows.Forms.TabPage();
            this.tabPageLancamento = new System.Windows.Forms.TabPage();
            this.tabPageDiasCorridos = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoLdi,
            this.CodigoObjeto,
            this.NomeCliente,
            this.DataLancamento,
            this.DiasCorridos});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 70);
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
            this.dataGridView1.Size = new System.Drawing.Size(791, 341);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // CodigoLdi
            // 
            this.CodigoLdi.DataPropertyName = "CodigoLdi";
            this.CodigoLdi.FillWeight = 90F;
            this.CodigoLdi.HeaderText = "Número LDI";
            this.CodigoLdi.Name = "CodigoLdi";
            this.CodigoLdi.ReadOnly = true;
            this.CodigoLdi.Width = 90;
            // 
            // CodigoObjeto
            // 
            this.CodigoObjeto.DataPropertyName = "CodigoObjeto";
            this.CodigoObjeto.FillWeight = 120F;
            this.CodigoObjeto.HeaderText = "Objeto";
            this.CodigoObjeto.Name = "CodigoObjeto";
            this.CodigoObjeto.ReadOnly = true;
            this.CodigoObjeto.Width = 120;
            // 
            // NomeCliente
            // 
            this.NomeCliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NomeCliente.DataPropertyName = "NomeCliente";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NomeCliente.DefaultCellStyle = dataGridViewCellStyle2;
            this.NomeCliente.HeaderText = "Nome do Cliente";
            this.NomeCliente.Name = "NomeCliente";
            this.NomeCliente.ReadOnly = true;
            // 
            // DataLancamento
            // 
            this.DataLancamento.DataPropertyName = "DataLancamento";
            this.DataLancamento.FillWeight = 150F;
            this.DataLancamento.HeaderText = "Lançamento";
            this.DataLancamento.Name = "DataLancamento";
            this.DataLancamento.ReadOnly = true;
            this.DataLancamento.Width = 150;
            // 
            // DiasCorridos
            // 
            this.DiasCorridos.DataPropertyName = "DiasCorridos";
            this.DiasCorridos.HeaderText = "Dias corridos";
            this.DiasCorridos.Name = "DiasCorridos";
            this.DiasCorridos.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BtnImprimirListaAtual);
            this.panel3.Controls.Add(this.BtnRetornaTodosNaoEntregues);
            this.panel3.Controls.Add(this.BtnColarConteudoJaCopiado);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(791, 48);
            this.panel3.TabIndex = 0;
            // 
            // BtnImprimirListaAtual
            // 
            this.BtnImprimirListaAtual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnImprimirListaAtual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnImprimirListaAtual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImprimirListaAtual.Image = global::SISAPO.Properties.Resources.impressão_26;
            this.BtnImprimirListaAtual.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnImprimirListaAtual.Location = new System.Drawing.Point(589, 5);
            this.BtnImprimirListaAtual.Name = "BtnImprimirListaAtual";
            this.BtnImprimirListaAtual.Size = new System.Drawing.Size(196, 38);
            this.BtnImprimirListaAtual.TabIndex = 1;
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
            this.BtnRetornaTodosNaoEntregues.Location = new System.Drawing.Point(274, 5);
            this.BtnRetornaTodosNaoEntregues.Name = "BtnRetornaTodosNaoEntregues";
            this.BtnRetornaTodosNaoEntregues.Size = new System.Drawing.Size(261, 38);
            this.BtnRetornaTodosNaoEntregues.TabIndex = 0;
            this.BtnRetornaTodosNaoEntregues.Text = "Todos ainda não entregues";
            this.BtnRetornaTodosNaoEntregues.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnRetornaTodosNaoEntregues.UseVisualStyleBackColor = true;
            this.BtnRetornaTodosNaoEntregues.Click += new System.EventHandler(this.BtnRetornaTodosNaoEntregues_Click);
            // 
            // BtnColarConteudoJaCopiado
            // 
            this.BtnColarConteudoJaCopiado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnColarConteudoJaCopiado.Image = global::SISAPO.Properties.Resources.icons8_colar_26;
            this.BtnColarConteudoJaCopiado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnColarConteudoJaCopiado.Location = new System.Drawing.Point(5, 5);
            this.BtnColarConteudoJaCopiado.Name = "BtnColarConteudoJaCopiado";
            this.BtnColarConteudoJaCopiado.Size = new System.Drawing.Size(252, 38);
            this.BtnColarConteudoJaCopiado.TabIndex = 0;
            this.BtnColarConteudoJaCopiado.Text = "&Colar conteúdo já copiado";
            this.BtnColarConteudoJaCopiado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnColarConteudoJaCopiado.UseVisualStyleBackColor = true;
            this.BtnColarConteudoJaCopiado.Click += new System.EventHandler(this.BtnColarConteudoJaCopiado_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.LblQuantidade);
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 411);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(791, 51);
            this.panel2.TabIndex = 3;
            // 
            // LblQuantidade
            // 
            this.LblQuantidade.AutoSize = true;
            this.LblQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblQuantidade.ForeColor = System.Drawing.Color.Maroon;
            this.LblQuantidade.Location = new System.Drawing.Point(3, 11);
            this.LblQuantidade.Name = "LblQuantidade";
            this.LblQuantidade.Size = new System.Drawing.Size(0, 31);
            this.LblQuantidade.TabIndex = 1;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(610, 11);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(175, 31);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "&Fechar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tabPageNumeroLDI);
            this.tabControl1.Controls.Add(this.tabPageObjeto);
            this.tabControl1.Controls.Add(this.tabPageNomeCliente);
            this.tabControl1.Controls.Add(this.tabPageLancamento);
            this.tabControl1.Controls.Add(this.tabPageDiasCorridos);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 48);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(791, 22);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPageNumeroLDI
            // 
            this.tabPageNumeroLDI.Location = new System.Drawing.Point(4, 25);
            this.tabPageNumeroLDI.Name = "tabPageNumeroLDI";
            this.tabPageNumeroLDI.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNumeroLDI.Size = new System.Drawing.Size(783, 0);
            this.tabPageNumeroLDI.TabIndex = 0;
            this.tabPageNumeroLDI.Text = "Ordenar por: Número LDI";
            this.tabPageNumeroLDI.UseVisualStyleBackColor = true;
            // 
            // tabPageObjeto
            // 
            this.tabPageObjeto.Location = new System.Drawing.Point(4, 25);
            this.tabPageObjeto.Name = "tabPageObjeto";
            this.tabPageObjeto.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageObjeto.Size = new System.Drawing.Size(783, 0);
            this.tabPageObjeto.TabIndex = 1;
            this.tabPageObjeto.Text = "Ordenar por: Objeto";
            this.tabPageObjeto.UseVisualStyleBackColor = true;
            // 
            // tabPageNomeCliente
            // 
            this.tabPageNomeCliente.Location = new System.Drawing.Point(4, 25);
            this.tabPageNomeCliente.Name = "tabPageNomeCliente";
            this.tabPageNomeCliente.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNomeCliente.Size = new System.Drawing.Size(783, 0);
            this.tabPageNomeCliente.TabIndex = 2;
            this.tabPageNomeCliente.Text = "Ordenar por: Nome do Cliente";
            this.tabPageNomeCliente.UseVisualStyleBackColor = true;
            // 
            // tabPageLancamento
            // 
            this.tabPageLancamento.Location = new System.Drawing.Point(4, 25);
            this.tabPageLancamento.Name = "tabPageLancamento";
            this.tabPageLancamento.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLancamento.Size = new System.Drawing.Size(783, 0);
            this.tabPageLancamento.TabIndex = 3;
            this.tabPageLancamento.Text = "Ordenar por: Lançamento";
            this.tabPageLancamento.UseVisualStyleBackColor = true;
            // 
            // tabPageDiasCorridos
            // 
            this.tabPageDiasCorridos.Location = new System.Drawing.Point(4, 25);
            this.tabPageDiasCorridos.Name = "tabPageDiasCorridos";
            this.tabPageDiasCorridos.Size = new System.Drawing.Size(783, 0);
            this.tabPageDiasCorridos.TabIndex = 4;
            this.tabPageDiasCorridos.Text = "Ordenar por: Dias corridos";
            this.tabPageDiasCorridos.UseVisualStyleBackColor = true;
            // 
            // FormularioAuxilioGestaoDia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 462);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "FormularioAuxilioGestaoDia";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulário de auxílio à gestão do dia";
            this.Load += new System.EventHandler(this.FormularioAuxilioGestaoDia_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ObjetosComPrazoGuardaVencido_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BtnColarConteudoJaCopiado;
        private System.Windows.Forms.Button BtnImprimirListaAtual;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageNumeroLDI;
        private System.Windows.Forms.TabPage tabPageObjeto;
        private System.Windows.Forms.TabPage tabPageNomeCliente;
        private System.Windows.Forms.TabPage tabPageLancamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoLdi;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoObjeto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataLancamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiasCorridos;
        private System.Windows.Forms.TabPage tabPageDiasCorridos;
        private System.Windows.Forms.Button BtnRetornaTodosNaoEntregues;
        private System.Windows.Forms.Label LblQuantidade;
    }
}