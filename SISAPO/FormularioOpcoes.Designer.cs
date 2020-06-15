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
            this.checkBoxRequererVerificacaoDeObjetosJaEntregues = new System.Windows.Forms.CheckBox();
            this.checkBoxExibirItensJaEntregues = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageObjetosAguardandoRetirada = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tabPageExibirItensJaEntregues = new System.Windows.Forms.TabPage();
            this.tabPageConfiguracoesAgencia = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.LblNomeAgencia = new System.Windows.Forms.Label();
            this.BtnAtualizarConfiguracoesAgencia = new System.Windows.Forms.Button();
            this.txtEnderecoAgencia = new System.Windows.Forms.TextBox();
            this.bindingSourceTabelaConfiguracoesSistema = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetConfiguracoes = new SISAPO.DataSetConfiguracoes();
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
            // checkBoxRequererVerificacaoDeObjetosJaEntregues
            // 
            this.checkBoxRequererVerificacaoDeObjetosJaEntregues.AutoSize = true;
            this.checkBoxRequererVerificacaoDeObjetosJaEntregues.Location = new System.Drawing.Point(15, 19);
            this.checkBoxRequererVerificacaoDeObjetosJaEntregues.Name = "checkBoxRequererVerificacaoDeObjetosJaEntregues";
            this.checkBoxRequererVerificacaoDeObjetosJaEntregues.Size = new System.Drawing.Size(279, 17);
            this.checkBoxRequererVerificacaoDeObjetosJaEntregues.TabIndex = 0;
            this.checkBoxRequererVerificacaoDeObjetosJaEntregues.Text = "&Solicitar verificação de Objetos ainda não Entregues?";
            this.checkBoxRequererVerificacaoDeObjetosJaEntregues.UseVisualStyleBackColor = true;
            this.checkBoxRequererVerificacaoDeObjetosJaEntregues.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBoxExibirItensJaEntregues
            // 
            this.checkBoxExibirItensJaEntregues.AutoSize = true;
            this.checkBoxExibirItensJaEntregues.Location = new System.Drawing.Point(15, 19);
            this.checkBoxExibirItensJaEntregues.Name = "checkBoxExibirItensJaEntregues";
            this.checkBoxExibirItensJaEntregues.Size = new System.Drawing.Size(189, 17);
            this.checkBoxExibirItensJaEntregues.TabIndex = 0;
            this.checkBoxExibirItensJaEntregues.Text = "Incluir itens entregues na pesquisa";
            this.checkBoxExibirItensJaEntregues.UseVisualStyleBackColor = true;
            this.checkBoxExibirItensJaEntregues.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
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
            this.tabPageObjetosAguardandoRetirada.Controls.Add(this.checkBox1);
            this.tabPageObjetosAguardandoRetirada.Controls.Add(this.checkBoxRequererVerificacaoDeObjetosJaEntregues);
            this.tabPageObjetosAguardandoRetirada.Location = new System.Drawing.Point(4, 22);
            this.tabPageObjetosAguardandoRetirada.Name = "tabPageObjetosAguardandoRetirada";
            this.tabPageObjetosAguardandoRetirada.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageObjetosAguardandoRetirada.Size = new System.Drawing.Size(731, 385);
            this.tabPageObjetosAguardandoRetirada.TabIndex = 0;
            this.tabPageObjetosAguardandoRetirada.Text = "Objetos aguardando retirada";
            this.tabPageObjetosAguardandoRetirada.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 42);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(145, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "&Marcar todos Atualizados";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // tabPageExibirItensJaEntregues
            // 
            this.tabPageExibirItensJaEntregues.Controls.Add(this.checkBoxExibirItensJaEntregues);
            this.tabPageExibirItensJaEntregues.Location = new System.Drawing.Point(4, 22);
            this.tabPageExibirItensJaEntregues.Name = "tabPageExibirItensJaEntregues";
            this.tabPageExibirItensJaEntregues.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExibirItensJaEntregues.Size = new System.Drawing.Size(731, 385);
            this.tabPageExibirItensJaEntregues.TabIndex = 1;
            this.tabPageExibirItensJaEntregues.Text = "Consulta de objetos aguardando retirada";
            this.tabPageExibirItensJaEntregues.UseVisualStyleBackColor = true;
            // 
            // tabPageConfiguracoesAgencia
            // 
            this.tabPageConfiguracoesAgencia.Controls.Add(this.label1);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.LblNomeAgencia);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.BtnAtualizarConfiguracoesAgencia);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.txtEnderecoAgencia);
            this.tabPageConfiguracoesAgencia.Controls.Add(this.txtNomeAgencia);
            this.tabPageConfiguracoesAgencia.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfiguracoesAgencia.Name = "tabPageConfiguracoesAgencia";
            this.tabPageConfiguracoesAgencia.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfiguracoesAgencia.Size = new System.Drawing.Size(731, 385);
            this.tabPageConfiguracoesAgencia.TabIndex = 2;
            this.tabPageConfiguracoesAgencia.Text = "Configurações da agência";
            this.tabPageConfiguracoesAgencia.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Endereço da agência";
            // 
            // LblNomeAgencia
            // 
            this.LblNomeAgencia.AutoSize = true;
            this.LblNomeAgencia.Location = new System.Drawing.Point(7, 8);
            this.LblNomeAgencia.Name = "LblNomeAgencia";
            this.LblNomeAgencia.Size = new System.Drawing.Size(91, 13);
            this.LblNomeAgencia.TabIndex = 0;
            this.LblNomeAgencia.Text = "Nome da agência";
            // 
            // BtnAtualizarConfiguracoesAgencia
            // 
            this.BtnAtualizarConfiguracoesAgencia.Location = new System.Drawing.Point(653, 21);
            this.BtnAtualizarConfiguracoesAgencia.Name = "BtnAtualizarConfiguracoesAgencia";
            this.BtnAtualizarConfiguracoesAgencia.Size = new System.Drawing.Size(75, 67);
            this.BtnAtualizarConfiguracoesAgencia.TabIndex = 4;
            this.BtnAtualizarConfiguracoesAgencia.Text = "Atualizar";
            this.BtnAtualizarConfiguracoesAgencia.UseVisualStyleBackColor = true;
            this.BtnAtualizarConfiguracoesAgencia.Click += new System.EventHandler(this.BtnAtualizarConfiguracoesAgencia_Click);
            // 
            // txtEnderecoAgencia
            // 
            this.txtEnderecoAgencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnderecoAgencia.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSourceTabelaConfiguracoesSistema, "EnderecoAgenciaLocal", true));
            this.txtEnderecoAgencia.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTabelaConfiguracoesSistema, "EnderecoAgenciaLocal", true));
            this.txtEnderecoAgencia.Location = new System.Drawing.Point(9, 68);
            this.txtEnderecoAgencia.Name = "txtEnderecoAgencia";
            this.txtEnderecoAgencia.Size = new System.Drawing.Size(640, 20);
            this.txtEnderecoAgencia.TabIndex = 3;
            this.txtEnderecoAgencia.Text = "604 SUL AV LO-15 NR 09/10 - LTS 115/16 - ";
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
            // txtNomeAgencia
            // 
            this.txtNomeAgencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNomeAgencia.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSourceTabelaConfiguracoesSistema, "NomeAgenciaLocal", true));
            this.txtNomeAgencia.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTabelaConfiguracoesSistema, "NomeAgenciaLocal", true));
            this.txtNomeAgencia.Location = new System.Drawing.Point(8, 24);
            this.txtNomeAgencia.Name = "txtNomeAgencia";
            this.txtNomeAgencia.Size = new System.Drawing.Size(640, 20);
            this.txtNomeAgencia.TabIndex = 1;
            this.txtNomeAgencia.Text = "AC VIA PALMAS BRASIL";
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
            this.tabPageObjetosAguardandoRetirada.PerformLayout();
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
        private System.Windows.Forms.CheckBox checkBoxRequererVerificacaoDeObjetosJaEntregues;
        private System.Windows.Forms.CheckBox checkBoxExibirItensJaEntregues;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageObjetosAguardandoRetirada;
        private System.Windows.Forms.TabPage tabPageExibirItensJaEntregues;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabPage tabPageConfiguracoesAgencia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblNomeAgencia;
        private System.Windows.Forms.Button BtnAtualizarConfiguracoesAgencia;
        private System.Windows.Forms.TextBox txtEnderecoAgencia;
        private System.Windows.Forms.TextBox txtNomeAgencia;
        private System.Windows.Forms.BindingSource bindingSourceTabelaConfiguracoesSistema;
        private DataSetConfiguracoes dataSetConfiguracoes;
        private DataSetConfiguracoesTableAdapters.TabelaConfiguracoesSistemaTableAdapter tabelaConfiguracoesSistemaTableAdapter;
    }
}