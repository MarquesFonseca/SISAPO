﻿using System;
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
    public partial class FormularioAtualizacaoObjetosCompleto : Form
    {
        //modelo para preparação do novo modelo.17-06-2019-21-33
        public bool abortarAtualizacao = false;
        DataSet ObjetosConsultaRastreamento = new DataSet();
        private static StringBuilder textoConsulta = new StringBuilder();
        private string CodigoObjetoAtual = string.Empty;
        private bool LinksDosObjetosRastreadosSeparados = false;
        //private bool UltimoElemento = false;
        private enum TipoTela { Rastreamento1, ListaObjetos2, DetalhesDeObjetos3, Postado, SaiuParaEntrega, AguargandoRetirada }
        //private enum TipoSituacao { Postado, Saiu_para_entrega_ao_destinatário, Entregue, Distribuido_ao_remetente, Aguardando_retirada, Disponivel_em_Caixa_Postal }
        //private TipoSituacao tipoSituacao = TipoSituacao.Postado;
        private DataSet dadosAgencia = new DataSet();

        private TipoTela tipoTela = TipoTela.Rastreamento1;
        private string TelaRastreamento_1_1 = @"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Nova_Rastreamento_Problema\RastreamantoDetalhes-1-1-problema.htm";
        //private string TelaRastreamento_1_1 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\1-1-TelaRastreamento.htm";
        private string TelaDetalhesDeObjetos_1_3 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\1-3-TelaDetalhesDeObjetos_defeito.htm";
        //private string TelaNomeCliente_1_4 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\1-4-TelaNomeCliente.htm";
        //private string TelaRastreamento_2_1 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\2-1-TelaRastreamento.htm";
        private string TelaListaDeObjetos_2_2 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\2-2-TelaListaDeObjetos.htm";
        private string TelaDetalhesDeObjetos_2_3 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\2-3-TelaDetalhesDeObjetos.htm";
        //private string TelaNomeCliente_2_4 = @"C:\Users\MARQUES\Documents\visual studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\2-4-TelaNomeCliente.htm";
        //private string TelaNomeCliente_2_4 = @"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\Rastreamento_Unificado_htm.htm";

        private string enderecoSRO = @"http://websro2/rastreamento/sro";

        private List<object> listaObjectsLinhasMarcadasPostado1 = new List<object>();
        private List<object> listaObjectsLinhasMarcadasSaiuParaEntregaAoDestinatario2 = new List<object>();
        private List<object> listaObjectsLinhasMarcadasAguardandoRetirada3 = new List<object>();
        private List<object> listaObjectsLinhasMarcadasEntregues4 = new List<object>();

        public FormularioAtualizacaoObjetosCompleto()
        {
            InitializeComponent();
            ObjetosConsultaRastreamento = new DataSet();
            textoConsulta = new StringBuilder();
            CodigoObjetoAtual = string.Empty;
            //UltimoElemento = false;
            tipoTela = TipoTela.Rastreamento1;

            dadosAgencia = RetornaDadosAgencia();

            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
            {
                this.WindowState = FormWindowState.Maximized;
                // caminho que define o teste
                //caminho para tela de consulta atraves de códigos de objetos rastreadores
                webBrowser1.Url = new Uri(TelaRastreamento_1_1);
            }
            if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
            {
                //caminho para tela de consulta atraves de códigos de objetos rastreadores
                webBrowser1.Url = new Uri(enderecoSRO);
            }
        }

        private void FormularioAtualizacaoObjetosCompleto_Load(object sender, EventArgs e)
        {
            textBox1.Visible = Configuracoes.ExibicaoMensagensParaDesenvolvedor;
            panel1.Visible = Configuracoes.ExibicaoMensagensParaDesenvolvedor;

            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                ObjetosConsultaRastreamento = dao.RetornaDataSet("SELECT TOP 1 CodigoObjeto, Atualizado FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Boolean, Valor = false });

                if (ObjetosConsultaRastreamento == null || ObjetosConsultaRastreamento.Tables[0].Rows.Count == 0) return;
                if (ObjetosConsultaRastreamento.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ObjetosConsultaRastreamento.Tables[0].Rows)
                    {
                        textoConsulta.AppendLine(item[0].ToString());
                    }

                    for (int i = 0; i < 5; i++) SendKeys.Send("{TAB}");
                }
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

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            #region Validações
            if (webBrowser1.Url.AbsoluteUri == "about:blank")
            {
                EscreveTextoTextBox("Url - about:blank - return");
                return;
            }
            if (ObjetosConsultaRastreamento == null)
            {
                EscreveTextoTextBox("ObjetosConsultaRastreamento == null -- return");
                return;
            }
            if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
            {
                if (ObjetosConsultaRastreamento.Tables[0].Rows.Count == 0)
                {
                    EscreveTextoTextBox("ObjetoConsultaRastreamento com contagem 0 - return");
                    webBrowser1.Document.GetElementsByTagName("textarea")[0].Focus();
                    return;
                }
            }
            #endregion
            //return;
            try
            {
                EscreveTextoTextBox("TipoTela atual = " + tipoTela.ToString());
                Mensagens.InformaDesenvolvedor(string.Format("Parada para verificar A tela atual..."));
                switch (tipoTela)
                {
                    #region Acessando Rastreamento1
                    case TipoTela.Rastreamento1:
                        #region TipoAmbiente.Producao
                        if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                        {
                            if (webBrowser1.Document.GetElementsByTagName("textarea").Count == 0) return;
                            else
                            {
                                webBrowser1.Document.GetElementsByTagName("textarea")[0].InnerText = textoConsulta.ToString();
                                webBrowser1.Document.GetElementsByTagName("textarea")[0].Focus();
                                EscreveTextoTextBox("Será processado: [" + ObjetosConsultaRastreamento.Tables[0].Rows.Count.ToString() + "]");
                                if (ObjetosConsultaRastreamento.Tables[0].Rows.Count == 1)
                                {
                                    //para 1 objeto
                                    tipoTela = TipoTela.DetalhesDeObjetos3;
                                    EscreveTextoTextBox("Será encaminhado para a tela 'TipoTela.DetalhesDeObjetos3'");
                                }
                                if (ObjetosConsultaRastreamento.Tables[0].Rows.Count > 1)
                                {    //para 2 objetos
                                    tipoTela = TipoTela.ListaObjetos2;
                                    EscreveTextoTextBox("Será encaminhado para a tela 'TipoTela.DetalhesDeObjetos2'");
                                }
                                SendKeys.Send("{TAB}");
                                SendKeys.Send("{ENTER}");
                            }
                        }
                        #endregion

                        #region TipoAmbiente.Desenvolvimento
                        if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                        {
                            if (ObjetosConsultaRastreamento.Tables[0].Rows.Count == 1)
                            {
                                //para 1 objeto
                                tipoTela = TipoTela.DetalhesDeObjetos3;
                                webBrowser1.Url = new Uri(TelaDetalhesDeObjetos_1_3);
                                //webBrowser1.Url = new Uri(TelaRastreamento_1_1);
                                //webBrowser1.Url = new Uri(@"file:///C:/Users/MARQUES/Documents/Visual%20Studio%202010/Projects/SISAPO/SISAPO/bin/Debug/Nova_Rastreamento_Problema/RastreamantoDetalhes-3-2-problema.htm");
                                //webBrowser1.Url = new Uri(@"file:///H:/bin/Debug/TelasRastreamento/1-3-TelaDetalhesDeObjetos_defeito2.htm");
                                webBrowser1.Url = new Uri(@"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Rastreamento 05062019\OH239391285BR_DETALHES.htm");
                            }
                            if (ObjetosConsultaRastreamento.Tables[0].Rows.Count > 1)
                            {
                                //para 2 objetos
                                tipoTela = TipoTela.ListaObjetos2;
                                webBrowser1.Url = new Uri(TelaListaDeObjetos_2_2);
                            }
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

                        SeparaLinksDosObjetosRastreados();

                        #region Trata Objetos Postados
                        if (listaObjectsLinhasMarcadasPostado1.Count > 0)
                        {
                            //object objecto = listaObjectsLinhasMarcadasPostado1[0];
                            //string teste = ((object[])(objecto))[1].ToString(); //"PM130013941BR"
                            CodigoObjetoAtual = ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[1].ToString();
                            tipoTela = TipoTela.Postado;

                            #region TipoAmbiente.Producao
                            if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                            {
                                EscreveTextoTextBox("antes de clicar...");
                                Mensagens.InformaDesenvolvedor(string.Format("Antes de clicar no link para ir aos detalhes de 'Objeto Postado'."));
                                //webBrowser1.Document.InvokeScript("DetalhesPesquisa", new object[] { listaObjectsLinhasMarcadasPostado1[0] });
                                webBrowser1.Document.InvokeScript("DetalhesPesquisa", new object[] { 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[0].ToString(), /*DETALHESINTRA*/
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[1].ToString(), /*OA899613034BR*/ 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[2].ToString(), /*LDI*/
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[3].ToString(), /*1094248527*/ 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[4].ToString(), /*1*/ 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[5].ToString(), /*31/08/2018 10:34:00*/ 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[6].ToString() /*intra*/ });
                                //return;
                            }
                            #endregion
                            #region TipoAmbiente.Desenvolvimento
                            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                            {
                                webBrowser1.Url = new Uri(@"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Rastreamento 05062019\OH239391285BR_POSTADO.htm");
                                return;
                            }
                            #endregion
                        }
                        #endregion

                        #region Trata Objetos que saíram para entrega ao destinatário
                        if (listaObjectsLinhasMarcadasSaiuParaEntregaAoDestinatario2.Count > 0)
                        {
                            CodigoObjetoAtual = ((object[])(listaObjectsLinhasMarcadasSaiuParaEntregaAoDestinatario2[0]))[1].ToString();
                            tipoTela = TipoTela.SaiuParaEntrega;

                            #region TipoAmbiente.Producao
                            if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                            {
                                EscreveTextoTextBox("antes de clicar...");
                                Mensagens.InformaDesenvolvedor(string.Format("Antes de clicar no link para ir aos detalhes de 'Objetos que saíram para entrega ao destinatário'."));
                                //webBrowser1.Document.InvokeScript("DetalhesPesquisa", new object[] { listaObjectsLinhasMarcadasSaiuParaEntregaAoDestinatario2[0] });
                                webBrowser1.Document.InvokeScript("DetalhesPesquisa", new object[] { 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[0].ToString(), /*DETALHESINTRA*/
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[1].ToString(), /*OA899613034BR*/ 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[2].ToString(), /*LDI*/
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[3].ToString(), /*1094248527*/ 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[4].ToString(), /*1*/ 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[5].ToString(), /*31/08/2018 10:34:00*/ 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[6].ToString() /*intra*/ });
                                //return;
                            }
                            #endregion
                            #region TipoAmbiente.Desenvolvimento
                            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                            {
                                webBrowser1.Url = new Uri(@"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Rastreamento 05062019\OH239391285BR_SAIU_PARA_ENTREGA_DESTINATARIO.htm");
                                return;
                            }
                            #endregion
                        }
                        #endregion

                        #region Trata Objetos Aguardando Retirada
                        if (listaObjectsLinhasMarcadasAguardandoRetirada3.Count > 0)
                        {
                            CodigoObjetoAtual = ((object[])(listaObjectsLinhasMarcadasAguardandoRetirada3[0]))[1].ToString();
                            tipoTela = TipoTela.AguargandoRetirada;

                            #region TipoAmbiente.Producao
                            if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                            {
                                EscreveTextoTextBox("antes de clicar...");
                                Mensagens.InformaDesenvolvedor(string.Format("Antes de clicar no link para ir aos detalhes de 'Objetos Aguardando Retirada'."));
                                //webBrowser1.Document.InvokeScript("DetalhesPesquisa", new object[] { listaObjectsLinhasMarcadasAguardandoRetirada3[0] });
                                webBrowser1.Document.InvokeScript("DetalhesPesquisa", new object[] { 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[0].ToString(), /*DETALHESINTRA*/
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[1].ToString(), /*OA899613034BR*/ 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[2].ToString(), /*LDI*/
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[3].ToString(), /*1094248527*/ 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[4].ToString(), /*1*/ 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[5].ToString(), /*31/08/2018 10:34:00*/ 
                                ((object[])(listaObjectsLinhasMarcadasPostado1[0]))[6].ToString() /*intra*/ });
                                //return;
                            }
                            #endregion
                            #region TipoAmbiente.Desenvolvimento
                            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                            {
                                webBrowser1.Url = new Uri(@"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Rastreamento 05062019\OH239391285BR_AGUARDANDO_RETIRADA.htm");
                                return;
                            }
                            #endregion
                        }
                        #endregion

                        Mensagens.InformaDesenvolvedor(string.Format("Antes de fechar por completo o formulário... FIM."));
                        this.Close();//terminou as capturas e fecha pra puchar o próximo...
                        break;
                    #endregion

                    //------------------------------------------------------

                    #region TipoTela.Postado
                    case TipoTela.Postado:
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
                            string InnerTextPostado = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText;
                            if (InnerTextPostado.Contains("Unidade:"))
                            {
                                UnidadePostagem = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Unidade: ", "");
                                EscreveTextoTextBox("Unidade: " + UnidadePostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> Unidade: " + UnidadePostagem.ToString()));
                                continue;
                            }
                            else if (InnerTextPostado.Contains("Município:"))
                            {
                                MunicipioPostagem = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Município: ", "");
                                EscreveTextoTextBox("Município: " + MunicipioPostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> Município: " + MunicipioPostagem.ToString()));
                                continue;
                            }
                            else if (InnerTextPostado.Contains("Criado em:"))
                            {
                                CriacaoPostagem = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Criado em: ", "");
                                EscreveTextoTextBox("Criado em: " + CriacaoPostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> Criado em: " + CriacaoPostagem.ToString()));
                                continue;
                            }
                            else if (InnerTextPostado.Contains("CEP Destino:"))
                            {
                                string temp = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("CEP Destino: ", "");
                                CepDestinoPostagem = temp.Split(':')[0].Substring(0, (temp.Length >= 8 ? 8 : temp.Length));
                                EscreveTextoTextBox("CEP Destino: " + CepDestinoPostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> CEP Destino: " + CepDestinoPostagem.ToString()));
                                NomeClientePostagem = temp.Split(':')[1].TrimStart().TrimEnd();
                                EscreveTextoTextBox("Destinatário: " + NomeClientePostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> Destinatário: " + NomeClientePostagem.ToString()));
                                continue;
                            }
                            else if (InnerTextPostado.Contains("AR:"))
                            {
                                string temp = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("AR: ", "");
                                ARPostagem = temp.Split(':')[0].Substring(0, (temp.Length >= 1 ? 1 : temp.Length));
                                EscreveTextoTextBox("AR: " + ARPostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> AR: " + ARPostagem.ToString()));
                                MPPostagem = temp.Split(':')[1].TrimStart().TrimEnd();
                                EscreveTextoTextBox("MP: " + MPPostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Capturando--> MP: " + MPPostagem.ToString()));
                                continue;
                            }
                            else if (InnerTextPostado.Contains("Data Máx Prev Entrega:"))
                            {
                                DataMaxPrevistaEntregaPostagem = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Data Máx Prev Entrega: ", "").TrimStart().TrimEnd();
                                EscreveTextoTextBox("Data Máx Prev Entrega: " + DataMaxPrevistaEntregaPostagem.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Data Máx Prev Entrega: " + DataMaxPrevistaEntregaPostagem.ToString()));
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
															new Parametros("@NomeCliente", TipoCampo.Text, NomeClientePostagem.ToUpper()),
															new Parametros("@UnidadePostagem", TipoCampo.Text, UnidadePostagem),
															new Parametros("@MunicipioPostagem", TipoCampo.Text, MunicipioPostagem),
															new Parametros("@CriacaoPostagem", TipoCampo.Text, CriacaoPostagem),
															new Parametros("@CepDestinoPostagem", TipoCampo.Text, CepDestinoPostagem),
															new Parametros("@ARPostagem", TipoCampo.Text, ARPostagem),
															new Parametros("@MPPostagem", TipoCampo.Text, MPPostagem),
															new Parametros("@DataMaxPrevistaEntregaPostagem", TipoCampo.Text, DataMaxPrevistaEntregaPostagem),
															new Parametros("@CodigoObjeto", TipoCampo.Text, CodigoObjetoAtual)
											});
                                            listaObjectsLinhasMarcadasPostado1.RemoveAt(0);
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
                                            listaObjectsLinhasMarcadasPostado1.RemoveAt(0);
                                        }
                                    }
                                }
                                #endregion
                                break;//pois supoe que é a ultima linha embaixo.... obvio.
                            }
                        }
                        #endregion

                        #region Volta
                        tipoTela = TipoTela.DetalhesDeObjetos3;
                        if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                        {
                            webBrowser1.Url = new Uri(@"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Rastreamento 05062019\OH239391285BR_DETALHES.htm");
                            //return;
                        }
                        if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                        {
                            Mensagens.InformaDesenvolvedor(string.Format("Antes de voltar após ter gravado os dados postado linha 460..."));
                            for (int i = 0; i < 7; i++) SendKeys.Send("{TAB}");
                            SendKeys.Send("{ENTER}");
                            //webBrowser1.GoBack();
                        }
                        break;
                        #endregion
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
                            string InnerTextSaiuParaEntrega = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText;
                            if (InnerTextSaiuParaEntrega.Contains("Unidade:"))
                            {
                                UnidadeLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Unidade: ", "");
                                EscreveTextoTextBox("Unidade: " + UnidadeLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Unidade: " + UnidadeLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Município:"))
                            {
                                MunicipioLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Município: ", "");
                                EscreveTextoTextBox("Município: " + MunicipioLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Município: " + MunicipioLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Criado em:"))
                            {
                                CriacaoLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Criado em: ", "");
                                EscreveTextoTextBox("Criado em: " + CriacaoLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Criado em: " + CriacaoLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Carteiro:"))
                            {
                                CarteiroLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Carteiro: ", "");
                                EscreveTextoTextBox("Carteiro: " + CarteiroLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Carteiro: " + CarteiroLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Distrito:"))
                            {
                                DistritoLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Distrito: ", "");
                                EscreveTextoTextBox("Distrito: " + DistritoLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Distrito: " + DistritoLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("LOEC:"))
                            {
                                string temp = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("LOEC: ", "");
                                NumeroLOEC = temp.Split(':')[0].Replace(" - ID", "");
                                EscreveTextoTextBox("LOEC: " + NumeroLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("LOEC: " + NumeroLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Cliente:"))
                            {
                                ClienteLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Cliente: ", "");
                                EscreveTextoTextBox("Cliente: " + ClienteLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Cliente: " + ClienteLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Endereço:"))
                            {
                                EnderecoLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Endereço: ", "");
                                EscreveTextoTextBox("Endereço: " + EnderecoLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Endereço: " + EnderecoLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Bairro:"))
                            {
                                BairroLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Bairro: ", "");
                                EscreveTextoTextBox("Bairro: " + BairroLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Bairro: " + BairroLOEC.ToString()));
                                continue;
                            }
                            else if (InnerTextSaiuParaEntrega.Contains("Localidade:"))
                            {
                                LocalidadeLOEC = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Localidade: ", "");
                                EscreveTextoTextBox("Localidade: " + LocalidadeLOEC.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Localidade: " + LocalidadeLOEC.ToString()));
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
															new Parametros("@UnidadeLOEC", TipoCampo.Text, UnidadeLOEC.ToUpper()),
															new Parametros("@MunicipioLOEC", TipoCampo.Text, MunicipioLOEC.ToUpper()),
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
                                            listaObjectsLinhasMarcadasSaiuParaEntregaAoDestinatario2.RemoveAt(0);
                                        }
                                        if (ds.Tables[0].Rows[0]["NomeCliente"].ToString() != "")
                                        {
                                            //já tem algo e não grava o nome
                                            Mensagens.InformaDesenvolvedor(string.Format("Gravando no banco detalhes objeto SaiuParaEntrega"));
                                            dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET UnidadeLOEC = @UnidadeLOEC, MunicipioLOEC = @MunicipioLOEC, CriacaoLOEC = @CriacaoLOEC, CarteiroLOEC = @CarteiroLOEC, DistritoLOEC = @DistritoLOEC, NumeroLOEC = @NumeroLOEC, EnderecoLOEC = @EnderecoLOEC, BairroLOEC = @BairroLOEC, LocalidadeLOEC = @LocalidadeLOEC WHERE CodigoObjeto = @CodigoObjeto ", new List<Parametros>(){ 
															new Parametros("@UnidadeLOEC", TipoCampo.Text, UnidadeLOEC.ToUpper()),
															new Parametros("@MunicipioLOEC", TipoCampo.Text, MunicipioLOEC.ToUpper()),
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
                                            listaObjectsLinhasMarcadasSaiuParaEntregaAoDestinatario2.RemoveAt(0);
                                        }
                                    }
                                }
                                #endregion
                                break;//pois supoe que é a ultima linha embaixo.... obvio.
                            }
                        }
                        #endregion

                        tipoTela = TipoTela.DetalhesDeObjetos3;
                        if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                        {
                            webBrowser1.Url = new Uri(@"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Rastreamento 05062019\OH239391285BR_DETALHES.htm");
                            return;
                        }
                        if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                        {
                            Mensagens.InformaDesenvolvedor(string.Format("Antes de voltar após ter gravado os dados SaiuParaEntrega..."));
                            for (int i = 0; i < 8; i++) SendKeys.Send("{TAB}");
                            SendKeys.Send("{ENTER}");
                            //webBrowser1.GoBack();
                        }
                        break;
                    #endregion

                    #region TipoTela.AguargandoRetirada
                    case TipoTela.AguargandoRetirada:
                        string Ldi = string.Empty;
                        string NomeCliente = string.Empty;
                        CodigoObjetoAtual = ((object[])(listaObjectsLinhasMarcadasAguardandoRetirada3[0]))[1].ToString();
                        #region Capturando os dados
                        EscreveTextoTextBox("Capturando o nome em TipoTela.AguargandoRetirada");
                        for (int i = 0; i < webBrowser1.Document.GetElementsByTagName("TR").Count; i++)
                        {
                            string InnerTextAguargandoRetirada = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText;
                            if (InnerTextAguargandoRetirada.Contains("LDI:"))
                            {
                                Ldi = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("LDI: ", "");
                                Ldi = Ldi.Substring(0, 12);
                                EscreveTextoTextBox("Ldi: " + Ldi.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Ldi: " + Ldi.ToString()));
                                continue;
                            }
                            if (InnerTextAguargandoRetirada.Contains("Cliente:"))
                            {
                                NomeCliente = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Cliente: ", "");
                                EscreveTextoTextBox("Nome Cliente:" + NomeCliente.ToString());
                                Mensagens.InformaDesenvolvedor(string.Format("Nome Cliente:" + NomeCliente.ToString()));
                                break;
                            }
                        }
                        #endregion
                        #region grava no banco de dados
                        if (NomeCliente != "")
                        {
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
                                        Mensagens.InformaDesenvolvedor(string.Format("Gravando no banco detalhes objeto Aguardando retirada"));
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET NomeCliente = @NomeCliente, CodigoLdi = @CodigoLdi, Atualizado = @Atualizado WHERE CodigoObjeto = @CodigoObjeto ", new List<Parametros>(){ 
                                            new Parametros("@NomeCliente", TipoCampo.Text, NomeCliente.ToUpper()),
                                            new Parametros("@CodigoLdi", TipoCampo.Text, Ldi),
                                            new Parametros("@Atualizado", TipoCampo.Boolean, true),
                                            new Parametros("@CodigoObjeto", TipoCampo.Text, CodigoObjetoAtual)});
                                        listaObjectsLinhasMarcadasAguardandoRetirada3.RemoveAt(0);
                                    }
                                    if (ds.Tables[0].Rows[0]["NomeCliente"].ToString() != "")
                                    {
                                        //já tem algo e não grava o nome
                                        Mensagens.InformaDesenvolvedor(string.Format("Gravando no banco detalhes objeto Aguardando retirada"));
                                        dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET CodigoLdi = @CodigoLdi, Atualizado = @Atualizado WHERE CodigoObjeto = @CodigoObjeto ", new List<Parametros>(){ 
                                            //new Parametros("@NomeCliente", TipoCampo.Text, NomeCliente.ToUpper()),
                                            new Parametros("@CodigoLdi", TipoCampo.Text, Ldi),
                                            new Parametros("@Atualizado", TipoCampo.Boolean, true),
                                            new Parametros("@CodigoObjeto", TipoCampo.Text, CodigoObjetoAtual)});
                                        listaObjectsLinhasMarcadasAguardandoRetirada3.RemoveAt(0);
                                    }
                                }
                            }
                        }
                        #endregion

                        tipoTela = TipoTela.DetalhesDeObjetos3;
                        if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                        {
                            webBrowser1.Url = new Uri(TelaDetalhesDeObjetos_2_3);
                            return;
                        }
                        if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                        {
                            for (int i = 0; i < 8; i++) SendKeys.Send("{TAB}");
                            SendKeys.Send("{ENTER}");
                            //webBrowser1.GoBack();
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
            //verifica se já foi buscados os links
            if (LinksDosObjetosRastreadosSeparados == false)
            {
                Mensagens.InformaDesenvolvedor(string.Format("Será feito SeparaLinksDosObjetosRastreados()"));
                #region Separa Tipos de linhas
                var itensss = webBrowser1.Document.All.Cast<HtmlElement>().AsEnumerable();
                var linhas = itensss.Cast<HtmlElement>().AsEnumerable().Where(x => x.TagName.ToUpper() == "TR");
                foreach (HtmlElement item in linhas)
                {
                    try
                    {
                        if (item.InnerText == null || string.IsNullOrEmpty(item.InnerText.Trim())) continue;
                        else
                        {
                            #region "Entregue"
                            if (item.InnerText.ToUpper().Contains("Entregue".ToUpper()))
                            {
                                string linkRetornado = RetornaLinkJavaPeloLink(item, 23, 104).Replace("%20", " ");
                                #region Teste DetalhesPesquisa('DETALHESINTRA','PM130013941BR','BDI','1234231139','1','21/05/2019%2010:34:34','intra')
                                object objects = new object[]
                                {
                                    linkRetornado.Split('\'')[1], /*DETALHESINTRA*/
                                    linkRetornado.Split('\'')[3], /*PM130013941BR*/ 
                                    linkRetornado.Split('\'')[5], /*BDI*/
                                    linkRetornado.Split('\'')[7], /*1234231139*/ 
                                    linkRetornado.Split('\'')[9], /*1*/ 
                                    linkRetornado.Split('\'')[11], /*21/05/2019%2010:34:34*/ 
                                    linkRetornado.Split('\'')[13] /*intra*/ 
                                };
                                listaObjectsLinhasMarcadasEntregues4.Add(objects);
                                #endregion
                                Mensagens.InformaDesenvolvedor(string.Format("Add listaLinhasMarcadasEntregues:\n{0}", item.InnerText));

                                #region UPDATE TabelaObjetosSROLocal
                                CodigoObjetoAtual = linkRetornado.Split('\'')[3].ToString();
                                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                                {
                                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET ObjetoEntregue = @ObjetoEntregue WHERE (CodigoObjeto = @CodigoObjeto)"
                                                , new List<Parametros>() { 
						                    new Parametros { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Int, Valor = 1 },
						                    new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }});
                                }
                                #endregion
                                continue;
                            }
                            #endregion
                            #region "Distribuído ao remetente"
                            if (item.InnerText.ToUpper().Contains("Distribuído ao remetente".ToUpper()))
                            {
                                string linkRetornado = RetornaLinkJavaPeloLink(item, 23, 104).Replace("%20", " ");
                                #region Teste DetalhesPesquisa('DETALHESINTRA','PM130013941BR','BDI','1234231139','1','21/05/2019%2010:34:34','intra')
                                object objects = new object[]
                                {
                                    linkRetornado.Split('\'')[1], /*DETALHESINTRA*/
                                    linkRetornado.Split('\'')[3], /*PM130013941BR*/ 
                                    linkRetornado.Split('\'')[5], /*BDI*/
                                    linkRetornado.Split('\'')[7], /*1234231139*/ 
                                    linkRetornado.Split('\'')[9], /*1*/ 
                                    linkRetornado.Split('\'')[11], /*21/05/2019%2010:34:34*/ 
                                    linkRetornado.Split('\'')[13] /*intra*/ 
                                };
                                listaObjectsLinhasMarcadasEntregues4.Add(objects);
                                #endregion
                                Mensagens.InformaDesenvolvedor(string.Format("Add listaLinhasMarcadasDistribuidoAoRemetente:\n{0}", item.InnerText));

                                #region UPDATE TabelaObjetosSROLocal
                                CodigoObjetoAtual = linkRetornado.Split('\'')[3].ToString();
                                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                                {
                                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Situacao = @Situacao, ObjetoEntregue = @ObjetoEntregue WHERE (CodigoObjeto = @CodigoObjeto)"
                                             , new List<Parametros>() { 
                                         new Parametros { Nome = "@Situacao", Tipo = TipoCampo.Text, Valor = "Distribuído ao remetente".Trim().ToUpper() },
                                         new Parametros { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Int, Valor = 1 },
                                         new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
                                     });
                                }
                                #endregion
                                continue;
                            }
                            #endregion

                            #region "Aguardando retirada"
                            if (item.InnerText.ToUpper().Contains("Aguardando retirada".ToUpper()))
                            {
                                string linkRetornado = RetornaLinkJavaPeloLink(item, 23, 104).Replace("%20", " ");
                                #region Teste DetalhesPesquisa('DETALHESINTRA','PM130013941BR','LDI','1147539447','1','21/05/2019%2010:01:09','intra')
                                object objects = new object[]
                                {
                                    linkRetornado.Split('\'')[1], /*DETALHESINTRA*/
                                    linkRetornado.Split('\'')[3], /*PM130013941BR*/ 
                                    linkRetornado.Split('\'')[5], /*LDI*/
                                    linkRetornado.Split('\'')[7], /*1147539447*/ 
                                    linkRetornado.Split('\'')[9], /*1*/ 
                                    linkRetornado.Split('\'')[11], /*21/05/2019%2010:01:09*/ 
                                    linkRetornado.Split('\'')[13] /*intra*/ 
                                };
                                listaObjectsLinhasMarcadasAguardandoRetirada3.Add(objects);
                                #endregion
                                Mensagens.InformaDesenvolvedor(string.Format("Add listaLinhasMarcadasAguardandoRetirada:\n{0}", item.InnerText));
                                continue;
                            }
                            #endregion
                            #region "Aguardando retirada - Área sem entrega
                            if (item.InnerText.ToUpper().Contains("Aguardando retirada - Área sem entrega".ToUpper()))
                            {
                                string linkRetornado = RetornaLinkJavaPeloLink(item, 23, 104).Replace("%20", " ");
                                #region Teste DetalhesPesquisa('DETALHESINTRA','PM130013941BR','BDI','1234231139','1','21/05/2019%2010:34:34','intra')
                                object objects = new object[]
                                {
                                    linkRetornado.Split('\'')[1], /*DETALHESINTRA*/
                                    linkRetornado.Split('\'')[3], /*PM130013941BR*/ 
                                    linkRetornado.Split('\'')[5], /*BDI*/
                                    linkRetornado.Split('\'')[7], /*1234231139*/ 
                                    linkRetornado.Split('\'')[9], /*1*/ 
                                    linkRetornado.Split('\'')[11], /*21/05/2019%2010:34:34*/ 
                                    linkRetornado.Split('\'')[13] /*intra*/ 
                                };
                                listaObjectsLinhasMarcadasAguardandoRetirada3.Add(objects);
                                #endregion
                                Mensagens.InformaDesenvolvedor(string.Format("Add listaLinhasMarcadasAguardandoRetiradaAreaSemEntrega:\n{0}", item.InnerText));
                                continue;
                            }
                            #endregion
                            #region "Disponível em Caixa Postal"
                            if (item.InnerText.ToUpper().Contains("Disponível em Caixa Postal".ToUpper()))
                            {
                                string linkRetornado = RetornaLinkJavaPeloLink(item, 23, 104).Replace("%20", " ");
                                #region Teste DetalhesPesquisa('DETALHESINTRA','PM130013941BR','BDI','1234231139','1','21/05/2019%2010:34:34','intra')
                                object objects = new object[]
                                {
                                    linkRetornado.Split('\'')[1], /*DETALHESINTRA*/
                                    linkRetornado.Split('\'')[3], /*PM130013941BR*/ 
                                    linkRetornado.Split('\'')[5], /*BDI*/
                                    linkRetornado.Split('\'')[7], /*1234231139*/ 
                                    linkRetornado.Split('\'')[9], /*1*/ 
                                    linkRetornado.Split('\'')[11], /*21/05/2019%2010:34:34*/ 
                                    linkRetornado.Split('\'')[13] /*intra*/ 
                                };
                                listaObjectsLinhasMarcadasAguardandoRetirada3.Add(objects);
                                #endregion
                                Mensagens.InformaDesenvolvedor(string.Format("Add listaLinhasMarcadasDisponivelEmCaixaPostal:\n{0}", item.InnerText));

                                #region UPDATE TabelaObjetosSROLocal
                                CodigoObjetoAtual = linkRetornado.Split('\'')[3].ToString();
                                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                                {
                                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET CaixaPostal = @CaixaPostal WHERE (CodigoObjeto = @CodigoObjeto)"
                                             , new List<Parametros>() { 
										 new Parametros { Nome = "@CaixaPostal", Tipo = TipoCampo.Boolean, Valor = true },
										 new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual }
									 });
                                }
                                #endregion
                                continue;
                            }
                            #endregion

                            #region "Saiu para entrega ao destinatário
                            if (item.InnerText.ToUpper().Contains("Saiu para entrega ao destinatário".ToUpper()))
                            {
                                string linkRetornado = RetornaLinkJavaPeloLink(item, 23, 105).Replace("%20", " ");
                                #region Teste DetalhesPesquisa('DETALHESINTRA','PM130013941BR','OEC','32897920981','1','17/05/2019%2009:32:52','intra')
                                object objects = new object[]
                                {
                                    linkRetornado.Split('\'')[1], /*DETALHESINTRA*/
                                    linkRetornado.Split('\'')[3], /*PM130013941BR*/ 
                                    linkRetornado.Split('\'')[5], /*OEC*/
                                    linkRetornado.Split('\'')[7], /*32897920981*/ 
                                    linkRetornado.Split('\'')[9], /*1*/ 
                                    linkRetornado.Split('\'')[11], /*17/05/2019%2009:32:52*/ 
                                    linkRetornado.Split('\'')[13] /*intra*/ 
                                };
                                listaObjectsLinhasMarcadasSaiuParaEntregaAoDestinatario2.Add(objects);
                                #endregion
                                Mensagens.InformaDesenvolvedor(string.Format("Add listaLinhasMarcadasSaiuParaEntregaAoDestinatario:\n{0}", item.InnerText));
                                continue;
                            }
                            #endregion

                            #region "Postado"
                            if (item.InnerText.ToUpper().Contains("Postado".ToUpper()) ||
                                item.InnerText.ToUpper().Contains("Postado após o horário".ToUpper()) ||
                                item.InnerText.ToUpper().Contains("Postado depois do horário".ToUpper()) ||
                                item.InnerText.ToUpper().Contains("Postagem - DH".ToUpper()))
                            {
                                string linkRetornado = RetornaLinkJavaPeloLink(item, 23, 103).Replace("%20", " ");
                                #region Teste DetalhesPesquisa('DETALHESINTRA','PM130013941BR','PO','4984498610','1','08/05/2019%2017:06:14','intra')
                                object objectsss = new object[7]
                                {
                                    linkRetornado.Substring(18,13), /*DETALHESINTRA*/
                                    linkRetornado.Substring(34,13), /*PM130013941BR*/ 
                                    linkRetornado.Substring(50,3), /*PO*/
                                    linkRetornado.Substring(55,10), /*4984498610*/ 
                                    linkRetornado.Substring(68,1), /*1*/ 
                                    linkRetornado.Substring(72,21), /*08/05/2019%2017:06:14*/ 
                                    linkRetornado.Substring(96,5) /*intra*/ 
                                };
                                listaObjectsLinhasMarcadasPostado1.Add(objectsss);
                                #endregion
                                Mensagens.InformaDesenvolvedor(string.Format("Add listaLinhasMarcadasPostado:\n{0}", item.InnerText));
                                LinksDosObjetosRastreadosSeparados = true;
                                continue;

                            }
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        LinksDosObjetosRastreadosSeparados = false;
                        Mensagens.Erro(ex.Message);
                    }
                }
                #endregion
            }
            else
            {
                Mensagens.InformaDesenvolvedor(string.Format("Não será feito SeparaLinksDosObjetosRastreados()"));
            }
        }

        private string RetornaCodigoObjetoAtualPeloLink(HtmlElement link, string qualLinhaEstaBuscando, int startIndex = 57, int length = 13)
        {
            try
            {
                string CodigoObjetoAtual = "";

                foreach (HtmlElement item in ((System.Windows.Forms.HtmlElement)(link)).GetElementsByTagName("A"))
                {
                    var linkatual = ((System.Windows.Forms.HtmlElement)(item)).OuterHtml;
                    Mensagens.InformaDesenvolvedor(string.Format("{0} - Pegando o objeto atual: '{1}'", qualLinhaEstaBuscando, linkatual.Substring(57, 14)));
                    CodigoObjetoAtual = linkatual.Substring(startIndex, length);
                    break;
                }
                return CodigoObjetoAtual;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        private string RetornaLinkJavaPeloLink(HtmlElement link, int startIndex = 23, int length = 103)
        {
            try
            {
                string linkAtual = "";
                foreach (var pegalinkAtual2 in ((System.Windows.Forms.HtmlElement)(link)).GetElementsByTagName("A"))
                {
                    linkAtual = ((System.Windows.Forms.HtmlElement)(pegalinkAtual2)).OuterHtml.Substring(startIndex, length);
                    break;
                }
                return linkAtual;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public static FormularioAtualizacaoObjetosCompleto RetornaComponentes()
        {
            FormularioAtualizacaoObjetosCompleto formularioAtualizacaoObjetosCompleto;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioAtualizacaoObjetosCompleto")
                {
                    formularioAtualizacaoObjetosCompleto = (FormularioAtualizacaoObjetosCompleto)item;
                    return (FormularioAtualizacaoObjetosCompleto)item;
                }
            }
            return null;
        }

        private StringBuilder textoTextBox = new StringBuilder();
        private void EscreveTextoTextBox(string mensagem)
        {
            textoTextBox.AppendLine(mensagem);
            textBox1.Text = textoTextBox.ToString();
            textBox1.ScrollToCaret();
        }

        private void FormularioAtualizacaoObjetosCompleto_KeyDown(object sender, KeyEventArgs e)
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
