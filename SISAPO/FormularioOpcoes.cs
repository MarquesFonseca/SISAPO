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
    public partial class FormularioOpcoes : Form
    {
        public FormularioOpcoes()
        {
            InitializeComponent();
        }

        private void FormularioOpcoes_Load(object sender, EventArgs e)
        {
            this.tabelaConfiguracoesSistemaTableAdapter.Connection.ConnectionString = ClassesDiversas.Configuracoes.strConexao;
            this.tabelaConfiguracoesSistemaTableAdapter.Fill(this.dataSetConfiguracoes.TabelaConfiguracoesSistema);
            comboBoxSupEst.Text = ((DataRowView)bindingSourceTabelaConfiguracoesSistema.Current).Row["SuperintendenciaEstadual"].ToString().Replace("SE/", "");
            checkBoxExibirObjetosEmCaixaPostal.Checked = FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirCaixaPostalPesquisa_toolStripMenuItem.Checked;
            checkBoxExibirItensJaEntregues.Checked = FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirItensJaEntreguesToolStripMenuItem.Checked;
            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento) BtnMarcarTodosAtualizados.Visible = true;
            else BtnMarcarTodosAtualizados.Visible = false;
        }

        private void BtnRequererVerificacaoDeObjetosJaEntregues_Click(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem_Click(sender, e);
        }

        private void checkBoxExibirObjetosEmCaixaPostal_CheckedChanged(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirCaixaPostalPesquisa_toolStripMenuItem.Checked = checkBoxExibirObjetosEmCaixaPostal.Checked;
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirCaixaPostalPesquisa_toolStripMenuItem_Click(sender, e);
        }

        private void checkBoxExibirItensJaEntregues_CheckedChanged(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirItensJaEntreguesToolStripMenuItem.Checked = checkBoxExibirItensJaEntregues.Checked;
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirItensJaEntreguesToolStripMenuItem_Click(sender, e);
        }

        private void BtnMarcarTodosAtualizados_Click(object sender, EventArgs e)
        {
            DialogResult pergunta = Mensagens.Pergunta("Deseja realmente marcar/desmarcar objetos como atualizado?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (pergunta == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            if (pergunta == System.Windows.Forms.DialogResult.Yes)
            {
                //grava no banco de dados
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Atualizado = @Atualizado", new List<Parametros>(){
                                            new Parametros("@Atualizado", TipoCampo.Boolean, true)});
                }
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();
            }
        }

        private void FormularioOpcoes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            if (e.KeyCode == Keys.F9)
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().sRORastreamentoUnificadoToolStripMenuItem_Click(sender, e);
            }
            if (e.KeyCode == Keys.F12)
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().visualizarListaDeObjetosToolStripMenuItem_Click(sender, e);
            }
        }

        private void BtnAtualizarConfiguracoesAgencia_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNomeAgencia.Text))
                {
                    Mensagens.Erro("O campo Nome da Agência se encontra vazio.");
                    return;
                }
                if (string.IsNullOrEmpty(txtEnderecoAgencia.Text))
                {
                    Mensagens.Erro("O campo Endereço da Agência se encontra vazio.");
                    return;
                }
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                    dao.ExecutaSQL(string.Format("UPDATE TabelaConfiguracoesSistema SET NomeAgenciaLocal = @NomeAgenciaLocal, EnderecoAgenciaLocal = @EnderecoAgenciaLocal, SuperintendenciaEstadual = @SuperintendenciaEstadual, CepUnidade = @CepUnidade, CidadeAgenciaLocal = @CidadeAgenciaLocal, UFAgenciaLocal = @UFAgenciaLocal, TelefoneAgenciaLocal = @TelefoneAgenciaLocal, HorarioFuncionamentoAgenciaLocal = @HorarioFuncionamentoAgenciaLocal Where Codigo = @Codigo"), new List<Parametros>(){
                                            new Parametros("@NomeAgenciaLocal", TipoCampo.Text, txtNomeAgencia.Text),
                                            new Parametros("@EnderecoAgenciaLocal", TipoCampo.Text, txtEnderecoAgencia.Text),
                                            new Parametros("@SuperintendenciaEstadual", TipoCampo.Text, string.Format("{0}", comboBoxSupEst.Text)),
                                            new Parametros("@CepUnidade", TipoCampo.Text, txtCepUnidade.Text),

                                            new Parametros("@CidadeAgenciaLocal", TipoCampo.Text, txtCidadeAgenciaLocal.Text),
                                            new Parametros("@UFAgenciaLocal", TipoCampo.Text, comboBoxUFAgenciaLocal.Text),
                                            new Parametros("@TelefoneAgenciaLocal", TipoCampo.Text, txtTelefoneAgenciaLocal.Text),
                                            new Parametros("@HorarioFuncionamentoAgenciaLocal", TipoCampo.Text, txtHorarioFuncionamentoAgenciaLocal.Text),

                                            new Parametros("@Codigo", TipoCampo.Int, 2)});

                }
                Mensagens.Informa("Gravado com sucesso!", MessageBoxIcon.Information, MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
            }
        }

        
    }
}
