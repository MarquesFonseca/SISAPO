﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SISAPO.ClassesDiversas;

namespace SISAPO
{
    public partial class FormularioAuxilioGestaoDiaNovo : Form
    {
        DataTable listaObjetos = new DataTable();
        string MontaFiltro = string.Empty;

        public FormularioAuxilioGestaoDiaNovo()
        {
            InitializeComponent();
            DataTable listaObjetos = new DataTable();
        }

        private void FormularioAuxilioGestaoDia_Load(object sender, EventArgs e)
        {
            //BtnColarConteudoJaCopiado_Click(sender, e);
            BtnColarConteudoJaCopiado.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ObjetosComPrazoGuardaVencido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void BtnColarConteudoJaCopiado_Click(object sender, EventArgs e)
        {
            string curDir = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeArquivo = string.Format("GestaoDoDia.txt");
            string nomeEnderecoArquivo = string.Format(@"{0}\{1}", curDir, nomeArquivo);
            StringBuilder textoColadoAreaTransferencia = new StringBuilder();

            //SE NÃO ESTIVER VAZIO ENTRA
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) ||
                string.IsNullOrEmpty(Clipboard.GetDataObject().GetDataPresent(DataFormats.Text).ToString()))
            {
                textoColadoAreaTransferencia = textoColadoAreaTransferencia.Append(Clipboard.GetDataObject().GetData(DataFormats.Text).ToString());

                //grava texto no arquivo
                using (Arquivos arq = new Arquivos())
                {
                    arq.GravarArquivo(nomeEnderecoArquivo, textoColadoAreaTransferencia.ToString());
                }

                try
                {
                    string texto = textoColadoAreaTransferencia.ToString();

                    listaObjetos = RetornaListaObjetos(textoColadoAreaTransferencia.ToString());


                    if (listaObjetos == null || listaObjetos.Rows.Count == 0)
                    {
                        FiltrarPorPrazosVENCIDOSCheckBox.Enabled = FiltrarPorPrazosVENCENDOHOJECheckBox.Enabled = FiltrarPorPrazosAVENCERCheckBox.Enabled = false;
                        FiltrarPorClassificacaoPACCCheckBox.Enabled = FiltrarPorClassificacaoSEDEXCheckBox.Enabled = FiltrarPorClassificacaoDIVERSOSCheckBox.Enabled = false;

                        dataGridView1.DataSource = listaObjetos;
                        listaObjetos.Clear(); //Retira os valores da tabela mantendo os campos
                        Mensagens.Informa("Não foi possível carregar.\nCopie a lista e clique no botão para tentar novamente .");
                        LbnQuantidadeRegistros.Text = string.Format("{0}", listaObjetos.Rows.Count);
                        return;
                    }

                    FiltrarPorPrazosVENCIDOSCheckBox.Enabled = FiltrarPorPrazosVENCENDOHOJECheckBox.Enabled = FiltrarPorPrazosAVENCERCheckBox.Enabled = true;
                    FiltrarPorPrazosVENCIDOSCheckBox.Checked = true;
                    FiltrarPorClassificacaoPACCCheckBox.Enabled = FiltrarPorClassificacaoSEDEXCheckBox.Enabled = FiltrarPorClassificacaoDIVERSOSCheckBox.Enabled = true;
                    FiltrarPorClassificacaoPACCCheckBox.Checked = FiltrarPorClassificacaoSEDEXCheckBox.Checked = FiltrarPorClassificacaoDIVERSOSCheckBox.Checked = true;

                    bindingSourceObjetosNaoEntregues = new BindingSource();
                    bindingSourceObjetosNaoEntregues.DataSource = listaObjetos;
                    dataGridView1.DataSource = bindingSourceObjetosNaoEntregues;

                    FiltrosCheckBox();

                    //LbnQuantidadeRegistros.Text = bindingSourceObjetosNaoEntregues.Count.ToString();
                    //dataGridView1.Focus();
                }
                catch (IOException) { }
            }
        }

        private DataTable RetornaListaObjetos(string Texto)
        {
            DataTable dtbLista = new DataTable();
            dtbLista.Columns.Add("CodigoLdi", typeof(string));
            dtbLista.Columns.Add("Sigla", typeof(string));
            dtbLista.Columns.Add("CodigoObjeto", typeof(string));
            dtbLista.Columns.Add("TipoClassificacao", typeof(string));
            dtbLista.Columns.Add("NomeCliente", typeof(string));
            dtbLista.Columns.Add("DataLancamento", typeof(DateTime));
            dtbLista.Columns.Add("QtdDiasCorridos", typeof(string));
            dtbLista.Columns.Add("PrazoTipoClassificacao", typeof(int));
            dtbLista.Columns.Add("DataVencimento", typeof(DateTime));
            dtbLista.Columns.Add("StatusPrazo", typeof(string));
            dtbLista.Columns.Add("QtdDiasVencidos", typeof(string));


            try
            {
                string[] linha = Texto.Split('\n');

                for (int i = 0; i < linha.Length; i++)
                {
                    if (linha[i] == "" || linha[i] == "\r") continue;
                    string[] Parteslinha = linha[i].Split('\t');
                    string ParteLinhaCodigoLdi = Parteslinha.Length >= 1 ? Parteslinha[0].Trim().ToUpper() : "";
                    string ParteLinhaCodigoObjeto = Parteslinha.Length >= 2 ? Parteslinha[1].Trim().ToUpper() : "";
                    //string ParteLinhaDataLancamento = Parteslinha.Length >= 3 ? Parteslinha[2].Trim().ToUpper() : "";
                    //string QtdDiasCorridos = "0";
                    //bool validaData;
                    //try
                    //{
                    //    Convert.ToDateTime(ParteLinhaDataLancamento);
                    //    validaData = true;
                    //}
                    //catch (Exception)
                    //{
                    //    validaData = false;
                    //}
                    //if (validaData)
                    //{
                    //    QtdDiasCorridos = Convert.ToString((DateTime.Now.Date - ParteLinhaDataLancamento.ToDateTime().Date).TotalDays);
                    //}


                    if (ParteLinhaCodigoObjeto != "")
                    {
                        using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                        {
                            if (!DAO.TestaConexao(ClassesDiversas.Configuracoes.strConexao, TipoBanco.OleDb)) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return null; }

                            //ParteLinhaCodigoObjeto = "JU939963115BR";

                            DataTable RetornaLista = RetornaListaObjetosNaoEntregues(string.Format("CodigoObjeto = '{0}' AND CodigoLdi = '{1}'", ParteLinhaCodigoObjeto, ParteLinhaCodigoLdi));
                            if (RetornaLista.Rows.Count == 0)
                            {
                                continue;
                            }
                            else
                            {
                                //CodigoLdi, Sigla, CodigoObjeto, TipoClassificacao, NomeCliente, DataLancamento, QtdDiasCorridos, PrazoTipoClassificacao, DataVencimento, StatusPrazo, QtdDiasVencidos

                                dtbLista.Rows.Add(RetornaLista.Rows[0]["CodigoLdi"], RetornaLista.Rows[0]["Sigla"], RetornaLista.Rows[0]["CodigoObjeto"], RetornaLista.Rows[0]["TipoClassificacao"], RetornaLista.Rows[0]["NomeCliente"], RetornaLista.Rows[0]["DataLancamento"], RetornaLista.Rows[0]["QtdDiasCorridos"], RetornaLista.Rows[0]["PrazoTipoClassificacao"], RetornaLista.Rows[0]["DataVencimento"], RetornaLista.Rows[0]["StatusPrazo"], RetornaLista.Rows[0]["QtdDiasVencidos"]);
                            }
                        }
                    }
                }
                return dtbLista;
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
                return dtbLista;
            }
        }

        private DataTable RetornaListaObjetosNaoEntregues()
        {
            DataTable dtbLista = new DataTable();
            dtbLista.Columns.Add("CodigoLdi", typeof(string));
            dtbLista.Columns.Add("Sigla", typeof(string));
            dtbLista.Columns.Add("CodigoObjeto", typeof(string));
            dtbLista.Columns.Add("TipoClassificacao", typeof(string));
            dtbLista.Columns.Add("NomeCliente", typeof(string));
            dtbLista.Columns.Add("DataLancamento", typeof(DateTime));
            dtbLista.Columns.Add("QtdDiasCorridos", typeof(string));
            dtbLista.Columns.Add("PrazoTipoClassificacao", typeof(int));
            dtbLista.Columns.Add("DataVencimento", typeof(DateTime));
            dtbLista.Columns.Add("StatusPrazo", typeof(string));
            dtbLista.Columns.Add("QtdDiasVencidos", typeof(string));

            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return null; }

                    StringBuilder stringSQL = new StringBuilder();
                    stringSQL.AppendLine("SELECT                                                                                                                                                                                                                                                                   ");
                    stringSQL.AppendLine(" ObjetosNaoEntregues.CodigoLdi                                                                                                                                                                                                                                           ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.Sigla                                                                                                                                                                                                                                               ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.CodigoObjeto                                                                                                                                                                                                                                        ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.TipoClassificacao                                                                                                                                                                                                                                   ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.NomeCliente                                                                                                                                                                                                                                         ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.DataLancamento                                                                                                                                                                                                                                      ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.QtdDiasCorridos                                                                                                                                                                                                                                     ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.PrazoTipoClassificacao                                                                                                                                                                                                                              ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.DataVencimento                                                                                                                                                                                                                                      ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.StatusPrazo                                                                                                                                                                                                                                         ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.QtdDiasVencidos                                                                                                                                                                                                                                     ");
                    stringSQL.AppendLine("FROM (                                                                                                                                                                                                                                                                   ");
                    stringSQL.AppendLine("SELECT                                                                                                                                                                                                                                                                   ");
                    stringSQL.AppendLine(" TabelaObjetosSROLocal.CodigoLdi                                                                                                                                                                                                                                         ");
                    stringSQL.AppendLine(",Left(TabelaObjetosSROLocal.CodigoObjeto, 2) AS Sigla                                                                                                                                                                                                                    ");
                    stringSQL.AppendLine(",TabelaObjetosSROLocal.CodigoObjeto                                                                                                                                                                                                                                      ");
                    stringSQL.AppendLine(",(SELECT TiposPostais.TipoClassificacao from TiposPostais WHERE TiposPostais.Sigla = Left(TabelaObjetosSROLocal.CodigoObjeto, 2)) AS TipoClassificacao                                                                                                                   ");
                    stringSQL.AppendLine(",TabelaObjetosSROLocal.NomeCliente                                                                                                                                                                                                                                       ");
                    stringSQL.AppendLine(",FORMAT(TabelaObjetosSROLocal.DataLancamento, \"dd/MM/yyyy\") AS DataLancamento                                                                                                                                                                                            ");
                    stringSQL.AppendLine(",DATEDIFF(\"d\", TabelaObjetosSROLocal.DataLancamento, Now()) AS QtdDiasCorridos                                                                                                                                                                                           ");
                    stringSQL.AppendLine(",IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado Is Null, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado) AS PrazoTipoClassificacao                                                                                                ");
                    stringSQL.AppendLine(",FORMAT(DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado Is Null, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), TabelaObjetosSROLocal.DataLancamento),\"dd/MM/yyyy\") as DataVencimento                               ");
                    stringSQL.AppendLine(",SWITCH                                                                                                                                                                                                                                                                  ");
                    stringSQL.AppendLine("(                                                                                                                                                                                                                                                                        ");
                    stringSQL.AppendLine("	DATEDIFF(\"d\", FORMAT(DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado Is Null, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), TabelaObjetosSROLocal.DataLancamento),\"dd/MM/yyyy\"), Now()) = 0 , \"VENCENDO HOJE\",   ");
                    stringSQL.AppendLine("	DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado Is Null, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), TabelaObjetosSROLocal.DataLancamento) < NOW() , \"VENCIDO\",                                                ");
                    stringSQL.AppendLine("	DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado Is Null, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), TabelaObjetosSROLocal.DataLancamento) > NOW() , \"A VENCER\"                                                ");
                    stringSQL.AppendLine(") AS StatusPrazo                                                                                                                                                                                                                                                         ");
                    stringSQL.AppendLine(",DATEDIFF(\"d\", FORMAT(DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado Is Null, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), TabelaObjetosSROLocal.DataLancamento),\"dd/MM/yyyy\"), Now()) AS QtdDiasVencidos        ");
                    stringSQL.AppendLine("FROM TabelaObjetosSROLocal                                                                                                                                                    ");
                    stringSQL.AppendLine("WHERE (TabelaObjetosSROLocal.ObjetoEntregue = FALSE)                                                                                                                                                                                                                     ");
                    stringSQL.AppendLine(") AS ObjetosNaoEntregues order BY ObjetosNaoEntregues.QtdDiasCorridos DESC, ObjetosNaoEntregues.TipoClassificacao ASC                                                                               ");

                    dtbLista = dao.RetornaDataTable(stringSQL.ToString());

                    bindingSourceObjetosNaoEntregues = new BindingSource();
                    bindingSourceObjetosNaoEntregues.DataSource = dtbLista;
                    dataGridView1.DataSource = bindingSourceObjetosNaoEntregues;
                }
                return dtbLista;
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
                return dtbLista;
            }
        }

        private DataTable RetornaListaObjetosNaoEntregues(string filtrosAdicionais)
        {
            DataTable dtbLista = new DataTable();
            dtbLista.Columns.Add("CodigoLdi", typeof(string));
            dtbLista.Columns.Add("Sigla", typeof(string));
            dtbLista.Columns.Add("CodigoObjeto", typeof(string));
            dtbLista.Columns.Add("TipoClassificacao", typeof(string));
            dtbLista.Columns.Add("NomeCliente", typeof(string));
            dtbLista.Columns.Add("DataLancamento", typeof(DateTime));
            dtbLista.Columns.Add("QtdDiasCorridos", typeof(string));
            dtbLista.Columns.Add("PrazoTipoClassificacao", typeof(int));
            dtbLista.Columns.Add("DataVencimento", typeof(DateTime));
            dtbLista.Columns.Add("StatusPrazo", typeof(string));
            dtbLista.Columns.Add("QtdDiasVencidos", typeof(string));

            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return null; }

                    StringBuilder stringSQL = new StringBuilder();
                    stringSQL.AppendLine("SELECT                                                                                                                                                                                                                                                                   ");
                    stringSQL.AppendLine(" ObjetosNaoEntregues.CodigoLdi                                                                                                                                                                                                                                           ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.Sigla                                                                                                                                                                                                                                               ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.CodigoObjeto                                                                                                                                                                                                                                        ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.TipoClassificacao                                                                                                                                                                                                                                   ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.NomeCliente                                                                                                                                                                                                                                         ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.DataLancamento                                                                                                                                                                                                                                      ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.QtdDiasCorridos                                                                                                                                                                                                                                     ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.PrazoTipoClassificacao                                                                                                                                                                                                                              ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.DataVencimento                                                                                                                                                                                                                                      ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.StatusPrazo                                                                                                                                                                                                                                         ");
                    stringSQL.AppendLine(",ObjetosNaoEntregues.QtdDiasVencidos                                                                                                                                                                                                                                     ");
                    stringSQL.AppendLine("FROM (                                                                                                                                                                                                                                                                   ");
                    stringSQL.AppendLine("SELECT                                                                                                                                                                                                                                                                   ");
                    stringSQL.AppendLine(" TabelaObjetosSROLocal.CodigoLdi                                                                                                                                                                                                                                         ");
                    stringSQL.AppendLine(",Left(TabelaObjetosSROLocal.CodigoObjeto, 2) AS Sigla                                                                                                                                                                                                                    ");
                    stringSQL.AppendLine(",TabelaObjetosSROLocal.CodigoObjeto                                                                                                                                                                                                                                      ");
                    stringSQL.AppendLine(",(SELECT TiposPostais.TipoClassificacao from TiposPostais WHERE TiposPostais.Sigla = Left(TabelaObjetosSROLocal.CodigoObjeto, 2)) AS TipoClassificacao                                                                                                                   ");
                    stringSQL.AppendLine(",TabelaObjetosSROLocal.NomeCliente                                                                                                                                                                                                                                       ");
                    stringSQL.AppendLine(",FORMAT(TabelaObjetosSROLocal.DataLancamento, \"dd/MM/yyyy\") AS DataLancamento                                                                                                                                                                                            ");
                    stringSQL.AppendLine(",DATEDIFF(\"d\", TabelaObjetosSROLocal.DataLancamento, Now()) AS QtdDiasCorridos                                                                                                                                                                                           ");
                    stringSQL.AppendLine(",IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado Is Null, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado) AS PrazoTipoClassificacao                                                                                                ");
                    stringSQL.AppendLine(",FORMAT(DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado Is Null, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), TabelaObjetosSROLocal.DataLancamento),\"dd/MM/yyyy\") as DataVencimento                               ");
                    stringSQL.AppendLine(",SWITCH                                                                                                                                                                                                                                                                  ");
                    stringSQL.AppendLine("(                                                                                                                                                                                                                                                                        ");
                    stringSQL.AppendLine("	DATEDIFF(\"d\", FORMAT(DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado Is Null, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), TabelaObjetosSROLocal.DataLancamento),\"dd/MM/yyyy\"), Now()) = 0 , \"VENCENDO HOJE\",   ");
                    stringSQL.AppendLine("	DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado Is Null, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), TabelaObjetosSROLocal.DataLancamento) < NOW() , \"VENCIDO\",                                                ");
                    stringSQL.AppendLine("	DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado Is Null, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), TabelaObjetosSROLocal.DataLancamento) > NOW() , \"A VENCER\"                                                ");
                    stringSQL.AppendLine(") AS StatusPrazo                                                                                                                                                                                                                                                         ");
                    stringSQL.AppendLine(",DATEDIFF(\"d\", FORMAT(DATEADD(\"d\", IIf(TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado Is Null, 0, TabelaObjetosSROLocal.TipoPostalPrazoDiasCorridosRegulamentado), TabelaObjetosSROLocal.DataLancamento),\"dd/MM/yyyy\"), Now()) AS QtdDiasVencidos        ");
                    stringSQL.AppendLine("FROM TabelaObjetosSROLocal                                                                                                                                                    ");
                    stringSQL.AppendLine("WHERE (TabelaObjetosSROLocal.ObjetoEntregue = FALSE)");
                    stringSQL.AppendLine(") AS ObjetosNaoEntregues ");
                    stringSQL.AppendLine("WHERE " + filtrosAdicionais.Replace("'", "\"").Replace("StatusPrazo", "ObjetosNaoEntregues.StatusPrazo").Replace("TipoClassificacao", "ObjetosNaoEntregues.TipoClassificacao"));
                    //stringSQL.AppendLine("ORDER BY ObjetosNaoEntregues.QtdDiasCorridos DESC, ObjetosNaoEntregues.TipoClassificacao ASC");

                    string resulteste = stringSQL.ToString();

                    dtbLista = dao.RetornaDataTable(stringSQL.ToString());
                }
                return dtbLista;
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
                return dtbLista;
            }
        }

        private void BtnImprimirListaAtual_Click(object sender, EventArgs e)
        {
            DataTable listaObjetosListaImpressao = RetornaListaObjetosNaoEntregues(MontaFiltro);

            if (listaObjetosListaImpressao.Rows.Count == 0) return;

            listaObjetos = listaObjetosListaImpressao.AsEnumerable().OrderBy(r => r.Field<string>("NomeCliente")).CopyToDataTable();

            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioImpressaoAuxilioGestaoDia")
                {
                    item.Close();
                    break;
                }
            }

            //FormularioImpressaoAuxilioGestaoDia formularioImpressaoAuxilioGestaoDia = new FormularioImpressaoAuxilioGestaoDia(novoCodigoSelecionados);
            FormularioImpressaoAuxilioGestaoDia formularioImpressaoAuxilioGestaoDia = new FormularioImpressaoAuxilioGestaoDia(listaObjetos);
            formularioImpressaoAuxilioGestaoDia.MdiParent = MdiParent;
            formularioImpressaoAuxilioGestaoDia.Show();
            formularioImpressaoAuxilioGestaoDia.WindowState = FormWindowState.Normal;
            formularioImpressaoAuxilioGestaoDia.WindowState = FormWindowState.Maximized;
            formularioImpressaoAuxilioGestaoDia.Activate();
        }

        private void BtnRetornaTodosNaoEntregues_Click(object sender, EventArgs e)
        {
            try
            {
                listaObjetos = RetornaListaObjetosNaoEntregues();
                if (listaObjetos == null || listaObjetos.Rows.Count == 0)
                {
                    FiltrarPorPrazosVENCIDOSCheckBox.Enabled = FiltrarPorPrazosVENCENDOHOJECheckBox.Enabled = FiltrarPorPrazosAVENCERCheckBox.Enabled = false;
                    FiltrarPorClassificacaoPACCCheckBox.Enabled = FiltrarPorClassificacaoSEDEXCheckBox.Enabled = FiltrarPorClassificacaoDIVERSOSCheckBox.Enabled = false;
                    //Mensagens.Informa("Não foi possível carregar.\nCopie a lista e clique no botão para tentar novamente ."); 
                    return;
                }

                FiltrarPorPrazosVENCIDOSCheckBox.Enabled = FiltrarPorPrazosVENCENDOHOJECheckBox.Enabled = FiltrarPorPrazosAVENCERCheckBox.Enabled = true;
                FiltrarPorPrazosVENCIDOSCheckBox.Checked = true;
                FiltrarPorClassificacaoPACCCheckBox.Enabled = FiltrarPorClassificacaoSEDEXCheckBox.Enabled = FiltrarPorClassificacaoDIVERSOSCheckBox.Enabled = true;
                FiltrarPorClassificacaoPACCCheckBox.Checked = FiltrarPorClassificacaoSEDEXCheckBox.Checked = FiltrarPorClassificacaoDIVERSOSCheckBox.Checked = true;

                bindingSourceObjetosNaoEntregues = new BindingSource();
                bindingSourceObjetosNaoEntregues.DataSource = listaObjetos;
                dataGridView1.DataSource = bindingSourceObjetosNaoEntregues;

                FiltrosCheckBox();
            }
            catch (IOException) { }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null) return;
            string CodigoObjeto = this.dataGridView1.CurrentRow.Cells["CodigoObjeto"].Value.ToString();
            using (FormularioSRORastreamentoUnificado formularioSRORastreamentoUnificado = new FormularioSRORastreamentoUnificado(CodigoObjeto))
            {
                formularioSRORastreamentoUnificado.WindowState = FormWindowState.Normal;
                formularioSRORastreamentoUnificado.StartPosition = FormStartPosition.CenterScreen;
                formularioSRORastreamentoUnificado.Text = string.Format(@"SRO - Rastreamento Unificado - http://websro2.correiosnet.int/rastreamento/sro?opcao=PESQUISA&objetos={0}", CodigoObjeto);
                formularioSRORastreamentoUnificado.ShowDialog();
            }
        }

        private void FiltrarPorPrazosVENCIDOSCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FiltrosCheckBox();
        }

        private void FiltrarPorPrazosVENCENDOHOJECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FiltrosCheckBox();
        }

        private void FiltrarPorPrazosAVENCERCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FiltrosCheckBox();
        }

        private void FiltrarPorClassificacaoPACCCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FiltrosCheckBox();
        }

        private void FiltrarPorClassificacaoSEDEXCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FiltrosCheckBox();
        }

        private void FiltrarPorClassificacaoDIVERSOSCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FiltrosCheckBox();
        }

        private void FiltrosCheckBox()
        {
            bool PrazosVENCIDOS = FiltrarPorPrazosVENCIDOSCheckBox.Checked;
            bool PrazosVENCENDOHOJE = FiltrarPorPrazosVENCENDOHOJECheckBox.Checked;
            bool PrazosAVENCER = FiltrarPorPrazosAVENCERCheckBox.Checked;

            bool ClassificacaoPAC = FiltrarPorClassificacaoPACCCheckBox.Checked;
            bool ClassificacaoSEDEX = FiltrarPorClassificacaoSEDEXCheckBox.Checked;
            bool ClassificacaoDIVERSOS = FiltrarPorClassificacaoDIVERSOSCheckBox.Checked;

            MontaFiltro = string.Empty;

            if (!PrazosVENCIDOS && !PrazosVENCENDOHOJE && !PrazosAVENCER && !ClassificacaoPAC && !ClassificacaoSEDEX && !ClassificacaoDIVERSOS)
            {
                MontaFiltro = "1<>1";
            }
            else if (!PrazosVENCIDOS && !PrazosVENCENDOHOJE && !PrazosAVENCER)
            {
                MontaFiltro = "1<>1";
            }
            else if (!ClassificacaoPAC && !ClassificacaoSEDEX && !ClassificacaoDIVERSOS)
            {
                MontaFiltro = "1<>1";
            }
            else
            {
                if (!PrazosVENCIDOS && !PrazosVENCENDOHOJE && !PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencido','Vencendo Hoje','A Vencer')";
                }
                if (PrazosVENCIDOS && PrazosVENCENDOHOJE && PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencido','Vencendo Hoje','A Vencer')";
                }
                if (PrazosVENCIDOS && !PrazosVENCENDOHOJE && !PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencido')";
                }
                if (!PrazosVENCIDOS && PrazosVENCENDOHOJE && !PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencendo Hoje')";
                }
                if (!PrazosVENCIDOS && !PrazosVENCENDOHOJE && PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('A Vencer')";
                }
                if (PrazosVENCIDOS && !PrazosVENCENDOHOJE && PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencido','A Vencer')";
                }
                if (PrazosVENCIDOS && PrazosVENCENDOHOJE && !PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencido','Vencendo Hoje')";
                }
                if (!PrazosVENCIDOS && PrazosVENCENDOHOJE && PrazosAVENCER)
                {
                    MontaFiltro = "StatusPrazo IN('Vencendo Hoje','A Vencer')";
                }

                if (!ClassificacaoPAC && !ClassificacaoSEDEX && !ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('PAC','SEDEX','DIVERSOS')";
                }
                if (ClassificacaoPAC && ClassificacaoSEDEX && ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('PAC','SEDEX','DIVERSOS')";
                }
                if (ClassificacaoPAC && !ClassificacaoSEDEX && !ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('PAC')";
                }
                if (!ClassificacaoPAC && ClassificacaoSEDEX && !ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('SEDEX')";
                }
                if (!ClassificacaoPAC && !ClassificacaoSEDEX && ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('DIVERSOS')";
                }
                if (ClassificacaoPAC && !ClassificacaoSEDEX && ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('PAC','DIVERSOS')";
                }
                if (ClassificacaoPAC && ClassificacaoSEDEX && !ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('PAC','SEDEX')";
                }
                if (!ClassificacaoPAC && ClassificacaoSEDEX && ClassificacaoDIVERSOS)
                {
                    MontaFiltro += " AND TipoClassificacao IN('SEDEX','DIVERSOS')";
                }
            }


            bindingSourceObjetosNaoEntregues.Filter = MontaFiltro;

            LbnQuantidadeRegistros.Text = bindingSourceObjetosNaoEntregues.Count.ToString();

            dataGridView1.Focus();
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl3.SelectedIndex == 0)
            {
                BtnColarConteudoJaCopiado.Visible = false;
                BtnRetornaTodosNaoEntregues.Visible = true;
            }
            if (tabControl3.SelectedIndex == 1)
            {
                BtnRetornaTodosNaoEntregues.Visible = false;
                BtnColarConteudoJaCopiado.Visible = true;
            }
        }
    }
}
