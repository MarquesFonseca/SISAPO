using System;
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
    public partial class FormularioAlteracaoObjeto : Form
    {
        public string CodigoObjeto = string.Empty;
        public string NomeCliente = string.Empty;
        public string NumeroLDI = string.Empty;
        public string DataLancamento = string.Empty;
        public string Situacao = string.Empty;
        public string DataModificacao = string.Empty;
        public bool ObjetoEmCaixaPostal = false;
        public bool ObjetoJaEntregue = false;
        public bool ObjetoJaAtualizado = false;

        public bool Cancelando = false;

        public FormularioAlteracaoObjeto()
        {
            Cancelando = false;
            InitializeComponent();
            TxtNomeCliente.Focus();
        }

        private void FormularioAlteracaoObjeto_Load(object sender, EventArgs e)
        {
            TxtCodigoObjeto.Text = CodigoObjeto;
            TxtNomeCliente.Text = NomeCliente;
            TxtNumeroLDI.Text = NumeroLDI;
            TxtDataLancamento.Text = DataLancamento;
            TxtSituacao.Text = Situacao;
            TxtDataBaixa.Text = DataModificacao;
            checkBoxObjetoCaixaPostal.Checked = ObjetoEmCaixaPostal;
            checkBoxObjetoJaEntregue.Checked = ObjetoJaEntregue;
            checkBoxObjetoJaAtualizado.Checked = ObjetoJaAtualizado;
            
            RetornaDadosPostagem();

            
            tabControl1.TabPages[0].Focus();
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            TxtNomeCliente.ScrollToCaret();
            TxtNomeCliente.ScrollToCaret();
        }

        private void RetornaDadosPostagem()
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                DataTable dt = dao.RetornaDataSet("SELECT top 1 UnidadePostagem, MunicipioPostagem, CriacaoPostagem, CepDestinoPostagem, ARPostagem, MPPostagem, DataMaxPrevistaEntregaPostagem FROM TabelaObjetosSROLocal WHERE (CodigoObjeto = @CodigoObjeto)", new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjeto }).Tables[0];
                if (dt.Rows.Count == 1)
                {
                    TxtUnidadePostagem.Text = dt.Rows[0]["UnidadePostagem"].ToString();
                    TxtMunicipioPostagem.Text = dt.Rows[0]["MunicipioPostagem"].ToString();
                    TxtCriacaoPostagem.Text = dt.Rows[0]["CriacaoPostagem"].ToString();
                    //TxtCepDestinoPostagem.Text = dt.Rows[0]["CepDestinoPostagem"].ToString();
                    TxtCepDestinoPostagem.Text = dt.Rows[0]["CepDestinoPostagem"].ToString().Length >= 8 ? string.Format("{0}{1}", dt.Rows[0]["CepDestinoPostagem"].ToString().Substring(0, 5), dt.Rows[0]["CepDestinoPostagem"].ToString().Substring(5, dt.Rows[0]["CepDestinoPostagem"].ToString().Length - 5)) : dt.Rows[0]["CepDestinoPostagem"].ToString();
                    //TxtDataMaxPrevistaEntregaPostagem.Text = dt.Rows[0]["DataMaxPrevistaEntregaPostagem"].ToString();
                    //dt.Rows[0]["DataMaxPrevistaEntregaPostagem"].ToDateTime().GetDateTimeFormats()[14];
                    DateTime dataValida; //Verifica Se a data for valida
                    TxtDataMaxPrevistaEntregaPostagem.Text = (DateTime.TryParse(dt.Rows[0]["DataMaxPrevistaEntregaPostagem"].ToString(), out dataValida)) ?
                                    dt.Rows[0]["DataMaxPrevistaEntregaPostagem"].ToString().ToDateTime().GetDateTimeFormats()[14].ToUpper() : dt.Rows[0]["DataMaxPrevistaEntregaPostagem"].ToString();        
                    if(string.IsNullOrEmpty(dt.Rows[0]["ARPostagem"].ToString()))
                        TxtARPostagem.Text = "";
                    if(dt.Rows[0]["ARPostagem"].ToString() == "S")
                        TxtARPostagem.Text = "SIM";
                    if(dt.Rows[0]["ARPostagem"].ToString() == "N")
                        TxtARPostagem.Text = "NÃO";

                    if (string.IsNullOrEmpty(dt.Rows[0]["MPPostagem"].ToString()))
                        TxtMPPostagem.Text = "";
                    if (dt.Rows[0]["MPPostagem"].ToString() == "S")
                        TxtMPPostagem.Text = "SIM";
                    if (dt.Rows[0]["MPPostagem"].ToString() == "N")
                        TxtMPPostagem.Text = "NÃO";                    
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            CodigoObjeto = TxtCodigoObjeto.Text;
            NomeCliente = TxtNomeCliente.Text.RemoveAcentos();
            NumeroLDI = TxtNumeroLDI.Text;
            DataLancamento = TxtDataLancamento.Text;
            Situacao = TxtSituacao.Text;
            DataModificacao = TxtDataBaixa.Text;
            ObjetoEmCaixaPostal = checkBoxObjetoCaixaPostal.Checked;
            ObjetoJaEntregue = checkBoxObjetoJaEntregue.Checked;
            ObjetoJaAtualizado = checkBoxObjetoJaAtualizado.Checked;

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelando = true;
            this.Close();
        }

        private void FormularioAlteracaoObjeto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Cancelando = true;
                this.Close();
            }
        }
    }
}
