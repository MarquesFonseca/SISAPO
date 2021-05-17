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
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                    DataSet jaCadastrado = dao.RetornaDataSet(string.Format("SELECT DISTINCT CodigoObjeto, NomeCliente, CaixaPostal FROM TabelaObjetosSROLocal WHERE (CodigoObjeto = '{0}')", linhaItemCodigoObjeto));

                    if (jaCadastrado.Tables[0].Rows.Count >= 1)
                    {
                        string CodigoObjetoAtual = jaCadastrado.Tables[0].Rows[0]["CodigoObjeto"].ToString();
                        string NomeCliente = jaCadastrado.Tables[0].Rows[0]["NomeCliente"].ToString().ToUpper().RemoveAcentos();
                        bool SeECaixaPostal = Convert.ToBoolean(jaCadastrado.Tables[0].Rows[0]["CaixaPostal"]);
                        bool SeEAoRemetente = (
                            NomeCliente.ToUpper().RemoveAcentos().Contains("ORIGEM") || 
                            NomeCliente.ToUpper().RemoveAcentos().Contains("DEVOLUCAO") ||
                            NomeCliente.ToUpper().RemoveAcentos().Contains("DEVOLUCA") ||
                            NomeCliente.ToUpper().RemoveAcentos().Contains("DEVOLUC") ||
                            NomeCliente.ToUpper().RemoveAcentos().Contains("DEVOLU") ||
                            NomeCliente.ToUpper().RemoveAcentos().Contains("DEVOL") ||
                            NomeCliente.ToUpper().RemoveAcentos().Contains("DEVOLUCAO") ||
                            NomeCliente.ToUpper().RemoveAcentos().Contains("REMETENTE") ||
                            NomeCliente.ToUpper().RemoveAcentos().Contains("REMETENT") ||
                            NomeCliente.ToUpper().RemoveAcentos().Contains("REMETEN") ||
                            NomeCliente.ToUpper().RemoveAcentos().Contains("REMETE") ||
                            NomeCliente.ToUpper().RemoveAcentos().Contains("REMET") ||
                            NomeCliente.ToUpper().RemoveAcentos().Contains("REME") ||
                            NomeCliente.ToUpper().RemoveAcentos().Contains("REMETENTE")) ? true : false;
                        string TipoPostalServico = string.Empty;
                        string TipoPostalSiglaCodigo = string.Empty;
                        string TipoPostalNomeSiglaCodigo = string.Empty;
                        string TipoPostalPrazoDiasCorridosRegulamentado = "7";

                        if (FormularioPrincipal.TiposPostais.Rows.Count > 0)
                        {
                            DataRow drTipoPostal = FormularioPrincipal.TiposPostais.AsEnumerable().First(T => T["Sigla"].Equals(CodigoObjetoAtual.Substring(0, 2))); //["Código"] - Pega linha retornada dos tipos postais vinda do Excel

                            //Exemplo "LB327263658SE"
                            //[0] - Serviço: NAO URGENTE 
                            //[1] - Código: LB 
                            //[2] - Nome: OBJETO INTERNACIONAL PRIME 
                            //[3] - Prazo dias corridos no destino (Caixa Postal): 30 
                            //[4] - Prazo dias corridos no destino (Caída/Pedida): 20 
                            //[5] - Prazo dias corridos na origem/devolução/remetente (Caixa Postal): 20 
                            //[6] - Prazo dias corridos na origem/devolução/remetente (Caída/Pedida): 20

                            TipoPostalServico = drTipoPostal["Servico"].ToString();
                            TipoPostalSiglaCodigo = drTipoPostal["Sigla"].ToString();
                            TipoPostalNomeSiglaCodigo = drTipoPostal["Descricao"].ToString();
                            TipoPostalPrazoDiasCorridosRegulamentado = "7";

                            //Se for Caixa Postal e Não for Ao remetente
                            if (SeECaixaPostal && !SeEAoRemetente)
                            {
                                // Pega campo "Prazo dias corridos no destino (Caixa Postal)"
                                TipoPostalPrazoDiasCorridosRegulamentado = drTipoPostal["PrazoDestinoCaixaPostal"].ToString();
                            }
                            //Se for Caixa Postal e Se for Ao remetente
                            if (SeECaixaPostal && SeEAoRemetente)
                            {
                                // Pega campo "Prazo dias corridos na origem/devolução/remetente (Caixa Postal)"
                                TipoPostalPrazoDiasCorridosRegulamentado = drTipoPostal["PrazoRemetenteCaixaPostal"].ToString();
                            }
                            //Se Não for Caixa Postal && Não for Ao remetente
                            if (!SeECaixaPostal && !SeEAoRemetente)
                            {
                                // Pega campo "Prazo dias corridos no destino (Caída/Pedida)"
                                TipoPostalPrazoDiasCorridosRegulamentado = drTipoPostal["PrazoDestinoCaidaPedida"].ToString();
                            }
                            //Se Não for Caixa Postal && Se for Ao remetente
                            if (!SeECaixaPostal && SeEAoRemetente)
                            {
                                // Pega campo "Prazo dias corridos na origem/devolução/remetente (Caída/Pedida)"
                                TipoPostalPrazoDiasCorridosRegulamentado = drTipoPostal["PrazoRemetenteCaidaPedida"].ToString();
                            }
                        }

                        //existe na base de dados
                        dao.ExecutaSQL(string.Format("UPDATE TabelaObjetosSROLocal SET DataLancamento = @DataLancamento, DataModificacao = @DataModificacao, Situacao = @Situacao, Atualizado = @Atualizado, ObjetoEntregue = @ObjetoEntregue, TipoPostalServico = @TipoPostalServico, TipoPostalSiglaCodigo = @TipoPostalSiglaCodigo, TipoPostalNomeSiglaCodigo = @TipoPostalNomeSiglaCodigo, TipoPostalPrazoDiasCorridosRegulamentado = @TipoPostalPrazoDiasCorridosRegulamentado WHERE (CodigoObjeto = @CodigoObjeto)"), new List<Parametros>(){
                                            new Parametros("@DataLancamento", TipoCampo.Text, linhaItemDataLancamento),
                                            new Parametros("@DataModificacao", TipoCampo.Text, linhaItemDataModificacao),
                                            new Parametros("@Situacao", TipoCampo.Text, linhaItemSituacao),
                                            new Parametros("@Atualizado",TipoCampo.Int, jaCadastrado.Tables[0].Rows[0]["NomeCliente"].ToString() == "" ? 0 : 1),
                                            new Parametros("@ObjetoEntregue", TipoCampo.Int, linhaItemDataModificacao == "" ? 0 : 1),

                                            new Parametros("@TipoPostalServico", TipoCampo.Text, TipoPostalServico),
                                            new Parametros("@TipoPostalSiglaCodigo", TipoCampo.Text, TipoPostalSiglaCodigo),
                                            new Parametros("@TipoPostalNomeSiglaCodigo", TipoCampo.Text, TipoPostalNomeSiglaCodigo),
                                            new Parametros("@TipoPostalPrazoDiasCorridosRegulamentado", TipoCampo.Text, TipoPostalPrazoDiasCorridosRegulamentado),

                                            new Parametros("@CodigoObjeto", TipoCampo.Text, linhaItemCodigoObjeto)});
                    }
                    else
                    {
                        if (jaCadastrado.Tables[0].Rows.Count == 0)//não existe na base de dados
                        {
                            string TipoPostalServico = string.Empty;
                            string TipoPostalSiglaCodigo = string.Empty;
                            string TipoPostalNomeSiglaCodigo = string.Empty;
                            string TipoPostalPrazoDiasCorridosRegulamentado = "7";

                            if (FormularioPrincipal.TiposPostais.Rows.Count > 0)
                            {
                                DataRow drTipoPostal = FormularioPrincipal.TiposPostais.AsEnumerable().First(T => T["Sigla"].Equals(linhaItemCodigoObjeto.Substring(0, 2))); //["Código"] - Pega linha retornada dos tipos postais vinda do Excel

                                TipoPostalServico = drTipoPostal["Servico"].ToString();
                                TipoPostalSiglaCodigo = drTipoPostal["Sigla"].ToString();
                                TipoPostalNomeSiglaCodigo = drTipoPostal["Descricao"].ToString();

                                //Obedece a opçao de nao ser Caixa Postal && Não ser Ao remetente
                                // Pega campo "Prazo dias corridos no destino (Caída/Pedida)"
                                TipoPostalPrazoDiasCorridosRegulamentado = drTipoPostal["PrazoDestinoCaidaPedida"].ToString();
                            }

                            dao.ExecutaSQL("INSERT INTO TabelaObjetosSROLocal (CodigoObjeto, CodigoLdi, NomeCliente, DataLancamento, DataModificacao, Situacao, Atualizado, ObjetoEntregue, CaixaPostal) VALUES (@CodigoObjeto, @CodigoLdi, @NomeCliente, @DataLancamento, @DataModificacao, @Situacao, @Atualizado, @ObjetoEntregue, @CaixaPostal)",
                            new List<Parametros>() {
                                    new Parametros() { Nome = "CodigoObjeto", Tipo = TipoCampo.Text, Valor = linhaItemCodigoObjeto },
                                    new Parametros() { Nome = "CodigoLdi", Tipo = TipoCampo.Text, Valor = "" },
                                    new Parametros() { Nome = "NomeCliente", Tipo = TipoCampo.Text, Valor = "" },
                                    new Parametros() { Nome = "DataLancamento", Tipo = TipoCampo.Text, Valor = linhaItemDataLancamento },
                                    new Parametros() { Nome = "DataModificacao", Tipo = TipoCampo.Text, Valor = linhaItemDataModificacao },
                                    new Parametros() { Nome = "Situacao", Tipo = TipoCampo.Text, Valor = linhaItemSituacao },
                                    new Parametros() { Nome = "Atualizado", Tipo = TipoCampo.Int, Valor = 0 },
                                    new Parametros() { Nome = "ObjetoEntregue", Tipo = TipoCampo.Text, Valor = (linhaItemDataModificacao == "" ? 0 : 1) },
                                    new Parametros() { Nome = "CaixaPostal", Tipo = TipoCampo.Text, Valor = 0 },//considera todos com não caixa postal

                                    new Parametros() { Nome = "TipoPostalServico", Tipo = TipoCampo.Text, Valor = TipoPostalServico },
                                    new Parametros() { Nome = "TipoPostalSiglaCodigo", Tipo = TipoCampo.Text, Valor = TipoPostalSiglaCodigo },
                                    new Parametros() { Nome = "TipoPostalNomeSiglaCodigo", Tipo = TipoCampo.Text, Valor = TipoPostalNomeSiglaCodigo },
                                    new Parametros() { Nome = "TipoPostalPrazoDiasCorridosRegulamentado", Tipo = TipoCampo.Text, Valor = TipoPostalPrazoDiasCorridosRegulamentado }});
                        }
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
                textBox1.Enabled = true;
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

    }
}
