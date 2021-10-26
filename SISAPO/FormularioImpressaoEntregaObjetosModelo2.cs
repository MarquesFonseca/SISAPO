﻿using PdfSharp;
using PdfSharp.Pdf;
using SISAPO;
using SISAPO.ClassesDiversas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace SISAPO
{
    public partial class FormularioImpressaoEntregaObjetosModelo2 : Form
    {
        public delegate void InvokeDelegate();

        public List<string> CodigoSelecionados = new List<string>();
        public DataTable CodigosSelecionadoAgrupados = new DataTable();
        public bool ImprimirUmPorFolha = false;
        public bool ImprimirVariosPorFolha = false;
        private StringBuilder Html = new StringBuilder();
        private StringBuilder StrTXTPLR = new StringBuilder();
        private StringBuilder HtmlConsolidado = new StringBuilder();

        private string DataListaAtual = string.Empty;
        private string NumeroListaAtual = string.Empty;
        private string NomeArquivoHtml = string.Empty;
        private string TituloHTML = string.Empty;
        private string NomeArquivoPLRTXT = string.Empty;

        public FormularioImpressaoEntregaObjetosModelo2(List<string> _codigosSelecionados, bool _ImprimirUmPorFolha, bool _ImprimirVariosPorFolha)
        {
            InitializeComponent();
            CodigoSelecionados = _codigosSelecionados;
            ImprimirUmPorFolha = _ImprimirUmPorFolha;
            ImprimirVariosPorFolha = _ImprimirVariosPorFolha;

            FormularioImpressaoEntregaAgruparObjetos _formularioImpressaoEntregaAgruparObjetos = new FormularioImpressaoEntregaAgruparObjetos(CodigoSelecionados);
            //_formularioImpressaoEntregaAgruparObjetos.ShowDialog();
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

            DataListaAtual = DateTime.Now.ToString();
            NumeroListaAtual = string.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
            NomeArquivoHtml = string.Format("SISAPO-SRO-Lista-de-entrega-[Modelo-LDI-{0:yyyyMMddHHmmss}].html", NumeroListaAtual);
            TituloHTML = string.Format("SISAPO-SRO Lista de entrega [{0:dd/MM/yyyy HH:mm:ss}]", DateTime.Now);
            NomeArquivoPLRTXT = string.Format("PLR-[{0:yyyyMMddHHmmss}].txt", NumeroListaAtual);

            string Html = RetornaHtml(TituloHTML).ToString();
            GeraArquivoProntoHTMLImpressao(Html, NomeArquivoHtml);
        }

        private void FormularioImpressaoEntregaObjetosModelo2_Load(object sender, EventArgs e)
        {
            StrTXTPLR = new StringBuilder();
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
            CodigosSelecionadoAgrupados.DefaultView.Sort = "NomeCliente";
            CodigosSelecionadoAgrupados = CodigosSelecionadoAgrupados.DefaultView.ToTable();


            string Html = RetornaHtml(TituloHTML).ToString();
            GeraArquivoProntoHTMLImpressao(Html, string.Format("SISAPO-SRO-Lista-de-entrega-[Modelo-LDI-{0:yyyyMMddHHmmss}].html", DateTime.Now));

            BeginInvoke(new InvokeDelegate(Fecha_Form));
            return;
        }

        public void GeraArquivoProntoHTMLImpressao(string html, string nomeArquivoHtml)
        {
            string curDirTemp = Path.GetTempPath();
            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeEnderecoArquivo = string.Format(@"{0}{1}", curDirTemp, nomeArquivoHtml);
            string nomeEnderecoArquivoPLR = string.Format(@"C:\PLR\{0:yyyy-MM-dd}\PLR-{1}.html", DateTime.Now, NumeroListaAtual);

            if (!Arquivos.Existe(string.Format(@"{0}{1}", curDirTemp, "JsBarcode.all.min.js"), false))
            {
                if (Arquivos.Existe(string.Format(@"{0}\{1}", curDir, "JsBarcode.all.min.js"), false))
                {
                    File.Copy(string.Format(@"{0}\{1}", curDir, "JsBarcode.all.min.js"), string.Format(@"{0}{1}", curDirTemp, "JsBarcode.all.min.js"));//Copia o arquivo “arquivo.txt” da unidade C: para a D:
                }
            }

            //grava texto no arquivo
            using (Arquivos arq = new Arquivos())
            {
                arq.GravarArquivo(nomeEnderecoArquivo, html);

                if (Configuracoes.GerarQRCodePLRNaLdi || Configuracoes.GerarTXTPLRNaLdi)
                {
                    arq.GravarArquivo(string.Format(@"{0}PLR-{1}.html", curDirTemp, NumeroListaAtual), HtmlConsolidado.ToString());
                    //arq.GravarArquivo(nomeEnderecoArquivoPLR, HtmlConsolidado.ToString());

                    ////using TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator;
                    //string nomeEnderecoArquivoPLRPDF = string.Format(@"C:\PLR\{0:yyyy-MM-dd}\{1}.pdf", DateTime.Now, NumeroListaAtual);
                    ////string html2 = File.ReadAllText(nomeEnderecoArquivo);
                    //PdfDocument pdf = PdfGenerator.GeneratePdf(HtmlConsolidado.ToString(), PageSize.Letter);
                    //pdf.Save(nomeEnderecoArquivoPLRPDF);
                }
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
            if (Configuracoes.GerarQRCodePLRNaLdi || Configuracoes.GerarTXTPLRNaLdi)
            {
                //pProcess = new System.Diagnostics.Process();
                pProcess.StartInfo.Arguments = string.Format(@"{0}PLR-{1}.html", curDirTemp, NumeroListaAtual);
                pProcess.Start();
            }
            //pProcess.WaitForExit();
        }

        private StringBuilder RetornaHtml(string tituloHTML)
        {
            //string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            //string nomeArquivo = curDir + "\\Resources\\JsBarcode.all.min.js";
            Html = new StringBuilder();
            #region Head HTML
            Html.AppendLine("<!DOCTYPE html>");
            Html.AppendLine("<html lang=\"pt-br\">");
            Html.AppendLine("<head>");
            Html.AppendLine("    <title>" + tituloHTML + "</title>");
            Html.AppendLine("    <meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\"/>");
            Html.AppendLine("    <script src=\"JsBarcode.all.min.js\"></script>");

            //string curDirTemp = Path.GetTempPath();
            //curDirTemp = "file:///" + curDirTemp.Replace(@"\", "/");
            //Html.AppendLine("    <script src=\""+ curDirTemp +"JsBarcode.all.min.js\"></script>");

            //string curDirTemp = @"https://cdnjs.cloudflare.com/ajax/libs/jsbarcode/3.11.5/";
            //Html.AppendLine("    <script src=\"" + curDirTemp + "JsBarcode.all.min.js\"></script>");

            Html.AppendLine("    <script type=\"text/javascript\">");
            Html.AppendLine("        window.onload = function () {");
            Html.AppendLine("            JsBarcode(\".barcode\").init();");
            Html.AppendLine("        }");
            Html.AppendLine("    </script>");
            Html.AppendLine("    <style type=\"text/css\">");
            Html.AppendLine("        .quebrapagina {");
            Html.AppendLine("            page-break-before: always;");
            Html.AppendLine("            page-break-inside: avoid;");
            Html.AppendLine("        }");
            Html.AppendLine("        BODY {");
            Html.AppendLine("            margin-top: 0;");
            Html.AppendLine("            margin-left: 0;");
            Html.AppendLine("            margin-right: 0;");
            Html.AppendLine("            background-color: #FFFFFF;");
            Html.AppendLine("        }");
            Html.AppendLine("        .geral {");
            Html.AppendLine("            border: 0px solid rgb(252, 0, 0);");
            Html.AppendLine("            width: 850px;");
            Html.AppendLine("            margin: auto;");
            Html.AppendLine("            min-height: 1100px;");
            Html.AppendLine("        }");
            Html.AppendLine("         ");
            Html.AppendLine("        table.TabelaPLR {");
            Html.AppendLine("          font-family: \"Courier New\", Courier, monospace;");
            Html.AppendLine("          border: 0px dotted #000000;");
            Html.AppendLine("          background-color: #EEEEEE;");
            Html.AppendLine("          width: 350px;");
            Html.AppendLine("          height: 250px;");
            Html.AppendLine("          text-align: left;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.TabelaPLR td, table.TabelaPLR th {");
            Html.AppendLine("          border: 1px dotted #000000;");
            Html.AppendLine("          padding: 3px 2px;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.TabelaPLR tbody td {");
            Html.AppendLine("          font-size: 12px;");
            Html.AppendLine("          font-weight: bold;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.TabelaPLR tr:nth - child(even) {");
            Html.AppendLine("            background: #C1C1C1;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.TabelaPLR thead {");
            Html.AppendLine("          background: #ADADAD;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.TabelaPLR thead th {");
            Html.AppendLine("          font-size: 19px;");
            Html.AppendLine("          font-weight: bold;");
            Html.AppendLine("          color: #000000;");
            Html.AppendLine("          text-align: center;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.TabelaPLR tfoot td {");
            Html.AppendLine("          font-size: 21px;");
            Html.AppendLine("        }");
            Html.AppendLine("         ");
            Html.AppendLine("        table.Consolidado {");
            Html.AppendLine("          font-family: Arial, Helvetica, sans-serif;");
            Html.AppendLine("          border: 1px solid #000000;");
            Html.AppendLine("          background-color: #EEEEEE;");
            Html.AppendLine("          width: 100%;");
            Html.AppendLine("          text-align: left;");
            Html.AppendLine("          margin-top: 10px;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.Consolidado td, table.Consolidado th {");
            Html.AppendLine("          padding: 3px 2px;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.Consolidado tbody td {");
            Html.AppendLine("          font-size: 14px;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.Consolidado tr:nth-child(even) {");
            Html.AppendLine("          background: #C1C1C1;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.Consolidado thead {");
            Html.AppendLine("          background: #C4C4C4;");
            Html.AppendLine("          border-bottom: 2px solid #444444;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.Consolidado thead th {");
            Html.AppendLine("          font-size: 15px;");
            Html.AppendLine("          font-weight: bold;");
            Html.AppendLine("          color: #000000;");
            Html.AppendLine("          border-left: 2px solid #C4C4C4;");
            Html.AppendLine("        }");
            Html.AppendLine("        table.Consolidado thead th:first-child {");
            Html.AppendLine("          border-left: none;");
            Html.AppendLine("        }");
            Html.AppendLine("    </style>");
            Html.AppendLine("</head>");
            Html.AppendLine("<body>");
            Html.AppendLine("    <div class=\"geral\">");
            #endregion

            using (FormWaiting frm = new FormWaiting(ProcessandoListaObjetosSelecionados))
            {
                frm.ShowDialog(this);
            }

            #region Rodapé
            Html.AppendLine("    </div>");
            Html.AppendLine("</body>");
            Html.AppendLine("</html>");
            #endregion

            return Html;
        }

        public void ProcessandoListaObjetosSelecionados()
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                DataRow drConfiguracoes = dao.RetornaDataRow("SELECT top 1 NomeAgenciaLocal, EnderecoAgenciaLocal, SuperintendenciaEstadual, CepUnidade FROM TabelaConfiguracoesSistema");
                string NomeAgenciaLocal = drConfiguracoes["NomeAgenciaLocal"].ToString();
                string CepUnidade = drConfiguracoes["CepUnidade"].ToString();
                string SuperintendenciaEstadual = drConfiguracoes["SuperintendenciaEstadual"].ToString();

                int contador = 0;
                foreach (DataRow itemCodigoSelecionado in CodigosSelecionadoAgrupados.Rows)
                {
                    //if (itemCodigoSelecionado["Impresso"].ToInt() == 1) continue;
                    contador++;
                    string codigoAtual = itemCodigoSelecionado["CodigoObjeto"].ToString();
                    string nomeAtual = itemCodigoSelecionado["NomeCliente"].ToString();
                    //List<object> ListaCodigoAgrupados = CodigosSelecionadoAgrupados.AsEnumerable()
                    //    .Where(G => G["Grupo"].ToString() == itemCodigoSelecionado["CodigoObjeto"].ToString() && G["Impresso"].ToInt() == 0)
                    //    .Select(C => C["CodigoObjeto"]).ToList();

                    DataRow dr = dao.RetornaDataRow("SELECT top 1 Codigo, CodigoObjeto, CodigoLdi, NomeCliente, DataLancamento, DataModificacao, Situacao, Atualizado, ObjetoEntregue, CaixaPostal, UnidadePostagem, MunicipioPostagem, CriacaoPostagem, CepDestinoPostagem, ARPostagem, MPPostagem, DataMaxPrevistaEntregaPostagem, UnidadeLOEC, MunicipioLOEC, CriacaoLOEC, CarteiroLOEC, DistritoLOEC, NumeroLOEC, EnderecoLOEC, BairroLOEC, LocalidadeLOEC, SituacaoDestinatarioAusente, AgrupadoDestinatarioAusente, CoordenadasDestinatarioAusente, Comentario, TipoPostalServico, TipoPostalSiglaCodigo, TipoPostalNomeSiglaCodigo, TipoPostalPrazoDiasCorridosRegulamentado FROM TabelaObjetosSROLocal WHERE(CodigoObjeto IN('" + codigoAtual + "')) ORDER BY NomeCliente");

                    #region Carrega Variáveis
                    string CodigoObjeto = dr["CodigoObjeto"].ToString();
                    string CodigoObjetoFormatado = string.Format("{0} {1}-{2} {3}",
                    dr["CodigoObjeto"].ToString().Substring(0, 2),
                    dr["CodigoObjeto"].ToString().Substring(2, 8),
                    dr["CodigoObjeto"].ToString().Substring(10, 1),
                    dr["CodigoObjeto"].ToString().Substring(11, 2));
                    string CodigoLdi = dr["CodigoLdi"].ToString();
                    CodigoLdi = string.IsNullOrEmpty(CodigoLdi) ? "000000000000" : CodigoLdi;
                    string NomeCliente = dr["NomeCliente"].ToString();
                    DateTime DataLancamento = Convert.ToDateTime(dr["DataLancamento"]);
                    string DataLancamentoFormatada = dr["DataLancamento"].ToString();
                    string DataModificacao = dr["DataModificacao"].ToString();
                    string Situacao = dr["Situacao"].ToString();
                    bool Atualizado = Convert.ToBoolean(dr["Atualizado"]);
                    bool ObjetoEntregue = Convert.ToBoolean(dr["ObjetoEntregue"]);
                    bool CaixaPostal = Convert.ToBoolean(dr["CaixaPostal"]);
                    string UnidadePostagem = dr["UnidadePostagem"].ToString();
                    string MunicipioPostagem = dr["MunicipioPostagem"].ToString();
                    string CriacaoPostagem = dr["CriacaoPostagem"].ToString();
                    string CepDestinoPostagem = dr["CepDestinoPostagem"].ToString();
                    string ARPostagem = dr["ARPostagem"].ToString();
                    string MPPostagem = dr["MPPostagem"].ToString();
                    string DataMaxPrevistaEntregaPostagem = dr["DataMaxPrevistaEntregaPostagem"].ToString();
                    string UnidadeLOEC = dr["UnidadeLOEC"].ToString();
                    string MunicipioLOEC = dr["MunicipioLOEC"].ToString();
                    string CriacaoLOEC = dr["CriacaoLOEC"].ToString();
                    string CarteiroLOEC = dr["CarteiroLOEC"].ToString();
                    string DistritoLOEC = dr["DistritoLOEC"].ToString();
                    string NumeroLOEC = dr["NumeroLOEC"].ToString();
                    string EnderecoLOEC = dr["EnderecoLOEC"].ToString();
                    string BairroLOEC = dr["BairroLOEC"].ToString();
                    string LocalidadeLOEC = dr["LocalidadeLOEC"].ToString();
                    string SituacaoDestinatarioAusente = dr["SituacaoDestinatarioAusente"].ToString();
                    string AgrupadoDestinatarioAusente = dr["AgrupadoDestinatarioAusente"].ToString();
                    string CoordenadasDestinatarioAusente = dr["CoordenadasDestinatarioAusente"].ToString();
                    string Comentario = dr["Comentario"].ToString();
                    string TipoPostalServico = dr["TipoPostalServico"].ToString();
                    string TipoPostalSiglaCodigo = dr["TipoPostalSiglaCodigo"].ToString();
                    string TipoPostalNomeSiglaCodigo = dr["TipoPostalNomeSiglaCodigo"].ToString();
                    string TipoPostalPrazoDiasCorridosRegulamentado = dr["TipoPostalPrazoDiasCorridosRegulamentado"].ToString();
                    //string DataListaAtual = DataListaAtual;
                    //string NumeroListaAtual = NumeroListaAtual;
                    //string item = string.empty; 
                    #endregion

                    string QtdTotal = CodigosSelecionadoAgrupados.Rows.ToString();

                    if (string.IsNullOrEmpty(CodigoLdi))
                    {
                        Mensagens.Erro("Não foi encontrado o 'Número da LDI'.\nSerá necessário atualizar o objeto atual [" + CodigoObjeto + "] - " + NomeCliente + "");
                        FormularioPrincipal.RetornaComponentesFormularioPrincipal().timerAtualizacaoNovosRegistros.Stop();

                        List<Parametros> pr = new List<Parametros>() {
                            new Parametros() { Nome = "@Atualizado", Tipo = TipoCampo.Boolean, Valor = false },
                            new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjeto } };
                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Atualizado = @Atualizado  WHERE (CodigoObjeto = @CodigoObjeto)", pr);

                        FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();
                        Mensagens.Erro("O processamento continuará sem o objeto [" + CodigoObjeto + "] - " + NomeCliente + ".");
                        FormularioPrincipal.RetornaComponentesFormularioPrincipal().timerAtualizacaoNovosRegistros.Start();
                        contador = contador - 1;
                        continue;
                    }

                    Html.AppendLine("");
                    Html.AppendLine("	    <!-- Inicia objeto [" + CodigoObjeto + "] -->");
                    Html.AppendLine("	    <div style=\"width: 100%; height:310px; border: 0px solid rgb(0, 105, 105)\">");
                    Html.AppendLine("	    	<div style=\"font-size: 22px; font-family: Times; font-weight: bold; float: left; height: 25px; border: 0px solid rgb(10, 43, 190)\">");
                    Html.AppendLine("	    		ECT</div>");
                    Html.AppendLine("	    	<div style=\"font-size: 22px; font-family: Times; font-weight: bold; padding-left: 80px; float: left; height: 25px; border: 0px solid rgb(179, 8, 170)\">");
                    Html.AppendLine("	    		LISTA DE DISTRIBUIÇÃO INTERNA</div>");
                    Html.AppendLine("	    	<div style=\"font-size: 18px; font-family: Arial; padding-top: 4px; padding-left: 55px; float: left; height: 20px; border: 0px solid rgb(255, 230, 3)\">");
                    Html.AppendLine("	    		" + DataLancamento.ToShortDateString() + " - " + DataLancamento.ToShortTimeString() + "</div>");
                    Html.AppendLine("	    	<div style=\"font-size: 18px; font-family: Arial; padding-top: 1px; padding-left: 10px; float: right; height: 25px; border: 0px solid rgb(248, 6, 216)\">");
                    Html.AppendLine("	    		Versão 3.4.4</div>");
                    Html.AppendLine("	    	<div style=\"font-size: 18px; font-family: Arial;padding-top: 48px; float: left; border: 0px solid rgb(252, 0, 0)\">");
                    Html.AppendLine("	    		Lista&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:LDI " + CodigoLdi + "</div>");
                    Html.AppendLine("	    	<svg style=\"border: 0px solid rgb(10, 43, 190); float: left;\" class=\"barcode\" jsbarcode-format=\"CODE39\"");
                    Html.AppendLine("	    		jsbarcode-value=\"" + CodigoLdi + "\" jsbarcode-displayValue=\"false\" jsbarcode-marginBottom=\"0\"");
                    Html.AppendLine("	    		jsbarcode-marginleft=\"30\" jsbarcode-marginRight=\"205px\" jsbarcode-margintop=\"0\" jsbarcode-width=\"1\"");
                    Html.AppendLine("	    		jsbarcode-height=\"50\"></svg>");
                    Html.AppendLine("	    	<div style=\"font-size: 56px; font-family: Arial; font-weight: bold; color: #DCDCDC; padding-top: 1px; margin-left: 0px; float: left; height: 65px; border: 0px solid rgb(248, 6, 216)\">");

                    if (string.IsNullOrWhiteSpace(NomeCliente))
                        Html.AppendLine("	    		" + " " + "</div>");
                    else
                        Html.AppendLine("	    		" + NomeCliente.Substring(0, 1) + "</div>");
                    Html.AppendLine("	    	<div style=\"font-size: 18px; font-family: Arial; width: 748px; float: left; border: 0px solid rgb(252, 0, 0)\">");
                    Html.AppendLine("	    		Unidade&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:" + CepUnidade + " - " + NomeAgenciaLocal + "</div>");
                    Html.AppendLine("	    	<div style=\"text-align: right; font-size: 18px; font-family: Arial;width: 100px; float: left; border: 0px solid rgb(252, 0, 0)\">");
                    Html.AppendLine("	    		" + string.Format("SE/{0}", SuperintendenciaEstadual) + "</div>");
                    Html.AppendLine("	    	<div style=\"width: 100%; border-bottom: 2px solid #000000; float: left; margin-top: 10px;\"></div>");
                    Html.AppendLine("	    	<div style=\"font-size: 18px; font-family: Consolas; width: auto; float: left; border: 0px solid rgb(252, 0, 0)\">");
                    Html.AppendLine("	    		Item&nbsp;");
                    Html.AppendLine("	    		Objeto&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    Html.AppendLine("	    		Comentário&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    Html.AppendLine("	    		Matrícula&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    Html.AppendLine("	    		Rubrica&nbsp;&nbsp;");
                    Html.AppendLine("	    	</div>");
                    Html.AppendLine("	    	<div style=\"font-size: 18px; font-family: Consolas; float: left; margin-top: 5px; border: 0px solid rgb(252, 0, 0); width: 30em; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;\">");
                    Html.AppendLine("	    		" + string.Format("{0:000}", contador) + "&nbsp;&nbsp;");
                    Html.AppendLine("	    		" + CodigoObjetoFormatado + " &nbsp;&nbsp;&nbsp;" + Comentario + "");
                    Html.AppendLine("	    	</div>");
                    Html.AppendLine("	    	<div style=\"margin-left:535px;width: 180px; float: left; height: 0px; border: 1px solid #000000\"></div>");
                    Html.AppendLine("	    	<div style=\"width: 15px; float: left; height: 0px; border: 1px solid #ffffff\"></div>");
                    Html.AppendLine("	    	<div style=\"width: 105px; float: left; height: 0px; border: 1px solid #000000\"></div>");
                    Html.AppendLine("	    	<div style=\"font-size: 16px; font-family: Consolas; width: 100%;height: auto; float: left; margin-top: 0px; border: 0px solid rgb(201, 204, 17)\">");
                    Html.AppendLine("	    		<div style=\"float: left; ; border: 0px solid rgb(255, 10, 243);width: 500px;\">");
                    Html.AppendLine("	    			<div style=\"font-size: 18px; font-family: Consolas; float: left; margin-top: 1px; border: 0px solid rgb(252, 0, 0); width: 28em; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;\">");
                    Html.AppendLine("	    				Destinatário&nbsp;:");
                    Html.AppendLine("	    				" + NomeCliente + "");
                    Html.AppendLine("	    			</div>");
                    Html.AppendLine("	    			<div style=\"font-size: 18px; font-family: Consolas; width: 100%; float: left; margin-top: 0px; border: 0px solid rgb(252, 0, 0)\">");
                    Html.AppendLine("	    				Doc. ID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:____________________________");
                    Html.AppendLine("	    			</div>");
                    Html.AppendLine("	    			<div style=\"font-size: 18px; font-family: Consolas; width: 100%; float: left; margin-top: 0px; border: 0px solid rgb(252, 0, 0)\">");
                    Html.AppendLine("	    				Data e Hora&nbsp;&nbsp;:____________________________");
                    Html.AppendLine("	    			</div>");
                    Html.AppendLine("	    			<div style=\"font-size: 18px; font-family: Consolas; width: 100%; float: left; margin-top: 0px; border: 0px solid rgb(252, 0, 0)\">");
                    Html.AppendLine("	    				Nome&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:_____________________________________________");
                    Html.AppendLine("	    			</div>");
                    Html.AppendLine("	    		</div>");
                    Html.AppendLine("	    		<div style=\"width: 345px;border: 0px solid rgb(0, 17, 255); height: auto; padding-top: 0px; float: left;\">");
                    Html.AppendLine("	    			<div style=\"font-size: 16px; font-family: Consolas; width: auto;float: left; margin-top: 0px; border: 0px solid rgb(252, 0, 0)\">");
                    Html.AppendLine("	    				Endereço:&nbsp;");
                    Html.AppendLine("	    			</div>");
                    Html.AppendLine("	    			<div style=\"text-align: start; font-size: 16px; font-family: Consolas; width: 255px; float: left; margin-top: 0px; border: 0px solid rgb(252, 0, 0); height: 5.6em; overflow: hidden; text-overflow: ellipsis; white-space: initial;\">");
                    Html.AppendLine("	    				" + string.Format("{0} {1} {2} {3}", EnderecoLOEC, BairroLOEC, LocalidadeLOEC.Replace("/", "-"), MunicipioLOEC.Replace("/", "-")) + "");
                    Html.AppendLine("	    			</div>");
                    Html.AppendLine("	    		</div>");
                    Html.AppendLine("	    	</div>");

                    if (ImprimirVariosPorFolha)
                    {
                        Html.AppendLine("	    	<div style=\"width: 100%; margin-top: 25px; border-bottom: 1px dashed #000000; float: left;\"></div>");
                    }

                    Html.AppendLine("	    	<div style=\"width: 100%; height:25px; border: 0px solid rgb(16, 238, 8); float: left; \"></div>");
                    Html.AppendLine("	    </div>");
                    Html.AppendLine("	    <!-- Finaliza objeto [" + CodigoObjeto + "] -->");
                    Html.AppendLine("");

                    #region Escreve Texto para o TXT PLR
                    StringBuilder temp = new StringBuilder();
                    temp.Append(CodigoObjeto); temp.Append("[TAB]");
                    temp.Append(CodigoLdi); temp.Append("[TAB]");
                    temp.Append(NomeCliente); temp.Append("[TAB]");
                    temp.Append(DataLancamentoFormatada); temp.Append("[TAB]");
                    temp.Append(DataModificacao); temp.Append("[TAB]");
                    temp.Append(Situacao); temp.Append("[TAB]");
                    temp.Append(Atualizado == true ? "1" : "0"); temp.Append("[TAB]");
                    temp.Append(ObjetoEntregue == true ? "1" : "0"); temp.Append("[TAB]");
                    temp.Append(CaixaPostal == true ? "1" : "0"); temp.Append("[TAB]");
                    temp.Append(UnidadePostagem); temp.Append("[TAB]");
                    temp.Append(MunicipioPostagem); temp.Append("[TAB]");
                    temp.Append(CriacaoPostagem); temp.Append("[TAB]");
                    temp.Append(CepDestinoPostagem); temp.Append("[TAB]");
                    temp.Append(ARPostagem.ToUpper() == "SIM" ? "1" : "0"); temp.Append("[TAB]");
                    temp.Append(MPPostagem.ToUpper() == "SIM" ? "1" : "0"); temp.Append("[TAB]");
                    temp.Append(DataMaxPrevistaEntregaPostagem); temp.Append("[TAB]");
                    temp.Append(UnidadeLOEC); temp.Append("[TAB]");
                    temp.Append(MunicipioLOEC); temp.Append("[TAB]");
                    temp.Append(CriacaoLOEC); temp.Append("[TAB]");
                    temp.Append(CarteiroLOEC); temp.Append("[TAB]");
                    temp.Append(DistritoLOEC); temp.Append("[TAB]");
                    temp.Append(NumeroLOEC); temp.Append("[TAB]");
                    temp.Append(EnderecoLOEC); temp.Append("[TAB]");
                    temp.Append(BairroLOEC); temp.Append("[TAB]");
                    temp.Append(LocalidadeLOEC); temp.Append("[TAB]");
                    temp.Append(SituacaoDestinatarioAusente); temp.Append("[TAB]");
                    temp.Append(AgrupadoDestinatarioAusente.ToUpper() == "SIM" ? "1" : "0"); temp.Append("[TAB]");
                    temp.Append(CoordenadasDestinatarioAusente); temp.Append("[TAB]");
                    temp.Append(Comentario); temp.Append("[TAB]");
                    temp.Append(TipoPostalServico); temp.Append("[TAB]");
                    temp.Append(TipoPostalSiglaCodigo); temp.Append("[TAB]");
                    temp.Append(TipoPostalNomeSiglaCodigo); temp.Append("[TAB]");
                    temp.Append(TipoPostalPrazoDiasCorridosRegulamentado); temp.Append("[TAB]");
                    temp.Append(DataListaAtual); temp.Append("[TAB]");
                    temp.Append(NumeroListaAtual); temp.Append("[TAB]");
                    temp.Append(contador.ToString()); temp.Append("[TAB]");
                    temp.Append(CodigosSelecionadoAgrupados.Rows.Count.ToString());
                    string temp2 = temp.ToString();
                    StrTXTPLR.AppendLine(temp.ToString());
                    #endregion

                    //Último registro
                    if (contador == CodigosSelecionadoAgrupados.Rows.Count)
                    {
                        if (Configuracoes.GerarQRCodePLRNaLdi && ImprimirUmPorFolha)
                        {
                            #region Cria QRCode

                            #region Escreve Texto para o QR Code
                            StringBuilder Str = new StringBuilder();
                            Str.Append(CodigoObjeto); Str.Append("[TAB]");
                            Str.Append(CodigoLdi); Str.Append("[TAB]");
                            Str.Append(NomeCliente); Str.Append("[TAB]");
                            Str.Append(DataLancamentoFormatada); Str.Append("[TAB]");
                            Str.Append(DataModificacao); Str.Append("[TAB]");
                            Str.Append(Situacao); Str.Append("[TAB]");
                            Str.Append(Atualizado == true ? "1" : "0"); Str.Append("[TAB]");
                            Str.Append(ObjetoEntregue == true ? "1" : "0"); Str.Append("[TAB]");
                            Str.Append(CaixaPostal == true ? "1" : "0"); Str.Append("[TAB]");
                            Str.Append(UnidadePostagem); Str.Append("[TAB]");
                            Str.Append(MunicipioPostagem); Str.Append("[TAB]");
                            Str.Append(CriacaoPostagem); Str.Append("[TAB]");
                            Str.Append(CepDestinoPostagem); Str.Append("[TAB]");
                            Str.Append(ARPostagem.ToUpper() == "SIM" ? "1" : "0"); Str.Append("[TAB]");
                            Str.Append(MPPostagem.ToUpper() == "SIM" ? "1" : "0"); Str.Append("[TAB]");
                            Str.Append(DataMaxPrevistaEntregaPostagem); Str.Append("[TAB]");
                            Str.Append(UnidadeLOEC); Str.Append("[TAB]");
                            Str.Append(MunicipioLOEC); Str.Append("[TAB]");
                            Str.Append(CriacaoLOEC); Str.Append("[TAB]");
                            Str.Append(CarteiroLOEC); Str.Append("[TAB]");
                            Str.Append(DistritoLOEC); Str.Append("[TAB]");
                            Str.Append(NumeroLOEC); Str.Append("[TAB]");
                            Str.Append(EnderecoLOEC); Str.Append("[TAB]");
                            Str.Append(BairroLOEC); Str.Append("[TAB]");
                            Str.Append(LocalidadeLOEC); Str.Append("[TAB]");
                            Str.Append(SituacaoDestinatarioAusente); Str.Append("[TAB]");
                            Str.Append(AgrupadoDestinatarioAusente.ToUpper() == "SIM" ? "1" : "0"); Str.Append("[TAB]");
                            Str.Append(CoordenadasDestinatarioAusente); Str.Append("[TAB]");
                            Str.Append(Comentario); Str.Append("[TAB]");
                            Str.Append(TipoPostalServico); Str.Append("[TAB]");
                            Str.Append(TipoPostalSiglaCodigo); Str.Append("[TAB]");
                            Str.Append(TipoPostalNomeSiglaCodigo); Str.Append("[TAB]");
                            Str.Append(TipoPostalPrazoDiasCorridosRegulamentado); Str.Append("[TAB]");
                            Str.Append(DataListaAtual); Str.Append("[TAB]");
                            Str.Append(NumeroListaAtual); Str.Append("[TAB]");
                            Str.Append(contador.ToString()); Str.Append("[TAB]");
                            Str.Append(CodigosSelecionadoAgrupados.Rows.Count.ToString());
                            #endregion

                            #region Monta Imagem QR Code
                            string ObjetoLinhaQRCode = Str.ToString().Replace("NÃ", "NA");
                            //string compacta = ClassesDiversas.FormataString.Compacta(Str.ToString());
                            //string descompacta = ClassesDiversas.FormataString.Descompacta(compacta); 
                            //Bitmap textoBitmap = GerarQRCode(250, 250, compacta);
                            Bitmap textoBitmap = GerarQRCode(250, 250, ObjetoLinhaQRCode);
                            byte[] TextoByte = BitmapToBytes(textoBitmap);
                            #endregion

                            #region Tabela PLR - Pré Lista de Remessa
                            Html.AppendLine("       <table class=\"TabelaPLR\" style=\"margin-top: 650px; margin-left: 100px; border: 0px solid rgb(252, 0, 0); \">");
                            Html.AppendLine("       <tbody>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td><div style=\"width: 100%; height:250px; border: 0px solid rgb(16, 238, 8);\">  <center>          <img src=" + string.Format("data:image/png;base64,{0}", Convert.ToBase64String(TextoByte)) + " />     </center>  </div></td>");
                            Html.AppendLine("       <td><table class=\"TabelaPLR\">");
                            Html.AppendLine("       <thead>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <th colspan=\"3\">PLR - Pr&eacute; Lista de Remessa</th>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       </thead>");
                            Html.AppendLine("       <tbody>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>Número da Lista</td>");
                            Html.AppendLine("       <td>" + NumeroListaAtual + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>Data da emiss&atilde;o da lista</td>");
                            Html.AppendLine("       <td>" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>Item/Qtd. total</td>");
                            Html.AppendLine("       <td>" + string.Format("{0}/{1}", contador, CodigosSelecionadoAgrupados.Rows.Count) + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>C&oacute;digo Objeto atual</td>");
                            Html.AppendLine("       <td>" + CodigoObjeto + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>N&uacute;mero LDI Lan&ccedil;amento</td>");
                            Html.AppendLine("       <td>" + CodigoLdi + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>Data Lan&ccedil;amento LDI</td>");
                            Html.AppendLine("       <td>" + DataLancamento.ToShortDateString() + " " + DataLancamento.ToShortTimeString() + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>Prazo do item</td>");
                            if (string.IsNullOrWhiteSpace(TipoPostalPrazoDiasCorridosRegulamentado))
                                Html.AppendLine("       <td>Não encontrado</td>");
                            else
                                Html.AppendLine("       <td>" + TipoPostalPrazoDiasCorridosRegulamentado + " dias corridos</td>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>Data&nbsp;limite devolu&ccedil;&atilde;o</td>");
                            Html.AppendLine("       <td>" + DataLancamento.ToDateTime().Date.AddDays(Convert.ToDouble(string.IsNullOrWhiteSpace(TipoPostalPrazoDiasCorridosRegulamentado) ? "0" : TipoPostalPrazoDiasCorridosRegulamentado)).ToDateTime().ToShortDateString() + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       </tbody>");
                            Html.AppendLine("       </table></td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       </tbody>");
                            Html.AppendLine("       </table>");
                            #endregion

                            #endregion
                        }
                        if (Configuracoes.GerarTXTPLRNaLdi)
                        {
                            #region Cria Arquivo TXT                            
                            using (Arquivos arq = new Arquivos())
                            {
                                Arquivos.CriarDiretorio(string.Format(@"C:\PLR\{0:yyyy-MM-dd}", DateTime.Now));
                                string nomeEnderecoArquivo = string.Format(@"C:\PLR\{0:yyyy-MM-dd}\{1}.txt", DateTime.Now, NumeroListaAtual);
                                string tempo = StrTXTPLR.ToString();
                                arq.GravarArquivo(nomeEnderecoArquivo, StrTXTPLR.ToString());


                                //string nomeEnderecoArquivoHtmlDaTemp = string.Format(@"{0}{1}", Path.GetTempPath(), NomeArquivoHtml);
                                ////file:///C:/PLR/2021-10-15/20211015172808.html
                                //nomeEnderecoArquivoHtmlDaTemp = "file:///" + nomeEnderecoArquivoHtmlDaTemp.Replace(@"\", "/");


                                //StringBuilder SrtRedirect = new StringBuilder();
                                //SrtRedirect.AppendLine("<html lang=\"pt-br\">");
                                //SrtRedirect.AppendLine("    <head>");
                                //SrtRedirect.AppendLine("        <meta http-equiv='refresh' content='0; URL="+ nomeEnderecoArquivoHtmlDaTemp + "'>");
                                //SrtRedirect.AppendLine("    </head>");
                                //SrtRedirect.AppendLine("<html>");

                                //string nomeEnderecoArquivoAtalho = string.Format(@"C:\PLR\{0:yyyy-MM-dd}\{1}.html", DateTime.Now, NumeroListaAtual);
                                //arq.GravarArquivo(nomeEnderecoArquivoAtalho, SrtRedirect.ToString());
                            }
                            #endregion
                        }

                        if (Configuracoes.GerarQRCodePLRNaLdi || Configuracoes.GerarTXTPLRNaLdi)
                        {
                            #region Consolidado [Última Página]
                            HtmlConsolidado = new StringBuilder();
                            #region Head HTML
                            HtmlConsolidado.AppendLine("<!DOCTYPE html>");
                            HtmlConsolidado.AppendLine("<html lang=\"pt-br\">");
                            HtmlConsolidado.AppendLine("<head>");
                            HtmlConsolidado.AppendLine("    <title>PLR - Pré Lista de Remessa número: " + NumeroListaAtual + "</title>");
                            HtmlConsolidado.AppendLine("    <meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\"/>");
                            HtmlConsolidado.AppendLine("    <style type=\"text/css\">");
                            HtmlConsolidado.AppendLine("        .quebrapagina {");
                            HtmlConsolidado.AppendLine("            page-break-before: always;");
                            HtmlConsolidado.AppendLine("            page-break-inside: avoid;");
                            HtmlConsolidado.AppendLine("        }");
                            HtmlConsolidado.AppendLine("        BODY {");
                            HtmlConsolidado.AppendLine("            margin-top: 0;");
                            HtmlConsolidado.AppendLine("            margin-left: 0;");
                            HtmlConsolidado.AppendLine("            margin-right: 0;");
                            HtmlConsolidado.AppendLine("            background-color: #FFFFFF;");
                            HtmlConsolidado.AppendLine("        }");
                            HtmlConsolidado.AppendLine("        .geral {");
                            HtmlConsolidado.AppendLine("            border: 0px solid rgb(252, 0, 0);");
                            HtmlConsolidado.AppendLine("            width: 850px;");
                            HtmlConsolidado.AppendLine("            margin: auto;");
                            HtmlConsolidado.AppendLine("            min-height: 1100px;");
                            HtmlConsolidado.AppendLine("        }");
                            HtmlConsolidado.AppendLine("        table.Consolidado {");
                            HtmlConsolidado.AppendLine("          font-family: Arial, Helvetica, sans-serif;");
                            HtmlConsolidado.AppendLine("          border: 1px solid #000000;");
                            HtmlConsolidado.AppendLine("          background-color: #EEEEEE;");
                            HtmlConsolidado.AppendLine("          width: 100%;");
                            HtmlConsolidado.AppendLine("          text-align: left;");
                            HtmlConsolidado.AppendLine("          margin-top: 10px;");
                            HtmlConsolidado.AppendLine("        }");
                            HtmlConsolidado.AppendLine("        table.Consolidado td, table.Consolidado th {");
                            HtmlConsolidado.AppendLine("          padding: 3px 2px;");
                            HtmlConsolidado.AppendLine("        }");
                            HtmlConsolidado.AppendLine("        table.Consolidado tbody td {");
                            HtmlConsolidado.AppendLine("          font-size: 14px;");
                            HtmlConsolidado.AppendLine("        }");
                            HtmlConsolidado.AppendLine("        table.Consolidado tr:nth-child(even) {");
                            HtmlConsolidado.AppendLine("          background: #C1C1C1;");
                            HtmlConsolidado.AppendLine("        }");
                            HtmlConsolidado.AppendLine("        table.Consolidado thead {");
                            HtmlConsolidado.AppendLine("          background: #C4C4C4;");
                            HtmlConsolidado.AppendLine("          border-bottom: 2px solid #444444;");
                            HtmlConsolidado.AppendLine("        }");
                            HtmlConsolidado.AppendLine("        table.Consolidado thead th {");
                            HtmlConsolidado.AppendLine("          font-size: 15px;");
                            HtmlConsolidado.AppendLine("          font-weight: bold;");
                            HtmlConsolidado.AppendLine("          color: #000000;");
                            HtmlConsolidado.AppendLine("          border-left: 2px solid #C4C4C4;");
                            HtmlConsolidado.AppendLine("        }");
                            HtmlConsolidado.AppendLine("        table.Consolidado thead th:first-child {");
                            HtmlConsolidado.AppendLine("          border-left: none;");
                            HtmlConsolidado.AppendLine("        }");
                            HtmlConsolidado.AppendLine("    </style>");
                            HtmlConsolidado.AppendLine("</head>");
                            HtmlConsolidado.AppendLine("<body>");
                            HtmlConsolidado.AppendLine("    <div class=\"geral\">");
                            #endregion

                            #region Cabeçalho Consolidado
                            HtmlConsolidado.AppendLine("       <table class=\"Consolidado\">");
                            HtmlConsolidado.AppendLine("       <thead>");
                            HtmlConsolidado.AppendLine("       <tr>");
                            HtmlConsolidado.AppendLine("       <th>PLR - Pr&eacute Lista de Remessa - Consolidado</th>");
                            HtmlConsolidado.AppendLine("       </tr>");
                            HtmlConsolidado.AppendLine("       </thead>");
                            HtmlConsolidado.AppendLine("       <tbody>");
                            HtmlConsolidado.AppendLine("       </tbody>");
                            HtmlConsolidado.AppendLine("       </table>");
                            HtmlConsolidado.AppendLine("       <table class=\"Consolidado\">");
                            HtmlConsolidado.AppendLine("       <thead>");
                            HtmlConsolidado.AppendLine("       <tr>");
                            HtmlConsolidado.AppendLine("       <th>N&uacute;mero da Lista</th>");
                            HtmlConsolidado.AppendLine("       <th>Data da emiss&atilde;o</th>");
                            HtmlConsolidado.AppendLine("       <th>Qtd. total</th>");
                            HtmlConsolidado.AppendLine("       </tr>");
                            HtmlConsolidado.AppendLine("       </thead>");
                            HtmlConsolidado.AppendLine("       <tbody>");
                            HtmlConsolidado.AppendLine("       <tr>");
                            HtmlConsolidado.AppendLine("       <td>" + NumeroListaAtual + "</td>");
                            HtmlConsolidado.AppendLine("       <td>" + DataListaAtual + "</td>");
                            HtmlConsolidado.AppendLine("       <td>" + CodigosSelecionadoAgrupados.Rows.Count.ToString() + "</td>");
                            HtmlConsolidado.AppendLine("       </tr>");
                            HtmlConsolidado.AppendLine("       </tbody>");
                            HtmlConsolidado.AppendLine("       </table>");
                            HtmlConsolidado.AppendLine("");
                            #endregion

                            #region Lista Relação Objetos                            
                            HtmlConsolidado.AppendLine("       <table class=\"Consolidado\">");
                            HtmlConsolidado.AppendLine("       <thead>");
                            HtmlConsolidado.AppendLine("       <tr>");
                            HtmlConsolidado.AppendLine("       <th>Item/Qtd.</th>"); //Item/Qtd.
                            HtmlConsolidado.AppendLine("       <th>C&oacute;digo Objeto</th>");//Codigo Objeto
                            HtmlConsolidado.AppendLine("       <th>Nome Cliente</th>"); //Nome Cliente
                            HtmlConsolidado.AppendLine("       <th>N&uacute;m. LDI</th>"); //Núm. LDI
                            HtmlConsolidado.AppendLine("       <th>Data LDI</th>"); //Data LDI
                            HtmlConsolidado.AppendLine("       <th>Comentário</th>"); //Comentario
                                                                                      //HtmlConsolidado.AppendLine("       <th>Serviço Postal</th>"); //TipoPostalNomeSiglaCodigo
                            HtmlConsolidado.AppendLine("       <th>Prazo</th>"); // Prazo
                            HtmlConsolidado.AppendLine("       <th>Data devolu&ccedil;&atilde;o</th>");//Data Devolução
                            HtmlConsolidado.AppendLine("       </tr>");
                            HtmlConsolidado.AppendLine("       </thead>");
                            HtmlConsolidado.AppendLine("       <tbody>");

                            int contadorConsolidado = 0;
                            foreach (DataRow itemConsolidado in CodigosSelecionadoAgrupados.Rows)
                            {
                                contadorConsolidado++;
                                DataRow drConsolidado = dao.RetornaDataRow("SELECT top 1 CodigoObjeto, CodigoLdi, NomeCliente, DataLancamento, Comentario, TipoPostalNomeSiglaCodigo, TipoPostalPrazoDiasCorridosRegulamentado FROM TabelaObjetosSROLocal WHERE(CodigoObjeto IN('" + itemConsolidado["CodigoObjeto"].ToString() + "')) ORDER BY NomeCliente");
                                HtmlConsolidado.AppendLine("       <tr>");
                                HtmlConsolidado.AppendLine("       <td>" + string.Format("{0}/{1}", contadorConsolidado, CodigosSelecionadoAgrupados.Rows.Count) + "</td>");//Item/Qtd.
                                HtmlConsolidado.AppendLine("       <td>" + drConsolidado["CodigoObjeto"].ToString() + "</td>");//Código Objeto
                                HtmlConsolidado.AppendLine("       <td>" + drConsolidado["NomeCliente"].ToString() + "</td>"); //Nome Cliente
                                HtmlConsolidado.AppendLine("       <td>" + drConsolidado["CodigoLdi"].ToString() + "</td>"); //Num. LDI
                                HtmlConsolidado.AppendLine("       <td>" + drConsolidado["DataLancamento"].ToString() + "</td>"); //Data LDI
                                HtmlConsolidado.AppendLine("       <td>" + drConsolidado["Comentario"].ToString() + "</td>"); //Comentario
                                                                                                                              //HtmlConsolidado.AppendLine("       <td>" + drConsolidado["TipoPostalNomeSiglaCodigo"].ToString() + "</td>"); //TipoPostalNomeSiglaCodigo

                                if (string.IsNullOrWhiteSpace(drConsolidado["TipoPostalPrazoDiasCorridosRegulamentado"].ToString()))
                                    HtmlConsolidado.AppendLine("       <td>Não encontrado</td>"); //Prazo
                                else
                                    HtmlConsolidado.AppendLine("       <td>" + drConsolidado["TipoPostalPrazoDiasCorridosRegulamentado"].ToString() + "</td>"); //Prazo
                                HtmlConsolidado.AppendLine("       <td>" + drConsolidado["DataLancamento"].ToDateTime().Date.AddDays(Convert.ToDouble(string.IsNullOrWhiteSpace(drConsolidado["TipoPostalPrazoDiasCorridosRegulamentado"].ToString()) ? "0" : drConsolidado["TipoPostalPrazoDiasCorridosRegulamentado"].ToString())).ToDateTime().ToShortDateString() + "</td>"); //Data devolução
                                HtmlConsolidado.AppendLine("       </tr>");
                            }
                            HtmlConsolidado.AppendLine("       </tbody>");
                            HtmlConsolidado.AppendLine("       </table>");
                            #endregion

                            #region Rodapé
                            HtmlConsolidado.AppendLine("    </div>");
                            HtmlConsolidado.AppendLine("</body>");
                            HtmlConsolidado.AppendLine("</html>");
                            #endregion

                            #endregion
                        }

                        continue;
                    }
                    if (ImprimirUmPorFolha)
                    {
                        if (Configuracoes.GerarQRCodePLRNaLdi)
                        {
                            #region Cria QRCode

                            #region Escreve Texto para o QR Code
                            StringBuilder Str = new StringBuilder();
                            Str.Append(CodigoObjeto); Str.Append("[TAB]");
                            Str.Append(CodigoLdi); Str.Append("[TAB]");
                            Str.Append(NomeCliente); Str.Append("[TAB]");
                            Str.Append(DataLancamentoFormatada); Str.Append("[TAB]");
                            Str.Append(DataModificacao); Str.Append("[TAB]");
                            Str.Append(Situacao); Str.Append("[TAB]");
                            Str.Append(Atualizado == true ? "1" : "0"); Str.Append("[TAB]");
                            Str.Append(ObjetoEntregue == true ? "1" : "0"); Str.Append("[TAB]");
                            Str.Append(CaixaPostal == true ? "1" : "0"); Str.Append("[TAB]");
                            Str.Append(UnidadePostagem); Str.Append("[TAB]");
                            Str.Append(MunicipioPostagem); Str.Append("[TAB]");
                            Str.Append(CriacaoPostagem); Str.Append("[TAB]");
                            Str.Append(CepDestinoPostagem); Str.Append("[TAB]");
                            Str.Append(ARPostagem.ToUpper() == "SIM" ? "1" : "0"); Str.Append("[TAB]");
                            Str.Append(MPPostagem.ToUpper() == "SIM" ? "1" : "0"); Str.Append("[TAB]");
                            Str.Append(DataMaxPrevistaEntregaPostagem); Str.Append("[TAB]");
                            Str.Append(UnidadeLOEC); Str.Append("[TAB]");
                            Str.Append(MunicipioLOEC); Str.Append("[TAB]");
                            Str.Append(CriacaoLOEC); Str.Append("[TAB]");
                            Str.Append(CarteiroLOEC); Str.Append("[TAB]");
                            Str.Append(DistritoLOEC); Str.Append("[TAB]");
                            Str.Append(NumeroLOEC); Str.Append("[TAB]");
                            Str.Append(EnderecoLOEC); Str.Append("[TAB]");
                            Str.Append(BairroLOEC); Str.Append("[TAB]");
                            Str.Append(LocalidadeLOEC); Str.Append("[TAB]");
                            Str.Append(SituacaoDestinatarioAusente); Str.Append("[TAB]");
                            Str.Append(AgrupadoDestinatarioAusente.ToUpper() == "SIM" ? "1" : "0"); Str.Append("[TAB]");
                            Str.Append(CoordenadasDestinatarioAusente); Str.Append("[TAB]");
                            Str.Append(Comentario); Str.Append("[TAB]");
                            Str.Append(TipoPostalServico); Str.Append("[TAB]");
                            Str.Append(TipoPostalSiglaCodigo); Str.Append("[TAB]");
                            Str.Append(TipoPostalNomeSiglaCodigo); Str.Append("[TAB]");
                            Str.Append(TipoPostalPrazoDiasCorridosRegulamentado); Str.Append("[TAB]");
                            Str.Append(DataListaAtual); Str.Append("[TAB]");
                            Str.Append(NumeroListaAtual); Str.Append("[TAB]");
                            Str.Append(contador.ToString()); Str.Append("[TAB]");
                            Str.Append(CodigosSelecionadoAgrupados.Rows.Count.ToString());
                            #endregion

                            #region Monta Imagem QR Code
                            string ObjetoLinhaQRCode = Str.ToString().Replace("NÃ", "NA");
                            //string compacta = ClassesDiversas.FormataString.Compacta(Str.ToString());
                            //string descompacta = ClassesDiversas.FormataString.Descompacta(compacta); 
                            //Bitmap textoBitmap = GerarQRCode(250, 250, compacta);
                            Bitmap textoBitmap = GerarQRCode(250, 250, ObjetoLinhaQRCode);
                            byte[] TextoByte = BitmapToBytes(textoBitmap);
                            #endregion

                            #region Tabela PLR - Pré Lista de Remessa
                            Html.AppendLine("       <table class=\"TabelaPLR\" style=\"margin-top: 650px; margin-left: 100px; border: 0px solid rgb(252, 0, 0); \">");
                            Html.AppendLine("       <tbody>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td><div style=\"width: 100%; height:250px; border: 0px solid rgb(16, 238, 8);\">  <center>          <img src=" + string.Format("data:image/png;base64,{0}", Convert.ToBase64String(TextoByte)) + " />     </center>  </div></td>");
                            Html.AppendLine("       <td><table class=\"TabelaPLR\">");
                            Html.AppendLine("       <thead>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <th colspan=\"3\">PLR - Pr&eacute; Lista de Remessa</th>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       </thead>");
                            Html.AppendLine("       <tbody>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>Número da Lista</td>");
                            Html.AppendLine("       <td>" + NumeroListaAtual + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>Data da emiss&atilde;o da lista</td>");
                            Html.AppendLine("       <td>" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>Item/Qtd. total</td>");
                            Html.AppendLine("       <td>" + string.Format("{0}/{1}", contador, CodigosSelecionadoAgrupados.Rows.Count) + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>C&oacute;digo Objeto atual</td>");
                            Html.AppendLine("       <td>" + CodigoObjeto + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>N&uacute;mero LDI Lan&ccedil;amento</td>");
                            Html.AppendLine("       <td>" + CodigoLdi + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>Data Lan&ccedil;amento LDI</td>");
                            Html.AppendLine("       <td>" + DataLancamento.ToShortDateString() + " " + DataLancamento.ToShortTimeString() + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>Prazo do item</td>");
                            if (string.IsNullOrWhiteSpace(TipoPostalPrazoDiasCorridosRegulamentado))
                                Html.AppendLine("       <td>Não encontrado</td>");
                            else
                                Html.AppendLine("       <td>" + TipoPostalPrazoDiasCorridosRegulamentado + " dias corridos</td>");
                            Html.AppendLine("       <tr>");
                            Html.AppendLine("       <td>Data&nbsp;limite devolu&ccedil;&atilde;o</td>");
                            Html.AppendLine("       <td>" + DataLancamento.ToDateTime().Date.AddDays(Convert.ToDouble(string.IsNullOrWhiteSpace(TipoPostalPrazoDiasCorridosRegulamentado) ? "0" : TipoPostalPrazoDiasCorridosRegulamentado)).ToDateTime().ToShortDateString() + "</td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       </tbody>");
                            Html.AppendLine("       </table></td>");
                            Html.AppendLine("       </tr>");
                            Html.AppendLine("       </tbody>");
                            Html.AppendLine("       </table>");
                            #endregion

                            #endregion
                        }

                        Html.AppendLine("		<div class=\"quebrapagina\"></div>");
                    }
                    if (ImprimirVariosPorFolha)
                    {
                        //Variável que que será testada --> ATÉ 40.000 OBJETOS GERAM 10.000 PÁGINAS - É O SUFICIENTE
                        string[] valorVerificar = { "4", "8", "12", "16", "20", "24", "28", "32", "36", "40", "44", "48", "52", "56", "60", "64", "68", "72", "76", "80", "84", "88", "92", "96", "100", "104", "108", "112", "116", "120", "124", "128", "132", "136", "140", "144", "148", "152", "156", "160", "164", "168", "172", "176", "180", "184", "188", "192", "196", "200", "204", "208", "212", "216", "220", "224", "228", "232", "236", "240", "244", "248", "252", "256", "260", "264", "268", "272", "276", "280", "284", "288", "292", "296", "300", "304", "308", "312", "316", "320", "324", "328", "332", "336", "340", "344", "348", "352", "356", "360", "364", "368", "372", "376", "380", "384", "388", "392", "396", "400", "404", "408", "412", "416", "420", "424", "428", "432", "436", "440", "444", "448", "452", "456", "460", "464", "468", "472", "476", "480", "484", "488", "492", "496", "500", "504", "508", "512", "516", "520", "524", "528", "532", "536", "540", "544", "548", "552", "556", "560", "564", "568", "572", "576", "580", "584", "588", "592", "596", "600", "604", "608", "612", "616", "620", "624", "628", "632", "636", "640", "644", "648", "652", "656", "660", "664", "668", "672", "676", "680", "684", "688", "692", "696", "700", "704", "708", "712", "716", "720", "724", "728", "732", "736", "740", "744", "748", "752", "756", "760", "764", "768", "772", "776", "780", "784", "788", "792", "796", "800", "804", "808", "812", "816", "820", "824", "828", "832", "836", "840", "844", "848", "852", "856", "860", "864", "868", "872", "876", "880", "884", "888", "892", "896", "900", "904", "908", "912", "916", "920", "924", "928", "932", "936", "940", "944", "948", "952", "956", "960", "964", "968", "972", "976", "980", "984", "988", "992", "996", "1000", "1004", "1008", "1012", "1016", "1020", "1024", "1028", "1032", "1036", "1040", "1044", "1048", "1052", "1056", "1060", "1064", "1068", "1072", "1076", "1080", "1084", "1088", "1092", "1096", "1100", "1104", "1108", "1112", "1116", "1120", "1124", "1128", "1132", "1136", "1140", "1144", "1148", "1152", "1156", "1160", "1164", "1168", "1172", "1176", "1180", "1184", "1188", "1192", "1196", "1200", "1204", "1208", "1212", "1216", "1220", "1224", "1228", "1232", "1236", "1240", "1244", "1248", "1252", "1256", "1260", "1264", "1268", "1272", "1276", "1280", "1284", "1288", "1292", "1296", "1300", "1304", "1308", "1312", "1316", "1320", "1324", "1328", "1332", "1336", "1340", "1344", "1348", "1352", "1356", "1360", "1364", "1368", "1372", "1376", "1380", "1384", "1388", "1392", "1396", "1400", "1404", "1408", "1412", "1416", "1420", "1424", "1428", "1432", "1436", "1440", "1444", "1448", "1452", "1456", "1460", "1464", "1468", "1472", "1476", "1480", "1484", "1488", "1492", "1496", "1500", "1504", "1508", "1512", "1516", "1520", "1524", "1528", "1532", "1536", "1540", "1544", "1548", "1552", "1556", "1560", "1564", "1568", "1572", "1576", "1580", "1584", "1588", "1592", "1596", "1600", "1604", "1608", "1612", "1616", "1620", "1624", "1628", "1632", "1636", "1640", "1644", "1648", "1652", "1656", "1660", "1664", "1668", "1672", "1676", "1680", "1684", "1688", "1692", "1696", "1700", "1704", "1708", "1712", "1716", "1720", "1724", "1728", "1732", "1736", "1740", "1744", "1748", "1752", "1756", "1760", "1764", "1768", "1772", "1776", "1780", "1784", "1788", "1792", "1796", "1800", "1804", "1808", "1812", "1816", "1820", "1824", "1828", "1832", "1836", "1840", "1844", "1848", "1852", "1856", "1860", "1864", "1868", "1872", "1876", "1880", "1884", "1888", "1892", "1896", "1900", "1904", "1908", "1912", "1916", "1920", "1924", "1928", "1932", "1936", "1940", "1944", "1948", "1952", "1956", "1960", "1964", "1968", "1972", "1976", "1980", "1984", "1988", "1992", "1996", "2000", "2004", "2008", "2012", "2016", "2020", "2024", "2028", "2032", "2036", "2040", "2044", "2048", "2052", "2056", "2060", "2064", "2068", "2072", "2076", "2080", "2084", "2088", "2092", "2096", "2100", "2104", "2108", "2112", "2116", "2120", "2124", "2128", "2132", "2136", "2140", "2144", "2148", "2152", "2156", "2160", "2164", "2168", "2172", "2176", "2180", "2184", "2188", "2192", "2196", "2200", "2204", "2208", "2212", "2216", "2220", "2224", "2228", "2232", "2236", "2240", "2244", "2248", "2252", "2256", "2260", "2264", "2268", "2272", "2276", "2280", "2284", "2288", "2292", "2296", "2300", "2304", "2308", "2312", "2316", "2320", "2324", "2328", "2332", "2336", "2340", "2344", "2348", "2352", "2356", "2360", "2364", "2368", "2372", "2376", "2380", "2384", "2388", "2392", "2396", "2400", "2404", "2408", "2412", "2416", "2420", "2424", "2428", "2432", "2436", "2440", "2444", "2448", "2452", "2456", "2460", "2464", "2468", "2472", "2476", "2480", "2484", "2488", "2492", "2496", "2500", "2504", "2508", "2512", "2516", "2520", "2524", "2528", "2532", "2536", "2540", "2544", "2548", "2552", "2556", "2560", "2564", "2568", "2572", "2576", "2580", "2584", "2588", "2592", "2596", "2600", "2604", "2608", "2612", "2616", "2620", "2624", "2628", "2632", "2636", "2640", "2644", "2648", "2652", "2656", "2660", "2664", "2668", "2672", "2676", "2680", "2684", "2688", "2692", "2696", "2700", "2704", "2708", "2712", "2716", "2720", "2724", "2728", "2732", "2736", "2740", "2744", "2748", "2752", "2756", "2760", "2764", "2768", "2772", "2776", "2780", "2784", "2788", "2792", "2796", "2800", "2804", "2808", "2812", "2816", "2820", "2824", "2828", "2832", "2836", "2840", "2844", "2848", "2852", "2856", "2860", "2864", "2868", "2872", "2876", "2880", "2884", "2888", "2892", "2896", "2900", "2904", "2908", "2912", "2916", "2920", "2924", "2928", "2932", "2936", "2940", "2944", "2948", "2952", "2956", "2960", "2964", "2968", "2972", "2976", "2980", "2984", "2988", "2992", "2996", "3000", "3004", "3008", "3012", "3016", "3020", "3024", "3028", "3032", "3036", "3040", "3044", "3048", "3052", "3056", "3060", "3064", "3068", "3072", "3076", "3080", "3084", "3088", "3092", "3096", "3100", "3104", "3108", "3112", "3116", "3120", "3124", "3128", "3132", "3136", "3140", "3144", "3148", "3152", "3156", "3160", "3164", "3168", "3172", "3176", "3180", "3184", "3188", "3192", "3196", "3200", "3204", "3208", "3212", "3216", "3220", "3224", "3228", "3232", "3236", "3240", "3244", "3248", "3252", "3256", "3260", "3264", "3268", "3272", "3276", "3280", "3284", "3288", "3292", "3296", "3300", "3304", "3308", "3312", "3316", "3320", "3324", "3328", "3332", "3336", "3340", "3344", "3348", "3352", "3356", "3360", "3364", "3368", "3372", "3376", "3380", "3384", "3388", "3392", "3396", "3400", "3404", "3408", "3412", "3416", "3420", "3424", "3428", "3432", "3436", "3440", "3444", "3448", "3452", "3456", "3460", "3464", "3468", "3472", "3476", "3480", "3484", "3488", "3492", "3496", "3500", "3504", "3508", "3512", "3516", "3520", "3524", "3528", "3532", "3536", "3540", "3544", "3548", "3552", "3556", "3560", "3564", "3568", "3572", "3576", "3580", "3584", "3588", "3592", "3596", "3600", "3604", "3608", "3612", "3616", "3620", "3624", "3628", "3632", "3636", "3640", "3644", "3648", "3652", "3656", "3660", "3664", "3668", "3672", "3676", "3680", "3684", "3688", "3692", "3696", "3700", "3704", "3708", "3712", "3716", "3720", "3724", "3728", "3732", "3736", "3740", "3744", "3748", "3752", "3756", "3760", "3764", "3768", "3772", "3776", "3780", "3784", "3788", "3792", "3796", "3800", "3804", "3808", "3812", "3816", "3820", "3824", "3828", "3832", "3836", "3840", "3844", "3848", "3852", "3856", "3860", "3864", "3868", "3872", "3876", "3880", "3884", "3888", "3892", "3896", "3900", "3904", "3908", "3912", "3916", "3920", "3924", "3928", "3932", "3936", "3940", "3944", "3948", "3952", "3956", "3960", "3964", "3968", "3972", "3976", "3980", "3984", "3988", "3992", "3996", "4000", "4004", "4008", "4012", "4016", "4020", "4024", "4028", "4032", "4036", "4040", "4044", "4048", "4052", "4056", "4060", "4064", "4068", "4072", "4076", "4080", "4084", "4088", "4092", "4096", "4100", "4104", "4108", "4112", "4116", "4120", "4124", "4128", "4132", "4136", "4140", "4144", "4148", "4152", "4156", "4160", "4164", "4168", "4172", "4176", "4180", "4184", "4188", "4192", "4196", "4200", "4204", "4208", "4212", "4216", "4220", "4224", "4228", "4232", "4236", "4240", "4244", "4248", "4252", "4256", "4260", "4264", "4268", "4272", "4276", "4280", "4284", "4288", "4292", "4296", "4300", "4304", "4308", "4312", "4316", "4320", "4324", "4328", "4332", "4336", "4340", "4344", "4348", "4352", "4356", "4360", "4364", "4368", "4372", "4376", "4380", "4384", "4388", "4392", "4396", "4400", "4404", "4408", "4412", "4416", "4420", "4424", "4428", "4432", "4436", "4440", "4444", "4448", "4452", "4456", "4460", "4464", "4468", "4472", "4476", "4480", "4484", "4488", "4492", "4496", "4500", "4504", "4508", "4512", "4516", "4520", "4524", "4528", "4532", "4536", "4540", "4544", "4548", "4552", "4556", "4560", "4564", "4568", "4572", "4576", "4580", "4584", "4588", "4592", "4596", "4600", "4604", "4608", "4612", "4616", "4620", "4624", "4628", "4632", "4636", "4640", "4644", "4648", "4652", "4656", "4660", "4664", "4668", "4672", "4676", "4680", "4684", "4688", "4692", "4696", "4700", "4704", "4708", "4712", "4716", "4720", "4724", "4728", "4732", "4736", "4740", "4744", "4748", "4752", "4756", "4760", "4764", "4768", "4772", "4776", "4780", "4784", "4788", "4792", "4796", "4800", "4804", "4808", "4812", "4816", "4820", "4824", "4828", "4832", "4836", "4840", "4844", "4848", "4852", "4856", "4860", "4864", "4868", "4872", "4876", "4880", "4884", "4888", "4892", "4896", "4900", "4904", "4908", "4912", "4916", "4920", "4924", "4928", "4932", "4936", "4940", "4944", "4948", "4952", "4956", "4960", "4964", "4968", "4972", "4976", "4980", "4984", "4988", "4992", "4996", "5000", "5004", "5008", "5012", "5016", "5020", "5024", "5028", "5032", "5036", "5040", "5044", "5048", "5052", "5056", "5060", "5064", "5068", "5072", "5076", "5080", "5084", "5088", "5092", "5096", "5100", "5104", "5108", "5112", "5116", "5120", "5124", "5128", "5132", "5136", "5140", "5144", "5148", "5152", "5156", "5160", "5164", "5168", "5172", "5176", "5180", "5184", "5188", "5192", "5196", "5200", "5204", "5208", "5212", "5216", "5220", "5224", "5228", "5232", "5236", "5240", "5244", "5248", "5252", "5256", "5260", "5264", "5268", "5272", "5276", "5280", "5284", "5288", "5292", "5296", "5300", "5304", "5308", "5312", "5316", "5320", "5324", "5328", "5332", "5336", "5340", "5344", "5348", "5352", "5356", "5360", "5364", "5368", "5372", "5376", "5380", "5384", "5388", "5392", "5396", "5400", "5404", "5408", "5412", "5416", "5420", "5424", "5428", "5432", "5436", "5440", "5444", "5448", "5452", "5456", "5460", "5464", "5468", "5472", "5476", "5480", "5484", "5488", "5492", "5496", "5500", "5504", "5508", "5512", "5516", "5520", "5524", "5528", "5532", "5536", "5540", "5544", "5548", "5552", "5556", "5560", "5564", "5568", "5572", "5576", "5580", "5584", "5588", "5592", "5596", "5600", "5604", "5608", "5612", "5616", "5620", "5624", "5628", "5632", "5636", "5640", "5644", "5648", "5652", "5656", "5660", "5664", "5668", "5672", "5676", "5680", "5684", "5688", "5692", "5696", "5700", "5704", "5708", "5712", "5716", "5720", "5724", "5728", "5732", "5736", "5740", "5744", "5748", "5752", "5756", "5760", "5764", "5768", "5772", "5776", "5780", "5784", "5788", "5792", "5796", "5800", "5804", "5808", "5812", "5816", "5820", "5824", "5828", "5832", "5836", "5840", "5844", "5848", "5852", "5856", "5860", "5864", "5868", "5872", "5876", "5880", "5884", "5888", "5892", "5896", "5900", "5904", "5908", "5912", "5916", "5920", "5924", "5928", "5932", "5936", "5940", "5944", "5948", "5952", "5956", "5960", "5964", "5968", "5972", "5976", "5980", "5984", "5988", "5992", "5996", "6000", "6004", "6008", "6012", "6016", "6020", "6024", "6028", "6032", "6036", "6040", "6044", "6048", "6052", "6056", "6060", "6064", "6068", "6072", "6076", "6080", "6084", "6088", "6092", "6096", "6100", "6104", "6108", "6112", "6116", "6120", "6124", "6128", "6132", "6136", "6140", "6144", "6148", "6152", "6156", "6160", "6164", "6168", "6172", "6176", "6180", "6184", "6188", "6192", "6196", "6200", "6204", "6208", "6212", "6216", "6220", "6224", "6228", "6232", "6236", "6240", "6244", "6248", "6252", "6256", "6260", "6264", "6268", "6272", "6276", "6280", "6284", "6288", "6292", "6296", "6300", "6304", "6308", "6312", "6316", "6320", "6324", "6328", "6332", "6336", "6340", "6344", "6348", "6352", "6356", "6360", "6364", "6368", "6372", "6376", "6380", "6384", "6388", "6392", "6396", "6400", "6404", "6408", "6412", "6416", "6420", "6424", "6428", "6432", "6436", "6440", "6444", "6448", "6452", "6456", "6460", "6464", "6468", "6472", "6476", "6480", "6484", "6488", "6492", "6496", "6500", "6504", "6508", "6512", "6516", "6520", "6524", "6528", "6532", "6536", "6540", "6544", "6548", "6552", "6556", "6560", "6564", "6568", "6572", "6576", "6580", "6584", "6588", "6592", "6596", "6600", "6604", "6608", "6612", "6616", "6620", "6624", "6628", "6632", "6636", "6640", "6644", "6648", "6652", "6656", "6660", "6664", "6668", "6672", "6676", "6680", "6684", "6688", "6692", "6696", "6700", "6704", "6708", "6712", "6716", "6720", "6724", "6728", "6732", "6736", "6740", "6744", "6748", "6752", "6756", "6760", "6764", "6768", "6772", "6776", "6780", "6784", "6788", "6792", "6796", "6800", "6804", "6808", "6812", "6816", "6820", "6824", "6828", "6832", "6836", "6840", "6844", "6848", "6852", "6856", "6860", "6864", "6868", "6872", "6876", "6880", "6884", "6888", "6892", "6896", "6900", "6904", "6908", "6912", "6916", "6920", "6924", "6928", "6932", "6936", "6940", "6944", "6948", "6952", "6956", "6960", "6964", "6968", "6972", "6976", "6980", "6984", "6988", "6992", "6996", "7000", "7004", "7008", "7012", "7016", "7020", "7024", "7028", "7032", "7036", "7040", "7044", "7048", "7052", "7056", "7060", "7064", "7068", "7072", "7076", "7080", "7084", "7088", "7092", "7096", "7100", "7104", "7108", "7112", "7116", "7120", "7124", "7128", "7132", "7136", "7140", "7144", "7148", "7152", "7156", "7160", "7164", "7168", "7172", "7176", "7180", "7184", "7188", "7192", "7196", "7200", "7204", "7208", "7212", "7216", "7220", "7224", "7228", "7232", "7236", "7240", "7244", "7248", "7252", "7256", "7260", "7264", "7268", "7272", "7276", "7280", "7284", "7288", "7292", "7296", "7300", "7304", "7308", "7312", "7316", "7320", "7324", "7328", "7332", "7336", "7340", "7344", "7348", "7352", "7356", "7360", "7364", "7368", "7372", "7376", "7380", "7384", "7388", "7392", "7396", "7400", "7404", "7408", "7412", "7416", "7420", "7424", "7428", "7432", "7436", "7440", "7444", "7448", "7452", "7456", "7460", "7464", "7468", "7472", "7476", "7480", "7484", "7488", "7492", "7496", "7500", "7504", "7508", "7512", "7516", "7520", "7524", "7528", "7532", "7536", "7540", "7544", "7548", "7552", "7556", "7560", "7564", "7568", "7572", "7576", "7580", "7584", "7588", "7592", "7596", "7600", "7604", "7608", "7612", "7616", "7620", "7624", "7628", "7632", "7636", "7640", "7644", "7648", "7652", "7656", "7660", "7664", "7668", "7672", "7676", "7680", "7684", "7688", "7692", "7696", "7700", "7704", "7708", "7712", "7716", "7720", "7724", "7728", "7732", "7736", "7740", "7744", "7748", "7752", "7756", "7760", "7764", "7768", "7772", "7776", "7780", "7784", "7788", "7792", "7796", "7800", "7804", "7808", "7812", "7816", "7820", "7824", "7828", "7832", "7836", "7840", "7844", "7848", "7852", "7856", "7860", "7864", "7868", "7872", "7876", "7880", "7884", "7888", "7892", "7896", "7900", "7904", "7908", "7912", "7916", "7920", "7924", "7928", "7932", "7936", "7940", "7944", "7948", "7952", "7956", "7960", "7964", "7968", "7972", "7976", "7980", "7984", "7988", "7992", "7996", "8000", "8004", "8008", "8012", "8016", "8020", "8024", "8028", "8032", "8036", "8040", "8044", "8048", "8052", "8056", "8060", "8064", "8068", "8072", "8076", "8080", "8084", "8088", "8092", "8096", "8100", "8104", "8108", "8112", "8116", "8120", "8124", "8128", "8132", "8136", "8140", "8144", "8148", "8152", "8156", "8160", "8164", "8168", "8172", "8176", "8180", "8184", "8188", "8192", "8196", "8200", "8204", "8208", "8212", "8216", "8220", "8224", "8228", "8232", "8236", "8240", "8244", "8248", "8252", "8256", "8260", "8264", "8268", "8272", "8276", "8280", "8284", "8288", "8292", "8296", "8300", "8304", "8308", "8312", "8316", "8320", "8324", "8328", "8332", "8336", "8340", "8344", "8348", "8352", "8356", "8360", "8364", "8368", "8372", "8376", "8380", "8384", "8388", "8392", "8396", "8400", "8404", "8408", "8412", "8416", "8420", "8424", "8428", "8432", "8436", "8440", "8444", "8448", "8452", "8456", "8460", "8464", "8468", "8472", "8476", "8480", "8484", "8488", "8492", "8496", "8500", "8504", "8508", "8512", "8516", "8520", "8524", "8528", "8532", "8536", "8540", "8544", "8548", "8552", "8556", "8560", "8564", "8568", "8572", "8576", "8580", "8584", "8588", "8592", "8596", "8600", "8604", "8608", "8612", "8616", "8620", "8624", "8628", "8632", "8636", "8640", "8644", "8648", "8652", "8656", "8660", "8664", "8668", "8672", "8676", "8680", "8684", "8688", "8692", "8696", "8700", "8704", "8708", "8712", "8716", "8720", "8724", "8728", "8732", "8736", "8740", "8744", "8748", "8752", "8756", "8760", "8764", "8768", "8772", "8776", "8780", "8784", "8788", "8792", "8796", "8800", "8804", "8808", "8812", "8816", "8820", "8824", "8828", "8832", "8836", "8840", "8844", "8848", "8852", "8856", "8860", "8864", "8868", "8872", "8876", "8880", "8884", "8888", "8892", "8896", "8900", "8904", "8908", "8912", "8916", "8920", "8924", "8928", "8932", "8936", "8940", "8944", "8948", "8952", "8956", "8960", "8964", "8968", "8972", "8976", "8980", "8984", "8988", "8992", "8996", "9000", "9004", "9008", "9012", "9016", "9020", "9024", "9028", "9032", "9036", "9040", "9044", "9048", "9052", "9056", "9060", "9064", "9068", "9072", "9076", "9080", "9084", "9088", "9092", "9096", "9100", "9104", "9108", "9112", "9116", "9120", "9124", "9128", "9132", "9136", "9140", "9144", "9148", "9152", "9156", "9160", "9164", "9168", "9172", "9176", "9180", "9184", "9188", "9192", "9196", "9200", "9204", "9208", "9212", "9216", "9220", "9224", "9228", "9232", "9236", "9240", "9244", "9248", "9252", "9256", "9260", "9264", "9268", "9272", "9276", "9280", "9284", "9288", "9292", "9296", "9300", "9304", "9308", "9312", "9316", "9320", "9324", "9328", "9332", "9336", "9340", "9344", "9348", "9352", "9356", "9360", "9364", "9368", "9372", "9376", "9380", "9384", "9388", "9392", "9396", "9400", "9404", "9408", "9412", "9416", "9420", "9424", "9428", "9432", "9436", "9440", "9444", "9448", "9452", "9456", "9460", "9464", "9468", "9472", "9476", "9480", "9484", "9488", "9492", "9496", "9500", "9504", "9508", "9512", "9516", "9520", "9524", "9528", "9532", "9536", "9540", "9544", "9548", "9552", "9556", "9560", "9564", "9568", "9572", "9576", "9580", "9584", "9588", "9592", "9596", "9600", "9604", "9608", "9612", "9616", "9620", "9624", "9628", "9632", "9636", "9640", "9644", "9648", "9652", "9656", "9660", "9664", "9668", "9672", "9676", "9680", "9684", "9688", "9692", "9696", "9700", "9704", "9708", "9712", "9716", "9720", "9724", "9728", "9732", "9736", "9740", "9744", "9748", "9752", "9756", "9760", "9764", "9768", "9772", "9776", "9780", "9784", "9788", "9792", "9796", "9800", "9804", "9808", "9812", "9816", "9820", "9824", "9828", "9832", "9836", "9840", "9844", "9848", "9852", "9856", "9860", "9864", "9868", "9872", "9876", "9880", "9884", "9888", "9892", "9896", "9900", "9904", "9908", "9912", "9916", "9920", "9924", "9928", "9932", "9936", "9940", "9944", "9948", "9952", "9956", "9960", "9964", "9968", "9972", "9976", "9980", "9984", "9988", "9992", "9996", "10000", "10004", "10008", "10012", "10016", "10020", "10024", "10028", "10032", "10036", "10040", "10044", "10048", "10052", "10056", "10060", "10064", "10068", "10072", "10076", "10080", "10084", "10088", "10092", "10096", "10100", "10104", "10108", "10112", "10116", "10120", "10124", "10128", "10132", "10136", "10140", "10144", "10148", "10152", "10156", "10160", "10164", "10168", "10172", "10176", "10180", "10184", "10188", "10192", "10196", "10200", "10204", "10208", "10212", "10216", "10220", "10224", "10228", "10232", "10236", "10240", "10244", "10248", "10252", "10256", "10260", "10264", "10268", "10272", "10276", "10280", "10284", "10288", "10292", "10296", "10300", "10304", "10308", "10312", "10316", "10320", "10324", "10328", "10332", "10336", "10340", "10344", "10348", "10352", "10356", "10360", "10364", "10368", "10372", "10376", "10380", "10384", "10388", "10392", "10396", "10400", "10404", "10408", "10412", "10416", "10420", "10424", "10428", "10432", "10436", "10440", "10444", "10448", "10452", "10456", "10460", "10464", "10468", "10472", "10476", "10480", "10484", "10488", "10492", "10496", "10500", "10504", "10508", "10512", "10516", "10520", "10524", "10528", "10532", "10536", "10540", "10544", "10548", "10552", "10556", "10560", "10564", "10568", "10572", "10576", "10580", "10584", "10588", "10592", "10596", "10600", "10604", "10608", "10612", "10616", "10620", "10624", "10628", "10632", "10636", "10640", "10644", "10648", "10652", "10656", "10660", "10664", "10668", "10672", "10676", "10680", "10684", "10688", "10692", "10696", "10700", "10704", "10708", "10712", "10716", "10720", "10724", "10728", "10732", "10736", "10740", "10744", "10748", "10752", "10756", "10760", "10764", "10768", "10772", "10776", "10780", "10784", "10788", "10792", "10796", "10800", "10804", "10808", "10812", "10816", "10820", "10824", "10828", "10832", "10836", "10840", "10844", "10848", "10852", "10856", "10860", "10864", "10868", "10872", "10876", "10880", "10884", "10888", "10892", "10896", "10900", "10904", "10908", "10912", "10916", "10920", "10924", "10928", "10932", "10936", "10940", "10944", "10948", "10952", "10956", "10960", "10964", "10968", "10972", "10976", "10980", "10984", "10988", "10992", "10996", "11000", "11004", "11008", "11012", "11016", "11020", "11024", "11028", "11032", "11036", "11040", "11044", "11048", "11052", "11056", "11060", "11064", "11068", "11072", "11076", "11080", "11084", "11088", "11092", "11096", "11100", "11104", "11108", "11112", "11116", "11120", "11124", "11128", "11132", "11136", "11140", "11144", "11148", "11152", "11156", "11160", "11164", "11168", "11172", "11176", "11180", "11184", "11188", "11192", "11196", "11200", "11204", "11208", "11212", "11216", "11220", "11224", "11228", "11232", "11236", "11240", "11244", "11248", "11252", "11256", "11260", "11264", "11268", "11272", "11276", "11280", "11284", "11288", "11292", "11296", "11300", "11304", "11308", "11312", "11316", "11320", "11324", "11328", "11332", "11336", "11340", "11344", "11348", "11352", "11356", "11360", "11364", "11368", "11372", "11376", "11380", "11384", "11388", "11392", "11396", "11400", "11404", "11408", "11412", "11416", "11420", "11424", "11428", "11432", "11436", "11440", "11444", "11448", "11452", "11456", "11460", "11464", "11468", "11472", "11476", "11480", "11484", "11488", "11492", "11496", "11500", "11504", "11508", "11512", "11516", "11520", "11524", "11528", "11532", "11536", "11540", "11544", "11548", "11552", "11556", "11560", "11564", "11568", "11572", "11576", "11580", "11584", "11588", "11592", "11596", "11600", "11604", "11608", "11612", "11616", "11620", "11624", "11628", "11632", "11636", "11640", "11644", "11648", "11652", "11656", "11660", "11664", "11668", "11672", "11676", "11680", "11684", "11688", "11692", "11696", "11700", "11704", "11708", "11712", "11716", "11720", "11724", "11728", "11732", "11736", "11740", "11744", "11748", "11752", "11756", "11760", "11764", "11768", "11772", "11776", "11780", "11784", "11788", "11792", "11796", "11800", "11804", "11808", "11812", "11816", "11820", "11824", "11828", "11832", "11836", "11840", "11844", "11848", "11852", "11856", "11860", "11864", "11868", "11872", "11876", "11880", "11884", "11888", "11892", "11896", "11900", "11904", "11908", "11912", "11916", "11920", "11924", "11928", "11932", "11936", "11940", "11944", "11948", "11952", "11956", "11960", "11964", "11968", "11972", "11976", "11980", "11984", "11988", "11992", "11996", "12000", "12004", "12008", "12012", "12016", "12020", "12024", "12028", "12032", "12036", "12040", "12044", "12048", "12052", "12056", "12060", "12064", "12068", "12072", "12076", "12080", "12084", "12088", "12092", "12096", "12100", "12104", "12108", "12112", "12116", "12120", "12124", "12128", "12132", "12136", "12140", "12144", "12148", "12152", "12156", "12160", "12164", "12168", "12172", "12176", "12180", "12184", "12188", "12192", "12196", "12200", "12204", "12208", "12212", "12216", "12220", "12224", "12228", "12232", "12236", "12240", "12244", "12248", "12252", "12256", "12260", "12264", "12268", "12272", "12276", "12280", "12284", "12288", "12292", "12296", "12300", "12304", "12308", "12312", "12316", "12320", "12324", "12328", "12332", "12336", "12340", "12344", "12348", "12352", "12356", "12360", "12364", "12368", "12372", "12376", "12380", "12384", "12388", "12392", "12396", "12400", "12404", "12408", "12412", "12416", "12420", "12424", "12428", "12432", "12436", "12440", "12444", "12448", "12452", "12456", "12460", "12464", "12468", "12472", "12476", "12480", "12484", "12488", "12492", "12496", "12500", "12504", "12508", "12512", "12516", "12520", "12524", "12528", "12532", "12536", "12540", "12544", "12548", "12552", "12556", "12560", "12564", "12568", "12572", "12576", "12580", "12584", "12588", "12592", "12596", "12600", "12604", "12608", "12612", "12616", "12620", "12624", "12628", "12632", "12636", "12640", "12644", "12648", "12652", "12656", "12660", "12664", "12668", "12672", "12676", "12680", "12684", "12688", "12692", "12696", "12700", "12704", "12708", "12712", "12716", "12720", "12724", "12728", "12732", "12736", "12740", "12744", "12748", "12752", "12756", "12760", "12764", "12768", "12772", "12776", "12780", "12784", "12788", "12792", "12796", "12800", "12804", "12808", "12812", "12816", "12820", "12824", "12828", "12832", "12836", "12840", "12844", "12848", "12852", "12856", "12860", "12864", "12868", "12872", "12876", "12880", "12884", "12888", "12892", "12896", "12900", "12904", "12908", "12912", "12916", "12920", "12924", "12928", "12932", "12936", "12940", "12944", "12948", "12952", "12956", "12960", "12964", "12968", "12972", "12976", "12980", "12984", "12988", "12992", "12996", "13000", "13004", "13008", "13012", "13016", "13020", "13024", "13028", "13032", "13036", "13040", "13044", "13048", "13052", "13056", "13060", "13064", "13068", "13072", "13076", "13080", "13084", "13088", "13092", "13096", "13100", "13104", "13108", "13112", "13116", "13120", "13124", "13128", "13132", "13136", "13140", "13144", "13148", "13152", "13156", "13160", "13164", "13168", "13172", "13176", "13180", "13184", "13188", "13192", "13196", "13200", "13204", "13208", "13212", "13216", "13220", "13224", "13228", "13232", "13236", "13240", "13244", "13248", "13252", "13256", "13260", "13264", "13268", "13272", "13276", "13280", "13284", "13288", "13292", "13296", "13300", "13304", "13308", "13312", "13316", "13320", "13324", "13328", "13332", "13336", "13340", "13344", "13348", "13352", "13356", "13360", "13364", "13368", "13372", "13376", "13380", "13384", "13388", "13392", "13396", "13400", "13404", "13408", "13412", "13416", "13420", "13424", "13428", "13432", "13436", "13440", "13444", "13448", "13452", "13456", "13460", "13464", "13468", "13472", "13476", "13480", "13484", "13488", "13492", "13496", "13500", "13504", "13508", "13512", "13516", "13520", "13524", "13528", "13532", "13536", "13540", "13544", "13548", "13552", "13556", "13560", "13564", "13568", "13572", "13576", "13580", "13584", "13588", "13592", "13596", "13600", "13604", "13608", "13612", "13616", "13620", "13624", "13628", "13632", "13636", "13640", "13644", "13648", "13652", "13656", "13660", "13664", "13668", "13672", "13676", "13680", "13684", "13688", "13692", "13696", "13700", "13704", "13708", "13712", "13716", "13720", "13724", "13728", "13732", "13736", "13740", "13744", "13748", "13752", "13756", "13760", "13764", "13768", "13772", "13776", "13780", "13784", "13788", "13792", "13796", "13800", "13804", "13808", "13812", "13816", "13820", "13824", "13828", "13832", "13836", "13840", "13844", "13848", "13852", "13856", "13860", "13864", "13868", "13872", "13876", "13880", "13884", "13888", "13892", "13896", "13900", "13904", "13908", "13912", "13916", "13920", "13924", "13928", "13932", "13936", "13940", "13944", "13948", "13952", "13956", "13960", "13964", "13968", "13972", "13976", "13980", "13984", "13988", "13992", "13996", "14000", "14004", "14008", "14012", "14016", "14020", "14024", "14028", "14032", "14036", "14040", "14044", "14048", "14052", "14056", "14060", "14064", "14068", "14072", "14076", "14080", "14084", "14088", "14092", "14096", "14100", "14104", "14108", "14112", "14116", "14120", "14124", "14128", "14132", "14136", "14140", "14144", "14148", "14152", "14156", "14160", "14164", "14168", "14172", "14176", "14180", "14184", "14188", "14192", "14196", "14200", "14204", "14208", "14212", "14216", "14220", "14224", "14228", "14232", "14236", "14240", "14244", "14248", "14252", "14256", "14260", "14264", "14268", "14272", "14276", "14280", "14284", "14288", "14292", "14296", "14300", "14304", "14308", "14312", "14316", "14320", "14324", "14328", "14332", "14336", "14340", "14344", "14348", "14352", "14356", "14360", "14364", "14368", "14372", "14376", "14380", "14384", "14388", "14392", "14396", "14400", "14404", "14408", "14412", "14416", "14420", "14424", "14428", "14432", "14436", "14440", "14444", "14448", "14452", "14456", "14460", "14464", "14468", "14472", "14476", "14480", "14484", "14488", "14492", "14496", "14500", "14504", "14508", "14512", "14516", "14520", "14524", "14528", "14532", "14536", "14540", "14544", "14548", "14552", "14556", "14560", "14564", "14568", "14572", "14576", "14580", "14584", "14588", "14592", "14596", "14600", "14604", "14608", "14612", "14616", "14620", "14624", "14628", "14632", "14636", "14640", "14644", "14648", "14652", "14656", "14660", "14664", "14668", "14672", "14676", "14680", "14684", "14688", "14692", "14696", "14700", "14704", "14708", "14712", "14716", "14720", "14724", "14728", "14732", "14736", "14740", "14744", "14748", "14752", "14756", "14760", "14764", "14768", "14772", "14776", "14780", "14784", "14788", "14792", "14796", "14800", "14804", "14808", "14812", "14816", "14820", "14824", "14828", "14832", "14836", "14840", "14844", "14848", "14852", "14856", "14860", "14864", "14868", "14872", "14876", "14880", "14884", "14888", "14892", "14896", "14900", "14904", "14908", "14912", "14916", "14920", "14924", "14928", "14932", "14936", "14940", "14944", "14948", "14952", "14956", "14960", "14964", "14968", "14972", "14976", "14980", "14984", "14988", "14992", "14996", "15000", "15004", "15008", "15012", "15016", "15020", "15024", "15028", "15032", "15036", "15040", "15044", "15048", "15052", "15056", "15060", "15064", "15068", "15072", "15076", "15080", "15084", "15088", "15092", "15096", "15100", "15104", "15108", "15112", "15116", "15120", "15124", "15128", "15132", "15136", "15140", "15144", "15148", "15152", "15156", "15160", "15164", "15168", "15172", "15176", "15180", "15184", "15188", "15192", "15196", "15200", "15204", "15208", "15212", "15216", "15220", "15224", "15228", "15232", "15236", "15240", "15244", "15248", "15252", "15256", "15260", "15264", "15268", "15272", "15276", "15280", "15284", "15288", "15292", "15296", "15300", "15304", "15308", "15312", "15316", "15320", "15324", "15328", "15332", "15336", "15340", "15344", "15348", "15352", "15356", "15360", "15364", "15368", "15372", "15376", "15380", "15384", "15388", "15392", "15396", "15400", "15404", "15408", "15412", "15416", "15420", "15424", "15428", "15432", "15436", "15440", "15444", "15448", "15452", "15456", "15460", "15464", "15468", "15472", "15476", "15480", "15484", "15488", "15492", "15496", "15500", "15504", "15508", "15512", "15516", "15520", "15524", "15528", "15532", "15536", "15540", "15544", "15548", "15552", "15556", "15560", "15564", "15568", "15572", "15576", "15580", "15584", "15588", "15592", "15596", "15600", "15604", "15608", "15612", "15616", "15620", "15624", "15628", "15632", "15636", "15640", "15644", "15648", "15652", "15656", "15660", "15664", "15668", "15672", "15676", "15680", "15684", "15688", "15692", "15696", "15700", "15704", "15708", "15712", "15716", "15720", "15724", "15728", "15732", "15736", "15740", "15744", "15748", "15752", "15756", "15760", "15764", "15768", "15772", "15776", "15780", "15784", "15788", "15792", "15796", "15800", "15804", "15808", "15812", "15816", "15820", "15824", "15828", "15832", "15836", "15840", "15844", "15848", "15852", "15856", "15860", "15864", "15868", "15872", "15876", "15880", "15884", "15888", "15892", "15896", "15900", "15904", "15908", "15912", "15916", "15920", "15924", "15928", "15932", "15936", "15940", "15944", "15948", "15952", "15956", "15960", "15964", "15968", "15972", "15976", "15980", "15984", "15988", "15992", "15996", "16000", "16004", "16008", "16012", "16016", "16020", "16024", "16028", "16032", "16036", "16040", "16044", "16048", "16052", "16056", "16060", "16064", "16068", "16072", "16076", "16080", "16084", "16088", "16092", "16096", "16100", "16104", "16108", "16112", "16116", "16120", "16124", "16128", "16132", "16136", "16140", "16144", "16148", "16152", "16156", "16160", "16164", "16168", "16172", "16176", "16180", "16184", "16188", "16192", "16196", "16200", "16204", "16208", "16212", "16216", "16220", "16224", "16228", "16232", "16236", "16240", "16244", "16248", "16252", "16256", "16260", "16264", "16268", "16272", "16276", "16280", "16284", "16288", "16292", "16296", "16300", "16304", "16308", "16312", "16316", "16320", "16324", "16328", "16332", "16336", "16340", "16344", "16348", "16352", "16356", "16360", "16364", "16368", "16372", "16376", "16380", "16384", "16388", "16392", "16396", "16400", "16404", "16408", "16412", "16416", "16420", "16424", "16428", "16432", "16436", "16440", "16444", "16448", "16452", "16456", "16460", "16464", "16468", "16472", "16476", "16480", "16484", "16488", "16492", "16496", "16500", "16504", "16508", "16512", "16516", "16520", "16524", "16528", "16532", "16536", "16540", "16544", "16548", "16552", "16556", "16560", "16564", "16568", "16572", "16576", "16580", "16584", "16588", "16592", "16596", "16600", "16604", "16608", "16612", "16616", "16620", "16624", "16628", "16632", "16636", "16640", "16644", "16648", "16652", "16656", "16660", "16664", "16668", "16672", "16676", "16680", "16684", "16688", "16692", "16696", "16700", "16704", "16708", "16712", "16716", "16720", "16724", "16728", "16732", "16736", "16740", "16744", "16748", "16752", "16756", "16760", "16764", "16768", "16772", "16776", "16780", "16784", "16788", "16792", "16796", "16800", "16804", "16808", "16812", "16816", "16820", "16824", "16828", "16832", "16836", "16840", "16844", "16848", "16852", "16856", "16860", "16864", "16868", "16872", "16876", "16880", "16884", "16888", "16892", "16896", "16900", "16904", "16908", "16912", "16916", "16920", "16924", "16928", "16932", "16936", "16940", "16944", "16948", "16952", "16956", "16960", "16964", "16968", "16972", "16976", "16980", "16984", "16988", "16992", "16996", "17000", "17004", "17008", "17012", "17016", "17020", "17024", "17028", "17032", "17036", "17040", "17044", "17048", "17052", "17056", "17060", "17064", "17068", "17072", "17076", "17080", "17084", "17088", "17092", "17096", "17100", "17104", "17108", "17112", "17116", "17120", "17124", "17128", "17132", "17136", "17140", "17144", "17148", "17152", "17156", "17160", "17164", "17168", "17172", "17176", "17180", "17184", "17188", "17192", "17196", "17200", "17204", "17208", "17212", "17216", "17220", "17224", "17228", "17232", "17236", "17240", "17244", "17248", "17252", "17256", "17260", "17264", "17268", "17272", "17276", "17280", "17284", "17288", "17292", "17296", "17300", "17304", "17308", "17312", "17316", "17320", "17324", "17328", "17332", "17336", "17340", "17344", "17348", "17352", "17356", "17360", "17364", "17368", "17372", "17376", "17380", "17384", "17388", "17392", "17396", "17400", "17404", "17408", "17412", "17416", "17420", "17424", "17428", "17432", "17436", "17440", "17444", "17448", "17452", "17456", "17460", "17464", "17468", "17472", "17476", "17480", "17484", "17488", "17492", "17496", "17500", "17504", "17508", "17512", "17516", "17520", "17524", "17528", "17532", "17536", "17540", "17544", "17548", "17552", "17556", "17560", "17564", "17568", "17572", "17576", "17580", "17584", "17588", "17592", "17596", "17600", "17604", "17608", "17612", "17616", "17620", "17624", "17628", "17632", "17636", "17640", "17644", "17648", "17652", "17656", "17660", "17664", "17668", "17672", "17676", "17680", "17684", "17688", "17692", "17696", "17700", "17704", "17708", "17712", "17716", "17720", "17724", "17728", "17732", "17736", "17740", "17744", "17748", "17752", "17756", "17760", "17764", "17768", "17772", "17776", "17780", "17784", "17788", "17792", "17796", "17800", "17804", "17808", "17812", "17816", "17820", "17824", "17828", "17832", "17836", "17840", "17844", "17848", "17852", "17856", "17860", "17864", "17868", "17872", "17876", "17880", "17884", "17888", "17892", "17896", "17900", "17904", "17908", "17912", "17916", "17920", "17924", "17928", "17932", "17936", "17940", "17944", "17948", "17952", "17956", "17960", "17964", "17968", "17972", "17976", "17980", "17984", "17988", "17992", "17996", "18000", "18004", "18008", "18012", "18016", "18020", "18024", "18028", "18032", "18036", "18040", "18044", "18048", "18052", "18056", "18060", "18064", "18068", "18072", "18076", "18080", "18084", "18088", "18092", "18096", "18100", "18104", "18108", "18112", "18116", "18120", "18124", "18128", "18132", "18136", "18140", "18144", "18148", "18152", "18156", "18160", "18164", "18168", "18172", "18176", "18180", "18184", "18188", "18192", "18196", "18200", "18204", "18208", "18212", "18216", "18220", "18224", "18228", "18232", "18236", "18240", "18244", "18248", "18252", "18256", "18260", "18264", "18268", "18272", "18276", "18280", "18284", "18288", "18292", "18296", "18300", "18304", "18308", "18312", "18316", "18320", "18324", "18328", "18332", "18336", "18340", "18344", "18348", "18352", "18356", "18360", "18364", "18368", "18372", "18376", "18380", "18384", "18388", "18392", "18396", "18400", "18404", "18408", "18412", "18416", "18420", "18424", "18428", "18432", "18436", "18440", "18444", "18448", "18452", "18456", "18460", "18464", "18468", "18472", "18476", "18480", "18484", "18488", "18492", "18496", "18500", "18504", "18508", "18512", "18516", "18520", "18524", "18528", "18532", "18536", "18540", "18544", "18548", "18552", "18556", "18560", "18564", "18568", "18572", "18576", "18580", "18584", "18588", "18592", "18596", "18600", "18604", "18608", "18612", "18616", "18620", "18624", "18628", "18632", "18636", "18640", "18644", "18648", "18652", "18656", "18660", "18664", "18668", "18672", "18676", "18680", "18684", "18688", "18692", "18696", "18700", "18704", "18708", "18712", "18716", "18720", "18724", "18728", "18732", "18736", "18740", "18744", "18748", "18752", "18756", "18760", "18764", "18768", "18772", "18776", "18780", "18784", "18788", "18792", "18796", "18800", "18804", "18808", "18812", "18816", "18820", "18824", "18828", "18832", "18836", "18840", "18844", "18848", "18852", "18856", "18860", "18864", "18868", "18872", "18876", "18880", "18884", "18888", "18892", "18896", "18900", "18904", "18908", "18912", "18916", "18920", "18924", "18928", "18932", "18936", "18940", "18944", "18948", "18952", "18956", "18960", "18964", "18968", "18972", "18976", "18980", "18984", "18988", "18992", "18996", "19000", "19004", "19008", "19012", "19016", "19020", "19024", "19028", "19032", "19036", "19040", "19044", "19048", "19052", "19056", "19060", "19064", "19068", "19072", "19076", "19080", "19084", "19088", "19092", "19096", "19100", "19104", "19108", "19112", "19116", "19120", "19124", "19128", "19132", "19136", "19140", "19144", "19148", "19152", "19156", "19160", "19164", "19168", "19172", "19176", "19180", "19184", "19188", "19192", "19196", "19200", "19204", "19208", "19212", "19216", "19220", "19224", "19228", "19232", "19236", "19240", "19244", "19248", "19252", "19256", "19260", "19264", "19268", "19272", "19276", "19280", "19284", "19288", "19292", "19296", "19300", "19304", "19308", "19312", "19316", "19320", "19324", "19328", "19332", "19336", "19340", "19344", "19348", "19352", "19356", "19360", "19364", "19368", "19372", "19376", "19380", "19384", "19388", "19392", "19396", "19400", "19404", "19408", "19412", "19416", "19420", "19424", "19428", "19432", "19436", "19440", "19444", "19448", "19452", "19456", "19460", "19464", "19468", "19472", "19476", "19480", "19484", "19488", "19492", "19496", "19500", "19504", "19508", "19512", "19516", "19520", "19524", "19528", "19532", "19536", "19540", "19544", "19548", "19552", "19556", "19560", "19564", "19568", "19572", "19576", "19580", "19584", "19588", "19592", "19596", "19600", "19604", "19608", "19612", "19616", "19620", "19624", "19628", "19632", "19636", "19640", "19644", "19648", "19652", "19656", "19660", "19664", "19668", "19672", "19676", "19680", "19684", "19688", "19692", "19696", "19700", "19704", "19708", "19712", "19716", "19720", "19724", "19728", "19732", "19736", "19740", "19744", "19748", "19752", "19756", "19760", "19764", "19768", "19772", "19776", "19780", "19784", "19788", "19792", "19796", "19800", "19804", "19808", "19812", "19816", "19820", "19824", "19828", "19832", "19836", "19840", "19844", "19848", "19852", "19856", "19860", "19864", "19868", "19872", "19876", "19880", "19884", "19888", "19892", "19896", "19900", "19904", "19908", "19912", "19916", "19920", "19924", "19928", "19932", "19936", "19940", "19944", "19948", "19952", "19956", "19960", "19964", "19968", "19972", "19976", "19980", "19984", "19988", "19992", "19996", "20000", "20004", "20008", "20012", "20016", "20020", "20024", "20028", "20032", "20036", "20040", "20044", "20048", "20052", "20056", "20060", "20064", "20068", "20072", "20076", "20080", "20084", "20088", "20092", "20096", "20100", "20104", "20108", "20112", "20116", "20120", "20124", "20128", "20132", "20136", "20140", "20144", "20148", "20152", "20156", "20160", "20164", "20168", "20172", "20176", "20180", "20184", "20188", "20192", "20196", "20200", "20204", "20208", "20212", "20216", "20220", "20224", "20228", "20232", "20236", "20240", "20244", "20248", "20252", "20256", "20260", "20264", "20268", "20272", "20276", "20280", "20284", "20288", "20292", "20296", "20300", "20304", "20308", "20312", "20316", "20320", "20324", "20328", "20332", "20336", "20340", "20344", "20348", "20352", "20356", "20360", "20364", "20368", "20372", "20376", "20380", "20384", "20388", "20392", "20396", "20400", "20404", "20408", "20412", "20416", "20420", "20424", "20428", "20432", "20436", "20440", "20444", "20448", "20452", "20456", "20460", "20464", "20468", "20472", "20476", "20480", "20484", "20488", "20492", "20496", "20500", "20504", "20508", "20512", "20516", "20520", "20524", "20528", "20532", "20536", "20540", "20544", "20548", "20552", "20556", "20560", "20564", "20568", "20572", "20576", "20580", "20584", "20588", "20592", "20596", "20600", "20604", "20608", "20612", "20616", "20620", "20624", "20628", "20632", "20636", "20640", "20644", "20648", "20652", "20656", "20660", "20664", "20668", "20672", "20676", "20680", "20684", "20688", "20692", "20696", "20700", "20704", "20708", "20712", "20716", "20720", "20724", "20728", "20732", "20736", "20740", "20744", "20748", "20752", "20756", "20760", "20764", "20768", "20772", "20776", "20780", "20784", "20788", "20792", "20796", "20800", "20804", "20808", "20812", "20816", "20820", "20824", "20828", "20832", "20836", "20840", "20844", "20848", "20852", "20856", "20860", "20864", "20868", "20872", "20876", "20880", "20884", "20888", "20892", "20896", "20900", "20904", "20908", "20912", "20916", "20920", "20924", "20928", "20932", "20936", "20940", "20944", "20948", "20952", "20956", "20960", "20964", "20968", "20972", "20976", "20980", "20984", "20988", "20992", "20996", "21000", "21004", "21008", "21012", "21016", "21020", "21024", "21028", "21032", "21036", "21040", "21044", "21048", "21052", "21056", "21060", "21064", "21068", "21072", "21076", "21080", "21084", "21088", "21092", "21096", "21100", "21104", "21108", "21112", "21116", "21120", "21124", "21128", "21132", "21136", "21140", "21144", "21148", "21152", "21156", "21160", "21164", "21168", "21172", "21176", "21180", "21184", "21188", "21192", "21196", "21200", "21204", "21208", "21212", "21216", "21220", "21224", "21228", "21232", "21236", "21240", "21244", "21248", "21252", "21256", "21260", "21264", "21268", "21272", "21276", "21280", "21284", "21288", "21292", "21296", "21300", "21304", "21308", "21312", "21316", "21320", "21324", "21328", "21332", "21336", "21340", "21344", "21348", "21352", "21356", "21360", "21364", "21368", "21372", "21376", "21380", "21384", "21388", "21392", "21396", "21400", "21404", "21408", "21412", "21416", "21420", "21424", "21428", "21432", "21436", "21440", "21444", "21448", "21452", "21456", "21460", "21464", "21468", "21472", "21476", "21480", "21484", "21488", "21492", "21496", "21500", "21504", "21508", "21512", "21516", "21520", "21524", "21528", "21532", "21536", "21540", "21544", "21548", "21552", "21556", "21560", "21564", "21568", "21572", "21576", "21580", "21584", "21588", "21592", "21596", "21600", "21604", "21608", "21612", "21616", "21620", "21624", "21628", "21632", "21636", "21640", "21644", "21648", "21652", "21656", "21660", "21664", "21668", "21672", "21676", "21680", "21684", "21688", "21692", "21696", "21700", "21704", "21708", "21712", "21716", "21720", "21724", "21728", "21732", "21736", "21740", "21744", "21748", "21752", "21756", "21760", "21764", "21768", "21772", "21776", "21780", "21784", "21788", "21792", "21796", "21800", "21804", "21808", "21812", "21816", "21820", "21824", "21828", "21832", "21836", "21840", "21844", "21848", "21852", "21856", "21860", "21864", "21868", "21872", "21876", "21880", "21884", "21888", "21892", "21896", "21900", "21904", "21908", "21912", "21916", "21920", "21924", "21928", "21932", "21936", "21940", "21944", "21948", "21952", "21956", "21960", "21964", "21968", "21972", "21976", "21980", "21984", "21988", "21992", "21996", "22000", "22004", "22008", "22012", "22016", "22020", "22024", "22028", "22032", "22036", "22040", "22044", "22048", "22052", "22056", "22060", "22064", "22068", "22072", "22076", "22080", "22084", "22088", "22092", "22096", "22100", "22104", "22108", "22112", "22116", "22120", "22124", "22128", "22132", "22136", "22140", "22144", "22148", "22152", "22156", "22160", "22164", "22168", "22172", "22176", "22180", "22184", "22188", "22192", "22196", "22200", "22204", "22208", "22212", "22216", "22220", "22224", "22228", "22232", "22236", "22240", "22244", "22248", "22252", "22256", "22260", "22264", "22268", "22272", "22276", "22280", "22284", "22288", "22292", "22296", "22300", "22304", "22308", "22312", "22316", "22320", "22324", "22328", "22332", "22336", "22340", "22344", "22348", "22352", "22356", "22360", "22364", "22368", "22372", "22376", "22380", "22384", "22388", "22392", "22396", "22400", "22404", "22408", "22412", "22416", "22420", "22424", "22428", "22432", "22436", "22440", "22444", "22448", "22452", "22456", "22460", "22464", "22468", "22472", "22476", "22480", "22484", "22488", "22492", "22496", "22500", "22504", "22508", "22512", "22516", "22520", "22524", "22528", "22532", "22536", "22540", "22544", "22548", "22552", "22556", "22560", "22564", "22568", "22572", "22576", "22580", "22584", "22588", "22592", "22596", "22600", "22604", "22608", "22612", "22616", "22620", "22624", "22628", "22632", "22636", "22640", "22644", "22648", "22652", "22656", "22660", "22664", "22668", "22672", "22676", "22680", "22684", "22688", "22692", "22696", "22700", "22704", "22708", "22712", "22716", "22720", "22724", "22728", "22732", "22736", "22740", "22744", "22748", "22752", "22756", "22760", "22764", "22768", "22772", "22776", "22780", "22784", "22788", "22792", "22796", "22800", "22804", "22808", "22812", "22816", "22820", "22824", "22828", "22832", "22836", "22840", "22844", "22848", "22852", "22856", "22860", "22864", "22868", "22872", "22876", "22880", "22884", "22888", "22892", "22896", "22900", "22904", "22908", "22912", "22916", "22920", "22924", "22928", "22932", "22936", "22940", "22944", "22948", "22952", "22956", "22960", "22964", "22968", "22972", "22976", "22980", "22984", "22988", "22992", "22996", "23000", "23004", "23008", "23012", "23016", "23020", "23024", "23028", "23032", "23036", "23040", "23044", "23048", "23052", "23056", "23060", "23064", "23068", "23072", "23076", "23080", "23084", "23088", "23092", "23096", "23100", "23104", "23108", "23112", "23116", "23120", "23124", "23128", "23132", "23136", "23140", "23144", "23148", "23152", "23156", "23160", "23164", "23168", "23172", "23176", "23180", "23184", "23188", "23192", "23196", "23200", "23204", "23208", "23212", "23216", "23220", "23224", "23228", "23232", "23236", "23240", "23244", "23248", "23252", "23256", "23260", "23264", "23268", "23272", "23276", "23280", "23284", "23288", "23292", "23296", "23300", "23304", "23308", "23312", "23316", "23320", "23324", "23328", "23332", "23336", "23340", "23344", "23348", "23352", "23356", "23360", "23364", "23368", "23372", "23376", "23380", "23384", "23388", "23392", "23396", "23400", "23404", "23408", "23412", "23416", "23420", "23424", "23428", "23432", "23436", "23440", "23444", "23448", "23452", "23456", "23460", "23464", "23468", "23472", "23476", "23480", "23484", "23488", "23492", "23496", "23500", "23504", "23508", "23512", "23516", "23520", "23524", "23528", "23532", "23536", "23540", "23544", "23548", "23552", "23556", "23560", "23564", "23568", "23572", "23576", "23580", "23584", "23588", "23592", "23596", "23600", "23604", "23608", "23612", "23616", "23620", "23624", "23628", "23632", "23636", "23640", "23644", "23648", "23652", "23656", "23660", "23664", "23668", "23672", "23676", "23680", "23684", "23688", "23692", "23696", "23700", "23704", "23708", "23712", "23716", "23720", "23724", "23728", "23732", "23736", "23740", "23744", "23748", "23752", "23756", "23760", "23764", "23768", "23772", "23776", "23780", "23784", "23788", "23792", "23796", "23800", "23804", "23808", "23812", "23816", "23820", "23824", "23828", "23832", "23836", "23840", "23844", "23848", "23852", "23856", "23860", "23864", "23868", "23872", "23876", "23880", "23884", "23888", "23892", "23896", "23900", "23904", "23908", "23912", "23916", "23920", "23924", "23928", "23932", "23936", "23940", "23944", "23948", "23952", "23956", "23960", "23964", "23968", "23972", "23976", "23980", "23984", "23988", "23992", "23996", "24000", "24004", "24008", "24012", "24016", "24020", "24024", "24028", "24032", "24036", "24040", "24044", "24048", "24052", "24056", "24060", "24064", "24068", "24072", "24076", "24080", "24084", "24088", "24092", "24096", "24100", "24104", "24108", "24112", "24116", "24120", "24124", "24128", "24132", "24136", "24140", "24144", "24148", "24152", "24156", "24160", "24164", "24168", "24172", "24176", "24180", "24184", "24188", "24192", "24196", "24200", "24204", "24208", "24212", "24216", "24220", "24224", "24228", "24232", "24236", "24240", "24244", "24248", "24252", "24256", "24260", "24264", "24268", "24272", "24276", "24280", "24284", "24288", "24292", "24296", "24300", "24304", "24308", "24312", "24316", "24320", "24324", "24328", "24332", "24336", "24340", "24344", "24348", "24352", "24356", "24360", "24364", "24368", "24372", "24376", "24380", "24384", "24388", "24392", "24396", "24400", "24404", "24408", "24412", "24416", "24420", "24424", "24428", "24432", "24436", "24440", "24444", "24448", "24452", "24456", "24460", "24464", "24468", "24472", "24476", "24480", "24484", "24488", "24492", "24496", "24500", "24504", "24508", "24512", "24516", "24520", "24524", "24528", "24532", "24536", "24540", "24544", "24548", "24552", "24556", "24560", "24564", "24568", "24572", "24576", "24580", "24584", "24588", "24592", "24596", "24600", "24604", "24608", "24612", "24616", "24620", "24624", "24628", "24632", "24636", "24640", "24644", "24648", "24652", "24656", "24660", "24664", "24668", "24672", "24676", "24680", "24684", "24688", "24692", "24696", "24700", "24704", "24708", "24712", "24716", "24720", "24724", "24728", "24732", "24736", "24740", "24744", "24748", "24752", "24756", "24760", "24764", "24768", "24772", "24776", "24780", "24784", "24788", "24792", "24796", "24800", "24804", "24808", "24812", "24816", "24820", "24824", "24828", "24832", "24836", "24840", "24844", "24848", "24852", "24856", "24860", "24864", "24868", "24872", "24876", "24880", "24884", "24888", "24892", "24896", "24900", "24904", "24908", "24912", "24916", "24920", "24924", "24928", "24932", "24936", "24940", "24944", "24948", "24952", "24956", "24960", "24964", "24968", "24972", "24976", "24980", "24984", "24988", "24992", "24996", "25000", "25004", "25008", "25012", "25016", "25020", "25024", "25028", "25032", "25036", "25040", "25044", "25048", "25052", "25056", "25060", "25064", "25068", "25072", "25076", "25080", "25084", "25088", "25092", "25096", "25100", "25104", "25108", "25112", "25116", "25120", "25124", "25128", "25132", "25136", "25140", "25144", "25148", "25152", "25156", "25160", "25164", "25168", "25172", "25176", "25180", "25184", "25188", "25192", "25196", "25200", "25204", "25208", "25212", "25216", "25220", "25224", "25228", "25232", "25236", "25240", "25244", "25248", "25252", "25256", "25260", "25264", "25268", "25272", "25276", "25280", "25284", "25288", "25292", "25296", "25300", "25304", "25308", "25312", "25316", "25320", "25324", "25328", "25332", "25336", "25340", "25344", "25348", "25352", "25356", "25360", "25364", "25368", "25372", "25376", "25380", "25384", "25388", "25392", "25396", "25400", "25404", "25408", "25412", "25416", "25420", "25424", "25428", "25432", "25436", "25440", "25444", "25448", "25452", "25456", "25460", "25464", "25468", "25472", "25476", "25480", "25484", "25488", "25492", "25496", "25500", "25504", "25508", "25512", "25516", "25520", "25524", "25528", "25532", "25536", "25540", "25544", "25548", "25552", "25556", "25560", "25564", "25568", "25572", "25576", "25580", "25584", "25588", "25592", "25596", "25600", "25604", "25608", "25612", "25616", "25620", "25624", "25628", "25632", "25636", "25640", "25644", "25648", "25652", "25656", "25660", "25664", "25668", "25672", "25676", "25680", "25684", "25688", "25692", "25696", "25700", "25704", "25708", "25712", "25716", "25720", "25724", "25728", "25732", "25736", "25740", "25744", "25748", "25752", "25756", "25760", "25764", "25768", "25772", "25776", "25780", "25784", "25788", "25792", "25796", "25800", "25804", "25808", "25812", "25816", "25820", "25824", "25828", "25832", "25836", "25840", "25844", "25848", "25852", "25856", "25860", "25864", "25868", "25872", "25876", "25880", "25884", "25888", "25892", "25896", "25900", "25904", "25908", "25912", "25916", "25920", "25924", "25928", "25932", "25936", "25940", "25944", "25948", "25952", "25956", "25960", "25964", "25968", "25972", "25976", "25980", "25984", "25988", "25992", "25996", "26000", "26004", "26008", "26012", "26016", "26020", "26024", "26028", "26032", "26036", "26040", "26044", "26048", "26052", "26056", "26060", "26064", "26068", "26072", "26076", "26080", "26084", "26088", "26092", "26096", "26100", "26104", "26108", "26112", "26116", "26120", "26124", "26128", "26132", "26136", "26140", "26144", "26148", "26152", "26156", "26160", "26164", "26168", "26172", "26176", "26180", "26184", "26188", "26192", "26196", "26200", "26204", "26208", "26212", "26216", "26220", "26224", "26228", "26232", "26236", "26240", "26244", "26248", "26252", "26256", "26260", "26264", "26268", "26272", "26276", "26280", "26284", "26288", "26292", "26296", "26300", "26304", "26308", "26312", "26316", "26320", "26324", "26328", "26332", "26336", "26340", "26344", "26348", "26352", "26356", "26360", "26364", "26368", "26372", "26376", "26380", "26384", "26388", "26392", "26396", "26400", "26404", "26408", "26412", "26416", "26420", "26424", "26428", "26432", "26436", "26440", "26444", "26448", "26452", "26456", "26460", "26464", "26468", "26472", "26476", "26480", "26484", "26488", "26492", "26496", "26500", "26504", "26508", "26512", "26516", "26520", "26524", "26528", "26532", "26536", "26540", "26544", "26548", "26552", "26556", "26560", "26564", "26568", "26572", "26576", "26580", "26584", "26588", "26592", "26596", "26600", "26604", "26608", "26612", "26616", "26620", "26624", "26628", "26632", "26636", "26640", "26644", "26648", "26652", "26656", "26660", "26664", "26668", "26672", "26676", "26680", "26684", "26688", "26692", "26696", "26700", "26704", "26708", "26712", "26716", "26720", "26724", "26728", "26732", "26736", "26740", "26744", "26748", "26752", "26756", "26760", "26764", "26768", "26772", "26776", "26780", "26784", "26788", "26792", "26796", "26800", "26804", "26808", "26812", "26816", "26820", "26824", "26828", "26832", "26836", "26840", "26844", "26848", "26852", "26856", "26860", "26864", "26868", "26872", "26876", "26880", "26884", "26888", "26892", "26896", "26900", "26904", "26908", "26912", "26916", "26920", "26924", "26928", "26932", "26936", "26940", "26944", "26948", "26952", "26956", "26960", "26964", "26968", "26972", "26976", "26980", "26984", "26988", "26992", "26996", "27000", "27004", "27008", "27012", "27016", "27020", "27024", "27028", "27032", "27036", "27040", "27044", "27048", "27052", "27056", "27060", "27064", "27068", "27072", "27076", "27080", "27084", "27088", "27092", "27096", "27100", "27104", "27108", "27112", "27116", "27120", "27124", "27128", "27132", "27136", "27140", "27144", "27148", "27152", "27156", "27160", "27164", "27168", "27172", "27176", "27180", "27184", "27188", "27192", "27196", "27200", "27204", "27208", "27212", "27216", "27220", "27224", "27228", "27232", "27236", "27240", "27244", "27248", "27252", "27256", "27260", "27264", "27268", "27272", "27276", "27280", "27284", "27288", "27292", "27296", "27300", "27304", "27308", "27312", "27316", "27320", "27324", "27328", "27332", "27336", "27340", "27344", "27348", "27352", "27356", "27360", "27364", "27368", "27372", "27376", "27380", "27384", "27388", "27392", "27396", "27400", "27404", "27408", "27412", "27416", "27420", "27424", "27428", "27432", "27436", "27440", "27444", "27448", "27452", "27456", "27460", "27464", "27468", "27472", "27476", "27480", "27484", "27488", "27492", "27496", "27500", "27504", "27508", "27512", "27516", "27520", "27524", "27528", "27532", "27536", "27540", "27544", "27548", "27552", "27556", "27560", "27564", "27568", "27572", "27576", "27580", "27584", "27588", "27592", "27596", "27600", "27604", "27608", "27612", "27616", "27620", "27624", "27628", "27632", "27636", "27640", "27644", "27648", "27652", "27656", "27660", "27664", "27668", "27672", "27676", "27680", "27684", "27688", "27692", "27696", "27700", "27704", "27708", "27712", "27716", "27720", "27724", "27728", "27732", "27736", "27740", "27744", "27748", "27752", "27756", "27760", "27764", "27768", "27772", "27776", "27780", "27784", "27788", "27792", "27796", "27800", "27804", "27808", "27812", "27816", "27820", "27824", "27828", "27832", "27836", "27840", "27844", "27848", "27852", "27856", "27860", "27864", "27868", "27872", "27876", "27880", "27884", "27888", "27892", "27896", "27900", "27904", "27908", "27912", "27916", "27920", "27924", "27928", "27932", "27936", "27940", "27944", "27948", "27952", "27956", "27960", "27964", "27968", "27972", "27976", "27980", "27984", "27988", "27992", "27996", "28000", "28004", "28008", "28012", "28016", "28020", "28024", "28028", "28032", "28036", "28040", "28044", "28048", "28052", "28056", "28060", "28064", "28068", "28072", "28076", "28080", "28084", "28088", "28092", "28096", "28100", "28104", "28108", "28112", "28116", "28120", "28124", "28128", "28132", "28136", "28140", "28144", "28148", "28152", "28156", "28160", "28164", "28168", "28172", "28176", "28180", "28184", "28188", "28192", "28196", "28200", "28204", "28208", "28212", "28216", "28220", "28224", "28228", "28232", "28236", "28240", "28244", "28248", "28252", "28256", "28260", "28264", "28268", "28272", "28276", "28280", "28284", "28288", "28292", "28296", "28300", "28304", "28308", "28312", "28316", "28320", "28324", "28328", "28332", "28336", "28340", "28344", "28348", "28352", "28356", "28360", "28364", "28368", "28372", "28376", "28380", "28384", "28388", "28392", "28396", "28400", "28404", "28408", "28412", "28416", "28420", "28424", "28428", "28432", "28436", "28440", "28444", "28448", "28452", "28456", "28460", "28464", "28468", "28472", "28476", "28480", "28484", "28488", "28492", "28496", "28500", "28504", "28508", "28512", "28516", "28520", "28524", "28528", "28532", "28536", "28540", "28544", "28548", "28552", "28556", "28560", "28564", "28568", "28572", "28576", "28580", "28584", "28588", "28592", "28596", "28600", "28604", "28608", "28612", "28616", "28620", "28624", "28628", "28632", "28636", "28640", "28644", "28648", "28652", "28656", "28660", "28664", "28668", "28672", "28676", "28680", "28684", "28688", "28692", "28696", "28700", "28704", "28708", "28712", "28716", "28720", "28724", "28728", "28732", "28736", "28740", "28744", "28748", "28752", "28756", "28760", "28764", "28768", "28772", "28776", "28780", "28784", "28788", "28792", "28796", "28800", "28804", "28808", "28812", "28816", "28820", "28824", "28828", "28832", "28836", "28840", "28844", "28848", "28852", "28856", "28860", "28864", "28868", "28872", "28876", "28880", "28884", "28888", "28892", "28896", "28900", "28904", "28908", "28912", "28916", "28920", "28924", "28928", "28932", "28936", "28940", "28944", "28948", "28952", "28956", "28960", "28964", "28968", "28972", "28976", "28980", "28984", "28988", "28992", "28996", "29000", "29004", "29008", "29012", "29016", "29020", "29024", "29028", "29032", "29036", "29040", "29044", "29048", "29052", "29056", "29060", "29064", "29068", "29072", "29076", "29080", "29084", "29088", "29092", "29096", "29100", "29104", "29108", "29112", "29116", "29120", "29124", "29128", "29132", "29136", "29140", "29144", "29148", "29152", "29156", "29160", "29164", "29168", "29172", "29176", "29180", "29184", "29188", "29192", "29196", "29200", "29204", "29208", "29212", "29216", "29220", "29224", "29228", "29232", "29236", "29240", "29244", "29248", "29252", "29256", "29260", "29264", "29268", "29272", "29276", "29280", "29284", "29288", "29292", "29296", "29300", "29304", "29308", "29312", "29316", "29320", "29324", "29328", "29332", "29336", "29340", "29344", "29348", "29352", "29356", "29360", "29364", "29368", "29372", "29376", "29380", "29384", "29388", "29392", "29396", "29400", "29404", "29408", "29412", "29416", "29420", "29424", "29428", "29432", "29436", "29440", "29444", "29448", "29452", "29456", "29460", "29464", "29468", "29472", "29476", "29480", "29484", "29488", "29492", "29496", "29500", "29504", "29508", "29512", "29516", "29520", "29524", "29528", "29532", "29536", "29540", "29544", "29548", "29552", "29556", "29560", "29564", "29568", "29572", "29576", "29580", "29584", "29588", "29592", "29596", "29600", "29604", "29608", "29612", "29616", "29620", "29624", "29628", "29632", "29636", "29640", "29644", "29648", "29652", "29656", "29660", "29664", "29668", "29672", "29676", "29680", "29684", "29688", "29692", "29696", "29700", "29704", "29708", "29712", "29716", "29720", "29724", "29728", "29732", "29736", "29740", "29744", "29748", "29752", "29756", "29760", "29764", "29768", "29772", "29776", "29780", "29784", "29788", "29792", "29796", "29800", "29804", "29808", "29812", "29816", "29820", "29824", "29828", "29832", "29836", "29840", "29844", "29848", "29852", "29856", "29860", "29864", "29868", "29872", "29876", "29880", "29884", "29888", "29892", "29896", "29900", "29904", "29908", "29912", "29916", "29920", "29924", "29928", "29932", "29936", "29940", "29944", "29948", "29952", "29956", "29960", "29964", "29968", "29972", "29976", "29980", "29984", "29988", "29992", "29996", "30000", "30004", "30008", "30012", "30016", "30020", "30024", "30028", "30032", "30036", "30040", "30044", "30048", "30052", "30056", "30060", "30064", "30068", "30072", "30076", "30080", "30084", "30088", "30092", "30096", "30100", "30104", "30108", "30112", "30116", "30120", "30124", "30128", "30132", "30136", "30140", "30144", "30148", "30152", "30156", "30160", "30164", "30168", "30172", "30176", "30180", "30184", "30188", "30192", "30196", "30200", "30204", "30208", "30212", "30216", "30220", "30224", "30228", "30232", "30236", "30240", "30244", "30248", "30252", "30256", "30260", "30264", "30268", "30272", "30276", "30280", "30284", "30288", "30292", "30296", "30300", "30304", "30308", "30312", "30316", "30320", "30324", "30328", "30332", "30336", "30340", "30344", "30348", "30352", "30356", "30360", "30364", "30368", "30372", "30376", "30380", "30384", "30388", "30392", "30396", "30400", "30404", "30408", "30412", "30416", "30420", "30424", "30428", "30432", "30436", "30440", "30444", "30448", "30452", "30456", "30460", "30464", "30468", "30472", "30476", "30480", "30484", "30488", "30492", "30496", "30500", "30504", "30508", "30512", "30516", "30520", "30524", "30528", "30532", "30536", "30540", "30544", "30548", "30552", "30556", "30560", "30564", "30568", "30572", "30576", "30580", "30584", "30588", "30592", "30596", "30600", "30604", "30608", "30612", "30616", "30620", "30624", "30628", "30632", "30636", "30640", "30644", "30648", "30652", "30656", "30660", "30664", "30668", "30672", "30676", "30680", "30684", "30688", "30692", "30696", "30700", "30704", "30708", "30712", "30716", "30720", "30724", "30728", "30732", "30736", "30740", "30744", "30748", "30752", "30756", "30760", "30764", "30768", "30772", "30776", "30780", "30784", "30788", "30792", "30796", "30800", "30804", "30808", "30812", "30816", "30820", "30824", "30828", "30832", "30836", "30840", "30844", "30848", "30852", "30856", "30860", "30864", "30868", "30872", "30876", "30880", "30884", "30888", "30892", "30896", "30900", "30904", "30908", "30912", "30916", "30920", "30924", "30928", "30932", "30936", "30940", "30944", "30948", "30952", "30956", "30960", "30964", "30968", "30972", "30976", "30980", "30984", "30988", "30992", "30996", "31000", "31004", "31008", "31012", "31016", "31020", "31024", "31028", "31032", "31036", "31040", "31044", "31048", "31052", "31056", "31060", "31064", "31068", "31072", "31076", "31080", "31084", "31088", "31092", "31096", "31100", "31104", "31108", "31112", "31116", "31120", "31124", "31128", "31132", "31136", "31140", "31144", "31148", "31152", "31156", "31160", "31164", "31168", "31172", "31176", "31180", "31184", "31188", "31192", "31196", "31200", "31204", "31208", "31212", "31216", "31220", "31224", "31228", "31232", "31236", "31240", "31244", "31248", "31252", "31256", "31260", "31264", "31268", "31272", "31276", "31280", "31284", "31288", "31292", "31296", "31300", "31304", "31308", "31312", "31316", "31320", "31324", "31328", "31332", "31336", "31340", "31344", "31348", "31352", "31356", "31360", "31364", "31368", "31372", "31376", "31380", "31384", "31388", "31392", "31396", "31400", "31404", "31408", "31412", "31416", "31420", "31424", "31428", "31432", "31436", "31440", "31444", "31448", "31452", "31456", "31460", "31464", "31468", "31472", "31476", "31480", "31484", "31488", "31492", "31496", "31500", "31504", "31508", "31512", "31516", "31520", "31524", "31528", "31532", "31536", "31540", "31544", "31548", "31552", "31556", "31560", "31564", "31568", "31572", "31576", "31580", "31584", "31588", "31592", "31596", "31600", "31604", "31608", "31612", "31616", "31620", "31624", "31628", "31632", "31636", "31640", "31644", "31648", "31652", "31656", "31660", "31664", "31668", "31672", "31676", "31680", "31684", "31688", "31692", "31696", "31700", "31704", "31708", "31712", "31716", "31720", "31724", "31728", "31732", "31736", "31740", "31744", "31748", "31752", "31756", "31760", "31764", "31768", "31772", "31776", "31780", "31784", "31788", "31792", "31796", "31800", "31804", "31808", "31812", "31816", "31820", "31824", "31828", "31832", "31836", "31840", "31844", "31848", "31852", "31856", "31860", "31864", "31868", "31872", "31876", "31880", "31884", "31888", "31892", "31896", "31900", "31904", "31908", "31912", "31916", "31920", "31924", "31928", "31932", "31936", "31940", "31944", "31948", "31952", "31956", "31960", "31964", "31968", "31972", "31976", "31980", "31984", "31988", "31992", "31996", "32000", "32004", "32008", "32012", "32016", "32020", "32024", "32028", "32032", "32036", "32040", "32044", "32048", "32052", "32056", "32060", "32064", "32068", "32072", "32076", "32080", "32084", "32088", "32092", "32096", "32100", "32104", "32108", "32112", "32116", "32120", "32124", "32128", "32132", "32136", "32140", "32144", "32148", "32152", "32156", "32160", "32164", "32168", "32172", "32176", "32180", "32184", "32188", "32192", "32196", "32200", "32204", "32208", "32212", "32216", "32220", "32224", "32228", "32232", "32236", "32240", "32244", "32248", "32252", "32256", "32260", "32264", "32268", "32272", "32276", "32280", "32284", "32288", "32292", "32296", "32300", "32304", "32308", "32312", "32316", "32320", "32324", "32328", "32332", "32336", "32340", "32344", "32348", "32352", "32356", "32360", "32364", "32368", "32372", "32376", "32380", "32384", "32388", "32392", "32396", "32400", "32404", "32408", "32412", "32416", "32420", "32424", "32428", "32432", "32436", "32440", "32444", "32448", "32452", "32456", "32460", "32464", "32468", "32472", "32476", "32480", "32484", "32488", "32492", "32496", "32500", "32504", "32508", "32512", "32516", "32520", "32524", "32528", "32532", "32536", "32540", "32544", "32548", "32552", "32556", "32560", "32564", "32568", "32572", "32576", "32580", "32584", "32588", "32592", "32596", "32600", "32604", "32608", "32612", "32616", "32620", "32624", "32628", "32632", "32636", "32640", "32644", "32648", "32652", "32656", "32660", "32664", "32668", "32672", "32676", "32680", "32684", "32688", "32692", "32696", "32700", "32704", "32708", "32712", "32716", "32720", "32724", "32728", "32732", "32736", "32740", "32744", "32748", "32752", "32756", "32760", "32764", "32768", "32772", "32776", "32780", "32784", "32788", "32792", "32796", "32800", "32804", "32808", "32812", "32816", "32820", "32824", "32828", "32832", "32836", "32840", "32844", "32848", "32852", "32856", "32860", "32864", "32868", "32872", "32876", "32880", "32884", "32888", "32892", "32896", "32900", "32904", "32908", "32912", "32916", "32920", "32924", "32928", "32932", "32936", "32940", "32944", "32948", "32952", "32956", "32960", "32964", "32968", "32972", "32976", "32980", "32984", "32988", "32992", "32996", "33000", "33004", "33008", "33012", "33016", "33020", "33024", "33028", "33032", "33036", "33040", "33044", "33048", "33052", "33056", "33060", "33064", "33068", "33072", "33076", "33080", "33084", "33088", "33092", "33096", "33100", "33104", "33108", "33112", "33116", "33120", "33124", "33128", "33132", "33136", "33140", "33144", "33148", "33152", "33156", "33160", "33164", "33168", "33172", "33176", "33180", "33184", "33188", "33192", "33196", "33200", "33204", "33208", "33212", "33216", "33220", "33224", "33228", "33232", "33236", "33240", "33244", "33248", "33252", "33256", "33260", "33264", "33268", "33272", "33276", "33280", "33284", "33288", "33292", "33296", "33300", "33304", "33308", "33312", "33316", "33320", "33324", "33328", "33332", "33336", "33340", "33344", "33348", "33352", "33356", "33360", "33364", "33368", "33372", "33376", "33380", "33384", "33388", "33392", "33396", "33400", "33404", "33408", "33412", "33416", "33420", "33424", "33428", "33432", "33436", "33440", "33444", "33448", "33452", "33456", "33460", "33464", "33468", "33472", "33476", "33480", "33484", "33488", "33492", "33496", "33500", "33504", "33508", "33512", "33516", "33520", "33524", "33528", "33532", "33536", "33540", "33544", "33548", "33552", "33556", "33560", "33564", "33568", "33572", "33576", "33580", "33584", "33588", "33592", "33596", "33600", "33604", "33608", "33612", "33616", "33620", "33624", "33628", "33632", "33636", "33640", "33644", "33648", "33652", "33656", "33660", "33664", "33668", "33672", "33676", "33680", "33684", "33688", "33692", "33696", "33700", "33704", "33708", "33712", "33716", "33720", "33724", "33728", "33732", "33736", "33740", "33744", "33748", "33752", "33756", "33760", "33764", "33768", "33772", "33776", "33780", "33784", "33788", "33792", "33796", "33800", "33804", "33808", "33812", "33816", "33820", "33824", "33828", "33832", "33836", "33840", "33844", "33848", "33852", "33856", "33860", "33864", "33868", "33872", "33876", "33880", "33884", "33888", "33892", "33896", "33900", "33904", "33908", "33912", "33916", "33920", "33924", "33928", "33932", "33936", "33940", "33944", "33948", "33952", "33956", "33960", "33964", "33968", "33972", "33976", "33980", "33984", "33988", "33992", "33996", "34000", "34004", "34008", "34012", "34016", "34020", "34024", "34028", "34032", "34036", "34040", "34044", "34048", "34052", "34056", "34060", "34064", "34068", "34072", "34076", "34080", "34084", "34088", "34092", "34096", "34100", "34104", "34108", "34112", "34116", "34120", "34124", "34128", "34132", "34136", "34140", "34144", "34148", "34152", "34156", "34160", "34164", "34168", "34172", "34176", "34180", "34184", "34188", "34192", "34196", "34200", "34204", "34208", "34212", "34216", "34220", "34224", "34228", "34232", "34236", "34240", "34244", "34248", "34252", "34256", "34260", "34264", "34268", "34272", "34276", "34280", "34284", "34288", "34292", "34296", "34300", "34304", "34308", "34312", "34316", "34320", "34324", "34328", "34332", "34336", "34340", "34344", "34348", "34352", "34356", "34360", "34364", "34368", "34372", "34376", "34380", "34384", "34388", "34392", "34396", "34400", "34404", "34408", "34412", "34416", "34420", "34424", "34428", "34432", "34436", "34440", "34444", "34448", "34452", "34456", "34460", "34464", "34468", "34472", "34476", "34480", "34484", "34488", "34492", "34496", "34500", "34504", "34508", "34512", "34516", "34520", "34524", "34528", "34532", "34536", "34540", "34544", "34548", "34552", "34556", "34560", "34564", "34568", "34572", "34576", "34580", "34584", "34588", "34592", "34596", "34600", "34604", "34608", "34612", "34616", "34620", "34624", "34628", "34632", "34636", "34640", "34644", "34648", "34652", "34656", "34660", "34664", "34668", "34672", "34676", "34680", "34684", "34688", "34692", "34696", "34700", "34704", "34708", "34712", "34716", "34720", "34724", "34728", "34732", "34736", "34740", "34744", "34748", "34752", "34756", "34760", "34764", "34768", "34772", "34776", "34780", "34784", "34788", "34792", "34796", "34800", "34804", "34808", "34812", "34816", "34820", "34824", "34828", "34832", "34836", "34840", "34844", "34848", "34852", "34856", "34860", "34864", "34868", "34872", "34876", "34880", "34884", "34888", "34892", "34896", "34900", "34904", "34908", "34912", "34916", "34920", "34924", "34928", "34932", "34936", "34940", "34944", "34948", "34952", "34956", "34960", "34964", "34968", "34972", "34976", "34980", "34984", "34988", "34992", "34996", "35000", "35004", "35008", "35012", "35016", "35020", "35024", "35028", "35032", "35036", "35040", "35044", "35048", "35052", "35056", "35060", "35064", "35068", "35072", "35076", "35080", "35084", "35088", "35092", "35096", "35100", "35104", "35108", "35112", "35116", "35120", "35124", "35128", "35132", "35136", "35140", "35144", "35148", "35152", "35156", "35160", "35164", "35168", "35172", "35176", "35180", "35184", "35188", "35192", "35196", "35200", "35204", "35208", "35212", "35216", "35220", "35224", "35228", "35232", "35236", "35240", "35244", "35248", "35252", "35256", "35260", "35264", "35268", "35272", "35276", "35280", "35284", "35288", "35292", "35296", "35300", "35304", "35308", "35312", "35316", "35320", "35324", "35328", "35332", "35336", "35340", "35344", "35348", "35352", "35356", "35360", "35364", "35368", "35372", "35376", "35380", "35384", "35388", "35392", "35396", "35400", "35404", "35408", "35412", "35416", "35420", "35424", "35428", "35432", "35436", "35440", "35444", "35448", "35452", "35456", "35460", "35464", "35468", "35472", "35476", "35480", "35484", "35488", "35492", "35496", "35500", "35504", "35508", "35512", "35516", "35520", "35524", "35528", "35532", "35536", "35540", "35544", "35548", "35552", "35556", "35560", "35564", "35568", "35572", "35576", "35580", "35584", "35588", "35592", "35596", "35600", "35604", "35608", "35612", "35616", "35620", "35624", "35628", "35632", "35636", "35640", "35644", "35648", "35652", "35656", "35660", "35664", "35668", "35672", "35676", "35680", "35684", "35688", "35692", "35696", "35700", "35704", "35708", "35712", "35716", "35720", "35724", "35728", "35732", "35736", "35740", "35744", "35748", "35752", "35756", "35760", "35764", "35768", "35772", "35776", "35780", "35784", "35788", "35792", "35796", "35800", "35804", "35808", "35812", "35816", "35820", "35824", "35828", "35832", "35836", "35840", "35844", "35848", "35852", "35856", "35860", "35864", "35868", "35872", "35876", "35880", "35884", "35888", "35892", "35896", "35900", "35904", "35908", "35912", "35916", "35920", "35924", "35928", "35932", "35936", "35940", "35944", "35948", "35952", "35956", "35960", "35964", "35968", "35972", "35976", "35980", "35984", "35988", "35992", "35996", "36000", "36004", "36008", "36012", "36016", "36020", "36024", "36028", "36032", "36036", "36040", "36044", "36048", "36052", "36056", "36060", "36064", "36068", "36072", "36076", "36080", "36084", "36088", "36092", "36096", "36100", "36104", "36108", "36112", "36116", "36120", "36124", "36128", "36132", "36136", "36140", "36144", "36148", "36152", "36156", "36160", "36164", "36168", "36172", "36176", "36180", "36184", "36188", "36192", "36196", "36200", "36204", "36208", "36212", "36216", "36220", "36224", "36228", "36232", "36236", "36240", "36244", "36248", "36252", "36256", "36260", "36264", "36268", "36272", "36276", "36280", "36284", "36288", "36292", "36296", "36300", "36304", "36308", "36312", "36316", "36320", "36324", "36328", "36332", "36336", "36340", "36344", "36348", "36352", "36356", "36360", "36364", "36368", "36372", "36376", "36380", "36384", "36388", "36392", "36396", "36400", "36404", "36408", "36412", "36416", "36420", "36424", "36428", "36432", "36436", "36440", "36444", "36448", "36452", "36456", "36460", "36464", "36468", "36472", "36476", "36480", "36484", "36488", "36492", "36496", "36500", "36504", "36508", "36512", "36516", "36520", "36524", "36528", "36532", "36536", "36540", "36544", "36548", "36552", "36556", "36560", "36564", "36568", "36572", "36576", "36580", "36584", "36588", "36592", "36596", "36600", "36604", "36608", "36612", "36616", "36620", "36624", "36628", "36632", "36636", "36640", "36644", "36648", "36652", "36656", "36660", "36664", "36668", "36672", "36676", "36680", "36684", "36688", "36692", "36696", "36700", "36704", "36708", "36712", "36716", "36720", "36724", "36728", "36732", "36736", "36740", "36744", "36748", "36752", "36756", "36760", "36764", "36768", "36772", "36776", "36780", "36784", "36788", "36792", "36796", "36800", "36804", "36808", "36812", "36816", "36820", "36824", "36828", "36832", "36836", "36840", "36844", "36848", "36852", "36856", "36860", "36864", "36868", "36872", "36876", "36880", "36884", "36888", "36892", "36896", "36900", "36904", "36908", "36912", "36916", "36920", "36924", "36928", "36932", "36936", "36940", "36944", "36948", "36952", "36956", "36960", "36964", "36968", "36972", "36976", "36980", "36984", "36988", "36992", "36996", "37000", "37004", "37008", "37012", "37016", "37020", "37024", "37028", "37032", "37036", "37040", "37044", "37048", "37052", "37056", "37060", "37064", "37068", "37072", "37076", "37080", "37084", "37088", "37092", "37096", "37100", "37104", "37108", "37112", "37116", "37120", "37124", "37128", "37132", "37136", "37140", "37144", "37148", "37152", "37156", "37160", "37164", "37168", "37172", "37176", "37180", "37184", "37188", "37192", "37196", "37200", "37204", "37208", "37212", "37216", "37220", "37224", "37228", "37232", "37236", "37240", "37244", "37248", "37252", "37256", "37260", "37264", "37268", "37272", "37276", "37280", "37284", "37288", "37292", "37296", "37300", "37304", "37308", "37312", "37316", "37320", "37324", "37328", "37332", "37336", "37340", "37344", "37348", "37352", "37356", "37360", "37364", "37368", "37372", "37376", "37380", "37384", "37388", "37392", "37396", "37400", "37404", "37408", "37412", "37416", "37420", "37424", "37428", "37432", "37436", "37440", "37444", "37448", "37452", "37456", "37460", "37464", "37468", "37472", "37476", "37480", "37484", "37488", "37492", "37496", "37500", "37504", "37508", "37512", "37516", "37520", "37524", "37528", "37532", "37536", "37540", "37544", "37548", "37552", "37556", "37560", "37564", "37568", "37572", "37576", "37580", "37584", "37588", "37592", "37596", "37600", "37604", "37608", "37612", "37616", "37620", "37624", "37628", "37632", "37636", "37640", "37644", "37648", "37652", "37656", "37660", "37664", "37668", "37672", "37676", "37680", "37684", "37688", "37692", "37696", "37700", "37704", "37708", "37712", "37716", "37720", "37724", "37728", "37732", "37736", "37740", "37744", "37748", "37752", "37756", "37760", "37764", "37768", "37772", "37776", "37780", "37784", "37788", "37792", "37796", "37800", "37804", "37808", "37812", "37816", "37820", "37824", "37828", "37832", "37836", "37840", "37844", "37848", "37852", "37856", "37860", "37864", "37868", "37872", "37876", "37880", "37884", "37888", "37892", "37896", "37900", "37904", "37908", "37912", "37916", "37920", "37924", "37928", "37932", "37936", "37940", "37944", "37948", "37952", "37956", "37960", "37964", "37968", "37972", "37976", "37980", "37984", "37988", "37992", "37996", "38000", "38004", "38008", "38012", "38016", "38020", "38024", "38028", "38032", "38036", "38040", "38044", "38048", "38052", "38056", "38060", "38064", "38068", "38072", "38076", "38080", "38084", "38088", "38092", "38096", "38100", "38104", "38108", "38112", "38116", "38120", "38124", "38128", "38132", "38136", "38140", "38144", "38148", "38152", "38156", "38160", "38164", "38168", "38172", "38176", "38180", "38184", "38188", "38192", "38196", "38200", "38204", "38208", "38212", "38216", "38220", "38224", "38228", "38232", "38236", "38240", "38244", "38248", "38252", "38256", "38260", "38264", "38268", "38272", "38276", "38280", "38284", "38288", "38292", "38296", "38300", "38304", "38308", "38312", "38316", "38320", "38324", "38328", "38332", "38336", "38340", "38344", "38348", "38352", "38356", "38360", "38364", "38368", "38372", "38376", "38380", "38384", "38388", "38392", "38396", "38400", "38404", "38408", "38412", "38416", "38420", "38424", "38428", "38432", "38436", "38440", "38444", "38448", "38452", "38456", "38460", "38464", "38468", "38472", "38476", "38480", "38484", "38488", "38492", "38496", "38500", "38504", "38508", "38512", "38516", "38520", "38524", "38528", "38532", "38536", "38540", "38544", "38548", "38552", "38556", "38560", "38564", "38568", "38572", "38576", "38580", "38584", "38588", "38592", "38596", "38600", "38604", "38608", "38612", "38616", "38620", "38624", "38628", "38632", "38636", "38640", "38644", "38648", "38652", "38656", "38660", "38664", "38668", "38672", "38676", "38680", "38684", "38688", "38692", "38696", "38700", "38704", "38708", "38712", "38716", "38720", "38724", "38728", "38732", "38736", "38740", "38744", "38748", "38752", "38756", "38760", "38764", "38768", "38772", "38776", "38780", "38784", "38788", "38792", "38796", "38800", "38804", "38808", "38812", "38816", "38820", "38824", "38828", "38832", "38836", "38840", "38844", "38848", "38852", "38856", "38860", "38864", "38868", "38872", "38876", "38880", "38884", "38888", "38892", "38896", "38900", "38904", "38908", "38912", "38916", "38920", "38924", "38928", "38932", "38936", "38940", "38944", "38948", "38952", "38956", "38960", "38964", "38968", "38972", "38976", "38980", "38984", "38988", "38992", "38996", "39000", "39004", "39008", "39012", "39016", "39020", "39024", "39028", "39032", "39036", "39040", "39044", "39048", "39052", "39056", "39060", "39064", "39068", "39072", "39076", "39080", "39084", "39088", "39092", "39096", "39100", "39104", "39108", "39112", "39116", "39120", "39124", "39128", "39132", "39136", "39140", "39144", "39148", "39152", "39156", "39160", "39164", "39168", "39172", "39176", "39180", "39184", "39188", "39192", "39196", "39200", "39204", "39208", "39212", "39216", "39220", "39224", "39228", "39232", "39236", "39240", "39244", "39248", "39252", "39256", "39260", "39264", "39268", "39272", "39276", "39280", "39284", "39288", "39292", "39296", "39300", "39304", "39308", "39312", "39316", "39320", "39324", "39328", "39332", "39336", "39340", "39344", "39348", "39352", "39356", "39360", "39364", "39368", "39372", "39376", "39380", "39384", "39388", "39392", "39396", "39400", "39404", "39408", "39412", "39416", "39420", "39424", "39428", "39432", "39436", "39440", "39444", "39448", "39452", "39456", "39460", "39464", "39468", "39472", "39476", "39480", "39484", "39488", "39492", "39496", "39500", "39504", "39508", "39512", "39516", "39520", "39524", "39528", "39532", "39536", "39540", "39544", "39548", "39552", "39556", "39560", "39564", "39568", "39572", "39576", "39580", "39584", "39588", "39592", "39596", "39600", "39604", "39608", "39612", "39616", "39620", "39624", "39628", "39632", "39636", "39640", "39644", "39648", "39652", "39656", "39660", "39664", "39668", "39672", "39676", "39680", "39684", "39688", "39692", "39696", "39700", "39704", "39708", "39712", "39716", "39720", "39724", "39728", "39732", "39736", "39740", "39744", "39748", "39752", "39756", "39760", "39764", "39768", "39772", "39776", "39780", "39784", "39788", "39792", "39796", "39800", "39804", "39808", "39812", "39816", "39820", "39824", "39828", "39832", "39836", "39840", "39844", "39848", "39852", "39856", "39860", "39864", "39868", "39872", "39876", "39880", "39884", "39888", "39892", "39896", "39900", "39904", "39908", "39912", "39916", "39920", "39924", "39928", "39932", "39936", "39940", "39944", "39948", "39952", "39956", "39960", "39964", "39968", "39972", "39976", "39980", "39984", "39988", "39992", "39996", "40000" };

                        if (valorVerificar.Contains(contador.ToString()))
                        {
                            //Html.AppendLine("	<div class=\"quebrapagina\">" + ((contador/4) + 1) +"</div>");
                            Html.AppendLine("		<div class=\"quebrapagina\"></div>");
                        }
                    }
                }
            }
        }

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        private Bitmap GerarQRCode(int width, int height, string text)
        {
            try
            {
                var bw = new ZXing.BarcodeWriter();
                var encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                var resultado = new Bitmap(bw.Write(text));
                return resultado;
            }
            catch
            {
                throw;
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Visible = false;
            this.Close();
        }

        private void FormularioImpressaoEntregaObjetosModelo2_KeyDown(object sender, KeyEventArgs e)
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

        private void Fecha_Form()
        {
            this.Close();
        }
    }
}
