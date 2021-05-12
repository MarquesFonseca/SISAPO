using SISAPO;
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
    public partial class FormularioAlteracaoTiposPostaisMassa : Form
    {
        WaitWndFun waitForm = new WaitWndFun();
        private Dictionary<string, string> selecionados;

        public FormularioAlteracaoTiposPostaisMassa()
        {
            InitializeComponent();
        }

        public FormularioAlteracaoTiposPostaisMassa(Dictionary<string, string> _selecionados)
        {
            InitializeComponent();
            this.selecionados = _selecionados;
        }

        private void FormularioAlteracaoTiposPostaisMassa_Load(object sender, EventArgs e)
        {
            label5.Text = string.Format("Esta ação afetará o prazo de todos os '{0}' tipos postais selecionados. Se deseja esta alteração, após redefinir, clique em Gravar.", selecionados.Count);
            DataInicial_dateTimePicker.Text = DateTime.Now.Date.ToShortDateString();
            DataInicial_dateTimePicker.Enabled = false;
        }

        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (comboBoxTipoClassificacao.SelectedIndex == -1)
            {
                Mensagens.Erro("Selecione uma classificação para os selecionados.");
                return;
            }

            //grava aqui no banco..
            AtualizaTodosSelecionados();

            this.Close();
        }

        void AtualizaTodosSelecionados()
        {
            //PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao

#if !DEBUG
            waitForm.Show(this);
#endif

            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                foreach (var item in selecionados)
                {
                    List<Parametros> pr = new List<Parametros>() {
                            new Parametros() { Nome = "@PrazoDestinoCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoDestinatarioCaidaPedidaUpDown.Value.ToString() },
                            new Parametros() { Nome = "@PrazoDestinoCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoDestinatarioCaixaPostalUpDown.Value.ToString() },
                            new Parametros() { Nome = "@PrazoRemetenteCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaidaPedidaUpDown.Value.ToString() },
                            new Parametros() { Nome = "@PrazoRemetenteCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaixaPostalUpDown.Value.ToString() },
                            new Parametros() { Nome = "@TipoClassificacao", Tipo = TipoCampo.Text, Valor = comboBoxTipoClassificacao.SelectedItem.ToString() },
                            new Parametros() { Nome = "@DataAlteracao", Tipo = TipoCampo.Text, Valor = DateTime.Now.ToString() },
                            new Parametros() { Nome = "@Codigo", Tipo = TipoCampo.Int, Valor = item.Key.ToInt() },
                            new Parametros() { Nome = "@Sigla", Tipo = TipoCampo.Text, Valor = item.Value.ToString() }
                        };
                    dao.ExecutaSQL("UPDATE TiposPostais SET PrazoDestinoCaidaPedida = @PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal = @PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida = @PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal = @PrazoRemetenteCaixaPostal, TipoClassificacao = @TipoClassificacao, DataAlteracao = @DataAlteracao  WHERE (Codigo = @Codigo AND Sigla = @Sigla)", pr);


                    List<Parametros> prSigla = new List<Parametros>() {
                            new Parametros() { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Int, Valor = false },
                            new Parametros() { Nome = "@TipoPostalSiglaCodigo", Tipo = TipoCampo.Text, Valor = item.Value.ToString() }
                        };
                    DataSet DsObjetosNaoEntreguesMesmoTipoSigla = dao.RetornaDataSet("SELECT Codigo, CodigoObjeto, NomeCliente, ObjetoEntregue, CaixaPostal, Comentario, TipoPostalPrazoDiasCorridosRegulamentado FROM TabelaObjetosSROLocal WHERE ObjetoEntregue = @ObjetoEntregue AND Left(CodigoObjeto, 2) = @TipoPostalSiglaCodigo", prSigla);
                    if (DsObjetosNaoEntreguesMesmoTipoSigla.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow itemObjetoNaoEntregue in DsObjetosNaoEntreguesMesmoTipoSigla.Tables[0].Rows)
                        {
                            //verificar se é caixa postal 
                            //verificar se é remetente
                            bool AoRemetente = false;
                            bool ClienteCaixaPostal = false;

                            string CodigoObjetoAtual = itemObjetoNaoEntregue["CodigoObjeto"].ToString();
                            string NomeClienteAtual = itemObjetoNaoEntregue["NomeCliente"].ToString();
                            string ComentarioAtual = itemObjetoNaoEntregue["Comentario"].ToString();

                            if (NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REMETENTE") ||
                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REMETENT") ||
                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REMETEN") ||
                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REMETE") ||
                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REMET") ||
                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REME") ||
                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("REM") ||
                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("DEVOLUCAO") ||
                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("DEVOLUCA") ||
                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("DEVOLUC") ||
                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("DEVOLU") ||
                                NomeClienteAtual.ToUpper().RemoveAcentos().Contains("ORIGEM"))
                            {
                                AoRemetente = true;
                            }

                            if (ComentarioAtual.ToUpper().RemoveAcentos().Contains("REMETENTE") ||
                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("REMETENT") ||
                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("REMETEN") ||
                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("REMETE") ||
                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("REMET") ||
                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("REME") ||
                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("REM") ||
                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("DEVOLUCAO") ||
                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("DEVOLUCA") ||
                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("DEVOLUC") ||
                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("DEVOLU") ||
                                ComentarioAtual.ToUpper().RemoveAcentos().Contains("ORIGEM"))
                            {
                                AoRemetente = true;
                            }

                            if (Convert.ToBoolean(itemObjetoNaoEntregue["CaixaPostal"]))
                            {
                                ClienteCaixaPostal = true;
                            }

                            if (AoRemetente) // ao remetente
                            {
                                if (ClienteCaixaPostal)
                                {
                                    List<Parametros> pr1 = new List<Parametros>() {
                                        new Parametros() { Nome = "@PrazoRemetenteCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaixaPostalUpDown.Value.ToString() },
                                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual.ToString() },
                                        new Parametros() { Nome = "@DataInicial_dateTimePicker", Tipo = TipoCampo.Text, Valor = DataInicial_dateTimePicker.Text.ToString() }
                                    };
                                    if (radioButtonAPartirDoDia.Checked)
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoRemetenteCaixaPostal WHERE (CodigoObjeto = @CodigoObjeto) AND format(DataLancamento, 'dd/MM/yyyy') >= @DataInicial_dateTimePicker", pr1);
                                    else
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoRemetenteCaixaPostal WHERE (CodigoObjeto = @CodigoObjeto)", pr1);
                                }
                                else // não é caixa postal
                                {
                                    List<Parametros> pr2 = new List<Parametros>() {
                                        new Parametros() { Nome = "@PrazoRemetenteCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoRemetenteCaidaPedidaUpDown.Value.ToString() },
                                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual.ToString() },
                                        new Parametros() { Nome = "@DataInicial_dateTimePicker", Tipo = TipoCampo.Text, Valor = DataInicial_dateTimePicker.Text.ToString() }
                                    };
                                    if (radioButtonAPartirDoDia.Checked)
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoRemetenteCaidaPedida WHERE (CodigoObjeto = @CodigoObjeto) AND format(DataLancamento, 'dd/MM/yyyy') >= @DataInicial_dateTimePicker", pr2);
                                    else
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoRemetenteCaidaPedida WHERE (CodigoObjeto = @CodigoObjeto)", pr2);
                                }
                            }
                            else // ao destinatário
                            {
                                if (ClienteCaixaPostal)
                                {
                                    List<Parametros> pr3 = new List<Parametros>() {
                                        new Parametros() { Nome = "@PrazoDestinoCaixaPostal", Tipo = TipoCampo.Text, Valor = PrazoDestinatarioCaixaPostalUpDown.Value.ToString() },
                                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual.ToString() },
                                        new Parametros() { Nome = "@DataInicial_dateTimePicker", Tipo = TipoCampo.Text, Valor = DataInicial_dateTimePicker.Text.ToString() }
                                    };
                                    if (radioButtonAPartirDoDia.Checked)
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoDestinoCaixaPostal WHERE (CodigoObjeto = @CodigoObjeto) AND format(DataLancamento, 'dd/MM/yyyy') >= @DataInicial_dateTimePicker", pr3);
                                    else
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoDestinoCaixaPostal WHERE (CodigoObjeto = @CodigoObjeto)", pr3);
                                }
                                else // nao é caixa postal
                                {
                                    List<Parametros> pr4 = new List<Parametros>() {
                                        new Parametros() { Nome = "@PrazoDestinoCaidaPedida", Tipo = TipoCampo.Text, Valor = PrazoDestinatarioCaidaPedidaUpDown.Value.ToString() },
                                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual.ToString() },
                                        new Parametros() { Nome = "@DataInicial_dateTimePicker", Tipo = TipoCampo.Text, Valor = DataInicial_dateTimePicker.Text.ToString() }
                                    };
                                    if (radioButtonAPartirDoDia.Checked)
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoDestinoCaidaPedida WHERE (CodigoObjeto = @CodigoObjeto) AND format(DataLancamento, 'dd/MM/yyyy') >= @DataInicial_dateTimePicker", pr4);
                                    else
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoDestinoCaidaPedida WHERE (CodigoObjeto = @CodigoObjeto)", pr4);
                                }
                            }
                        }
                    }
                }
            }

#if !DEBUG
            waitForm.Close();
#endif
        }

        private void FormularioAlteracaoTiposPostaisMassa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyData == Keys.F5)
            {
                this.BtnGravar_Click(sender, e);
            }
        }

        private void radioButtonAPartirDoDia_CheckedChanged(object sender, EventArgs e)
        {
            DataInicial_dateTimePicker.Enabled = radioButtonAPartirDoDia.Checked;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
