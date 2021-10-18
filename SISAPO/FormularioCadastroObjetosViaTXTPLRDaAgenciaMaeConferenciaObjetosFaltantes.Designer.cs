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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes));
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CodigoObjeto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoLdi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomeCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataLancamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataModificacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Situacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Atualizado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjetoEntregue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CaixaPostal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnidadePostagem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MunicipioPostagem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CriacaoPostagem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CepDestinoPostagem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ARPostagem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MPPostagem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataMaxPrevistaEntregaPostagem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnidadeLOEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MunicipioLOEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CriacaoLOEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarteiroLOEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DistritoLOEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroLOEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnderecoLOEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BairroLOEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocalidadeLOEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SituacaoDestinatarioAusente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgrupadoDestinatarioAusente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoordenadasDestinatarioAusente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoPostalServico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoPostalSiglaCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoPostalNomeSiglaCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoPostalPrazoDiasCorridosRegulamentado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataListaAtual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroListaAtual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemAtual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtdTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.LblQtdFaltantes = new System.Windows.Forms.Label();
            this.BtnEnviarEmailAgenciaMae = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.label3.Size = new System.Drawing.Size(883, 42);
            this.label3.TabIndex = 0;
            this.label3.Text = "LISTA DE OBJETOS FALTANTES";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoObjeto,
            this.CodigoLdi,
            this.NomeCliente,
            this.DataLancamento,
            this.DataModificacao,
            this.Situacao,
            this.Atualizado,
            this.ObjetoEntregue,
            this.CaixaPostal,
            this.UnidadePostagem,
            this.MunicipioPostagem,
            this.CriacaoPostagem,
            this.CepDestinoPostagem,
            this.ARPostagem,
            this.MPPostagem,
            this.DataMaxPrevistaEntregaPostagem,
            this.UnidadeLOEC,
            this.MunicipioLOEC,
            this.CriacaoLOEC,
            this.CarteiroLOEC,
            this.DistritoLOEC,
            this.NumeroLOEC,
            this.EnderecoLOEC,
            this.BairroLOEC,
            this.LocalidadeLOEC,
            this.SituacaoDestinatarioAusente,
            this.AgrupadoDestinatarioAusente,
            this.CoordenadasDestinatarioAusente,
            this.Comentario,
            this.TipoPostalServico,
            this.TipoPostalSiglaCodigo,
            this.TipoPostalNomeSiglaCodigo,
            this.TipoPostalPrazoDiasCorridosRegulamentado,
            this.DataListaAtual,
            this.NumeroListaAtual,
            this.ItemAtual,
            this.QtdTotal});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 42);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(883, 364);
            this.dataGridView1.TabIndex = 1;
            // 
            // CodigoObjeto
            // 
            this.CodigoObjeto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CodigoObjeto.DataPropertyName = "CodigoObjeto";
            this.CodigoObjeto.HeaderText = "Código Objeto";
            this.CodigoObjeto.Name = "CodigoObjeto";
            this.CodigoObjeto.ReadOnly = true;
            this.CodigoObjeto.Width = 130;
            // 
            // CodigoLdi
            // 
            this.CodigoLdi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CodigoLdi.DataPropertyName = "CodigoLdi";
            this.CodigoLdi.HeaderText = "Núm. Ldi";
            this.CodigoLdi.Name = "CodigoLdi";
            this.CodigoLdi.ReadOnly = true;
            this.CodigoLdi.Width = 74;
            // 
            // NomeCliente
            // 
            this.NomeCliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NomeCliente.DataPropertyName = "NomeCliente";
            this.NomeCliente.HeaderText = "Nome Cliente";
            this.NomeCliente.Name = "NomeCliente";
            this.NomeCliente.ReadOnly = true;
            // 
            // DataLancamento
            // 
            this.DataLancamento.DataPropertyName = "DataLancamento";
            this.DataLancamento.HeaderText = "Data lançamento";
            this.DataLancamento.Name = "DataLancamento";
            this.DataLancamento.ReadOnly = true;
            this.DataLancamento.Visible = false;
            // 
            // DataModificacao
            // 
            this.DataModificacao.DataPropertyName = "DataModificacao";
            this.DataModificacao.HeaderText = "Data modificação";
            this.DataModificacao.Name = "DataModificacao";
            this.DataModificacao.ReadOnly = true;
            this.DataModificacao.Visible = false;
            // 
            // Situacao
            // 
            this.Situacao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Situacao.DataPropertyName = "Situacao";
            this.Situacao.HeaderText = "Situação";
            this.Situacao.Name = "Situacao";
            this.Situacao.ReadOnly = true;
            this.Situacao.Visible = false;
            // 
            // Atualizado
            // 
            this.Atualizado.DataPropertyName = "Atualizado";
            this.Atualizado.HeaderText = "Atualizado";
            this.Atualizado.Name = "Atualizado";
            this.Atualizado.ReadOnly = true;
            this.Atualizado.Visible = false;
            // 
            // ObjetoEntregue
            // 
            this.ObjetoEntregue.DataPropertyName = "ObjetoEntregue";
            this.ObjetoEntregue.HeaderText = "ObjetoEntregue";
            this.ObjetoEntregue.Name = "ObjetoEntregue";
            this.ObjetoEntregue.ReadOnly = true;
            this.ObjetoEntregue.Visible = false;
            // 
            // CaixaPostal
            // 
            this.CaixaPostal.DataPropertyName = "CaixaPostal";
            this.CaixaPostal.HeaderText = "CaixaPostal";
            this.CaixaPostal.Name = "CaixaPostal";
            this.CaixaPostal.ReadOnly = true;
            this.CaixaPostal.Visible = false;
            // 
            // UnidadePostagem
            // 
            this.UnidadePostagem.DataPropertyName = "UnidadePostagem";
            this.UnidadePostagem.HeaderText = "UnidadePostagem";
            this.UnidadePostagem.Name = "UnidadePostagem";
            this.UnidadePostagem.ReadOnly = true;
            this.UnidadePostagem.Visible = false;
            // 
            // MunicipioPostagem
            // 
            this.MunicipioPostagem.DataPropertyName = "MunicipioPostagem";
            this.MunicipioPostagem.HeaderText = "MunicipioPostagem";
            this.MunicipioPostagem.Name = "MunicipioPostagem";
            this.MunicipioPostagem.ReadOnly = true;
            this.MunicipioPostagem.Visible = false;
            // 
            // CriacaoPostagem
            // 
            this.CriacaoPostagem.DataPropertyName = "CriacaoPostagem";
            this.CriacaoPostagem.HeaderText = "CriacaoPostagem";
            this.CriacaoPostagem.Name = "CriacaoPostagem";
            this.CriacaoPostagem.ReadOnly = true;
            this.CriacaoPostagem.Visible = false;
            // 
            // CepDestinoPostagem
            // 
            this.CepDestinoPostagem.DataPropertyName = "CepDestinoPostagem";
            this.CepDestinoPostagem.HeaderText = "CepDestinoPostagem";
            this.CepDestinoPostagem.Name = "CepDestinoPostagem";
            this.CepDestinoPostagem.ReadOnly = true;
            this.CepDestinoPostagem.Visible = false;
            // 
            // ARPostagem
            // 
            this.ARPostagem.DataPropertyName = "ARPostagem";
            this.ARPostagem.HeaderText = "ARPostagem";
            this.ARPostagem.Name = "ARPostagem";
            this.ARPostagem.ReadOnly = true;
            this.ARPostagem.Visible = false;
            // 
            // MPPostagem
            // 
            this.MPPostagem.DataPropertyName = "MPPostagem";
            this.MPPostagem.HeaderText = "MPPostagem";
            this.MPPostagem.Name = "MPPostagem";
            this.MPPostagem.ReadOnly = true;
            this.MPPostagem.Visible = false;
            // 
            // DataMaxPrevistaEntregaPostagem
            // 
            this.DataMaxPrevistaEntregaPostagem.DataPropertyName = "DataMaxPrevistaEntregaPostagem";
            this.DataMaxPrevistaEntregaPostagem.HeaderText = "DataMaxPrevistaEntregaPostagem";
            this.DataMaxPrevistaEntregaPostagem.Name = "DataMaxPrevistaEntregaPostagem";
            this.DataMaxPrevistaEntregaPostagem.ReadOnly = true;
            this.DataMaxPrevistaEntregaPostagem.Visible = false;
            // 
            // UnidadeLOEC
            // 
            this.UnidadeLOEC.DataPropertyName = "UnidadeLOEC";
            this.UnidadeLOEC.HeaderText = "UnidadeLOEC";
            this.UnidadeLOEC.Name = "UnidadeLOEC";
            this.UnidadeLOEC.ReadOnly = true;
            this.UnidadeLOEC.Visible = false;
            // 
            // MunicipioLOEC
            // 
            this.MunicipioLOEC.DataPropertyName = "MunicipioLOEC";
            this.MunicipioLOEC.HeaderText = "MunicipioLOEC";
            this.MunicipioLOEC.Name = "MunicipioLOEC";
            this.MunicipioLOEC.ReadOnly = true;
            this.MunicipioLOEC.Visible = false;
            // 
            // CriacaoLOEC
            // 
            this.CriacaoLOEC.DataPropertyName = "CriacaoLOEC";
            this.CriacaoLOEC.HeaderText = "CriacaoLOEC";
            this.CriacaoLOEC.Name = "CriacaoLOEC";
            this.CriacaoLOEC.ReadOnly = true;
            this.CriacaoLOEC.Visible = false;
            // 
            // CarteiroLOEC
            // 
            this.CarteiroLOEC.DataPropertyName = "CarteiroLOEC";
            this.CarteiroLOEC.HeaderText = "CarteiroLOEC";
            this.CarteiroLOEC.Name = "CarteiroLOEC";
            this.CarteiroLOEC.ReadOnly = true;
            this.CarteiroLOEC.Visible = false;
            // 
            // DistritoLOEC
            // 
            this.DistritoLOEC.DataPropertyName = "DistritoLOEC";
            this.DistritoLOEC.HeaderText = "DistritoLOEC";
            this.DistritoLOEC.Name = "DistritoLOEC";
            this.DistritoLOEC.ReadOnly = true;
            this.DistritoLOEC.Visible = false;
            // 
            // NumeroLOEC
            // 
            this.NumeroLOEC.DataPropertyName = "NumeroLOEC";
            this.NumeroLOEC.HeaderText = "NumeroLOEC";
            this.NumeroLOEC.Name = "NumeroLOEC";
            this.NumeroLOEC.ReadOnly = true;
            this.NumeroLOEC.Visible = false;
            // 
            // EnderecoLOEC
            // 
            this.EnderecoLOEC.DataPropertyName = "EnderecoLOEC";
            this.EnderecoLOEC.HeaderText = "EnderecoLOEC";
            this.EnderecoLOEC.Name = "EnderecoLOEC";
            this.EnderecoLOEC.ReadOnly = true;
            this.EnderecoLOEC.Visible = false;
            // 
            // BairroLOEC
            // 
            this.BairroLOEC.DataPropertyName = "BairroLOEC";
            this.BairroLOEC.HeaderText = "BairroLOEC";
            this.BairroLOEC.Name = "BairroLOEC";
            this.BairroLOEC.ReadOnly = true;
            this.BairroLOEC.Visible = false;
            // 
            // LocalidadeLOEC
            // 
            this.LocalidadeLOEC.DataPropertyName = "LocalidadeLOEC";
            this.LocalidadeLOEC.HeaderText = "LocalidadeLOEC";
            this.LocalidadeLOEC.Name = "LocalidadeLOEC";
            this.LocalidadeLOEC.ReadOnly = true;
            this.LocalidadeLOEC.Visible = false;
            // 
            // SituacaoDestinatarioAusente
            // 
            this.SituacaoDestinatarioAusente.DataPropertyName = "SituacaoDestinatarioAusente";
            this.SituacaoDestinatarioAusente.HeaderText = "SituacaoDestinatarioAusente";
            this.SituacaoDestinatarioAusente.Name = "SituacaoDestinatarioAusente";
            this.SituacaoDestinatarioAusente.ReadOnly = true;
            this.SituacaoDestinatarioAusente.Visible = false;
            // 
            // AgrupadoDestinatarioAusente
            // 
            this.AgrupadoDestinatarioAusente.DataPropertyName = "AgrupadoDestinatarioAusente";
            this.AgrupadoDestinatarioAusente.HeaderText = "AgrupadoDestinatarioAusente";
            this.AgrupadoDestinatarioAusente.Name = "AgrupadoDestinatarioAusente";
            this.AgrupadoDestinatarioAusente.ReadOnly = true;
            this.AgrupadoDestinatarioAusente.Visible = false;
            // 
            // CoordenadasDestinatarioAusente
            // 
            this.CoordenadasDestinatarioAusente.DataPropertyName = "CoordenadasDestinatarioAusente";
            this.CoordenadasDestinatarioAusente.HeaderText = "CoordenadasDestinatarioAusente";
            this.CoordenadasDestinatarioAusente.Name = "CoordenadasDestinatarioAusente";
            this.CoordenadasDestinatarioAusente.ReadOnly = true;
            this.CoordenadasDestinatarioAusente.Visible = false;
            // 
            // Comentario
            // 
            this.Comentario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Comentario.DataPropertyName = "Comentario";
            this.Comentario.HeaderText = "Comentário";
            this.Comentario.Name = "Comentario";
            this.Comentario.ReadOnly = true;
            this.Comentario.Width = 85;
            // 
            // TipoPostalServico
            // 
            this.TipoPostalServico.DataPropertyName = "TipoPostalServico";
            this.TipoPostalServico.HeaderText = "TipoPostalServico";
            this.TipoPostalServico.Name = "TipoPostalServico";
            this.TipoPostalServico.ReadOnly = true;
            this.TipoPostalServico.Visible = false;
            // 
            // TipoPostalSiglaCodigo
            // 
            this.TipoPostalSiglaCodigo.DataPropertyName = "TipoPostalSiglaCodigo";
            this.TipoPostalSiglaCodigo.HeaderText = "TipoPostalSiglaCodigo";
            this.TipoPostalSiglaCodigo.Name = "TipoPostalSiglaCodigo";
            this.TipoPostalSiglaCodigo.ReadOnly = true;
            this.TipoPostalSiglaCodigo.Visible = false;
            // 
            // TipoPostalNomeSiglaCodigo
            // 
            this.TipoPostalNomeSiglaCodigo.DataPropertyName = "TipoPostalNomeSiglaCodigo";
            this.TipoPostalNomeSiglaCodigo.HeaderText = "TipoPostalNomeSiglaCodigo";
            this.TipoPostalNomeSiglaCodigo.Name = "TipoPostalNomeSiglaCodigo";
            this.TipoPostalNomeSiglaCodigo.ReadOnly = true;
            this.TipoPostalNomeSiglaCodigo.Visible = false;
            // 
            // TipoPostalPrazoDiasCorridosRegulamentado
            // 
            this.TipoPostalPrazoDiasCorridosRegulamentado.DataPropertyName = "TipoPostalPrazoDiasCorridosRegulamentado";
            this.TipoPostalPrazoDiasCorridosRegulamentado.HeaderText = "TipoPostalPrazoDiasCorridosRegulamentado";
            this.TipoPostalPrazoDiasCorridosRegulamentado.Name = "TipoPostalPrazoDiasCorridosRegulamentado";
            this.TipoPostalPrazoDiasCorridosRegulamentado.ReadOnly = true;
            this.TipoPostalPrazoDiasCorridosRegulamentado.Visible = false;
            // 
            // DataListaAtual
            // 
            this.DataListaAtual.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DataListaAtual.DataPropertyName = "DataListaAtual";
            this.DataListaAtual.HeaderText = "Data da lista";
            this.DataListaAtual.Name = "DataListaAtual";
            this.DataListaAtual.ReadOnly = true;
            this.DataListaAtual.Width = 68;
            // 
            // NumeroListaAtual
            // 
            this.NumeroListaAtual.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.NumeroListaAtual.DataPropertyName = "NumeroListaAtual";
            this.NumeroListaAtual.HeaderText = "Núm. lista";
            this.NumeroListaAtual.Name = "NumeroListaAtual";
            this.NumeroListaAtual.ReadOnly = true;
            this.NumeroListaAtual.Width = 72;
            // 
            // ItemAtual
            // 
            this.ItemAtual.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ItemAtual.DataPropertyName = "ItemAtual";
            dataGridViewCellStyle2.Format = "D5";
            dataGridViewCellStyle2.NullValue = null;
            this.ItemAtual.DefaultCellStyle = dataGridViewCellStyle2;
            this.ItemAtual.HeaderText = "Item";
            this.ItemAtual.Name = "ItemAtual";
            this.ItemAtual.ReadOnly = true;
            this.ItemAtual.Width = 52;
            // 
            // QtdTotal
            // 
            this.QtdTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.QtdTotal.DataPropertyName = "QtdTotal";
            this.QtdTotal.HeaderText = "Qtd. Total";
            this.QtdTotal.Name = "QtdTotal";
            this.QtdTotal.ReadOnly = true;
            this.QtdTotal.Width = 73;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnEnviarEmailAgenciaMae);
            this.panel1.Controls.Add(this.LblQtdFaltantes);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 406);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(883, 88);
            this.panel1.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Adobe Gothic Std B", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.Location = new System.Drawing.Point(12, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(293, 34);
            this.label13.TabIndex = 1;
            this.label13.Text = "Quantidade faltantes: ";
            // 
            // LblQtdFaltantes
            // 
            this.LblQtdFaltantes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblQtdFaltantes.AutoSize = true;
            this.LblQtdFaltantes.Font = new System.Drawing.Font("Adobe Gothic Std B", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblQtdFaltantes.ForeColor = System.Drawing.Color.DarkRed;
            this.LblQtdFaltantes.Location = new System.Drawing.Point(289, 26);
            this.LblQtdFaltantes.Name = "LblQtdFaltantes";
            this.LblQtdFaltantes.Size = new System.Drawing.Size(147, 34);
            this.LblQtdFaltantes.TabIndex = 1;
            this.LblQtdFaltantes.Text = "\'X\' Objetos";
            // 
            // BtnEnviarEmailAgenciaMae
            // 
            this.BtnEnviarEmailAgenciaMae.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnEnviarEmailAgenciaMae.Font = new System.Drawing.Font("Adobe Gothic Std B", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEnviarEmailAgenciaMae.Location = new System.Drawing.Point(681, 12);
            this.BtnEnviarEmailAgenciaMae.Name = "BtnEnviarEmailAgenciaMae";
            this.BtnEnviarEmailAgenciaMae.Size = new System.Drawing.Size(190, 62);
            this.BtnEnviarEmailAgenciaMae.TabIndex = 4;
            this.BtnEnviarEmailAgenciaMae.Text = "Comunicar itens à agência mãe";
            this.BtnEnviarEmailAgenciaMae.UseVisualStyleBackColor = true;
            this.BtnEnviarEmailAgenciaMae.Click += new System.EventHandler(this.BtnEnviarEmailAgenciaMae_Click);
            // 
            // FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 494);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulário da lista de objetos faltantes";
            this.Load += new System.EventHandler(this.FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoObjeto;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoLdi;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataLancamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataModificacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Situacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Atualizado;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjetoEntregue;
        private System.Windows.Forms.DataGridViewTextBoxColumn CaixaPostal;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnidadePostagem;
        private System.Windows.Forms.DataGridViewTextBoxColumn MunicipioPostagem;
        private System.Windows.Forms.DataGridViewTextBoxColumn CriacaoPostagem;
        private System.Windows.Forms.DataGridViewTextBoxColumn CepDestinoPostagem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ARPostagem;
        private System.Windows.Forms.DataGridViewTextBoxColumn MPPostagem;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataMaxPrevistaEntregaPostagem;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnidadeLOEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MunicipioLOEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn CriacaoLOEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarteiroLOEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn DistritoLOEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroLOEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnderecoLOEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn BairroLOEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalidadeLOEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn SituacaoDestinatarioAusente;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgrupadoDestinatarioAusente;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoordenadasDestinatarioAusente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comentario;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPostalServico;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPostalSiglaCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPostalNomeSiglaCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPostalPrazoDiasCorridosRegulamentado;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataListaAtual;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroListaAtual;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemAtual;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtdTotal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label LblQtdFaltantes;
        private System.Windows.Forms.Button BtnEnviarEmailAgenciaMae;
    }
}