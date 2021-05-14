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
    public partial class FormularioAlteracaoEnderecoObjeto : Form
    {
        public bool cancelar = true;
        public FormularioAlteracaoEnderecoObjeto()
        {
            InitializeComponent();
        }

        private void FormularioAlteracaoEnderecoObjeto_Load(object sender, EventArgs e)
        {
            cancelar = true;

            TxtEndereco.Focus();
            //SendKeys.Send("{TAB}");
            //SendKeys.Send("{TAB}");
            TxtEndereco.ScrollToCaret();
            TxtEndereco.ScrollToCaret();

            TxtEndereco.Select(TxtEndereco.Text.Length, 0);
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            cancelar = false;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancelar = true;
            this.Close();
        }

        private void FormularioAlteracaoEnderecoObjeto_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnAlterar_Click(sender, e);
            }
            if(e.KeyCode == Keys.Escape)
            {
                btnCancelar_Click(sender, e);
            }
        }
    }
}
