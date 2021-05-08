using System;
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

        public FormularioAuxilioGestaoDiaNovo()
        {
            InitializeComponent();
            DataTable listaObjetos = new DataTable();
        }

        private void FormularioAuxilioGestaoDia_Load(object sender, EventArgs e)
        {
            //BtnColarConteudoJaCopiado_Click(sender, e);
            FiltrarPorClassificacaoComboBox.SelectedIndex = 0;
            FiltrarPorPRAZOSComboBox.SelectedIndex = 0;
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
                    if (listaObjetos.Rows.Count == 0)
                    {
                        dataGridView1.DataSource = listaObjetos;
                        listaObjetos.Clear(); //Retira os valores da tabela mantendo os campos
                        Mensagens.Informa("Não foi possível carregar.\nCopie a lista e clique no botão para tentar novamente .");
                        LbnQuantidadeRegistros.Text = string.Format("{0}", listaObjetos.Rows.Count);
                        return;
                    }
                    LbnQuantidadeRegistros.Text = string.Format("{0}", listaObjetos.Rows.Count);
                    listaObjetos.DefaultView.Sort = "NomeCliente ASC";
                    dataGridView1.DataSource = listaObjetos;
                    this.dataGridView1.Sort(this.dataGridView1.Columns["NomeCliente"], ListSortDirection.Ascending);
                    dataGridView1.Focus();
                }
                catch (IOException) { }
            }
        }

        private DataTable RetornaListaObjetos(string Texto)
        {
            DataTable dtbLista = new DataTable();
            dtbLista.Columns.Add("CodigoLDI", typeof(string));
            dtbLista.Columns.Add("CodigoObjeto", typeof(string));
            dtbLista.Columns.Add("NomeCliente", typeof(string));
            dtbLista.Columns.Add("DataLancamento", typeof(DateTime));
            dtbLista.Columns.Add("DiasCorridos", typeof(int));
            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!DAO.TestaConexao(ClassesDiversas.Configuracoes.strConexao, TipoBanco.OleDb)) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return null; }
                    string[] linha = Texto.Split('\n');

                    for (int i = 0; i < linha.Length; i++)
                    {
                        if (linha[i] == "" || linha[i] == "\r") continue;
                        string[] Parteslinha = linha[i].Split('\t');
                        string ParteLinhaCodigoLdi = Parteslinha.Length >= 1 ? Parteslinha[0].Trim().ToUpper() : "";
                        string ParteLinhaCodigoObjeto = Parteslinha.Length >= 2 ? Parteslinha[1].Trim().ToUpper() : "";
                        string ParteLinhaNomeCliente = "";
                        string ParteLinhaDataLancamento = Parteslinha.Length >= 3 ? Parteslinha[2].Trim().ToUpper() : "";
                        string DiasCorridos = "0";
                        bool validaData;
                        try
                        {
                            Convert.ToDateTime(ParteLinhaDataLancamento);
                            validaData = true;
                        }
                        catch (Exception)
                        {
                            validaData = false;
                        }
                        if (validaData)
                        {
                            DiasCorridos = Convert.ToString((DateTime.Now.Date - ParteLinhaDataLancamento.ToDateTime().Date).TotalDays);
                        }


                        if (ParteLinhaCodigoObjeto != "")
                        {
                            DataSet ds = dao.RetornaDataSet(string.Format("SELECT DISTINCT CodigoObjeto, NomeCliente, DataLancamento FROM TabelaObjetosSROLocal WHERE (CodigoObjeto = '{0}')", ParteLinhaCodigoObjeto));

                            if (ds.Tables[0].Rows.Count >= 1)
                            {
                                ParteLinhaNomeCliente = ds.Tables[0].Rows[0][1].ToString();
                                ParteLinhaDataLancamento = ds.Tables[0].Rows[0][2].ToString();
                            }
                            dtbLista.Rows.Add(ParteLinhaCodigoLdi, ParteLinhaCodigoObjeto, ParteLinhaNomeCliente, ParteLinhaDataLancamento.ToDateTime(), DiasCorridos.ToInt());
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

        private void BtnImprimirListaAtual_Click(object sender, EventArgs e)
        {
            if (listaObjetos.Rows.Count == 0) return;

            listaObjetos = listaObjetos.AsEnumerable().OrderBy(r => r.Field<string>("NomeCliente")).CopyToDataTable();

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
                    //Mensagens.Informa("Não foi possível carregar.\nCopie a lista e clique no botão para tentar novamente ."); 
                    return;
                }

                bindingSourceObjetosNaoEntregues = new BindingSource();
                bindingSourceObjetosNaoEntregues.DataSource = listaObjetos;
                dataGridView1.DataSource = bindingSourceObjetosNaoEntregues;
                //bindingSourceObjetosNaoEntregues.Filter = "1 = 2";
                LbnQuantidadeRegistros.Text = bindingSourceObjetosNaoEntregues.Count.ToString();
                dataGridView1.Focus();
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

        private void FiltrarPorClassificacaoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FiltrarPorClassificacaoComboBox.SelectedItem == null) return;
            if (FiltrarPorPRAZOSComboBox.SelectedItem == null) return;
            Filtros();
        }

        private void FiltrarPorPRAZOSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FiltrarPorClassificacaoComboBox.SelectedItem == null) return;
            if (FiltrarPorPRAZOSComboBox.SelectedItem == null) return;
            Filtros();
        }

        private void Filtros()
        {
            string MontaFiltro = string.Empty;

            MontaFiltro = "1 = 1";

            if (FiltrarPorClassificacaoComboBox.SelectedItem.ToString() == "TODOS")
            {
                MontaFiltro += "";
            }
            else
            {
                MontaFiltro += string.Format(" AND TipoClassificacao = '{0}'", FiltrarPorClassificacaoComboBox.SelectedItem.ToString());
            }

            if (FiltrarPorPRAZOSComboBox.SelectedItem.ToString() == "TODOS")
            {
                MontaFiltro += "";
            }
            else
            {
                MontaFiltro += string.Format(" AND StatusPrazo = '{0}'", FiltrarPorPRAZOSComboBox.SelectedItem.ToString());
            }

            bindingSourceObjetosNaoEntregues.Filter = MontaFiltro;

            LbnQuantidadeRegistros.Text = bindingSourceObjetosNaoEntregues.Count.ToString();
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

        private void FiltrosCheckBox()
        {
            FiltrostESTE();
            return;

            bool ClassificacaoPAC = FiltrarPorClassificacaoPACCCheckBox.Checked;
            bool ClassificacaoSEDEX = FiltrarPorClassificacaoSEDEXCheckBox.Checked;
            bool ClassificacaoDIVERSOS = FiltrarPorClassificacaoDIVERSOSCheckBox.Checked;

            bool PrazosVENCIDOS = FiltrarPorPrazosVENCIDOSCheckBox.Checked;
            bool PrazosVENCENDOHOJE = FiltrarPorPrazosVENCENDOHOJECheckBox.Checked;
            bool PrazosAVENCER = FiltrarPorPrazosAVENCERCheckBox.Checked;

            string MontaFiltro = string.Empty;

            MontaFiltro = "1 = 1";

            MontaFiltro += " AND (StatusPrazo = 'VENCIDO' AND StatusPrazo = 'VENCENDO HOJE' AND StatusPrazo = 'A VENCER')";
            MontaFiltro += PrazosVENCIDOS ? " OR StatusPrazo = 'VENCIDO'" : "";
            MontaFiltro += PrazosVENCENDOHOJE ? " OR StatusPrazo = 'VENCENDO HOJE'" : "";
            MontaFiltro += PrazosAVENCER ? " OR StatusPrazo = 'A VENCER'" : "";

            //MontaFiltro += " AND (TipoClassificacao = 'PAC' AND TipoClassificacao = 'SEDEX' AND TipoClassificacao = 'DIVERSOS')";
            MontaFiltro += ClassificacaoPAC ? " OR TipoClassificacao = 'PAC'" : " OR TipoClassificacao <> 'PAC'";
            MontaFiltro += ClassificacaoSEDEX ? " OR TipoClassificacao = 'SEDEX'" : " OR TipoClassificacao <> 'SEDEX'";
            MontaFiltro += ClassificacaoDIVERSOS ? " OR TipoClassificacao = 'DIVERSOS'" : " OR TipoClassificacao <> 'DIVERSOS'";

            bindingSourceObjetosNaoEntregues.Filter = MontaFiltro;

            LbnQuantidadeRegistros.Text = bindingSourceObjetosNaoEntregues.Count.ToString();
        }

        private void FiltrostESTE()
        {
            bool ClassificacaoPAC = FiltrarPorClassificacaoPACCCheckBox.Checked;
            bool ClassificacaoSEDEX = FiltrarPorClassificacaoSEDEXCheckBox.Checked;
            bool ClassificacaoDIVERSOS = FiltrarPorClassificacaoDIVERSOSCheckBox.Checked;

            bool PrazosVENCIDOS = FiltrarPorPrazosVENCIDOSCheckBox.Checked;
            bool PrazosVENCENDOHOJE = FiltrarPorPrazosVENCENDOHOJECheckBox.Checked;
            bool PrazosAVENCER = FiltrarPorPrazosAVENCERCheckBox.Checked;

            string MontaFiltro = string.Empty;

            MontaFiltro = "1 = 1";

            //MontaFiltro += " AND (StatusPrazo = 'VENCIDO' AND StatusPrazo = 'VENCENDO HOJE' AND StatusPrazo = 'A VENCER')";
            MontaFiltro += PrazosVENCIDOS ? " AND (StatusPrazo = 'VENCIDO')" : "";
            MontaFiltro += PrazosVENCENDOHOJE ? " AND (StatusPrazo = 'VENCENDO HOJE')" : "";
            MontaFiltro += PrazosAVENCER ? " AND (StatusPrazo = 'A VENCER')" : "";

            ////MontaFiltro += " AND (TipoClassificacao = 'PAC' AND TipoClassificacao = 'SEDEX' AND TipoClassificacao = 'DIVERSOS')";
            //MontaFiltro += ClassificacaoPAC ? " OR TipoClassificacao = 'PAC'" : " OR TipoClassificacao <> 'PAC'";
            //MontaFiltro += ClassificacaoSEDEX ? " OR TipoClassificacao = 'SEDEX'" : " OR TipoClassificacao <> 'SEDEX'";
            //MontaFiltro += ClassificacaoDIVERSOS ? " OR TipoClassificacao = 'DIVERSOS'" : " OR TipoClassificacao <> 'DIVERSOS'";

            bindingSourceObjetosNaoEntregues.Filter = MontaFiltro;

            LbnQuantidadeRegistros.Text = bindingSourceObjetosNaoEntregues.Count.ToString();

        }
    }
}
