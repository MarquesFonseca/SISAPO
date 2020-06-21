using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using SISAPO.ClassesDiversas;

namespace SISAPO
{
    public partial class FormularioAtualizacaoObjetosDestinatarioAusente : Form
    {
        public bool abortarAtualizacao = false;
        private string CodigoObjetoAtual = string.Empty;
        public List<string> ListaLinksJavaScript = new List<string>();
        private bool UltimoElemento = false;
        private enum TipoTela { Rastreamento1, ListaObjetos2, DetalhesDeObjetos3, DestinatarioAusente }

        private DataSet dadosAgencia = new DataSet();

        private TipoTela tipoTela = TipoTela.Rastreamento1;
        private bool DetalhesDeObjetos3 = false;
        private string TelaRastreamento_1_1 = @"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Nova_Rastreamento_Problema\RastreamantoDetalhes-1-1-problema.htm";
        //private string TelaRastreamento_1_1 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\1-1-TelaRastreamento.htm";
        //private string TelaDetalhesDeObjetos_1_3 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\1-3-TelaDetalhesDeObjetos_defeito.htm";
        //private string TelaNomeCliente_1_4 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\1-4-TelaNomeCliente.htm";
        //private string TelaRastreamento_2_1 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\2-1-TelaRastreamento.htm";
        //private string TelaListaDeObjetos_2_2 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\2-2-TelaListaDeObjetos.htm";
        private string TelaDetalhesDeObjetos_2_3 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\2-3-TelaDetalhesDeObjetos.htm";
        //private string TelaNomeCliente_2_4 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\2-4-TelaNomeCliente.htm";
        //private string TelaNomeCliente_2_4 = @"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\Rastreamento_Unificado_htm.htm";

        private string enderecoSRO = @"http://websro2/rastreamento/sro?opcao=PESQUISA&objetos=";

        public FormularioAtualizacaoObjetosDestinatarioAusente(string codigoObjetoIniciado)
        {
            InitializeComponent();
            CodigoObjetoAtual = codigoObjetoIniciado;
            ListaLinksJavaScript = new List<string>();
            UltimoElemento = false;
            tipoTela = TipoTela.Rastreamento1;
            DetalhesDeObjetos3 = false;

            dadosAgencia = RetornaDadosAgencia();

            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
            {
                // caminho que define o teste
                //caminho para tela de consulta atraves de códigos de objetos rastreadores
                tipoTela = TipoTela.Rastreamento1;
                webBrowser1.Url = new Uri(TelaRastreamento_1_1);
            }
            if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
            {
                //caminho para tela de consulta atraves de códigos de objetos rastreadores
                tipoTela = TipoTela.DetalhesDeObjetos3;
                webBrowser1.Url = new Uri(enderecoSRO + codigoObjetoIniciado);
            }
        }

        private DataSet RetornaDadosAgencia()
        {
            DataSet ds = new DataSet();
            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    ds = dao.RetornaDataSet("SELECT TOP 1 NomeAgenciaLocal, EnderecoAgenciaLocal FROM TabelaConfiguracoesSistema");
                }
                return ds;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

        }

        private void FormularioAtualizacaoObjetosDestinatarioAusente_Load(object sender, EventArgs e)
        {
            textBox1.Visible = Configuracoes.ExibicaoMensagensParaDesenvolvedor;
            panel1.Visible = Configuracoes.ExibicaoMensagensParaDesenvolvedor;

            for (int i = 0; i < 5; i++) SendKeys.Send("{TAB}");
        }

        public static FormularioAtualizacaoObjetosDestinatarioAusente RetornaComponentes()
        {
            FormularioAtualizacaoObjetosDestinatarioAusente formularioAtualizacaoObjetosDestinatarioAusente;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioAtualizacaoObjetosDestinatarioAusente")
                {
                    formularioAtualizacaoObjetosDestinatarioAusente = (FormularioAtualizacaoObjetosDestinatarioAusente)item;
                    return (FormularioAtualizacaoObjetosDestinatarioAusente)item;
                }
            }
            return null;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.AbsoluteUri == "about:blank")
            {
                EscreveTextoTextBox("Url - about:blank - return");
                return;
            }
            if (string.IsNullOrEmpty(CodigoObjetoAtual))
            {
                Mensagens.InformaDesenvolvedor("string.IsNullOrEmpty(CodigoObjetoAtual)");
                EscreveTextoTextBox("CodigoObjetoAtual == null -- return");
                webBrowser1.Document.GetElementsByTagName("textarea")[0].Focus();
                return;
            }
            //return;
            try
            {
                EscreveTextoTextBox("TipoTela atual = " + tipoTela.ToString());

                switch (tipoTela)
                {
                    
                    #region Acessando TipoTela.Rastreamento1
                    case TipoTela.Rastreamento1:
                        #region TipoAmbiente.Producao
                        if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                        {
                            //webBrowser1.Document.InvokeScript("Detalhes", new object[] { "DETALHES", "0", "intra" });

                            if (webBrowser1.Document.GetElementsByTagName("textarea").Count == 0) return;
                            else
                            {
                                EscreveTextoTextBox("Será encaminhado para a tela 'TipoTela.DetalhesDeObjetos3'");
                                tipoTela = TipoTela.DetalhesDeObjetos3;
                                webBrowser1.Document.GetElementsByTagName("textarea")[0].InnerText = CodigoObjetoAtual.ToString();
                                webBrowser1.Document.GetElementsByTagName("textarea")[0].Focus();
                                SendKeys.Send("{TAB}");
                                SendKeys.Send("{ENTER}");
                            }
                        }
                        #endregion

                        #region TipoAmbiente.Desenvolvimento
                        if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                        {
                            EscreveTextoTextBox("Será encaminhado para a tela 'TipoTela.DetalhesDeObjetos3'");
                            tipoTela = TipoTela.DetalhesDeObjetos3;
                            //webBrowser1.Url = new Uri(TelaDetalhesDeObjetos_1_3);
                            //webBrowser1.Url = new Uri(TelaRastreamento_1_1);
                            //webBrowser1.Url = new Uri(@"file:///C:/Users/MARQUES/Documents/Visual%20Studio%202010/Projects/SISAPO/SISAPO/bin/Debug/Nova_Rastreamento_Problema/RastreamantoDetalhes-3-2-problema.htm");
                            webBrowser1.Url = new Uri(@"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Rastreamento 05062019\OH239391285BR_DETALHES.htm");
                        }
                        #endregion
                        break;
                    #endregion

                    #region Acessando ListaObjetos2
                    case TipoTela.ListaObjetos2:
                        tipoTela = TipoTela.DetalhesDeObjetos3;
                        //javascript: DetalhesTodos('DETALHES','-1','intra');

                        if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                            webBrowser1.Document.InvokeScript("DetalhesTodos", new object[] { "DETALHES", "-1", "intra" });
                        if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                            webBrowser1.Url = new Uri(TelaDetalhesDeObjetos_2_3);

                        break;
                    #endregion

                    #region Acessando DetalhesDeObjetos3
                    case TipoTela.DetalhesDeObjetos3:
                        //EscreveTextoTextBox("opa parou aqui...");
                        Mensagens.InformaDesenvolvedor("TipoTela.DetalhesDeObjetos3");
                        DetalhesDeObjetos3 = true;
                        if (ListaLinksJavaScript.Count == 0)
                            SeparaLinksDosObjetosRastreados();

                        //clica no item para visualizar nome;
                        foreach (var item in ListaLinksJavaScript)
                        {
                            //DetalhesPesquisa('DETALHESINTRA','OA899613034BR','LDI','1094248527','1','31/08/2018 10:34:00','intra')
                            //webBrowser1.Document.InvokeScript("DetalhesPesquisa", new object[] { "DETALHESINTRA", "OA899613034BR", "LDI", "1094248527", "1", "31/08/2018 10:34:00", "intra" });
                            UltimoElemento = ListaLinksJavaScript.Count == 1 ? true : false;
                            string PrimeiroListaLinksJavaScript = ListaLinksJavaScript[0];
                            CodigoObjetoAtual = PrimeiroListaLinksJavaScript.Substring(34, 13);
                            ListaLinksJavaScript.Remove(ListaLinksJavaScript[0]);
                            tipoTela = TipoTela.DestinatarioAusente;
                            DetalhesDeObjetos3 = true;
                            #region TipoAmbiente.Producao
                            if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                            {
                                EscreveTextoTextBox("antes de clicar...");
                                Mensagens.InformaDesenvolvedor("link para clicar...: " + PrimeiroListaLinksJavaScript);
                                webBrowser1.Document.InvokeScript("DetalhesPesquisa", new object[] {
                                PrimeiroListaLinksJavaScript.Split('\'')[1], /*DETALHESINTRA*/
                                PrimeiroListaLinksJavaScript.Split('\'')[3], /*OH239391285BR*/ 
                                PrimeiroListaLinksJavaScript.Split('\'')[5], /*OEC*/
                                PrimeiroListaLinksJavaScript.Split('\'')[7], /*33113410964*/ 
                                PrimeiroListaLinksJavaScript.Split('\'')[9], /*1*/ 
                                PrimeiroListaLinksJavaScript.Split('\'')[11],/*15/06/2019 08:24:18*/ 
                                PrimeiroListaLinksJavaScript.Split('\'')[13] /*intra*/ });
                            }
                            #endregion
                            #region TipoAmbiente.Desenvolvimento
                            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                            {
                                //webBrowser1.Url = new Uri(TelaNomeCliente_2_4);
                                webBrowser1.Url = new Uri(@"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Rastreamento 05062019\OH239391285BR_SAIU_PARA_ENTREGA_DESTINATARIO.htm");
                            }
                            #endregion
                            break;
                        }
                        break;
                    #endregion

                    #region TipoTela.DestinatarioAusente
                    case TipoTela.DestinatarioAusente:
                        EscreveTextoTextBox("Pegando os dados de Destinatario Ausente...");
                        Mensagens.InformaDesenvolvedor("Abrindo a detalhes antes de capturar os dados da tela para objeto Destinatario Ausente");
                        string SituacaoDestinatarioAusente = string.Empty;
                        string AgrupadoDestinatarioAusente = string.Empty;
                        string CoordenadasDestinatarioAusente = string.Empty;

                        #region Capturando os dados
                        for (int i = 0; i < webBrowser1.Document.GetElementsByTagName("TR").Count; i++)
                        {
                            string InnerTextSaiuParaEntrega = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Trim();
                            if (string.IsNullOrEmpty(InnerTextSaiuParaEntrega)) continue;
                            if (InnerTextSaiuParaEntrega.Contains("Situação:"))
                            {
                                SituacaoDestinatarioAusente = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Situação: ", "").ToUpper().Trim();
                                EscreveTextoTextBox("Situação: " + SituacaoDestinatarioAusente.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Situação: " + SituacaoDestinatarioAusente.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Agrupado:"))
                            {
                                AgrupadoDestinatarioAusente = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Agrupado: ", "").ToUpper().Trim();
                                EscreveTextoTextBox("Agrupado: " + AgrupadoDestinatarioAusente.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Agrupado: " + AgrupadoDestinatarioAusente.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Coordenadas:"))
                            {
                                CoordenadasDestinatarioAusente = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Coordenadas: ", "").ToUpper().Trim();
                                EscreveTextoTextBox("Coordenadas: " + CoordenadasDestinatarioAusente.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Coordenadas: " + CoordenadasDestinatarioAusente.ToString()));
                                break;//pois supoe que é a ultima linha embaixo.... obvio.
                            }
                        }
                        #endregion

                        #region grava no banco de dados
                        using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                        {
                            if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                            DataSet ds = dao.RetornaDataSet("SELECT top 1 NomeCliente FROM TabelaObjetosSROLocal WHERE (CodigoObjeto = @CodigoObjeto)", new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual });
                            if (ds.Tables[0].Rows.Count == 1)
                            {
                                Mensagens.InformaDesenvolvedor(string.Format("Gravando no banco detalhes objeto SaiuParaEntrega"));
                                dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET SituacaoDestinatarioAusente = @SituacaoDestinatarioAusente, AgrupadoDestinatarioAusente = @AgrupadoDestinatarioAusente, CoordenadasDestinatarioAusente = @CoordenadasDestinatarioAusente WHERE CodigoObjeto = @CodigoObjeto ", new List<Parametros>(){
                                                            new Parametros("@SituacaoDestinatarioAusente", TipoCampo.Text, SituacaoDestinatarioAusente),
                                                            new Parametros("@AgrupadoDestinatarioAusente", TipoCampo.Text, AgrupadoDestinatarioAusente),
                                                            new Parametros("@CoordenadasDestinatarioAusente", TipoCampo.Text, CoordenadasDestinatarioAusente),
                                                            new Parametros("@CodigoObjeto", TipoCampo.Text, CodigoObjetoAtual)
                                            });
                            }
                        }
                        #endregion

                        if (DetalhesDeObjetos3)
                        {
                            if (ListaLinksJavaScript.Count == 0)
                            {
                                DetalhesDeObjetos3 = false;
                                this.Close();
                            }
                            if (ListaLinksJavaScript.Count > 0)
                            {
                                DetalhesDeObjetos3 = true;
                                tipoTela = TipoTela.DetalhesDeObjetos3;
                                if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                                {
                                    webBrowser1.Url = new Uri(TelaDetalhesDeObjetos_2_3);
                                }
                                if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                                {
                                    for (int i = 0; i <= 7; i++) SendKeys.Send("{TAB}");
                                    //SendKeys.Send("{ENTER}");
                                    //webBrowser1.GoBack();
                                    //javascript:%20window.history.back();
                                    //webBrowser1.Document.InvokeScript("window.history.back()", new object[] { });
                                    //webBrowser1.Document.InvokeScript("eval", "window.history.back()");
                                    //webBrowser1.Document.Window.History.Go(-1);
                                }
                            }
                        }
                        break;
                    #endregion

                    default:
                        break;
                }

                return;
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message.ToString());
                throw new Exception();
            }
        }

        private void SeparaLinksDosObjetosRastreados()
        {
            bool ExisteCampoParaDestinatarioAusente = false;
            ListaLinksJavaScript = new List<string>();
            foreach (var itemTR in webBrowser1.Document.GetElementsByTagName("TR"))
            {
                string itemTRInnerText = ((System.Windows.Forms.HtmlElement)(itemTR)).InnerText;
                string itemTRInnerHtml = ((System.Windows.Forms.HtmlElement)(itemTR)).InnerHtml;
                string NomeAgenciaLocal = dadosAgencia.Tables[0].Rows[0][0].ToString().Trim().ToUpper();
                if (itemTRInnerText == null || string.IsNullOrEmpty(itemTRInnerText.Trim())) continue;

                foreach (var itemTD in ((System.Windows.Forms.HtmlElement)(itemTR)).GetElementsByTagName("TD"))
                {
                    if (((System.Windows.Forms.HtmlElement)(itemTD)).InnerText == null) continue;
                    string CampoAtual = ((System.Windows.Forms.HtmlElement)(itemTD)).InnerText.Trim().ToUpper();
                    if (CampoAtual == null || string.IsNullOrEmpty(CampoAtual.Trim())) continue;

                    #region "Destinatário Ausente"
                    if (CampoAtual.ToUpper().Contains("Destinatário Ausente".ToUpper()))
                    {
                        //Saiu para entrega
                        foreach (var pegalinkAtual2 in ((System.Windows.Forms.HtmlElement)(itemTR)).GetElementsByTagName("A"))
                        {
                            ExisteCampoParaDestinatarioAusente = true;
                            var linkatual = ((System.Windows.Forms.HtmlElement)(pegalinkAtual2)).OuterHtml.Replace("%20", " ");
                            ListaLinksJavaScript.Add(linkatual.Substring(21, 104));
                            return;
                        }
                        continue;
                    }
                    #endregion
                }
            }
            if (ExisteCampoParaDestinatarioAusente == false) this.Close();
        }

        private StringBuilder textoTextBox = new StringBuilder();
        private void EscreveTextoTextBox(string mensagem)
        {
            textoTextBox.AppendLine(mensagem);
            textBox1.Text = textoTextBox.ToString();
            textBox1.ScrollToCaret();
        }

        private void FormularioAtualizacaoObjetosDestinatarioAusente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Mensagens.Pergunta("Deseja abortar a atualização de todos os objetos?") == System.Windows.Forms.DialogResult.Yes)
                {
                    abortarAtualizacao = true;
                    this.Close();
                    return;
                }
            }
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            if (Mensagens.Pergunta("Deseja abortar a atualização de todos os objetos?") == System.Windows.Forms.DialogResult.Yes)
            {
                abortarAtualizacao = true;
                this.Close();
                return;
            }
        }

        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Mensagens.Pergunta("Deseja abortar a atualização de todos os objetos?") == System.Windows.Forms.DialogResult.Yes)
                {
                    abortarAtualizacao = true;
                    this.Close();
                    return;
                }
            }
        }
    }
}