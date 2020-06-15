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
            BtnColarConteudoJaCopiado_Click(sender, e);
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
        }

        private DataTable RetornaListaObjetos(string Texto)
        {
            DataTable dtbLista = new DataTable();
            dtbLista.Columns.Add("CodigoLDI", typeof(string));
            dtbLista.Columns.Add("CodigoObjeto", typeof(string));
            dtbLista.Columns.Add("NomeCliente", typeof(string));
            dtbLista.Columns.Add("DataLancamento", typeof(DateTime));
            try
            {
                if (!DAO.TestaConexao(ClassesDiversas.Configuracoes.strConexao, TipoBanco.OleDb))
                {
                    FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao;
                    return null;
                }
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    string[] linha = Texto.Split('\n');

                    for (int i = 0; i < linha.Length; i++)
                    {
                        if (linha[i] == "" || linha[i] == "\r") continue;
                        string[] Parteslinha = linha[i].Split('\t');
                        string ParteLinhaCodigoLdi = Parteslinha.Length >= 1 ? Parteslinha[0].Trim().ToUpper() : "";
                        string ParteLinhaCodigoObjeto = Parteslinha.Length >= 2 ? Parteslinha[1].Trim().ToUpper() : "";
                        string ParteLinhaNomeCliente = "";
                        string ParteLinhaDataLancamento = Parteslinha.Length >= 3 ? Parteslinha[2].Trim().ToUpper() : "";

                        if (ParteLinhaCodigoObjeto != "")
                        {
                            DataSet ds = dao.RetornaDataSet(string.Format("SELECT DISTINCT CodigoObjeto, NomeCliente, DataLancamento FROM TabelaObjetosSROLocal WHERE (CodigoObjeto = '{0}')", ParteLinhaCodigoObjeto));

                            if (ds.Tables[0].Rows.Count >= 1)
                            {
                                ParteLinhaNomeCliente = ds.Tables[0].Rows[0][1].ToString();
                                ParteLinhaDataLancamento = ds.Tables[0].Rows[0][2].ToString();
                            }
                            dtbLista.Rows.Add(ParteLinhaCodigoLdi, ParteLinhaCodigoObjeto, ParteLinhaNomeCliente, ParteLinhaDataLancamento.ToDateTime());
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

        private void tabControl1_Click(object sender, EventArgs e)
        {
            //"CodigoLDI"
            //"CodigoObjeto"
            //"NomeCliente"
            //"DataLancamento"                    
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
        }

        private void BtnImprimirListaAtual_Click(object sender, EventArgs e)
        {
            List<string> novoCodigoSelecionados = new List<string>();
            if (listaObjetos.Rows.Count == 0) return;
            //"CodigoLDI"
            //"CodigoObjeto"
            //"NomeCliente"
            //"DataLancamento"  
            if (tabControl1.SelectedIndex == 0)
            {
                novoCodigoSelecionados = listaObjetos.AsEnumerable().OrderBy(m => m["CodigoLDI"]).Select(c => c["CodigoObjeto"].ToString()).ToList();
            }
            if (tabControl1.SelectedIndex == 1)
            {
                novoCodigoSelecionados = listaObjetos.AsEnumerable().OrderBy(m => m["CodigoObjeto"]).Select(c => c["CodigoObjeto"].ToString()).ToList();
            }
            if (tabControl1.SelectedIndex == 2)
            {
                novoCodigoSelecionados = listaObjetos.AsEnumerable().OrderBy(m => m["NomeCliente"]).Select(c => c["CodigoObjeto"].ToString()).ToList();
            }
            if (tabControl1.SelectedIndex == 3)
            {
                novoCodigoSelecionados = listaObjetos.AsEnumerable().OrderBy(m => m["DataLancamento"]).Select(c => c["CodigoObjeto"].ToString()).ToList();
            }
            //List<string> novoCodigoSelecionados = listaObjetos.AsEnumerable().Select(c => c["CodigoObjeto"].ToString()).ToList();

            if (novoCodigoSelecionados.Count == 0)
            {
                //Mensagens.Informa("Não foi encontrado lançamento para o dia [" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "].");
                return;
            }
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = "Aguarde enquanto carrega o relatório de impressão.";

            //if (Application.OpenForms["FormularioImpressaoAuxilioGestaoDia"] != null) Application.OpenForms["FormularioImpressaoAuxilioGestaoDia"].Close();
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioImpressaoAuxilioGestaoDia")
                {
                    item.Close();
                    break;
                }
            }


            FormularioImpressaoAuxilioGestaoDia formularioImpressaoAuxilioGestaoDia = new FormularioImpressaoAuxilioGestaoDia(novoCodigoSelecionados);
            formularioImpressaoAuxilioGestaoDia.MdiParent = MdiParent;
            formularioImpressaoAuxilioGestaoDia.Show();
            formularioImpressaoAuxilioGestaoDia.WindowState = FormWindowState.Normal;
            formularioImpressaoAuxilioGestaoDia.WindowState = FormWindowState.Maximized;
            formularioImpressaoAuxilioGestaoDia.Activate();
        }
    }
}

