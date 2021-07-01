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
    public partial class FormularioAtualizacaoObjetosRequeridosJaEntregues : Form
    {
        public bool abortarAtualizacao = false;
        private string CodigoObjetoAtual = string.Empty;
        DataSet ObjetosConsultaRastreamento = new DataSet();
        private static StringBuilder textoConsulta = new StringBuilder();
        public List<string> ListaLinksJavaScript = new List<string>();
        private bool UltimoElemento = false;
        private enum TipoTela { Rastreamento1, ListaObjetos2, DetalhesDeObjetos3, NomeCliente4 }

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

        //private string enderecoSRO = @"http://websro2/rastreamento/sro?opcao=PESQUISA&objetos=";
        //private string enderecoSRO = @"https://app.correiosnet.int/rastreamento/sro?opcao=PESQUISA&objetos=";
        private string enderecoSRO = @"" + Configuracoes.EnderecosSRO["EnderecoSROPorObjeto"].ToString();

        public FormularioAtualizacaoObjetosRequeridosJaEntregues(string codigoObjetoIniciado)
        {
            InitializeComponent();
            CodigoObjetoAtual = codigoObjetoIniciado;
            ObjetosConsultaRastreamento = new DataSet();
            textoConsulta = new StringBuilder();
            ListaLinksJavaScript = new List<string>();
            UltimoElemento = false;
            tipoTela = TipoTela.Rastreamento1;
            DetalhesDeObjetos3 = false;

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

        private void FormularioAtualizacaoObjetosRequeridosJaEntregues_Load(object sender, EventArgs e)
        {
            textBox1.Visible = Configuracoes.ExibicaoMensagensParaDesenvolvedor;
            panel1.Visible = Configuracoes.ExibicaoMensagensParaDesenvolvedor;

            for (int i = 0; i < 5; i++) SendKeys.Send("{TAB}");
        }

        public static FormularioAtualizacaoObjetosRequeridosJaEntregues RetornaComponentes()
        {
            FormularioAtualizacaoObjetosRequeridosJaEntregues formularioAtualizacaoObjetosRequeridosJaEntregues;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioAtualizacaoObjetosRequeridosJaEntregues")
                {
                    formularioAtualizacaoObjetosRequeridosJaEntregues = (FormularioAtualizacaoObjetosRequeridosJaEntregues)item;
                    return (FormularioAtualizacaoObjetosRequeridosJaEntregues)item;
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
                        //return;

                        string NomeCliente = string.Empty;
                        string Ldi = string.Empty;

                        for (int i = 0; i < webBrowser1.Document.GetElementsByTagName("TR").Count; i++)
                        {
                            string teste = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText;

                            if (webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Contains("LDI:"))
                            {
                                Ldi = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("LDI: ", "");
                                Ldi = Ldi.Substring(0, 12);
                                EscreveTextoTextBox("Ldi: " + Ldi.ToString());
                            }

                            if (webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Contains("Cliente:"))
                            {
                                NomeCliente = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Cliente: ", "");
                                EscreveTextoTextBox(NomeCliente.ToString());
                                #region grava no banco de dados
                                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                                {
                                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                                    DataSet ds = dao.RetornaDataSet("SELECT top 1 NomeCliente FROM TabelaObjetosSROLocal WHERE (CodigoObjeto = @CodigoObjeto)", new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual });
                                    if (ds.Tables[0].Rows.Count == 1)
                                    {
                                        NomeCliente = NomeCliente.Trim() == "" ? ds.Tables[0].Rows[0]["NomeCliente"].ToString().ToUpper().RemoveAcentos() : NomeCliente.Trim().ToUpper().RemoveAcentos();
                                    }
                                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET NomeCliente = @NomeCliente, CodigoLdi = @CodigoLdi, Atualizado = @Atualizado WHERE CodigoObjeto = @CodigoObjeto ", new List<Parametros>(){ 
                                            new Parametros("@NomeCliente", TipoCampo.Text, NomeCliente),
                                            new Parametros("@CodigoLdi", TipoCampo.Text, Ldi),
                                            new Parametros("@Atualizado", TipoCampo.Boolean, true),
                                            new Parametros("@CodigoObjeto", TipoCampo.Text, CodigoObjetoAtual)});
                                }
                                #endregion
                                //ListaLinksJavaScript.RemoveAt(0);
                                break;
                            }
                        }

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
            bool objetoJaEntregue = false;

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
                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET DataModificacao = @DataModificacao, Situacao = @Situacao, ObjetoEntregue = @ObjetoEntregue, Atualizado = @Atualizado WHERE (CodigoObjeto = @CodigoObjeto)"
                                                , new List<Parametros>() { 
							new Parametros { Nome = "@DataModificacao", Tipo = TipoCampo.Text, Valor = DataModificacao },
							new Parametros { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = "Entregue".ToUpper() },
							new Parametros { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Boolean, Valor = true },
							new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Boolean, Valor = true },
							new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
						});
                    }
                    #endregion
                    break;
                }
                #endregion

                #region "Distribuído ao remetente"
                if (LinhaAtual.Contains("Distribuido ao remetente".ToUpper()))
                {
                    objetoJaEntregue = true;
                    Mensagens.InformaDesenvolvedor("Gravando no banco: " + "Distribuído ao remetente".ToUpper());
                    string DataModificacao = LinhaAtual.Trim().Substring(0, 19);
                    #region UPDATE TabelaObjetosSROLocal
                    using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                    {
                        if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET DataModificacao = @DataModificacao, Situacao = @Situacao, ObjetoEntregue = @ObjetoEntregue, Atualizado = @Atualizado WHERE (CodigoObjeto = @CodigoObjeto)"
                                                , new List<Parametros>() { 
							new Parametros { Nome = "@DataModificacao", Tipo = TipoCampo.Text, Valor = DataModificacao },
							new Parametros { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = "Distribuido ao remetente".ToUpper() },
							new Parametros { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Boolean, Valor = true },
							new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Boolean, Valor = true },
							new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
						});
                    }
                    #endregion
                    break;
                }
                #endregion
            }
            if (objetoJaEntregue == false)
            {
                #region UPDATE TabelaObjetosSROLocal
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Atualizado = @Atualizado WHERE (CodigoObjeto = @CodigoObjeto)"
                                            , new List<Parametros>() { 
						new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Boolean, Valor = true },
						new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
					});
                }
                #endregion
                
            }
            this.Close();
        }

        //private void SeparaLinksDosObjetosRastreados()
        //{
        //    //string DataModificacao = string.Empty;
        //    //string Situacao = string.Empty;
        //    //int ObjetoEntregue = 0;
        //    //int Atualizado = 1;
        //    //string CodigoObjeto = CodigoObjetoAtual;

        //    foreach (var itemTR in webBrowser1.Document.GetElementsByTagName("TR"))
        //    {
        //        string itemTRInnerText = ((System.Windows.Forms.HtmlElement)(itemTR)).InnerText;
        //        string itemTRInnerHtml = ((System.Windows.Forms.HtmlElement)(itemTR)).InnerHtml;
        //        if (itemTRInnerText == null || string.IsNullOrEmpty(itemTRInnerText.Trim())) continue;

        //        //verifica para ir mais rápido.... senao teria que percorrer todas as colunas até saber se é a linha desejada....
        //        if (!itemTRInnerText.ToUpper().Contains("Entregue".ToUpper()) || 
        //            !itemTRInnerText.ToUpper().Contains("Distribuído ao remetente".ToUpper())) continue;

        //        foreach (var itemTD in ((System.Windows.Forms.HtmlElement)(itemTR)).GetElementsByTagName("TD"))
        //        {
        //            if (((System.Windows.Forms.HtmlElement)(itemTD)).InnerText == null) continue;
        //            string CampoAtual = ((System.Windows.Forms.HtmlElement)(itemTD)).InnerText.Trim().ToUpper();
        //            if (CampoAtual == null || string.IsNullOrEmpty(CampoAtual.Trim())) continue;

        //            string DataModificacao = itemTRInnerText.Trim().Substring(0, 19);
        //            if (CampoAtual == "Entregue".ToUpper() || CampoAtual == "Distribuído ao remetente".ToUpper())
        //            {
        //                using (DAO dao = new DAO(TipoBanco.OleDb, strConexao))
        //                {
        //                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
        //                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET DataModificacao = @DataModificacao, Situacao = @Situacao, ObjetoEntregue = @ObjetoEntregue, Atualizado = @Atualizado WHERE (CodigoObjeto = @CodigoObjeto)"
        //                                             , new List<Parametros>() { 
        //                                 new Parametros { Nome = "@DataModificacao", Tipo = TipoCampo.Text, Valor = DataModificacao },
        //                                 new Parametros { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = CampoAtual.ToUpper() },
        //                                 new Parametros { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Int, Valor = 1 },
        //                                 new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 1 },
        //                                 new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
        //                             });
        //                }
        //                break;
        //            }
        //        }
        //    }


        //    this.Close();
        //}

        private bool VerificaSeELinhaDesejada(string linhaAtual)
        {
            //verifica para ir mais rápido.... senao teria que percorrer todas as colunas até saber se é a linha desejada....
            if (linhaAtual.Contains("Entregue".ToUpper()) == false)
            {
                if (linhaAtual.Contains("Distribuido ao remetente".ToUpper()) == false)
                {
                    return false;
                }
            }
            return true;
        }

        private StringBuilder textoTextBox = new StringBuilder();
        private void EscreveTextoTextBox(string mensagem)
        {
            textoTextBox.AppendLine(mensagem);
            textBox1.Text = textoTextBox.ToString();
            textBox1.ScrollToCaret();
        }

        private void FormularioAtualizacaoObjetosRequeridosJaEntregues_KeyDown(object sender, KeyEventArgs e)
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
    }
}