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
    public partial class FormularioImpressaoEntregaAgruparObjetos : Form
    {
        public DataTable DadosAgrupados = new DataTable();
        private DataView DadosAgrupadosDataView = new DataView();
        private List<string> CodigoSelecionadosEntrada = new List<string>();
        public bool ImpressaoCancelada = false;
        public bool ClicouNoConfirmar = false;

        public FormularioImpressaoEntregaAgruparObjetos(List<string> _codigoSelecionados)
        {
            InitializeComponent();
            this.ImpressaoCancelada = false;
            CodigoSelecionadosEntrada = _codigoSelecionados;
            ProcessandoListaObjetosSelecionados();
        }

        public void ProcessandoListaObjetosSelecionados()
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                DadosAgrupados.Columns.Add("CodigoObjeto", typeof(String));
                DadosAgrupados.Columns.Add("NomeCliente", typeof(String));
                DadosAgrupados.Columns.Add("NomeClienteAgrupado", typeof(String));
                DadosAgrupados.Columns.Add("Grupo", typeof(String));
                DadosAgrupados.Columns.Add("DataModificacao", typeof(String));
                DadosAgrupados.Columns.Add("Endereco", typeof(String));

                //DadosAgrupados = dao.RetornaDataTable("SELECT CodigoObjeto, NomeCliente, DataModificacao, (EnderecoLOEC + ' ' + BairroLOEC + ' ' + LocalidadeLOEC) as Endereco FROM TabelaObjetosSROLocal WHERE (CodigoObjeto IN ('" + itemCodigoSelecionado + "')) ORDER BY NomeCliente");

                int contador = 1;
                foreach (string itemCodigoSelecionado in CodigoSelecionadosEntrada)
                {
                    //(EnderecoLOEC & '-' & BairroLOEC & '-' & LocalidadeLOEC) as Endereco
                    DataRow dr = dao.RetornaDataRow("SELECT CodigoObjeto, NomeCliente, '' as Grupo, DataModificacao, (EnderecoLOEC + ' ' + BairroLOEC + ' ' + MunicipioLOEC + ' - ' + CepDestinoPostagem) as Endereco FROM TabelaObjetosSROLocal WHERE (CodigoObjeto IN ('" + itemCodigoSelecionado + "')) ORDER BY NomeCliente");
                    DataRow row = DadosAgrupados.NewRow();
                    row["CodigoObjeto"] = dr["CodigoObjeto"];
                    row["NomeCliente"] = dr["NomeCliente"];
                    row["NomeClienteAgrupado"] = "";
                    row["Grupo"] = "";
                    row["DataModificacao"] = dr["DataModificacao"];
                    row["Endereco"] = dr["Endereco"];
                    DadosAgrupados.Rows.Add(row);
                    contador++;
                }
            }

            DadosAgrupadosDataView = new DataView(DadosAgrupados);
            DadosAgrupadosDataView.RowFilter = "DataModificacao = ''";
            DadosAgrupadosDataView.Sort = "NomeCliente ASC";
        }
        private void FormularioImpressaoEntregaAgruparObjetos_Load(object sender, EventArgs e)
        {
            //DataView dv = new DataView(DadosAgrupados);
            //dv.RowFilter = "DataModificacao = ''"; // query example = "id = 10"
            ////DadosAgrupados = dv.ToTable();


            dataGridView1.DataSource = DadosAgrupadosDataView;
            //dataGridView1.Sort(this.dataGridView1.Columns["NomeCliente"], ListSortDirection.Ascending);
        }

        //public int grupoSelecionado = 0;
        //public List<string> codigosAgrupados = new List<string>();
        public int linhaInicialSelecao = 0;
        private void BtnAgruparSelecionados_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;

            bool primeiroItemSelecionado = false;
            string codigoReferencia = "";
            string nomeClientePrimeiroItemSelecionado = "";
            bool AlgumMarcado = false;

            for (int i = 0; i < DadosAgrupadosDataView.Count; i++)
            {
                if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[0].FormattedValue) || this.dataGridView1.Rows[i].Selected)
                {
                    AlgumMarcado = true;
                    string codigo = this.dataGridView1.Rows[i].Cells["CodigoObjeto"].FormattedValue.ToString();
                    string nomeCliente = this.dataGridView1.Rows[i].Cells["NomeCliente"].FormattedValue.ToString();
                    string grupo = this.dataGridView1.Rows[i].Cells["Grupo"].FormattedValue.ToString();

                    if (primeiroItemSelecionado == false)
                    {
                        primeiroItemSelecionado = true;
                        codigoReferencia = codigo;
                        linhaInicialSelecao = i;
                        nomeClientePrimeiroItemSelecionado = nomeCliente;
                        this.dataGridView1.Rows[i].Cells["NomeClienteAgrupado"].Value = nomeClientePrimeiroItemSelecionado;
                        this.dataGridView1.Rows[i].Cells["Grupo"].Value = codigoReferencia;
                    }
                    else
                    {
                        this.dataGridView1.Rows[i].Cells["NomeClienteAgrupado"].Value = nomeClientePrimeiroItemSelecionado;
                        this.dataGridView1.Rows[i].Cells["Grupo"].Value = codigoReferencia;
                    }
                    this.dataGridView1.Rows[i].Cells[0].Value = 0;
                }

            }

            if (AlgumMarcado)
                this.dataGridView1.Rows[linhaInicialSelecao].Selected = true;
            else
                this.dataGridView1.Rows[rowindex].Selected = true;
        }

        private void BtnRemoverSelecionados_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            bool primeiroItemSelecionado = false;
            bool AlgumMarcado = false;
            for (int i = 0; i < DadosAgrupadosDataView.Count; i++)
            {
                string codigo = this.dataGridView1.Rows[i].Cells["CodigoObjeto"].FormattedValue.ToString();
                string nomeCliente = this.dataGridView1.Rows[i].Cells["NomeCliente"].FormattedValue.ToString();
                string grupo = this.dataGridView1.Rows[i].Cells["Grupo"].FormattedValue.ToString();

                if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[0].FormattedValue) || this.dataGridView1.Rows[i].Selected)
                {
                    AlgumMarcado = true;
                    if (primeiroItemSelecionado == false)
                    {
                        primeiroItemSelecionado = true;
                        linhaInicialSelecao = i;
                    }
                    this.dataGridView1.Rows[i].Cells["NomeClienteAgrupado"].Value = "";
                    this.dataGridView1.Rows[i].Cells["Grupo"].Value = "";
                    this.dataGridView1.Rows[i].Cells[0].Value = 0;
                }
            }            

            if (AlgumMarcado)
                this.dataGridView1.Rows[linhaInicialSelecao].Selected = true;
            else
                this.dataGridView1.Rows[rowindex].Selected = true;
        }

        private void CheckBoxMarcar_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DadosAgrupadosDataView.Count; i++)
            {
                if (Convert.ToBoolean(this.dataGridView1.Rows[i].Selected))
                {
                    this.dataGridView1.Rows[i].Cells[0].Value = true;
                }
            }

            CheckBoxMarcar.Checked = false;
        }

        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            this.ImpressaoCancelada = false;
            ClicouNoConfirmar = true;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.ImpressaoCancelada = true;
            this.Close();
        }

        private void FormularioImpressaoEntregaAgruparObjetos_FormClosed(object sender, FormClosedEventArgs e)
        {
            DadosAgrupadosDataView.RowFilter = string.Empty;
            DadosAgrupados = DadosAgrupadosDataView.ToTable();

            if (ClicouNoConfirmar)
                ImpressaoCancelada = false;
            else
                ImpressaoCancelada = true;
        }

        private void FormularioImpressaoEntregaAgruparObjetos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BtnCancelar_Click(sender, e);
            }
            if (e.KeyCode == Keys.Enter)
            {
                BtnConcluir_Click(sender, e);
            }
        }
    }
}
