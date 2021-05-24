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
        private DataTable listaObjetos;

        public FormularioImpressaoAuxilioGestaoDia(DataTable _listaObjetos)
        {
            InitializeComponent();
            this.listaObjetos = _listaObjetos;
            //Classificacao.AsEnumerable().GroupBy(t => t).Distinct().ToList()
            Dictionary<string, DataTable> listaPCT = new Dictionary<string, DataTable>();
            DataTable DtTempPCT = new DataTable();
            DtTempPCT = FormataColunasDataTable(DtTempPCT);

            Dictionary<string, DataTable> listaPCTINT = new Dictionary<string, DataTable>();
            DataTable DtTempPCTINT = new DataTable();
            DtTempPCTINT = FormataColunasDataTable(DtTempPCTINT);

            Dictionary<string, DataTable> listaENV = new Dictionary<string, DataTable>();
            DataTable DtTempENV = new DataTable();
            DtTempENV = FormataColunasDataTable(DtTempENV);

            Dictionary<string, DataTable> listaCAIXAPOSTAL = new Dictionary<string, DataTable>();
            DataTable DtTempCAIXAPOSTAL = new DataTable();
            DtTempCAIXAPOSTAL = FormataColunasDataTable(DtTempCAIXAPOSTAL);

            Dictionary<string, DataTable> listaACOBRAR = new Dictionary<string, DataTable>();
            DataTable DtTempACOBRAR = new DataTable();
            DtTempACOBRAR = FormataColunasDataTable(DtTempACOBRAR);

            foreach (DataRow item in this.listaObjetos.Rows)
            {
                string Comentario = item["Comentario"].ToString();
                #region VerificaComentarioPCT
                if (VerificaComentarioPCT(Comentario))
                {
                    DtTempPCT.Rows.Add(item["CodigoLdi"], item["Sigla"], item["CodigoObjeto"], item["TipoClassificacao"], item["NomeCliente"], item["Comentario"], item["CaixaPostal"], item["DataLancamento"], item["QtdDiasCorridos"], item["PrazoTipoClassificacao"], item["DataVencimento"], item["StatusPrazo"], item["QtdDiasVencidos"]);
                    continue;
                }
                #endregion
                #region VerificaComentarioPCTINT
                if (VerificaComentarioPCTINT(Comentario))
                {
                    DtTempPCTINT.Rows.Add(item["CodigoLdi"], item["Sigla"], item["CodigoObjeto"], item["TipoClassificacao"], item["NomeCliente"], item["Comentario"], item["CaixaPostal"], item["DataLancamento"], item["QtdDiasCorridos"], item["PrazoTipoClassificacao"], item["DataVencimento"], item["StatusPrazo"], item["QtdDiasVencidos"]);
                    continue;
                }
                #endregion
                #region VerificaComentarioENV
                if (VerificaComentarioENV(Comentario))
                {
                    DtTempENV.Rows.Add(item["CodigoLdi"], item["Sigla"], item["CodigoObjeto"], item["TipoClassificacao"], item["NomeCliente"], item["Comentario"], item["CaixaPostal"], item["DataLancamento"], item["QtdDiasCorridos"], item["PrazoTipoClassificacao"], item["DataVencimento"], item["StatusPrazo"], item["QtdDiasVencidos"]);
                    continue;
                }
                #endregion
                #region VerificaComentarioCAIXAPOSTAL
                if (VerificaComentarioCAIXAPOSTAL(Comentario))
                {
                    DtTempCAIXAPOSTAL.Rows.Add(item["CodigoLdi"], item["Sigla"], item["CodigoObjeto"], item["TipoClassificacao"], item["NomeCliente"], item["Comentario"], item["CaixaPostal"], item["DataLancamento"], item["QtdDiasCorridos"], item["PrazoTipoClassificacao"], item["DataVencimento"], item["StatusPrazo"], item["QtdDiasVencidos"]);
                    continue;
                }
                #endregion
                #region VerificaComentarioACOBRAR
                if (VerificaComentarioACOBRAR(Comentario))
                {
                    DtTempACOBRAR.Rows.Add(item["CodigoLdi"], item["Sigla"], item["CodigoObjeto"], item["TipoClassificacao"], item["NomeCliente"], item["Comentario"], item["CaixaPostal"], item["DataLancamento"], item["QtdDiasCorridos"], item["PrazoTipoClassificacao"], item["DataVencimento"], item["StatusPrazo"], item["QtdDiasVencidos"]);
                    continue;
                }
                #endregion
                DtTempPCT.Rows.Add(item["CodigoLdi"], item["Sigla"], item["CodigoObjeto"], item["TipoClassificacao"], item["NomeCliente"], item["Comentario"], item["CaixaPostal"], item["DataLancamento"], item["QtdDiasCorridos"], item["PrazoTipoClassificacao"], item["DataVencimento"], item["StatusPrazo"], item["QtdDiasVencidos"]);
            }
        }

        private DataTable FormataColunasDataTable(DataTable datatable)
        {
            datatable.Columns.Add("CodigoLdi", typeof(string));
            datatable.Columns.Add("Sigla", typeof(string));
            datatable.Columns.Add("CodigoObjeto", typeof(string));
            datatable.Columns.Add("TipoClassificacao", typeof(string));
            datatable.Columns.Add("NomeCliente", typeof(string));
            datatable.Columns.Add("Comentario", typeof(string));
            datatable.Columns.Add("CaixaPostal", typeof(bool));
            datatable.Columns.Add("DataLancamento", typeof(DateTime));
            datatable.Columns.Add("QtdDiasCorridos", typeof(string));
            datatable.Columns.Add("PrazoTipoClassificacao", typeof(int));
            datatable.Columns.Add("DataVencimento", typeof(DateTime));
            datatable.Columns.Add("StatusPrazo", typeof(string));
            datatable.Columns.Add("QtdDiasVencidos", typeof(string));
            return datatable;
        }

        private bool VerificaComentarioACOBRAR(string comentario)
        {
            if (comentario.Contains("A COBRAR")) return true;
            return false;
        }

        private bool VerificaComentarioCAIXAPOSTAL(string comentario)
        {
            if (comentario.Contains("CAIXA POSTAL")) return true;
            return false;
        }

        private bool VerificaComentarioENV(string comentario)
        {
            if (comentario.Contains("ENV")) return true;
            return false;
        }

        private bool VerificaComentarioPCTINT(string comentario)
        {
            if (comentario.Contains("PCT"))
                if (comentario.Replace("PCT", "").Trim() != "")
                    if (comentario.Replace("PCT", "").Trim().Contains("INT"))
                        return true;
            return false;
        }

        private bool VerificaComentarioPCT(string comentario)
        {
            if (comentario.Contains("PCT"))
            {
                if (comentario.Contains("INT"))
                {
                    return false;
                }
                if (comentario.Contains("INTERNACIONAL"))
                {
                    return false;
                }
                return true;
            }
            return false;
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
            Html.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\" />");
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
            Html.AppendLine("  font-size: 16px;");
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

            Html.AppendLine("	<table align=\"Center\" width=\"850\" style=\"border:1px solid #000000;\"> ");
            Html.AppendLine("	<tbody>		");
            Html.AppendLine("		<tr>");
            Html.AppendLine("		    <td width=\"770\" bgcolor=\"#F3F3F3\" style=\"text-align:right\"><b><font size=\"3\">TOTAL:</font></b></td>");
            Html.AppendLine("		    <td width=\"80\" bgcolor=\"#F3F3F3\" style=\"text-align:center\"><b><font size=\"6\">" + listaObjetos.Rows.Count + "</font></b></td>");
            Html.AppendLine("		</tr>");
            Html.AppendLine("	</tbody>");
            Html.AppendLine("	</table>");

            Html.AppendLine("	<table align=\"Center\" width=\"850\" style=\"border:1px solid #000000;\"> ");
            Html.AppendLine("	<tbody>		");
            Html.AppendLine("		<tr>");
            Html.AppendLine("		    <td width=\"90\" bgcolor=\"#F3F3F3\"><b><font size=\"3\">  Núm. LDI</font></b></td>");
            Html.AppendLine("		    <td width=\"150\" bgcolor=\"#F3F3F3\"><b><font size=\"3\">  Objeto</font></b></td>");
            Html.AppendLine("		    <td width=\"450\" bgcolor=\"#F3F3F3\"><b><font size=\"3\">  Destinatário</font></b></td>");
            Html.AppendLine("		    <td width=\"90\" bgcolor=\"#F3F3F3\"><b><font size=\"3\">  Lançamento</font></b></td>");
            Html.AppendLine("		    <td width=\"70\" bgcolor=\"#F3F3F3\"><b><font size=\"3\">  Corridos</font></b></td>");
            Html.AppendLine("		</tr>");
            foreach (DataRow dr in listaObjetos.Rows)
            {
                #region Modelo QE 812 219 679 BR
                //QE 812 219 679 BR
                //string CodigoObjetoFormatado = string.Format("{0} {1} {2} {3} {4}",
                //dr["CodigoObjeto"].ToString().Substring(0, 2),
                //dr["CodigoObjeto"].ToString().Substring(2, 3),
                //dr["CodigoObjeto"].ToString().Substring(5, 3),
                //dr["CodigoObjeto"].ToString().Substring(8, 3),
                //dr["CodigoObjeto"].ToString().Substring(11, 2));
                #endregion
                #region Modelo QE 81221967-9 BR
                //QE 81221967-9 BR
                string CodigoObjetoFormatado = string.Format("{0} {1}-{2} {3}",
                dr["CodigoObjeto"].ToString().Substring(0, 2),
                dr["CodigoObjeto"].ToString().Substring(2, 8),
                dr["CodigoObjeto"].ToString().Substring(10, 1),
                dr["CodigoObjeto"].ToString().Substring(11, 2));
                #endregion
                string DiasCorridos = Convert.ToString((DateTime.Now.Date - dr["DataLancamento"].ToDateTime().Date).TotalDays);

                Html.AppendLine("		<tr>");
                Html.AppendLine("		    <td>" + dr["CodigoLdi"].ToString() + "</td>");
                Html.AppendLine("		    <td>" + CodigoObjetoFormatado + "</td>");
                Html.AppendLine("		    <td><b>" + dr["NomeCliente"].ToString() + "</b></td>");
                Html.AppendLine("		    <td>" + dr["DataLancamento"].ToString() + "</td>");
                Html.AppendLine("		    <td>&nbsp;&nbsp;" + DiasCorridos + " dias</td>");
                Html.AppendLine("		</tr>");
            }
            Html.AppendLine("	</tbody>");
            Html.AppendLine("	</table>");
            //Html.AppendLine("	<div class=\"espaco\">--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</div>");
            Html.AppendLine("	<div class=\"espaco\"></div>");
            Html.AppendLine("		");

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
