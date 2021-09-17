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
    public partial class FormularioVerObjetosNaoAtualizados : Form
    {
        public FormularioVerObjetosNaoAtualizados()
        {
            InitializeComponent();
        }

        private void FormularioVerObjetosNaoAtualizados_Load(object sender, EventArgs e)
        {
            btnAtualizar_Click(sender, e);
        }

        private void TxtObjetoAtual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormularioVerObjetosNaoAtualizados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnCancelar_Click(sender, e);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            #region atualiza grid
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                dataGridView1.DataSource = dao.RetornaDataTable("SELECT CodigoObjeto from TabelaObjetosSROLocal WHERE Atualizado = FALSE");
            }
            #endregion
        }
    }
}
