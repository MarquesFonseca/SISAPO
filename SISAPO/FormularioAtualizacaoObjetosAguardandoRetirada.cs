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
    public partial class FormularioAtualizacaoObjetosAguardandoRetirada : Form
    {
        public bool abortarAtualizacao = false;
        private static StringBuilder textoConsulta = new StringBuilder();
        private string CodigoObjetoAtual = string.Empty;
        public List<string> ListaLinksJavaScript = new List<string>();
        private bool UltimoElemento = false;
        private enum TipoTela { Rastreamento1, ListaObjetos2, DetalhesDeObjetos3, NomeCliente4 }


        private DataSet dadosAgencia = new DataSet();

        private TipoTela tipoTela = TipoTela.Rastreamento1;
        private bool DetalhesDeObjetos3 = false;
        //private string TelaRastreamento_1_1 = @"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Nova_Rastreamento_Problema\RastreamantoDetalhes-1-1-problema.htm";
        private string TelaRastreamento_1_1 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\1-1-TelaRastreamento.htm";
        //private string TelaDetalhesDeObjetos_1_3 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\1-3-TelaDetalhesDeObjetos_defeito.htm";
        //private string TelaNomeCliente_1_4 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\1-4-TelaNomeCliente.htm";
        //private string TelaRastreamento_2_1 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\2-1-TelaRastreamento.htm";
        //private string TelaListaDeObjetos_2_2 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\2-2-TelaListaDeObjetos.htm";
        private string TelaDetalhesDeObjetos_2_3 = @"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Rastreamento\05062019\OH239391285BR_DETALHES.htm";
        //private string TelaNomeCliente_2_4 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\2-4-TelaNomeCliente.htm";
        private string TelaNomeCliente_2_4 = @"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\Rastreamento_Unificado_htm.htm";

        private string enderecoSRO = @"http://websro2/rastreamento/sro?opcao=PESQUISA&objetos=";

        public FormularioAtualizacaoObjetosAguardandoRetirada(string codigoObjetoIniciado)
        {
            InitializeComponent();
            CodigoObjetoAtual = codigoObjetoIniciado;
            textoConsulta = new StringBuilder();
            ListaLinksJavaScript = new List<string>();
            UltimoElemento = false;

            DetalhesDeObjetos3 = false;
            dadosAgencia = RetornaDadosAgencia();

            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
            {
                // caminho que define o teste
                //caminho para tela de consulta atraves de códigos de objetos rastreadores
                //tipoTela = TipoTela.Rastreamento1;
                //webBrowser1.Url = new Uri(TelaRastreamento_1_1);
                CodigoObjetoAtual = "QB137892122BR";
                //string TelaNomeCliente_2_4 = @"J:\Rastreamento_Unificado_SC349045411BR.htm";
                string TelaNomeCliente_2_4 = @"J:\Rastreamento_Unificado_S_QB137892122BR.htm";
                //"J:\Rastreamento_Unificado_SC349045411BR.htm";
                tipoTela = TipoTela.NomeCliente4;
                webBrowser1.Url = new Uri(TelaNomeCliente_2_4);
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

        private void FormularioAtualizacaoObjetosAguardandoRetirada_Load(object sender, EventArgs e)
        {
            textBox1.Visible = Configuracoes.ExibicaoMensagensParaDesenvolvedor;
            panel1.Visible = Configuracoes.ExibicaoMensagensParaDesenvolvedor;

            for (int i = 0; i < 5; i++) SendKeys.Send("{TAB}");
        }

        public static FormularioAtualizacaoObjetosAguardandoRetirada RetornaComponentes()
        {
            FormularioAtualizacaoObjetosAguardandoRetirada formularioAtualizacaoObjetosAguardandoRetirada;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioAtualizacaoObjetosAguardandoRetirada")
                {
                    formularioAtualizacaoObjetosAguardandoRetirada = (FormularioAtualizacaoObjetosAguardandoRetirada)item;
                    return (FormularioAtualizacaoObjetosAguardandoRetirada)item;
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
                            //CodigoObjetoAtual = PrimeiroListaLinksJavaScript.Substring(34, 13);
                            ListaLinksJavaScript.Remove(ListaLinksJavaScript[0]);
                            tipoTela = TipoTela.NomeCliente4;
                            DetalhesDeObjetos3 = true;
                            #region TipoAmbiente.Producao
                            if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                            {
                                EscreveTextoTextBox("antes de clicar...");
                                Mensagens.InformaDesenvolvedor("Antes de clicar no link do aguardando retirada... \nwebBrowser1.Document.InvokeScript(\"DetalhesPesquisa\"....");
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
                                webBrowser1.Url = new Uri(TelaNomeCliente_2_4);
                            }
                            #endregion
                            break;
                        }
                        break;
                    #endregion

                    #region TipoTela.NomeCliente4
                    case TipoTela.NomeCliente4:
                        EscreveTextoTextBox("Pegando o nome...");
                        Mensagens.InformaDesenvolvedor("Chegou na tela de capura do nome.... oba");
                        //return;

                        string NomeCliente = string.Empty;
                        string Ldi = string.Empty;
                        string Comentario = string.Empty;
                        string DataLancamento = string.Empty;
                        string EnderecoLOEC = string.Empty;
                        string BairroLOEC = string.Empty;
                        string LocalidadeLOECCEP = string.Empty;
                        string MunicipioLOEC = string.Empty;
                        bool SeEAoRemetente = false;
                        bool SeECaixaPostal = false;
                        //referente ao Tipo Postal                        
                        string TipoPostalServico = string.Empty;
                        string TipoPostalSiglaCodigo = string.Empty;
                        string TipoPostalNomeSiglaCodigo = string.Empty;
                        string TipoPostalPrazoDiasCorridosRegulamentado = string.Empty;

                        #region For BuscaHTMLs
                        for (int i = 0; i < webBrowser1.Document.GetElementsByTagName("TR").Count; i++)
                        {
                            if (string.IsNullOrEmpty(webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText)) continue;

                            string teste = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText;
                            #region Criado em
                            if (webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Contains("Criado em:"))
                            {
                                DataLancamento = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Criado em: ", "");
                                EscreveTextoTextBox("Criado em: " + DataLancamento.ToString());
                                continue;
                            }
                            #endregion
                            #region LDI
                            if (webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Contains("LDI:"))
                            {
                                Ldi = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("LDI: ", "");
                                if (!string.IsNullOrEmpty(Ldi))
                                {
                                    if (Ldi.Contains("-"))
                                    {
                                        Ldi = Ldi.Split('-')[0].Trim();
                                    }
                                }
                                EscreveTextoTextBox("Ldi: " + Ldi.ToString());
                                continue;
                            }
                            #endregion
                            #region Comentário
                            if (webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Contains("Comentário:"))
                            {
                                Comentario = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Comentário: ", "");
                                Comentario = Comentario.RemoveAcentos().ToUpper().Trim();
                                Comentario = Configuracoes.RetornaCaixaPostalCorrigidaDefeitoString(Comentario);
                                if (string.IsNullOrEmpty(Comentario))
                                {
                                    DataRow drTipoPostal = FormularioPrincipal.TiposPostais.AsEnumerable().First(T => T["Sigla"].Equals(CodigoObjetoAtual.Substring(0, 2))); //["Código"] - Pega linha retornada dos tipos postais vinda do Excel
                                    string descricao = drTipoPostal["Descricao"].ToString().ToUpper().RemoveAcentos();
                                    if (descricao.Contains("A COBRAR") ||
                                        descricao.Contains("PAGAMENTO NA ENTREGA") ||
                                        descricao.Contains("PAGAMENTO ENTREGA"))
                                    {
                                        Comentario = "A COBRAR";
                                    }
                                }

                                EscreveTextoTextBox("Comentário: " + Comentario.ToString());
                                continue;
                            }
                            #endregion
                            #region Cliente
                            if (webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Contains("Cliente:"))
                            {
                                NomeCliente = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Cliente: ", "");
                                NomeCliente = NomeCliente.RemoveAcentos().ToUpper().Trim();
                                EscreveTextoTextBox(NomeCliente.ToString());
                                continue;
                            }
                            #endregion
                            #region Endereço
                            if (webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Contains("Endereço:"))
                            {
                                EnderecoLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Endereço: ", "");
                                EnderecoLOEC = EnderecoLOEC.RemoveAcentos().ToUpper().Trim();
                                EscreveTextoTextBox(EnderecoLOEC.ToString());
                                continue;
                            }
                            #endregion
                            #region Bairro
                            if (webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Contains("Bairro:"))
                            {
                                BairroLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Bairro: ", "");
                                BairroLOEC = BairroLOEC.RemoveAcentos().ToUpper().Trim();
                                EscreveTextoTextBox(BairroLOEC.ToString());
                                continue;
                            }
                            #endregion
                            #region Localidade
                            if (webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Contains("Localidade"))
                            {
                                string LocalidadeRetornada = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Localidade ", "");

                                if (!string.IsNullOrEmpty(LocalidadeRetornada))
                                {
                                    if (LocalidadeRetornada.Contains("-"))
                                    {
                                        LocalidadeLOECCEP = LocalidadeRetornada.Split('-')[0].Trim();
                                        MunicipioLOEC = LocalidadeRetornada.Split('-')[1].Trim();
                                    }
                                }
                                EscreveTextoTextBox(LocalidadeLOECCEP.ToString());
                                break;//por ser o ultimo campo a ser buscado, não tem necessidade de ficar rodando mais.
                            }
                            #endregion
                        }
                        #endregion                        

                        #region grava no banco de dados                             
                        using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                        {
                            if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                            DataSet DsCliente = dao.RetornaDataSet("SELECT TOP 1 Codigo, CodigoObjeto, CodigoLdi, NomeCliente, DataLancamento, Atualizado, ObjetoEntregue, CaixaPostal, MunicipioLOEC, EnderecoLOEC, BairroLOEC, LocalidadeLOEC, Comentario, TipoPostalServico, TipoPostalSiglaCodigo, TipoPostalNomeSiglaCodigo, TipoPostalPrazoDiasCorridosRegulamentado FROM TabelaObjetosSROLocal WHERE (CodigoObjeto = @CodigoObjeto) ORDER BY Codigo DESC", new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual });
                            if (DsCliente.Tables[0].Rows.Count == 0) break;

                            //NomeCliente = DsCliente.Tables[0].Rows[0]["NomeCliente"].ToString().ToUpper().RemoveAcentos();
                            NomeCliente = NomeCliente.Trim() == "" ? DsCliente.Tables[0].Rows[0]["NomeCliente"].ToString().ToUpper().RemoveAcentos() : NomeCliente.Trim().ToUpper().RemoveAcentos();
                            NomeCliente = NomeCliente.Replace("- " + Comentario, "").Trim();//evita repetir o mesmo comentario varias vezes
                            NomeCliente = string.Format("{0} - {1}", NomeCliente, Comentario);

                            EnderecoLOEC = string.IsNullOrEmpty(EnderecoLOEC) ? DsCliente.Tables[0].Rows[0]["EnderecoLOEC"].ToString().RemoveAcentos().ToUpper() : EnderecoLOEC;
                            BairroLOEC = string.IsNullOrEmpty(BairroLOEC) ? DsCliente.Tables[0].Rows[0]["BairroLOEC"].ToString().RemoveAcentos().ToUpper() : BairroLOEC;
                            LocalidadeLOECCEP = string.IsNullOrEmpty(LocalidadeLOECCEP) ? DsCliente.Tables[0].Rows[0]["LocalidadeLOEC"].ToString().RemoveAcentos().ToUpper() : LocalidadeLOECCEP;
                            MunicipioLOEC = (string.IsNullOrEmpty(MunicipioLOEC) || MunicipioLOEC == "/") ? DsCliente.Tables[0].Rows[0]["MunicipioLOEC"].ToString().RemoveAcentos().ToUpper() : MunicipioLOEC;

                            SeECaixaPostal = Convert.ToBoolean(DsCliente.Tables[0].Rows[0]["CaixaPostal"]);
                            SeECaixaPostal = !SeECaixaPostal ? Configuracoes.RetornaSeECaixaPostal(NomeCliente) : SeECaixaPostal;
                            
                            SeEAoRemetente = Configuracoes.RetornaSeEAoRemetente(NomeCliente);
                            
                            TipoPostalPrazoDiasCorridosRegulamentado = Configuracoes.RetornaTipoPostalPrazoDiasCorridosRegulamentado(CodigoObjetoAtual, SeEAoRemetente, SeECaixaPostal, ref TipoPostalServico, ref TipoPostalSiglaCodigo, ref TipoPostalNomeSiglaCodigo);

                            Mensagens.InformaDesenvolvedor("Cheguei até a gravação do update do nome: " + NomeCliente);
                            dao.ExecutaSQL(@"UPDATE TabelaObjetosSROLocal SET 
                                            CodigoLdi = @CodigoLdi, 
                                            NomeCliente = @NomeCliente, 
                                            DataLancamento = @DataLancamento,
                                            Atualizado = @Atualizado, 
                                            CaixaPostal = @CaixaPostal,
                                            Comentario = @Comentario, 
                                            EnderecoLOEC = @EnderecoLOEC,
                                            BairroLOEC = @BairroLOEC,
                                            LocalidadeLOEC = @LocalidadeLOEC,                                            
                                            MunicipioLOEC = @MunicipioLOEC,
                                            TipoPostalServico = @TipoPostalServico, 
                                            TipoPostalSiglaCodigo = @TipoPostalSiglaCodigo, 
                                            TipoPostalNomeSiglaCodigo = @TipoPostalNomeSiglaCodigo, 
                                            TipoPostalPrazoDiasCorridosRegulamentado = @TipoPostalPrazoDiasCorridosRegulamentado 
                                            WHERE CodigoObjeto = @CodigoObjeto ", new List<Parametros>(){
                                            new Parametros("@CodigoLdi", TipoCampo.Text, Ldi),
                                            new Parametros("@NomeCliente", TipoCampo.Text, NomeCliente),
                                            new Parametros("@DataLancamento", TipoCampo.Text, DataLancamento),
                                            new Parametros("@Atualizado", TipoCampo.Boolean, true),
                                            new Parametros("@CaixaPostal", TipoCampo.Boolean, SeECaixaPostal),
                                            new Parametros("@Comentario", TipoCampo.Text, Comentario),
                                            new Parametros("@EnderecoLOEC", TipoCampo.Text, EnderecoLOEC),
                                            new Parametros("@BairroLOEC", TipoCampo.Text, BairroLOEC),
                                            new Parametros("@LocalidadeLOEC", TipoCampo.Text, LocalidadeLOECCEP),
                                            new Parametros("@MunicipioLOEC", TipoCampo.Text, MunicipioLOEC),
                                            new Parametros("@TipoPostalServico", TipoCampo.Text, TipoPostalServico),
                                            new Parametros("@TipoPostalSiglaCodigo", TipoCampo.Text, TipoPostalSiglaCodigo),
                                            new Parametros("@TipoPostalNomeSiglaCodigo", TipoCampo.Text, TipoPostalNomeSiglaCodigo),
                                            new Parametros("@TipoPostalPrazoDiasCorridosRegulamentado", TipoCampo.Text, TipoPostalPrazoDiasCorridosRegulamentado),

                                            new Parametros("@CodigoObjeto", TipoCampo.Text, CodigoObjetoAtual)});

                            this.Close();
                            break;
                        }
                        #endregion

                        if (DetalhesDeObjetos3)
                        {
                            if (ListaLinksJavaScript.Count == 0)
                            {
                                DetalhesDeObjetos3 = false;
                                Mensagens.InformaDesenvolvedor("Vou fechar... Já peguei os dados da tela nome... ");
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

        private StringBuilder textoTextBox = new StringBuilder();
        private void EscreveTextoTextBox(string mensagem)
        {
            textoTextBox.AppendLine(mensagem);
            textBox1.Text = textoTextBox.ToString();
            textBox1.ScrollToCaret();
        }


        private void SeparaLinksDosObjetosRastreados()
        {
            bool objetoJaEntregue = false;
            ListaLinksJavaScript = new List<string>();

            var itensss = webBrowser1.Document.All.Cast<HtmlElement>().AsEnumerable();
            var linhas = itensss.Cast<HtmlElement>().AsEnumerable().Where(x => x.TagName.ToUpper() == "TR");
            foreach (HtmlElement item in linhas)
            {
                if (item.InnerText == null || string.IsNullOrEmpty(item.InnerText.Trim())) continue;
                string LinhaAtual = item.InnerText.Trim().ToUpper().RemoveAcentos();
                if (!VerificaSeELinhaDesejada(LinhaAtual)) continue;

                #region "Entregue"
                if (LinhaAtual.Contains("Entregue".ToUpper()))
                {
                    objetoJaEntregue = true;
                    Mensagens.InformaDesenvolvedor("Gravando no banco: " + "Entregue".ToUpper());
                    string DataModificacao = LinhaAtual.Trim().Substring(0, 19);
                    #region UPDATE TabelaObjetosSROLocal
                    using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                    {
                        if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET DataModificacao = @DataModificacao, Situacao = @Situacao, ObjetoEntregue = @ObjetoEntregue WHERE (CodigoObjeto = @CodigoObjeto)"
                            , new List<Parametros>() {
                                new Parametros { Nome = "@DataModificacao", Tipo = TipoCampo.Text, Valor = DataModificacao },
                                new Parametros { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = "Entregue".ToUpper() },
                                new Parametros { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Int, Valor = 1 },
                                new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
                            });
                    }
                    #endregion
                    continue;
                }
                #endregion

                #region "Distribuído ao remetente"
                if (LinhaAtual.Contains("Distribuido ao remetente".RemoveAcentos().ToUpper()))
                {
                    objetoJaEntregue = true;
                    Mensagens.InformaDesenvolvedor("Gravando no banco: " + "Distribuído ao remetente".ToUpper());
                    string DataModificacao = LinhaAtual.Trim().Substring(0, 19);
                    #region UPDATE TabelaObjetosSROLocal
                    using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                    {
                        if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET DataModificacao = @DataModificacao, Situacao = @Situacao, ObjetoEntregue = @ObjetoEntregue WHERE (CodigoObjeto = @CodigoObjeto)"
                            , new List<Parametros>() {
                                new Parametros { Nome = "@DataModificacao", Tipo = TipoCampo.Text, Valor = DataModificacao },
                                new Parametros { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = "Distribuido ao remetente".ToUpper() },
                                new Parametros { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Int, Valor = 1 },
                                new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
                            });
                    }
                    #endregion
                    continue;
                }
                #endregion

                //daqui para baixo sai imediatamente break
                #region "Disponível em Caixa Postal"
                if (LinhaAtual.Contains("Disponivel em Caixa Postal".RemoveAcentos().ToUpper()) ||
                    LinhaAtual.Contains("Aguardando retirada em Caixa Postal".RemoveAcentos().ToUpper()))
                {
                    var linkAtual = ((System.Windows.Forms.HtmlElement)(item)).GetElementsByTagName("A")[0].OuterHtml.Replace("%20", " ");
                    ListaLinksJavaScript.Add(linkAtual.Substring(21, 102));

                    #region UPDATE TabelaObjetosSROLocal
                    using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                    {
                        if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                        //Seta como CaixaPostal
                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET CaixaPostal = @CaixaPostal WHERE (CodigoObjeto = @CodigoObjeto)"
                                 , new List<Parametros>() {
                                         new Parametros { Nome = "@CaixaPostal", Tipo = TipoCampo.Int, Valor = 1 },
                                         new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
                                     });

                        if (objetoJaEntregue == false) //não foi entregue coloca a situação "Disponível em Caixa Postal"
                        {
                            dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Situacao = @Situacao WHERE (CodigoObjeto = @CodigoObjeto)"
                                     , new List<Parametros>() {
                                         new Parametros { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = "Disponível em Caixa Postal".ToUpper() },
                                         new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
                                     });
                        }
                    }
                    #endregion
                    //continue;
                    break;
                }
                #endregion

                #region "Aguardando retirada"
                if (LinhaAtual.Contains("Aguardando retirada".RemoveAcentos().ToUpper()))
                {
                    var linkAtual = ((System.Windows.Forms.HtmlElement)(item)).GetElementsByTagName("A")[0].OuterHtml.Replace("%20", " ");
                    ListaLinksJavaScript.Add(linkAtual.Substring(21, 102));

                    #region UPDATE TabelaObjetosSROLocal
                    if (objetoJaEntregue == false) //não foi entregue coloca a situação "Disponível em Caixa Postal"
                    {
                        using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                        {
                            if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                            dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Situacao = @Situacao WHERE (CodigoObjeto = @CodigoObjeto)"
                                     , new List<Parametros>() {
                                         new Parametros { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = "Aguardando retirada".ToUpper() },
                                         new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
                                     });
                        }
                    }
                    #endregion
                    //continue;
                    break;
                }
                #endregion

                #region "Aguardando retirada - Área sem entrega"
                if (LinhaAtual.Contains("Aguardando retirada - Area sem entrega".RemoveAcentos().ToUpper()))
                {
                    var linkAtual = ((System.Windows.Forms.HtmlElement)(item)).GetElementsByTagName("A")[0].OuterHtml.Replace("%20", " ");
                    ListaLinksJavaScript.Add(linkAtual.Substring(21, 102));

                    #region UPDATE TabelaObjetosSROLocal
                    if (objetoJaEntregue == false) //não foi entregue coloca a situação "Disponível em Caixa Postal"
                    {
                        using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                        {
                            if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                            dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Situacao = @Situacao WHERE (CodigoObjeto = @CodigoObjeto)"
                                     , new List<Parametros>() {
                                         new Parametros { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = "Aguardando retirada - Area sem entrega".ToUpper() },
                                         new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
                                     });

                        }
                    }
                    #endregion
                    //continue;
                    break;
                }
                #endregion
            }

            if (ListaLinksJavaScript == null || ListaLinksJavaScript.Count == 0)
            {
                DetalhesDeObjetos3 = false;
                Mensagens.InformaDesenvolvedor("Vou fechar... Não tem nunhum campo: Disponivel em Caixa Postal ou Aguardando Retirada");
                this.Close();
            }
        }


        //server 275
        //private void SeparaLinksDosObjetosRastreados()
        //{
        //    bool objetoJaEntregue = false;
        //    ListaLinksJavaScript = new List<string>();
        //    //string CodigoObjetoAtual = "0";
        //    foreach (var itemTR in webBrowser1.Document.GetElementsByTagName("TR"))
        //    {
        //        string itemTRInnerText = ((System.Windows.Forms.HtmlElement)(itemTR)).InnerText;
        //        string itemTRInnerHtml = ((System.Windows.Forms.HtmlElement)(itemTR)).InnerHtml;
        //        if (itemTRInnerText == null || string.IsNullOrEmpty(itemTRInnerText.Trim())) continue;
        //        if (!VerificaSeELinhaDesejada(itemTRInnerText)) continue;

        //        Mensagens.InformaDesenvolvedor("passou pela: '...verifica para ir mais rápido.... senao teria que percorrer todas as colunas até saber se é a linha desejada....'");
        //        Mensagens.InformaDesenvolvedor("itemTRInnerText: " + itemTRInnerText);

        //        foreach (var itemTD in ((System.Windows.Forms.HtmlElement)(itemTR)).GetElementsByTagName("TD"))
        //        {
        //            if (((System.Windows.Forms.HtmlElement)(itemTD)).InnerText == null) continue;
        //            string CampoAtual = ((System.Windows.Forms.HtmlElement)(itemTD)).InnerText.Trim().ToUpper();
        //            if (CampoAtual == null || string.IsNullOrEmpty(CampoAtual.Trim())) continue;

        //            if (CampoAtual == "Entregue".ToUpper() ||
        //                CampoAtual == "Distribuído ao remetente".ToUpper() ||
        //                CampoAtual == "Encaminhado".ToUpper() ||
        //                CampoAtual == "Disponível em Caixa Postal".ToUpper())
        //            {
        //                foreach (var pegalinkAtual in ((System.Windows.Forms.HtmlElement)(itemTR)).GetElementsByTagName("A"))
        //                {
        //                    //var linkatual = ((System.Windows.Forms.HtmlElement)(pegalinkAtual)).OuterHtml.Replace("%20", " ");
        //                    //CodigoObjetoAtual = linkatual.Substring(55, 13);

        //                    #region pega os "Entregue" | "Distribuído ao remetente" | "Encaminhado"
        //                    if (CampoAtual == "Entregue".ToUpper() ||
        //                        CampoAtual == "Distribuído ao remetente".ToUpper() ||
        //                        CampoAtual == "Encaminhado".ToUpper())
        //                    {
        //                        Mensagens.InformaDesenvolvedor("Gravando no banco : " + CampoAtual);
        //                        objetoJaEntregue = true;
        //                        string DataModificacao = itemTRInnerText.Trim().Substring(0, 19);
        //                        using (DAO dao = new DAO(TipoBanco.OleDb, strConexao))
        //                        {
        //                            if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
        //                            dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET DataModificacao = @DataModificacao, Situacao = @Situacao, ObjetoEntregue = @ObjetoEntregue WHERE (CodigoObjeto = @CodigoObjeto)"
        //                                                 , new List<Parametros>() { 
        //                                         new Parametros { Nome = "@DataModificacao", Tipo = TipoCampo.Text, Valor = DataModificacao },
        //                                         new Parametros { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = CampoAtual.ToUpper() },
        //                                         new Parametros { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Int, Valor = 1 },
        //                                         new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
        //                                     });
        //                        }
        //                        break;
        //                    }
        //                    #endregion

        //                    #region pega os "Disponível em Caixa Postal"
        //                    if (CampoAtual == "Disponível em Caixa Postal".ToUpper())
        //                    {
        //                        Mensagens.InformaDesenvolvedor("Gravando no banco: " + CampoAtual);
        //                        using (DAO dao = new DAO(TipoBanco.OleDb, strConexao))
        //                        {
        //                            if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
        //                            dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Situacao = @Situacao, CaixaPostal = @CaixaPostal WHERE (CodigoObjeto = @CodigoObjeto)"
        //                                     , new List<Parametros>() { 
        //                                         new Parametros { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = CampoAtual.Trim().ToUpper() },
        //                                         new Parametros { Nome = "@CaixaPostal", Tipo = TipoCampo.Int, Valor = 1 },
        //                                         new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
        //                                     });
        //                        }
        //                        break;
        //                    }
        //                    #endregion
        //                }
        //            }

        //            if (CampoAtual == "Aguardando retirada".ToUpper() || CampoAtual == "Aguardando retirada - Área sem entrega".ToUpper() || CampoAtual == "Aguardando retirada - Area sem entrega".ToUpper())
        //            {
        //                string NomeAgenciaLocal = dadosAgencia.Tables[0].Rows[0][0].ToString().Trim().ToUpper();
        //                if (itemTRInnerText.Trim().ToUpper().Contains(NomeAgenciaLocal.ToUpper()) == false)
        //                {
        //                    Mensagens.InformaDesenvolvedor(itemTRInnerText.Trim().ToUpper() + "\n\n(itemTRInnerText.Trim().ToUpper().Contains(NomeAgenciaLocal.ToUpper()) == false)");
        //                    continue;//se não estiver a referencia à agência não continua...	
        //                }
        //            }

        //            #region pega os "Aguardando retirada" ou "Disponível em Caixa Postal"
        //            if (CampoAtual == "Aguardando retirada".ToUpper() ||
        //                CampoAtual == "Aguardando retirada - Área sem entrega".ToUpper() ||
        //                CampoAtual == "Aguardando retirada - Area sem entrega".ToUpper() ||
        //                CampoAtual == "Disponível em Caixa Postal".ToUpper())
        //            {
        //                //Aguardando retirada ou Disponível em Caixa Postal
        //                foreach (var pegalinkAtual2 in ((System.Windows.Forms.HtmlElement)(itemTR)).GetElementsByTagName("A"))
        //                {
        //                    var linkatual = ((System.Windows.Forms.HtmlElement)(pegalinkAtual2)).OuterHtml.Replace("%20", " ");
        //                    ListaLinksJavaScript.Add(linkatual.Substring(21, 102));

        //                    if (objetoJaEntregue == false) //não foi entregue
        //                    {
        //                        using (DAO dao = new DAO(TipoBanco.OleDb, strConexao))
        //                        {
        //                            if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
        //                            dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Situacao = @Situacao WHERE (CodigoObjeto = @CodigoObjeto)"
        //                                     , new List<Parametros>() { 
        //                                 new Parametros { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = CampoAtual.Trim().ToUpper() },
        //                                 new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
        //                             });
        //                        }
        //                    }
        //                    return;
        //                }
        //            }
        //            #endregion

        //            //adicionado dia 07/06/2019
        //            #region pega os "Postado" ou "Postado após o horário"
        //            //if (CampoAtual == "Postado".ToUpper() || CampoAtual == "Postado após o horário".ToUpper())
        //            //{
        //            ////Postado" ou "Postado após o horário
        //            //foreach (var pegalinkAtual2 in ((System.Windows.Forms.HtmlElement)(itemTR)).GetElementsByTagName("A"))
        //            //{
        //            //var linkatual = ((System.Windows.Forms.HtmlElement)(pegalinkAtual2)).OuterHtml;
        //            //ListaLinksJavaScript.Add(linkatual.Substring(21, 102));
        //            //return;
        //            //}
        //            //}
        //            #endregion
        //        }
        //    }
        //}

        private bool VerificaSeELinhaDesejada(string linhaAtual)
        {
            //verifica para ir mais rápido.... senao teria que percorrer todas as colunas até saber se é a linha desejada....
            if (linhaAtual.Contains("Entregue".ToUpper()) == false)
            {
                if (linhaAtual.Contains("Distribuido ao remetente".RemoveAcentos().ToUpper()) == false)
                {
                    if (linhaAtual.Contains("Disponivel em Caixa Postal".RemoveAcentos().ToUpper()) == false)
                    {
                        if (linhaAtual.Contains("Aguardando retirada em Caixa Postal".RemoveAcentos().ToUpper()) == false)
                        {
                            if (linhaAtual.Contains("Aguardando retirada".RemoveAcentos().ToUpper()) == false)
                            {
                                if (linhaAtual.Contains("Aguardando retirada - Area sem entrega".RemoveAcentos().ToUpper()) == false)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void FormularioAtualizacaoObjetosAguardandoRetirada_KeyDown(object sender, KeyEventArgs e)
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
