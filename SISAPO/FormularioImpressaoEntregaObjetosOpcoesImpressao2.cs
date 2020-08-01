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
    public partial class FormularioImpressaoEntregaObjetosOpcoesImpressao2 : Form
    {
        public bool OrdenacaoPorNomeDestinatario = false;
        public bool OrdenacaoPorDataLancamento = false;
        public bool OrdenacaoPorOrdemCrescente = true;
        public bool ImprimirUmPorFolha = false;
        public bool ImprimirVariosPorFolha = false;
        public bool Cancelou = false;
        public bool ClicouNoConfirmar = false;
        public FormularioConsulta.ModeloImpressaoListaObjetos ModeloImpressaoListaObjetos;

        private bool InicioTela = true;

        public FormularioImpressaoEntregaObjetosOpcoesImpressao2(FormularioConsulta.ModeloImpressaoListaObjetos _modeloImpressaoListaObjetos)
        {
            InitializeComponent();
            ModeloImpressaoListaObjetos = _modeloImpressaoListaObjetos;
        }

        private void FormularioImpressaoEntregaObjetosOpcoesImpressao2_Load(object sender, EventArgs e)
        {
            if (ModeloImpressaoListaObjetos == FormularioConsulta.ModeloImpressaoListaObjetos.ModeloComum)
            {
                groupBoxQuantidadeFolhas.Visible = false;
            }
            checkBoxOrdenacaoPorNomeDestinatario.Checked = true;
            checkBoxImprimirUmPorFolha.Checked = true;

            InicioTela = false;
        }

        private void FormularioImpressaoEntregaObjetosOpcoesImpressao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
            if (e.KeyCode == Keys.Escape)
            {
                Cancelou = true;
                this.Close();
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            ClicouNoConfirmar = true;
            OrdenacaoPorNomeDestinatario = checkBoxOrdenacaoPorNomeDestinatario.Checked;
            OrdenacaoPorDataLancamento = checkBoxOrdenacaoPorDataLancamento.Checked;
            ImprimirUmPorFolha = checkBoxImprimirUmPorFolha.Checked;
            ImprimirVariosPorFolha = checkBoxImprimirVariosPorFolha.Checked;

            Cancelou = false;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelou = true;
            this.Close();
        }

        private void checkBoxOrdenacaoPorNomeDestinatario_CheckedChanged(object sender, EventArgs e)
        {
            OrdenacaoPorNomeDestinatario = checkBoxOrdenacaoPorNomeDestinatario.Checked;

            if (checkBoxOrdenacaoPorNomeDestinatario.Checked)
            {
                checkBoxOrdenacaoPorNomeDestinatario.ForeColor = Color.Red;
                //OrdenacaoPorOrdemCrescente
                if (!InicioTela)
                    OrdenacaoPorOrdemCrescente = (Mensagens.Pergunta("Organizar os nomes em ordem crescente?\n\n(Letras iniciais do alfabeto em cima") == DialogResult.Yes ? true : false);
            }
            else
            {
                checkBoxOrdenacaoPorNomeDestinatario.ForeColor = System.Drawing.SystemColors.Highlight;
            }

            checkBoxOrdenacaoPorDataLancamento.Checked = !checkBoxOrdenacaoPorNomeDestinatario.Checked;
        }

        private void checkBoxOrdenacaoPorDataLancamento_CheckedChanged(object sender, EventArgs e)
        {
            OrdenacaoPorDataLancamento = checkBoxOrdenacaoPorDataLancamento.Checked;

            if (checkBoxOrdenacaoPorDataLancamento.Checked)
            {
                checkBoxOrdenacaoPorDataLancamento.ForeColor = Color.Red;
                //OrdenacaoPorOrdemCrescente
                if (!InicioTela)
                    OrdenacaoPorOrdemCrescente = (Mensagens.Pergunta("Organizar as datas ordem crescente?\n\n(Lançados primeiros em cima)") == DialogResult.Yes ? true : false);
            }
            else
            {
                checkBoxOrdenacaoPorDataLancamento.ForeColor = System.Drawing.SystemColors.Highlight;
            }

            checkBoxOrdenacaoPorNomeDestinatario.Checked = !checkBoxOrdenacaoPorDataLancamento.Checked;
        }

        private void checkBoxImprimirUmPorFolha_CheckedChanged(object sender, EventArgs e)
        {
            ImprimirUmPorFolha = checkBoxImprimirUmPorFolha.Checked;

            if (checkBoxImprimirUmPorFolha.Checked)
            {
                checkBoxImprimirUmPorFolha.ForeColor = Color.Red;
            }
            else
            {
                checkBoxImprimirUmPorFolha.ForeColor = System.Drawing.SystemColors.Highlight;
            }

            checkBoxImprimirVariosPorFolha.Checked = !checkBoxImprimirUmPorFolha.Checked;
        }

        private void checkBoxImprimirVariosPorFolha_CheckedChanged(object sender, EventArgs e)
        {
            ImprimirVariosPorFolha = checkBoxImprimirVariosPorFolha.Checked;

            if (checkBoxImprimirVariosPorFolha.Checked)
            {
                checkBoxImprimirVariosPorFolha.ForeColor = Color.Red;
            }
            else
            {
                checkBoxImprimirVariosPorFolha.ForeColor = System.Drawing.SystemColors.Highlight;
            }

            checkBoxImprimirUmPorFolha.Checked = !checkBoxImprimirVariosPorFolha.Checked;
        }

        private void FormularioImpressaoEntregaObjetosOpcoesImpressao2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnCancelar_Click(sender, e);
            }
            if (e.KeyCode == Keys.Enter)
            {
                btnConfirmar_Click(sender, e);
            }
        }

        private void FormularioImpressaoEntregaObjetosOpcoesImpressao2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ClicouNoConfirmar)
                Cancelou = false;
            else
                Cancelou = true;
        }
    }
}
