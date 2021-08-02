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
    public partial class FormAcesso : Form
    {
        public bool Autenticado = false;
        public FormAcesso()
        {
            InitializeComponent();
        }

        private void FormAcesso_Load(object sender, EventArgs e)
        {
            this.Autenticado = false;
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
            Autenticado = false;
            if (string.IsNullOrEmpty(TxtUsuario.Text))
            {
                Mensagens.Informa("Informe o Usuário.");
                TxtUsuario.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(TxtSenha.Text))
            {
                Mensagens.Informa("Informe a Senha.");
                TxtSenha.Focus();
                return;
            }
            else if (TxtUsuario.Text.ToUpper() != "marquesfonseca".ToUpper())
            {
                Mensagens.Erro("Usuário incorreto!");
                TxtUsuario.Focus();
                return;
            }
            else if (TxtSenha.Text.ToUpper() != "9342456".ToUpper())
            {
                Mensagens.Erro("Senha incorreta!");
                TxtSenha.Focus();
                return;
            }
            else
            {
                Autenticado = true;
                this.Close();
            }
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
