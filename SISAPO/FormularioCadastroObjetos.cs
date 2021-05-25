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
    public partial class FormularioCadastroObjetos : Form
    {
        DataTable listaObjetos = new DataTable();
        StringBuilder textoColadoAreaTransferencia = new StringBuilder();

        public FormularioCadastroObjetos()
        {
            InitializeComponent();
            tabControl1.Visible = false;
            this.backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(bw_DoWork);
            this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            //label2.Text = "Barra de progresso";
            LblMensagem.Text = "";
        }

        private DataTable RetornaListaObjetos(string Texto)
        {
            DataTable dtbLista = new DataTable();
            dtbLista.Columns.Add("CodigoObjeto", typeof(string));
            dtbLista.Columns.Add("DataLancamento", typeof(DateTime));
            dtbLista.Columns.Add("DataModificacao", typeof(string));
            dtbLista.Columns.Add("Situacao", typeof(string));
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

                    if (ParteLinhaCodigoObjeto != "")
                    {
                        dtbLista.Rows.Add(
                            ParteLinhaCodigoObjeto,
                            Convert.ToDateTime(ParteLinhaDataLancamento),
                            ParteLinhaDataModificacao,
                            ParteLinhaSituacao);
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

        private void FormularioCadastroObjetos_KeyDown(object sender, KeyEventArgs e)
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
                    textBox1.Text = tempTXT;

                    LblMensagem.Text = "Certifique-se que o conteúdo na caixa de texto é o mesmo desejado!";


                    listaObjetos = RetornaListaObjetos(textoColadoAreaTransferencia.ToString());

                    if (listaObjetos.Rows.Count == 0)
                    {
                        LblQuantidadeImportados.Text = "";
                        tabControl1.Visible = false;
                        this.BtnGravar.Enabled = false;
                        label2.Text = "";
                        progressBar1.Visible = false;
                    }
                    if (listaObjetos.Rows.Count > 0)
                    {
                        LblQuantidadeImportados.Text = string.Format("Listados para importação: '{0}' objetos", listaObjetos.Rows.Count);
                        dataGridView1.DataSource = listaObjetos;
                        tabControl1.Visible = true;
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
                        textBox1.Text = arquivo.ToString();
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
                LblMensagem.Text = "";
                //listaObjetos = RetornaListaObjetos(textoColadoAreaTransferencia.ToString());
                //dataGridView1.DataSource = listaObjetos;
                if (listaObjetos.Rows.Count == 0)
                {
                    Mensagens.Informa("Não foi possível gravar. O campo está vazio."); return;
                }
                textBox1.Enabled = false;
                //int contador = 0;
                progressBar1.Value = 0;
                label2.Text = "Barra de progresso";

                if (!this.backgroundWorker1.IsBusy)
                {
                    this.backgroundWorker1.RunWorkerAsync();
                    this.BtnGravar.Enabled = false;
                    this.BtnAdicionarItem.Enabled = false;
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
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();
                string linhaItemCodigoObjeto = item["CodigoObjeto"].ToString();
                string linhaItemDataLancamento = item["DataLancamento"].ToString();
                string linhaItemDataModificacao = item["DataModificacao"].ToString();
                string linhaItemSituacao = item["Situacao"].ToString();
                temp = string.Format("{0}-{1}-{2}-{3}", linhaItemCodigoObjeto, linhaItemDataLancamento, linhaItemDataModificacao, linhaItemSituacao);

                contador++;
                int progresso = (contador * 100) / listaObjetos.Rows.Count;
                worker.ReportProgress(progresso);

                string CodigoObjetoAtual = string.Empty;
                string NomeCliente = string.Empty;
                bool SeECaixaPostal = false;
                bool SeEAoRemetente = false;

                string TipoPostalServico = string.Empty;
                string TipoPostalSiglaCodigo = string.Empty;
                string TipoPostalNomeSiglaCodigo = string.Empty;
                string TipoPostalPrazoDiasCorridosRegulamentado = string.Empty;

                TipoPostalPrazoDiasCorridosRegulamentado = Configuracoes.RetornaTipoPostalPrazoDiasCorridosRegulamentado(CodigoObjetoAtual, SeEAoRemetente, SeECaixaPostal, ref TipoPostalServico, ref TipoPostalSiglaCodigo, ref TipoPostalNomeSiglaCodigo);
                if (string.IsNullOrEmpty(TipoPostalPrazoDiasCorridosRegulamentado))
                {
                    Mensagens.Erro(string.Format("Não foi encontrado o Tipo Postal [ {0} ].\nUma gestão de tipos postais é necessário.", linhaItemCodigoObjeto.Substring(0, 2)));
                    //continua mesmo não tendo o tipo postal desejado....
                }

                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                    DataSet jaCadastrado = dao.RetornaDataSet(string.Format("SELECT DISTINCT CodigoObjeto, NomeCliente, CaixaPostal FROM TabelaObjetosSROLocal WHERE (CodigoObjeto = '{0}')", linhaItemCodigoObjeto));

                    if (jaCadastrado.Tables[0].Rows.Count >= 1)//existe na base de dados
                    {
                        CodigoObjetoAtual = jaCadastrado.Tables[0].Rows[0]["CodigoObjeto"].ToString();
                        NomeCliente = jaCadastrado.Tables[0].Rows[0]["NomeCliente"].ToString().ToUpper().RemoveAcentos();
                        SeECaixaPostal = Convert.ToBoolean(jaCadastrado.Tables[0].Rows[0]["CaixaPostal"]);
                        SeECaixaPostal = !SeECaixaPostal ? Configuracoes.RetornaSeECaixaPostal(NomeCliente) : SeECaixaPostal;
                        SeEAoRemetente = Configuracoes.RetornaSeEAoRemetente(NomeCliente);

                        TipoPostalPrazoDiasCorridosRegulamentado = Configuracoes.RetornaTipoPostalPrazoDiasCorridosRegulamentado(CodigoObjetoAtual, SeEAoRemetente, SeECaixaPostal, ref TipoPostalServico, ref TipoPostalSiglaCodigo, ref TipoPostalNomeSiglaCodigo);
                        TipoPostalPrazoDiasCorridosRegulamentado = TipoPostalPrazoDiasCorridosRegulamentado == "" ? "7" : TipoPostalPrazoDiasCorridosRegulamentado;

                        //existe na base de dados
                        dao.ExecutaSQL(string.Format("UPDATE TabelaObjetosSROLocal SET DataLancamento = @DataLancamento, DataModificacao = @DataModificacao, Situacao = @Situacao, Atualizado = @Atualizado, ObjetoEntregue = @ObjetoEntregue, TipoPostalServico = @TipoPostalServico, TipoPostalSiglaCodigo = @TipoPostalSiglaCodigo, TipoPostalNomeSiglaCodigo = @TipoPostalNomeSiglaCodigo, TipoPostalPrazoDiasCorridosRegulamentado = @TipoPostalPrazoDiasCorridosRegulamentado WHERE (CodigoObjeto = @CodigoObjeto)"), new List<Parametros>(){
                                            new Parametros("@DataLancamento", TipoCampo.Text, linhaItemDataLancamento),
                                            new Parametros("@DataModificacao", TipoCampo.Text, linhaItemDataModificacao),
                                            new Parametros("@Situacao", TipoCampo.Text, linhaItemSituacao),
                                            new Parametros("@Atualizado",TipoCampo.Boolean, jaCadastrado.Tables[0].Rows[0]["NomeCliente"].ToString() == "" ? false : true),
                                            new Parametros("@ObjetoEntregue", TipoCampo.Boolean, linhaItemDataModificacao == "" ? false : true),

                                            new Parametros("@TipoPostalServico", TipoCampo.Text, TipoPostalServico == "" ? (object)DBNull.Value : TipoPostalServico),
                                            new Parametros("@TipoPostalSiglaCodigo", TipoCampo.Text, TipoPostalSiglaCodigo == "" ? (object)DBNull.Value : TipoPostalSiglaCodigo),
                                            new Parametros("@TipoPostalNomeSiglaCodigo", TipoCampo.Text, TipoPostalNomeSiglaCodigo == "" ? (object)DBNull.Value : TipoPostalNomeSiglaCodigo),
                                            new Parametros("@TipoPostalPrazoDiasCorridosRegulamentado", TipoCampo.Text, TipoPostalPrazoDiasCorridosRegulamentado == "" ? (object)DBNull.Value : TipoPostalPrazoDiasCorridosRegulamentado),

                                            new Parametros("@CodigoObjeto", TipoCampo.Text, linhaItemCodigoObjeto)});
                    }
                    else//não existe na base de dados
                    {
                        dao.ExecutaSQL("INSERT INTO TabelaObjetosSROLocal (CodigoObjeto, CodigoLdi, NomeCliente, DataLancamento, DataModificacao, Situacao, Atualizado, ObjetoEntregue, CaixaPostal) VALUES (@CodigoObjeto, @CodigoLdi, @NomeCliente, @DataLancamento, @DataModificacao, @Situacao, @Atualizado, @ObjetoEntregue, @CaixaPostal)",
                        new List<Parametros>() {
                                    new Parametros() { Nome = "CodigoObjeto", Tipo = TipoCampo.Text, Valor = linhaItemCodigoObjeto },
                                    new Parametros() { Nome = "CodigoLdi", Tipo = TipoCampo.Text, Valor = "" },
                                    new Parametros() { Nome = "NomeCliente", Tipo = TipoCampo.Text, Valor = "" },
                                    new Parametros() { Nome = "DataLancamento", Tipo = TipoCampo.Text, Valor = linhaItemDataLancamento },
                                    new Parametros() { Nome = "DataModificacao", Tipo = TipoCampo.Text, Valor = linhaItemDataModificacao },
                                    new Parametros() { Nome = "Situacao", Tipo = TipoCampo.Text, Valor = linhaItemSituacao },
                                    new Parametros() { Nome = "Atualizado", Tipo = TipoCampo.Boolean, Valor = false },
                                    new Parametros() { Nome = "ObjetoEntregue", Tipo = TipoCampo.Boolean, Valor = (linhaItemDataModificacao == "" ? false : true) },
                                    new Parametros() { Nome = "CaixaPostal", Tipo = TipoCampo.Boolean, Valor = false },//considera todos com não caixa postal

                                    new Parametros() { Nome = "TipoPostalServico", Tipo = TipoCampo.Text, Valor = TipoPostalServico == "" ? (object)DBNull.Value : TipoPostalServico },
                                    new Parametros() { Nome = "TipoPostalSiglaCodigo", Tipo = TipoCampo.Text, Valor = TipoPostalSiglaCodigo == "" ? (object)DBNull.Value : TipoPostalSiglaCodigo },
                                    new Parametros() { Nome = "TipoPostalNomeSiglaCodigo", Tipo = TipoCampo.Text, Valor = TipoPostalNomeSiglaCodigo == "" ? (object)DBNull.Value : TipoPostalNomeSiglaCodigo },
                                    new Parametros() { Nome = "TipoPostalPrazoDiasCorridosRegulamentado", Tipo = TipoCampo.Text, Valor = "7" }});
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
            textBox1.Enabled = true;
            if (e.Cancelled)
            {
                Mensagens.Informa("Rotina Cancelada!!", MessageBoxIcon.Hand, MessageBoxButtons.OK);
            }
            else
            {
                //Mensagens.Informa("Atualização finalizada com sucesso.", MessageBoxIcon.Information, MessageBoxButtons.OK);   
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().AtualizaDataHoraUltimaAtualizacaoImportacao();
                this.label2.Text = "Total atualizados: " + e.Result.ToString();
                this.BtnGravar.Enabled = true;
                this.textBox1.Enabled = true;
                this.BtnAdicionarItem.Enabled = true;
            }

            if (Application.OpenForms["FormularioConsulta"] != null) //verifica se está aberto
                FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();

            FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();

            if (FormularioPrincipal.RetornaComponentesFormularioPrincipal().RetornaQuantidadeObjetoNaoAtualizado() > 0)
            {
                if (FormularioPrincipal.AtualizandoNovosObjetos == false)
                {
                    DialogResult pergunta = Mensagens.Pergunta("Uma busca de dados faz necessária.\n\nDeseja realizar a busca agora?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (pergunta == System.Windows.Forms.DialogResult.Yes)
                    {
                        FormularioPrincipal.RetornaComponentesFormularioPrincipal().atualizarNovosObjetosToolStripMenuItem_Click(sender, e);
                    }
                }
            }
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

                if (formularioAdicionarItemObjeto.dtbLista.Rows.Count > 0)
                {
                    foreach (DataRow item in formularioAdicionarItemObjeto.dtbLista.Rows)
                    {
                        if (listaObjetos == null || listaObjetos.Rows.Count == 0)
                        {
                            listaObjetos = new DataTable();
                            listaObjetos.Columns.Add("CodigoObjeto", typeof(string));
                            listaObjetos.Columns.Add("DataLancamento", typeof(DateTime));
                            listaObjetos.Columns.Add("DataModificacao", typeof(string));
                            listaObjetos.Columns.Add("Situacao", typeof(string));
                        }
                        bool existe = listaObjetos.AsEnumerable().Any(t => t["CodigoObjeto"].ToString() == item["CodigoObjeto"].ToString());
                        if (!existe)
                            listaObjetos.Rows.Add(item["CodigoObjeto"].ToString(), item["DataLancamento"], item["DataModificacao"].ToString(), item["Situacao"].ToString());

                        listaObjetos.DefaultView.Sort = "DataLancamento DESC";
                        listaObjetos = listaObjetos.DefaultView.ToTable();
                    }

                    if (listaObjetos.Rows.Count == 0)
                    {
                        LblQuantidadeImportados.Text = "";
                        tabControl1.Visible = false;
                        this.BtnGravar.Enabled = false;
                        label2.Text = "";
                        progressBar1.Visible = false;
                    }
                    if (listaObjetos.Rows.Count > 0)
                    {
                        LblQuantidadeImportados.Text = string.Format("Listados para importação: '{0}' objetos", listaObjetos.Rows.Count);
                        dataGridView1.DataSource = listaObjetos;
                        tabControl1.Visible = true;
                        this.BtnGravar.Enabled = true;
                        label2.Text = "Barra de progresso";
                        progressBar1.Visible = true;
                        BtnGravar.Focus();
                    }


                }
            }
        }
    }
}
