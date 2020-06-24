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
    public partial class FormularioCoordenadasExibicaoMapa : Form
    {
        //https://www.google.com.br/maps/search/-10.22285,-48.34052
        private string enderecoPesquisaMaps = @"https://www.google.com.br/maps/search/";


        //Dados Objetos
        public string CodigoObjetoFormatado = string.Empty;
        public string NomeCliente = string.Empty;
        public string DataLancamento = string.Empty;
        public string ARPostagem = string.Empty;
        public string MPPostagem = string.Empty;
        public string AgrupadoDestinatarioAusente = string.Empty;
        public string Situacao = string.Empty;
        public string ObjetoEntregue = string.Empty;
        public string DataModificacao = string.Empty;
        //Dados de Origem:
        public string CriacaoPostagem = string.Empty;
        public string MunicipioPostagem = string.Empty;
        public string UnidadePostagem = string.Empty;
        public string CepDestinoPostagem = string.Empty;
        //Endereço Entrega:
        public string EnderecoLOEC = string.Empty;
        public string BairroLOEC = string.Empty;
        public string LocalidadeLOEC = string.Empty;
        public string MunicipioLOEC = string.Empty;
        //Dados da Entrega:
        public string CriacaoLOEC = string.Empty;
        public string UnidadeLOEC = string.Empty;
        public string DistritoLOEC = string.Empty;
        public string CarteiroLOEC = string.Empty;
        public string CoordenadasAtual = string.Empty;



        public FormularioCoordenadasExibicaoMapa()
        {
            InitializeComponent();
        }

        public FormularioCoordenadasExibicaoMapa(
            string CoordenadasItemAtual,
            string CodigoObjetoFormatado = "",
            string NomeCliente = "",
            string EnderecoCompleto = "", 
            string DataCriacaoLOEC = "", 
            string UnidadeLOEC = "", 
            string DistritoLOEC = "", 
            string CarteiroLOEC = "")
        {
            InitializeComponent();

            LblCodigo.Text = CodigoObjetoFormatado;
            LblDestinatario.Text = NomeCliente;
            LblEndereco.Text = EnderecoCompleto;
            LblDataEntrega.Text = DataCriacaoLOEC;
            LblUnidadeEntrega.Text = UnidadeLOEC;
            LblDistrito.Text = DistritoLOEC;
            LblCarteiro.Text = CarteiroLOEC;
            LblCoordenadas.Text = CoordenadasItemAtual;

            this.Text = string.Format("Coordenadas [Destinatário Ausente] - Objeto: [{0}] - Coordenadas: [{1}]", CodigoObjetoFormatado, CoordenadasItemAtual);


            webBrowser1.IsWebBrowserContextMenuEnabled = false;

            webBrowser1.Url = new Uri(string.Format("{0}{1}", enderecoPesquisaMaps, CoordenadasItemAtual));
        }

        private void FormularioCoordenadasExibicaoMapa_Load(object sender, EventArgs e)
        {

        }

        private void FormularioCoordenadasExibicaoMapa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            if (e.KeyCode == Keys.F12)
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().visualizarListaDeObjetosToolStripMenuItem_Click(sender, e);
            }
            if (e.KeyCode == Keys.F9)
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().sRORastreamentoUnificadoToolStripMenuItem_Click(sender, e);
            }

        }

        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            if (e.KeyCode == Keys.F12)
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().visualizarListaDeObjetosToolStripMenuItem_Click(sender, e);
            }
            if (e.KeyCode == Keys.F9)
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().sRORastreamentoUnificadoToolStripMenuItem_Click(sender, e);
            }
        }
    }
}
