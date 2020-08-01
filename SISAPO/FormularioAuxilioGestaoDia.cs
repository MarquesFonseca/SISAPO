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
    public partial class FormularioAuxilioGestaoDia : Form
    {
        DataTable listaObjetos = new DataTable();

        public FormularioAuxilioGestaoDia()
        {
            InitializeComponent();
            DataTable listaObjetos = new DataTable();
        }

        private void FormularioAuxilioGestaoDia_Load(object sender, EventArgs e)
        {
            //BtnColarConteudoJaCopiado_Click(sender, e);
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
                        LblQuantidade.Text = string.Format("Quantidade: {0}", listaObjetos.Rows.Count);
                        return;
                    }
                    LblQuantidade.Text = string.Format("Quantidade: {0}", listaObjetos.Rows.Count);
                    listaObjetos.DefaultView.Sort = "NomeCliente ASC";
                    dataGridView1.DataSource = listaObjetos;
                    this.dataGridView1.Sort(this.dataGridView1.Columns["NomeCliente"], ListSortDirection.Ascending);
                    tabControl1.SelectTab(tabPageNomeCliente);
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

                    DataSet ds = dao.RetornaDataSet(string.Format("SELECT DISTINCT CodigoObjeto, CodigoLDI, NomeCliente, DataLancamento, DATEDIFF(\"d\", DataLancamento, Now()) AS DiasCorridos FROM TabelaObjetosSROLocal WHERE (ObjetoEntregue = {0})", 0));

                    dtbLista = ds.Tables[0];
                    LblQuantidade.Text = string.Format("Quantidade: {0}", dtbLista.Rows.Count);
                }
                return dtbLista;
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
                return dtbLista;
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (listaObjetos.Rows.Count == 0) return;
            //"CodigoLDI"
            //"CodigoObjeto"
            //"NomeCliente"
            //"DataLancamento"   
            //"DiasCorridos"                 
            if (tabControl1.SelectedIndex == 0)
            {
                listaObjetos.DefaultView.Sort = "CodigoLDI ASC";
                dataGridView1.DataSource = listaObjetos;
                dataGridView1.Focus();
            }
            if (tabControl1.SelectedIndex == 1)
            {
                listaObjetos.DefaultView.Sort = "CodigoObjeto ASC";
                dataGridView1.DataSource = listaObjetos;
                dataGridView1.Focus();
            }
            if (tabControl1.SelectedIndex == 2)
            {
                listaObjetos.DefaultView.Sort = "NomeCliente ASC";
                dataGridView1.DataSource = listaObjetos;
                dataGridView1.Focus();
            }
            if (tabControl1.SelectedIndex == 3)
            {
                listaObjetos.DefaultView.Sort = "DataLancamento ASC";
                dataGridView1.DataSource = listaObjetos;
                dataGridView1.Focus();
            }
            if (tabControl1.SelectedIndex == 4)
            {
                listaObjetos.DefaultView.Sort = "DiasCorridos ASC";
                dataGridView1.DataSource = listaObjetos;
                dataGridView1.Focus();
            }
        }

        private void BtnImprimirListaAtual_Click(object sender, EventArgs e)
        {
            if (listaObjetos.Rows.Count == 0) return;

            if (tabControl1.SelectedIndex == 0)//"CodigoLDI"
            {
                listaObjetos = listaObjetos.AsEnumerable().OrderBy(r => r.Field<string>("CodigoLDI")).CopyToDataTable();
                //novoCodigoSelecionados = listaObjetos.AsEnumerable().OrderBy(m => m["CodigoLDI"]).Select(c => c["CodigoObjeto"].ToString()).ToList();
            }
            if (tabControl1.SelectedIndex == 1)//"CodigoObjeto"
            {
                listaObjetos = listaObjetos.AsEnumerable().OrderBy(r => r.Field<string>("CodigoObjeto")).CopyToDataTable();
                //novoCodigoSelecionados = listaObjetos.AsEnumerable().OrderBy(m => m["CodigoObjeto"]).Select(c => c["CodigoObjeto"].ToString()).ToList();
            }
            if (tabControl1.SelectedIndex == 2)//"NomeCliente"
            {
                listaObjetos = listaObjetos.AsEnumerable().OrderBy(r => r.Field<string>("NomeCliente")).CopyToDataTable();
                //novoCodigoSelecionados = listaObjetos.AsEnumerable().OrderBy(m => m["NomeCliente"]).Select(c => c["CodigoObjeto"].ToString()).ToList();
            }
            if (tabControl1.SelectedIndex == 3)//"DataLancamento"
            {
                listaObjetos = listaObjetos.AsEnumerable().OrderBy(r => r.Field<string>("DataLancamento")).CopyToDataTable();
                //novoCodigoSelecionados = listaObjetos.AsEnumerable().OrderBy(m => m["DataLancamento"]).Select(c => c["CodigoObjeto"].ToString()).ToList();
            }
            if (tabControl1.SelectedIndex == 4)//"DiasCorridos"
            {
                listaObjetos = listaObjetos.AsEnumerable().OrderBy(r => r.Field<int>("DiasCorridos")).CopyToDataTable();
                //novoCodigoSelecionados = listaObjetos.AsEnumerable().OrderBy(m => m["DiasCorridos"]).Select(c => c["CodigoObjeto"].ToString()).ToList();
            }
            //FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = "Aguarde enquanto carrega o relatório de impressão.";

            //if (Application.OpenForms["FormularioImpressaoAuxilioGestaoDia"] != null) Application.OpenForms["FormularioImpressaoAuxilioGestaoDia"].Close();
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
                listaObjetos.DefaultView.Sort = "NomeCliente ASC";
                dataGridView1.DataSource = listaObjetos;
                this.dataGridView1.Sort(this.dataGridView1.Columns["NomeCliente"], ListSortDirection.Ascending);
                tabControl1.SelectTab(tabPageNomeCliente);
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
    }
}

