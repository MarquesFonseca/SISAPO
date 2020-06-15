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
    public partial class FormularioImpressaoAuxilioGestaoDia : Form
    {
        public List<string> CodigoSelecionados = new List<string>();

        public FormularioImpressaoAuxilioGestaoDia(List<string> _codigosSelecionados)
        {
            InitializeComponent();
            CodigoSelecionados = _codigosSelecionados;
        }

        private void FormularioImpressaoAuxilioGestaoDia_Load(object sender, EventArgs e)
        {
            string Html = RetornaHtml().ToString();
            webBrowser1.DocumentText = Html;
        }

        private StringBuilder RetornaHtml()
        {
            StringBuilder Html = new StringBuilder();
            Html.AppendLine("<html><head>");
            Html.AppendLine("<meta http-equiv=\"content-type\" content=\"text/html; charset=windows-1252\">");
            Html.AppendLine("<title>Lista de auxílio à gestão do dia [" + DateTime.Now.Date.ToShortDateString() + "]</title>");
            Html.AppendLine("<style>");
            Html.AppendLine(".espaco {");
            Html.AppendLine("padding: 9.5px;");
            Html.AppendLine("text-align:center;");
            Html.AppendLine("}");
            Html.AppendLine("TD.clsMenu");
            Html.AppendLine("{");
            Html.AppendLine("  color: #FFFFFF;");
            Html.AppendLine("  font-weight: bold;");
            Html.AppendLine("}");
            Html.AppendLine("");
            Html.AppendLine("BODY {");
            Html.AppendLine("  margin-top: 0;");
            Html.AppendLine("  margin-left: 0;");
            Html.AppendLine("  margin-right: 0;");
            Html.AppendLine("  color: #003399;");
            Html.AppendLine("  font-size: 12px;");
            Html.AppendLine("  font-family: arial;");
            Html.AppendLine("  background-color: #FFFFFF;");
            Html.AppendLine("}");
            Html.AppendLine("");
            Html.AppendLine("TD {");
            Html.AppendLine("  color: #000000;");
            Html.AppendLine("  font-size: 12px;");
            Html.AppendLine("  font-family: arial;");
            Html.AppendLine("}");

            Html.AppendLine("th, td {");
            Html.AppendLine("padding: 7px;");
            Html.AppendLine("text-align: left;");
            Html.AppendLine("border-bottom: 1px solid #ddd;");
            Html.AppendLine("}");
            Html.AppendLine("tr:hover {background-color: #f5f5f5;}");

            Html.AppendLine("A {");
            Html.AppendLine("  color: 003366;");
            Html.AppendLine("  font-size: 11px;");
            Html.AppendLine("  font-family: arial;");
            Html.AppendLine("  text-decoration : none;");
            Html.AppendLine("}");
            Html.AppendLine("A:Visited {");
            Html.AppendLine("  font-size: 11px;");
            Html.AppendLine("  font-family: arial;");
            Html.AppendLine("}");
            Html.AppendLine("A:Hover {");
            Html.AppendLine("  font-size: 11px;");
            Html.AppendLine("  font-family: arial;");
            Html.AppendLine("  text-decoration : undeline;");
            Html.AppendLine("}");
            Html.AppendLine("A.clsMenu");
            Html.AppendLine("{");
            Html.AppendLine("  color: #FFFFFF;");
            Html.AppendLine("  font-weight: bold;");
            Html.AppendLine("}");
            Html.AppendLine("");
            Html.AppendLine("SELECT {");
            Html.AppendLine("  color: #003366;");
            Html.AppendLine("  font-size: 11px;");
            Html.AppendLine("  font-weight: bold;");
            Html.AppendLine("  font-family: arial;");
            Html.AppendLine("  background-color: #EEEEEE;");
            Html.AppendLine("}");
            Html.AppendLine("");
            Html.AppendLine("INPUT {");
            Html.AppendLine("  color: #003366;");
            Html.AppendLine("  font-size: 11px;");
            Html.AppendLine("  font-weight: bold;");
            Html.AppendLine("  font-family: arial;");
            Html.AppendLine("  background-color: #EEEEEE;");
            Html.AppendLine("}");
            Html.AppendLine("");
            Html.AppendLine("TEXTAREA {");
            Html.AppendLine("  color: #003366;");
            Html.AppendLine("  font-size: 11px;");
            Html.AppendLine("  font-weight: bold;");
            Html.AppendLine("  font-family: arial;");
            Html.AppendLine("  background-color: #EEEEEE;");
            Html.AppendLine("}");
            Html.AppendLine("</style>");
            Html.AppendLine("</head>");
            Html.AppendLine("");
            Html.AppendLine("<body>");
            Html.AppendLine("<form action=\"/rastreamento/sro\" name=\"Recipiente\" method=\"Post\">");
            Html.AppendLine("");

            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                Html.AppendLine("	<table align=\"Center\" width=\"750\" style=\"border:1px solid #000000;\"> ");
                Html.AppendLine("	<tbody>		");
                Html.AppendLine("		<tr>");
                Html.AppendLine("		    <td width=\"100\" bgcolor=\"#F3F3F3\"><font size=\"1\"><b><font size=\"3\">Número LDI</font></b></font></td>");
                Html.AppendLine("		    <td width=\"120\" bgcolor=\"#F3F3F3\"><font size=\"1\"><b><font size=\"3\">Objeto</font></b></font></td>");
                Html.AppendLine("		    <td width=\"380\" bgcolor=\"#F3F3F3\"><font size=\"1\"><b><font size=\"3\">Nome do cliente</font></b></font></td>");
                Html.AppendLine("		    <td width=\"150\" bgcolor=\"#F3F3F3\"><font size=\"1\"><b><font size=\"3\">Lançamento</font></b></font></td>");
                Html.AppendLine("		</tr>");
                foreach (string itemCodigoSelecionado in CodigoSelecionados)
                {
                    DataRow dr = dao.RetornaDataRow("SELECT Codigo, CodigoObjeto, CodigoLdi, NomeCliente, DataLancamento, Atualizado, Situacao, DataModificacao FROM TabelaObjetosSROLocal WHERE (CodigoObjeto IN ('" + itemCodigoSelecionado + "'))");
                    DataRow drConfiguracoes = dao.RetornaDataRow("SELECT NomeAgenciaLocal, EnderecoAgenciaLocal FROM TabelaConfiguracoesSistema");
                    Html.AppendLine("		<tr>");
                    Html.AppendLine("		    <td>" + dr["CodigoLdi"].ToString() + "</td>");
                    Html.AppendLine("		    <td>" + dr["CodigoObjeto"].ToString() + "</td>");
                    Html.AppendLine("		    <td>" + dr["NomeCliente"].ToString() + "</td>");
                    Html.AppendLine("		    <td>" + dr["DataLancamento"].ToString() + "</td>");
                    Html.AppendLine("		</tr>");
                }
                Html.AppendLine("	</tbody>");
                Html.AppendLine("	</table>");
                //Html.AppendLine("	<div class=\"espaco\">--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</div>");
                Html.AppendLine("	<div class=\"espaco\"></div>");
                Html.AppendLine("		");
            }

            Html.AppendLine("</form>");
            Html.AppendLine("");
            Html.AppendLine("<form action=\"/rastreamento/sro\" name=\"Rodape\" method=\"Post\">");
            Html.AppendLine("	<table align=\"Center\" border=\"0\">");
            Html.AppendLine("		<tbody>");
            Html.AppendLine("		<tr>");
            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeArquivo = curDir + "\\Resources\\imp01.gif";
            Html.AppendLine("			<td align=\"Center\"><a href=\"javascript: window.print();\" name=\"Imprimir\"><img src=\"" + nomeArquivo + "\" alt=\"\" height=\"16\" width=\"19\" border=\"0\"></a></td>");
            Html.AppendLine("		</tr>");
            Html.AppendLine("		<tr>");
            Html.AppendLine("			<td align=\"Center\"><a href=\"javascript: window.print();\" name=\"Imprimir\"> </a></td>			");
            Html.AppendLine("		</tr>");
            Html.AppendLine("	</tbody></table>");
            Html.AppendLine("</form>");
            Html.AppendLine("</body>");
            Html.AppendLine("</html>");

            return Html;
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
