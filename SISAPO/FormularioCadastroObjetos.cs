﻿using System;
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
            //tabControl1.Visible = false;
            this.BtnGravar.Enabled = false;
            this.backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(bw_DoWork);
            this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void FormularioCadastroObjetos_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            //label2.Text = "Barra de progresso";
            LblMensagem.Text = "";
            panel3.Visible = false;

            ConfiguraMenusBotoesParaACCAgenciaComunitaria(Configuracoes.ACCAgenciaComunitaria);
        }

        public void ConfiguraMenusBotoesParaACCAgenciaComunitaria(bool @ModoACCAgenciaComunitaria)
        {
            if (Configuracoes.ReceberObjetosViaQRCodePLRDaAgenciaMae)
            {
                BtnAdicionarPorPLRPreListaRemessa.Visible = true;
                dataGridView1.Visible = false;
                dataGridViewQRCode.Visible = true;
            }
            else
            {
                BtnAdicionarPorPLRPreListaRemessa.Visible = false;
                dataGridView1.Visible = true;
                dataGridViewQRCode.Visible = false;
            }


            LblMensagem.Visible = !@ModoACCAgenciaComunitaria;
            BtnColarConteudoJaCopiado.Visible = !@ModoACCAgenciaComunitaria;
            BtnAdicionarItem.Enabled = !@ModoACCAgenciaComunitaria;
        }

        public static FormularioCadastroObjetos RetornaComponentesFormularioCadastroObjetos()
        {
            FormularioCadastroObjetos formularioCadastroObjetos;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioCadastroObjetos")
                {
                    formularioCadastroObjetos = (FormularioCadastroObjetos)item;
                    return (FormularioCadastroObjetos)item;
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
                    //textBox1.Text = tempTXT;

                    LblMensagem.Text = "Certifique-se que o conteúdo na caixa de texto é o mesmo desejado!";


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
                        dataGridView1.DataSource = listaObjetos;
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
                LblMensagem.Text = "";
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
                string linhaItemComentario = item["Comentario"].ToString();
                temp = string.Format("{0}-{1}-{2}-{3}", linhaItemCodigoObjeto, linhaItemDataLancamento, linhaItemDataModificacao, linhaItemSituacao, linhaItemComentario);

                contador++;
                int progresso = (contador * 100) / listaObjetos.Rows.Count;
                worker.ReportProgress(progresso);

                string CodigoObjetoAtual = linhaItemCodigoObjeto;
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
                if (linhaItemSituacao == "CANCELAMENTO DE LANÇAMENTO INTERNO")
                {
                    //remove do banco
                    //dao.ExecutaSQL(string.Format("DELETE FROM TabelaObjetosSROLocal WHERE (CodigoObjeto = @CodigoObjeto)"), new List<Parametros>(){
                    //                    new Parametros("@CodigoObjeto", TipoCampo.Text, linhaItemCodigoObjeto)});
                    continue;
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

                        //precisa ter essa verificação porque o objeto atual pode ser caixa postal ou ao remente... portanto verificar para trazer...
                        TipoPostalPrazoDiasCorridosRegulamentado = Configuracoes.RetornaTipoPostalPrazoDiasCorridosRegulamentado(CodigoObjetoAtual, SeEAoRemetente, SeECaixaPostal, ref TipoPostalServico, ref TipoPostalSiglaCodigo, ref TipoPostalNomeSiglaCodigo);

                        //existe na base de dados
                        if (string.IsNullOrEmpty(linhaItemDataModificacao))//se não tem modificação... não alterar o campo situação e data modificação.
                        {
                            dao.ExecutaSQL(string.Format("UPDATE TabelaObjetosSROLocal SET Comentario = @Comentario, Atualizado = @Atualizado WHERE (CodigoObjeto = @CodigoObjeto)"), new List<Parametros>(){
                                            new Parametros("@Comentario", TipoCampo.Text, linhaItemComentario),
                                            new Parametros("@Atualizado",TipoCampo.Boolean, jaCadastrado.Tables[0].Rows[0]["NomeCliente"].ToString() == "" ? false : true),
                                            new Parametros("@CodigoObjeto", TipoCampo.Text, linhaItemCodigoObjeto)});
                        }
                        if (!string.IsNullOrEmpty(linhaItemDataModificacao))//se não tem modificação... não alterar o campo situação e data modificação.
                        {
                            dao.ExecutaSQL(string.Format("UPDATE TabelaObjetosSROLocal SET DataModificacao = @DataModificacao, Situacao = @Situacao, Comentario = @Comentario, Atualizado = @Atualizado, ObjetoEntregue = @ObjetoEntregue WHERE (CodigoObjeto = @CodigoObjeto)"), new List<Parametros>(){
                                            new Parametros("@DataModificacao", TipoCampo.Text, linhaItemDataModificacao),
                                            new Parametros("@Situacao", TipoCampo.Text, linhaItemSituacao),
                                            new Parametros("@Comentario", TipoCampo.Text, linhaItemComentario),
                                            new Parametros("@Atualizado",TipoCampo.Boolean, jaCadastrado.Tables[0].Rows[0]["NomeCliente"].ToString() == "" ? false : true),
                                            new Parametros("@ObjetoEntregue", TipoCampo.Boolean, linhaItemDataModificacao == "" ? false : true),

                                            new Parametros("@CodigoObjeto", TipoCampo.Text, linhaItemCodigoObjeto)});
                        }
                    }
                    else//não existe na base de dados
                    {
                        dao.ExecutaSQL("INSERT INTO TabelaObjetosSROLocal (CodigoObjeto, CodigoLdi, NomeCliente, DataLancamento, DataModificacao, Situacao, Comentario, Atualizado, ObjetoEntregue, CaixaPostal) VALUES (@CodigoObjeto, @CodigoLdi, @NomeCliente, @DataLancamento, @DataModificacao, @Situacao, @Comentario, @Atualizado, @ObjetoEntregue, @CaixaPostal)",
                        new List<Parametros>() {
                                    new Parametros() { Nome = "CodigoObjeto", Tipo = TipoCampo.Text, Valor = linhaItemCodigoObjeto },
                                    new Parametros() { Nome = "CodigoLdi", Tipo = TipoCampo.Text, Valor = "" },
                                    new Parametros() { Nome = "NomeCliente", Tipo = TipoCampo.Text, Valor = "" },
                                    new Parametros() { Nome = "DataLancamento", Tipo = TipoCampo.Text, Valor = string.IsNullOrEmpty(linhaItemDataLancamento) ? DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") : linhaItemDataLancamento},
                                    new Parametros() { Nome = "DataModificacao", Tipo = TipoCampo.Text, Valor = linhaItemDataModificacao },
                                    new Parametros() { Nome = "Situacao", Tipo = TipoCampo.Text, Valor = linhaItemSituacao },
                                    new Parametros() { Nome = "Comentario", Tipo = TipoCampo.Text, Valor = linhaItemComentario },
                                    new Parametros() { Nome = "Atualizado", Tipo = TipoCampo.Boolean, Valor = false },
                                    new Parametros() { Nome = "ObjetoEntregue", Tipo = TipoCampo.Boolean, Valor = (linhaItemDataModificacao == "" ? false : true) },
                                    new Parametros() { Nome = "CaixaPostal", Tipo = TipoCampo.Boolean, Valor = false },//considera todos com não caixa postal

                                    new Parametros() { Nome = "TipoPostalServico", Tipo = TipoCampo.Text, Valor = TipoPostalServico == "" ? (object)DBNull.Value : TipoPostalServico },
                                    new Parametros() { Nome = "TipoPostalSiglaCodigo", Tipo = TipoCampo.Text, Valor = TipoPostalSiglaCodigo == "" ? (object)DBNull.Value : TipoPostalSiglaCodigo },
                                    new Parametros() { Nome = "TipoPostalNomeSiglaCodigo", Tipo = TipoCampo.Text, Valor = TipoPostalNomeSiglaCodigo == "" ? (object)DBNull.Value : TipoPostalNomeSiglaCodigo },
                                    new Parametros() { Nome = "TipoPostalPrazoDiasCorridosRegulamentado", Tipo = TipoCampo.Text, Valor = TipoPostalPrazoDiasCorridosRegulamentado == "" ? "7" : TipoPostalPrazoDiasCorridosRegulamentado }});
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
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().AtualizaDataHoraUltimaAtualizacaoImportacao();
                this.label2.Text = "Total atualizados: " + e.Result.ToString();
                this.BtnGravar.Enabled = true;
                //this.textBox1.Enabled = true;
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
                        dataGridView1.DataSource = listaObjetos;
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
            if (dataGridView1.Rows.Count > 0)
                listaObjetos.Clear();

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
                                TipoPostalPrazoDiasCorridosRegulamentado
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
    }
}
