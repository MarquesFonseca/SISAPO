using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SISAPO.ClassesDiversas;
using System.IO;

namespace SISAPO
{
    public partial class FormularioConsulta : Form
    {
        public string dataInicial = string.Empty;
        public string datafinal = string.Empty;
        public enum ModeloImpressaoListaObjetos { ModeloLDI, ModeloComum };
        public ModeloImpressaoListaObjetos _modeloImpressaoListaObjetos = ModeloImpressaoListaObjetos.ModeloLDI;

        public static string NovoFiltro = string.Empty;

        public FormularioConsulta()
        {
            InitializeComponent();

            //tabelaObjetosSROLocalTableAdapter
            //this.tabelaObjetosSROLocalTableAdapter.Update(dataSetTabelaObjetosSROLocal.TabelaObjetosSROLocal);

            //StringBuilder selectCommand = new StringBuilder();
            //selectCommand.AppendLine("");
            //selectCommand.AppendLine("SELECT        Codigo, CodigoObjeto, IIf(CodigoLdi IS NULL OR                                                                                                                                                                              ");
            //selectCommand.AppendLine("                         CodigoLdi = '', '000000000000', CodigoLdi) AS CodigoLdi, NomeCliente, Format(IIf(TabelaObjetosSROLocal.DataLancamento IS NULL OR                                                                                 ");
            //selectCommand.AppendLine("                         TabelaObjetosSROLocal.DataLancamento = '', Format(NOW(), 'dd/MM/yyyy 00:00:01'), TabelaObjetosSROLocal.DataLancamento), 'dd/MM/yyyy hh:mm:ss') AS DataLancamento, Format(DataModificacao, 'dd/MM/yyyy hh:mm:ss') ");
            //selectCommand.AppendLine("                          AS DataModificacao, Situacao, Atualizado, ObjetoEntregue, CaixaPostal, UnidadePostagem, MunicipioPostagem, CriacaoPostagem, CepDestinoPostagem, ARPostagem, MPPostagem, DataMaxPrevistaEntregaPostagem,         ");
            //selectCommand.AppendLine("                         UnidadeLOEC, MunicipioLOEC, CriacaoLOEC, CarteiroLOEC, DistritoLOEC, NumeroLOEC, EnderecoLOEC, BairroLOEC, LocalidadeLOEC, SituacaoDestinatarioAusente, AgrupadoDestinatarioAusente,                             ");
            //selectCommand.AppendLine("                         CoordenadasDestinatarioAusente, Comentario, TipoPostalServico, TipoPostalSiglaCodigo, TipoPostalNomeSiglaCodigo, TipoPostalPrazoDiasCorridosRegulamentado                                                        ");
            //selectCommand.AppendLine("FROM            TabelaObjetosSROLocal                                                                                                                                                                                                     ");
            //selectCommand.AppendLine("WHERE        (Format(DataLancamento, 'yyyy/MM/dd') BETWEEN Format(?, 'yyyy/MM/dd') AND Format(?, 'yyyy/MM/dd'))                                                                                                                           ");



        }

        private void FormularioConsulta_Load(object sender, EventArgs e)
        {
            checkBoxIncluirCaixaPostal.Checked = FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirCaixaPostalPesquisa_toolStripMenuItem.Checked;
            checkBoxIncluirBaixados.Checked = FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirItensJaEntreguesToolStripMenuItem.Checked;

            splitContainer3.Panel2Collapsed = true;

            DataFinal_dateTimePicker.Text = DateTime.Now.Date.ToShortDateString();
            DataInicial_dateTimePicker.Text = DateTime.Today.AddMonths(-1).Date.ToShortDateString();

            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
            {
                //DataFinal_dateTimePicker.Text = "25/08/2019";
                //DataInicial_dateTimePicker.Text = "25/09/2019";
            }

            //this.ConsultaTodosNaoEntreguesOrdenadoNome();

            this.dataGridView1.Sort(this.dataGridView1.Columns["DataLancamento"], ListSortDirection.Descending);
            TxtPesquisa.Focus();
            TxtPesquisa.SelecionaControle();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LblCodigoForaDoPadraoBrasileiro.Visible = false;

            MontaFiltro();
        }

        public void MontaFiltro()
        {
            string objetoCaixaPostalSelecionado = "";
            string objetoEntregueSelecionado = "";
            string campoPesquisa = "";
            bool ExibirItensEntregues = FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirItensJaEntreguesToolStripMenuItem.Checked;
            bool IncluirItensCaixaPostal = FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirCaixaPostalPesquisa_toolStripMenuItem.Checked;
            if (TxtPesquisa.Text != "")
            {
                if (FormularioPrincipal.RetornaComponentesFormularioPrincipal().PermiriBuscarPorLDINaPesquisaToolStripMenuItem.Checked == true)
                {
                    campoPesquisa = string.Format("(CodigoObjeto like '%{0}%' OR CodigoLdi like '%{0}%' OR NomeCliente like '%{0}%')", TxtPesquisa.Text.RemoveSimbolos().RemoveSpecialChars());
                }
                else
                {
                    campoPesquisa = string.Format("(CodigoObjeto like '%{0}%' OR NomeCliente like '%{0}%')", TxtPesquisa.Text.RemoveSimbolos().RemoveSpecialChars());
                }
            }
            if (!ExibirItensEntregues)
            {
                objetoEntregueSelecionado = string.Format(" AND ObjetoEntregue = {0} ", false);

                if (TxtPesquisa.Text == "")
                    objetoEntregueSelecionado = objetoEntregueSelecionado.Replace(" AND ", "");

                if (!IncluirItensCaixaPostal)
                {
                    objetoCaixaPostalSelecionado = string.Format(" AND CaixaPostal = {0} ", false);

                    //if (TxtPesquisa.Text == "")
                    // objetoCaixaPostalSelecionado = objetoCaixaPostalSelecionado.Replace(" AND CaixaPostal ", " CaixaPostal ");
                }
            }
            else
            {
                if (!IncluirItensCaixaPostal)
                {
                    objetoCaixaPostalSelecionado = string.Format(" AND CaixaPostal = {0} ", false);

                    if (TxtPesquisa.Text == "")
                        objetoCaixaPostalSelecionado = objetoCaixaPostalSelecionado.Replace(" AND CaixaPostal ", " CaixaPostal ");
                }
            }

            NovoFiltro = string.Format("{0}{1}{2}",
                campoPesquisa,
                objetoEntregueSelecionado,
                objetoCaixaPostalSelecionado);

            tabelaObjetosSROLocalBindingSource.Filter = NovoFiltro;

            LbnQuantidadeRegistros.Text = string.Format("{0}", tabelaObjetosSROLocalBindingSource.Count);
            Btn0SelecionarLinhas.Text = string.Format("&0 - Selecionar {0} linhas", tabelaObjetosSROLocalBindingSource.Count);
            if (tabelaObjetosSROLocalBindingSource.Count == 0)
            {
                LblDadosPostagemIndisponivel.Visible = true;
                LblDadosPostagemIndisponivel.Text = "Dados de postagem não disponível";

                labelUnidadePostagem.Visible =
                labelMunicipioPostagem.Visible =
                labelCriacaoPostagem.Visible =
                labelCepDestinoPostagem.Visible =
                labelARPostagem.Visible =
                labelMPPostagem.Visible =
                labelDataMaxPrevistaEntregaPostagem.Visible = false;

                LblUnidadePostagem.Visible =
                LblMunicipioPostagem.Visible =
                LblCriacaoPostagem.Visible =
                LblCepDestinoPostagem.Visible =
                LblARPostagem.Visible =
                LblMPPostagem.Visible =
                LblDataMaxPrevistaEntregaPostagem.Visible = false;

                LblDadosTentativaEntregaIndisponivel.Visible = true;
                LblDadosTentativaEntregaIndisponivel.Text = "Dados de entrega não disponível";

                labelUnidadeLOEC.Visible =
                labelMunicipioLOEC.Visible =
                labelCriacaoLOEC.Visible =
                labelCarteiroLOEC.Visible =
                labelDistritoLOEC.Visible =
                labelNumeroLOEC.Visible =
                labelEnderecoLOEC.Visible =
                labelBairroLOEC.Visible =
                labelLocalidadeLOEC.Visible = false;

                LblUnidadeLOEC.Visible =
                LblMunicipioLOEC.Visible =
                LblCriacaoLOEC.Visible =
                LblCarteiroLOEC.Visible =
                LblDistritoLOEC.Visible =
                LblNumeroLOEC.Visible =
                LblEnderecoLOEC.Visible =
                LblBairroLOEC.Visible =
                LblLocalidadeLOEC.Visible = false;

                BtnCoordenadas.Visible = false;
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
            TxtPesquisa.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = false;
            if (e.KeyCode == (Keys.RButton | Keys.MButton | Keys.Space))
            {
                //para cima
                e.SuppressKeyPress = true;
                tabelaObjetosSROLocalBindingSource.MovePrevious();
            }
            if (e.KeyCode == (Keys.Back | Keys.Space))
            {
                //para baixo
                e.SuppressKeyPress = true;
                tabelaObjetosSROLocalBindingSource.MoveNext();
            }
            if (e.KeyCode == (Keys.LButton | Keys.MButton | Keys.Space))
            {
                //para esquerda
                dataGridView1.Focus();
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == (Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space))
            {
                //para direita
                dataGridView1.Focus();
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                //e.SuppressKeyPress = false;
                tabelaObjetosSROLocalBindingSource.MoveNext();
            }
        }

        private void tabelaObjetosSROLocalBindingSource_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            TxtPesquisa.Focus();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.ConsultaTodosNaoEntreguesOrdenadoNome();
        }

        private void FormularioConsulta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.L)
            {
                imprimirModeloLDIToolStripMenuItem_Click(sender, e);
            }
            if (e.Control && e.Shift && e.KeyCode == Keys.A)
            {
                imprimirAvisosDeChegadaSelecionadosToolStripMenuItem_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                TxtPesquisa.Text = string.Empty;
                TxtPesquisa.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnPesquisarSRO_Click(sender, e);
            }
            if (e.KeyCode == Keys.F2)
            {
                alterarItemToolStripMenuItem1_Click(sender, e);
            }
            if (e.KeyCode == Keys.F3)
            {

            }
            if (e.KeyCode == Keys.F4)
            {
                btnPesquisarSRO_Click(sender, e);
            }
            if (e.KeyCode == Keys.F5)
            {
                this.ConsultaTodosNaoEntreguesOrdenadoNome();
            }
            if (e.KeyCode == Keys.F6)
            {
                if (currentRow == null) return;

                foreach (Form item in MdiChildren)
                {
                    if (item.Name == "FormularioSRORastreamentoUnificado")
                    {
                        item.Close();
                        break;
                    }
                }

                FormularioSRORastreamentoUnificado formularioSRORastreamentoUnificado = new FormularioSRORastreamentoUnificado(currentRow["CodigoObjeto"].ToString());
                formularioSRORastreamentoUnificado.MdiParent = MdiParent;
                formularioSRORastreamentoUnificado.Show();
                formularioSRORastreamentoUnificado.WindowState = FormWindowState.Normal;
                formularioSRORastreamentoUnificado.WindowState = FormWindowState.Maximized;
                formularioSRORastreamentoUnificado.Activate();
            }
            if (e.KeyCode == Keys.F7)
            {
                FormularioHistoricoConsulta fm = new FormularioHistoricoConsulta();

                fm.ShowDialog();
                if (fm.CodigoRetorno == "")
                    return;

                TxtPesquisa.Text = "";
                TxtPesquisa.Text = fm.CodigoRetorno;
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

        private void btnPesquisarSRO_Click(object sender, EventArgs e)
        {
            LblCodigoForaDoPadraoBrasileiro.Visible = false;
            if (string.IsNullOrEmpty(TxtPesquisa.Text)) return;

            if (VerificaCodigoRastreamentoPadraoBrasileiro(TxtPesquisa.Text) == false)
            {
                LblCodigoForaDoPadraoBrasileiro.Visible = true;
                return;
            }

            //Mensagens.Informa("Antes de gravar o Código: " + TxtPesquisa.Text);
            GravaHistoricoConsulta(TxtPesquisa.Text);
            //Mensagens.Informa("Depois de gravar o Código: " + TxtPesquisa.Text);

            using (FormularioSRORastreamentoUnificado formularioSRORastreamentoUnificado = new FormularioSRORastreamentoUnificado(TxtPesquisa.Text))
            {
                formularioSRORastreamentoUnificado.WindowState = FormWindowState.Normal;
                formularioSRORastreamentoUnificado.StartPosition = FormStartPosition.CenterScreen;
                //formularioSRORastreamentoUnificado.Text = string.Format(@"SRO - Rastreamento Unificado - http://websro2.correiosnet.int/rastreamento/sro?opcao=PESQUISA&objetos={0}", TxtPesquisa.Text);
                formularioSRORastreamentoUnificado.Text = string.Format(@"SRO - Rastreamento Unificado - {0}{1}", Configuracoes.EnderecosSRO["EnderecoSROPorObjeto"].ToString(), TxtPesquisa.Text);
                formularioSRORastreamentoUnificado.ShowDialog();
            }
        }

        public bool VerificaCodigoRastreamentoPadraoBrasileiro(string TxtPesquisa)
        {
            if (TxtPesquisa.Length < 13)
            {
                return false;
            }

            for (int i = 0; i < TxtPesquisa.Length; i++)
            {
                //PrimeiroCaracter / SegundoCaracter / DecimoCaracter / DecimoPrimeiroCaracter nao pode ser número
                if (i == 0 || i == 1 || i == 11 || i == 12)
                {
                    //verifica se é letra. Não pode ser número
                    if (!System.Text.RegularExpressions.Regex.IsMatch(TxtPesquisa.Substring(i, 1), "^[0-9]")) continue;
                    else return false;
                }
                if (i >= 2 && i <= 10)
                {
                    //verifica se é número. Não pode ser Letra
                    if (System.Text.RegularExpressions.Regex.IsMatch(TxtPesquisa.Substring(i, 1), "^[0-9]")) continue;
                    else return false;
                }
            }

            return true;
        }

        private void GravaHistoricoConsulta(string codigoConsultado)
        {
            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                    List<Parametros> pr = new List<Parametros>() {
                            new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = codigoConsultado }
                        };

                    dao.ExecutaSQL("INSERT INTO TabelaHistoricoConsulta (CodigoObjeto) VALUES (@CodigoObjeto)", pr);
                }
            }
            catch (Exception EX)
            {
                Mensagens.Erro("Ocorreu um erro inesperado ao gravar o Histórico da Consulta. \nErro: " + EX.Message);
            }
        }

        private void tabelaObjetosSROLocalBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            #region Se for NULLO
            if (currentRow == null)
            {
                //FormularioPrincipal.RetornaComponentes().toolStripStatusLabel.Text = "";
                TxtItemSelecionado.Text = "";
                TxtCodigoObjetoSelecionado.Text = "";
                TxtCodigoLDISelecionado.Text = "";
                TxtDataLancamento.Text = "";
                TxtDataBaixa.Text = "";
                TxtSituacaoAtual.Text = "";
                TxtPrazoDias.Text = "";
                TxtDataVencimento.Text = "";

                LblDadosPostagemIndisponivel.Visible = true;
                LblDadosPostagemIndisponivel.Text = "Dados de postagem não disponível";

                labelUnidadePostagem.Visible =
                labelMunicipioPostagem.Visible =
                labelCriacaoPostagem.Visible =
                labelCepDestinoPostagem.Visible =
                labelARPostagem.Visible =
                labelMPPostagem.Visible =
                labelDataMaxPrevistaEntregaPostagem.Visible = false;

                LblUnidadePostagem.Visible =
                LblMunicipioPostagem.Visible =
                LblCriacaoPostagem.Visible =
                LblCepDestinoPostagem.Visible =
                LblARPostagem.Visible =
                LblMPPostagem.Visible =
                LblDataMaxPrevistaEntregaPostagem.Visible = false;




                LblDadosTentativaEntregaIndisponivel.Visible = true;
                LblDadosTentativaEntregaIndisponivel.Text = "Dados de entrega não disponível";

                labelUnidadeLOEC.Visible =
                labelMunicipioLOEC.Visible =
                labelCriacaoLOEC.Visible =
                labelCarteiroLOEC.Visible =
                labelDistritoLOEC.Visible =
                labelNumeroLOEC.Visible =
                labelEnderecoLOEC.Visible =
                labelBairroLOEC.Visible =
                labelLocalidadeLOEC.Visible = false;

                LblUnidadeLOEC.Visible =
                LblMunicipioLOEC.Visible =
                LblCriacaoLOEC.Visible =
                LblCarteiroLOEC.Visible =
                LblDistritoLOEC.Visible =
                LblNumeroLOEC.Visible =
                LblEnderecoLOEC.Visible =
                LblBairroLOEC.Visible =
                LblLocalidadeLOEC.Visible = false;

                BtnCoordenadas.Visible = false;

            }
            #endregion
            #region Se não for NULLO
            else
            {
                #region CodigoLdi
                TxtCodigoLDISelecionado.Text = currentRow["CodigoLdi"].ToString();
                #endregion

                #region DataLancamento
                TxtDataLancamento.Text = currentRow["DataLancamento"].ToString();
                #endregion

                #region CodigoObjetoFormatado
                string CodigoObjetoFormatado = string.Format("{0} {1} {2} {3} {4}",
                            currentRow["CodigoObjeto"].ToString().Substring(0, 2),
                            currentRow["CodigoObjeto"].ToString().Substring(2, 3),
                            currentRow["CodigoObjeto"].ToString().Substring(5, 3),
                            currentRow["CodigoObjeto"].ToString().Substring(8, 3),
                            currentRow["CodigoObjeto"].ToString().Substring(11, 2));
                #endregion

                #region CodigoObjeto
                TxtCodigoObjetoSelecionado.Text = currentRow["CodigoObjeto"].ToString();
                #endregion

                #region CodigoObjetoFormatado + NomeCliente
                //TxtItemSelecionado.Text = string.Format("{0} - {1}", CodigoObjetoFormatado, currentRow["NomeCliente"].ToString());
                TxtItemSelecionado.Text = string.Format("{0}", currentRow["NomeCliente"].ToString());
                #endregion

                #region Situacao
                TxtSituacaoAtual.Text = currentRow["Situacao"].ToString().Replace("NAO", "NÃO").Replace("DISTRIBUIDO", "DISTRIBUÍDO").Replace("DESTINATARIO", "DESTINATÁRIO");
                #endregion

                #region DataModificacao
                TxtDataBaixa.Text = currentRow["DataModificacao"].ToString();
                #endregion

                #region Prazo Dias
                string PrazoDestinoCaixaPostal = currentRow["TipoPostalPrazoDiasCorridosRegulamentado"].ToString();
                TxtPrazoDias.Text = PrazoDestinoCaixaPostal;
                #endregion

                #region DataVencimento
                if (string.IsNullOrEmpty(PrazoDestinoCaixaPostal))
                {
                    PrazoDestinoCaixaPostal = "0";
                    TxtDataVencimento.Text = "";
                    //drTipoPostal["PrazoDestinoCaixaPostal"].ToString();
                    //PrazoDestinoCaixaPostal = FormularioPrincipal.TiposPostais.AsEnumerable().First(T => T["Sigla"].Equals(currentRow["CodigoObjeto"].ToString().Substring(0, 2)))["PrazoDestinoCaixaPostal"].ToString();
                }
                else
                {
                    TxtDataVencimento.Text = currentRow["DataLancamento"].ToDateTime().Date.AddDays(Convert.ToDouble(PrazoDestinoCaixaPostal)).ToDateTime().ToShortDateString();
                }
                #endregion



                #region CoordenadasDestinatarioAusente
                if (string.IsNullOrEmpty(currentRow["CoordenadasDestinatarioAusente"].ToString()))
                {
                    BtnCoordenadas.Visible = false;
                }
                else
                {
                    BtnCoordenadas.Visible = true;
                }
                #endregion

                #region Dados de Postagem
                if (string.IsNullOrEmpty(currentRow["UnidadePostagem"].ToString()))
                {
                    LblDadosPostagemIndisponivel.Visible = true;
                    LblDadosPostagemIndisponivel.Text = "Dados de postagem não disponível";

                    labelUnidadePostagem.Visible =
                    labelMunicipioPostagem.Visible =
                    labelCriacaoPostagem.Visible =
                    labelCepDestinoPostagem.Visible =
                    labelARPostagem.Visible =
                    labelMPPostagem.Visible =
                    labelDataMaxPrevistaEntregaPostagem.Visible = false;

                    LblUnidadePostagem.Visible =
                    LblMunicipioPostagem.Visible =
                    LblCriacaoPostagem.Visible =
                    LblCepDestinoPostagem.Visible =
                    LblARPostagem.Visible =
                    LblMPPostagem.Visible =
                    LblDataMaxPrevistaEntregaPostagem.Visible = false;
                }
                else
                {
                    LblDadosPostagemIndisponivel.Visible = false;
                    LblDadosPostagemIndisponivel.Text = "";

                    labelUnidadePostagem.Visible =
                    labelMunicipioPostagem.Visible =
                    labelCriacaoPostagem.Visible =
                    labelCepDestinoPostagem.Visible =
                    labelARPostagem.Visible =
                    labelMPPostagem.Visible =
                    labelDataMaxPrevistaEntregaPostagem.Visible = true;

                    LblUnidadePostagem.Visible =
                    LblMunicipioPostagem.Visible =
                    LblCriacaoPostagem.Visible =
                    LblCepDestinoPostagem.Visible =
                    LblARPostagem.Visible =
                    LblMPPostagem.Visible =
                    LblDataMaxPrevistaEntregaPostagem.Visible = true;

                    LblUnidadePostagem.Text = currentRow["UnidadePostagem"].ToString();
                    LblMunicipioPostagem.Text = currentRow["MunicipioPostagem"].ToString();
                    LblCriacaoPostagem.Text = currentRow["CriacaoPostagem"].ToString();
                    LblCepDestinoPostagem.Text = currentRow["CepDestinoPostagem"].ToString();
                    LblARPostagem.Text = currentRow["ARPostagem"].ToString();
                    if (LblARPostagem.Text == "SIM") LblARPostagem.ForeColor = Color.Red;
                    else LblARPostagem.ForeColor = SystemColors.ControlText;
                    LblMPPostagem.Text = currentRow["MPPostagem"].ToString();
                    if (LblMPPostagem.Text == "SIM") LblMPPostagem.ForeColor = Color.Red;
                    else LblMPPostagem.ForeColor = SystemColors.ControlText;
                    LblDataMaxPrevistaEntregaPostagem.Text = currentRow["DataMaxPrevistaEntregaPostagem"].ToString() == "" ? "Dado indisponível" : currentRow["DataMaxPrevistaEntregaPostagem"].ToString();
                }
                #endregion

                #region Dados LOEC
                if (string.IsNullOrEmpty(currentRow["UnidadeLOEC"].ToString()))
                {
                    LblDadosTentativaEntregaIndisponivel.Visible = true;
                    LblDadosTentativaEntregaIndisponivel.Text = "Dados de entrega não disponível";

                    labelUnidadeLOEC.Visible =
                    labelMunicipioLOEC.Visible =
                    labelCriacaoLOEC.Visible =
                    labelCarteiroLOEC.Visible =
                    labelDistritoLOEC.Visible =
                    labelNumeroLOEC.Visible =
                    labelEnderecoLOEC.Visible =
                    labelBairroLOEC.Visible =
                    labelLocalidadeLOEC.Visible = false;

                    LblUnidadeLOEC.Visible =
                    LblMunicipioLOEC.Visible =
                    LblCriacaoLOEC.Visible =
                    LblCarteiroLOEC.Visible =
                    LblDistritoLOEC.Visible =
                    LblNumeroLOEC.Visible =
                    LblEnderecoLOEC.Visible =
                    LblBairroLOEC.Visible =
                    LblLocalidadeLOEC.Visible = false;
                }
                else
                {
                    LblDadosTentativaEntregaIndisponivel.Visible = false;
                    LblDadosTentativaEntregaIndisponivel.Text = "";

                    labelUnidadeLOEC.Visible =
                    labelMunicipioLOEC.Visible =
                    labelCriacaoLOEC.Visible =
                    labelCarteiroLOEC.Visible =
                    labelDistritoLOEC.Visible =
                    labelNumeroLOEC.Visible =
                    labelEnderecoLOEC.Visible =
                    labelBairroLOEC.Visible =
                    labelLocalidadeLOEC.Visible = true;

                    LblUnidadeLOEC.Visible =
                    LblMunicipioLOEC.Visible =
                    LblCriacaoLOEC.Visible =
                    LblCarteiroLOEC.Visible =
                    LblDistritoLOEC.Visible =
                    LblNumeroLOEC.Visible =
                    LblEnderecoLOEC.Visible =
                    LblBairroLOEC.Visible =
                    LblLocalidadeLOEC.Visible = true;

                    LblUnidadeLOEC.Text = currentRow["UnidadeLOEC"].ToString();
                    LblMunicipioLOEC.Text = currentRow["MunicipioLOEC"].ToString();
                    LblCriacaoLOEC.Text = currentRow["CriacaoLOEC"].ToString();
                    LblCarteiroLOEC.Text = currentRow["CarteiroLOEC"].ToString();
                    LblDistritoLOEC.Text = currentRow["DistritoLOEC"].ToString();
                    LblNumeroLOEC.Text = currentRow["NumeroLOEC"].ToString();
                    LblEnderecoLOEC.Text = currentRow["EnderecoLOEC"].ToString();
                    LblBairroLOEC.Text = currentRow["BairroLOEC"].ToString();
                    LblLocalidadeLOEC.Text = currentRow["LocalidadeLOEC"].ToString();
                }
                #endregion


                #region Carregamento GridView2

                string nomeCliente = string.IsNullOrEmpty(currentRow["NomeCliente"].ToString()) ? "" : currentRow["NomeCliente"].ToString();
                string enderecoLOEC = string.IsNullOrEmpty(currentRow["EnderecoLOEC"].ToString()) ? "" : currentRow["EnderecoLOEC"].ToString();
                string NomeMaisEndereco = string.Format("{0} {1}", nomeCliente, enderecoLOEC);

                if (!string.IsNullOrWhiteSpace(nomeCliente) && string.IsNullOrEmpty(enderecoLOEC))
                {
                    //vazio
                    splitContainer3.Panel2Collapsed = true;
                    bindingSource2.DataSource = null;

                    //não vazio
                    var Resultado = ((System.Windows.Forms.BindingSource)dataGridView1.DataSource).Cast<DataRowView>().Where(T =>
                    //(string.Format("{0} {1}", T["NomeCliente"], T["EnderecoLOEC"]).Contains(NomeMaisEndereco) && Convert.ToBoolean(T["ObjetoEntregue"]) == false));
                    (T["NomeCliente"].ToString().Contains(nomeCliente) && Convert.ToBoolean(T["ObjetoEntregue"]) == false));
                    //(nomeCliente.Contains(T["NomeCliente"].ToString()) && Convert.ToBoolean(T["ObjetoEntregue"]) == false));

                    groupBoxSemelhantes.Text = string.Format("Semelhantes - QTD.: {0}", Resultado.Count());
                    if (Resultado.Count() <= 1)
                    {
                        splitContainer3.Panel2Collapsed = true;
                        bindingSource2.DataSource = null;
                    }
                    if (Resultado.Count() > 1)
                    {
                        splitContainer3.Panel2Collapsed = false;
                        bindingSource2.DataSource = Resultado;
                    }
                }
                if (!string.IsNullOrWhiteSpace(nomeCliente) && !string.IsNullOrEmpty(enderecoLOEC))
                {
                    //não vazio

                    var Resultado = ((System.Windows.Forms.BindingSource)dataGridView1.DataSource).Cast<DataRowView>().Where(T =>
                    //(string.Format("{0} {1}", T["NomeCliente"], T["EnderecoLOEC"]).Contains(NomeMaisEndereco) && Convert.ToBoolean(T["ObjetoEntregue"]) == false) ||
                    (T["NomeCliente"].ToString().Contains(nomeCliente) && Convert.ToBoolean(T["ObjetoEntregue"]) == false) ||
                    //(nomeCliente.Contains(T["NomeCliente"].ToString()) && Convert.ToBoolean(T["ObjetoEntregue"]) == false) ||
                    (T["EnderecoLOEC"].ToString().Contains(enderecoLOEC) && Convert.ToBoolean(T["ObjetoEntregue"]) == false));

                    groupBoxSemelhantes.Text = string.Format("Semelhantes - QTD.: {0}", Resultado.Count());
                    if (Resultado.Count() <= 1)
                    {
                        splitContainer3.Panel2Collapsed = true;
                        bindingSource2.DataSource = null;
                    }
                    if (Resultado.Count() > 1)
                    {
                        splitContainer3.Panel2Collapsed = false;
                        bindingSource2.DataSource = Resultado;
                    }
                }
                #endregion
            }
            #endregion
        }

        public DataRow currentRow
        {
            get
            {
                int position = this.BindingContext[tabelaObjetosSROLocalBindingSource].Position;
                if (position > -1)
                {
                    return ((DataRowView)tabelaObjetosSROLocalBindingSource.Current).Row;
                }
                else
                {
                    return null;
                }
            }
        }

        private void FormularioConsulta_Activated(object sender, EventArgs e)
        {
            TxtPesquisa.Focus();
        }

        public static FormularioConsulta RetornaComponentesFormularioConsulta()
        {
            FormularioConsulta formularioConsulta;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioConsulta")
                {
                    formularioConsulta = (FormularioConsulta)item;
                    return (FormularioConsulta)item;
                }
            }
            return null;
        }

        WaitWndFun waitForm = new WaitWndFun();
        public void ConsultaTodosNaoEntreguesOrdenadoNome()
        {
            try
            {
                int posicao = tabelaObjetosSROLocalBindingSource.Position;
                if (!DAO.TestaConexao(ClassesDiversas.Configuracoes.strConexao, TipoBanco.OleDb))
                {
                    FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao;
                    return;
                }
                dataInicial = Convert.ToDateTime(DataInicial_dateTimePicker.Text).ToString("yyyy/MM/dd");
                datafinal = Convert.ToDateTime(DataFinal_dateTimePicker.Text).ToString("yyyy/MM/dd");


                waitForm.Show(this);
                this.tabelaObjetosSROLocalTableAdapter.Connection.ConnectionString = ClassesDiversas.Configuracoes.strConexao;

                //string commandText = @"SELECT Codigo, CodigoObjeto, IIf(CodigoLdi IS NULL OR CodigoLdi = '','000000000000',CodigoLdi) AS CodigoLdi, NomeCliente, Format(IIf(TabelaObjetosSROLocal.DataLancamento IS NULL OR TabelaObjetosSROLocal.DataLancamento = '', Format(NOW(),'dd/MM/yyyy 00:00:01'), TabelaObjetosSROLocal.DataLancamento), 'dd/MM/yyyy hh:mm:ss') AS DataLancamento, Format(DataModificacao, 'dd/MM/yyyy hh:mm:ss') AS DataModificacao, Situacao, Atualizado, ObjetoEntregue, CaixaPostal, UnidadePostagem, MunicipioPostagem, CriacaoPostagem, CepDestinoPostagem, ARPostagem, MPPostagem, DataMaxPrevistaEntregaPostagem, UnidadeLOEC, MunicipioLOEC, CriacaoLOEC, CarteiroLOEC, DistritoLOEC, NumeroLOEC, EnderecoLOEC, BairroLOEC, LocalidadeLOEC, SituacaoDestinatarioAusente, AgrupadoDestinatarioAusente, CoordenadasDestinatarioAusente, Comentario, TipoPostalServico, TipoPostalSiglaCodigo, TipoPostalNomeSiglaCodigo, TipoPostalPrazoDiasCorridosRegulamentado FROM            TabelaObjetosSROLocal WHERE        (Format(DataLancamento, 'yyyy/MM/dd') BETWEEN Format(?, 'yyyy/MM/dd') AND Format(?, 'yyyy/MM/dd')) ORDER BY DataLancamento DESC";
                //this.tabelaObjetosSROLocalTableAdapter.Adapter.SelectCommand =  new System.Data.OleDb.OleDbCommand(commandText);

                this.tabelaObjetosSROLocalTableAdapter.Fill(this.dataSetTabelaObjetosSROLocal.TabelaObjetosSROLocal, dataInicial, datafinal);
                this.MontaFiltro();
                waitForm.Close();

                #region 1 tentativa nao deu certo
                ////BindingSource bs2 = new BindingSource();
                //object dataSourceGridView1 = dataGridView1.DataSource;
                //bindingSource2.DataSource = dataSourceGridView1;
                ////dataGridView2.DataSource = bindingSource2;

                //foreach (DataGridViewColumn item in dataGridView2.Columns)
                //{
                //    if (item.Name == "dataGridViewTextBoxColumnCodigoObjeto" ||
                //       item.Name == "dataGridViewTextBoxColumnNomeCliente" ||
                //       item.Name == "dataGridViewTextBoxColumnEnderecoLOEC")
                //    {
                //        continue;
                //    }
                //    else
                //    {
                //        item.Visible = false;
                //    }
                //}
                //bindingSource2.Filter = "EnderecoLOEC = '804 SUL ALAMEDA 6 20'";
                ////EnderecoLOEC = '804 SUL ALAMEDA 6 20' 
                #endregion

                //foreach (DataGridViewColumn item in dataGridView2.Columns)
                //{
                //    if (item.Name == "dataGridViewTextBoxColumnCodigoObjeto" ||
                //       item.Name == "dataGridViewTextBoxColumnNomeCliente" ||
                //       item.Name == "dataGridViewTextBoxColumnEnderecoLOEC")
                //    {
                //        continue;
                //    }
                //    else
                //    {
                //        item.Visible = false;
                //    }
                //}
                //dataGridView2.DataSource = dataGridView1.DataSource.

                //var tbl = ((System.Windows.Forms.BindingSource)dataGridView1.DataSource).Cast<DataRowView>().Where(T => T["EnderecoLOEC"].ToString().Contains("804 SUL ALAMEDA 6 20"));//.AsEnumerable().Where(T => T.Rows .ToString().Contains("804 SUL ALAMEDA 6 20"));
                //bindingSource2.DataSource = tbl;













                tabelaObjetosSROLocalBindingSource.Position = posicao;
                dataGridView1.Focus();
            }
            catch (Exception ex)
            {
                foreach (Form item in Application.OpenForms)
                {
                    if (item.Name == "WaitForm")
                    {
                        waitForm.Close();
                        break;
                    }
                }
                Mensagens.Erro(ex.Message);
            }
        }

        public void AlterarDataAoIniciarODIa()
        {
            if (DateTime.Now.Date.ToShortDateString().ToDateTime().Date > DataFinal_dateTimePicker.Text.ToDateTime().Date)
            {
                //DataFinal_dateTimePicker.Text = DateTime.Now.Date.ToShortDateString();
            }
        }

        private void MarcarComoEntregueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentRow == null) return;
            if (currentRow["ObjetoEntregue"].ToInt() == 1)
            {
                //"Desmarcar...."
                currentRow["ObjetoEntregue"] = 0;
            }
            else
            {
                //"Marcar...."
                currentRow["ObjetoEntregue"] = 1;
            }

            if (!DAO.TestaConexao(ClassesDiversas.Configuracoes.strConexao, TipoBanco.OleDb))
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao;
                return;
            }
            this.tabelaObjetosSROLocalTableAdapter.Connection.ConnectionString = ClassesDiversas.Configuracoes.strConexao;
            this.tabelaObjetosSROLocalTableAdapter.Update(dataSetTabelaObjetosSROLocal.TabelaObjetosSROLocal);
        }

        public void MarcarSelecionadosComoAtualizado()
        {
            //using (FormWaiting frm = new FormWaiting(ProcessandoMarcarSelecionadosComoAtualizado))
            //{
            //    frm.ShowDialog(this);
            //}
            ProcessandoMarcarSelecionadosComoAtualizado();
        }

        void ProcessandoMarcarSelecionadosComoAtualizado()
        {
            waitForm.Show(this);
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    //var atualizado = dataGridView1.SelectedRows[i].Cells["atualizadoDataGridViewCheckBoxColumn"].Value;
                    string CodigoObjeto = this.dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                    List<Parametros> pr = new List<Parametros>() {
                            new Parametros() { Nome = "@Atualizado", Tipo = TipoCampo.Boolean, Valor = true },
                            new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjeto }
                        };
                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Atualizado = @Atualizado  WHERE (CodigoObjeto = @CodigoObjeto)", pr);
                    this.dataGridView1.SelectedRows[i].Cells["atualizadoDataGridViewCheckBoxColumn"].Value = true;
                }
            }
            waitForm.Close();
        }

        public void ProcessandoMarcarSelecionadosComoNaoAtualizado()
        {
            waitForm.Show(this);
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    //var atualizado = dataGridView1.SelectedRows[i].Cells["atualizadoDataGridViewCheckBoxColumn"].Value;
                    string CodigoObjeto = this.dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                    List<Parametros> pr = new List<Parametros>() {
                            new Parametros() { Nome = "@Atualizado", Tipo = TipoCampo.Boolean, Valor = false },
                            new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjeto }
                        };
                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Atualizado = @Atualizado  WHERE (CodigoObjeto = @CodigoObjeto)", pr);
                    this.dataGridView1.SelectedRows[i].Cells["atualizadoDataGridViewCheckBoxColumn"].Value = false;
                }
            }
            waitForm.Close();
        }

        private void MarcarAtualizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarcarSelecionadosComoAtualizado();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (currentRow == null) return;
            if (currentRow["ObjetoEntregue"].ToInt() == 1)
            {
                MarcarComoEntregueToolStripMenuItem.Text = "Desmarcar objeto já entregue";
            }
            else
            {
                MarcarComoEntregueToolStripMenuItem.Text = "Marcar objeto já entregue";
            }
        }

        private void toolStripMenuItemImprimirListaEntrega_Click(object sender, EventArgs e)
        {
            //FormularioPrincipal.RetornaComponentesFormularioPrincipal().imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem_Click(sender, e);
        }

        private string ultimoCodigoDetalhado = string.Empty;
        private void BtnDetalharObjetosSelecionado_Click(object sender, EventArgs e)
        {
            if (currentRow == null) return;

            bool estaAberto = false;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioSRORastreamentoUnificado")
                {
                    if (item.Text.Contains("Rastreamento Unificado Detalhado"))
                    {
                        if (ultimoCodigoDetalhado == currentRow["CodigoObjeto"].ToString())
                        {
                            estaAberto = true;
                            item.Activate();
                            break;
                        }
                        else
                        {
                            estaAberto = false;
                            item.Close();
                            break;
                        }
                    }
                }
            }

            if (estaAberto == true) return;

            //ultimoCodigoDetalhado = currentRow["CodigoObjeto"].ToString();
            //FormularioSRORastreamentoUnificado formularioSRORastreamentoUnificado = new FormularioSRORastreamentoUnificado(ultimoCodigoDetalhado);
            //formularioSRORastreamentoUnificado.MdiParent = MdiParent;
            //formularioSRORastreamentoUnificado.Text = string.Format(@"SRO - Rastreamento Unificado Detalhado [{0}]", ultimoCodigoDetalhado);
            //formularioSRORastreamentoUnificado.Show();
            //formularioSRORastreamentoUnificado.WindowState = FormWindowState.Maximized;
            //formularioSRORastreamentoUnificado.Activate();

            ultimoCodigoDetalhado = currentRow["CodigoObjeto"].ToString();
            using (FormularioSRORastreamentoUnificado formularioSRORastreamentoUnificado = new FormularioSRORastreamentoUnificado(ultimoCodigoDetalhado))
            {
                formularioSRORastreamentoUnificado.WindowState = FormWindowState.Normal;
                formularioSRORastreamentoUnificado.StartPosition = FormStartPosition.CenterScreen;
                //formularioSRORastreamentoUnificado.Text = string.Format(@"SRO - Rastreamento Unificado - http://websro2.correiosnet.int/rastreamento/sro?opcao=PESQUISA&objetos={0}", ultimoCodigoDetalhado);
                formularioSRORastreamentoUnificado.Text = string.Format(@"SRO - Rastreamento Unificado Detalhado [{0}]", ultimoCodigoDetalhado);
                formularioSRORastreamentoUnificado.ShowDialog();
            }
        }

        private void ChkIncluirItensEntreguesNaPesquisa_CheckedChanged(object sender, EventArgs e)
        {
            //FormularioPrincipal.RetornaComponentes().ExibirItensJaEntreguesToolStripMenuItem.Checked = ChkIncluirItensEntreguesNaPesquisa.Checked;
            //FormularioPrincipal.RetornaComponentes().ExibirItensJaEntreguesToolStripMenuItem_Click(sender, e);
        }

        private void ChkIncluirItensCaixaPostalNaPesquisa_CheckedChanged(object sender, EventArgs e)
        {
            //FormularioPrincipal.RetornaComponentes().ExibirCaixaPostalPesquisa_toolStripMenuItem.Checked = ChkIncluirItensEntreguesNaPesquisa.Checked;
            //FormularioPrincipal.RetornaComponentes().ExibirCaixaPostalPesquisa_toolStripMenuItem_Click(sender, e);
        }


        public void removerItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0) return;

            DialogResult resposta = Mensagens.Pergunta("Realmente deseja remover itens selecionados?", MessageBoxButtons.YesNoCancel);
            if (resposta == DialogResult.No || resposta == DialogResult.Cancel)
            {
                return;
            }

            if (!DAO.TestaConexao(ClassesDiversas.Configuracoes.strConexao, TipoBanco.OleDb))
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao;
                return;
            }

            int position = this.BindingContext[tabelaObjetosSROLocalBindingSource].Position - this.dataGridView1.SelectedRows.Count + 1;

            waitForm.Show(this);
            for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                string CodigoObjeto = this.dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                string Nome = this.dataGridView1.SelectedRows[i].Cells[2].Value.ToString();
                //int teste = currentRow["Codigo"].ToInt();
                this.tabelaObjetosSROLocalTableAdapter.Connection.ConnectionString = ClassesDiversas.Configuracoes.strConexao;
                this.tabelaObjetosSROLocalTableAdapter.Delete(CodigoObjeto);
            }
            waitForm.Close();

            if (position > -1)
            {
                this.BindingContext[tabelaObjetosSROLocalBindingSource].Position = position;
            }

            FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();

            FormularioConsulta_Activated(sender, e);
        }

        Dictionary<string, string> dicionarioCodigo_Nome = new Dictionary<string, string>();
        Dictionary<string, DateTime> dicionarioCodigo_DataLancamento = new Dictionary<string, DateTime>();
        public void GeraImpressaoItensSelecionados(ModeloImpressaoListaObjetos _modeloImpressaoListaObjetos)
        {
            dicionarioCodigo_Nome = new Dictionary<string, string>();
            dicionarioCodigo_DataLancamento = new Dictionary<string, DateTime>();
            List<string> novosCodigosSelecionadosOrdenados = new List<string>();

            if (dataGridView1.SelectedRows.Count == 0)
            {
                Mensagens.Informa("Para impressão da lista de entrega é necessário selecionar algum objeto.");
                return;
            }
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioImpressaoEntregaObjetosModelo1" ||
                    item.Name == "FormularioImpressaoEntregaObjetosModelo2")
                {
                    item.Close();
                    break;
                }
            }

            if (_modeloImpressaoListaObjetos == ModeloImpressaoListaObjetos.ModeloLDI)
            {
                if (this.dataGridView1.SelectedRows.Count == 1)
                {
                    FormularioPrincipal.OpcoesImpressaoOrdenacaoPorNomeDestinatario = true;
                    FormularioPrincipal.OpcoesImpressaoOrdenacaoPorDataLancamento = false;
                    FormularioPrincipal.OpcoesImpressaoOrdenacaoPorOrdemCrescente = true;
                    FormularioPrincipal.OpcoesImpressaoImprimirUmPorFolha = true;
                    FormularioPrincipal.OpcoesImpressaoImprimirVariosPorFolha = false;
                }
                if (this.dataGridView1.SelectedRows.Count > 1)
                {
                    using (FormularioImpressaoEntregaObjetosOpcoesImpressao2 formularioImpressaoEntregaObjetosOpcoesImpressao = new FormularioImpressaoEntregaObjetosOpcoesImpressao2(_modeloImpressaoListaObjetos))
                    {
                        formularioImpressaoEntregaObjetosOpcoesImpressao.ShowDialog();
                        if (formularioImpressaoEntregaObjetosOpcoesImpressao.Cancelou == true) return;

                        FormularioPrincipal.OpcoesImpressaoOrdenacaoPorNomeDestinatario = formularioImpressaoEntregaObjetosOpcoesImpressao.OrdenacaoPorNomeDestinatario;
                        FormularioPrincipal.OpcoesImpressaoOrdenacaoPorDataLancamento = formularioImpressaoEntregaObjetosOpcoesImpressao.OrdenacaoPorDataLancamento;
                        FormularioPrincipal.OpcoesImpressaoOrdenacaoPorOrdemCrescente = formularioImpressaoEntregaObjetosOpcoesImpressao.OrdenacaoPorOrdemCrescente;
                        FormularioPrincipal.OpcoesImpressaoImprimirUmPorFolha = formularioImpressaoEntregaObjetosOpcoesImpressao.ImprimirUmPorFolha;
                        FormularioPrincipal.OpcoesImpressaoImprimirVariosPorFolha = formularioImpressaoEntregaObjetosOpcoesImpressao.ImprimirVariosPorFolha;
                    }
                }
            }
            if (_modeloImpressaoListaObjetos == ModeloImpressaoListaObjetos.ModeloComum)
            {
                //não vai mostrar a tela de opções
                FormularioPrincipal.OpcoesImpressaoOrdenacaoPorNomeDestinatario = true;
                FormularioPrincipal.OpcoesImpressaoOrdenacaoPorDataLancamento = false;
                FormularioPrincipal.OpcoesImpressaoOrdenacaoPorOrdemCrescente = true;
                FormularioPrincipal.OpcoesImpressaoImprimirUmPorFolha = false;
                FormularioPrincipal.OpcoesImpressaoImprimirVariosPorFolha = true;
            }

            #region Ordem alfabética
            if (FormularioPrincipal.OpcoesImpressaoOrdenacaoPorNomeDestinatario)
            {
                for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    //codigoObjetoDataGridViewTextBoxColumn
                    //nomeClienteDataGridViewTextBoxColumn
                    //DataLancamento
                    bool existe = dicionarioCodigo_Nome.AsEnumerable().Any(t => t.Key == this.dataGridView1.SelectedRows[i].Cells["codigoObjetoDataGridViewTextBoxColumn"].Value.ToString());
                    if (!existe)
                        dicionarioCodigo_Nome.Add(this.dataGridView1.SelectedRows[i].Cells["codigoObjetoDataGridViewTextBoxColumn"].Value.ToString(), this.dataGridView1.SelectedRows[i].Cells["nomeClienteDataGridViewTextBoxColumn"].Value.ToString());
                }
                if (FormularioPrincipal.OpcoesImpressaoOrdenacaoPorOrdemCrescente)
                    novosCodigosSelecionadosOrdenados = dicionarioCodigo_Nome.AsEnumerable().OrderBy(t => t.Value).Select(c => c.Key).ToList();
                else
                    novosCodigosSelecionadosOrdenados = dicionarioCodigo_Nome.AsEnumerable().OrderByDescending(t => t.Value).Select(c => c.Key).ToList();
            }
            #endregion

            #region Ordem de Lançamento
            if (FormularioPrincipal.OpcoesImpressaoOrdenacaoPorDataLancamento)
            {
                for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    //codigoObjetoDataGridViewTextBoxColumn
                    //nomeClienteDataGridViewTextBoxColumn
                    //DataLancamento
                    bool existe = dicionarioCodigo_DataLancamento.AsEnumerable().Any(t => t.Key == this.dataGridView1.SelectedRows[i].Cells["codigoObjetoDataGridViewTextBoxColumn"].Value.ToString());
                    if (!existe)
                        dicionarioCodigo_DataLancamento.Add(this.dataGridView1.SelectedRows[i].Cells["codigoObjetoDataGridViewTextBoxColumn"].Value.ToString(), this.dataGridView1.SelectedRows[i].Cells["DataLancamento"].Value.ToDateTime());
                }
                if (FormularioPrincipal.OpcoesImpressaoOrdenacaoPorOrdemCrescente)
                    novosCodigosSelecionadosOrdenados = dicionarioCodigo_DataLancamento.AsEnumerable().OrderBy(t => t.Value).Select(c => c.Key).ToList();
                else
                    novosCodigosSelecionadosOrdenados = dicionarioCodigo_DataLancamento.AsEnumerable().OrderByDescending(t => t.Value).Select(c => c.Key).ToList();
            }
            #endregion


            if (_modeloImpressaoListaObjetos == ModeloImpressaoListaObjetos.ModeloLDI)
            {
                FormularioImpressaoEntregaObjetosModelo2 formularioImpressaoEntregaObjetos = new FormularioImpressaoEntregaObjetosModelo2(novosCodigosSelecionadosOrdenados, FormularioPrincipal.OpcoesImpressaoImprimirUmPorFolha, FormularioPrincipal.OpcoesImpressaoImprimirVariosPorFolha);
                //formularioImpressaoEntregaObjetos.MdiParent = MdiParent;
                //formularioImpressaoEntregaObjetos.Show();
                //formularioImpressaoEntregaObjetos.WindowState = FormWindowState.Maximized;
                //formularioImpressaoEntregaObjetos.Activate();
            }
            if (_modeloImpressaoListaObjetos == ModeloImpressaoListaObjetos.ModeloComum)
            {
                FormularioImpressaoEntregaObjetosModelo1 formularioImpressaoEntregaObjetos = new FormularioImpressaoEntregaObjetosModelo1(novosCodigosSelecionadosOrdenados);
                //formularioImpressaoEntregaObjetos.MdiParent = MdiParent;
                //formularioImpressaoEntregaObjetos.Show();
                //formularioImpressaoEntregaObjetos.WindowState = FormWindowState.Maximized;
                //formularioImpressaoEntregaObjetos.Activate();
            }
        }

        public void GeraAvisosDeChegadaSelecionados()
        {
            List<string> novosCodigosSelecionadosOrdenados = new List<string>();
            dicionarioCodigo_DataLancamento = new Dictionary<string, DateTime>();
            if (dataGridView1.SelectedRows.Count == 0)
            {
                Mensagens.Informa("Para impressão da lista de entrega é necessário selecionar algum objeto.");
                return;
            }

            //Ordem de Lançamento
            for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                //codigoObjetoDataGridViewTextBoxColumn
                //nomeClienteDataGridViewTextBoxColumn
                //DataLancamento
                bool existe = dicionarioCodigo_DataLancamento.AsEnumerable().Any(t => t.Key == this.dataGridView1.SelectedRows[i].Cells["codigoObjetoDataGridViewTextBoxColumn"].Value.ToString());
                if (!existe)
                    dicionarioCodigo_DataLancamento.Add(this.dataGridView1.SelectedRows[i].Cells["codigoObjetoDataGridViewTextBoxColumn"].Value.ToString(), this.dataGridView1.SelectedRows[i].Cells["DataLancamento"].Value.ToDateTime());
            }
            FormularioPrincipal.OpcoesImpressaoOrdenacaoPorOrdemCrescente = true;
            if (FormularioPrincipal.OpcoesImpressaoOrdenacaoPorOrdemCrescente)
            {
                //novosCodigosSelecionadosOrdenados = dicionarioCodigo_DataLancamento.AsEnumerable().OrderBy(t => t.Value).Select(c => c.Key).ToList();
                novosCodigosSelecionadosOrdenados = dicionarioCodigo_DataLancamento.AsEnumerable().Select(c => c.Key).ToList();
            }
            else
            {
                novosCodigosSelecionadosOrdenados = dicionarioCodigo_DataLancamento.AsEnumerable().OrderByDescending(t => t.Value).Select(c => c.Key).ToList();
            }

            FormularioImpressaoAvisosChegada FormularioImpressaoAvisosChegada = new FormularioImpressaoAvisosChegada(novosCodigosSelecionadosOrdenados);
        }

        public void GeraImpressaoItensLancadosNoDiaHoje(bool _incluirItensEntregues, bool _incluirItensCaixaPostal)
        {
            //string valorAtualTXTCampoPesquisa = TxtPesquisa.Text;
            TxtPesquisa.Text = string.Empty;

            string dataInicial = DateTime.Now.Date.ToString("yyyy/MM/dd");
            string datafinal = DateTime.Now.Date.ToString("yyyy/MM/dd");

            string objetoCaixaPostalSelecionado = "";
            string objetoEntregueSelecionado = "";

            bool IncluirItensEntregues = _incluirItensEntregues;
            bool IncluirItensCaixaPostal = _incluirItensCaixaPostal;


            if (!IncluirItensEntregues)
            {
                objetoEntregueSelecionado = string.Format(" AND ObjetoEntregue = {0} ", false);

                //if (TxtPesquisa.Text == "")
                //    objetoEntregueSelecionado = objetoEntregueSelecionado.Replace(" AND ", "");
                objetoEntregueSelecionado = objetoEntregueSelecionado.Replace(" AND ", "");

                if (!IncluirItensCaixaPostal)
                {
                    objetoCaixaPostalSelecionado = string.Format(" AND CaixaPostal = {0} ", false);
                }
            }
            else
            {
                if (!IncluirItensCaixaPostal)
                {
                    objetoCaixaPostalSelecionado = string.Format(" AND CaixaPostal = {0} ", false);

                    if (TxtPesquisa.Text == "")
                        objetoCaixaPostalSelecionado = objetoCaixaPostalSelecionado.Replace(" AND CaixaPostal ", " CaixaPostal ");
                }
            }

            string NovoFiltro = string.Format("{0}{1}", objetoEntregueSelecionado, objetoCaixaPostalSelecionado);

            if (!DAO.TestaConexao(ClassesDiversas.Configuracoes.strConexao, TipoBanco.OleDb))
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao;
                return;
            }
            DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao);

            List<Parametros> Pr = new List<Parametros>()
            {
                    new Parametros() { Nome = "@DataInicial", Tipo = TipoCampo.Text, Valor = dataInicial }
                    ,new Parametros() { Nome = "@DataFinal", Tipo = TipoCampo.Text, Valor = datafinal }
            };

            DataSet ds = dao.RetornaDataSet(@"SELECT Codigo, CodigoObjeto, NomeCliente, CaixaPostal, ObjetoEntregue 
                                                FROM TabelaObjetosSROLocal 
                                                WHERE 
            (Format(DataLancamento, 'yyyy/MM/dd') BETWEEN Format(@DataInicial, 'yyyy/MM/dd') AND Format(@DataFinal, 'yyyy/MM/dd'))
            " + string.Format("{0}", NovoFiltro == "" ? "" : "AND " + NovoFiltro)
            + " ORDER BY NomeCliente", Pr);

            List<string> ListaCodigoOrdenadosPeloNomeCliente = ds.Tables[0].AsEnumerable()
                //Where(m => Convert.ToBoolean(m["CaixaPostal"]) == IncluirItensCaixaPostal)
                //.Where(m => Convert.ToBoolean(m["ObjetoEntregue"]) == IncluirObjetoEntregue)
                .OrderBy(t => t.Table.Columns["NomeCliente"]).Select(c => c["CodigoObjeto"].ToString()).ToList();
            dao.Dispose();

            if (ListaCodigoOrdenadosPeloNomeCliente.Count == 0)
            {
                Mensagens.Informa("Não foi encontrado lançamento para o dia [" + DateTime.Now.GetDateTimeFormats()[14] + "].");
                return;
            }
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = "Aguarde enquanto carrega o relatório de impressão.";

            //if (Application.OpenForms["FormularioImpressaoEntregaObjetos"] != null) Application.OpenForms["FormularioImpressaoEntregaObjetos"].Close();
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioImpressaoEntregaObjetos")
                {
                    item.Close();
                    break;
                }
            }
            FormularioImpressaoEntregaObjetosModelo1 formularioImpressaoEntregaObjetos = new FormularioImpressaoEntregaObjetosModelo1(ListaCodigoOrdenadosPeloNomeCliente);
            formularioImpressaoEntregaObjetos.MdiParent = MdiParent;
            formularioImpressaoEntregaObjetos.Show();
            //formularioImpressaoEntregaObjetos.WindowState = FormWindowState.Normal;
            formularioImpressaoEntregaObjetos.WindowState = FormWindowState.Maximized;
            formularioImpressaoEntregaObjetos.Activate();
        }

        private void DataInicial_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            //this.ConsultaTodosNaoEntreguesOrdenadoNome();
        }

        private void DataFinal_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            //this.ConsultaTodosNaoEntreguesOrdenadoNome();
        }

        private void DataInicial_dateTimePicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //e.SuppressKeyPress = true;
                //DataFinal_dateTimePicker.Focus();
                SendKeys.Send("{TAB}");
            }
        }

        private void DataFinal_dateTimePicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //e.SuppressKeyPress = true;
                //btnPesquisarSRO.Focus();
                SendKeys.Send("{TAB}");
            }
        }

        public void alterarItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlterarItem(sender, e);
        }

        private void alterarItemToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AlterarItem(sender, e);
        }

        private void AlterarItem(object sender, EventArgs e)
        {
            try
            {
                if (!DAO.TestaConexao(ClassesDiversas.Configuracoes.strConexao, TipoBanco.OleDb))
                {
                    FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao;
                    return;
                }
                if (currentRow == null) return;
                if (dataGridView1.SelectedRows.Count == 0) return;

                string CodigoObjetoSelecionado = "";
                for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    CodigoObjetoSelecionado = this.dataGridView1.SelectedRows[i].Cells["codigoObjetoDataGridViewTextBoxColumn"].Value.ToString();
                    break;
                }

                FormularioAlteracaoObjeto frm = new FormularioAlteracaoObjeto() { CodigoObjeto = CodigoObjetoSelecionado };
                frm.ShowDialog();

                if (frm.Cancelando) return;

                currentRow["CodigoObjeto"] = frm.CodigoObjeto;//string CodigoObjeto,
                currentRow["NomeCliente"] = string.Format("{0} - {1}", frm.NomeCliente.ToUpper().RemoveAcentos(), frm.Comentario);//string NomeCliente,
                currentRow["CodigoLdi"] = frm.NumeroLDI;//string CodigoLdi,
                currentRow["DataLancamento"] = frm.DataLancamento;
                currentRow["Situacao"] = frm.Situacao;//string Situacao,
                currentRow["DataModificacao"] = frm.DataModificacao;
                currentRow["CaixaPostal"] = frm.ObjetoEmCaixaPostal;//bool CaixaPostal,
                currentRow["ObjetoEntregue"] = frm.ObjetoJaEntregue;//bool ObjetoEntregue,
                currentRow["Atualizado"] = frm.ObjetoJaAtualizado;//bool Atualizado,
                currentRow["Comentario"] = frm.Comentario.ToUpper().RemoveAcentos();//string Comentario,

                //string UnidadePostagem,
                //string MunicipioPostagem,
                //string CriacaoPostagem,
                currentRow["CepDestinoPostagem"] = frm.LocalidadeLOEC;//string CepDestinoPostagem,
                //string ARPostagem,
                //string MPPostagem,
                //string DataMaxPrevistaEntregaPostagem,

                //string UnidadeLOEC,
                currentRow["MunicipioLOEC"] = frm.MunicipioLOEC;//string MunicipioLOEC, 
                //string CriacaoLOEC,
                //string CarteiroLOEC,
                //string DistritoLOEC,
                //string NumeroLOEC,
                currentRow["EnderecoLOEC"] = frm.EnderecoLOEC;//string EnderecoLOEC,
                currentRow["BairroLOEC"] = frm.BairroLOEC;//string BairroLOEC,
                currentRow["LocalidadeLOEC"] = frm.LocalidadeLOEC;//string LocalidadeLOEC,

                //string SituacaoDestinatarioAusente,
                //string AgrupadoDestinatarioAusente,
                //string CoordenadasDestinatarioAusente

                bool SeEAoRemetente = frm.checkBoxAoRemetente.Checked;

                string TipoPostalServico = string.Empty;
                string TipoPostalSiglaCodigo = string.Empty;
                string TipoPostalNomeSiglaCodigo = string.Empty;
                string TipoPostalPrazoDiasCorridosRegulamentado = string.Empty;

                TipoPostalPrazoDiasCorridosRegulamentado = Configuracoes.RetornaTipoPostalPrazoDiasCorridosRegulamentado(CodigoObjetoSelecionado, SeEAoRemetente, frm.ObjetoEmCaixaPostal, ref TipoPostalServico, ref TipoPostalSiglaCodigo, ref TipoPostalNomeSiglaCodigo);
                if (string.IsNullOrEmpty(TipoPostalPrazoDiasCorridosRegulamentado))
                {
                    Mensagens.Erro(string.Format("Não foi encontrado o Tipo Postal [ {0} ].\nUma gestão de tipos postais é necessário.", CodigoObjetoSelecionado.Substring(0, 2)));
                    //continua mesmo não tendo o tipo postal desejado....
                }

                currentRow["TipoPostalServico"] = TipoPostalServico;//string TipoPostalServico,
                currentRow["TipoPostalSiglaCodigo"] = TipoPostalSiglaCodigo;//string TipoPostalSiglaCodigo,
                currentRow["TipoPostalNomeSiglaCodigo"] = TipoPostalNomeSiglaCodigo;//string TipoPostalNomeSiglaCodigo,
                currentRow["TipoPostalPrazoDiasCorridosRegulamentado"] = TipoPostalPrazoDiasCorridosRegulamentado;//string TipoPostalPrazoDiasCorridosRegulamentado,

                int position = this.BindingContext[tabelaObjetosSROLocalBindingSource].Position;
                if (position > -1) this.BindingContext[tabelaObjetosSROLocalBindingSource].Position = position;

                FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();

                FormularioConsulta_Activated(sender, e);
            }
            catch (Exception ex)
            {
                Mensagens.Erro("Ocorreu um erro inesperado.\nErro: " + ex);
                //throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FormularioHistoricoConsulta fm = new FormularioHistoricoConsulta())
            {
                fm.ShowDialog();
            }
        }

        private void linkLabelHistoricoConsulta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormularioHistoricoConsulta fm = new FormularioHistoricoConsulta();

            fm.ShowDialog();
            if (fm.CodigoRetorno == "")
                return;

            TxtPesquisa.Text = "";
            TxtPesquisa.Text = fm.CodigoRetorno;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            TxtPesquisa.Text = "";
            TxtPesquisa.SelectAll();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //alterarItemToolStripMenuItem_Click(sender, e);
            BtnDetalharObjetosSelecionado_Click(sender, e);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TxtPesquisa.Text = "";
            TxtPesquisa.Focus();
        }

        private void checkBoxSomenteHoje_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSomenteHoje.Checked == true)
            {
                //checkBoxSomenteHoje.Text = "Voltar para mês anterior";
                DataInicial_dateTimePicker.Text = DateTime.Now.Date.ToShortDateString();
                DataFinal_dateTimePicker.Text = DateTime.Now.Date.ToShortDateString();
                DataInicial_dateTimePicker.Enabled = DataFinal_dateTimePicker.Enabled = false;
                labelDataInicial.Enabled = labelDataFinal.Enabled = false;
            }
            else
            {
                //checkBoxSomenteHoje.Text = "Lançados hoje";
                labelDataInicial.Enabled = labelDataFinal.Enabled = true;
                DataInicial_dateTimePicker.Enabled = DataFinal_dateTimePicker.Enabled = true;
                DataFinal_dateTimePicker.Text = DateTime.Now.Date.ToShortDateString();
                DataInicial_dateTimePicker.Text = DateTime.Today.AddMonths(-1).Date.ToShortDateString();
            }
            this.ConsultaTodosNaoEntreguesOrdenadoNome();
        }

        private void BtnCoordenadas_Click(object sender, EventArgs e)
        {
            if (currentRow == null) return;

            string CoordenadasAtual = currentRow["CoordenadasDestinatarioAusente"].ToString();

            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

            if (File.Exists(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            }
            else if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            }
            else if (File.Exists(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            }
            else if (File.Exists(@"C:\Program Files\Google\Chrome\Application\chrome.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            }
            else
            {
                return;
            }

            //pProcess.StartInfo.Arguments = "https://maps.google.com/maps?t=k&q=loc:-10.22285+-48.34052";
            pProcess.StartInfo.Arguments = string.Format("https://www.google.com.br/maps/search/{0}", CoordenadasAtual);
            pProcess.Start();
            //pProcess.WaitForExit();



            return;

            //VerificaNavegador();

            //string CodigoObjetoFormatado = string.Format("{0} {1} {2} {3} {4}",
            //        currentRow["CodigoObjeto"].ToString().Substring(0, 2),
            //        currentRow["CodigoObjeto"].ToString().Substring(2, 3),
            //        currentRow["CodigoObjeto"].ToString().Substring(5, 3),
            //        currentRow["CodigoObjeto"].ToString().Substring(8, 3),
            //        currentRow["CodigoObjeto"].ToString().Substring(11, 2));
            //string NomeCliente = currentRow["NomeCliente"].ToString();
            //string EnderecoCompleto = string.Format("{0}, {1}, {2}, {3}", currentRow["EnderecoLOEC"], currentRow["BairroLOEC"], currentRow["CepDestinoPostagem"], currentRow["MunicipioLOEC"]);
            //string DataCriacaoLOEC = currentRow["CriacaoLOEC"].ToString();
            //string UnidadeLOEC = currentRow["UnidadeLOEC"].ToString();
            //string DistritoLOEC = currentRow["DistritoLOEC"].ToString();
            //string CarteiroLOEC = currentRow["CarteiroLOEC"].ToString();
            ////string CoordenadasAtual = currentRow["CoordenadasDestinatarioAusente"].ToString();
            ////"-10.22285,-48.34052"

            //FormularioCoordenadasExibicaoMapa formularioCoordenadasExibicaoMapa = new FormularioCoordenadasExibicaoMapa(CoordenadasAtual, CodigoObjetoFormatado, NomeCliente, EnderecoCompleto, DataCriacaoLOEC, UnidadeLOEC, DistritoLOEC, CarteiroLOEC);

            ////formularioCoordenadasExibicaoMapa.MdiParent = MdiParent;
            //formularioCoordenadasExibicaoMapa.ShowDialog();
            //formularioCoordenadasExibicaoMapa.WindowState = FormWindowState.Normal;
            //formularioCoordenadasExibicaoMapa.WindowState = FormWindowState.Maximized;
            //formularioCoordenadasExibicaoMapa.Activate();
        }

        private bool VerificaNavegador()
        {
            int versaoNavegador;
            int RegVal = 0;
            try
            {
                // obtem a versão instalada do IE
                using (WebBrowser Wb = new WebBrowser())
                {
                    Mensagens.InformaDesenvolvedor("Versao navegador:" + Wb.Version.Major.ToString());
                    versaoNavegador = Wb.Version.Major;
                }
                // define a versão do IE
                if (versaoNavegador >= 11)
                {
                    RegVal = 11001;
                }
                else if (versaoNavegador == 10)
                {
                    RegVal = 10001;
                }
                else if (versaoNavegador == 9)
                {
                    RegVal = 9999;
                }
                else if (versaoNavegador == 8)
                {
                    RegVal = 8888;
                }
                else
                {
                    RegVal = 7000;
                }

                // define a chave atual
                Mensagens.InformaDesenvolvedor("Caminho: " + @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");
                Mensagens.InformaDesenvolvedor("CurrentProcess: " + System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe");
                //Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);
                //Key.SetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", RegVal, Microsoft.Win32.RegistryValueKind.DWord);
                //Key.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            this.ConsultaTodosNaoEntreguesOrdenadoNome();
        }

        private void FormularioConsulta_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioCadastroObjetos")
                {
                    item.WindowState = FormWindowState.Maximized;
                    item.Activate();
                    return;
                }
            }
        }

        private void imprimirModeloLDIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().modeloLDIToolStripMenuItem_Click(sender, e);
        }

        private void imprimirModeloComumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().modeloComumToolStripMenuItem_Click(sender, e);
        }

        private void imprimirAvisosDeChegadaSelecionadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().imprimirAvisosDeChegadaSelecionadosToolStripMenuItem_Click(sender, e);
        }

        private void alterarSituaçãoDeItensSelecionadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) return;

                FormularioAlterarSituacaoItensSelecionados formularioAlterarSituacaoItensSelecionados = new FormularioAlterarSituacaoItensSelecionados();
                formularioAlterarSituacaoItensSelecionados.ShowDialog();

                if (string.IsNullOrEmpty(formularioAlterarSituacaoItensSelecionados.itemMotivoBaixaSelecionado)) return;

                List<string> ListaGridSelecaoAtual = new List<string>();

                for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    bool existe = ListaGridSelecaoAtual.AsEnumerable().Any(t => t == this.dataGridView1.SelectedRows[i].Cells["codigoObjetoDataGridViewTextBoxColumn"].Value.ToString());
                    if (!existe)
                        ListaGridSelecaoAtual.Add(this.dataGridView1.SelectedRows[i].Cells["codigoObjetoDataGridViewTextBoxColumn"].Value.ToString());
                }

                if (ListaGridSelecaoAtual.Count == 0) return;

                FormularioConsulta.RetornaComponentesFormularioConsulta().AlterarSituacaoItensSelecionados(ListaGridSelecaoAtual, formularioAlterarSituacaoItensSelecionados.itemMotivoBaixaSelecionado);

                //if (Mensagens.Pergunta("Itens atualizado com sucesso! Deseja atualizar grid?") == System.Windows.Forms.DialogResult.Yes)
                //{
                    if (Application.OpenForms["FormularioConsulta"] != null) //verifica se está aberto
                        FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();

                    FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();
                //}
            }
            catch (Exception ex)
            {
                Mensagens.Erro("Ocorreu um erro ao realizar a tarefa: " + ex);
            }

        }

        public void AlterarSituacaoItensSelecionados(List<string> ListaCodigosGrid, string MotivoBaixaInformado)
        {
            try
            {
                waitForm.Show(this);
                foreach (string itemCodigo in ListaCodigosGrid)
                {
                    using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                    {
                        if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                        string dataHoje = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        string dataModificacaoRetornada = dao.RetornaValor("SELECT DataModificacao FROM TabelaObjetosSROLocal WHERE (CodigoObjeto = '" + itemCodigo + "')").ToString();
                        if (string.IsNullOrEmpty(dataModificacaoRetornada))
                            dataModificacaoRetornada = dataHoje;

                        List<Parametros> pr = new List<Parametros>() {
                            new Parametros() { Nome = "@DataModificacao", Tipo = TipoCampo.Text, Valor = dataModificacaoRetornada },
                            new Parametros() { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = MotivoBaixaInformado },
                            new Parametros() { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Boolean, Valor = true },

                            new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = itemCodigo }
                        };
                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET DataModificacao = @DataModificacao, Situacao = @Situacao, ObjetoEntregue = @ObjetoEntregue WHERE (CodigoObjeto = @CodigoObjeto)", pr);
                    }
                }
                waitForm.Close();
            }
            catch (Exception EX)
            {
                Mensagens.Erro("Ocorreu um erro inesperado ao gravar. \nErro: " + EX.Message);
            }
        }

        private void alterarComentarioSelecionadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            FormularioAlterarComentarioItensSelecionados formularioAlterarComentarioItensSelecionados = new FormularioAlterarComentarioItensSelecionados();
            formularioAlterarComentarioItensSelecionados.ShowDialog();

            if (formularioAlterarComentarioItensSelecionados.ClicouCancelar) return;

            if (string.IsNullOrEmpty(formularioAlterarComentarioItensSelecionados.comboBoxComentario.Text)) return;

            List<string> ListaGridSelecaoAtual = new List<string>();

            for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                bool existe = ListaGridSelecaoAtual.AsEnumerable().Any(t => t == this.dataGridView1.SelectedRows[i].Cells["codigoObjetoDataGridViewTextBoxColumn"].Value.ToString());
                if (!existe)
                    ListaGridSelecaoAtual.Add(this.dataGridView1.SelectedRows[i].Cells["codigoObjetoDataGridViewTextBoxColumn"].Value.ToString());
            }

            if (ListaGridSelecaoAtual.Count == 0) return;

            FormularioConsulta.RetornaComponentesFormularioConsulta().AlterarComentarioItensSelecionados(ListaGridSelecaoAtual, formularioAlterarComentarioItensSelecionados.comboBoxComentario.Text.RemoveAcentos().ToUpper());

            if (Application.OpenForms["FormularioConsulta"] != null) //verifica se está aberto
                FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();

            FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();
        }

        public void AlterarComentarioItensSelecionados(List<string> ListaCodigosGrid, string Comentario)
        {
            try
            {
                foreach (string itemCodigo in ListaCodigosGrid)
                {
                    using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                    {
                        if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                        List<Parametros> pr = new List<Parametros>() {
                            new Parametros() { Nome = "@Comentario", Tipo = TipoCampo.Text, Valor = Comentario },

                            new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = itemCodigo }
                        };
                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Comentario = @Comentario WHERE (CodigoObjeto = @CodigoObjeto)", pr);
                    }
                }
            }
            catch (Exception EX)
            {
                Mensagens.Erro("Ocorreu um erro inesperado ao gravar. \nErro: " + EX.Message);
            }
        }

        private void AtualizarObjetosSelecionadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().marcarSelecionadosComoNaoAtualizadoToolStripMenuItem_Click(sender, e);
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().atualizarNovosObjetosToolStripMenuItem_Click(sender, e);
        }


        private void Btn1ImprimirLDI_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0) return;
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().modeloLDIToolStripMenuItem_Click(sender, e);
        }

        private void Btn2ImprimirAvisos_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0) return;
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().imprimirAvisosDeChegadaSelecionadosToolStripMenuItem_Click(sender, e);
        }

        private void Btn3AlterarItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0) return;
            AlterarItem(sender, e);
        }

        private void Btn4SituacaoBaixa_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0) return;
            alterarSituaçãoDeItensSelecionadosToolStripMenuItem_Click(sender, e);
        }

        private void Btn5Comentario_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0) return;
            alterarComentarioSelecionadosToolStripMenuItem_Click(sender, e);
        }

        private void Btn6Atualizar_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0) return;
            AtualizarObjetosSelecionadosToolStripMenuItem_Click(sender, e);
        }

        private void Btn0SelecionarLinhas_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
        }

        private void BtnImprimirAssinaturasHoje_Click(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().imprimirListaLDIsAssinaturasPorOrdemAlfabéticaToolStripMenuItem_Click(sender, e);
        }

        private void BtnImprimirAvisosHoje_Click(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().imprimirAvisoDeChegadaHOJEExcetoEntreguesECaixaPostalToolStripMenuItem_Click(sender, e);
        }

        private void checkBoxIncluirCaixaPostal_CheckedChanged(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirCaixaPostalPesquisa_toolStripMenuItem.Checked = checkBoxIncluirCaixaPostal.Checked;
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirCaixaPostalPesquisa_toolStripMenuItem_Click(sender, e);
        }

        private void checkBoxIncluirBaixados_CheckedChanged(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirItensJaEntreguesToolStripMenuItem.Checked = checkBoxIncluirBaixados.Checked;
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirItensJaEntreguesToolStripMenuItem_Click(sender, e);
        }

        private void desfazerBaixaAnteriorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            List<string> ListaGridSelecaoAtual = new List<string>();

            try
            {
                for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    bool existe = ListaGridSelecaoAtual.AsEnumerable().Any(t => t == this.dataGridView1.SelectedRows[i].Cells["codigoObjetoDataGridViewTextBoxColumn"].Value.ToString());
                    if (!existe)
                        ListaGridSelecaoAtual.Add(this.dataGridView1.SelectedRows[i].Cells["codigoObjetoDataGridViewTextBoxColumn"].Value.ToString());
                }

                if (ListaGridSelecaoAtual.Count == 0) return;

                #region atualiza tabela
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                    foreach (string itemCodigoAtual in ListaGridSelecaoAtual)
                    {
                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET DataModificacao = @DataModificacao, Situacao = @Situacao, ObjetoEntregue = @ObjetoEntregue WHERE (CodigoObjeto = @CodigoObjeto)"
                            , new List<Parametros>() {
                                new Parametros { Nome = "@DataModificacao", Tipo = TipoCampo.Text, Valor = DBNull.Value },
                                new Parametros { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = "AGUARDANDO RETIRADA".ToUpper() },
                                new Parametros { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Boolean, Valor = false },
                                new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = itemCodigoAtual }
                            });
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                Mensagens.Erro("Ocorreu o seguinte erro: " + ex);
            }
            finally
            {
                if (Application.OpenForms["FormularioConsulta"] != null) //verifica se está aberto
                    FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();

                FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();
            }
        }
    }
}


