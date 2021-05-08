﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SISAPO.ClassesDiversas;

namespace SISAPO
{
    public partial class FormularioTiposPostais : Form
    {
        private BindingSource bsTiposPostais = null;
        private DataTable dtTiposPostais = null;
        //private string filtroAtual = string.Empty;

        public FormularioTiposPostais()
        {
            InitializeComponent();
        }

        private void FormularioTiposPostais_Load(object sender, EventArgs e)
        {
            comboBoxTipoClassificacao.SelectedIndex = 0;

            using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                dtTiposPostais = dao.RetornaDataTable("SELECT Codigo, Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao FROM TiposPostais");

                bsTiposPostais = new BindingSource();
                bsTiposPostais.DataSource = dtTiposPostais;
                dataGridView1.DataSource = bsTiposPostais;
                LbnQuantidadeRegistros.Text = bsTiposPostais.Count.ToString();
                SendKeys.Send("\t");
                SendKeys.Send("\t");

            }
        }

        private void BtnAdicionarNovosTiposPostais_Click(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().cadastrarNovosTiposPostaisToolStripMenuItem_Click(sender, e);
        }
        
        private void comboBoxTipoClassificacao_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bsTiposPostais == null) return;
            MontaFiltro();
        }
        
        private void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            if (bsTiposPostais == null) return;
            MontaFiltro();
        }

        private void MontaFiltro()
        {
            string CampoPesquisa = string.Empty;
            string CampoTipoClassificacao = string.Empty;

            if (TxtPesquisa.Text != "")
            {
                CampoPesquisa = string.Format("(Descricao like '%{0}%' OR Sigla like '%{0}%')", TxtPesquisa.Text.RemoveSimbolos().RemoveSpecialChars());

                if (comboBoxTipoClassificacao.SelectedItem.ToString().ToUpper() == "Todos".ToUpper())
                {
                    CampoTipoClassificacao = "";
                }
                if (comboBoxTipoClassificacao.SelectedItem.ToString().ToUpper() == "PAC".ToUpper())
                {
                    CampoTipoClassificacao = " AND (TipoClassificacao = 'PAC')";
                }
                if (comboBoxTipoClassificacao.SelectedItem.ToString().ToUpper() == "SEDEX".ToUpper())
                {
                    CampoTipoClassificacao = " AND (TipoClassificacao = 'SEDEX')";
                }
                if (comboBoxTipoClassificacao.SelectedItem.ToString().ToUpper() == "Diversos".ToUpper())
                {
                    CampoTipoClassificacao = " AND (TipoClassificacao = 'Diversos')";
                }
                if (comboBoxTipoClassificacao.SelectedItem.ToString().ToUpper() == "SEM CLASSIFICAÇÃO".ToUpper())
                {
                    CampoTipoClassificacao = " AND (TipoClassificacao = 'SEM CLASSIFICAÇÃO')";
                }
            }
            else
            {
                CampoPesquisa = "";
                if (comboBoxTipoClassificacao.SelectedItem.ToString().ToUpper() == "Todos".ToUpper())
                {
                    CampoTipoClassificacao = "";
                }
                if (comboBoxTipoClassificacao.SelectedItem.ToString().ToUpper() == "PAC".ToUpper())
                {
                    CampoTipoClassificacao = "(TipoClassificacao = 'PAC')";
                }
                if (comboBoxTipoClassificacao.SelectedItem.ToString().ToUpper() == "SEDEX".ToUpper())
                {
                    CampoTipoClassificacao = "(TipoClassificacao = 'SEDEX')";
                }
                if (comboBoxTipoClassificacao.SelectedItem.ToString().ToUpper() == "Diversos".ToUpper())
                {
                    CampoTipoClassificacao = "(TipoClassificacao = 'Diversos')";
                }
                if (comboBoxTipoClassificacao.SelectedItem.ToString().ToUpper() == "SEM CLASSIFICAÇÃO".ToUpper())
                {
                    CampoTipoClassificacao = "(TipoClassificacao = 'SEM CLASSIFICAÇÃO')";
                }
            }

            string resultado = string.Format("{0}{1}", CampoPesquisa, CampoTipoClassificacao);

            bsTiposPostais.Filter = resultado;

            LbnQuantidadeRegistros.Text = bsTiposPostais.Count.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            TxtPesquisa.Text = "";
            TxtPesquisa.SelectAll();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TxtPesquisa.Text = "";
            TxtPesquisa.Focus();
        }

        Dictionary<string, string> selecionados = new Dictionary<string, string>();
        private void acoesAlterarPrazoTodosSelecionadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selecionados = new Dictionary<string, string>();
            if (dataGridView1.SelectedRows.Count == 0)
            {
                Mensagens.Informa("Para impressão da lista de entrega é necessário selecionar algum objeto.");
                return;
            }

            for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                bool existe = selecionados.AsEnumerable().Any(t => t.Key == this.dataGridView1.SelectedRows[i].Cells["Codigo"].Value.ToString());
                if (!existe)
                    selecionados.Add(this.dataGridView1.SelectedRows[i].Cells["Codigo"].Value.ToString(), this.dataGridView1.SelectedRows[i].Cells["Sigla"].Value.ToString());
            }

            using (FormularioAlteracaoTiposPostaisMassa formularioAlteracaoTiposPostaisMassa = new FormularioAlteracaoTiposPostaisMassa(selecionados))
            {
                formularioAlteracaoTiposPostaisMassa.ShowDialog();
            }
            selecionados = new Dictionary<string, string>();


            using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                dtTiposPostais = dao.RetornaDataTable("SELECT Codigo, Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao FROM TiposPostais");
                bsTiposPostais = new BindingSource();
                bsTiposPostais.DataSource = dtTiposPostais;
                dataGridView1.DataSource = bsTiposPostais;
                LbnQuantidadeRegistros.Text = bsTiposPostais.Count.ToString();
            }

            FormularioPrincipal.TiposPostais = Configuracoes.RetornaTiposPostais();

            MontaFiltro();
        }

        private void FormularioTiposPostais_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }            
        }
    }
}
