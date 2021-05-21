using SISAPO;
using SISAPO.ClassesDiversas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SISAPO
{
    public partial class FormularioAlteracaoTiposPostaisMassa : Form
    {
        WaitWndFun waitForm = new WaitWndFun();
        private Dictionary<string, string> selecionados;
        string NomeComboBoxTipoClassificacao = string.Empty;
        string ClassificacaoInicial = string.Empty;

        //public FormularioAlteracaoTiposPostaisMassa()
        //{
        //    InitializeComponent();
        //}

        public FormularioAlteracaoTiposPostaisMassa(Dictionary<string, string> _selecionados, string _classificacao)
        {
            InitializeComponent();
            this.selecionados = _selecionados;
            this.ClassificacaoInicial = _classificacao;
            this.backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(bw_DoWork);
            this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void FormularioAlteracaoTiposPostaisMassa_Load(object sender, EventArgs e)
        {
            label5.Text = string.Format("Esta ação afetará o prazo de todos os '{0}' tipos postais selecionados. Se deseja esta alteração, após redefinir, clique em Gravar.", selecionados.Count);
            comboBoxTipoClassificacao.SelectedItem = ClassificacaoInicial;
            DataInicial_dateTimePicker.Text = DateTime.Now.Date.ToShortDateString();
            DataInicial_dateTimePicker.Enabled = false;

            progressBar1.Value = 0;
            LblProgresso1.Text = "";
            LblProgresso2.Text = "";
        }

        private void BtnGravar_Click(object sender, EventArgs e)
        {


            ////grava aqui no banco..
            //AtualizaTodosSelecionados();

            //this.Close();






            try
            {
                if (comboBoxTipoClassificacao.SelectedIndex == -1)
                {
                    Mensagens.Erro("Selecione uma classificação para os selecionados.");
                    return;
                }
                progressBar1.Value = 0;
                LblProgresso1.Text = "";
                LblProgresso2.Text = "";

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

        string TempLblProgresso1 = "";
        string TempLblProgresso2 = "";
        int Porcentagem1 = 0;
        int Porcentagem2 = 0;
        int QtdTotal1 = 0;
        int QtdTotal2 = 0;
        int QtdTotalGeral = 0;
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            TempLblProgresso1 = "";
            Porcentagem1 = 0;
            QtdTotal1 = selecionados.Count;

            TempLblProgresso2 = "";
            Porcentagem2 = 0;
            QtdTotal2 = 0;

            QtdTotalGeral = 0;

            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                int contador1 = 0;
                foreach (var item in selecionados)
                {
                    contador1++;

                    if (backgroundWorker1.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    TempLblProgresso1 = string.Format("[{0}/{1}] ==> {2}", contador1, QtdTotal1, item.Value.ToString());
                    Porcentagem1 = (contador1 * 100) / QtdTotal1;
                    worker.ReportProgress(Porcentagem1);

                    TempLblProgresso2 = "";
                    Porcentagem2 = 0;
                    //worker.ReportProgress(Porcentagem1);


                    #region Grava TiposPostais
                    List<Parametros> pr = new List<Parametros>() {
                            new Parametros() { Nome = "@PrazoDestinoCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoDestinatarioCaidaPedidaUpDown.Value.ToString() },
                            new Parametros() { Nome = "@PrazoDestinoCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoDestinatarioCaixaPostalUpDown.Value.ToString() },
                            new Parametros() { Nome = "@PrazoRemetenteCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaidaPedidaUpDown.Value.ToString() },
                            new Parametros() { Nome = "@PrazoRemetenteCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaixaPostalUpDown.Value.ToString() },
                            new Parametros() { Nome = "@TipoClassificacao", Tipo = TipoCampo.Text, Valor = NomeComboBoxTipoClassificacao },
                            new Parametros() { Nome = "@DataAlteracao", Tipo = TipoCampo.Text, Valor = DateTime.Now.ToString() },
                            new Parametros() { Nome = "@Codigo", Tipo = TipoCampo.Int, Valor = item.Key.ToInt() },
                            new Parametros() { Nome = "@Sigla", Tipo = TipoCampo.Text, Valor = item.Value.ToString() }
                        };
                    dao.ExecutaSQL("UPDATE TiposPostais SET PrazoDestinoCaidaPedida = @PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal = @PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida = @PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal = @PrazoRemetenteCaixaPostal, TipoClassificacao = @TipoClassificacao, DataAlteracao = @DataAlteracao  WHERE (Codigo = @Codigo AND Sigla = @Sigla)", pr);
                    #endregion

                    List<Parametros> prSigla = new List<Parametros>() {
                            new Parametros() { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Int, Valor = false },
                            new Parametros() { Nome = "@TipoPostalSiglaCodigo", Tipo = TipoCampo.Text, Valor = item.Value.ToString() }
                        };
                    DataSet DsObjetosNaoEntreguesMesmoTipoSigla = dao.RetornaDataSet("SELECT Codigo, CodigoObjeto, NomeCliente, ObjetoEntregue, CaixaPostal, Comentario, TipoPostalPrazoDiasCorridosRegulamentado FROM TabelaObjetosSROLocal WHERE ObjetoEntregue = @ObjetoEntregue AND Left(CodigoObjeto, 2) = @TipoPostalSiglaCodigo", prSigla);
                    QtdTotal2 = DsObjetosNaoEntreguesMesmoTipoSigla.Tables[0].Rows.Count;
                    if (QtdTotal2 == 0)
                    {
                        TempLblProgresso2 = "";
                        Porcentagem2 = 0;
                        worker.ReportProgress(Porcentagem1);
                    }
                    if (DsObjetosNaoEntreguesMesmoTipoSigla.Tables[0].Rows.Count > 0)
                    {

                        QtdTotalGeral = QtdTotalGeral + QtdTotal2;
                        int contador2 = 0;

                        foreach (DataRow itemObjetoNaoEntregue in DsObjetosNaoEntreguesMesmoTipoSigla.Tables[0].Rows)
                        {
                            contador2++;

                            if (backgroundWorker1.CancellationPending)
                            {
                                e.Cancel = true;
                                break;
                            }

                            TempLblProgresso2 = string.Format("[{0}/{1}] ==> {2} - {3}", contador2, QtdTotal2, item.Value.ToString(), itemObjetoNaoEntregue["CodigoObjeto"].ToString());
                            Porcentagem2 = (contador2 * 100) / QtdTotal2;
                            worker.ReportProgress(Porcentagem1);

                            //verificar se é caixa postal 
                            //verificar se é remetente
                            bool AoRemetente = false;
                            bool ClienteCaixaPostal = false;

                            string CodigoObjetoAtual = itemObjetoNaoEntregue["CodigoObjeto"].ToString();
                            string NomeClienteAtual = itemObjetoNaoEntregue["NomeCliente"].ToString();
                            string ComentarioAtual = itemObjetoNaoEntregue["Comentario"].ToString();

                            AoRemetente = Configuracoes.RetornaSeEAoRemetente(NomeClienteAtual);
                            AoRemetente = Configuracoes.RetornaSeEAoRemetente(ComentarioAtual);

                            ClienteCaixaPostal = Convert.ToBoolean(itemObjetoNaoEntregue["CaixaPostal"]);
                            #region ao remetente
                            if (AoRemetente) // ao remetente
                            {
                                if (ClienteCaixaPostal)
                                {
                                    List<Parametros> pr1 = new List<Parametros>() {
                                        new Parametros() { Nome = "@PrazoRemetenteCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaixaPostalUpDown.Value.ToString() },
                                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual.ToString() },
                                        new Parametros() { Nome = "@DataInicial_dateTimePicker", Tipo = TipoCampo.Text, Valor = DataInicial_dateTimePicker.Text.ToString() }
                                    };
                                    if (radioButtonAPartirDoDia.Checked)
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoRemetenteCaixaPostal WHERE (CodigoObjeto = @CodigoObjeto) AND format(DataLancamento, 'dd/MM/yyyy') >= @DataInicial_dateTimePicker", pr1);
                                    else
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoRemetenteCaixaPostal WHERE (CodigoObjeto = @CodigoObjeto)", pr1);
                                }
                                else // não é caixa postal
                                {
                                    List<Parametros> pr2 = new List<Parametros>() {
                                        new Parametros() { Nome = "@PrazoRemetenteCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaidaPedidaUpDown.Value.ToString() },
                                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual.ToString() },
                                        new Parametros() { Nome = "@DataInicial_dateTimePicker", Tipo = TipoCampo.Text, Valor = DataInicial_dateTimePicker.Text.ToString() }
                                    };
                                    if (radioButtonAPartirDoDia.Checked)
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoRemetenteCaidaPedida WHERE (CodigoObjeto = @CodigoObjeto) AND format(DataLancamento, 'dd/MM/yyyy') >= @DataInicial_dateTimePicker", pr2);
                                    else
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoRemetenteCaidaPedida WHERE (CodigoObjeto = @CodigoObjeto)", pr2);
                                }
                            }
                            #endregion
                            #region ao destinatário
                            else // ao destinatário
                            {
                                if (ClienteCaixaPostal)
                                {
                                    List<Parametros> pr3 = new List<Parametros>() {
                                        new Parametros() { Nome = "@PrazoDestinoCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoDestinatarioCaixaPostalUpDown.Value.ToString() },
                                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual.ToString() },
                                        new Parametros() { Nome = "@DataInicial_dateTimePicker", Tipo = TipoCampo.Text, Valor = DataInicial_dateTimePicker.Text.ToString() }
                                    };
                                    if (radioButtonAPartirDoDia.Checked)
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoDestinoCaixaPostal WHERE (CodigoObjeto = @CodigoObjeto) AND format(DataLancamento, 'dd/MM/yyyy') >= @DataInicial_dateTimePicker", pr3);
                                    else
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoDestinoCaixaPostal WHERE (CodigoObjeto = @CodigoObjeto)", pr3);
                                }
                                else // nao é caixa postal
                                {
                                    List<Parametros> pr4 = new List<Parametros>() {
                                        new Parametros() { Nome = "@PrazoDestinoCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoDestinatarioCaidaPedidaUpDown.Value.ToString() },
                                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual.ToString() },
                                        new Parametros() { Nome = "@DataInicial_dateTimePicker", Tipo = TipoCampo.Text, Valor = DataInicial_dateTimePicker.Value.Date.ToShortDateString() }
                                    };
                                    if (radioButtonAPartirDoDia.Checked)
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoDestinoCaidaPedida WHERE (CodigoObjeto = @CodigoObjeto) AND format(DataLancamento, 'dd/MM/yyyy') >= @DataInicial_dateTimePicker", pr4);
                                    else
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoDestinoCaidaPedida WHERE (CodigoObjeto = @CodigoObjeto)", pr4);
                                }
                            }
                            #endregion]

                            TempLblProgresso2 = string.Format("[{0}/{1}] ==> {2} - {3}", contador2, QtdTotal2, item.Value.ToString(), itemObjetoNaoEntregue["CodigoObjeto"].ToString());
                            Porcentagem2 = (contador2 * 100) / QtdTotal2;
                            worker.ReportProgress(Porcentagem1);
                        }
                    }
                }
            }
            e.Result = selecionados.Count;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            #region Porcentagem1
            if (Porcentagem1 == 0)
            {
                this.LblProgresso1.Text = "";
                //this.progressBar1.Value = e.ProgressPercentage;
                this.progressBar1.Value = Porcentagem1;
            }
            if (Porcentagem1 > 0)
            {
                this.LblProgresso1.Text = string.Format("{0}% completado...{1}", Porcentagem1.ToString(), TempLblProgresso1);
                //this.progressBar1.Value = e.ProgressPercentage;
                this.progressBar1.Value = Porcentagem1;
            }
            #endregion

            #region Porcentagem2
            if (Porcentagem2 == 0)
            {
                this.LblProgresso2.Text = "";
                //this.progressBar2.Value = e.ProgressPercentage;
                this.progressBar2.Value = Porcentagem2;
            }
            if (Porcentagem2 > 0)
            {
                this.LblProgresso2.Text = string.Format("{0}% completado...{1}", Porcentagem2.ToString(), TempLblProgresso2);
                //this.progressBar2.Value = e.ProgressPercentage;
                this.progressBar2.Value = Porcentagem2;
            }
            #endregion
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Mensagens.Informa("Rotina Cancelada!!", MessageBoxIcon.Hand, MessageBoxButtons.OK);
            }
            else
            {

                if (Porcentagem1 == 100)
                {
                    this.progressBar2.Value = this.progressBar1.Value;
                }

                //Mensagens.Informa("Atualização finalizada com sucesso.", MessageBoxIcon.Information, MessageBoxButtons.OK);   
                this.BtnGravar.Enabled = true;

                if (QtdTotal1 == 1)
                {
                    this.LblProgresso1.Text = "Qtd. Tipo Postal atualizado: " + QtdTotal1.ToString();
                }
                if (QtdTotal1 > 1)
                {
                    this.LblProgresso1.Text = "Qtd. Tipos Postais atualizados: " + QtdTotal1.ToString();
                }

                if (QtdTotalGeral == 1)
                {
                    this.LblProgresso2.Text = "Qtd. Objeto atualizado: " + QtdTotalGeral.ToString();
                }
                if (QtdTotalGeral > 1)
                {
                    this.LblProgresso2.Text = "Qtd. Objetos atualizados: " + QtdTotalGeral.ToString();
                }

                TempLblProgresso1 = "";
                TempLblProgresso2 = "";
                Porcentagem1 = 0;
                Porcentagem2 = 0;
                QtdTotal1 = 0;
                QtdTotal2 = 0;
                QtdTotalGeral = 0;

                Mensagens.Informa("Atualização realizada com sucesso!");
                this.Close();
                //FormularioPrincipal.RetornaComponentesFormularioPrincipal().AtualizaDataHoraUltimaAtualizacaoImportacao();
            }

        }

        private void FormularioAlteracaoTiposPostaisMassa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                BtnCancelar_Click(sender, e);
            }
            if (e.KeyData == Keys.F5)
            {
                this.BtnGravar_Click(sender, e);
            }
        }

        private void radioButtonAPartirDoDia_CheckedChanged(object sender, EventArgs e)
        {
            DataInicial_dateTimePicker.Enabled = radioButtonAPartirDoDia.Checked;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            this.Close();
        }

        private void comboBoxTipoClassificacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTipoClassificacao.SelectedIndex == -1) return;
            NomeComboBoxTipoClassificacao = comboBoxTipoClassificacao.SelectedItem.ToString();
        }

        private void PrazoDestinatarioCaidaPedidaUpDown_MouseClick(object sender, MouseEventArgs e)
        {
            PrazoDestinatarioCaidaPedidaUpDown.Select(0, PrazoDestinatarioCaidaPedidaUpDown.Text.Length);
            PrazoDestinatarioCaidaPedidaUpDown.Focus();
        }

        private void PrazoDestinatarioCaixaPostalUpDown_MouseClick(object sender, MouseEventArgs e)
        {
            PrazoDestinatarioCaixaPostalUpDown.Select(0, PrazoDestinatarioCaixaPostalUpDown.Text.Length);
            PrazoDestinatarioCaixaPostalUpDown.Focus();
        }

        private void PrazoRemetenteCaidaPedidaUpDown_MouseClick(object sender, MouseEventArgs e)
        {
            PrazoRemetenteCaidaPedidaUpDown.Select(0, PrazoRemetenteCaidaPedidaUpDown.Text.Length);
            PrazoRemetenteCaidaPedidaUpDown.Focus();
        }

        private void PrazoRemetenteCaixaPostalUpDown_MouseClick(object sender, MouseEventArgs e)
        {
            PrazoRemetenteCaixaPostalUpDown.Select(0, PrazoRemetenteCaixaPostalUpDown.Text.Length);
            PrazoRemetenteCaixaPostalUpDown.Focus();
        }

        //        void AtualizaTodosSelecionados()
        //        {
        //            //PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao

        //#if !DEBUG
        //            waitForm.Show(this);
        //#endif

        //            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
        //            {
        //                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
        //                foreach (var item in selecionados)
        //                {
        //                    List<Parametros> pr = new List<Parametros>() {
        //                            new Parametros() { Nome = "@PrazoDestinoCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoDestinatarioCaidaPedidaUpDown.Value.ToString() },
        //                            new Parametros() { Nome = "@PrazoDestinoCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoDestinatarioCaixaPostalUpDown.Value.ToString() },
        //                            new Parametros() { Nome = "@PrazoRemetenteCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaidaPedidaUpDown.Value.ToString() },
        //                            new Parametros() { Nome = "@PrazoRemetenteCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaixaPostalUpDown.Value.ToString() },
        //                            new Parametros() { Nome = "@TipoClassificacao", Tipo = TipoCampo.Text, Valor = comboBoxTipoClassificacao.SelectedItem.ToString() },
        //                            new Parametros() { Nome = "@DataAlteracao", Tipo = TipoCampo.Text, Valor = DateTime.Now.ToString() },
        //                            new Parametros() { Nome = "@Codigo", Tipo = TipoCampo.Int, Valor = item.Key.ToInt() },
        //                            new Parametros() { Nome = "@Sigla", Tipo = TipoCampo.Text, Valor = item.Value.ToString() }
        //                        };
        //                    dao.ExecutaSQL("UPDATE TiposPostais SET PrazoDestinoCaidaPedida = @PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal = @PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida = @PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal = @PrazoRemetenteCaixaPostal, TipoClassificacao = @TipoClassificacao, DataAlteracao = @DataAlteracao  WHERE (Codigo = @Codigo AND Sigla = @Sigla)", pr);


        //                    List<Parametros> prSigla = new List<Parametros>() {
        //                            new Parametros() { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Int, Valor = false },
        //                            new Parametros() { Nome = "@TipoPostalSiglaCodigo", Tipo = TipoCampo.Text, Valor = item.Value.ToString() }
        //                        };
        //                    DataSet DsObjetosNaoEntreguesMesmoTipoSigla = dao.RetornaDataSet("SELECT Codigo, CodigoObjeto, NomeCliente, ObjetoEntregue, CaixaPostal, Comentario, TipoPostalPrazoDiasCorridosRegulamentado FROM TabelaObjetosSROLocal WHERE ObjetoEntregue = @ObjetoEntregue AND Left(CodigoObjeto, 2) = @TipoPostalSiglaCodigo", prSigla);
        //                    if (DsObjetosNaoEntreguesMesmoTipoSigla.Tables[0].Rows.Count > 0)
        //                    {
        //                        foreach (DataRow itemObjetoNaoEntregue in DsObjetosNaoEntreguesMesmoTipoSigla.Tables[0].Rows)
        //                        {
        //                            //verificar se é caixa postal 
        //                            //verificar se é remetente
        //                            bool AoRemetente = false;
        //                            bool ClienteCaixaPostal = false;

        //                            string CodigoObjetoAtual = itemObjetoNaoEntregue["CodigoObjeto"].ToString();
        //                            string NomeClienteAtual = itemObjetoNaoEntregue["NomeCliente"].ToString();
        //                            string ComentarioAtual = itemObjetoNaoEntregue["Comentario"].ToString();

        //                            if (NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REMETENTE") ||
        //                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REMETENT") ||
        //                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REMETEN") ||
        //                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REMETE") ||
        //                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REMET") ||
        //                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REME") ||
        //                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REM") ||
        //                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("DEVOLUCAO") ||
        //                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("DEVOLUCA") ||
        //                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("DEVOLUC") ||
        //                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("DEVOLU") ||
        //                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("ORIGEM"))
        //                            {
        //                                AoRemetente = true;
        //                            }

        //                            if (ComentarioAtual.ToUpper().RemoveAcentos().Contains("REMETENTE") ||
        //                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("REMETENT") ||
        //                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("REMETEN") ||
        //                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("REMETE") ||
        //                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("REMET") ||
        //                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("REME") ||
        //                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("REM") ||
        //                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("DEVOLUCAO") ||
        //                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("DEVOLUCA") ||
        //                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("DEVOLUC") ||
        //                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("DEVOLU") ||
        //                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("ORIGEM"))
        //                            {
        //                                AoRemetente = true;
        //                            }

        //                            if (Convert.ToBoolean(itemObjetoNaoEntregue["CaixaPostal"]))
        //                            {
        //                                ClienteCaixaPostal = true;
        //                            }

        //                            if (AoRemetente) // ao remetente
        //                            {
        //                                if (ClienteCaixaPostal)
        //                                {
        //                                    List<Parametros> pr1 = new List<Parametros>() {
        //                                        new Parametros() { Nome = "@PrazoRemetenteCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaixaPostalUpDown.Value.ToString() },
        //                                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual.ToString() },
        //                                        new Parametros() { Nome = "@DataInicial_dateTimePicker", Tipo = TipoCampo.Text, Valor = DataInicial_dateTimePicker.Text.ToString() }
        //                                    };
        //                                    if (radioButtonAPartirDoDia.Checked)
        //                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoRemetenteCaixaPostal WHERE (CodigoObjeto = @CodigoObjeto) AND format(DataLancamento, 'dd/MM/yyyy') >= @DataInicial_dateTimePicker", pr1);
        //                                    else
        //                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoRemetenteCaixaPostal WHERE (CodigoObjeto = @CodigoObjeto)", pr1);
        //                                }
        //                                else // não é caixa postal
        //                                {
        //                                    List<Parametros> pr2 = new List<Parametros>() {
        //                                        new Parametros() { Nome = "@PrazoRemetenteCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaidaPedidaUpDown.Value.ToString() },
        //                                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual.ToString() },
        //                                        new Parametros() { Nome = "@DataInicial_dateTimePicker", Tipo = TipoCampo.Text, Valor = DataInicial_dateTimePicker.Text.ToString() }
        //                                    };
        //                                    if (radioButtonAPartirDoDia.Checked)
        //                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoRemetenteCaidaPedida WHERE (CodigoObjeto = @CodigoObjeto) AND format(DataLancamento, 'dd/MM/yyyy') >= @DataInicial_dateTimePicker", pr2);
        //                                    else
        //                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoRemetenteCaidaPedida WHERE (CodigoObjeto = @CodigoObjeto)", pr2);
        //                                }
        //                            }
        //                            else // ao destinatário
        //                            {
        //                                if (ClienteCaixaPostal)
        //                                {
        //                                    List<Parametros> pr3 = new List<Parametros>() {
        //                                        new Parametros() { Nome = "@PrazoDestinoCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoDestinatarioCaixaPostalUpDown.Value.ToString() },
        //                                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual.ToString() },
        //                                        new Parametros() { Nome = "@DataInicial_dateTimePicker", Tipo = TipoCampo.Text, Valor = DataInicial_dateTimePicker.Text.ToString() }
        //                                    };
        //                                    if (radioButtonAPartirDoDia.Checked)
        //                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoDestinoCaixaPostal WHERE (CodigoObjeto = @CodigoObjeto) AND format(DataLancamento, 'dd/MM/yyyy') >= @DataInicial_dateTimePicker", pr3);
        //                                    else
        //                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoDestinoCaixaPostal WHERE (CodigoObjeto = @CodigoObjeto)", pr3);
        //                                }
        //                                else // nao é caixa postal
        //                                {
        //                                    List<Parametros> pr4 = new List<Parametros>() {
        //                                        new Parametros() { Nome = "@PrazoDestinoCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoDestinatarioCaidaPedidaUpDown.Value.ToString() },
        //                                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual.ToString() },
        //                                        new Parametros() { Nome = "@DataInicial_dateTimePicker", Tipo = TipoCampo.Text, Valor = DataInicial_dateTimePicker.Text.ToString() }
        //                                    };
        //                                    if (radioButtonAPartirDoDia.Checked)
        //                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoDestinoCaidaPedida WHERE (CodigoObjeto = @CodigoObjeto) AND format(DataLancamento, 'dd/MM/yyyy') >= @DataInicial_dateTimePicker", pr4);
        //                                    else
        //                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoDestinoCaidaPedida WHERE (CodigoObjeto = @CodigoObjeto)", pr4);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //#if !DEBUG
        //            waitForm.Close();
        //#endif
        //        }
    }
}
