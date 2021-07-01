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
    public partial class FormularioSRORastreamentoUnificado : Form
    {
        //private TipoAmbiente tipoAmbiente = TipoAmbiente.Producao;
        //private enum TipoAmbiente { Producao, Desenvolvimento }
        private static StringBuilder textoConsulta = new StringBuilder();
        //private string enderecoSRO = @"http://websro2/rastreamento/sro";
        //private string enderecoSRO = @"https://app.correiosnet.int/rastreamento/sro";
        private string enderecoSRO = @"" + Configuracoes.EnderecosSRO["EnderecoSRO"].ToString();
        private string enderecoSRODesenvolvimento = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\1-1-TelaRastreamento.htm";
        public bool detalhamentoObjetoGridView = false;

        public FormularioSRORastreamentoUnificado()
        {
            InitializeComponent();
            detalhamentoObjetoGridView = false;
            textoConsulta = new StringBuilder();
            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                webBrowser1.Url = new Uri(enderecoSRODesenvolvimento);
            if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                webBrowser1.Url = new Uri(enderecoSRO);
        }

        public FormularioSRORastreamentoUnificado(string CodigoRastreamento)
        {
            InitializeComponent();
            detalhamentoObjetoGridView = true;
            textoConsulta = new StringBuilder();
            textoConsulta.AppendLine(CodigoRastreamento);
            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                webBrowser1.Url = new Uri(enderecoSRODesenvolvimento);
            if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
            {
                //string link = string.Format(@"http://websro2.correiosnet.int/rastreamento/sro?opcao=PESQUISA&objetos={0}", CodigoRastreamento);
                string link = string.Format(@"{0}{1}", Configuracoes.EnderecosSRO["EnderecoSROPorObjeto"].ToString(), CodigoRastreamento);
                webBrowser1.Url = new Uri(link);
            }
        }

        private void FormularioSRORastreamentoUnificado_Load(object sender, EventArgs e)
        {
            
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //webBrowser1.Document.Body.Style = "zoom:150%;";

            TxtEnderecoUrl.Text = webBrowser1.Url.AbsoluteUri;
            //se vazio
            if (webBrowser1.Url.AbsoluteUri == "about:blank") return;

            if (string.IsNullOrEmpty(textoConsulta.ToString()))
            {
                webBrowser1.Document.GetElementsByTagName("textarea")[0].Focus();
                return;
            }
            else
            {
                //webBrowser1.Document.GetElementsByTagName("textarea")[0].InnerText = textoConsulta.ToString();
                //webBrowser1.Document.GetElementsByTagName("textarea")[0].Focus();
                //SendKeys.Send("{TAB}");
                //SendKeys.Send("{ENTER}");
                return;
            }

        }

        private void FormularioSRORastreamentoUnificado_KeyDown(object sender, KeyEventArgs e)
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
