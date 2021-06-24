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
    public partial class FormularioImpressaoTestePDF : Form
    {
        private string EnderecoPDF;

        public FormularioImpressaoTestePDF(string _EnderecoPDF)
        {
            InitializeComponent();
            this.EnderecoPDF = _EnderecoPDF;
        }
        private void FormularioImpressaoTestePDF_Load(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri(EnderecoPDF);
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

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

        private void BtnImprimirPagina_Click(object sender, EventArgs e)
        {
            //webBrowser1.ShowPropertiesDialog();
            webBrowser1.ShowPrintPreviewDialog();

        }
    }
}
