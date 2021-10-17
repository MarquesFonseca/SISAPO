namespace SISAPO
{
    partial class FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes));
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoObjeto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Destinatario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CepDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClienteRemetente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnderecoRemetente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroPLP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contrato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoAdministrativo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CartaoAdministrativo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(752, 42);
            this.label3.TabIndex = 1;
            this.label3.Text = "LISTA DE OBJETOS FALTANTES";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item,
            this.CodigoObjeto,
            this.Destinatario,
            this.CepDestino,
            this.ClienteRemetente,
            this.EnderecoRemetente,
            this.NumeroPLP,
            this.Contrato,
            this.CodigoAdministrativo,
            this.CartaoAdministrativo});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 42);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 40;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(752, 418);
            this.dataGridView1.TabIndex = 8;
            // 
            // Item
            // 
            this.Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Item.DataPropertyName = "Item";
            this.Item.HeaderText = "Item";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Width = 52;
            // 
            // CodigoObjeto
            // 
            this.CodigoObjeto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CodigoObjeto.DataPropertyName = "CodigoObjeto";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodigoObjeto.DefaultCellStyle = dataGridViewCellStyle2;
            this.CodigoObjeto.HeaderText = "Código Objeto";
            this.CodigoObjeto.Name = "CodigoObjeto";
            this.CodigoObjeto.ReadOnly = true;
            this.CodigoObjeto.Width = 99;
            // 
            // Destinatario
            // 
            this.Destinatario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Destinatario.DataPropertyName = "Destinatario";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Destinatario.DefaultCellStyle = dataGridViewCellStyle3;
            this.Destinatario.HeaderText = "Nome do Destinatário";
            this.Destinatario.Name = "Destinatario";
            this.Destinatario.ReadOnly = true;
            // 
            // CepDestino
            // 
            this.CepDestino.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CepDestino.DataPropertyName = "CepDestino";
            this.CepDestino.HeaderText = "CEP Destino";
            this.CepDestino.Name = "CepDestino";
            this.CepDestino.ReadOnly = true;
            this.CepDestino.Width = 92;
            // 
            // ClienteRemetente
            // 
            this.ClienteRemetente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ClienteRemetente.DataPropertyName = "ClienteRemetente";
            this.ClienteRemetente.HeaderText = "Remetente";
            this.ClienteRemetente.Name = "ClienteRemetente";
            this.ClienteRemetente.ReadOnly = true;
            this.ClienteRemetente.Width = 84;
            // 
            // EnderecoRemetente
            // 
            this.EnderecoRemetente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.EnderecoRemetente.DataPropertyName = "EnderecoRemetente";
            this.EnderecoRemetente.HeaderText = "End. Remetente";
            this.EnderecoRemetente.Name = "EnderecoRemetente";
            this.EnderecoRemetente.ReadOnly = true;
            this.EnderecoRemetente.Visible = false;
            // 
            // NumeroPLP
            // 
            this.NumeroPLP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NumeroPLP.DataPropertyName = "NumeroPLP";
            this.NumeroPLP.HeaderText = "Nº PLP";
            this.NumeroPLP.Name = "NumeroPLP";
            this.NumeroPLP.ReadOnly = true;
            this.NumeroPLP.Width = 67;
            // 
            // Contrato
            // 
            this.Contrato.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Contrato.DataPropertyName = "Contrato";
            this.Contrato.HeaderText = "Contrato";
            this.Contrato.Name = "Contrato";
            this.Contrato.ReadOnly = true;
            this.Contrato.Width = 72;
            // 
            // CodigoAdministrativo
            // 
            this.CodigoAdministrativo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CodigoAdministrativo.DataPropertyName = "CodigoAdministrativo";
            this.CodigoAdministrativo.HeaderText = "Cód. Adm.";
            this.CodigoAdministrativo.Name = "CodigoAdministrativo";
            this.CodigoAdministrativo.ReadOnly = true;
            this.CodigoAdministrativo.Width = 81;
            // 
            // CartaoAdministrativo
            // 
            this.CartaoAdministrativo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CartaoAdministrativo.DataPropertyName = "CartaoAdministrativo";
            this.CartaoAdministrativo.HeaderText = "Cartão Adm.";
            this.CartaoAdministrativo.Name = "CartaoAdministrativo";
            this.CartaoAdministrativo.ReadOnly = true;
            this.CartaoAdministrativo.Width = 90;
            // 
            // FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 460);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulário da lista de objetos faltantes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoObjeto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Destinatario;
        private System.Windows.Forms.DataGridViewTextBoxColumn CepDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClienteRemetente;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnderecoRemetente;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroPLP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contrato;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoAdministrativo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CartaoAdministrativo;
    }
}