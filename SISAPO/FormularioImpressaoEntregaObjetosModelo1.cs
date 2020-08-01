using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SISAPO
{
    public partial class FormularioImpressaoEntregaObjetosModelo1 : Form
    {
        public List<string> CodigoSelecionados = new List<string>();
        public DataTable CodigosSelecionadoAgrupados = new DataTable();

        public delegate void InvokeDelegate();

        public FormularioImpressaoEntregaObjetosModelo1(List<string> _codigosSelecionados)
        {
            InitializeComponent();
            CodigoSelecionados = _codigosSelecionados;

            FormularioImpressaoEntregaAgruparObjetos _formularioImpressaoEntregaAgruparObjetos = new FormularioImpressaoEntregaAgruparObjetos(CodigoSelecionados);
            _formularioImpressaoEntregaAgruparObjetos.ShowDialog();
            if (_formularioImpressaoEntregaAgruparObjetos.ImpressaoCancelada)
            {
                //BeginInvoke(new InvokeDelegate(Fecha_Form));
                return;
            }
            System.Data.DataColumn newColumn = new System.Data.DataColumn("Impresso", typeof(System.Int32));
            newColumn.DefaultValue = 0;
            _formularioImpressaoEntregaAgruparObjetos.DadosAgrupados.Columns.Add(newColumn);

            CodigosSelecionadoAgrupados = _formularioImpressaoEntregaAgruparObjetos.DadosAgrupados;
            CodigosSelecionadoAgrupados.DefaultView.Sort = "NomeCliente";
            CodigosSelecionadoAgrupados = CodigosSelecionadoAgrupados.DefaultView.ToTable();

            string Html = RetornaHtml().ToString();
            GeraArquivoProntoHTMLImpressao(Html, string.Format("SISAPO-SRO-Lista-de-entrega-[Modelo-Comum-{0:yyyyMMddHHmmss}].html", DateTime.Now));
            //webBrowser1.DocumentText = Html;
        }

        private void FormularioImpressaoEntregaObjetosModelo1_Load(object sender, EventArgs e)
        {
            FormularioImpressaoEntregaAgruparObjetos _formularioImpressaoEntregaAgruparObjetos = new FormularioImpressaoEntregaAgruparObjetos(CodigoSelecionados);
            _formularioImpressaoEntregaAgruparObjetos.ShowDialog();
            if (_formularioImpressaoEntregaAgruparObjetos.ImpressaoCancelada)
            {
                BeginInvoke(new InvokeDelegate(Fecha_Form));
                return;
            }
            System.Data.DataColumn newColumn = new System.Data.DataColumn("Impresso", typeof(System.Int32));
            newColumn.DefaultValue = 0;
            _formularioImpressaoEntregaAgruparObjetos.DadosAgrupados.Columns.Add(newColumn);

            CodigosSelecionadoAgrupados = _formularioImpressaoEntregaAgruparObjetos.DadosAgrupados;
            //CodigosSelecionadoAgrupados.DefaultView.Sort = "NomeCliente";
            CodigosSelecionadoAgrupados = CodigosSelecionadoAgrupados.DefaultView.ToTable();

            string Html = RetornaHtml().ToString();
            GeraArquivoProntoHTMLImpressao(Html, string.Format("SISAPO-SRO-Lista-de-entrega-[Modelo-Comum-{0:yyyyMMddHHmmss}].html", DateTime.Now));
            //webBrowser1.DocumentText = Html;
        }

        public void GeraArquivoProntoHTMLImpressao(string html, string nomeArquivoHtml)
        {
            string curDirTemp = Path.GetTempPath();
            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeEnderecoArquivo = string.Format(@"{0}{1}", curDirTemp, nomeArquivoHtml);
            //string nomeArquivo = curDir + "\\Resources\\imp01.gif";

            if (!Arquivos.Existe(string.Format(@"{0}{1}", curDirTemp, "imp01.gif"), false))
            {
                if (Arquivos.Existe(string.Format(@"{0}\Resources\{1}", curDir, "imp01.gif"), false))
                {
                    File.Copy(string.Format(@"{0}\Resources\{1}", curDir, "imp01.gif"), string.Format(@"{0}{1}", curDirTemp, "imp01.gif"));//Copia o arquivo “arquivo.txt” da unidade C: para a D:
                }
            }

            //grava texto no arquivo
            using (Arquivos arq = new Arquivos())
            {
                arq.GravarArquivo(nomeEnderecoArquivo, html);
            }

            webBrowser1.Navigate("");
            //this.webBrowser1.Url = new Uri(nomeEnderecoArquivo);

            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

            if (File.Exists(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            }
            else if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            }
            else if (File.Exists(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            }
            else if (File.Exists(@"C:\Program Files\Google\Chrome\Application\chrome.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            }
            else
            {
                return;
            }

            pProcess.StartInfo.Arguments = string.Format("\"{0}\"", nomeEnderecoArquivo);
            pProcess.Start();
            //pProcess.WaitForExit();
        }

        private void Fecha_Form()
        {
            this.Close();
        }

        private StringBuilder Html = new StringBuilder();
        private StringBuilder RetornaHtml()
        {
            Html = new StringBuilder();
            Html.AppendLine("<!DOCTYPE html>");
            Html.AppendLine("<html>");
            Html.AppendLine("<head>");
            Html.AppendLine("   <title>" + string.Format("SISAPO-SRO Lista de entrega [{0:dd/MM/yyyy HH:mm:ss}]", DateTime.Now) + "</title>");
            Html.AppendLine("   <meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\"/>");
            Html.AppendLine("   <style type=\"text/css\">");
            Html.AppendLine("       .espaco {");
            Html.AppendLine("           padding: 9.5px;");
            Html.AppendLine("           text-align:center;");
            Html.AppendLine("       }");
            Html.AppendLine("       TD.clsMenu{");
            Html.AppendLine("           color: #FFFFFF;");
            Html.AppendLine("           font-weight: bold;");
            Html.AppendLine("       }");
            Html.AppendLine("       BODY{");
            Html.AppendLine("           margin-top: 0;");
            Html.AppendLine("           margin-left: 0;");
            Html.AppendLine("           margin-right: 0;");
            Html.AppendLine("           color: #003399;");
            Html.AppendLine("           font-size: 12px;");
            Html.AppendLine("           font-family: arial;");
            Html.AppendLine("           background-color: #FFFFFF;");
            Html.AppendLine("       }");
            Html.AppendLine("       TD{");
            Html.AppendLine("           color: #000000;");
            Html.AppendLine("           font-size: 12px;");
            Html.AppendLine("           font-family: arial;");
            Html.AppendLine("       }");
            Html.AppendLine("       A{");
            Html.AppendLine("           color: 003366;");
            Html.AppendLine("           font-size: 11px;");
            Html.AppendLine("           font-family: arial;");
            Html.AppendLine("           text-decoration : none;");
            Html.AppendLine("       }");
            Html.AppendLine("       A:Visited{");
            Html.AppendLine("           font-size: 11px;");
            Html.AppendLine("           font-family: arial;");
            Html.AppendLine("       }");
            Html.AppendLine("       A:Hover{");
            Html.AppendLine("           font-size: 11px;");
            Html.AppendLine("           font-family: arial;");
            Html.AppendLine("           text-decoration : undeline;");
            Html.AppendLine("       }");
            Html.AppendLine("       A.clsMenu{");
            Html.AppendLine("           color: #FFFFFF;");
            Html.AppendLine("           font-weight: bold;");
            Html.AppendLine("       }");
            Html.AppendLine("       SELECT{");
            Html.AppendLine("           color: #003366;");
            Html.AppendLine("           font-size: 11px;");
            Html.AppendLine("           font-weight: bold;");
            Html.AppendLine("           font-family: arial;");
            Html.AppendLine("           background-color: #EEEEEE;");
            Html.AppendLine("       }");
            Html.AppendLine("       INPUT{");
            Html.AppendLine("           color: #003366;");
            Html.AppendLine("           font-size: 11px;");
            Html.AppendLine("           font-weight: bold;");
            Html.AppendLine("           font-family: arial;");
            Html.AppendLine("           background-color: #EEEEEE;");
            Html.AppendLine("       }");
            Html.AppendLine("       TEXTAREA{");
            Html.AppendLine("           color: #003366;");
            Html.AppendLine("           font-size: 11px;");
            Html.AppendLine("           font-weight: bold;");
            Html.AppendLine("           font-family: arial;");
            Html.AppendLine("           background-color: #EEEEEE;");
            Html.AppendLine("       }");
            Html.AppendLine("       </style>");
            Html.AppendLine("       </head>");
            Html.AppendLine("       ");
            Html.AppendLine("   <body>");
            Html.AppendLine("   <form action=\"/rastreamento/sro\" name=\"Recipiente\" method=\"Post\">");
            Html.AppendLine("   ");

            using (FormWaiting frm = new FormWaiting(ProcessandoListaObjetosSelecionados))
            {
                frm.ShowDialog(this);
            }

            Html.AppendLine("</form>");
            Html.AppendLine("");
            Html.AppendLine("<form action=\"/rastreamento/sro\" name=\"Rodape\" method=\"Post\">");
            Html.AppendLine("	<table align=\"Center\" border=\"0\">");
            Html.AppendLine("		<tbody>");
            Html.AppendLine("		<tr>");
            string curDirTemp = System.IO.Path.GetTempPath();
            string nomeArquivo = curDirTemp + "imp01.gif";
            nomeArquivo = nomeArquivo.Replace("C:", "file:///C:");
            nomeArquivo = nomeArquivo.Replace("D:", "file:///D:");
            nomeArquivo = nomeArquivo.Replace("\\", "/");
            Html.AppendLine("			<td align=\"Center\"><a href=\"javascript: window.print();\" name=\"Imprimir\"><img src=\"" + nomeArquivo + "\" alt=\"Imprimir\" height=\"16\" width=\"19\" border=\"0\"></a></td>");
            Html.AppendLine("		</tr>");
            Html.AppendLine("		<tr>");
            Html.AppendLine("			<td align=\"Center\"><a href=\"javascript: window.print();\" name=\"Imprimir\"></a></td>			");
            Html.AppendLine("		</tr>");
            Html.AppendLine("	</tbody></table>");
            Html.AppendLine("</form>");
            Html.AppendLine("</body>");
            Html.AppendLine("</html>");

            return Html;
        }

        public void ProcessandoListaObjetosSelecionados()
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                DataRow drConfiguracoes = dao.RetornaDataRow("SELECT NomeAgenciaLocal, EnderecoAgenciaLocal FROM TabelaConfiguracoesSistema");
                int contador = 0;
                foreach (DataRow itemCodigoSelecionado in CodigosSelecionadoAgrupados.Rows)
                {
                    if (itemCodigoSelecionado["Impresso"].ToInt() == 1) continue;

                    string codigoAtual = itemCodigoSelecionado["CodigoObjeto"].ToString();
                    string nomeAtual = itemCodigoSelecionado["NomeCliente"].ToString();
                    List<object> ListaCodigoAgrupados = CodigosSelecionadoAgrupados.AsEnumerable()
                        .Where(G => G["Grupo"].ToString() == itemCodigoSelecionado["CodigoObjeto"].ToString() && G["Impresso"].ToInt() == 0)
                        .Select(C => C["CodigoObjeto"]).ToList();

                    DataRow dr = dao.RetornaDataRow("SELECT Codigo, CodigoObjeto, CodigoLdi, NomeCliente, DataLancamento, Atualizado, Situacao, DataModificacao FROM TabelaObjetosSROLocal WHERE (CodigoObjeto IN ('" + itemCodigoSelecionado["CodigoObjeto"] + "'))");

                    Html.AppendLine("	<table align=\"Center\" width=\"750\" style=\"border:1px solid #000000;\"> ");
                    Html.AppendLine("	<tbody>		");



                    if (ListaCodigoAgrupados.Count == 0)
                    {
                        #region Se Entregue / Não Entregue                        
                        if (dr["DataModificacao"].ToString().Trim() == "") // não entregue
                        {
                            Html.AppendLine("		<tr>");
                            Html.AppendLine("			<td colspan=\"2\" align=\"Left\" bgcolor=\"#F3F3F3\"><font size=\"1\"><b><font size=\"3\">Lançamento: " + dr["DataLancamento"].ToDateTime().ToShortDateString() + "</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font size=\"3\">Data da Entrega: ______/______/__________</font></b></font></td>");
                            Html.AppendLine("		</tr>");

                            Html.AppendLine("		<tr>");
                            Html.AppendLine("			<td colspan=\"2\" align=\"Left\" bgcolor=\"#F3F3F3\"><div style=\"font-size: 16px; float: left; margin-top: 1px; border: 0px solid rgb(252, 0, 0); width: 46em; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;\"><b> " + dr["CodigoObjeto"].ToString().Trim() + " - " + dr["NomeCliente"].ToString().Trim() + "</b></div></td>");
                            Html.AppendLine("		</tr>		");
                        }
                        else // já entregue
                        {
                            //Html.AppendLine("		<tr>");
                            //Html.AppendLine("			<td colspan=\"2\" align=\"Left\" bgcolor=\"#F3F3F3\"><font size=\"1\"><b><font size=\"3\">Lançamento: " + dr["DataLancamento"].ToDateTime().ToShortDateString() + "&nbsp;--[" + dr["Situacao"].ToString().Trim() + "]--&nbsp;Data da modificação: " + dr["DataModificacao"].ToString().Trim() + "</font></b></font></td>");
                            //Html.AppendLine("		</tr>");
                            Html.AppendLine("		<tr>");
                            Html.AppendLine("			<td colspan=\"2\" align=\"Left\" bgcolor=\"#F3F3F3\"><font size=\"1\"><b><font size=\"3\">Lançamento: " + dr["DataLancamento"].ToDateTime().ToShortDateString() + "</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font size=\"4\">Data da Baixa: " + dr["DataModificacao"].ToString().Trim() + "</font></b></font></td>");
                            Html.AppendLine("		</tr>");

                            Html.AppendLine("		<tr>");
                            Html.AppendLine("			<td colspan=\"2\" align=\"Left\" bgcolor=\"#F3F3F3\"><div style=\"font-size: 16px; float: left; margin-top: 1px; border: 0px solid rgb(252, 0, 0); width: 46em; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;\"><b> " + dr["CodigoObjeto"].ToString().Trim() + " - " + dr["NomeCliente"].ToString().Trim() + "</b></div></td>");
                            Html.AppendLine("		</tr>		");
                        }                        
                        #endregion                        
                        CodigosSelecionadoAgrupados.Rows[contador]["Impresso"] = 1;
                    }
                    else//(ListaCodigoAgrupados.Count > 0)
                    {
                        Html.AppendLine("		<tr>");
                        Html.AppendLine("			<td colspan=\"2\" align=\"Left\" bgcolor=\"#F3F3F3\"><font size=\"1\"><b><font size=\"3\">Lançamento: " + dr["DataLancamento"].ToDateTime().ToShortDateString() + "</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font size=\"3\">Data da Entrega: ______/______/__________</font></b></font></td>");
                        Html.AppendLine("		</tr>");

                        foreach (object item in ListaCodigoAgrupados)
                        {
                            DataRow drGrupo = dao.RetornaDataRow("SELECT CodigoObjeto, NomeCliente, DataLancamento, Situacao, DataModificacao FROM TabelaObjetosSROLocal WHERE CodigoObjeto IN ('" + item.ToString() + "')");

                            #region Não Entregue
                            if (drGrupo["DataModificacao"].ToString().Trim() == "") // não entregue
                            {
                                Html.AppendLine("		<tr>");
                                Html.AppendLine("			<td colspan=\"2\" align=\"Left\" bgcolor=\"#F3F3F3\"><div style=\"font-size: 16px; float: left; margin-top: 1px; border: 0px solid rgb(252, 0, 0); width: 46em; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;\"><b> " + drGrupo["CodigoObjeto"].ToString().Trim() + " - " + drGrupo["NomeCliente"].ToString().Trim() + "</b></div></td>");
                                Html.AppendLine("		</tr>		");
                            }
                            #endregion

                            int novoContador = 0;
                            foreach (DataRow items in CodigosSelecionadoAgrupados.Rows)
                            {
                                if (CodigosSelecionadoAgrupados.Rows[novoContador]["CodigoObjeto"].ToString() == drGrupo["CodigoObjeto"].ToString())
                                {
                                    CodigosSelecionadoAgrupados.Rows[novoContador]["Impresso"] = 1;
                                    break;
                                }
                                novoContador++;
                            }
                        }
                    }
                    if (dr["DataModificacao"].ToString().Trim() == "") // não entregue
                    {
                        Html.AppendLine("		");
                        Html.AppendLine("		<tr>");
                        Html.AppendLine("			<td align=\"Right\" width=\"400\" bgcolor=\"#F3F3F3\"><b>&nbsp;Nome&nbsp;do&nbsp;recebedor:</b></td>");
                        Html.AppendLine("			<td align=\"Left\" width=\"auto\" bgcolor=\"#F3F3F3\"><font size=\"4\">____________________________________________________________</font></td>");
                        Html.AppendLine("		</tr>	");
                        Html.AppendLine("		<tr>");
                        Html.AppendLine("			<td align=\"Right\" width=\"400\" bgcolor=\"#F3F3F3\"><b> Doc. do recebedor:</b></td>");
                        Html.AppendLine("			<td align=\"Left\" width=\"auto\" bgcolor=\"#F3F3F3\"><font size=\"4\">___________________________&nbsp;</font>CPF:<font size=\"4\">______________________________</font></td>");
                        Html.AppendLine("		</tr>");

                        if (ListaCodigoAgrupados.Count == 0)
                        {
                            Html.AppendLine("		<tr>");
                            Html.AppendLine("			<td colspan=\"2\" align=\"Center\" bgcolor=\"#F3F3F3\"><font size=\"2\">Declaro ter recebido o objeto " + dr["CodigoObjeto"].ToString().Trim() + " na agência " + drConfiguracoes["NomeAgenciaLocal"].ToString() + " <br>localizada na " + drConfiguracoes["EnderecoAgenciaLocal"].ToString() + ". Por ser verdade assino.</br></font></td>");
                            Html.AppendLine("		</tr>	");
                        }
                        else//(ListaCodigoAgrupados.Count > 0)
                        {
                            Html.AppendLine("		<tr>");
                            Html.AppendLine("			<td colspan=\"2\" align=\"Center\" bgcolor=\"#F3F3F3\"><font size=\"2\">Declaro ter recebido os objetos acima mencionados na agência " + drConfiguracoes["NomeAgenciaLocal"].ToString() + " <br>localizada na " + drConfiguracoes["EnderecoAgenciaLocal"].ToString() + ". Por ser verdade assino.</br></font></td>");
                            Html.AppendLine("		</tr>	");
                        }

                        Html.AppendLine("		<tr>");
                        Html.AppendLine("			<td colspan=\"2\" align=\"center\" bgcolor=\"#F3F3F3\"><font size=\"4\">__________________________________________________</font></td>");
                        Html.AppendLine("		</tr>");
                        Html.AppendLine("		<tr>");
                        Html.AppendLine("			<td colspan=\"2\" align=\"center\" bgcolor=\"#F3F3F3\"><font size=\"1\">Assinatura legível do recebedor.</font></td>");
                        Html.AppendLine("		</tr>");
                    }
                    else // já entregue
                    {
                        Html.AppendLine("		");
                        //Html.AppendLine("		<tr>");
                        //Html.AppendLine("			<td colspan=\"2\" align=\"Left\" bgcolor=\"#F3F3F3\"><font size=\"1\"><b><font size=\"3\">Data da Baixa: " + dr["DataModificacao"].ToDateTime().ToShortDateString() + "</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font size=\"3\">&nbsp;</font></b></font></td>");
                        //Html.AppendLine("		</tr>");
                        Html.AppendLine("		<tr>");
                        Html.AppendLine("			<td colspan=\"2\" align=\"Left\" bgcolor=\"#F3F3F3\"><font size=\"4\"><b>SITUAÇÃO: <u>" + dr["Situacao"].ToString() + "</u></b></font></td>");
                        Html.AppendLine("		</tr>");

                        Html.AppendLine("		<tr>");
                        Html.AppendLine("			<td colspan=\"2\" align=\"left\" bgcolor=\"#F3F3F3\"><font size=\"1\">Obs.: No momento da impressão desta lista o objeto atual se encontra <u>'" + dr["Situacao"].ToString() + "'</u> </font></td>");
                        Html.AppendLine("		</tr>");
                    }
                    Html.AppendLine("	</tbody>");
                    Html.AppendLine("	</table>");
                    //Html.AppendLine("	<div class=\"espaco\">--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</div>");
                    Html.AppendLine("	<div class=\"espaco\"></div>");
                    Html.AppendLine("		");
                    contador++;
                }
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void FormularioImpressaoEntregaObjetosModelo1_KeyDown(object sender, KeyEventArgs e)
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
