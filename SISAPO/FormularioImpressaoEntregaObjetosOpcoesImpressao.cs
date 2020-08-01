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
    public partial class FormularioImpressaoEntregaObjetosOpcoesImpressao : Form
    {        
        public bool IncluirItensJaEntregues = false;
        public bool IncluirItensCaixaPostal = false;
        public bool Cancelou = true;

        public FormularioImpressaoEntregaObjetosOpcoesImpressao()
        {
            InitializeComponent();
        }

        private void FormularioImpressaoEntregaObjetosOpcoesImpressao_Load(object sender, EventArgs e)
        {
            checkBoxIncluirItensJaEntregues.Checked = true;
        }

        private void FormularioImpressaoEntregaObjetosOpcoesImpressao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }        

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            IncluirItensJaEntregues = checkBoxIncluirItensJaEntregues.Checked;
            IncluirItensCaixaPostal = checkBoxIncluirItensCaixaPostal.Checked;
            Cancelou = false;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxIncluirItensJaEntregues_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIncluirItensJaEntregues.Checked)
            {
                checkBoxIncluirItensJaEntregues.ForeColor = Color.Red;
            }
            else
            {
                checkBoxIncluirItensJaEntregues.ForeColor = System.Drawing.SystemColors.Highlight;
            }
        }

        private void checkBoxIncluirItensCaixaPostal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIncluirItensCaixaPostal.Checked)
            {
                checkBoxIncluirItensCaixaPostal.ForeColor = Color.Red;
            }
            else
            {
                checkBoxIncluirItensCaixaPostal.ForeColor = System.Drawing.SystemColors.Highlight;
            }
        }
    }
}
