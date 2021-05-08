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
    public partial class FormularioCadastroTiposPostais : Form
    {
        DataTable listaObjetos = new DataTable();
        StringBuilder textoColadoAreaTransferencia = new StringBuilder();

        public FormularioCadastroTiposPostais()
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

        private void FormularioCadastroTiposPostais_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            //label2.Text = "Barra de progresso";
            LblMensagem.Text = "";
        }

        private DataTable RetornaListaObjetos(string Texto)
        {
            string Servico = string.Empty;
            string Sigla = string.Empty;
            string Descricao = string.Empty;
            string PrazoDestinoCaidaPedida = string.Empty;
            string PrazoDestinoCaixaPostal = string.Empty;
            string PrazoRemetenteCaidaPedida = string.Empty;
            string PrazoRemetenteCaixaPostal = string.Empty;
            string TipoClassificacao = string.Empty;
            string DataAlteracao = string.Empty;

            DataTable dtbLista = new DataTable();
            dtbLista.Columns.Add("Servico", typeof(string));
            dtbLista.Columns.Add("Sigla", typeof(string));
            dtbLista.Columns.Add("Descricao", typeof(string));
            dtbLista.Columns.Add("PrazoDestinoCaidaPedida", typeof(string));
            dtbLista.Columns.Add("PrazoDestinoCaixaPostal", typeof(string));
            dtbLista.Columns.Add("PrazoRemetenteCaidaPedida", typeof(string));
            dtbLista.Columns.Add("PrazoRemetenteCaixaPostal", typeof(string));
            dtbLista.Columns.Add("TipoClassificacao", typeof(string));
            dtbLista.Columns.Add("DataAlteracao", typeof(string));

            try
            {
                string[] linha = Texto.Split('\n');

                for (int i = 0; i < linha.Length; i++)
                {
                    if (linha[i] == "" || linha[i] == "\r") continue;
                    string[] Parteslinha = linha[i].Split('\t');
                    Servico = Parteslinha.Length >= 1 ? Parteslinha[0].Trim().ToUpper() : "";
                    if (Servico == "<") continue;
                    Sigla = Parteslinha.Length >= 2 ? Parteslinha[1].Trim().ToUpper() : "";
                    Descricao = Parteslinha.Length >= 3 ? Parteslinha[2].Trim().ToUpper() : "";
                    Descricao = Descricao.RemoveAcento_DICIONARIO();
                    DataAlteracao = DateTime.Now.ToString();

                    if (Descricao.Contains("PAC") || Descricao.Contains("SEDEX") || Descricao.Contains("EXPRES") || Descricao.Contains("EXPRESS"))
                    {
                        PrazoDestinoCaidaPedida = "7";
                        PrazoDestinoCaixaPostal = "30";
                        PrazoRemetenteCaidaPedida = "7";
                        PrazoRemetenteCaixaPostal = "20";
                        if (Descricao.Contains("PAC"))
                        {
                            TipoClassificacao = "PAC";
                        }
                        if (Descricao.Contains("SEDEX") || Descricao.Contains("EXPRES") || Descricao.Contains("EXPRESS"))
                        {
                            TipoClassificacao = "SEDEX";
                        }
                    }
                    else
                    {
                        PrazoDestinoCaidaPedida = "20";
                        PrazoDestinoCaixaPostal = "30";
                        PrazoRemetenteCaidaPedida = "20";
                        PrazoRemetenteCaixaPostal = "20";
                        TipoClassificacao = "SEM CLASSIFICAÇÃO";
                    }

                    dtbLista.Rows.Add(
                        Servico,
                        Sigla,
                        Descricao,
                        PrazoDestinoCaidaPedida,
                        PrazoDestinoCaixaPostal,
                        PrazoRemetenteCaidaPedida,
                        PrazoRemetenteCaixaPostal,
                        TipoClassificacao,
                        DataAlteracao);
                }


                dtbLista.DefaultView.Sort = "Sigla ASC";
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
            //if (e.KeyCode == Keys.F12)
            //{
            //    FormularioPrincipal.RetornaComponentesFormularioPrincipal().visualizarListaDeObjetosToolStripMenuItem_Click(sender, e);
            //}
            //if (e.KeyCode == Keys.F9)
            //{
            //    FormularioPrincipal.RetornaComponentesFormularioPrincipal().sRORastreamentoUnificadoToolStripMenuItem_Click(sender, e);
            //}
        }

        private void BtnColarConteudoJaCopiado_Click(object sender, EventArgs e)
        {
            textoColadoAreaTransferencia = new StringBuilder();

            string curDir = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeArquivo = "itensAtualizacaoTiposPostais.txt";
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

                try
                {
                    progressBar1.Value = 0;
                    string[] linha = textoColadoAreaTransferencia.ToString().Split('\n');

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
            //string temp = "";
            int contador = 0;
            BackgroundWorker worker = (BackgroundWorker)sender;
            foreach (DataRow item in listaObjetos.Rows)
            {
                contador++;

                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                //FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();
                string Servico = item["Servico"].ToString();
                string Sigla = item["Sigla"].ToString();
                string Descricao = item["Descricao"].ToString();
                string PrazoDestinoCaidaPedida = item["PrazoDestinoCaidaPedida"].ToString();
                string PrazoDestinoCaixaPostal = item["PrazoDestinoCaixaPostal"].ToString();
                string PrazoRemetenteCaidaPedida = item["PrazoRemetenteCaidaPedida"].ToString();
                string PrazoRemetenteCaixaPostal = item["PrazoRemetenteCaixaPostal"].ToString();
                string TipoClassificacao = item["TipoClassificacao"].ToString();
                string DataAlteracao = item["DataAlteracao"].ToString();

                temp = string.Format("[{0}/{1}] ==> {2} - {3}", contador, listaObjetos.Rows.Count, Sigla, Descricao);

                int progresso = (contador * 100) / listaObjetos.Rows.Count;
                worker.ReportProgress(progresso);
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                    DataSet jaCadastrado = dao.RetornaDataSet(string.Format("SELECT DISTINCT Sigla FROM TiposPostais WHERE (Sigla = '{0}')", Sigla));

                    if (jaCadastrado.Tables[0].Rows.Count > 0)
                    {
                        //já existe --> pula
                        continue;
                    }
                    else
                    {
                        //Inserir novo registro.
                        dao.ExecutaSQL("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES (@Servico, @Sigla, @Descricao, @PrazoDestinoCaidaPedida, @PrazoDestinoCaixaPostal, @PrazoRemetenteCaidaPedida, @PrazoRemetenteCaixaPostal, @TipoClassificacao, @DataAlteracao)",
                            new List<Parametros>() {
                                    new Parametros() { Nome = "Servico", Tipo = TipoCampo.Text, Valor = Servico },
                                    new Parametros() { Nome = "Sigla", Tipo = TipoCampo.Text, Valor = Sigla },
                                    new Parametros() { Nome = "Descricao", Tipo = TipoCampo.Text, Valor = Descricao },
                                    new Parametros() { Nome = "PrazoDestinoCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoDestinoCaidaPedida },
                                    new Parametros() { Nome = "PrazoDestinoCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoDestinoCaixaPostal },
                                    new Parametros() { Nome = "PrazoRemetenteCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaidaPedida },
                                    new Parametros() { Nome = "PrazoRemetenteCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaixaPostal },
                                    new Parametros() { Nome = "TipoClassificacao", Tipo = TipoCampo.Text, Valor = TipoClassificacao },
                                    new Parametros() { Nome = "DataAlteracao", Tipo = TipoCampo.Text, Valor = DateTime.Now.ToString() }});
                    }

                }
            }
            e.Result = listaObjetos.Rows.Count;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.label2.Text = string.Format("{0}% completado...{1}", e.ProgressPercentage.ToString(), temp);
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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

        private void FormularioCadastroTiposPostais_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Close();
        }

    }
}
