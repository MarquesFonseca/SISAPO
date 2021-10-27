﻿using SISAPO.ClassesDiversas;
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
        public bool SepararPorPrazo = false;
        public bool NaoSepararPorPrazo = false;
        public bool Cancelou = false;
        public bool ClicouNoConfirmar = false;
        public FormularioConsulta.ModeloImpressaoListaObjetos ModeloImpressaoListaObjetos;

        private bool InicioTela = true;

        public FormularioImpressaoEntregaObjetosOpcoesImpressao2(FormularioConsulta.ModeloImpressaoListaObjetos _modeloImpressaoListaObjetos)
        {
            InitializeComponent();
            ModeloImpressaoListaObjetos = _modeloImpressaoListaObjetos;
            checkBoxImprimirUmPorFolha.ForeColor = Color.Red;
            checkBoxSepararListaPorPrazo.ForeColor = Color.Red;



            //tabControl3.Visible = true;
            //this.Size = new Size(794, 491);
            if (Configuracoes.GerarTXTPLRNaLdi || Configuracoes.GerarQRCodePLRNaLdi)
            {
                tabControl3.TabPages[0].Text = "Escolha como deseja formar a PLR (Pré Lista de Remessa para outra unidade)";
                checkBoxSepararListaPorPrazo.Checked = true;
            }
            else
            {
                tabControl3.TabPages[0].Text = "Escolha como deseja formar a lista";
                checkBoxSepararListaPorPrazo.Checked = true;
            }
        }

        private void FormularioImpressaoEntregaObjetosOpcoesImpressao2_Load(object sender, EventArgs e)
        {
            //novo modelo - 13-05-2021
            if (ModeloImpressaoListaObjetos == FormularioConsulta.ModeloImpressaoListaObjetos.ModeloComum)
            {
                tabControl4.Visible = false;
            }
            comboBoxTipoOrdenacao.SelectedIndex = 0;
            comboBoxOrdemCrescenteDescrecente.SelectedIndex = 0;

            checkBoxImprimirUmPorFolha.ForeColor = Color.Red;

            InicioTela = false;
            tabControl2.Focus();
            tabControl2.SelecionaControle();
            SendKeys.Send("{TAB}");
        }

        private void FormularioImpressaoEntregaObjetosOpcoesImpressao_KeyDown(object sender, KeyEventArgs e)
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

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            ClicouNoConfirmar = true;
            OrdenacaoPorNomeDestinatario = comboBoxTipoOrdenacao.SelectedIndex == 0 ? true : false;
            OrdenacaoPorDataLancamento = comboBoxTipoOrdenacao.SelectedIndex == 1 ? true : false;
            OrdenacaoPorOrdemCrescente = comboBoxOrdemCrescenteDescrecente.SelectedIndex == 0 ? true : false;
            ImprimirUmPorFolha = checkBoxImprimirUmPorFolha.Checked;
            ImprimirVariosPorFolha = checkBoxImprimirVariosPorFolha.Checked;
            SepararPorPrazo = checkBoxSepararListaPorPrazo.Checked;
            NaoSepararPorPrazo = checkBoxNaoSepararPorPrazo.Checked;
            Cancelou = false;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelou = true;
            this.Close();
        }


        private void checkBoxImprimirUmPorFolha_CheckedChanged(object sender, EventArgs e)
        {
            ImprimirUmPorFolha = checkBoxImprimirUmPorFolha.Checked;
            ImprimirVariosPorFolha = !checkBoxImprimirUmPorFolha.Checked;

            if (checkBoxImprimirUmPorFolha.Checked)
            {
                checkBoxImprimirUmPorFolha.ForeColor = Color.Red;
            }
            else
            {
                checkBoxImprimirUmPorFolha.ForeColor = System.Drawing.SystemColors.ControlText;
            }

            //checkBoxImprimirVariosPorFolha.Checked = !checkBoxImprimirUmPorFolha.Checked;
        }

        private void checkBoxImprimirVariosPorFolha_CheckedChanged(object sender, EventArgs e)
        {
            ImprimirVariosPorFolha = checkBoxImprimirVariosPorFolha.Checked;
            ImprimirUmPorFolha = !checkBoxImprimirVariosPorFolha.Checked;

            if (checkBoxImprimirVariosPorFolha.Checked)
            {
                checkBoxImprimirVariosPorFolha.ForeColor = Color.Red;
            }
            else
            {
                checkBoxImprimirVariosPorFolha.ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }

        private void checkBoxSepararListaPorPrazo_CheckedChanged(object sender, EventArgs e)
        {
            SepararPorPrazo = checkBoxSepararListaPorPrazo.Checked;
            NaoSepararPorPrazo = !checkBoxSepararListaPorPrazo.Checked;

            if (checkBoxSepararListaPorPrazo.Checked)
            {
                checkBoxSepararListaPorPrazo.ForeColor = Color.Red;
            }
            else
            {
                checkBoxSepararListaPorPrazo.ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }

        private void checkBoxNaoSepararPorPrazo_CheckedChanged(object sender, EventArgs e)
        {
            NaoSepararPorPrazo = checkBoxNaoSepararPorPrazo.Checked;
            SepararPorPrazo = !checkBoxNaoSepararPorPrazo.Checked;

            if (checkBoxNaoSepararPorPrazo.Checked)
            {
                checkBoxNaoSepararPorPrazo.ForeColor = Color.Red;
            }
            else
            {
                checkBoxNaoSepararPorPrazo.ForeColor = System.Drawing.SystemColors.ControlText;
            }
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

        private void comboBoxTipoOrdenacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTipoOrdenacao.SelectedIndex == -1)
            {
                Mensagens.Informa("");
                comboBoxTipoOrdenacao.Focus();
                return;
            }
            if (comboBoxTipoOrdenacao.SelectedIndex == 0)//Ordenacao Por Nome do Cliente
            {
                OrdenacaoPorNomeDestinatario = true;
                OrdenacaoPorDataLancamento = false;
            }
            if (comboBoxTipoOrdenacao.SelectedIndex == 1)//ordenação por Data de Lançamento
            {
                OrdenacaoPorNomeDestinatario = false;
                OrdenacaoPorDataLancamento = true;
            }
        }

        private void comboBoxOrdemCrescenteDescrecente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxOrdemCrescenteDescrecente.SelectedIndex == -1)
            {
                Mensagens.Informa("");
                comboBoxOrdemCrescenteDescrecente.Focus();
                return;
            }
            if (comboBoxOrdemCrescenteDescrecente.SelectedIndex == 0) //Ordenação Crescente [A-Z]
            {
                OrdenacaoPorOrdemCrescente = true;
            }
            if (comboBoxOrdemCrescenteDescrecente.SelectedIndex == 1) //Ordenação Decrescente [Z-A]
            {
                OrdenacaoPorOrdemCrescente = false;
            }
        }

        private void comboBoxTipoOrdenacao_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void comboBoxOrdemCrescenteDescrecente_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void checkBoxImprimirUmPorFolha_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void checkBoxImprimirVariosPorFolha_KeyDown(object sender, KeyEventArgs e)
        {

        }


    }
}
