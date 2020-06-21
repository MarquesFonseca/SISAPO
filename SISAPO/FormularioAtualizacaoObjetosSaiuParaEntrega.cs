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
    public partial class FormularioAtualizacaoObjetosSaiuParaEntrega : Form
    {
        public bool abortarAtualizacao = false;
        private string CodigoObjetoAtual = string.Empty;
        public List<string> ListaLinksJavaScript = new List<string>();
        private bool UltimoElemento = false;
        private enum TipoTela { Rastreamento1, ListaObjetos2, DetalhesDeObjetos3, SaiuParaEntrega }

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

        public FormularioAtualizacaoObjetosSaiuParaEntrega(string codigoObjetoIniciado)
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

        private void FormularioAtualizacaoObjetosSaiuParaEntrega_Load(object sender, EventArgs e)
        {
            textBox1.Visible = Configuracoes.ExibicaoMensagensParaDesenvolvedor;
            panel1.Visible = Configuracoes.ExibicaoMensagensParaDesenvolvedor;

            for (int i = 0; i < 5; i++) SendKeys.Send("{TAB}");
        }

        public static FormularioAtualizacaoObjetosSaiuParaEntrega RetornaComponentes()
        {
            FormularioAtualizacaoObjetosSaiuParaEntrega formularioAtualizacaoObjetosSaiuParaEntrega;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioAtualizacaoObjetosSaiuParaEntrega")
                {
                    formularioAtualizacaoObjetosSaiuParaEntrega = (FormularioAtualizacaoObjetosSaiuParaEntrega)item;
                    return (FormularioAtualizacaoObjetosSaiuParaEntrega)item;
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
                            tipoTela = TipoTela.SaiuParaEntrega;
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

                    #region TipoTela.SaiuParaEntrega
                    case TipoTela.SaiuParaEntrega:
                        EscreveTextoTextBox("Pegando os dados de SaiuParaEntrega...");
                        Mensagens.InformaDesenvolvedor("Abrindo a detalhes antes de capturar os dados da tela para objeto SaiuParaEntrega");
                        string UnidadeLOEC = string.Empty;
                        string MunicipioLOEC = string.Empty;
                        string CriacaoLOEC = string.Empty;
                        string CarteiroLOEC = string.Empty;
                        string DistritoLOEC = string.Empty;
                        string NumeroLOEC = string.Empty;
                        string ClienteLOEC = string.Empty;
                        string EnderecoLOEC = string.Empty;
                        string BairroLOEC = string.Empty;
                        string LocalidadeLOEC = string.Empty;

                        #region Capturando os dados
                        for (int i = 0; i < webBrowser1.Document.GetElementsByTagName("TR").Count; i++)
                        {
                            string InnerTextSaiuParaEntrega = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Trim();
                            if (string.IsNullOrEmpty(InnerTextSaiuParaEntrega)) continue;
                            if (InnerTextSaiuParaEntrega.Contains("Unidade:"))
                            {
                                UnidadeLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Unidade: ", "").ToUpper().Trim();
                                UnidadeLOEC = UnidadeLOEC.Length >= 8 ? string.Format("{0}-{1}", UnidadeLOEC.Substring(0, 5), UnidadeLOEC.Substring(5, UnidadeLOEC.Length - 5)) : UnidadeLOEC;
                                EscreveTextoTextBox("Unidade: " + UnidadeLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Unidade: " + UnidadeLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Município:"))
                            {
                                MunicipioLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Município: ", "").ToUpper().Trim();
                                EscreveTextoTextBox("Município: " + MunicipioLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Município: " + MunicipioLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Criado em:"))
                            {
                                CriacaoLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Criado em: ", "").ToUpper().Trim();
                                EscreveTextoTextBox("Criado em: " + CriacaoLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Criado em: " + CriacaoLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Carteiro:"))
                            {
                                CarteiroLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Carteiro: ", "").ToUpper().Trim();
                                EscreveTextoTextBox("Carteiro: " + CarteiroLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Carteiro: " + CarteiroLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Distrito:"))
                            {
                                DistritoLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Distrito: ", "").ToUpper().Trim();
                                EscreveTextoTextBox("Distrito: " + DistritoLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Distrito: " + DistritoLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("LOEC:"))
                            {
                                string temp = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("LOEC: ", "").ToUpper().Trim();
                                NumeroLOEC = temp.Split(':')[0].Replace(" - ID", "");
                                EscreveTextoTextBox("LOEC: " + NumeroLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("LOEC: " + NumeroLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Cliente:"))
                            {
                                ClienteLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Cliente: ", "").ToUpper().Trim();
                                EscreveTextoTextBox("Cliente: " + ClienteLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Cliente: " + ClienteLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Endereço:"))
                            {
                                EnderecoLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Endereço: ", "").ToUpper().Trim();
                                EscreveTextoTextBox("Endereço: " + EnderecoLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Endereço: " + EnderecoLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Bairro:"))
                            {
                                BairroLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Bairro: ", "").ToUpper().Trim();
                                EscreveTextoTextBox("Bairro: " + BairroLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Bairro: " + BairroLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Localidade:"))
                            {
                                LocalidadeLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Localidade: ", "").ToUpper().Trim();
                                LocalidadeLOEC = LocalidadeLOEC.Length >= 8 ? string.Format("{0}-{1}", LocalidadeLOEC.Substring(0, 5), LocalidadeLOEC.Substring(5, LocalidadeLOEC.Length - 5)) : LocalidadeLOEC;
                                LocalidadeLOEC = LocalidadeLOEC.Replace("- /", "").Trim();
                                EscreveTextoTextBox("Localidade: " + LocalidadeLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Localidade: " + LocalidadeLOEC.ToString()));

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
                                if (ds.Tables[0].Rows[0]["NomeCliente"] == null ||
                                ds.Tables[0].Rows[0]["NomeCliente"] == DBNull.Value ||
                                ds.Tables[0].Rows[0]["NomeCliente"].ToString() == "")
                                {
                                    //grava o nomecliente
                                    Mensagens.InformaDesenvolvedor(string.Format("Gravando no banco detalhes objeto SaiuParaEntrega"));
                                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET UnidadeLOEC = @UnidadeLOEC, MunicipioLOEC = @MunicipioLOEC, CriacaoLOEC = @CriacaoLOEC, CarteiroLOEC = @CarteiroLOEC, DistritoLOEC = @DistritoLOEC, NumeroLOEC = @NumeroLOEC, NomeCliente = @NomeCliente, EnderecoLOEC = @EnderecoLOEC, BairroLOEC = @BairroLOEC, LocalidadeLOEC = @LocalidadeLOEC WHERE CodigoObjeto = @CodigoObjeto ", new List<Parametros>(){ 
															new Parametros("@UnidadeLOEC", TipoCampo.Text, UnidadeLOEC),
															new Parametros("@MunicipioLOEC", TipoCampo.Text, MunicipioLOEC),
															new Parametros("@CriacaoLOEC", TipoCampo.Text, CriacaoLOEC),
															new Parametros("@CarteiroLOEC", TipoCampo.Text, CarteiroLOEC),
															new Parametros("@DistritoLOEC", TipoCampo.Text, DistritoLOEC),
															new Parametros("@NumeroLOEC", TipoCampo.Text, NumeroLOEC),
															new Parametros("@NomeCliente", TipoCampo.Text, ClienteLOEC),
															new Parametros("@EnderecoLOEC", TipoCampo.Text, EnderecoLOEC),
															new Parametros("@BairroLOEC", TipoCampo.Text, BairroLOEC),
															new Parametros("@LocalidadeLOEC", TipoCampo.Text, LocalidadeLOEC),
															new Parametros("@CodigoObjeto", TipoCampo.Text, CodigoObjetoAtual)
											});
                                }
                                if (ds.Tables[0].Rows[0]["NomeCliente"].ToString() != "")
                                {
                                    //já tem algo e não grava o nome
                                    Mensagens.InformaDesenvolvedor(string.Format("Gravando no banco detalhes objeto SaiuParaEntrega"));
                                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET UnidadeLOEC = @UnidadeLOEC, MunicipioLOEC = @MunicipioLOEC, CriacaoLOEC = @CriacaoLOEC, CarteiroLOEC = @CarteiroLOEC, DistritoLOEC = @DistritoLOEC, NumeroLOEC = @NumeroLOEC, EnderecoLOEC = @EnderecoLOEC, BairroLOEC = @BairroLOEC, LocalidadeLOEC = @LocalidadeLOEC WHERE CodigoObjeto = @CodigoObjeto ", new List<Parametros>(){ 
															new Parametros("@UnidadeLOEC", TipoCampo.Text, UnidadeLOEC),
															new Parametros("@MunicipioLOEC", TipoCampo.Text, MunicipioLOEC),
															new Parametros("@CriacaoLOEC", TipoCampo.Text, CriacaoLOEC),
															new Parametros("@CarteiroLOEC", TipoCampo.Text, CarteiroLOEC),
															new Parametros("@DistritoLOEC", TipoCampo.Text, DistritoLOEC),
															new Parametros("@NumeroLOEC", TipoCampo.Text, NumeroLOEC),
															//new Parametros("@ClienteLOEC", TipoCampo.Text, ClienteLOEC),
															new Parametros("@EnderecoLOEC", TipoCampo.Text, EnderecoLOEC),
															new Parametros("@BairroLOEC", TipoCampo.Text, BairroLOEC),
															new Parametros("@LocalidadeLOEC", TipoCampo.Text, LocalidadeLOEC),
															new Parametros("@CodigoObjeto", TipoCampo.Text, CodigoObjetoAtual)
											});
                                }
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
            bool ExisteCampoParaSaiuParaEntrega = false;
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

                    #region "Saiu para entrega ao destinatário
                    if (CampoAtual.ToUpper().Contains("Saiu para entrega ao destinatário".ToUpper()))
                    {
                        //Saiu para entrega
                        foreach (var pegalinkAtual2 in ((System.Windows.Forms.HtmlElement)(itemTR)).GetElementsByTagName("A"))
                        {
                            ExisteCampoParaSaiuParaEntrega = true;
                            var linkatual = ((System.Windows.Forms.HtmlElement)(pegalinkAtual2)).OuterHtml.Replace("%20", " ");
                            ListaLinksJavaScript.Add(linkatual.Substring(21, 104));
                            return;
                        }
                        continue;
                    }
                    #endregion
                }
            }
            if (ExisteCampoParaSaiuParaEntrega == false) this.Close();
        }

        private StringBuilder textoTextBox = new StringBuilder();
        private void EscreveTextoTextBox(string mensagem)
        {
            textoTextBox.AppendLine(mensagem);
            textBox1.Text = textoTextBox.ToString();
            textBox1.ScrollToCaret();
        }

        private void FormularioAtualizacaoObjetosSaiuParaEntrega_KeyDown(object sender, KeyEventArgs e)
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