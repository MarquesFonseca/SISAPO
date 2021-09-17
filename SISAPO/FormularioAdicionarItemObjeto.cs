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
    public partial class FormularioAdicionarItemObjeto : Form
    {
        public DataTable dtbLista = new DataTable();
        public bool ClicouConfirmar = false;
        public bool ClicouCancelar = false;

        private List<string> ListaItensMotivoBaixa = new List<string>();

        public FormularioAdicionarItemObjeto()
        {
            InitializeComponent();
        }

        private void FormularioAdicionarItemObjeto_Load(object sender, EventArgs e)
        {
            dtbLista = new DataTable();
            dtbLista.Columns.Add("CodigoObjeto", typeof(string));
            dtbLista.Columns.Add("DataLancamento", typeof(DateTime));
            dtbLista.Columns.Add("DataModificacao", typeof(string));
            dtbLista.Columns.Add("Situacao", typeof(string));
            dtbLista.Columns.Add("Comentario", typeof(string));



            ListaItensMotivoBaixa = new List<string>();
            ListaItensMotivoBaixa.Add("PCT");
            ListaItensMotivoBaixa.Add("PCT INT");
            ListaItensMotivoBaixa.Add("ENV");

            ListaItensMotivoBaixa.Add("PCT AO REMETENTE");
            ListaItensMotivoBaixa.Add("PCT INT AO REMETENTE");
            ListaItensMotivoBaixa.Add("ENV AO REMETENTE");

            ListaItensMotivoBaixa.Add("PCT TERMO CONSTATACAO");
            ListaItensMotivoBaixa.Add("PCT INT TERMO CONSTATACAO");
            ListaItensMotivoBaixa.Add("ENV TERMO CONSTATACAO");

            comboBoxComentario.DataSource = ListaItensMotivoBaixa;
            //comboBoxComentario.Focus();

            SendKeys.Send("{TAB}");





            TxtObjetoAtual.Focus();
            TxtObjetoAtual.ScrollToCaret();
            TxtObjetoAtual.ScrollToCaret();

            TxtObjetoAtual.Select(TxtObjetoAtual.Text.Length, 0);
        }

        private void TxtObjetoAtual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                if (Mensagens.Pergunta("Realmente deseja sair?") == DialogResult.Yes)
                {
                    ClicouCancelar = true;
                    ClicouConfirmar = false;
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            if (string.IsNullOrEmpty(TxtObjetoAtual.Text)) return;

            if (e.KeyData != Keys.Enter) return;

            if (VerificaCodigoRastreamentoPadraoBrasileiro(TxtObjetoAtual.Text) == false)
                return;
            if (TxtObjetoAtual.Text.Length > 13)
                return;

            AdicionaItemLista();
        }

        private void AdicionaItemLista()
        {
            string CodigoAtual = TxtObjetoAtual.Text;
            string DataLancamento = DateTime.Now.ToString();
            string DataModificacao = "";
            string Situacao = "Aguardando retirada".ToUpper();
            string Comentario = comboBoxComentario.Text;
            try
            {
                bool existe = dtbLista.AsEnumerable().Any(t => t["CodigoObjeto"].ToString() == CodigoAtual);
                if (!existe)
                    dtbLista.Rows.Add(CodigoAtual, Convert.ToDateTime(DataLancamento), DataModificacao, Situacao, Comentario.RemoveAcentos().ToUpper());

                dataGridView1.DataSource = dtbLista;
                dtbLista.DefaultView.Sort = "DataLancamento DESC";
                dtbLista = dtbLista.DefaultView.ToTable();
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
            }
            finally
            {
                TxtObjetoAtual.Text = "";
                TxtObjetoAtual.Focus();
                TxtObjetoAtual.ScrollToCaret();
                TxtObjetoAtual.ScrollToCaret();

                TxtObjetoAtual.Select(TxtObjetoAtual.Text.Length, 0);
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

        private void btnConfirmar_Click(object sender, EventArgs e)
        {            
            if(!string.IsNullOrWhiteSpace(TxtObjetoAtual.Text))
            {
                if (VerificaCodigoRastreamentoPadraoBrasileiro(TxtObjetoAtual.Text) == false)
                    return;

                if (TxtObjetoAtual.Text.Length > 13)
                    return;

                AdicionaItemLista();
            }

            if (dtbLista.Rows.Count == 0)
            {
                ClicouConfirmar = false;
                ClicouCancelar = true;
            }
            else
            {
                ClicouConfirmar = true;
                ClicouCancelar = false;
            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClicouConfirmar = false;
            ClicouCancelar = true;
            this.Close();
        }

        private void FormularioAdicionarItemObjeto_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F5)
            {
                btnConfirmar_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnCancelar_Click(sender, e);
            }
        }

        private void comboBoxComentario_Leave(object sender, EventArgs e)
        {
            //deletar
        }

        private void comboBoxComentario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxComentario.SelectedIndex != -1)
            {
                //SendKeys.Send("{TAB}");
                TxtObjetoAtual.Focus();
                TxtObjetoAtual.ScrollToCaret();

                TxtObjetoAtual.Select(TxtObjetoAtual.Text.Length, 0);
            }
        }

        private void comboBoxComentario_TextChanged(object sender, EventArgs e)
        {
            int Pos = comboBoxComentario.SelectionStart;
            comboBoxComentario.Text = comboBoxComentario.Text.ToUpper();
            comboBoxComentario.SelectionStart = Pos;
            //comboBoxComentario.Text = comboBoxComentario.Text.ToUpper();
        }
    }
}
