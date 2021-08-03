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
    public partial class FormularioAlterarComentarioItensSelecionados : Form
    {
        public bool ClicouConfirmar = false;
        public bool ClicouCancelar = false;
        private List<string> ListaItensMotivoBaixa = new List<string>();

        public FormularioAlterarComentarioItensSelecionados()
        {
            InitializeComponent();
        }

        private void FormularioAlterarComentarioItensSelecionados_Load(object sender, EventArgs e)
        {
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
            comboBoxComentario.Focus();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxComentario.Text))
            {
                Mensagens.Informa("Informe um Comentário.");
                return;
            }
            else
            {
                ClicouConfirmar = true;
                comboBoxComentario.Text = comboBoxComentario.Text.ToUpper();
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClicouCancelar = true;
            this.Close();
        }

        private void FormularioAlterarComentarioItensSelecionados_KeyDown(object sender, KeyEventArgs e)
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

        private void comboBoxComentario_KeyDown(object sender, KeyEventArgs e)
        {
            dataGridView1_KeyDown(sender, e);
        }
    }
}
