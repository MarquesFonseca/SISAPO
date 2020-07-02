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
    public partial class FormularioAtualizacaoObjetosPostados : Form
    {
        public bool abortarAtualizacao = false;
        private string CodigoObjetoAtual = string.Empty;
        public List<string> ListaLinksJavaScript = new List<string>();
        private bool UltimoElemento = false;
        private enum TipoTela { Rastreamento1, ListaObjetos2, DetalhesDeObjetos3, DadosPostado }

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

        public FormularioAtualizacaoObjetosPostados(string codigoObjetoIniciado)
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

        private void FormularioAtualizacaoObjetosPostados_Load(object sender, EventArgs e)
        {
            textBox1.Visible = Configuracoes.ExibicaoMensagensParaDesenvolvedor;
            panel1.Visible = Configuracoes.ExibicaoMensagensParaDesenvolvedor;

            for (int i = 0; i < 5; i++) SendKeys.Send("{TAB}");
        }

        public static FormularioAtualizacaoObjetosPostados RetornaComponentes()
        {
            FormularioAtualizacaoObjetosPostados formularioAtualizacaoObjetosPostados;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioAtualizacaoObjetosPostados")
                {
                    formularioAtualizacaoObjetosPostados = (FormularioAtualizacaoObjetosPostados)item;
                    return (FormularioAtualizacaoObjetosPostados)item;
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
                            tipoTela = TipoTela.DadosPostado;
                            DetalhesDeObjetos3 = true;
                            #region TipoAmbiente.Producao
                            if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                            {
                                EscreveTextoTextBox("antes de clicar...");
                                Mensagens.InformaDesenvolvedor("link para clicar...: " + PrimeiroListaLinksJavaScript);
                                webBrowser1.Document.InvokeScript("DetalhesPesquisa", new object[] {
                                PrimeiroListaLinksJavaScript.Split('\'')[1], /*DETALHESINTRA*/
                                PrimeiroListaLinksJavaScript.Split('\'')[3], /*OA899613034BR*/ 
                                PrimeiroListaLinksJavaScript.Split('\'')[5], /*LDI*/
                                PrimeiroListaLinksJavaScript.Split('\'')[7], /*1094248527*/ 
                                PrimeiroListaLinksJavaScript.Split('\'')[9], /*1*/ 
                                PrimeiroListaLinksJavaScript.Split('\'')[11],/*31/08/2018 10:34:00*/ 
                                PrimeiroListaLinksJavaScript.Split('\'')[13] /*intra*/ });
                            }
                            #endregion
                            #region TipoAmbiente.Desenvolvimento
                            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                            {
                                //webBrowser1.Url = new Uri(TelaNomeCliente_2_4);
                                webBrowser1.Url = new Uri(@"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Rastreamento 05062019\OH239391285BR_POSTADO.htm");
                            }
                            #endregion
                            break;
                        }
                        break;
                    #endregion

                    #region TipoTela.DadosPostado
                    case TipoTela.DadosPostado:
                        EscreveTextoTextBox("Pegando os dados de postado...");
                        Mensagens.InformaDesenvolvedor("Abrindo a detalhes objeto postado");
                        string UnidadePostagem = string.Empty;
                        string MunicipioPostagem = string.Empty;
                        string CriacaoPostagem = string.Empty;
                        string CepDestinoPostagem = string.Empty;
                        string NomeClientePostagem = string.Empty;
                        string ARPostagem = string.Empty;
                        string MPPostagem = string.Empty;
                        string DataMaxPrevistaEntregaPostagem = string.Empty;

                        #region Capturando os dados
                        for (int i = 0; i < webBrowser1.Document.GetElementsByTagName("TR").Count; i++)
                        {
                            if (string.IsNullOrEmpty(webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText)) continue;

                            string InnerTextPostado = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText;
                            if (InnerTextPostado.Contains("Unidade:"))
                            {
                                UnidadePostagem = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Unidade: ", "").ToUpper().Trim();
                                UnidadePostagem = UnidadePostagem.Length >= 8 ? string.Format("{0}-{1}", UnidadePostagem.Substring(0, 5), UnidadePostagem.Substring(5, UnidadePostagem.Length - 5)) : UnidadePostagem;
                                EscreveTextoTextBox("Unidade: " + UnidadePostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> Unidade: " + UnidadePostagem.ToString()));
                                continue;
                            }
                            else if (InnerTextPostado.Contains("Município:"))
                            {
                                MunicipioPostagem = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Município: ", "").ToUpper().Trim();
                                MunicipioPostagem = MunicipioPostagem == "/" ? "" : MunicipioPostagem;
                                EscreveTextoTextBox("Município: " + MunicipioPostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> Município: " + MunicipioPostagem.ToString()));
                                continue;
                            }
                            else if (InnerTextPostado.Contains("Criado em:"))
                            {
                                CriacaoPostagem = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Criado em: ", "").ToUpper().Trim();
                                EscreveTextoTextBox("Criado em: " + CriacaoPostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> Criado em: " + CriacaoPostagem.ToString()));
                                continue;
                            }
                            else if (InnerTextPostado.Contains("CEP Destino:"))
                            {
                                string temp = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("CEP Destino: ", "").ToUpper().Trim();
                                CepDestinoPostagem = temp.Split(':')[0].Substring(0, (temp.Length >= 8 ? 8 : temp.Length));
                                CepDestinoPostagem = CepDestinoPostagem.Length >= 8 ? string.Format("{0}-{1}", CepDestinoPostagem.Substring(0, 5), CepDestinoPostagem.Substring(5, CepDestinoPostagem.Length - 5)) : CepDestinoPostagem;
                                EscreveTextoTextBox("CEP Destino: " + CepDestinoPostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> CEP Destino: " + CepDestinoPostagem.ToString()));
                                NomeClientePostagem = temp.Split(':')[1].TrimStart().TrimEnd();
                                EscreveTextoTextBox("Destinatário: " + NomeClientePostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> Destinatário: " + NomeClientePostagem.ToString()));
                                continue;
                            }
                            else if (InnerTextPostado.Contains("AR:"))
                            {
                                string temp = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("AR: ", "").ToUpper().Trim();
                                ARPostagem = temp.Split(':')[0].Substring(0, (temp.Length >= 1 ? 1 : temp.Length));
                                if (string.IsNullOrEmpty(ARPostagem)) ARPostagem = "";
                                if (ARPostagem == "M") ARPostagem = "";
                                if (ARPostagem == "S") ARPostagem = "SIM";
                                if (ARPostagem == "N") ARPostagem = "NÃO";
                                EscreveTextoTextBox("AR: " + ARPostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> AR: " + ARPostagem.ToString()));

                                MPPostagem = temp.Split(':')[1].TrimStart().TrimEnd();
                                if (string.IsNullOrEmpty(MPPostagem)) MPPostagem = "";
                                if (MPPostagem == "S") MPPostagem = "SIM";
                                if (MPPostagem == "N") MPPostagem = "NÃO";
                                EscreveTextoTextBox("MP: " + MPPostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> MP: " + MPPostagem.ToString()));

                                continue;
                            }
                            else if (InnerTextPostado.Contains("Data Máx Prev Entrega:"))
                            {
                                DataMaxPrevistaEntregaPostagem = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Data Máx Prev Entrega: ", "").ToUpper().Trim();
                                DataMaxPrevistaEntregaPostagem = DataMaxPrevistaEntregaPostagem.RemoveAcentos() == "DADO INDISPONIVEL" ? "" : DataMaxPrevistaEntregaPostagem.Trim().ToUpper();
                                //DataMaxPrevistaEntregaPostagem.ToDateTime().GetDateTimeFormats()[14];
                                //DateTime dataValida; //Verifica Se a data for valida
                                //DataMaxPrevistaEntregaPostagem = (DateTime.TryParse(DataMaxPrevistaEntregaPostagem, out dataValida)) ?
                                //    DataMaxPrevistaEntregaPostagem.ToDateTime().GetDateTimeFormats()[14].ToUpper() : DataMaxPrevistaEntregaPostagem;
                                EscreveTextoTextBox("Data Máx Prev Entrega: " + DataMaxPrevistaEntregaPostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Data Máx Prev Entrega: " + DataMaxPrevistaEntregaPostagem.ToString()));

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
                                    Mensagens.InformaDesenvolvedor(string.Format("Gravando no banco detalhes objeto postado"));
                                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET NomeCliente = @NomeCliente, UnidadePostagem = @UnidadePostagem, MunicipioPostagem = @MunicipioPostagem, CriacaoPostagem = @CriacaoPostagem, CepDestinoPostagem = @CepDestinoPostagem, ARPostagem = @ARPostagem, MPPostagem = @MPPostagem, DataMaxPrevistaEntregaPostagem = @DataMaxPrevistaEntregaPostagem WHERE CodigoObjeto = @CodigoObjeto ", new List<Parametros>(){
                                                            new Parametros("@NomeCliente", TipoCampo.Text, NomeClientePostagem.ToUpper().RemoveAcentos()),
                                                            new Parametros("@UnidadePostagem", TipoCampo.Text, UnidadePostagem),
                                                            new Parametros("@MunicipioPostagem", TipoCampo.Text, MunicipioPostagem),
                                                            new Parametros("@CriacaoPostagem", TipoCampo.Text, CriacaoPostagem),
                                                            new Parametros("@CepDestinoPostagem", TipoCampo.Text, CepDestinoPostagem),
                                                            new Parametros("@ARPostagem", TipoCampo.Text, ARPostagem),
                                                            new Parametros("@MPPostagem", TipoCampo.Text, MPPostagem),
                                                            new Parametros("@DataMaxPrevistaEntregaPostagem", TipoCampo.Text, DataMaxPrevistaEntregaPostagem),
                                                            new Parametros("@CodigoObjeto", TipoCampo.Text, CodigoObjetoAtual)
                                            });
                                }
                                if (ds.Tables[0].Rows[0]["NomeCliente"].ToString() != "")
                                {
                                    //já tem algo e não grava o nome
                                    Mensagens.InformaDesenvolvedor(string.Format("Gravando no banco detalhes objeto postado"));
                                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET UnidadePostagem = @UnidadePostagem, MunicipioPostagem = @MunicipioPostagem, CriacaoPostagem = @CriacaoPostagem, CepDestinoPostagem = @CepDestinoPostagem, ARPostagem = @ARPostagem, MPPostagem = @MPPostagem, DataMaxPrevistaEntregaPostagem = @DataMaxPrevistaEntregaPostagem WHERE CodigoObjeto = @CodigoObjeto ", new List<Parametros>(){ 
															//new Parametros("@NomeCliente", TipoCampo.Text, NomeClientePostagem.ToUpper()),
															new Parametros("@UnidadePostagem", TipoCampo.Text, UnidadePostagem),
                                                            new Parametros("@MunicipioPostagem", TipoCampo.Text, MunicipioPostagem),
                                                            new Parametros("@CriacaoPostagem", TipoCampo.Text, CriacaoPostagem),
                                                            new Parametros("@CepDestinoPostagem", TipoCampo.Text, CepDestinoPostagem),
                                                            new Parametros("@ARPostagem", TipoCampo.Text, ARPostagem),
                                                            new Parametros("@MPPostagem", TipoCampo.Text, MPPostagem),
                                                            new Parametros("@DataMaxPrevistaEntregaPostagem", TipoCampo.Text, DataMaxPrevistaEntregaPostagem),
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
                                    for (int i = 0; i < 7; i++) SendKeys.Send("{TAB}");
                                    SendKeys.Send("{ENTER}");
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
            Mensagens.InformaDesenvolvedor("Iniciando o método: SeparaLinksDosObjetosRastreados()");
            bool ExisteCampoParaPostado = false;
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

                    #region pega os "Postado"
                    if (CampoAtual.ToUpper().Contains("Postado".ToUpper()) ||
                        CampoAtual.ToUpper().Contains("Postado após o horário".ToUpper()) ||
                        CampoAtual.ToUpper().Contains("Postado depois do horário".ToUpper()) ||
                        CampoAtual.ToUpper().Contains("Postagem - DH".ToUpper()))
                    {
                        //Postados
                        foreach (var pegalinkAtual2 in ((System.Windows.Forms.HtmlElement)(itemTR)).GetElementsByTagName("A"))
                        {
                            ExisteCampoParaPostado = true;
                            var linkatual = ((System.Windows.Forms.HtmlElement)(pegalinkAtual2)).OuterHtml.Replace("%20", " ");
                            ListaLinksJavaScript.Add(linkatual.Substring(21, 101));
                            Mensagens.InformaDesenvolvedor("Peguei o link: " + linkatual.Substring(21, 101));
                            return;
                        }
                    }
                    #endregion
                }
            }
            Mensagens.InformaDesenvolvedor("Saindo do método 'SeparaLinksDosObjetosRastreados()'");
            if (ExisteCampoParaPostado == false) this.Close();
        }

        private StringBuilder textoTextBox = new StringBuilder();
        private void EscreveTextoTextBox(string mensagem)
        {
            textoTextBox.AppendLine(mensagem);
            textBox1.Text = textoTextBox.ToString();
            textBox1.ScrollToCaret();
        }

        private void FormularioAtualizacaoObjetos_KeyDown(object sender, KeyEventArgs e)
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