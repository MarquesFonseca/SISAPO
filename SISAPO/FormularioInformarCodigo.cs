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
    public partial class FormularioInformarCodigo : Form
    {
        public bool ClicouConfirmar = false;
        public bool ClicouCancelar = false;
        public string CodigoObjetoInformado = string.Empty;

        public FormularioInformarCodigo()
        {
            InitializeComponent();
        }

        private void FormularioInformarCodigo_Load(object sender, EventArgs e)
        {
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

            ProcessaObjeto();
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
            ProcessaObjeto();
        }

        private void ProcessaObjeto()
        {
            if (string.IsNullOrWhiteSpace(TxtObjetoAtual.Text))
            {
                Mensagens.Erro("Informe um Código de Rastreamento válido!");
                return;
            }

            if (!string.IsNullOrWhiteSpace(TxtObjetoAtual.Text))
            {
                if (VerificaCodigoRastreamentoPadraoBrasileiro(TxtObjetoAtual.Text) == false)
                    return;

                if (TxtObjetoAtual.Text.Length > 13)
                    return;
            }
            CodigoObjetoInformado = TxtObjetoAtual.Text;

            ClicouConfirmar = true;
            ClicouCancelar = false;

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
            if (e.KeyCode == Keys.F5)
            {
                btnConfirmar_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnCancelar_Click(sender, e);
            }
        }
    }
}
