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
    public partial class FormularioAlterarSituacaoItensSelecionados : Form
    {
        private DataTable dtbLista = new DataTable();
        public bool ClicouConfirmar = false;
        public bool ClicouCancelar = false;
        public string itemMotivoBaixaSelecionado = string.Empty;
        private Dictionary<string, string> ListaItensMotivoBaixa = new Dictionary<string, string>();
        public string nomeRecebedor = string.Empty;
        public string docRecebedor = string.Empty;

        public FormularioAlterarSituacaoItensSelecionados()
        {
            InitializeComponent();
        }

        private void FormularioAlterarSituacaoItensSelecionados_Load(object sender, EventArgs e)
        {
            itemMotivoBaixaSelecionado = string.Empty;

            ListaItensMotivoBaixa = new Dictionary<string, string>();
            ListaItensMotivoBaixa.Add("0 - DISTRIBUÍDO AO DESTINATÁRIO", "ENTREGUE");
            ListaItensMotivoBaixa.Add("3 - NÃO PROCURADO PELO REMETENTE", "NAO PROCURADO PELO REMETENTE");
            ListaItensMotivoBaixa.Add("4 - RECUSADO", "RECUSADO");
            ListaItensMotivoBaixa.Add("12 - REFUGADO/DESTRUÍDO", "REFUGADO/DESTRUÍDO");
            ListaItensMotivoBaixa.Add("14 - DESISTÊNCIA DE POSTAGEM PELO REMETENTE", "DESISTENCIA DE POSTAGEM PELO REMETENTE");
            ListaItensMotivoBaixa.Add("19 - MAL ENDEREÇADO", "MAL ENDERECADO");
            ListaItensMotivoBaixa.Add("23 - DISTRIBUÍDO AO REMETENTE", "DISTRIBUIDO AO REMETENTE");
            ListaItensMotivoBaixa.Add("26 - NÃO PROCURADO PELO DESTINATÁRIO", "NAO PROCURADO PELO DESTINATARIO");
            ListaItensMotivoBaixa.Add("39 - CANCELAMENTO DE LANÇAMENTO INTERNO", "CANCELAMENTO DE LANCAMENTO INTERNO");
            ListaItensMotivoBaixa.Add("52 - ROUBO A UNIDADE", "ROUBO A UNIDADE");
            ListaItensMotivoBaixa.Add("60 - AGUARDANDO PRAZO PARA REFUGO", "AGUARDANDO PRAZO PARA REFUGO");

            dtbLista = new DataTable();
            dtbLista.Columns.Add("MotivoBaixa", typeof(string));

            foreach (var item in ListaItensMotivoBaixa)
            {
                DataRow row = dtbLista.NewRow();
                row["MotivoBaixa"] = item.Key;
                dtbLista.Rows.Add(row);
            }

            dataGridViewMotivoBaixo.DataSource = dtbLista;
            dataGridViewMotivoBaixo.Focus();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            ClicouConfirmar = true;
            ClicouCancelar = false;

            for (int i = this.dataGridViewMotivoBaixo.SelectedRows.Count - 1; i >= 0; i--)
            {
                string itemGrid = this.dataGridViewMotivoBaixo.SelectedRows[i].Cells["MotivoBaixa"].Value.ToString();
                itemMotivoBaixaSelecionado = ListaItensMotivoBaixa.AsEnumerable().First(T => T.Key.ToString() == itemGrid).Value.ToString().RemoveAcentos().ToUpper().Trim();
                nomeRecebedor = TxtNomeRecebedor.Text;
                docRecebedor = TxtDocRecebedor.Text;
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClicouConfirmar = false;
            ClicouCancelar = true;
            itemMotivoBaixaSelecionado = string.Empty;
            this.Close();
        }

        private void FormularioAdicionarItemObjeto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnConfirmar_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnCancelar_Click(sender, e);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConfirmar_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnCancelar_Click(sender, e);
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnConfirmar_Click(sender, e);
        }
    }
}
