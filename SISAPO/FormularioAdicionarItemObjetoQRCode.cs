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
    public partial class FormularioAdicionarItemObjetoQRCode : Form
    {
        public DataTable dtbLista = new DataTable();
        public bool ClicouConfirmar = false;
        public bool ClicouCancelar = false;

        private List<string> ListaComentariosPadrao = new List<string>();

        public FormularioAdicionarItemObjetoQRCode()
        {
            InitializeComponent();
        }

        private void FormularioAdicionarItemObjetoQRCode_Load(object sender, EventArgs e)
        {
            dtbLista = new DataTable();
            dtbLista.Columns.Add("CodigoObjeto", typeof(string));
            dtbLista.Columns.Add("CodigoLdi", typeof(string));
            dtbLista.Columns.Add("NomeCliente", typeof(string));
            dtbLista.Columns.Add("DataLancamento", typeof(string));
            dtbLista.Columns.Add("DataModificacao", typeof(string));
            dtbLista.Columns.Add("Situacao", typeof(string));
            dtbLista.Columns.Add("Atualizado", typeof(string));
            dtbLista.Columns.Add("ObjetoEntregue", typeof(string));
            dtbLista.Columns.Add("CaixaPostal", typeof(string));
            dtbLista.Columns.Add("UnidadePostagem", typeof(string));
            dtbLista.Columns.Add("MunicipioPostagem", typeof(string));
            dtbLista.Columns.Add("CriacaoPostagem", typeof(string));
            dtbLista.Columns.Add("CepDestinoPostagem", typeof(string));
            dtbLista.Columns.Add("ARPostagem", typeof(string));
            dtbLista.Columns.Add("MPPostagem", typeof(string));
            dtbLista.Columns.Add("DataMaxPrevistaEntregaPostagem", typeof(string));
            dtbLista.Columns.Add("UnidadeLOEC", typeof(string));
            dtbLista.Columns.Add("MunicipioLOEC", typeof(string));
            dtbLista.Columns.Add("CriacaoLOEC", typeof(string));
            dtbLista.Columns.Add("CarteiroLOEC", typeof(string));
            dtbLista.Columns.Add("DistritoLOEC", typeof(string));
            dtbLista.Columns.Add("NumeroLOEC", typeof(string));
            dtbLista.Columns.Add("EnderecoLOEC", typeof(string));
            dtbLista.Columns.Add("BairroLOEC", typeof(string));
            dtbLista.Columns.Add("LocalidadeLOEC", typeof(string));
            dtbLista.Columns.Add("SituacaoDestinatarioAusente", typeof(string));
            dtbLista.Columns.Add("AgrupadoDestinatarioAusente", typeof(string));
            dtbLista.Columns.Add("CoordenadasDestinatarioAusente", typeof(string));
            dtbLista.Columns.Add("Comentario", typeof(string));
            dtbLista.Columns.Add("TipoPostalServico", typeof(string));
            dtbLista.Columns.Add("TipoPostalSiglaCodigo", typeof(string));
            dtbLista.Columns.Add("TipoPostalNomeSiglaCodigo", typeof(string));
            dtbLista.Columns.Add("TipoPostalPrazoDiasCorridosRegulamentado", typeof(string));
            dtbLista.Columns.Add("DataListaAtual", typeof(string));
            dtbLista.Columns.Add("NumeroListaAtual", typeof(string));
            dtbLista.Columns.Add("ItemAtual", typeof(string));
            dtbLista.Columns.Add("QtdTotal", typeof(string));

            //SendKeys.Send("{TAB}");
            TxtObjetoAtual.Focus();
            TxtObjetoAtual.ScrollToCaret();
            TxtObjetoAtual.ScrollToCaret();

            TxtObjetoAtual.Select(TxtObjetoAtual.Text.Length, 0);
        }

        private void TxtObjetoAtual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                if (Mensagens.Pergunta("Realmente deseja sair?") == DialogResult.Yes)
                {
                    ClicouCancelar = true;
                    ClicouConfirmar = false;
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            if (string.IsNullOrEmpty(TxtObjetoAtual.Text)) return;

            if (e.KeyData != Keys.Enter) return;

            if (VerificaPadraoLeitura(TxtObjetoAtual.Text) == false)
            {
                TxtObjetoAtual.Text = "";
                TxtObjetoAtual.Focus();
                TxtObjetoAtual.ScrollToCaret();
                TxtObjetoAtual.ScrollToCaret();

                TxtObjetoAtual.Select(TxtObjetoAtual.Text.Length, 0);
                return;
            }

            AdicionaItemLista();
        }

        private bool VerificaPadraoLeitura(string text)
        {
            bool retorno = true;
            try
            {
                string QRCode = TxtObjetoAtual.Text.ToUpper();
                //string descompacta = ClassesDiversas.FormataString.Descompacta(QRCode);
                string[] CelulasCampos = QRCode.Split(new string[] { "#TAB" }, StringSplitOptions.None);
                if (CelulasCampos.Count() < 37)
                    retorno = false;
            }
            catch (Exception ex)
            {
                retorno = false;
            }

            return retorno;
        }

        private void AdicionaItemLista()
        {
            //LV221247464CN#tab287077020401#tabJULIANA RODRIGUES#tab29/09/2021 10:40:11#tab29/09/2021 14:44:46#tabENTREGUE#tabTrue#tabTrue#tabFalse#tab00156-000 / CHINA#tab#tab22/08/2021 14:26:00#tab77019-096#tab#tab#tab#tab77100-970 / CDD PALMAS#tabPALMAS / TO#tab28/09/2021 11:52:52#tab83454624#tab502#tab112100021778#tabQUADRA ARSO 112 ALAMEDA 13 17#tabPLANO DIRETOR SUL#tab77019096#tabAUSENTE ENCAMINHADO ENTREGA INTERNA#tabNÃO#tab-10.25075,-48.34432#tabPCT INT#tabNAO URGENTE#tabLV#tabOBJETO INTERNACIONAL PRIME#tab20#tab
            string QRCode = TxtObjetoAtual.Text.ToUpper();
            //string descompacta = ClassesDiversas.FormataString.Descompacta(QRCode);
            string[] CelulasCampos = QRCode.Split(new string[] { "#TAB" }, StringSplitOptions.None);

            string CodigoObjeto = CelulasCampos[0];
            string CodigoLdi = CelulasCampos[1];
            string NomeCliente = CelulasCampos[2];
            DateTime DataLancamento = Convert.ToDateTime(CelulasCampos[3]);
            string DataModificacao = CelulasCampos[4];
            string Situacao = CelulasCampos[5];
            bool Atualizado = Convert.ToBoolean(CelulasCampos[6]);
            bool ObjetoEntregue = Convert.ToBoolean(CelulasCampos[7]);
            bool CaixaPostal = Convert.ToBoolean(CelulasCampos[8]);
            string UnidadePostagem = CelulasCampos[9];
            string MunicipioPostagem = CelulasCampos[10];
            string CriacaoPostagem = CelulasCampos[11];
            string CepDestinoPostagem = CelulasCampos[12];
            string ARPostagem = CelulasCampos[13];
            string MPPostagem = CelulasCampos[14];
            string DataMaxPrevistaEntregaPostagem = CelulasCampos[15];
            string UnidadeLOEC = CelulasCampos[16];
            string MunicipioLOEC = CelulasCampos[17];
            string CriacaoLOEC = CelulasCampos[18];
            string CarteiroLOEC = CelulasCampos[19];
            string DistritoLOEC = CelulasCampos[20];
            string NumeroLOEC = CelulasCampos[21];
            string EnderecoLOEC = CelulasCampos[22];
            string BairroLOEC = CelulasCampos[23];
            string LocalidadeLOEC = CelulasCampos[24];
            string SituacaoDestinatarioAusente = CelulasCampos[25];
            string AgrupadoDestinatarioAusente = CelulasCampos[26];
            string CoordenadasDestinatarioAusente = CelulasCampos[27];
            string Comentario = CelulasCampos[28];
            string TipoPostalServico = CelulasCampos[29];
            string TipoPostalSiglaCodigo = CelulasCampos[30];
            string TipoPostalNomeSiglaCodigo = CelulasCampos[31];
            string TipoPostalPrazoDiasCorridosRegulamentado = CelulasCampos[32];
            string DataListaAtual = CelulasCampos[33];
            string NumeroListaAtual = CelulasCampos[34];
            string ItemAtual = CelulasCampos[35];
            string QtdTotal = CelulasCampos[36];

            try
            {
                bool existe = dtbLista.AsEnumerable().Any(t => t["CodigoObjeto"].ToString() == CodigoObjeto);
                if (!existe)
                    dtbLista.Rows.Add(
                    CodigoObjeto,
                    CodigoLdi,
                    NomeCliente,
                    DataLancamento,
                    DataModificacao,
                    Situacao,
                    Atualizado,
                    ObjetoEntregue,
                    CaixaPostal,
                    UnidadePostagem,
                    MunicipioPostagem,
                    CriacaoPostagem,
                    CepDestinoPostagem,
                    ARPostagem,
                    MPPostagem,
                    DataMaxPrevistaEntregaPostagem,
                    UnidadeLOEC,
                    MunicipioLOEC,
                    CriacaoLOEC,
                    CarteiroLOEC,
                    DistritoLOEC,
                    NumeroLOEC,
                    EnderecoLOEC,
                    BairroLOEC,
                    LocalidadeLOEC,
                    SituacaoDestinatarioAusente,
                    AgrupadoDestinatarioAusente,
                    CoordenadasDestinatarioAusente,
                    Comentario,
                    TipoPostalServico,
                    TipoPostalSiglaCodigo,
                    TipoPostalNomeSiglaCodigo,
                    TipoPostalPrazoDiasCorridosRegulamentado,
                    DataListaAtual,
                    NumeroListaAtual,
                    ItemAtual,
                    QtdTotal
                        );

                dataGridView1.DataSource = dtbLista;
                dtbLista.DefaultView.Sort = "DataLancamento DESC";
                dtbLista = dtbLista.DefaultView.ToTable();
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
            }
            finally
            {
                TxtObjetoAtual.Text = "";
                TxtObjetoAtual.Focus();
                TxtObjetoAtual.ScrollToCaret();
                TxtObjetoAtual.ScrollToCaret();

                TxtObjetoAtual.Select(TxtObjetoAtual.Text.Length, 0);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtObjetoAtual.Text))
            {
                if (VerificaPadraoLeitura(TxtObjetoAtual.Text) == false)
                    return;

                AdicionaItemLista();
            }

            if (dtbLista.Rows.Count == 0)
            {
                ClicouConfirmar = false;
                ClicouCancelar = true;
            }
            else
            {
                ClicouConfirmar = true;
                ClicouCancelar = false;
            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClicouConfirmar = false;
            ClicouCancelar = true;
            this.Close();
        }

        private void FormularioAdicionarItemObjetoQRCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnConfirmar_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnCancelar_Click(sender, e);
            }
        }
    }
}
