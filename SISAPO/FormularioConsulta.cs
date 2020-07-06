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

        public static string NovoFiltro = string.Empty;

        public FormularioConsulta()
        {
            InitializeComponent();
        }

        private void FormularioConsulta_Load(object sender, EventArgs e)
        {
            DataFinal_dateTimePicker.Text = DateTime.Now.Date.ToShortDateString();
            DataInicial_dateTimePicker.Text = DateTime.Today.AddMonths(-1).Date.ToShortDateString();

            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
            {
                DataFinal_dateTimePicker.Text = "25/08/2019";
                DataInicial_dateTimePicker.Text = "25/09/2019";
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
                    campoPesquisa = string.Format("(CodigoObjeto like '%{0}' OR CodigoLdi like '{0}%' OR NomeCliente like '%{0}%')", TxtPesquisa.Text.RemoveSimbolos().RemoveSpecialChars());
                }
                else
                {
                    campoPesquisa = string.Format("(CodigoObjeto like '%{0}' OR NomeCliente like '%{0}%')", TxtPesquisa.Text.RemoveSimbolos().RemoveSpecialChars());
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

            GravaHistoricoConsulta(TxtPesquisa.Text);

            using (FormularioSRORastreamentoUnificado formularioSRORastreamentoUnificado = new FormularioSRORastreamentoUnificado(TxtPesquisa.Text))
            {
                formularioSRORastreamentoUnificado.WindowState = FormWindowState.Normal;
                formularioSRORastreamentoUnificado.StartPosition = FormStartPosition.CenterScreen;
                formularioSRORastreamentoUnificado.Text = string.Format(@"SRO - Rastreamento Unificado - http://websro2.correiosnet.int/rastreamento/sro?opcao=PESQUISA&objetos={0}", TxtPesquisa.Text);
                formularioSRORastreamentoUnificado.ShowDialog();
            }
        }

        private bool VerificaCodigoRastreamentoPadraoBrasileiro(string TxtPesquisa)
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
            if (!DAO.TestaConexao(ClassesDiversas.Configuracoes.strConexao, TipoBanco.OleDb))
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao;
                return;
            }
            DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao);

            List<Parametros> Pr = new List<Parametros>()
            {
                    new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = codigoConsultado }
            };

            dao.ExecutaSQL("Insert into TabelaHistoricoConsulta(CodigoObjeto) values (@CodigoObjeto)", Pr);
        }

        private void tabelaObjetosSROLocalBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (currentRow == null)
            {
                //FormularioPrincipal.RetornaComponentes().toolStripStatusLabel.Text = "";
                TxtItemSelecionado.Text = "";
                TxtCodigoObjetoSelecionado.Text = "";
                TxtCodigoLDISelecionado.Text = "";
                TxtDataLancamento.Text = "";
                TxtSituacaoAtual.Text = "";

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
            else
            {
                TxtCodigoLDISelecionado.Text = currentRow["CodigoLdi"].ToString();
                TxtDataLancamento.Text = currentRow["DataLancamento"].ToString();

                string CodigoObjetoFormatado = string.Format("{0} {1} {2} {3} {4}",
                    currentRow["CodigoObjeto"].ToString().Substring(0, 2),
                    currentRow["CodigoObjeto"].ToString().Substring(2, 3),
                    currentRow["CodigoObjeto"].ToString().Substring(5, 3),
                    currentRow["CodigoObjeto"].ToString().Substring(8, 3),
                    currentRow["CodigoObjeto"].ToString().Substring(11, 2));

                TxtCodigoObjetoSelecionado.Text = currentRow["CodigoObjeto"].ToString();
                TxtItemSelecionado.Text = string.Format("{0} - {1}", CodigoObjetoFormatado, currentRow["NomeCliente"].ToString());
                TxtSituacaoAtual.Text = currentRow["Situacao"].ToString().Replace("NAO", "NÃO").Replace("DISTRIBUIDO", "DISTRIBUÍDO").Replace("DESTINATARIO", "DESTINATÁRIO");

                //this.Text += TxtItemSelecionado.Text;
                if (string.IsNullOrEmpty(currentRow["CoordenadasDestinatarioAusente"].ToString()))
                {
                    BtnCoordenadas.Visible = false;
                }
                else
                {
                    BtnCoordenadas.Visible = true;
                }

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
            }
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
                this.tabelaObjetosSROLocalTableAdapter.Fill(this.dataSetTabelaObjetosSROLocal.TabelaObjetosSROLocal, dataInicial, datafinal);
                this.MontaFiltro();
                waitForm.Close();

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
                            new Parametros() { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = true },
                            new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjeto }
                        };
                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Atualizado = @Atualizado  WHERE (CodigoObjeto = @CodigoObjeto)", pr);
                    this.dataGridView1.SelectedRows[i].Cells["atualizadoDataGridViewCheckBoxColumn"].Value = true;
                }
            }
            waitForm.Close();
        }

        public void MarcarSelecionadosComoNaoAtualizado()
        {
            //using (FormWaiting frm = new FormWaiting(ProcessandoMarcarSelecionadosComoNaoAtualizado))
            //{
            //    frm.ShowDialog(this);
            //}
            ProcessandoMarcarSelecionadosComoNaoAtualizado();
        }

        void ProcessandoMarcarSelecionadosComoNaoAtualizado()
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
                            new Parametros() { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = false },
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
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem_Click(sender, e);
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

            ultimoCodigoDetalhado = currentRow["CodigoObjeto"].ToString();
            FormularioSRORastreamentoUnificado formularioSRORastreamentoUnificado = new FormularioSRORastreamentoUnificado(currentRow["CodigoObjeto"].ToString());
            formularioSRORastreamentoUnificado.MdiParent = MdiParent;
            formularioSRORastreamentoUnificado.Text = string.Format(@"SRO - Rastreamento Unificado Detalhado [{0}]", currentRow["CodigoObjeto"].ToString());
            formularioSRORastreamentoUnificado.Show();
            formularioSRORastreamentoUnificado.WindowState = FormWindowState.Maximized;
            formularioSRORastreamentoUnificado.Activate();
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

            FormularioConsulta_Activated(sender, e);
        }

        Dictionary<string, string> dicionarioCodigo_Nome = new Dictionary<string, string>();
        public void GeraImpressaoItensSelecionados()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                Mensagens.Informa("Nenhum objeto foi selecionado.");
                return;
            }
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioImpressaoEntregaObjetos")
                {
                    item.Close();
                    break;
                }
            }

            dicionarioCodigo_Nome = new Dictionary<string, string>();

            for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                bool existe = dicionarioCodigo_Nome.AsEnumerable().Any(t => t.Key == this.dataGridView1.SelectedRows[i].Cells[0].Value.ToString());
                if (!existe)
                    dicionarioCodigo_Nome.Add(this.dataGridView1.SelectedRows[i].Cells[0].Value.ToString(), this.dataGridView1.SelectedRows[i].Cells[1].Value.ToString());
            }

            List<string> novosCodigosSelecionadosOrdenados = dicionarioCodigo_Nome.AsEnumerable().OrderBy(t => t.Value).Select(c => c.Key).ToList();


            FormularioImpressaoEntregaObjetos formularioImpressaoEntregaObjetos = new FormularioImpressaoEntregaObjetos(novosCodigosSelecionadosOrdenados);
            formularioImpressaoEntregaObjetos.MdiParent = MdiParent;
            formularioImpressaoEntregaObjetos.Show();
            //formularioImpressaoEntregaObjetos.WindowState = FormWindowState.Normal;
            formularioImpressaoEntregaObjetos.WindowState = FormWindowState.Maximized;
            formularioImpressaoEntregaObjetos.Activate();
        }

        public void GeraImpressaoItensLancadosNoDiaHoje(bool _incluirItensEntregues, bool _incluirItensCaixaPostal)
        {
            //string valorAtualTXTCampoPesquisa = TxtPesquisa.Text;
            TxtPesquisa.Text = string.Empty;

            string dataInicial = DateTime.Now.Date.ToString("yyyy/MM/dd");
            string datafinal = DateTime.Now.Date.ToString("yyyy/MM/dd");

            string objetoCaixaPostalSelecionado = "";
            string objetoEntregueSelecionado = "";
            //bool ExibirItensEntregues = FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirItensJaEntreguesToolStripMenuItem.Checked;
            ////ExibirItensEntregues = true;//faz com que sempre imprima com os entregues... alterado dia 09/07/2019 por achar necessário...
            //bool IncluirItensCaixaPostal = FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirCaixaPostalPesquisa_toolStripMenuItem.Checked;

            bool IncluirItensEntregues = _incluirItensEntregues;
            //ExibirItensEntregues = true;//faz com que sempre imprima com os entregues... alterado dia 09/07/2019 por achar necessário...
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
            FormularioImpressaoEntregaObjetos formularioImpressaoEntregaObjetos = new FormularioImpressaoEntregaObjetos(ListaCodigoOrdenadosPeloNomeCliente);
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

        private void alterarItemToolStripMenuItem_Click(object sender, EventArgs e)
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

                string CodigoObjeto = currentRow["CodigoObjeto"].ToString();//string CodigoObjeto,
                string NomeCliente = currentRow["NomeCliente"].ToString();//string NomeCliente,
                string NumeroLDI = currentRow["CodigoLdi"].ToString();//string CodigoLdi,
                string DataLancamento = currentRow["DataLancamento"].ToString();
                string Situacao = currentRow["Situacao"].ToString();//string Situacao,
                string DataModificacao = currentRow["DataModificacao"].ToString();
                int ObjetoEmCaixaPostal = currentRow["CaixaPostal"].ToInt();//bool CaixaPostal,
                int ObjetoJaEntregue = currentRow["ObjetoEntregue"].ToInt();//bool ObjetoEntregue,
                int ObjetoJaAtualizado = currentRow["Atualizado"].ToInt();//bool Atualizado,

                string UnidadePostagem = currentRow["UnidadePostagem"].ToString();//UnidadePostagem
                string MunicipioPostagem = currentRow["MunicipioPostagem"].ToString();//MunicipioPostagem
                string CriacaoPostagem = currentRow["CriacaoPostagem"].ToString();//CriacaoPostagem
                string CepDestinoPostagem = currentRow["CepDestinoPostagem"].ToString();//CepDestinoPostagem
                string ARPostagem = currentRow["ARPostagem"].ToString();//ARPostagem
                string MPPostagem = currentRow["MPPostagem"].ToString();//MPPostagem
                string DataMaxPrevistaEntregaPostagem = currentRow["DataMaxPrevistaEntregaPostagem"].ToString();//DataMaxPrevistaEntregaPostagem

                string UnidadeLOEC = currentRow["UnidadeLOEC"].ToString();//UnidadeLOEC
                string MunicipioLOEC = currentRow["MunicipioLOEC"].ToString();//MunicipioLOEC
                string CriacaoLOEC = currentRow["CriacaoLOEC"].ToString();//CriacaoLOEC
                string CarteiroLOEC = currentRow["CarteiroLOEC"].ToString();//CarteiroLOEC
                string DistritoLOEC = currentRow["DistritoLOEC"].ToString();//DistritoLOEC
                string NumeroLOEC = currentRow["NumeroLOEC"].ToString();//NumeroLOEC
                string EnderecoLOEC = currentRow["EnderecoLOEC"].ToString();//EnderecoLOEC
                string BairroLOEC = currentRow["BairroLOEC"].ToString();//BairroLOEC
                string LocalidadeLOEC = currentRow["LocalidadeLOEC"].ToString();//LocalidadeLOEC

                string SituacaoDestinatarioAusente = currentRow["SituacaoDestinatarioAusente"].ToString();//SituacaoDestinatarioAusente
                string AgrupadoDestinatarioAusente = currentRow["AgrupadoDestinatarioAusente"].ToString();//AgrupadoDestinatarioAusente
                string CoordenadasDestinatarioAusente = currentRow["CoordenadasDestinatarioAusente"].ToString();//CoordenadasDestinatarioAusente

                FormularioAlteracaoObjeto frm = new FormularioAlteracaoObjeto();
                frm.CodigoObjeto = CodigoObjeto;
                frm.NomeCliente = NomeCliente;
                frm.NumeroLDI = NumeroLDI;
                frm.DataLancamento = DataLancamento;
                frm.Situacao = Situacao;
                frm.DataModificacao = DataModificacao;
                frm.ObjetoEmCaixaPostal = ObjetoEmCaixaPostal == 0 ? false : true;
                frm.ObjetoJaEntregue = ObjetoJaEntregue == 0 ? false : true;
                frm.ObjetoJaAtualizado = ObjetoJaAtualizado == 0 ? false : true;

                frm.UnidadePostagem = UnidadePostagem;
                frm.MunicipioPostagem = MunicipioPostagem;
                frm.CriacaoPostagem = CriacaoPostagem;
                frm.CepDestinoPostagem = CepDestinoPostagem;
                frm.ARPostagem = ARPostagem;
                frm.MPPostagem = MPPostagem;
                frm.DataMaxPrevistaEntregaPostagem = DataMaxPrevistaEntregaPostagem;

                frm.UnidadeLOEC = UnidadeLOEC;
                frm.MunicipioLOEC = MunicipioLOEC;
                frm.CriacaoLOEC = CriacaoLOEC;
                frm.CarteiroLOEC = CarteiroLOEC;
                frm.DistritoLOEC = DistritoLOEC;
                frm.NumeroLOEC = NumeroLOEC;
                frm.EnderecoLOEC = EnderecoLOEC;
                frm.BairroLOEC = BairroLOEC;
                frm.LocalidadeLOEC = LocalidadeLOEC;

                frm.SituacaoDestinatarioAusente = SituacaoDestinatarioAusente;
                frm.AgrupadoDestinatarioAusente = AgrupadoDestinatarioAusente;
                frm.CoordenadasDestinatarioAusente = CoordenadasDestinatarioAusente;

                frm.ShowDialog();

                if (frm.Cancelando) return;

                currentRow["CodigoObjeto"] = frm.CodigoObjeto;//string CodigoObjeto,
                currentRow["NomeCliente"] = frm.NomeCliente.ToUpper().RemoveAcentos();//string NomeCliente,
                currentRow["CodigoLdi"] = frm.NumeroLDI;//string CodigoLdi,
                currentRow["DataLancamento"] = frm.DataLancamento;
                currentRow["Situacao"] = frm.Situacao;//string Situacao,
                currentRow["DataModificacao"] = frm.DataModificacao;
                currentRow["CaixaPostal"] = frm.ObjetoEmCaixaPostal;//bool CaixaPostal,
                currentRow["ObjetoEntregue"] = frm.ObjetoJaEntregue;//bool ObjetoEntregue,
                currentRow["Atualizado"] = frm.ObjetoJaAtualizado;//bool Atualizado,

                //string UnidadePostagem,
                //string MunicipioPostagem,
                //string CriacaoPostagem,
                //string CepDestinoPostagem,
                //string ARPostagem,
                //string MPPostagem,
                //string DataMaxPrevistaEntregaPostagem,

                //string UnidadeLOEC,
                //string MunicipioLOEC,
                //string CriacaoLOEC,
                //string CarteiroLOEC,
                //string DistritoLOEC,
                //string NumeroLOEC,
                //string EnderecoLOEC,
                //string BairroLOEC,
                //string LocalidadeLOEC,

                //string SituacaoDestinatarioAusente,
                //string AgrupadoDestinatarioAusente,
                //string CoordenadasDestinatarioAusente


                this.tabelaObjetosSROLocalTableAdapter.Connection.ConnectionString = ClassesDiversas.Configuracoes.strConexao;
                this.tabelaObjetosSROLocalTableAdapter.Update(dataSetTabelaObjetosSROLocal.TabelaObjetosSROLocal);

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
            alterarItemToolStripMenuItem_Click(sender, e);
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

        private void alterarItemToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AlterarItem(sender, e);
        }

        private void BtnCoordenadas_Click(object sender, EventArgs e)
        {
            if (currentRow == null) return;

            string CoordenadasAtual = currentRow["CoordenadasDestinatarioAusente"].ToString();

            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

            if (File.Exists(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            }
            else if (File.Exists(@"C:\Program Files\Google\Chrome\Application\chrome.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            }
            else if (File.Exists(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            }
            else if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            }
            else
            {
                return;
            }

            //pProcess.StartInfo.Arguments = "https://www.google.com.br/maps/search/-10.22285,-48.34052";
            //pProcess.StartInfo.Arguments = "https://maps.google.com/maps?t=k&q=loc:-10.22285+-48.34052";
            pProcess.StartInfo.Arguments = string.Format("https://maps.google.com/maps?t=k&q=loc:{0}", CoordenadasAtual);
            pProcess.Start();
            pProcess.WaitForExit();



            return;

            //VerificaNavegador();

            string CodigoObjetoFormatado = string.Format("{0} {1} {2} {3} {4}",
                    currentRow["CodigoObjeto"].ToString().Substring(0, 2),
                    currentRow["CodigoObjeto"].ToString().Substring(2, 3),
                    currentRow["CodigoObjeto"].ToString().Substring(5, 3),
                    currentRow["CodigoObjeto"].ToString().Substring(8, 3),
                    currentRow["CodigoObjeto"].ToString().Substring(11, 2));
            string NomeCliente = currentRow["NomeCliente"].ToString();
            string EnderecoCompleto = string.Format("{0}, {1}, {2}, {3}", currentRow["EnderecoLOEC"], currentRow["BairroLOEC"], currentRow["CepDestinoPostagem"], currentRow["MunicipioLOEC"]);
            string DataCriacaoLOEC = currentRow["CriacaoLOEC"].ToString();
            string UnidadeLOEC = currentRow["UnidadeLOEC"].ToString();
            string DistritoLOEC = currentRow["DistritoLOEC"].ToString();
            string CarteiroLOEC = currentRow["CarteiroLOEC"].ToString();
            //string CoordenadasAtual = currentRow["CoordenadasDestinatarioAusente"].ToString();
            //"-10.22285,-48.34052"

            FormularioCoordenadasExibicaoMapa formularioCoordenadasExibicaoMapa = new FormularioCoordenadasExibicaoMapa(CoordenadasAtual, CodigoObjetoFormatado, NomeCliente, EnderecoCompleto, DataCriacaoLOEC, UnidadeLOEC, DistritoLOEC, CarteiroLOEC);

            //formularioCoordenadasExibicaoMapa.MdiParent = MdiParent;
            formularioCoordenadasExibicaoMapa.ShowDialog();
            formularioCoordenadasExibicaoMapa.WindowState = FormWindowState.Normal;
            formularioCoordenadasExibicaoMapa.WindowState = FormWindowState.Maximized;
            formularioCoordenadasExibicaoMapa.Activate();
        }

        private bool VerificaNavegador()
        {
            int versaoNavegador;
            int RegVal;
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
            catch (Exception ex)
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
    }
}
