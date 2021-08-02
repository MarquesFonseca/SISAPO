using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeraChaveSisapo
{
    public partial class FormAcesso : Form
    {
        public FormAcesso()
        {
            InitializeComponent();
        }

        private void FormAcesso_Load(object sender, EventArgs e)
        {
            TxtUsuario.Focus();
        }

        private void TxtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            if (e.KeyCode == Keys.Enter)
            {
                //SendKeys.Send("{TAB}");
                TxtSenha.Focus();
            }
        }

        private void TxtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnAcesso_Click(sender, e);
            }
        }

        private void BtnAcesso_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtUsuario.Text))
            {
                MessageBox.Show("Informe o Usuário.");
                TxtUsuario.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TxtSenha.Text))
            {
                MessageBox.Show("Informe a Senha.");
                TxtSenha.Focus();
                return;
            }
            if (TxtUsuario.Text.ToUpper() != "marquesfonseca".ToUpper())
            {
                MessageBox.Show("Usuário incorreto!");
                TxtUsuario.Focus();
                return;
            }
            if (TxtSenha.Text.ToUpper() != "9342456".ToUpper())
            {
                MessageBox.Show("Senha incorreta!");
                TxtSenha.Focus();
                return;
            }

            FormGeraChaveSisapo form = new FormGeraChaveSisapo();
            form.ShowDialog();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAcesso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }


    }
}
