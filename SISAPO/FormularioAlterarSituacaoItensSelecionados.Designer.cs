namespace SISAPO
{
    partial class FormularioAlterarSituacaoItensSelecionados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioAlterarSituacaoItensSelecionados));
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewMotivoBaixo = new System.Windows.Forms.DataGridView();
            this.MotivoBaixa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMotivoBaixo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.Location = new System.Drawing.Point(3, 6);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(240, 39);
            this.btnConfirmar.TabIndex = 0;
            this.btnConfirmar.Text = "[Enter] &Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(461, 6);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(181, 39);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "[Esc] Cancela&r";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.btnConfirmar);
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 326);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(645, 51);
            this.panel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 36);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selecione um motivo de baixo";
            // 
            // dataGridViewMotivoBaixo
            // 
            this.dataGridViewMotivoBaixo.AllowUserToAddRows = false;
            this.dataGridViewMotivoBaixo.AllowUserToDeleteRows = false;
            this.dataGridViewMotivoBaixo.AllowUserToOrderColumns = true;
            this.dataGridViewMotivoBaixo.AllowUserToResizeRows = false;
            this.dataGridViewMotivoBaixo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridViewMotivoBaixo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMotivoBaixo.ColumnHeadersVisible = false;
            this.dataGridViewMotivoBaixo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MotivoBaixa});
            this.dataGridViewMotivoBaixo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMotivoBaixo.Location = new System.Drawing.Point(0, 36);
            this.dataGridViewMotivoBaixo.MultiSelect = false;
            this.dataGridViewMotivoBaixo.Name = "dataGridViewMotivoBaixo";
            this.dataGridViewMotivoBaixo.ReadOnly = true;
            this.dataGridViewMotivoBaixo.RowHeadersVisible = false;
            this.dataGridViewMotivoBaixo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMotivoBaixo.Size = new System.Drawing.Size(645, 290);
            this.dataGridViewMotivoBaixo.TabIndex = 1;
            this.dataGridViewMotivoBaixo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridViewMotivoBaixo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // MotivoBaixa
            // 
            this.MotivoBaixa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MotivoBaixa.DataPropertyName = "MotivoBaixa";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MotivoBaixa.DefaultCellStyle = dataGridViewCellStyle1;
            this.MotivoBaixa.HeaderText = "MotivoBaixa";
            this.MotivoBaixa.Name = "MotivoBaixa";
            this.MotivoBaixa.ReadOnly = true;
            // 
            // FormularioAlterarSituacaoItensSelecionados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 377);
            this.Controls.Add(this.dataGridViewMotivoBaixo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormularioAlterarSituacaoItensSelecionados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulário alterar situação de itens";
            this.Load += new System.EventHandler(this.FormularioAlterarSituacaoItensSelecionados_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormularioAdicionarItemObjeto_KeyDown);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMotivoBaixo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewMotivoBaixo;
        private System.Windows.Forms.DataGridViewTextBoxColumn MotivoBaixa;
    }
}