using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using SISAPO.ClassesDiversas;

namespace SISAPO
{
    public partial class FormularioCadastroObjetosViaTXTPLRDaAgenciaMae : Form
    {
        DataTable listaObjetos = new DataTable();
        StringBuilder textoColadoAreaTransferencia = new StringBuilder();

        public FormularioCadastroObjetosViaTXTPLRDaAgenciaMae()
        {
            InitializeComponent();
            //tabControl1.Visible = false;
            this.BtnGravar.Enabled = false;
            this.backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(bw_DoWork);
            this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void FormularioCadastroObjetosViaTXTPLRDaAgenciaMae_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
        }

        public static FormularioCadastroObjetosViaTXTPLRDaAgenciaMae RetornaComponentesFormularioCadastroObjetosViaTXTPLRDaAgenciaMae()
        {
            FormularioCadastroObjetosViaTXTPLRDaAgenciaMae formularioCadastroObjetosViaTXTPLRDaAgenciaMae;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioCadastroObjetosViaTXTPLRDaAgenciaMae")
                {
                    formularioCadastroObjetosViaTXTPLRDaAgenciaMae = (FormularioCadastroObjetosViaTXTPLRDaAgenciaMae)item;
                    return (FormularioCadastroObjetosViaTXTPLRDaAgenciaMae)item;
                }
            }
            return null;
        }

        private DataTable RetornaListaObjetos(string Texto)
        {
            DataTable dtbLista = new DataTable();
            dtbLista.Columns.Add("CodigoObjeto", typeof(string));
            dtbLista.Columns.Add("DataLancamento", typeof(DateTime));
            dtbLista.Columns.Add("DataModificacao", typeof(string));
            dtbLista.Columns.Add("Situacao", typeof(string));
            dtbLista.Columns.Add("Comentario", typeof(string));
            try
            {
                string[] linha = Texto.Split('\n');

                for (int i = 0; i < linha.Length; i++)
                {
                    if (linha[i] == "" || linha[i] == "\r") continue;
                    string[] Parteslinha = linha[i].Split('\t');
                    string ParteLinhaAgencia = Parteslinha.Length >= 1 ? Parteslinha[0].Trim().ToUpper() : "";
                    string ParteLinhaCodigoObjeto = Parteslinha.Length >= 2 ? Parteslinha[1].Trim().ToUpper() : "";
                    string ParteLinhaDataLancamento = Parteslinha.Length >= 3 ? Parteslinha[2].Trim().ToUpper() : "";
                    string ParteLinhaDataModificacao = Parteslinha.Length >= 4 ? Parteslinha[3].Trim().ToUpper() : "";
                    string ParteLinhaSituacao = Parteslinha.Length >= 5 ? Parteslinha[4].Replace("\r", "").Trim().ToUpper() : "";
                    ParteLinhaSituacao = ParteLinhaSituacao == "OBJETO DISTRIBUIDO" ? "Entregue".ToUpper() : ParteLinhaSituacao;
                    ParteLinhaSituacao = ParteLinhaSituacao == "" ? "Aguardando retirada".ToUpper() : ParteLinhaSituacao;
                    ParteLinhaSituacao = ParteLinhaSituacao.RemoveAcento_DICIONARIO();
                    string ParteLinhaComentario = "PCT";

                    if (ParteLinhaCodigoObjeto != "")
                    {
                        dtbLista.Rows.Add
                            (
                                ParteLinhaCodigoObjeto,
                                Convert.ToDateTime(ParteLinhaDataLancamento),
                                ParteLinhaDataModificacao,
                                ParteLinhaSituacao,
                                ParteLinhaComentario
                            );
                    }
                }


                dtbLista.DefaultView.Sort = "DataLancamento DESC";
                dtbLista = dtbLista.DefaultView.ToTable();

                return dtbLista;
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
                return dtbLista;
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            FormularioConsulta lll = new FormularioConsulta();
            lll.ShowDialog();
        }

        private void FormularioCadastroObjetosViaTXTPLRDaAgenciaMae_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            if (e.KeyCode == Keys.F9)
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().sRORastreamentoUnificadoToolStripMenuItem_Click(sender, e);
            }
            if (e.KeyCode == Keys.F12)
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().visualizarListaDeObjetosToolStripMenuItem_Click(sender, e);
            }
        }

        private void BtnColarConteudoJaCopiado_Click(object sender, EventArgs e)
        {
            textoColadoAreaTransferencia = new StringBuilder();

            string curDir = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeArquivo = "itensAtualizacao.txt";
            string nomeEnderecoArquivo = string.Format(@"{0}\{1}", curDir, nomeArquivo);


            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) ||
                string.IsNullOrEmpty(Clipboard.GetDataObject().GetDataPresent(DataFormats.Text).ToString()))
            {
                textoColadoAreaTransferencia = textoColadoAreaTransferencia.Append(Clipboard.GetDataObject().GetData(DataFormats.Text).ToString());

                //grava texto no arquivo
                using (Arquivos arq = new Arquivos())
                {
                    arq.GravarArquivo(nomeEnderecoArquivo, textoColadoAreaTransferencia.ToString());
                }

                ////int saida = arq.AbrirArquivo(nomeEnderecoArquivo);
                //string file = string.Format(@"{0}\{1}", curDir, nomeArquivo);

                try
                {
                    progressBar1.Value = 0;
                    string tempTXT = textoColadoAreaTransferencia.ToString().Replace("\t", " ");
                    while (tempTXT.IndexOf("  ") >= 0) tempTXT = tempTXT.Replace("  ", " ");
                    //textBox1.Text = tempTXT;

                    listaObjetos = RetornaListaObjetos(textoColadoAreaTransferencia.ToString());

                    if (listaObjetos.Rows.Count == 0)
                    {
                        LblQuantidadeImportados.Text = "";
                        //tabControl1.Visible = false;
                        this.BtnGravar.Enabled = false;
                        label2.Text = "";
                        progressBar1.Visible = false;
                    }
                    if (listaObjetos.Rows.Count > 0)
                    {
                        LblQuantidadeImportados.Text = string.Format("Quantidade de objetos para importação: '{0}' objetos", listaObjetos.Rows.Count);
                        dataGridViewQRCode.DataSource = listaObjetos;
                        //tabControl1.Visible = true;
                        this.BtnGravar.Enabled = true;
                        label2.Text = "Barra de progresso";
                        progressBar1.Visible = true;
                        BtnGravar.Focus();
                    }
                }
                catch (IOException) { }
            }
            else
            {
                //MessageBox.Show("Não há texto na área de transferência.");
                //abrir a busca do arquivo no computador        
                //int size = -1;
                openFileDialog1.Title = "Selecione um arquivo para importar";
                openFileDialog1.Filter = "txt files (*.txt)|*.txt";
                openFileDialog1.FileName = "";
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.CheckPathExists = true;
                openFileDialog1.Multiselect = false;
                DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    string file = openFileDialog1.FileName;

                    try
                    {
                        StringBuilder arquivo = new StringBuilder();
                        arquivo.Append(File.ReadAllText(file).ToString());
                        //textBox1.Text = arquivo.ToString();
                        BtnGravar.Focus();
                    }
                    catch (IOException) { }
                }
            }
        }

        private void BtnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                //listaObjetos = RetornaListaObjetos(textoColadoAreaTransferencia.ToString());
                //dataGridView1.DataSource = listaObjetos;
                if (listaObjetos.Rows.Count == 0)
                {
                    Mensagens.Informa("Não foi possível importar. Nenhuma lista encontrada."); return;
                }

                //verifica e informa que a possibilidade de falta de objetos lidos na lista / nas listas(varias listas)
                var ListasAgrupadas = listaObjetos.AsEnumerable().GroupBy(T => T["NumeroListaAtual"]);//agrupo por lista
                foreach (var itemDataTableNumeroLista in ListasAgrupadas)
                {
                    DataTable DataTableNumeroLista = itemDataTableNumeroLista.AsEnumerable().OrderBy(T => T["ItemAtual"]).OrderBy(T => T["QtdTotal"]).CopyToDataTable();
                    string DataListaAtual = DataTableNumeroLista.Rows[0]["DataListaAtual"].ToDateTime().ToShortDateString();
                    string NumeroListaAtual = DataTableNumeroLista.Rows[0]["NumeroListaAtual"].ToString();
                    int QtdTotalLidosDestaLista = DataTableNumeroLista.Rows.Count;
                    int QtdTotalPLR = DataTableNumeroLista.Rows[0]["QtdTotal"].ToInt();
                    if (QtdTotalLidosDestaLista < QtdTotalPLR)
                    {
                        //informa na mensagem que falta objetos a ser lançados....
                        StringBuilder str = new StringBuilder();
                        str.AppendLine(string.Format("Atenção: Para a PLR [{0}] criada em [{1}], existem itens faltantes!\n", NumeroListaAtual, DataListaAtual));
                        str.AppendLine(string.Format("Qtd. total da PLR..: {0}", QtdTotalPLR));
                        str.AppendLine(string.Format("Qtd. de itens lidos: {0}", QtdTotalLidosDestaLista));
                        str.AppendLine(string.Format("\n"));
                        str.AppendLine(string.Format("Deseja continuar?"));
                        string mensagemFormatada = str.ToString();
                        if (Mensagens.Pergunta(mensagemFormatada, MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }

                progressBar1.Value = 0;
                label2.Text = "Barra de progresso";

                if (!this.backgroundWorker1.IsBusy)
                {
                    this.backgroundWorker1.RunWorkerAsync();
                    this.BtnGravar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
            }
        }


        string temp = "";
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int contador = 0;
                BackgroundWorker worker = (BackgroundWorker)sender;

                var ListasAgrupadas = listaObjetos.AsEnumerable().GroupBy(T => T["NumeroListaAtual"]);
                foreach (var itemDataTableNumeroLista in ListasAgrupadas)
                {
                    DataTable DataTableNumeroLista = itemDataTableNumeroLista.AsEnumerable().OrderBy(T => T["ItemAtual"]).OrderBy(T => T["QtdTotal"]).CopyToDataTable();

                    StringBuilder itensHTMListaLido = new StringBuilder();
                    int contadorPorLista = 0;
                    foreach (DataRow item in DataTableNumeroLista.Rows)
                    {
                        if (backgroundWorker1.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                        #region Linha item Atualização progresso
                        //FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();
                        string linhaItemCodigoObjeto = item["CodigoObjeto"].ToString();
                        string linhaItemDataLancamento = item["DataLancamento"].ToString();
                        string linhaItemDataModificacao = item["DataModificacao"].ToString();
                        string linhaItemSituacao = item["Situacao"].ToString();
                        string linhaItemComentario = item["Comentario"].ToString();
                        temp = string.Format("{0}-{1}-{2}-{3}", linhaItemCodigoObjeto, linhaItemDataLancamento, linhaItemDataModificacao, linhaItemSituacao, linhaItemComentario);
                        #endregion

                        #region Carrega Variaveis
                        string CodigoObjeto = item["CodigoObjeto"].ToString();
                        string CodigoLdi = item["CodigoLdi"].ToString();
                        string NomeCliente = item["NomeCliente"].ToString();
                        DateTime DataLancamento = Convert.ToDateTime(item["DataLancamento"]);
                        string DataModificacao = item["DataModificacao"].ToString();
                        string Situacao = item["Situacao"].ToString();
                        bool Atualizado = Convert.ToBoolean(item["Atualizado"].ToString() == "1" ? true : false);
                        bool ObjetoEntregue = Convert.ToBoolean(item["ObjetoEntregue"].ToString() == "1" ? true : false);
                        bool CaixaPostal = Convert.ToBoolean(item["CaixaPostal"].ToString() == "1" ? true : false);
                        string UnidadePostagem = item["UnidadePostagem"].ToString();
                        string MunicipioPostagem = item["MunicipioPostagem"].ToString();
                        string CriacaoPostagem = item["CriacaoPostagem"].ToString();
                        string CepDestinoPostagem = item["CepDestinoPostagem"].ToString();
                        string ARPostagem = item["ARPostagem"].ToString() == "1" ? "SIM" : "NÃO";
                        string MPPostagem = item["MPPostagem"].ToString() == "1" ? "SIM" : "NÃO";
                        string DataMaxPrevistaEntregaPostagem = item["DataMaxPrevistaEntregaPostagem"].ToString();
                        string UnidadeLOEC = item["UnidadeLOEC"].ToString();
                        string MunicipioLOEC = item["MunicipioLOEC"].ToString();
                        string CriacaoLOEC = item["CriacaoLOEC"].ToString();
                        string CarteiroLOEC = item["CarteiroLOEC"].ToString();
                        string DistritoLOEC = item["DistritoLOEC"].ToString();
                        string NumeroLOEC = item["NumeroLOEC"].ToString();
                        string EnderecoLOEC = item["EnderecoLOEC"].ToString();
                        string BairroLOEC = item["BairroLOEC"].ToString();
                        string LocalidadeLOEC = item["LocalidadeLOEC"].ToString();
                        string SituacaoDestinatarioAusente = item["SituacaoDestinatarioAusente"].ToString();
                        string AgrupadoDestinatarioAusente = item["AgrupadoDestinatarioAusente"].ToString() == "1" ? "SIM" : "NÃO";
                        string CoordenadasDestinatarioAusente = item["CoordenadasDestinatarioAusente"].ToString();
                        string Comentario = item["Comentario"].ToString();
                        string TipoPostalServico = item["TipoPostalServico"].ToString();
                        string TipoPostalSiglaCodigo = item["TipoPostalSiglaCodigo"].ToString();
                        string TipoPostalNomeSiglaCodigo = item["TipoPostalNomeSiglaCodigo"].ToString();
                        string TipoPostalPrazoDiasCorridosRegulamentado = item["TipoPostalPrazoDiasCorridosRegulamentado"].ToString();
                        string DataListaAtual = item["DataListaAtual"].ToString();
                        string NumeroListaAtual = item["NumeroListaAtual"].ToString();
                        string ItemAtual = item["ItemAtual"].ToString();
                        string QtdTotal = item["QtdTotal"].ToString();

                        string DataDevolucao = DataLancamento.ToDateTime().Date.AddDays(Convert.ToDouble(string.IsNullOrWhiteSpace(TipoPostalPrazoDiasCorridosRegulamentado) ? "0" : TipoPostalPrazoDiasCorridosRegulamentado)).ToDateTime().ToShortDateString();
                        string QtdTotaListaAtual = DataTableNumeroLista.Rows.Count.ToString();
                        #endregion

                        contador++;
                        int progresso = (contador * 100) / listaObjetos.Rows.Count;
                        worker.ReportProgress(progresso);

                        using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                        {
                            if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                            DataSet jaCadastrado = dao.RetornaDataSet(string.Format("SELECT DISTINCT CodigoObjeto, NomeCliente, CaixaPostal FROM TabelaObjetosSROLocal WHERE (CodigoObjeto = '{0}')", linhaItemCodigoObjeto));
                            #region Existe na base de dados
                            if (jaCadastrado.Tables[0].Rows.Count >= 1)//existe na base de dados
                            {
                                //existe na base de dados
                                dao.ExecutaSQL(string.Format("UPDATE TabelaObjetosSROLocal SET CodigoLdi = @CodigoLdi, NomeCliente = @NomeCliente, DataLancamento = @DataLancamento, DataModificacao = @DataModificacao, Situacao = @Situacao, Atualizado = @Atualizado, ObjetoEntregue = @ObjetoEntregue, CaixaPostal = @CaixaPostal, UnidadePostagem = @UnidadePostagem, MunicipioPostagem = @MunicipioPostagem, CriacaoPostagem = @CriacaoPostagem, CepDestinoPostagem = @CepDestinoPostagem, ARPostagem = @ARPostagem, MPPostagem = @MPPostagem, DataMaxPrevistaEntregaPostagem = @DataMaxPrevistaEntregaPostagem, UnidadeLOEC = @UnidadeLOEC, MunicipioLOEC = @MunicipioLOEC, CriacaoLOEC = @CriacaoLOEC, CarteiroLOEC = @CarteiroLOEC, DistritoLOEC = @DistritoLOEC, NumeroLOEC = @NumeroLOEC, EnderecoLOEC = @EnderecoLOEC, BairroLOEC = @BairroLOEC, LocalidadeLOEC = @LocalidadeLOEC, SituacaoDestinatarioAusente = @SituacaoDestinatarioAusente, AgrupadoDestinatarioAusente = @AgrupadoDestinatarioAusente, CoordenadasDestinatarioAusente = @CoordenadasDestinatarioAusente, Comentario = @Comentario, TipoPostalServico = @TipoPostalServico, TipoPostalSiglaCodigo = @TipoPostalSiglaCodigo, TipoPostalNomeSiglaCodigo = @TipoPostalNomeSiglaCodigo, TipoPostalPrazoDiasCorridosRegulamentado = @TipoPostalPrazoDiasCorridosRegulamentado " +
                                    "WHERE (CodigoObjeto = @CodigoObjeto)"), new List<Parametros>(){
                                    new Parametros("@CodigoLdi", TipoCampo.Text, CodigoLdi),
                                    new Parametros("@NomeCliente", TipoCampo.Text, NomeCliente),
                                    new Parametros("@DataLancamento", TipoCampo.DateTime, DataLancamento),
                                    new Parametros("@DataModificacao", TipoCampo.Text, DataModificacao),
                                    new Parametros("@Situacao", TipoCampo.Text, Situacao),
                                    new Parametros("@Atualizado", TipoCampo.Boolean, Atualizado),
                                    new Parametros("@ObjetoEntregue", TipoCampo.Boolean, ObjetoEntregue),
                                    new Parametros("@CaixaPostal", TipoCampo.Boolean, ObjetoEntregue),
                                    new Parametros("@UnidadePostagem", TipoCampo.Text, UnidadePostagem),
                                    new Parametros("@MunicipioPostagem", TipoCampo.Text, MunicipioPostagem),
                                    new Parametros("@CriacaoPostagem", TipoCampo.Text, CriacaoPostagem),
                                    new Parametros("@CepDestinoPostagem", TipoCampo.Text, CepDestinoPostagem),
                                    new Parametros("@ARPostagem", TipoCampo.Text, ARPostagem),
                                    new Parametros("@MPPostagem", TipoCampo.Text, MPPostagem),
                                    new Parametros("@DataMaxPrevistaEntregaPostagem", TipoCampo.Text, DataMaxPrevistaEntregaPostagem),
                                    new Parametros("@UnidadeLOEC", TipoCampo.Text, UnidadeLOEC),
                                    new Parametros("@MunicipioLOEC", TipoCampo.Text, MunicipioLOEC),
                                    new Parametros("@CriacaoLOEC", TipoCampo.Text, CriacaoLOEC),
                                    new Parametros("@CarteiroLOEC", TipoCampo.Text, CarteiroLOEC),
                                    new Parametros("@DistritoLOEC", TipoCampo.Text, DistritoLOEC),
                                    new Parametros("@NumeroLOEC", TipoCampo.Text, NumeroLOEC),
                                    new Parametros("@EnderecoLOEC", TipoCampo.Text, EnderecoLOEC),
                                    new Parametros("@BairroLOEC", TipoCampo.Text, BairroLOEC),
                                    new Parametros("@LocalidadeLOEC", TipoCampo.Text, LocalidadeLOEC),
                                    new Parametros("@SituacaoDestinatarioAusente", TipoCampo.Text, SituacaoDestinatarioAusente),
                                    new Parametros("@AgrupadoDestinatarioAusente", TipoCampo.Text, AgrupadoDestinatarioAusente),
                                    new Parametros("@CoordenadasDestinatarioAusente", TipoCampo.Text, CoordenadasDestinatarioAusente),
                                    new Parametros("@Comentario", TipoCampo.Text, Comentario),
                                    new Parametros("@TipoPostalServico", TipoCampo.Text, TipoPostalServico),
                                    new Parametros("@TipoPostalSiglaCodigo", TipoCampo.Text, TipoPostalSiglaCodigo),
                                    new Parametros("@TipoPostalNomeSiglaCodigo", TipoCampo.Text, TipoPostalNomeSiglaCodigo),
                                    new Parametros("@TipoPostalPrazoDiasCorridosRegulamentado", TipoCampo.Text, TipoPostalPrazoDiasCorridosRegulamentado),

                                    new Parametros("@CodigoObjeto", TipoCampo.Text, linhaItemCodigoObjeto)});
                            }
                            #endregion
                            #region Não existe na base de dados
                            else//não existe na base de dados
                            {
                                dao.ExecutaSQL("INSERT INTO TabelaObjetosSROLocal " +
                                    "(CodigoObjeto, CodigoLdi, NomeCliente, DataLancamento, DataModificacao, Situacao, Atualizado, ObjetoEntregue, CaixaPostal, UnidadePostagem, MunicipioPostagem, CriacaoPostagem, CepDestinoPostagem, ARPostagem, MPPostagem, DataMaxPrevistaEntregaPostagem, UnidadeLOEC, MunicipioLOEC, CriacaoLOEC, CarteiroLOEC, DistritoLOEC, NumeroLOEC, EnderecoLOEC, BairroLOEC, LocalidadeLOEC, SituacaoDestinatarioAusente, AgrupadoDestinatarioAusente, CoordenadasDestinatarioAusente, Comentario, TipoPostalServico, TipoPostalSiglaCodigo, TipoPostalNomeSiglaCodigo, TipoPostalPrazoDiasCorridosRegulamentado) VALUES " +
                                    "(@CodigoObjeto, @CodigoLdi, @NomeCliente, @DataLancamento, @DataModificacao, @Situacao, @Atualizado, @ObjetoEntregue, @CaixaPostal, @UnidadePostagem, @MunicipioPostagem, @CriacaoPostagem, @CepDestinoPostagem, @ARPostagem, @MPPostagem, @DataMaxPrevistaEntregaPostagem, @UnidadeLOEC, @MunicipioLOEC, @CriacaoLOEC, @CarteiroLOEC, @DistritoLOEC, @NumeroLOEC, @EnderecoLOEC, @BairroLOEC, @LocalidadeLOEC, @SituacaoDestinatarioAusente, @AgrupadoDestinatarioAusente, @CoordenadasDestinatarioAusente, @Comentario, @TipoPostalServico, @TipoPostalSiglaCodigo, @TipoPostalNomeSiglaCodigo, @TipoPostalPrazoDiasCorridosRegulamentado)",
                                new List<Parametros>() {
                                    new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjeto },
                                    new Parametros() { Nome = "@CodigoLdi", Tipo = TipoCampo.Text, Valor = CodigoLdi },
                                    new Parametros() { Nome = "@NomeCliente", Tipo = TipoCampo.Text, Valor = NomeCliente },
                                    new Parametros() { Nome = "@DataLancamento", Tipo = TipoCampo.DateTime, Valor = DataLancamento },
                                    new Parametros() { Nome = "@DataModificacao", Tipo = TipoCampo.Text, Valor = DataModificacao },
                                    new Parametros() { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = Situacao },
                                    new Parametros() { Nome = "@Atualizado", Tipo = TipoCampo.Boolean, Valor = Atualizado },
                                    new Parametros() { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Boolean, Valor = ObjetoEntregue },
                                    new Parametros() { Nome = "@CaixaPostal", Tipo = TipoCampo.Boolean, Valor = CaixaPostal },
                                    new Parametros() { Nome = "@UnidadePostagem", Tipo = TipoCampo.Text, Valor = UnidadePostagem },
                                    new Parametros() { Nome = "@MunicipioPostagem", Tipo = TipoCampo.Text, Valor = MunicipioPostagem },
                                    new Parametros() { Nome = "@CriacaoPostagem", Tipo = TipoCampo.Text, Valor = CriacaoPostagem },
                                    new Parametros() { Nome = "@CepDestinoPostagem", Tipo = TipoCampo.Text, Valor = CepDestinoPostagem },
                                    new Parametros() { Nome = "@ARPostagem", Tipo = TipoCampo.Text, Valor = ARPostagem },
                                    new Parametros() { Nome = "@MPPostagem", Tipo = TipoCampo.Text, Valor = MPPostagem },
                                    new Parametros() { Nome = "@DataMaxPrevistaEntregaPostagem", Tipo = TipoCampo.Text, Valor = DataMaxPrevistaEntregaPostagem },
                                    new Parametros() { Nome = "@UnidadeLOEC", Tipo = TipoCampo.Text, Valor = UnidadeLOEC },
                                    new Parametros() { Nome = "@MunicipioLOEC", Tipo = TipoCampo.Text, Valor = MunicipioLOEC },
                                    new Parametros() { Nome = "@CriacaoLOEC", Tipo = TipoCampo.Text, Valor = CriacaoLOEC },
                                    new Parametros() { Nome = "@CarteiroLOEC", Tipo = TipoCampo.Text, Valor = CarteiroLOEC },
                                    new Parametros() { Nome = "@DistritoLOEC", Tipo = TipoCampo.Text, Valor = DistritoLOEC },
                                    new Parametros() { Nome = "@NumeroLOEC", Tipo = TipoCampo.Text, Valor = NumeroLOEC },
                                    new Parametros() { Nome = "@EnderecoLOEC", Tipo = TipoCampo.Text, Valor = EnderecoLOEC },
                                    new Parametros() { Nome = "@BairroLOEC", Tipo = TipoCampo.Text, Valor = BairroLOEC },
                                    new Parametros() { Nome = "@LocalidadeLOEC", Tipo = TipoCampo.Text, Valor = LocalidadeLOEC },
                                    new Parametros() { Nome = "@SituacaoDestinatarioAusente", Tipo = TipoCampo.Text, Valor = SituacaoDestinatarioAusente },
                                    new Parametros() { Nome = "@AgrupadoDestinatarioAusente", Tipo = TipoCampo.Text, Valor = AgrupadoDestinatarioAusente },
                                    new Parametros() { Nome = "@CoordenadasDestinatarioAusente", Tipo = TipoCampo.Text, Valor = CoordenadasDestinatarioAusente },
                                    new Parametros() { Nome = "@Comentario", Tipo = TipoCampo.Text, Valor = Comentario },
                                    new Parametros() { Nome = "@TipoPostalServico", Tipo = TipoCampo.Text, Valor = TipoPostalServico },
                                    new Parametros() { Nome = "@TipoPostalSiglaCodigo", Tipo = TipoCampo.Text, Valor = TipoPostalSiglaCodigo },
                                    new Parametros() { Nome = "@TipoPostalNomeSiglaCodigo", Tipo = TipoCampo.Text, Valor = TipoPostalNomeSiglaCodigo },
                                    new Parametros() { Nome = "@TipoPostalPrazoDiasCorridosRegulamentado", Tipo = TipoCampo.Text, Valor = TipoPostalPrazoDiasCorridosRegulamentado }
                                });
                            }
                            #endregion

                            itensHTMListaLido = RetornaItensSegundaParteHTMListaLido(itensHTMListaLido, ItemAtual, QtdTotal, CodigoObjeto, NomeCliente, Comentario, TipoPostalPrazoDiasCorridosRegulamentado, DataDevolucao);

                            //envia email consolidado
                            contadorPorLista++;
                            if (contadorPorLista == DataTableNumeroLista.Rows.Count)
                            {
                                string PrimeiraParte = retornaPrimeiraParteHTMLListaLido();
                                string CabecalhoParte = RetornaCabecalhoSegundaParteHTMListaLido(NumeroListaAtual, DataListaAtual, QtdTotaListaAtual, QtdTotal);
                                string SegundaParte = retornaSegundaParteHTMLListaLido();
                                string SegundaParteCentral = itensHTMListaLido.ToString();
                                string TerceiraParteFinal = retornaTerceiraParteHTMLListaLido();

                                StringBuilder html = new StringBuilder();
                                html.AppendLine(PrimeiraParte);
                                html.AppendLine(CabecalhoParte);
                                html.AppendLine(SegundaParte);
                                html.AppendLine(SegundaParteCentral);
                                html.AppendLine(TerceiraParteFinal);
                                string htmlFinal = html.ToString();
                                EnviaEmailHTMLConsolidadoListaRecebimentoPLR(NumeroListaAtual, DateTime.Now.ToString(), html.ToString());
                            }
                        }
                    }
                }
                e.Result = listaObjetos.Rows.Count;
            }
            catch (Exception ex)
            {
                Mensagens.Erro("Ocorreu o seguinte erro: " + ex.Message);
            }
        }

        private string retornaSegundaParteHTMLListaLido()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine("		<thead>                               ");
            str.AppendLine("			<tr>                              ");
            str.AppendLine("				<th>Item/Qtd.</th>            ");
            str.AppendLine("				<th>Código Objeto</th>        ");
            str.AppendLine("				<th>Nome cliente</th>         ");
            str.AppendLine("				<th>Comentário</th>           ");
            str.AppendLine("				<th>Prazo</th>                ");
            str.AppendLine("				<th>Data devolução</th>       ");
            str.AppendLine("			</tr>                             ");
            str.AppendLine("		</thead>                              ");
            str.AppendLine("		<tbody>                               ");
            return str.ToString();
        }

        private string retornaPrimeiraParteHTMLListaLido()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("<!DOCTYPE html>                               ");
            str.AppendLine("<html>                                        ");
            str.AppendLine("<head>                                        ");
            str.AppendLine("	<title>HTML Table Generator</title>       ");
            str.AppendLine("	<style>                                   ");
            str.AppendLine("		table {                               ");
            str.AppendLine("			border:1px solid #b3adad;         ");
            str.AppendLine("			font-family:Arial, sans-serif;    ");
            str.AppendLine("			font-size:12px;                   ");
            str.AppendLine("			border-collapse:collapse;         ");
            str.AppendLine("			padding:5px;                      ");
            str.AppendLine("		}                                     ");
            str.AppendLine("		table th {                            ");
            str.AppendLine("			border:1px solid #b3adad;         ");
            str.AppendLine("			text-align:center;                ");
            str.AppendLine("			padding:5px;                      ");
            str.AppendLine("			background: #f0f0f0;              ");
            str.AppendLine("			color: #313030;                   ");
            str.AppendLine("		}                                     ");
            str.AppendLine("		table td {                            ");
            str.AppendLine("			border:1px solid #b3adad;         ");
            str.AppendLine("			text-align:left;                  ");
            str.AppendLine("			padding:5px;                      ");
            str.AppendLine("			background: #ffffff;              ");
            str.AppendLine("			color: #313030;                   ");
            str.AppendLine("		}                                     ");
            str.AppendLine("	</style>                                  ");
            str.AppendLine("</head>                                       ");
            str.AppendLine("<body>                                        ");
            str.AppendLine("	<table>                                   ");
            return str.ToString();
        }

        private string RetornaCabecalhoSegundaParteHTMListaLido(string NumeroListaAtual, string DataListaAtual, string QtdTotaListaAtual, string QtdTotal)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("		<thead>                               ");
            str.AppendLine("			<tr>                             ");
            str.AppendLine("				<th>Núm. lista</th>          ");
            str.AppendLine("				<th>Data emissão lista</th>  ");
            str.AppendLine("				<th>Data recebimento</th>    ");
            str.AppendLine("				<th>Qtd. Total de itens</th> ");
            str.AppendLine("				<th>Qtd. lidas/recebidas</th>");
            str.AppendLine("				<th>Qtd. faltantes</th>      ");
            str.AppendLine("			</tr>                            ");
            str.AppendLine("		</thead>                              ");
            str.AppendLine("			<tr>                             ");
            str.AppendLine("				<td>" + NumeroListaAtual + "</th>          ");
            str.AppendLine("				<td>" + DataListaAtual + "</th>  ");
            str.AppendLine("				<td>" + DateTime.Now.ToString() + "</th>    ");
            str.AppendLine("				<td>" + QtdTotal + "</th>");
            str.AppendLine("				<td>" + QtdTotaListaAtual + "</th> ");
            str.AppendLine("				<td>" + (QtdTotal.ToInt() - QtdTotaListaAtual.ToInt()) + "</th>      ");
            str.AppendLine("			</tr>                            ");
            return str.ToString();
        }

        private StringBuilder RetornaItensSegundaParteHTMListaLido(StringBuilder itensHTMListaLido, string ItemAtual, string QtdTotal, string CodigoObjeto, string NomeCliente, string Comentario, string TipoPostalPrazoDiasCorridosRegulamentado, string DataDevolucao)
        {
            itensHTMListaLido.AppendLine("			<tr>                  ");
            itensHTMListaLido.AppendLine("				<td>" + string.Format("{0:00000}/{1}", ItemAtual, QtdTotal) + "</td>   ");//Item/Qtd.
            itensHTMListaLido.AppendLine("				<td>" + CodigoObjeto + "</td>  ");//Código Objeto
            itensHTMListaLido.AppendLine("				<td>" + NomeCliente + "</td>  ");//Nome cliente
            itensHTMListaLido.AppendLine("				<td>" + Comentario + "</td>  ");//Comentário
            itensHTMListaLido.AppendLine("				<td>" + TipoPostalPrazoDiasCorridosRegulamentado + "</td>   ");//Prazo
            itensHTMListaLido.AppendLine("				<td>" + DataDevolucao + "</td>   ");//Data devolução
            itensHTMListaLido.AppendLine("			</tr>                 ");
            return itensHTMListaLido;
        }

        private string retornaTerceiraParteHTMLListaLido()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("		</tbody>                              ");
            str.AppendLine("	</table>                                  ");
            str.AppendLine("</body>                                       ");
            str.AppendLine("</html>                                       ");
            return str.ToString();
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.label2.Text = string.Format("{0}% completado...[{1}]", e.ProgressPercentage.ToString(), temp);
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //textBox1.Enabled = true;
            if (e.Cancelled)
            {
                Mensagens.Informa("Rotina Cancelada!!", MessageBoxIcon.Hand, MessageBoxButtons.OK);
            }
            else
            {
                //Mensagens.Informa("Atualização finalizada com sucesso.", MessageBoxIcon.Information, MessageBoxButtons.OK);   
                //FormularioPrincipal.RetornaComponentesFormularioPrincipal().AtualizaDataHoraUltimaAtualizacaoImportacao();
                this.label2.Text = "Total atualizados: " + e.Result.ToString();
                this.BtnGravar.Enabled = true;
                //this.textBox1.Enabled = true;

                if (dataGridViewQRCode.Rows.Count > 0)
                    listaObjetos.Clear();

            }

            if (Application.OpenForms["FormularioConsulta"] != null) //verifica se está aberto
                FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();

            //FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();

            //if (FormularioPrincipal.RetornaComponentesFormularioPrincipal().RetornaQuantidadeObjetoNaoAtualizado() > 0)
            //{
            //    if (FormularioPrincipal.AtualizandoNovosObjetos == false)
            //    {
            //        DialogResult pergunta = Mensagens.Pergunta("Uma busca de dados faz necessária.\n\nDeseja realizar a busca agora?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (pergunta == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            FormularioPrincipal.RetornaComponentesFormularioPrincipal().atualizarNovosObjetosToolStripMenuItem_Click(sender, e);
            //        }
            //    }
            //}
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormularioCadastroObjetos_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioConsulta")
                {
                    item.WindowState = FormWindowState.Maximized;
                    item.Activate();
                    return;
                }
            }
        }

        private void BtnAdicionarItem_Click(object sender, EventArgs e)
        {
            using (FormularioAdicionarItemObjeto formularioAdicionarItemObjeto = new FormularioAdicionarItemObjeto())
            {
                formularioAdicionarItemObjeto.ShowDialog();

                if (formularioAdicionarItemObjeto.ClicouCancelar) return;
                if (formularioAdicionarItemObjeto.ClicouConfirmar == false) return;

                if (formularioAdicionarItemObjeto.dtbLista.Rows.Count > 0)
                {
                    foreach (DataRow item in formularioAdicionarItemObjeto.dtbLista.Rows)
                    {
                        if (listaObjetos == null || listaObjetos.Rows.Count == 0)
                        {
                            listaObjetos = new DataTable();
                            listaObjetos.Columns.Add("CodigoObjeto", typeof(string));
                            listaObjetos.PrimaryKey = new DataColumn[] { listaObjetos.Columns["CodigoObjeto"] };

                            listaObjetos.Columns.Add("DataLancamento", typeof(DateTime));
                            listaObjetos.Columns.Add("DataModificacao", typeof(string));
                            listaObjetos.Columns.Add("Situacao", typeof(string));
                            listaObjetos.Columns.Add("Comentario", typeof(string));
                        }
                        bool existe = listaObjetos.AsEnumerable().Any(t => t["CodigoObjeto"].ToString() == item["CodigoObjeto"].ToString());
                        if (!existe)
                        {
                            listaObjetos.Rows.Add(item["CodigoObjeto"].ToString(), item["DataLancamento"], item["DataModificacao"].ToString(), item["Situacao"].ToString(), item["Comentario"].ToString());
                        }


                        listaObjetos.DefaultView.Sort = "DataLancamento DESC";
                        listaObjetos = listaObjetos.DefaultView.ToTable();
                    }

                    if (listaObjetos.Rows.Count == 0)
                    {
                        LblQuantidadeImportados.Text = "";
                        //tabControl1.Visible = false;
                        this.BtnGravar.Enabled = false;
                        label2.Text = "";
                        progressBar1.Visible = false;
                    }
                    if (listaObjetos.Rows.Count > 0)
                    {
                        LblQuantidadeImportados.Text = string.Format("Quantidade de objetos para importação: '{0}' objetos", listaObjetos.Rows.Count);
                        dataGridViewQRCode.DataSource = listaObjetos;
                        //tabControl1.Visible = true;
                        this.BtnGravar.Enabled = true;
                        label2.Text = "Barra de progresso";
                        progressBar1.Visible = true;
                        BtnGravar.Focus();
                    }


                }
            }
        }

        private void BtnLimparListaAtual_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = listaObjetos = new DataTable();
            if (dataGridViewQRCode.Rows.Count > 0)
                listaObjetos.Clear();

            LblQuantidadeImportados.Text = "";
            //tabControl1.Visible = false;
            this.BtnGravar.Enabled = false;
            label2.Text = "";
            progressBar1.Visible = false;
        }

        private void EnviarEmailConsolidadoRecebimento()
        {
            try
            {
                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                cliente.Port = Convert.ToInt32("587");
                cliente.Host = "smtp.gmail.com";
                cliente.EnableSsl = true;
                cliente.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                cliente.UseDefaultCredentials = false;
                cliente.Credentials = new System.Net.NetworkCredential("accluzimangues@gmail.com", "oxmt9212");

                System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();
                email.From = new System.Net.Mail.MailAddress("accluzimangues@gmail.com");
                email.To.Add("marques-fonseca@hotmail.com");
                email.To.Add("accluzimangues@gmail.com");
                email.Subject = "teste de email";
                email.IsBodyHtml = true;
                email.Body = RetornaCorpoEmailHTML();

                cliente.Send(email);



                Mensagens.Informa("Email enviado com sucesso!");

            }
            catch (Exception ex)
            {
                Mensagens.Erro("erro:" + ex.Message);
            }
        }

        private string RetornaCorpoEmailHTML()
        {
            StringBuilder Html = new StringBuilder();
            Html.AppendLine("<!DOCTYPE html>");
            Html.AppendLine("<html lang=\"pt-br\">");
            Html.AppendLine("<head>");
            Html.AppendLine("    <title>" + string.Format("SISAPO-SRO Lista de entrega [{0:dd/MM/yyyy HH:mm:ss}]", DateTime.Now) + "</title>");
            Html.AppendLine("    <meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\"/>");
            Html.AppendLine("    <script src=\"JsBarcode.all.min.js\"></script>");
            Html.AppendLine("    <script type=\"text/javascript\">");
            Html.AppendLine("        window.onload = function () {");
            Html.AppendLine("            JsBarcode(\".barcode\").init();");
            Html.AppendLine("        }");
            Html.AppendLine("    </script>");
            Html.AppendLine("    <style type=\"text/css\">");
            Html.AppendLine("        .quebrapagina {");
            Html.AppendLine("            page-break-before: always;");
            Html.AppendLine("            page-break-inside: avoid;");
            Html.AppendLine("        }");
            Html.AppendLine("        BODY {");
            Html.AppendLine("            margin-top: 0;");
            Html.AppendLine("            margin-left: 0;");
            Html.AppendLine("            margin-right: 0;");
            Html.AppendLine("            background-color: #FFFFFF;");
            Html.AppendLine("        }");
            Html.AppendLine("        .geral {");
            Html.AppendLine("            border: 0px solid rgb(252, 0, 0);");
            Html.AppendLine("            width: 850px;");
            Html.AppendLine("            margin: auto;");
            Html.AppendLine("            min-height: 1100px;");
            Html.AppendLine("        }");
            Html.AppendLine("");
            Html.AppendLine("        table.TabelaPLR {");
            Html.AppendLine("          font-family: \"Courier New\", Courier, monospace;");
            Html.AppendLine("          border: 0px dotted #000000;");
            Html.AppendLine("          background-color: #EEEEEE;");
            Html.AppendLine("          width: 350px;");
            Html.AppendLine("          height: 250px;");
            Html.AppendLine("          text-align: left;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.TabelaPLR td, table.TabelaPLR th {");
            Html.AppendLine("          border: 1px dotted #000000;");
            Html.AppendLine("          padding: 3px 2px;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.TabelaPLR tbody td {");
            Html.AppendLine("          font-size: 12px;");
            Html.AppendLine("          font-weight: bold;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.TabelaPLR tr:nth - child(even) {");
            Html.AppendLine("            background: #C1C1C1;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.TabelaPLR thead {");
            Html.AppendLine("          background: #ADADAD;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.TabelaPLR thead th {");
            Html.AppendLine("          font-size: 19px;");
            Html.AppendLine("          font-weight: bold;");
            Html.AppendLine("          color: #000000;");
            Html.AppendLine("          text-align: center;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.TabelaPLR tfoot td {");
            Html.AppendLine("          font-size: 21px;");
            Html.AppendLine("        }");
            Html.AppendLine("");
            Html.AppendLine("        table.Consolidado {");
            Html.AppendLine("          font-family: Arial, Helvetica, sans-serif;");
            Html.AppendLine("          border: 1px solid #000000;");
            Html.AppendLine("          background-color: #EEEEEE;");
            Html.AppendLine("          width: 100%;");
            Html.AppendLine("          text-align: left;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.Consolidado td, table.Consolidado th {");
            Html.AppendLine("          padding: 3px 2px;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.Consolidado tbody td {");
            Html.AppendLine("          font-size: 14px;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.Consolidado tr:nth-child(even) {");
            Html.AppendLine("          background: #C1C1C1;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.Consolidado thead {");
            Html.AppendLine("          background: #C4C4C4;");
            Html.AppendLine("          border-bottom: 2px solid #444444;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.Consolidado thead th {");
            Html.AppendLine("          font-size: 15px;");
            Html.AppendLine("          font-weight: bold;");
            Html.AppendLine("          color: #000000;");
            Html.AppendLine("          border-left: 2px solid #C4C4C4;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.Consolidado thead th:first-child {");
            Html.AppendLine("          border-left: none;");
            Html.AppendLine("        }");
            Html.AppendLine("    </style>");
            Html.AppendLine("</head>");
            Html.AppendLine("<body>");
            Html.AppendLine("    <div class=\"geral\">");


            #region Consolidado [Última Página]
            Html.AppendLine("		<div class=\"quebrapagina\"></div>");
            #region Cabeçalho Consolidado
            Html.AppendLine("       <table class=\"Consolidado\">");
            Html.AppendLine("       <thead>");
            Html.AppendLine("       <tr>");
            Html.AppendLine("       <th>PLR - Pr&eacute Lista de Remessa - Consolidado</th>");
            Html.AppendLine("       </tr>");
            Html.AppendLine("       </thead>");
            Html.AppendLine("       <tbody>");
            Html.AppendLine("       </tbody>");
            Html.AppendLine("       </table>");
            Html.AppendLine("");
            Html.AppendLine("       <table class=\"Consolidado\">");
            Html.AppendLine("       <thead>");
            Html.AppendLine("       <tr>");
            Html.AppendLine("       <th>N&uacute;mero da Lista</th>");
            Html.AppendLine("       <th>Data da emiss&atilde;o</th>");
            Html.AppendLine("       <th>Qtd. total</th>");
            Html.AppendLine("       </tr>");
            Html.AppendLine("       </thead>");
            Html.AppendLine("       <tbody>");
            Html.AppendLine("       <tr>");
            Html.AppendLine("       <td>111111</td>");
            Html.AppendLine("       <td>22222</td>");
            Html.AppendLine("       <td>22222</td>");
            Html.AppendLine("       </tr>");
            Html.AppendLine("       </tbody>");
            Html.AppendLine("       </table>");
            Html.AppendLine("");
            #endregion
            #endregion




            Html.AppendLine("    </div>");
            Html.AppendLine("</body>");
            Html.AppendLine("</html>");

            return Html.ToString();
        }

        private bool VerificaPadraoLeitura(string text)
        {
            bool retorno = true;
            try
            {
                //string QRCode = TxtObjetoAtual.Text.ToUpper();
                string QRCode = "";
                //string descompacta = ClassesDiversas.FormataString.Descompacta(QRCode);
                string[] CelulasCampos = QRCode.Split(new string[] { "[TAB]" }, StringSplitOptions.None);
                int QuantidadeCelulasCampos = CelulasCampos.Count();
                if (QuantidadeCelulasCampos < 37)
                    retorno = false;
            }
            catch (Exception ex)
            {
                retorno = false;
            }

            return retorno;
        }

        private void EnviaEmailHTMLConsolidadoListaRecebimentoPLR(string numeroListaAtual, string horaRecebimentoPLR, string Html)
        {
            try
            {
                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                cliente.Port = Convert.ToInt32("587");
                cliente.Host = "smtp.gmail.com";
                cliente.EnableSsl = true;
                cliente.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                cliente.UseDefaultCredentials = false;
                cliente.Credentials = new System.Net.NetworkCredential("accluzimangues@gmail.com", "oxmt9212");

                System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();
                email.From = new System.Net.Mail.MailAddress("accluzimangues@gmail.com");
                email.To.Add("marques.silva@correios.com.br");
                //email.To.Add("marques-fonseca@hotmail.com");
                email.To.Add("accluzimangues@gmail.com");
                email.Subject = "Resumo PLR [" + numeroListaAtual + "] recebida por Luzimangues às " + horaRecebimentoPLR + "";
                email.IsBodyHtml = true;
                email.Body = Html;

                cliente.Send(email);
            }
            catch (Exception ex)
            {

            }
        }

        private void BtnIniciarConferenciaPLR_Click(object sender, EventArgs e)
        {
            using (FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferencia formularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferencia = new FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferencia())
            {
                formularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferencia.ShowDialog();
            }
        }
    }
}
