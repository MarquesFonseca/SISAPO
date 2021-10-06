using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SISAPO.ClassesDiversas;

namespace SISAPO
{
    public partial class FormularioAuxilioGestaoDiaNovo : Form
    {
        WaitWndFun waitForm = new WaitWndFun();
        DataTable listaObjetos = new DataTable();
        string MontaFiltro = string.Empty;
        string ValorDataInicialDateTimePickerSelecionado = DateTime.Now.Date.ToShortDateString();
        public enum ModeloTelaAbertura { TelaAguardandoRetirada, TelaColarItensSRO }
        public ModeloTelaAbertura ModeloTelaAberturaSelecionado = ModeloTelaAbertura.TelaAguardandoRetirada;

        public FormularioAuxilioGestaoDiaNovo()
        {
            InitializeComponent();
            DataTable listaObjetos = new DataTable();

            ModeloTelaAberturaSelecionado = ModeloTelaAbertura.TelaAguardandoRetirada;
        }

        public FormularioAuxilioGestaoDiaNovo(ModeloTelaAbertura modeloTelaAberturaSelecionado)
        {
            InitializeComponent();

            DataTable listaObjetos = new DataTable();

            ModeloTelaAberturaSelecionado = modeloTelaAberturaSelecionado;
        }

        private void FormularioAuxilioGestaoDiaNovo_Load(object sender, EventArgs e)
        {
            this.ConfiguraMenusEBotoesParaACCAgenciaComunitaria(Configuracoes.ACCAgenciaComunitaria);

            if (ModeloTelaAberturaSelecionado == ModeloTelaAbertura.TelaAguardandoRetirada)
            {
                BtnColarConteudoJaCopiado.Visible = false;
                BtnRetornaTodosNaoEntregues.Visible = true;
                label3.Text = "AUXÍLIO A GESTÃO DO DIA - ITENS NÃO ENTREGUES";
            }
            if (ModeloTelaAberturaSelecionado == ModeloTelaAbertura.TelaColarItensSRO)
            {
                BtnColarConteudoJaCopiado.Visible = true;
                BtnRetornaTodosNaoEntregues.Visible = false;
                label3.Text = "AUXÍLIO A GESTÃO DO DIA - ITENS COLADOS SRO";
                FiltrarPorMaisFiltrosPorPrevisaoDiaCheckBox.Visible = false;
                DataInicial_dateTimePicker.Visible = false;
            }
        }

        public static FormularioAuxilioGestaoDiaNovo RetornaComponentesFormularioAuxilioGestaoDiaNovo()
        {
            FormularioAuxilioGestaoDiaNovo formularioAuxilioGestaoDiaNovo;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioAuxilioGestaoDiaNovo")
                {
                    formularioAuxilioGestaoDiaNovo = (FormularioAuxilioGestaoDiaNovo)item;
                    return (FormularioAuxilioGestaoDiaNovo)item;
                }
                if (item.Name == "FormularioAuxilioGestaoDiaNovoItensNaoEntregues")
                {
                    formularioAuxilioGestaoDiaNovo = (FormularioAuxilioGestaoDiaNovo)item;
                    return (FormularioAuxilioGestaoDiaNovo)item;
                }
                
            }
            return null;
        }

        private void BtnRetornaTodosNaoEntregues_Click(object sender, EventArgs e)
        {
            try
            {
                BtnRetornaTodosNaoEntregues.Enabled = false;

#if !DEBUG
                waitForm.Show(this);
#endif

                listaObjetos = RetornaListaObjetosNaoEntregues();
                if (listaObjetos == null || listaObjetos.Rows.Count == 0)
                {
                    FiltrarPorPrazosVENCIDOSCheckBox.Enabled = FiltrarPorPrazosVENCENDOHOJECheckBox.Enabled = FiltrarPorPrazosAVENCERCheckBox.Enabled = false;
                    FiltrarPorClassificacaoPACCCheckBox.Enabled = FiltrarPorClassificacaoSEDEXCheckBox.Enabled = FiltrarPorClassificacaoDIVERSOSCheckBox.Enabled = false;
                    FiltrarPorMaisFiltrosIncluirCaixaPostalCheckBox.Enabled = false;
                    FiltrarPorMaisFiltrosPorPrevisaoDiaCheckBox.Enabled = false;
                    //Mensagens.Informa("Não foi possível carregar.\nCopie a lista e clique no botão para tentar novamente ."); 
                    return;
                }

                FiltrarPorPrazosVENCIDOSCheckBox.Enabled = FiltrarPorPrazosVENCENDOHOJECheckBox.Enabled = FiltrarPorPrazosAVENCERCheckBox.Enabled = true;
                //FiltrarPorPrazosVENCIDOSCheckBox.Checked = FiltrarPorPrazosVENCENDOHOJECheckBox.Checked = FiltrarPorPrazosAVENCERCheckBox.Checked = true;

                FiltrarPorClassificacaoPACCCheckBox.Enabled = FiltrarPorClassificacaoSEDEXCheckBox.Enabled = FiltrarPorClassificacaoDIVERSOSCheckBox.Enabled = true;
                //FiltrarPorClassificacaoPACCCheckBox.Checked = FiltrarPorClassificacaoSEDEXCheckBox.Checked = FiltrarPorClassificacaoDIVERSOSCheckBox.Checked = true;

                FiltrarPorMaisFiltrosIncluirCaixaPostalCheckBox.Enabled = true;
                //FiltrarPorMaisFiltrosIncluirCaixaPostalCheckBox.Checked = true;

                FiltrarPorMaisFiltrosPorPrevisaoDiaCheckBox.Enabled = true;
                //FiltrarPorMaisFiltrosPorPrevisaoDiaCheckBox.Checked = false;

                bindingSourceObjetosNaoEntregues = new BindingSource();
                bindingSourceObjetosNaoEntregues.DataSource = listaObjetos;
                bindingSourceObjetosNaoEntregues.Sort = "QtdDiasCorridos Desc, Sigla ASC";
                dataGridView1.DataSource = bindingSourceObjetosNaoEntregues;

                FiltrosCheckBox();

#if !DEBUG
                waitForm.Close();
#endif
            }
            catch (IOException)
            {
#if !DEBUG
                waitForm.Close();
#endif
            }
            finally
            {
                BtnRetornaTodosNaoEntregues.Enabled = true;
            }
        }

        private void BtnColarConteudoJaCopiado_Click(object sender, EventArgs e)
        {
            BtnColarConteudoJaCopiado.Enabled = false;

            string curDir = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeArquivo = string.Format("GestaoDoDia.txt");
            string nomeEnderecoArquivo = string.Format(@"{0}\{1}", curDir, nomeArquivo);
            StringBuilder textoColadoAreaTransferencia = new StringBuilder();

            //SE NÃO ESTIVER VAZIO ENTRA
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
                    string texto = textoColadoAreaTransferencia.ToString();

                    listaObjetos = RetornaListaObjetos(textoColadoAreaTransferencia.ToString());


                    if (listaObjetos == null || listaObjetos.Rows.Count == 0)
                    {
                        FiltrarPorPrazosVENCIDOSCheckBox.Enabled = FiltrarPorPrazosVENCENDOHOJECheckBox.Enabled = FiltrarPorPrazosAVENCERCheckBox.Enabled = false;
                        FiltrarPorClassificacaoPACCCheckBox.Enabled = FiltrarPorClassificacaoSEDEXCheckBox.Enabled = FiltrarPorClassificacaoDIVERSOSCheckBox.Enabled = false;
                        FiltrarPorMaisFiltrosIncluirCaixaPostalCheckBox.Enabled = false;
                        FiltrarPorMaisFiltrosPorPrevisaoDiaCheckBox.Enabled = false;

                        dataGridView1.DataSource = listaObjetos;
                        listaObjetos.Clear(); //Retira os valores da tabela mantendo os campos
                        Mensagens.Informa("Não foi possível carregar.\nCopie a lista e clique no botão para tentar novamente .");
                        LbnQuantidadeRegistros.Text = string.Format("{0}", listaObjetos.Rows.Count);
                        return;
                    }

                    FiltrarPorPrazosVENCIDOSCheckBox.Enabled = FiltrarPorPrazosVENCENDOHOJECheckBox.Enabled = FiltrarPorPrazosAVENCERCheckBox.Enabled = true;
                    FiltrarPorPrazosVENCIDOSCheckBox.Checked = FiltrarPorPrazosVENCENDOHOJECheckBox.Checked = FiltrarPorPrazosAVENCERCheckBox.Checked = true;

                    FiltrarPorClassificacaoPACCCheckBox.Enabled = FiltrarPorClassificacaoSEDEXCheckBox.Enabled = FiltrarPorClassificacaoDIVERSOSCheckBox.Enabled = true;
                    FiltrarPorClassificacaoPACCCheckBox.Checked = FiltrarPorClassificacaoSEDEXCheckBox.Checked = FiltrarPorClassificacaoDIVERSOSCheckBox.Checked = true;

                    FiltrarPorMaisFiltrosIncluirCaixaPostalCheckBox.Enabled = true;
                    FiltrarPorMaisFiltrosIncluirCaixaPostalCheckBox.Checked = true;

                    FiltrarPorMaisFiltrosPorPrevisaoDiaCheckBox.Enabled = true;
                    FiltrarPorMaisFiltrosPorPrevisaoDiaCheckBox.Checked = false;

                    bindingSourceObjetosNaoEntregues = new BindingSource();
                    bindingSourceObjetosNaoEntregues.DataSource = listaObjetos;
                    dataGridView1.DataSource = bindingSourceObjetosNaoEntregues;

                    FiltrosCheckBox();

                    //LbnQuantidadeRegistros.Text = bindingSourceObjetosNaoEntregues.Count.ToString();
                    //dataGridView1.Focus();
                }
                catch (IOException)
                {
                }
                finally
                {
                    BtnColarConteudoJaCopiado.Enabled = true;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormularioAuxilioGestaoDiaNovo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                BtnImprimirListaAtual_Click(sender, e);
            }
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

        private DataTable RetornaListaObjetos(string Texto)
        {
            DataTable dtbLista = new DataTable();
            dtbLista.Columns.Add("CodigoLdi", typeof(string));
            dtbLista.Columns.Add("Sigla", typeof(string));
            dtbLista.Columns.Add("CodigoObjeto", typeof(string));
            dtbLista.Columns.Add("TipoClassificacao", typeof(string));
            dtbLista.Columns.Add("NomeCliente", typeof(string));
            dtbLista.Columns.Add("Comentario", typeof(string));
            dtbLista.Columns.Add("CaixaPostal", typeof(bool));
            dtbLista.Columns.Add("DataLancamento", typeof(DateTime));
            dtbLista.Columns.Add("QtdDiasCorridos", typeof(string));
            dtbLista.Columns.Add("PrazoTipoClassificacao", typeof(int));
            dtbLista.Columns.Add("DataVencimento", typeof(DateTime));
            dtbLista.Columns.Add("StatusPrazo", typeof(string));
            dtbLista.Columns.Add("QtdDiasVencidos", typeof(string));

            try
            {
                //waitForm.Show(this);

                string[] linha = Texto.Split('\n');

                for (int i = 0; i < linha.Length; i++)
                {
                    if (linha[i] == "" || linha[i] == "\r") continue;
                    string[] Parteslinha = linha[i].Split('\t');
                    string ParteLinhaCodigoLdi = Parteslinha.Length >= 1 ? Parteslinha[0].Trim().ToUpper() : "";
                    string ParteLinhaCodigoObjeto = Parteslinha.Length >= 2 ? Parteslinha[1].Trim().ToUpper() : "";

                    if (ParteLinhaCodigoObjeto != "")
                    {
                        using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                        {
                            if (!DAO.TestaConexao(ClassesDiversas.Configuracoes.strConexao, TipoBanco.OleDb)) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return null; }

                            //ParteLinhaCodigoObjeto = "JU939963115BR";

                            DataTable RetornaLista = RetornaListaObjetosNaoEntregues(string.Format("CodigoObjeto = '{0}' AND CodigoLdi = '{1}'", ParteLinhaCodigoObjeto, ParteLinhaCodigoLdi));

                            if (RetornaLista.Rows.Count == 0)
                            {
                                continue;
                            }
                            else
                            {
                                //CodigoLdi, Sigla, CodigoObjeto, TipoClassificacao, NomeCliente, DataLancamento, QtdDiasCorridos, PrazoTipoClassificacao, DataVencimento, StatusPrazo, QtdDiasVencidos

                                dtbLista.Rows.Add(RetornaLista.Rows[0]["CodigoLdi"], RetornaLista.Rows[0]["Sigla"], RetornaLista.Rows[0]["CodigoObjeto"], RetornaLista.Rows[0]["TipoClassificacao"], RetornaLista.Rows[0]["NomeCliente"], RetornaLista.Rows[0]["Comentario"], RetornaLista.Rows[0]["CaixaPostal"], RetornaLista.Rows[0]["DataLancamento"], RetornaLista.Rows[0]["QtdDiasCorridos"], RetornaLista.Rows[0]["PrazoTipoClassificacao"], RetornaLista.Rows[0]["DataVencimento"], RetornaLista.Rows[0]["StatusPrazo"], RetornaLista.Rows[0]["QtdDiasVencidos"]);
                            }
                        }
                    }
                }

                //waitForm.Close();
                return dtbLista;
            }
            catch (Exception ex)
            {
                //waitForm.Close();
                Mensagens.Erro(ex.Message);
                return dtbLista;
            }
        }

        public void ConfiguraMenusEBotoesParaACCAgenciaComunitaria(bool @ModoACCAgenciaComunitaria)
        {
            AtualizarObjetosSelecionadosToolStripMenuItem.Enabled = !@ModoACCAgenciaComunitaria;
        }

        private DataTable RetornaListaObjetosNaoEntregues()
        {
            DataTable dtbLista = new DataTable();
            dtbLista.Columns.Add("CodigoLdi", typeof(string));
            dtbLista.Columns.Add("Sigla", typeof(string));
            dtbLista.Columns.Add("CodigoObjeto", typeof(string));
            dtbLista.Columns.Add("TipoClassificacao", typeof(string));
            dtbLista.Columns.Add("NomeCliente", typeof(string));
            dtbLista.Columns.Add("Comentario", typeof(string));
            dtbLista.Columns.Add("CaixaPostal", typeof(bool));
            dtbLista.Columns.Add("DataLancamento", typeof(DateTime));
            dtbLista.Columns.Add("QtdDiasCorridos", typeof(string));
            dtbLista.Columns.Add("PrazoTipoClassificacao", typeof(int));
            dtbLista.Columns.Add("DataVencimento", typeof(DateTime));
            dtbLista.Columns.Add("StatusPrazo", typeof(string));
            dtbLista.Columns.Add("QtdDiasVencidos", typeof(string));

            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return null; }

                    StringBuilder stringSQL = RetornaSelectConsultaObjetosNaoEntregues();
                    stringSQL.AppendLine("ORDER BY ObjetosNaoEntregues.QtdDiasCorridos DESC, ObjetosNaoEntregues.TipoClassificacao ASC                                                                                                                                                                                                                                      ");

                    string teste = stringSQL.ToString();

                    dtbLista = dao.RetornaDataTable(stringSQL.ToString());

                    bindingSourceObjetosNaoEntregues = new BindingSource();
                    bindingSourceObjetosNaoEntregues.DataSource = dtbLista;
                    dataGridView1.DataSource = bindingSourceObjetosNaoEntregues;
                }
                return dtbLista;
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
                return dtbLista;
            }
        }

        private static StringBuilder RetornaSelectConsultaObjetosNaoEntregues()
        {
            StringBuilder stringSQL = new StringBuilder();
            stringSQL.AppendLine("SELECT                                                                                                                                                                                                                                                                                                                                                     ");
            stringSQL.AppendLine(" ObjetosNaoEntregues.CodigoLdi                                                                                                                                                                                                                                                                                                                             ");
            stringSQL.AppendLine(",ObjetosNaoEntregues.Sigla                                                                                                                                                                                                                                                                                                                                 ");
            stringSQL.AppendLine(",ObjetosNaoEntregues.CodigoObjeto                                                                                                                                                                                                                                                                                                                          ");
            stringSQL.AppendLine(",ObjetosNaoEntregues.TipoClassificacao                                                                                                                                                                                                                                                                                                                     ");
            stringSQL.AppendLine(",ObjetosNaoEntregues.NomeCliente                                                                                                                                                                                                                                                                                                                           ");
            stringSQL.AppendLine(",ObjetosNaoEntregues.Comentario                                                                                                                                                                                                                                                                                                                            ");
            stringSQL.AppendLine(",ObjetosNaoEntregues.CaixaPostal                                                                                                                                                                                                                                                                                                                           ");
            stringSQL.AppendLine(",ObjetosNaoEntregues.DataLancamento                                                                                                                                                                                                                                                                                                                        ");
            stringSQL.AppendLine(",ObjetosNaoEntregues.QtdDiasCorridos                                                                                                                                                                                                                                                                                                                       ");
            stringSQL.AppendLine(",ObjetosNaoEntregues.PrazoTipoClassificacao                                                                                                                                                                                                                                                                                                                ");
            stringSQL.AppendLine(",ObjetosNaoEntregues.DataVencimento                                                                                                                                                                                                                                                                                                                        ");
            stringSQL.AppendLine(",ObjetosNaoEntregues.StatusPrazo                                                                                                                                                                                                                                                                                                                           ");
            stringSQL.AppendLine(",ObjetosNaoEntregues.QtdDiasVencidos                                                                                                                                                                                                                                                                                                                       ");
            stringSQL.AppendLine("FROM (                                                                                                                                                                                                                                                                                                                                                     ");
            stringSQL.AppendLine("SELECT                                                                                                                                                                                                                                                                                                                                                     ");
            stringSQL.AppendLine(" TabelaObjetosSROLocal.CodigoLdi                                                                                                                                                                                                                                                                                                                           ");
            stringSQL.AppendLine(",Left(TabelaObjetosSROLocal.CodigoObjeto, 2) AS Sigla                                                                                                                                                                                                                                                                                                      ");
            stringSQL.AppendLine(",TabelaObjetosSROLocal.CodigoObjeto                                                                                                                                                                                                                                                                                                                        ");
            stringSQL.AppendLine(",(SELECT TiposPostais.TipoClassificacao from TiposPostais WHERE TiposPostais.Sigla = Left(TabelaObjetosSROLocal.CodigoObjeto, 2)) AS TipoClassificacao                                                                                                                                                                                                     ");
            //stringSQL.AppendLine(",TabelaObjetosSROLocal.NomeCliente                                                                                                                                                                                                                                                                                                                         ");
            stringSQL.AppendLine(",TabelaObjetosSROLocal.NomeCliente & IIf(TabelaObjetosSROLocal.Comentario IS NULL OR TabelaObjetosSROLocal.Comentario = '','', ' - ' & TabelaObjetosSROLocal.Comentario) AS NomeCliente                                                                                                                                                                                                                                                                                                                         ");
            stringSQL.AppendLine(",TabelaObjetosSROLocal.Comentario                                                                                                                                                                                                                                                                                                                          ");
            stringSQL.AppendLine(",TabelaObjetosSROLocal.CaixaPostal                                                                                                                                                                                                                                                                                                                         ");
            stringSQL.AppendLine(",FORMAT(IIf(TabelaObjetosSROLocal.DataLancamento = \"\" OR TabelaObjetosSROLocal.DataLancamento IS NULL, \"01/01/1900 00:00:00\", TabelaObjetosSROLocal.DataLancamento), \"dd/MM/yyyy\") AS DataLancamento                                                                                                                                                                                                ");
            stringSQL.AppendLine(",DATEDIFF(\"d\", IIf(TabelaObjetosSROLocal.DataLancamento = \"\" OR TabelaObjetosSROLocal.DataLancamento IS NULL, \"01/01/1900 00:00:00\", TabelaObjetosSROLocal.DataLancamento), Now()) AS QtdDiasCorridos                                                                                                                                                                                               ");
            stringSQL.AppendLine(",IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado = \"\" OR TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado IS NULL, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado) AS PrazoTipoClassificacao                                                                                                                                                                                  ");
            stringSQL.AppendLine(",FORMAT(DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado = \"\" OR TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado IS NULL, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), IIf(TabelaObjetosSROLocal.DataLancamento = \"\" OR TabelaObjetosSROLocal.DataLancamento IS NULL, \"01/01/1900 00:00:00\", TabelaObjetosSROLocal.DataLancamento)),\"dd/MM/yyyy\") AS DataVencimento                             ");
            stringSQL.AppendLine(",SWITCH                                                                                                                                                                                                                                                                                                                                                    ");
            stringSQL.AppendLine("(                                                                                                                                                                                                                                                                                                                                                          ");
            stringSQL.AppendLine("	DATEDIFF(\"d\", FORMAT(DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado = \"\" OR TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado IS NULL, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), IIf(TabelaObjetosSROLocal.DataLancamento = \"\" OR TabelaObjetosSROLocal.DataLancamento IS NULL, \"01/01/1900 00:00:00\", TabelaObjetosSROLocal.DataLancamento)),\"dd/MM/yyyy\"), Now()) = 0 , \"VENCENDO HOJE\", ");
            stringSQL.AppendLine("	DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado = \"\" OR TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado IS NULL, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), IIf(TabelaObjetosSROLocal.DataLancamento = \"\" OR TabelaObjetosSROLocal.DataLancamento IS NULL, \"01/01/1900 00:00:00\", TabelaObjetosSROLocal.DataLancamento)) < NOW() , \"VENCIDO\",                                              ");
            stringSQL.AppendLine("	DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado = \"\" OR TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado IS NULL, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), IIf(TabelaObjetosSROLocal.DataLancamento = \"\" OR TabelaObjetosSROLocal.DataLancamento IS NULL, \"01/01/1900 00:00:00\", TabelaObjetosSROLocal.DataLancamento)) > NOW() , \"A VENCER\"                                              ");
            stringSQL.AppendLine(") AS StatusPrazo                                                                                                                                                                                                                                                                                                                                           ");
            stringSQL.AppendLine(",DATEDIFF(\"d\", FORMAT(DATEADD(\"d\",IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado = \"\" OR TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado IS NULL, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), IIf(TabelaObjetosSROLocal.DataLancamento = \"\" OR TabelaObjetosSROLocal.DataLancamento IS NULL, \"01/01/1900 00:00:00\", TabelaObjetosSROLocal.DataLancamento)),\"dd/MM/yyyy\"), Now()) AS QtdDiasVencidos         ");
            stringSQL.AppendLine("FROM TabelaObjetosSROLocal                                                                                                                                                                                                                                                                                                                                 ");
            stringSQL.AppendLine("WHERE (TabelaObjetosSROLocal.ObjetoEntregue = FALSE)                                                                                                                                                                                                                                                                                                       ");
            stringSQL.AppendLine(") AS ObjetosNaoEntregues ");
            return stringSQL;
        }

        private DataTable RetornaListaObjetosNaoEntregues(string filtrosAdicionais)
        {
            DataTable dtbLista = new DataTable();
            dtbLista.Columns.Add("CodigoLdi", typeof(string));
            dtbLista.Columns.Add("Sigla", typeof(string));
            dtbLista.Columns.Add("CodigoObjeto", typeof(string));
            dtbLista.Columns.Add("TipoClassificacao", typeof(string));
            dtbLista.Columns.Add("NomeCliente", typeof(string));
            dtbLista.Columns.Add("Comentario", typeof(string));
            dtbLista.Columns.Add("CaixaPostal", typeof(bool));
            dtbLista.Columns.Add("DataLancamento", typeof(DateTime));
            dtbLista.Columns.Add("QtdDiasCorridos", typeof(string));
            dtbLista.Columns.Add("PrazoTipoClassificacao", typeof(int));
            dtbLista.Columns.Add("DataVencimento", typeof(DateTime));
            dtbLista.Columns.Add("StatusPrazo", typeof(string));
            dtbLista.Columns.Add("QtdDiasVencidos", typeof(string));

            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return null; }

                    StringBuilder stringSQL = RetornaSelectConsultaObjetosNaoEntregues();
                    stringSQL.AppendLine("WHERE " + filtrosAdicionais.Replace("'", "\"").Replace("StatusPrazo", "ObjetosNaoEntregues.StatusPrazo").Replace("TipoClassificacao", "ObjetosNaoEntregues.TipoClassificacao"));
                    //stringSQL.AppendLine("ORDER BY ObjetosNaoEntregues.QtdDiasCorridos DESC, ObjetosNaoEntregues.TipoClassificacao ASC");

                    string resulteste = stringSQL.ToString();

                    dtbLista = dao.RetornaDataTable(stringSQL.ToString());
                }
                return dtbLista;
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
                return dtbLista;
            }
        }

        private void BtnImprimirListaAtual_Click(object sender, EventArgs e)
        {
            //DataTable listaObjetosListaImpressao = RetornaListaObjetosNaoEntregues(MontaFiltro);
            //listaObjetosListaImpressao = (DataTable)bindingSourceObjetosNaoEntregues.DataSource;
            if (bindingSourceObjetosNaoEntregues.Current == null) return;
            DataTable listaObjetosListaImpressao = ((System.Data.DataRowView)bindingSourceObjetosNaoEntregues.Current).DataView.ToTable();

            if (listaObjetosListaImpressao.Rows.Count == 0) return;

            listaObjetos = listaObjetosListaImpressao.AsEnumerable().OrderBy(r => r.Field<string>("NomeCliente")).CopyToDataTable();

            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioImpressaoAuxilioGestaoDia")
                {
                    item.Close();
                    break;
                }
            }

            //FormularioImpressaoAuxilioGestaoDia formularioImpressaoAuxilioGestaoDia = new FormularioImpressaoAuxilioGestaoDia(listaObjetos);
            FormularioImpressaoAuxilioGestaoDiaAgrupados formularioImpressaoAuxilioGestaoDiaAgrupados = new FormularioImpressaoAuxilioGestaoDiaAgrupados(listaObjetos);
            //formularioImpressaoAuxilioGestaoDiaAgrupados.MdiParent = MdiParent;
            formularioImpressaoAuxilioGestaoDiaAgrupados.ShowDialog();
            //formularioImpressaoAuxilioGestaoDiaAgrupados.WindowState = FormWindowState.Normal;
            //formularioImpressaoAuxilioGestaoDiaAgrupados.WindowState = FormWindowState.Maximized;
            //formularioImpressaoAuxilioGestaoDiaAgrupados.Activate();
        }

        private void MudaCorLinhasGridView()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //vencidos
                if (Convert.ToInt32(row.Cells["QtdDiasVencidos"].Value) > 0)
                {
                    // Se for negativo, fica vermelho
                    row.DefaultCellStyle.BackColor = Color.LightSalmon;
                }
                //Vencendo Hoje
                if (Convert.ToInt32(row.Cells["QtdDiasVencidos"].Value) == 0)
                {
                    // Se for negativo, fica vermelho
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
                //A vencer
                if (Convert.ToInt32(row.Cells["QtdDiasVencidos"].Value) < 0)
                {
                    // Se for negativo, fica vermelho
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Configuracoes.ACCAgenciaComunitaria)
                return;

            if (this.dataGridView1.CurrentRow == null) return;
            string CodigoObjeto = this.dataGridView1.CurrentRow.Cells["CodigoObjeto"].Value.ToString();
            using (FormularioSRORastreamentoUnificado formularioSRORastreamentoUnificado = new FormularioSRORastreamentoUnificado(CodigoObjeto))
            {
                formularioSRORastreamentoUnificado.WindowState = FormWindowState.Normal;
                formularioSRORastreamentoUnificado.StartPosition = FormStartPosition.CenterScreen;
                //formularioSRORastreamentoUnificado.Text = string.Format(@"SRO - Rastreamento Unificado - http://websro2.correiosnet.int/rastreamento/sro?opcao=PESQUISA&objetos={0}", CodigoObjeto);
                formularioSRORastreamentoUnificado.Text = string.Format(@"SRO - Rastreamento Unificado - {0}{1}", Configuracoes.EnderecosSRO["EnderecoSROPorObjeto"].ToString(), CodigoObjeto);
                formularioSRORastreamentoUnificado.ShowDialog();
            }
        }

        private void FiltrarPorPrazosVENCIDOSCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FiltrosCheckBox();
        }

        private void FiltrarPorPrazosVENCENDOHOJECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FiltrosCheckBox();
        }

        private void FiltrarPorPrazosAVENCERCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FiltrosCheckBox();
        }

        private void FiltrarPorClassificacaoPACCCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FiltrosCheckBox();
        }

        private void FiltrarPorClassificacaoSEDEXCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FiltrosCheckBox();
        }

        private void FiltrarPorClassificacaoDIVERSOSCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FiltrosCheckBox();
        }

        private void FiltrarPorMaisFiltrosIncluirCaixaPostalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FiltrosCheckBox();
        }

        bool filtrarPorPrazoAVencerAntesDoCheckPorPrevisaoDia = false;
        private void FiltrarPorMaisFiltrosPorPrevisaoDiaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FiltrarPorMaisFiltrosPorPrevisaoDiaCheckBox.Checked)
            {
                filtrarPorPrazoAVencerAntesDoCheckPorPrevisaoDia = FiltrarPorPrazosAVENCERCheckBox.Checked;

                FiltrarPorPrazosAVENCERCheckBox.Checked = true;

                DataInicial_dateTimePicker.Enabled = true;
                DataInicial_dateTimePicker.Focus();//CHAMA O METODO DataInicial_dateTimePicker_GotFocus()
            }
            else
            {
                FiltrarPorPrazosAVENCERCheckBox.Checked = filtrarPorPrazoAVencerAntesDoCheckPorPrevisaoDia;
                DataInicial_dateTimePicker.Enabled = false;
                DataInicial_dateTimePicker.Value = DateTime.Now.Date;
            }
            FiltrosCheckBox();
        }

        private void FiltrosCheckBox()
        {
            bool PrazosVENCIDOS = FiltrarPorPrazosVENCIDOSCheckBox.Checked;
            bool PrazosVENCENDOHOJE = FiltrarPorPrazosVENCENDOHOJECheckBox.Checked;
            bool PrazosAVENCER = FiltrarPorPrazosAVENCERCheckBox.Checked;

            bool ClassificacaoPAC = FiltrarPorClassificacaoPACCCheckBox.Checked;
            bool ClassificacaoSEDEX = FiltrarPorClassificacaoSEDEXCheckBox.Checked;
            bool ClassificacaoDIVERSOS = FiltrarPorClassificacaoDIVERSOSCheckBox.Checked;

            bool IncluirCaixaPostal = FiltrarPorMaisFiltrosIncluirCaixaPostalCheckBox.Checked;

            bool PorPrevisaoDia = FiltrarPorMaisFiltrosPorPrevisaoDiaCheckBox.Checked;

            MontaFiltro = string.Empty;

            if (!PrazosVENCIDOS && !PrazosVENCENDOHOJE && !PrazosAVENCER && !ClassificacaoPAC && !ClassificacaoSEDEX && !ClassificacaoDIVERSOS)
            {
                MontaFiltro = "1<>1";
            }
            else if (!PrazosVENCIDOS && !PrazosVENCENDOHOJE && !PrazosAVENCER)
            {
                MontaFiltro = "1<>1";
            }
            else if (!ClassificacaoPAC && !ClassificacaoSEDEX && !ClassificacaoDIVERSOS)
            {
                MontaFiltro = "1<>1";
            }
            else
            {
                if (!PrazosVENCIDOS && !PrazosVENCENDOHOJE && !PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencido','Vencendo Hoje','A Vencer')";
                }
                if (PrazosVENCIDOS && PrazosVENCENDOHOJE && PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencido','Vencendo Hoje','A Vencer')";
                }
                if (PrazosVENCIDOS && !PrazosVENCENDOHOJE && !PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencido')";
                }
                if (!PrazosVENCIDOS && PrazosVENCENDOHOJE && !PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencendo Hoje')";
                }
                if (!PrazosVENCIDOS && !PrazosVENCENDOHOJE && PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('A Vencer')";
                }
                if (PrazosVENCIDOS && !PrazosVENCENDOHOJE && PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencido','A Vencer')";
                }
                if (PrazosVENCIDOS && PrazosVENCENDOHOJE && !PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencido','Vencendo Hoje')";
                }
                if (!PrazosVENCIDOS && PrazosVENCENDOHOJE && PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencendo Hoje','A Vencer')";
                }

                if (!ClassificacaoPAC && !ClassificacaoSEDEX && !ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('PAC','SEDEX','DIVERSOS')";
                }
                if (ClassificacaoPAC && ClassificacaoSEDEX && ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('PAC','SEDEX','DIVERSOS')";
                }
                if (ClassificacaoPAC && !ClassificacaoSEDEX && !ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('PAC')";
                }
                if (!ClassificacaoPAC && ClassificacaoSEDEX && !ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('SEDEX')";
                }
                if (!ClassificacaoPAC && !ClassificacaoSEDEX && ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('DIVERSOS')";
                }
                if (ClassificacaoPAC && !ClassificacaoSEDEX && ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('PAC','DIVERSOS')";
                }
                if (ClassificacaoPAC && ClassificacaoSEDEX && !ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('PAC','SEDEX')";
                }
                if (!ClassificacaoPAC && ClassificacaoSEDEX && ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('SEDEX','DIVERSOS')";
                }
            }

            #region IncluirCaixaPostal
            if (!IncluirCaixaPostal)
            {
                string incluirCaixaPostal = "AND (CaixaPostal = FALSE)";
                MontaFiltro = string.Format("{0} {1}", MontaFiltro, incluirCaixaPostal);
            }
            #endregion

            #region PorPrevisaoDia
            if (PorPrevisaoDia)
            {
                DayOfWeek diaSemana = Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).DayOfWeek;
                if (diaSemana == DayOfWeek.Saturday)
                {
                    if (Mensagens.Pergunta("Você selecionou um dia de SÁBADO.\nDeseja incluir o SÁBADO + DOMINGO\ne considerar como SEGUNDA?") == DialogResult.Yes)
                    {
                        DateTime Sabado = Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).Date.AddDays(0);
                        double qtdSabado = (DateTime.Now.Date - Sabado.Date).TotalDays;

                        DateTime Domingo = Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).Date.AddDays(1);
                        double qtdDomingo = (DateTime.Now.Date - Domingo.Date).TotalDays;

                        DateTime Segunda = Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).Date.AddDays(2);
                        double qtdSegunda = (DateTime.Now.Date - Segunda.Date).TotalDays;

                        string porPrevisaoDia = string.Format("AND (QtdDiasVencidos IN({0},{1},{2}))", qtdSegunda, qtdDomingo, qtdSabado);
                        MontaFiltro = string.Format("{0} {1}", MontaFiltro, porPrevisaoDia);
                    }
                    else
                    {
                        double qtd = (DateTime.Now.Date - Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).Date).TotalDays;
                        string porPrevisaoDia = string.Format("AND (QtdDiasVencidos IN({0}))", qtd);
                        MontaFiltro = string.Format("{0} {1}", MontaFiltro, porPrevisaoDia);
                    }
                }
                else if (diaSemana == DayOfWeek.Sunday)
                {
                    if (Mensagens.Pergunta("Você selecionou um dia de DOMINGO.\nDeseja incluir o SÁBADO + DOMINGO\ne considerar como SEGUNDA?") == DialogResult.Yes)
                    {
                        DateTime Sabado = Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).Date.AddDays(-1);
                        double qtdSabado = (DateTime.Now.Date - Sabado.Date).TotalDays;

                        DateTime Domingo = Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).Date.AddDays(0);
                        double qtdDomingo = (DateTime.Now.Date - Domingo.Date).TotalDays;

                        DateTime Segunda = Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).Date.AddDays(1);
                        double qtdSegunda = (DateTime.Now.Date - Segunda.Date).TotalDays;

                        string porPrevisaoDia = string.Format("AND (QtdDiasVencidos IN({0},{1},{2}))", qtdSegunda, qtdDomingo, qtdSabado);
                        MontaFiltro = string.Format("{0} {1}", MontaFiltro, porPrevisaoDia);
                    }
                    else
                    {
                        double qtd = (DateTime.Now.Date - Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).Date).TotalDays;
                        string porPrevisaoDia = string.Format("AND (QtdDiasVencidos IN({0}))", qtd);
                        MontaFiltro = string.Format("{0} {1}", MontaFiltro, porPrevisaoDia);
                    }
                }
                else if (diaSemana == DayOfWeek.Monday)//Monday - segunda
                {
                    if (Mensagens.Pergunta("Você selecionou um dia de SEGUNDA.\nDeseja incluir o SÁBADO + DOMINGO\ne considerar como SEGUNDA?") == DialogResult.Yes)
                    {
                        DateTime Sabado = Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).Date.AddDays(-2);
                        double qtdSabado = (DateTime.Now.Date - Sabado.Date).TotalDays;

                        DateTime Domingo = Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).Date.AddDays(-1);
                        double qtdDomingo = (DateTime.Now.Date - Domingo.Date).TotalDays;

                        DateTime Segunda = Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).Date.AddDays(0);
                        double qtdSegunda = (DateTime.Now.Date - Segunda.Date).TotalDays;

                        string porPrevisaoDia = string.Format("AND (QtdDiasVencidos IN({0},{1},{2}))", qtdSegunda, qtdDomingo, qtdSabado);
                        MontaFiltro = string.Format("{0} {1}", MontaFiltro, porPrevisaoDia);
                    }
                    else
                    {
                        double qtd = (DateTime.Now.Date - Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).Date).TotalDays;
                        string porPrevisaoDia = string.Format("AND (QtdDiasVencidos IN({0}))", qtd);
                        MontaFiltro = string.Format("{0} {1}", MontaFiltro, porPrevisaoDia);
                    }
                }
                else
                {
                    double qtd = (DateTime.Now.Date - Convert.ToDateTime(ValorDataInicialDateTimePickerSelecionado).Date).TotalDays;
                    string porPrevisaoDia = string.Format("AND (QtdDiasVencidos IN({0}))", qtd);
                    MontaFiltro = string.Format("{0} {1}", MontaFiltro, porPrevisaoDia);
                }
            }
            #endregion

            bindingSourceObjetosNaoEntregues.Filter = MontaFiltro;
            MudaCorLinhasGridView();

            LbnQuantidadeRegistros.Text = bindingSourceObjetosNaoEntregues.Count.ToString();
            dataGridView1.Focus();
        }

        private void radioButtonDataLancamento_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDataLancamento.Checked)
            {
                if (bindingSourceObjetosNaoEntregues.Count > 0)
                {
                    bindingSourceObjetosNaoEntregues.Sort = "QtdDiasCorridos Desc";
                    MudaCorLinhasGridView();
                }
            }
        }

        private void radioButtonNomeCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNomeCliente.Checked)
            {
                if (bindingSourceObjetosNaoEntregues.Count > 0)
                {
                    bindingSourceObjetosNaoEntregues.Sort = "NomeCliente ASC";
                    MudaCorLinhasGridView();
                }
            }
        }

        private void radioButtonTipoClassificacao_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTipoClassificacao.Checked)
            {
                if (bindingSourceObjetosNaoEntregues.Count > 0)
                {
                    bindingSourceObjetosNaoEntregues.Sort = "TipoClassificacao ASC";
                    MudaCorLinhasGridView();
                }
            }
        }

        private void radioButtonQtdDiasVencidos_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonQtdDiasVencidos.Checked)
            {
                if (bindingSourceObjetosNaoEntregues.Count > 0)
                {
                    bindingSourceObjetosNaoEntregues.Sort = "QtdDiasVencidos ASC";
                    MudaCorLinhasGridView();
                }
            }
        }

        private void FormularioAuxilioGestaoDiaNovo_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                waitForm.Close();
                //this.Dispose()
            }
            catch (Exception)
            {

            }

        }

        private void DataInicial_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ValorDataInicialDateTimePickerSelecionado = DataInicial_dateTimePicker.Value.Date.ToShortDateString();

            FiltrosCheckBox();
        }

        private void alterarSituacaoItensToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0) return;

            FormularioAlterarSituacaoItensSelecionados formularioAlterarSituacaoItensSelecionados = new FormularioAlterarSituacaoItensSelecionados();
            formularioAlterarSituacaoItensSelecionados.ShowDialog();

            if (string.IsNullOrEmpty(formularioAlterarSituacaoItensSelecionados.itemMotivoBaixaSelecionado)) return;

            List<string> ListaGridSelecaoAtual = RetornaListaCodigosGridSelecaoAtual(dataGridView1);
            if (ListaGridSelecaoAtual.Count == 0) return;

            FormularioConsulta.RetornaComponentesFormularioConsulta().AlterarSituacaoItensSelecionados(ListaGridSelecaoAtual, formularioAlterarSituacaoItensSelecionados.itemMotivoBaixaSelecionado);

            //if (Mensagens.Pergunta("Itens atualizado com sucesso! Deseja atualizar grid?") == System.Windows.Forms.DialogResult.Yes)
            //{
            if (ModeloTelaAberturaSelecionado == ModeloTelaAbertura.TelaAguardandoRetirada)
                BtnRetornaTodosNaoEntregues_Click(null, null);
            if (ModeloTelaAberturaSelecionado == ModeloTelaAbertura.TelaColarItensSRO)
                BtnColarConteudoJaCopiado_Click(null, null);
            //}

        }

        private List<string> RetornaListaCodigosGridSelecaoAtual(DataGridView Grid)
        {
            List<string> ListaGridSelecaoAtual = new List<string>();

            List<int> indicesLinhasSelecionadas = new List<int>();
            for (int i = this.dataGridView1.SelectedCells.Count - 1; i >= 0; i--)
            {
                DataGridViewCell cell = this.dataGridView1.SelectedCells[i];
                int rowIndex = cell.RowIndex;
                bool existe = indicesLinhasSelecionadas.AsEnumerable().Any(T => T == rowIndex);
                if (!existe)
                {
                    indicesLinhasSelecionadas.Add(rowIndex);
                    string CodigoAtual = this.dataGridView1.Rows[rowIndex].Cells["CodigoObjeto"].Value.ToString();
                    ListaGridSelecaoAtual.Add(CodigoAtual.ToUpper().Trim());
                }
            }
            return ListaGridSelecaoAtual;
        }

        private void AtualizarObjetosSelecionadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessandoMarcarSelecionadosComoNaoAtualizado();
        }

        public void ProcessandoMarcarSelecionadosComoNaoAtualizado()
        {
            if (dataGridView1.RowCount == 0) return;

            waitForm.Show(this);

            List<string> ListaGridSelecaoAtual = RetornaListaCodigosGridSelecaoAtual(dataGridView1);
            if (ListaGridSelecaoAtual.Count == 0)
            {
                waitForm.Close();
                return;
            }
            foreach (string itemCodigo in ListaGridSelecaoAtual)
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                    List<Parametros> pr = new List<Parametros>() {
                            new Parametros() { Nome = "@Atualizado", Tipo = TipoCampo.Boolean, Valor = false },
                            new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = itemCodigo }
                        };
                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Atualizado = @Atualizado  WHERE (CodigoObjeto = @CodigoObjeto)", pr);
                }
            }

            waitForm.Close();

            FormularioPrincipal.RetornaComponentesFormularioPrincipal().atualizarNovosObjetosToolStripMenuItem_Click(null, null);
        }

        private void alterarItemSomenteAtualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlterarItem(sender, e);
        }

        private void AlterarItem(object sender, EventArgs e)
        {
            int position = this.BindingContext[dataGridView1].Position;
            try
            {

                if (dataGridView1.RowCount == 0) return;

                List<string> ListaGridSelecaoAtual = RetornaListaCodigosGridSelecaoAtual(dataGridView1);
                if (ListaGridSelecaoAtual.Count == 0) return;

                FormularioAlteracaoObjeto frm = new FormularioAlteracaoObjeto() { CodigoObjeto = ListaGridSelecaoAtual[0] };
                frm.ShowDialog();

                if (frm.Cancelando) return;
            }
            catch (Exception ex)
            {
                Mensagens.Erro("Ocorreu um erro inesperado.\nErro: " + ex);
                //throw;
            }
            finally
            {

                if (position > -1) this.BindingContext[dataGridView1].Position = position;

                FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();

                if (ModeloTelaAberturaSelecionado == ModeloTelaAbertura.TelaAguardandoRetirada)
                    BtnRetornaTodosNaoEntregues_Click(null, null);
                if (ModeloTelaAberturaSelecionado == ModeloTelaAbertura.TelaColarItensSRO)
                    BtnColarConteudoJaCopiado_Click(null, null);
            }
        }

    }
}
