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
    public partial class FormularioCadastroObjetosViaQRCodePLRDaAgenciaMae : Form
    {
        DataTable listaObjetos = new DataTable();
        StringBuilder textoColadoAreaTransferencia = new StringBuilder();

        public FormularioCadastroObjetosViaQRCodePLRDaAgenciaMae()
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

        private void FormularioCadastroObjetosViaQRCodePLRDaAgenciaMae_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            //label2.Text = "Barra de progresso";
            panel3.Visible = false;
        }

        public static FormularioCadastroObjetosViaQRCodePLRDaAgenciaMae RetornaComponentesFormularioCadastroObjetosViaQRCodePLRDaAgenciaMae()
        {
            FormularioCadastroObjetosViaQRCodePLRDaAgenciaMae formularioCadastroObjetosViaQRCodePLRDaAgenciaMae;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioCadastroObjetosViaQRCodePLRDaAgenciaMae")
                {
                    formularioCadastroObjetosViaQRCodePLRDaAgenciaMae = (FormularioCadastroObjetosViaQRCodePLRDaAgenciaMae)item;
                    return (FormularioCadastroObjetosViaQRCodePLRDaAgenciaMae)item;
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

        private void FormularioCadastroObjetosViaQRCodePLRDaAgenciaMae_KeyDown(object sender, KeyEventArgs e)
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
                //textBox1.Enabled = false;
                //int contador = 0;
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
            int contador = 0;
            BackgroundWorker worker = (BackgroundWorker)sender;
            foreach (DataRow item in listaObjetos.Rows)
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
                DateTime DataLancamento = Convert.ToDateTime(item["DataLancamento"].ToString());
                string DataModificacao = item["DataModificacao"].ToString();
                string Situacao = item["Situacao"].ToString();
                bool Atualizado = Convert.ToBoolean(item["Atualizado"].ToString());
                bool ObjetoEntregue = Convert.ToBoolean(item["ObjetoEntregue"].ToString());
                bool CaixaPostal = Convert.ToBoolean(item["CaixaPostal"].ToString());
                string UnidadePostagem = item["UnidadePostagem"].ToString();
                string MunicipioPostagem = item["MunicipioPostagem"].ToString();
                string CriacaoPostagem = item["CriacaoPostagem"].ToString();
                string CepDestinoPostagem = item["CepDestinoPostagem"].ToString();
                string ARPostagem = item["ARPostagem"].ToString();
                string MPPostagem = item["MPPostagem"].ToString();
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
                string AgrupadoDestinatarioAusente = item["AgrupadoDestinatarioAusente"].ToString();
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
                #endregion
                contador++;
                int progresso = (contador * 100) / listaObjetos.Rows.Count;
                worker.ReportProgress(progresso);

                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                    DataSet jaCadastrado = dao.RetornaDataSet(string.Format("SELECT DISTINCT CodigoObjeto, NomeCliente, CaixaPostal FROM TabelaObjetosSROLocal WHERE (CodigoObjeto = '{0}')", linhaItemCodigoObjeto));
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
                }
            }
            e.Result = listaObjetos.Rows.Count;
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

        private void BtnVerObjetosNaoAtualizados_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            #region atualiza grid
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                dataGridView2.DataSource = dao.RetornaDataTable("SELECT CodigoObjeto from TabelaObjetosSROLocal WHERE Atualizado = FALSE");
            }
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            return;
        }

        private void BtnAdicionarPorPLRPreListaRemessa_Click(object sender, EventArgs e)
        {
            //LV221247464CN#tab287077020401#tabJULIANA RODRIGUES#tab29/09/2021 10:40:11#tab29/09/2021 14:44:46#tabENTREGUE#tabTrue#tabTrue#tabFalse#tab00156-000 / CHINA#tab#tab22/08/2021 14:26:00#tab77019-096#tab#tab#tab#tab77100-970 / CDD PALMAS#tabPALMAS / TO#tab28/09/2021 11:52:52#tab83454624#tab502#tab112100021778#tabQUADRA ARSO 112 ALAMEDA 13 17#tabPLANO DIRETOR SUL#tab77019096#tabAUSENTE ENCAMINHADO ENTREGA INTERNA#tabNÃO#tab-10.25075,-48.34432#tabPCT INT#tabNAO URGENTE#tabLV#tabOBJETO INTERNACIONAL PRIME#tab20#tab

            //BtnLimparListaAtual_Click(sender, e);

            using (FormularioAdicionarItemObjetoQRCode formularioAdicionarItemObjetoQRCode = new FormularioAdicionarItemObjetoQRCode())
            {
                formularioAdicionarItemObjetoQRCode.ShowDialog();

                if (formularioAdicionarItemObjetoQRCode.ClicouCancelar) return;
                if (formularioAdicionarItemObjetoQRCode.ClicouConfirmar == false) return;

                if (formularioAdicionarItemObjetoQRCode.dtbLista.Rows.Count > 0)
                {
                    foreach (DataRow item in formularioAdicionarItemObjetoQRCode.dtbLista.Rows)
                    {
                        if (listaObjetos == null || listaObjetos.Rows.Count == 0)
                        {
                            listaObjetos = new DataTable();
                            listaObjetos.Columns.Add("CodigoObjeto", typeof(string));
                            listaObjetos.PrimaryKey = new DataColumn[] { listaObjetos.Columns["CodigoObjeto"] };
                            listaObjetos.Columns.Add("CodigoLdi", typeof(string));
                            listaObjetos.Columns.Add("NomeCliente", typeof(string));
                            listaObjetos.Columns.Add("DataLancamento", typeof(DateTime));
                            listaObjetos.Columns.Add("DataModificacao", typeof(string));
                            listaObjetos.Columns.Add("Situacao", typeof(string));
                            listaObjetos.Columns.Add("Atualizado", typeof(bool));
                            listaObjetos.Columns.Add("ObjetoEntregue", typeof(bool));
                            listaObjetos.Columns.Add("CaixaPostal", typeof(bool));
                            listaObjetos.Columns.Add("UnidadePostagem", typeof(string));
                            listaObjetos.Columns.Add("MunicipioPostagem", typeof(string));
                            listaObjetos.Columns.Add("CriacaoPostagem", typeof(string));
                            listaObjetos.Columns.Add("CepDestinoPostagem", typeof(string));
                            listaObjetos.Columns.Add("ARPostagem", typeof(string));
                            listaObjetos.Columns.Add("MPPostagem", typeof(string));
                            listaObjetos.Columns.Add("DataMaxPrevistaEntregaPostagem", typeof(string));
                            listaObjetos.Columns.Add("UnidadeLOEC", typeof(string));
                            listaObjetos.Columns.Add("MunicipioLOEC", typeof(string));
                            listaObjetos.Columns.Add("CriacaoLOEC", typeof(string));
                            listaObjetos.Columns.Add("CarteiroLOEC", typeof(string));
                            listaObjetos.Columns.Add("DistritoLOEC", typeof(string));
                            listaObjetos.Columns.Add("NumeroLOEC", typeof(string));
                            listaObjetos.Columns.Add("EnderecoLOEC", typeof(string));
                            listaObjetos.Columns.Add("BairroLOEC", typeof(string));
                            listaObjetos.Columns.Add("LocalidadeLOEC", typeof(string));
                            listaObjetos.Columns.Add("SituacaoDestinatarioAusente", typeof(string));
                            listaObjetos.Columns.Add("AgrupadoDestinatarioAusente", typeof(string));
                            listaObjetos.Columns.Add("CoordenadasDestinatarioAusente", typeof(string));
                            listaObjetos.Columns.Add("Comentario", typeof(string));
                            listaObjetos.Columns.Add("TipoPostalServico", typeof(string));
                            listaObjetos.Columns.Add("TipoPostalSiglaCodigo", typeof(string));
                            listaObjetos.Columns.Add("TipoPostalNomeSiglaCodigo", typeof(string));
                            listaObjetos.Columns.Add("TipoPostalPrazoDiasCorridosRegulamentado", typeof(string));
                            listaObjetos.Columns.Add("DataListaAtual", typeof(string));
                            listaObjetos.Columns.Add("NumeroListaAtual", typeof(string));
                            listaObjetos.Columns.Add("ItemAtual", typeof(string));
                            listaObjetos.Columns.Add("QtdTotal", typeof(string));
                        }

                        string CodigoObjeto = item["CodigoObjeto"].ToString();
                        string CodigoLdi = item["CodigoLdi"].ToString();
                        string NomeCliente = item["NomeCliente"].ToString();
                        DateTime DataLancamento = Convert.ToDateTime(item["DataLancamento"].ToString());
                        string DataModificacao = item["DataModificacao"].ToString();
                        string Situacao = item["Situacao"].ToString();
                        bool Atualizado = Convert.ToBoolean(item["Atualizado"].ToString());
                        bool ObjetoEntregue = Convert.ToBoolean(item["ObjetoEntregue"].ToString());
                        bool CaixaPostal = Convert.ToBoolean(item["CaixaPostal"].ToString());
                        string UnidadePostagem = item["UnidadePostagem"].ToString();
                        string MunicipioPostagem = item["MunicipioPostagem"].ToString();
                        string CriacaoPostagem = item["CriacaoPostagem"].ToString();
                        string CepDestinoPostagem = item["CepDestinoPostagem"].ToString();
                        string ARPostagem = item["ARPostagem"].ToString();
                        string MPPostagem = item["MPPostagem"].ToString();
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
                        string AgrupadoDestinatarioAusente = item["AgrupadoDestinatarioAusente"].ToString();
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

                        bool existe = listaObjetos.AsEnumerable().Any(t => t["CodigoObjeto"].ToString() == item["CodigoObjeto"].ToString());
                        if (!existe)
                        {
                            listaObjetos.Rows.Add(
                                CodigoObjeto,
                                CodigoLdi,
                                NomeCliente,
                                DataLancamento,
                                DataModificacao,
                                Situacao,
                                Atualizado,
                                ObjetoEntregue,
                                CaixaPostal,
                                UnidadePostagem,
                                MunicipioPostagem,
                                CriacaoPostagem,
                                CepDestinoPostagem,
                                ARPostagem,
                                MPPostagem,
                                DataMaxPrevistaEntregaPostagem,
                                UnidadeLOEC,
                                MunicipioLOEC,
                                CriacaoLOEC,
                                CarteiroLOEC,
                                DistritoLOEC,
                                NumeroLOEC,
                                EnderecoLOEC,
                                BairroLOEC,
                                LocalidadeLOEC,
                                SituacaoDestinatarioAusente,
                                AgrupadoDestinatarioAusente,
                                CoordenadasDestinatarioAusente,
                                Comentario,
                                TipoPostalServico,
                                TipoPostalSiglaCodigo,
                                TipoPostalNomeSiglaCodigo,
                                TipoPostalPrazoDiasCorridosRegulamentado,
                                DataListaAtual,
                                NumeroListaAtual,
                                ItemAtual,
                                QtdTotal
                                );
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

        private void button2_Click(object sender, EventArgs e)
        {
            EnviarEmailConsolidadoRecebimento();
        }

        private void EnviarEmailConsolidadoRecebimento()
        {
            Email email = new Email();
            email.Assunto = "teste assunto";
            email.Destinatario = new List<string>() { "marques-fonseca@hotmail.com" };
            email.IsBodyHtml = true;
            email.Remetente = "accluzimangues@gmail.com";
            email.Senha = "oxmt9212";
            email.ServidorEnvio = "smtp.gmail.com";
            email.Texto = "teste";
            email.AnexoStream = new List<AnexoStream>();
            


            //string remetenteEmail = "accluzimangues@gmail.com"; //O e-mail do remetente
            //System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            //mail.To.Add("marques-fonseca@hotmail.com");//destinatário
            //mail.From = new System.Net.Mail.MailAddress(remetenteEmail, "ACC Luzimangues", System.Text.Encoding.UTF8);
            //mail.Subject = "Assunto:Este e-mail é um teste do Asp.Net";
            //mail.SubjectEncoding = System.Text.Encoding.UTF8;
            //mail.Body = "teste";
            //mail.BodyEncoding = System.Text.Encoding.UTF8;
            //mail.IsBodyHtml = true;
            //mail.Priority = System.Net.Mail.MailPriority.High; //Prioridade do E-Mail

            //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 465);  //Adicionando as credenciais do seu e-mail e senha:
            //client.Credentials = new System.Net.NetworkCredential(remetenteEmail, "oxmt9212");
            ////client.Port = 587; // Esta porta é a utilizada pelo Gmail para envio
            ////client.Host = "smtp.gmail.com"; //Definindo o provedor que irá disparar o e-mail
            //client.EnableSsl = true; //Gmail trabalha com Server Secured Layer
            //client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network; // modo de envio
            //client.UseDefaultCredentials = false; // vamos utilizar credencias especificas

            try
            {
                email.EnviaEmail();
                //client.Send(mail);
                Mensagens.Informa("Envio do E-mail com sucesso");
            }
            catch (Exception ex)
            {
                Mensagens.Erro("Ocorreu um erro ao enviar:" + ex.Message);
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
    }
}
