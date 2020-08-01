namespace SISAPO
{
    partial class FormularioImpressaoEntregaAgruparObjetos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioImpressaoEntregaAgruparObjetos));
            this.BtnAgruparSelecionados = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.BtnConcluir = new System.Windows.Forms.Button();
            this.BtnRemoverSelecionados = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CheckBoxMarcar = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Selecao = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CodigoObjeto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grupo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeClienteAgrupado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataModificacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Endereco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnAgruparSelecionados
            // 
            this.BtnAgruparSelecionados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAgruparSelecionados.Location = new System.Drawing.Point(12, 18);
            this.BtnAgruparSelecionados.Name = "BtnAgruparSelecionados";
            this.BtnAgruparSelecionados.Size = new System.Drawing.Size(185, 40);
            this.BtnAgruparSelecionados.TabIndex = 0;
            this.BtnAgruparSelecionados.Text = "Agrupar Marcados";
            this.BtnAgruparSelecionados.UseVisualStyleBackColor = true;
            this.BtnAgruparSelecionados.Click += new System.EventHandler(this.BtnAgruparSelecionados_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnCancelar);
            this.panel1.Controls.Add(this.BtnConcluir);
            this.panel1.Controls.Add(this.BtnRemoverSelecionados);
            this.panel1.Controls.Add(this.BtnAgruparSelecionados);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 228);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(851, 74);
            this.panel1.TabIndex = 1;
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelar.Location = new System.Drawing.Point(405, 18);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(184, 40);
            this.BtnCancelar.TabIndex = 2;
            this.BtnCancelar.Text = "Cancelar impressão";
            this.BtnCancelar.UseVisualStyleBackColor = true;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // BtnConcluir
            // 
            this.BtnConcluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnConcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConcluir.Location = new System.Drawing.Point(595, 18);
            this.BtnConcluir.Name = "BtnConcluir";
            this.BtnConcluir.Size = new System.Drawing.Size(244, 40);
            this.BtnConcluir.TabIndex = 2;
            this.BtnConcluir.Text = "Tudo pronto, vamos imprimir";
            this.BtnConcluir.UseVisualStyleBackColor = true;
            this.BtnConcluir.Click += new System.EventHandler(this.BtnConcluir_Click);
            // 
            // BtnRemoverSelecionados
            // 
            this.BtnRemoverSelecionados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRemoverSelecionados.Location = new System.Drawing.Point(203, 18);
            this.BtnRemoverSelecionados.Name = "BtnRemoverSelecionados";
            this.BtnRemoverSelecionados.Size = new System.Drawing.Size(185, 40);
            this.BtnRemoverSelecionados.TabIndex = 2;
            this.BtnRemoverSelecionados.Text = "Remover Marcados";
            this.BtnRemoverSelecionados.UseVisualStyleBackColor = true;
            this.BtnRemoverSelecionados.Click += new System.EventHandler(this.BtnRemoverSelecionados_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.CheckBoxMarcar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(851, 51);
            this.panel2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(250, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(302, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "*Obs.: Não está sendo exibido objetos já entregues.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(663, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Selecione os objetos e faça o agrupamento para o mesmo recebedor.";
            // 
            // CheckBoxMarcar
            // 
            this.CheckBoxMarcar.AutoSize = true;
            this.CheckBoxMarcar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxMarcar.Location = new System.Drawing.Point(51, 29);
            this.CheckBoxMarcar.Name = "CheckBoxMarcar";
            this.CheckBoxMarcar.Size = new System.Drawing.Size(193, 24);
            this.CheckBoxMarcar.TabIndex = 0;
            this.CheckBoxMarcar.Text = "Marcar selecionados";
            this.CheckBoxMarcar.UseVisualStyleBackColor = true;
            this.CheckBoxMarcar.CheckedChanged += new System.EventHandler(this.CheckBoxMarcar_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 51);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(851, 177);
            this.panel3.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selecao,
            this.CodigoObjeto,
            this.NomeCliente,
            this.Grupo,
            this.NomeClienteAgrupado,
            this.DataModificacao,
            this.Endereco});
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridView1.RowHeadersWidth = 30;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(851, 177);
            this.dataGridView1.TabIndex = 3;
            // 
            // Selecao
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.NullValue = false;
            this.Selecao.DefaultCellStyle = dataGridViewCellStyle18;
            this.Selecao.HeaderText = "Seleção";
            this.Selecao.Name = "Selecao";
            this.Selecao.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Selecao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Selecao.Width = 51;
            // 
            // CodigoObjeto
            // 
            this.CodigoObjeto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CodigoObjeto.DataPropertyName = "CodigoObjeto";
            this.CodigoObjeto.HeaderText = "Código Objeto";
            this.CodigoObjeto.Name = "CodigoObjeto";
            this.CodigoObjeto.ReadOnly = true;
            this.CodigoObjeto.Width = 99;
            // 
            // NomeCliente
            // 
            this.NomeCliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NomeCliente.DataPropertyName = "NomeCliente";
            this.NomeCliente.HeaderText = "Nome cliente";
            this.NomeCliente.Name = "NomeCliente";
            this.NomeCliente.ReadOnly = true;
            // 
            // Grupo
            // 
            this.Grupo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Grupo.DataPropertyName = "Grupo";
            this.Grupo.HeaderText = "Grupo";
            this.Grupo.Name = "Grupo";
            this.Grupo.ReadOnly = true;
            this.Grupo.Visible = false;
            // 
            // NomeClienteAgrupado
            // 
            this.NomeClienteAgrupado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.NomeClienteAgrupado.DataPropertyName = "NomeClienteAgrupado";
            this.NomeClienteAgrupado.HeaderText = "Nome cliente agrupado";
            this.NomeClienteAgrupado.Name = "NomeClienteAgrupado";
            this.NomeClienteAgrupado.ReadOnly = true;
            this.NomeClienteAgrupado.Width = 142;
            // 
            // DataModificacao
            // 
            this.DataModificacao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DataModificacao.DataPropertyName = "DataModificacao";
            this.DataModificacao.HeaderText = "Data modificação";
            this.DataModificacao.Name = "DataModificacao";
            this.DataModificacao.Visible = false;
            // 
            // Endereco
            // 
            this.Endereco.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Endereco.DataPropertyName = "Endereco";
            this.Endereco.HeaderText = "Endereco";
            this.Endereco.Name = "Endereco";
            this.Endereco.Width = 78;
            // 
            // FormularioImpressaoEntregaAgruparObjetos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 302);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormularioImpressaoEntregaAgruparObjetos";
            this.Text = "Formulário de agrupar itens para impressão";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormularioImpressaoEntregaAgruparObjetos_FormClosed);
            this.Load += new System.EventHandler(this.FormularioImpressaoEntregaAgruparObjetos_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormularioImpressaoEntregaAgruparObjetos_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnAgruparSelecionados;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BtnRemoverSelecionados;
        private System.Windows.Forms.CheckBox CheckBoxMarcar;
        private System.Windows.Forms.Button BtnConcluir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selecao;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoObjeto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Grupo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeClienteAgrupado;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataModificacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Endereco;
        private System.Windows.Forms.Button BtnCancelar;
    }
}