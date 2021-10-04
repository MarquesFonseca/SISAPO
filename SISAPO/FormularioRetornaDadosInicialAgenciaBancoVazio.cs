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
    public partial class FormularioRetornaDadosInicialAgenciaBancoVazio : Form
    {
        public bool abortarAtualizacao = false;
        private static StringBuilder textoConsulta = new StringBuilder();
        private string CodigoObjetoAtual = string.Empty;
        public List<string> ListaLinksJavaScript = new List<string>();
        private bool UltimoElemento = false;
        private enum TipoTela { Rastreamento1, ListaObjetos2, DetalhesDeObjetos3, NomeCliente4 }

        private TipoTela tipoTela = TipoTela.Rastreamento1;
        private bool DetalhesDeObjetos3 = false;
        private string TelaDetalhesDeObjetos_2_3 = @"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\Rastreamento\05062019\OH239391285BR_DETALHES.htm";
        private string TelaNomeCliente_2_4 = @"C:\Users\MARQUES\Documents\Visual Studio 2010\Projects\SISAPO\SISAPO\bin\Debug\TelasRastreamento\Rastreamento_Unificado_htm.htm";

        private string enderecoSRO = @"" + Configuracoes.EnderecosSRO["EnderecoSROPorObjeto"].ToString();

        public string CidadeAgenciaLinha = string.Empty;
        public string UFAgenciaLinha = string.Empty;
        public string NomeAgenciaLinha = string.Empty;
        public string EnderecoAgenciaLinha = string.Empty;


        public FormularioRetornaDadosInicialAgenciaBancoVazio(string codigoObjetoIniciado)
        {
            InitializeComponent();
            CodigoObjetoAtual = codigoObjetoIniciado;
            textoConsulta = new StringBuilder();
            ListaLinksJavaScript = new List<string>();
            UltimoElemento = false;

            DetalhesDeObjetos3 = false;

            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
            {
                // caminho que define o teste
                //caminho para tela de consulta atraves de códigos de objetos rastreadores
                //tipoTela = TipoTela.Rastreamento1;
                //webBrowser1.Url = new Uri(TelaRastreamento_1_1);
                CodigoObjetoAtual = "BY059347974BR";
                string TelaNomeCliente_2_4 = @"C:\Users\MARQUES\Downloads\BY059347974BR_AC_LUZIMANGUES.htm";
                //tipoTela = TipoTela.NomeCliente4;
                tipoTela = TipoTela.DetalhesDeObjetos3;
                webBrowser1.Url = new Uri(TelaNomeCliente_2_4);
            }
            if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
            {
                //caminho para tela de consulta atraves de códigos de objetos rastreadores
                tipoTela = TipoTela.DetalhesDeObjetos3;
                webBrowser1.Url = new Uri(enderecoSRO + codigoObjetoIniciado);
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
                        DataSet DsCliente = new DataSet();
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
                                //CodigoObjetoAtual = "LE253372517SE";
                                if (Configuracoes.SeEAoRemetenteAtualizacaoObjetosSaiuParaEntrega != null &&
                                    Configuracoes.SeEAoRemetenteAtualizacaoObjetosSaiuParaEntrega.AsEnumerable().Any(t => t.Key == CodigoObjetoAtual)) //existe?
                                {
                                    SeEAoRemetente = Configuracoes.SeEAoRemetenteAtualizacaoObjetosSaiuParaEntrega[CodigoObjetoAtual];
                                }

                                Comentario = webBrowser1.Document.GetElementsByTagName("TR")[i].InnerText.Replace("Comentário: ", "");
                                Comentario = Comentario.RemoveAcentos().ToUpper().Trim();
                                Comentario = Configuracoes.RetornaCaixaPostalCorrigidaDefeitoString(Comentario);
                                Comentario = Configuracoes.RetornaAoRemetenteCorrigidaDefeitoString(Comentario);

                                if (string.IsNullOrEmpty(Comentario))
                                {
                                    #region TrataTipo SC PC (A COBRAR)
                                    DataRow drTipoPostal = FormularioPrincipal.TiposPostais.AsEnumerable().First(T => T["Sigla"].Equals(CodigoObjetoAtual.Substring(0, 2))); //["Código"] - Pega linha retornada dos tipos postais vinda do Excel
                                    string descricao = drTipoPostal["Descricao"].ToString().ToUpper().RemoveAcentos();
                                    if (descricao.Contains("A COBRAR") ||
                                        descricao.Contains("PAGAMENTO NA ENTREGA") ||
                                        descricao.Contains("PAGAMENTO ENTREGA"))
                                    {
                                        Comentario = "A COBRAR";
                                        if (SeEAoRemetente)
                                            Comentario = "A COBRAR - AO REMETENTE";
                                    }
                                    else
                                    {
                                        if (SeEAoRemetente)
                                        {
                                            Comentario = string.Format("AO REMETENTE");
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    if (SeEAoRemetente)
                                    {
                                        if (!Comentario.Contains("AO REMETENTE"))
                                        {
                                            Comentario = string.Format("{0} - AO REMETENTE", Comentario);
                                        }
                                    }
                                }

                                Comentario = Comentario.Replace("AO REMETENTE - AO REMETENTE", "AO REMETENTE");
                                Comentario = Comentario.Replace("CAIXA POSTALL", "CAIXA POSTAL");

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

                        #region Retorna dados Objeto Atual (CodigoObjetoAtual)
                        using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                        {
                            if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                            DsCliente = dao.RetornaDataSet("SELECT TOP 1 Codigo, CodigoObjeto, CodigoLdi, NomeCliente, DataLancamento, Atualizado, ObjetoEntregue, CaixaPostal, MunicipioLOEC, EnderecoLOEC, BairroLOEC, LocalidadeLOEC, Comentario, TipoPostalServico, TipoPostalSiglaCodigo, TipoPostalNomeSiglaCodigo, TipoPostalPrazoDiasCorridosRegulamentado FROM TabelaObjetosSROLocal WHERE (CodigoObjeto = @CodigoObjeto) ORDER BY Codigo DESC", new Parametros { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjetoAtual });
                        }
                        if (DsCliente.Tables[0].Rows.Count == 0) break;
                        #endregion

                        #region Tratamento NomeCliente
                        NomeCliente = NomeCliente.Trim() == "" ? DsCliente.Tables[0].Rows[0]["NomeCliente"].ToString().ToUpper().RemoveAcentos() : NomeCliente.Trim().ToUpper().RemoveAcentos();
                        //NomeCliente = NomeCliente.Replace("- " + Comentario, "").Trim();//evita repetir o mesmo comentario varias vezes

                        //if (!string.IsNullOrEmpty(NomeCliente))//se o NomeCliente for fazio.... não colocar o Comentario (se não ficaria assim: " - PCT") / Eu quero assim: "" (sem nada já que não tem nome...)
                        //{
                        //    NomeCliente = string.Format("{0} - {1}", NomeCliente, Comentario);
                        //}

                        #endregion

                        #region Tratamento Comentario
                        Comentario = Comentario.Trim() == "" ? DsCliente.Tables[0].Rows[0]["Comentario"].ToString().ToUpper().RemoveAcentos() : Comentario.Trim().ToUpper().RemoveAcentos();
                        //NomeCliente = NomeCliente.Replace("- " + Comentario, "").Trim();//evita repetir o mesmo comentario varias vezes

                        //if (!string.IsNullOrEmpty(NomeCliente))//se o NomeCliente for fazio.... não colocar o Comentario (se não ficaria assim: " - PCT") / Eu quero assim: "" (sem nada já que não tem nome...)
                        //{
                        //    NomeCliente = string.Format("{0} - {1}", NomeCliente, Comentario);
                        //}

                        #endregion

                        #region Tratamento SeECaixaPostal
                        SeECaixaPostal = Convert.ToBoolean(DsCliente.Tables[0].Rows[0]["CaixaPostal"]);
                        SeECaixaPostal = !SeECaixaPostal ? Configuracoes.RetornaSeECaixaPostal(NomeCliente) : SeECaixaPostal;
                        #endregion

                        #region Tratamento SeEAoRemetente
                        SeEAoRemetente = !SeEAoRemetente ? Configuracoes.RetornaSeEAoRemetente(NomeCliente) : SeEAoRemetente;
                        #endregion

                        #region Tratamento do Endereço
                        EnderecoLOEC = string.IsNullOrEmpty(EnderecoLOEC) ? DsCliente.Tables[0].Rows[0]["EnderecoLOEC"].ToString().RemoveAcentos().ToUpper() : EnderecoLOEC;
                        EnderecoLOEC = (SeECaixaPostal && string.IsNullOrEmpty(EnderecoLOEC)) ? Comentario : EnderecoLOEC;

                        BairroLOEC = string.IsNullOrEmpty(BairroLOEC) ? DsCliente.Tables[0].Rows[0]["BairroLOEC"].ToString().RemoveAcentos().ToUpper() : BairroLOEC;

                        LocalidadeLOECCEP = string.IsNullOrEmpty(LocalidadeLOECCEP) ? DsCliente.Tables[0].Rows[0]["LocalidadeLOEC"].ToString().RemoveAcentos().ToUpper() : LocalidadeLOECCEP;
                        LocalidadeLOECCEP = (SeECaixaPostal && string.IsNullOrEmpty(LocalidadeLOECCEP)) ? Configuracoes.DadosAgencia.Tables[0].Rows[0]["CepUnidade"].ToString().RemoveAcentos().ToUpper() : LocalidadeLOECCEP;

                        MunicipioLOEC = (string.IsNullOrEmpty(MunicipioLOEC) || MunicipioLOEC == "/") ? DsCliente.Tables[0].Rows[0]["MunicipioLOEC"].ToString().RemoveAcentos().ToUpper() : MunicipioLOEC;
                        MunicipioLOEC = ((SeECaixaPostal && string.IsNullOrEmpty(MunicipioLOEC)) || (SeECaixaPostal && MunicipioLOEC == "/"))
                            ? string.Format("{0} / {1}", Configuracoes.DadosAgencia.Tables[0].Rows[0]["CidadeAgenciaLocal"].ToString().RemoveAcentos().ToUpper(), Configuracoes.DadosAgencia.Tables[0].Rows[0]["UFAgenciaLocal"].ToString().RemoveAcentos().ToUpper())
                            : MunicipioLOEC;
                        #endregion

                        #region Tratamento TipoPostalPrazoDiasCorridosRegulamentado
                        TipoPostalPrazoDiasCorridosRegulamentado = Configuracoes.RetornaTipoPostalPrazoDiasCorridosRegulamentado(CodigoObjetoAtual, SeEAoRemetente, SeECaixaPostal, ref TipoPostalServico, ref TipoPostalSiglaCodigo, ref TipoPostalNomeSiglaCodigo);
                        #endregion

                        #region Grava no Banco
                        using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                        {
                            if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

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
                                            new Parametros("@DataLancamento", TipoCampo.Text, string.IsNullOrEmpty(DataLancamento) ? DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") : DataLancamento),
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
                        }
                        #endregion
                        this.Close();
                        break;


                    //if (DetalhesDeObjetos3)
                    //{
                    //    if (ListaLinksJavaScript.Count == 0)
                    //    {
                    //        DetalhesDeObjetos3 = false;
                    //        Mensagens.InformaDesenvolvedor("Vou fechar... Já peguei os dados da tela nome... ");
                    //        this.Close();
                    //    }
                    //    if (ListaLinksJavaScript.Count > 0)
                    //    {
                    //        DetalhesDeObjetos3 = true;
                    //        tipoTela = TipoTela.DetalhesDeObjetos3;
                    //        if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento)
                    //        {
                    //            webBrowser1.Url = new Uri(TelaDetalhesDeObjetos_2_3);
                    //        }
                    //        if (Configuracoes.TipoAmbiente == TipoAmbiente.Producao)
                    //        {
                    //            for (int i = 0; i < 7; i++) SendKeys.Send("{TAB}");
                    //            SendKeys.Send("{ENTER}");
                    //            //webBrowser1.GoBack();
                    //        }
                    //    }
                    //}
                    //break;
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
        private DataTable DtTipoBaixaTelaAtual;
        private void CriaDataTableTiposBaixasTelaAtual()
        {
            DtTipoBaixaTelaAtual = new DataTable();
            DtTipoBaixaTelaAtual.Clear();
            DtTipoBaixaTelaAtual.Columns.Add("DataBaixa");
            DtTipoBaixaTelaAtual.Columns.Add("DescricaoBaixa");
            DtTipoBaixaTelaAtual.Columns.Add("LinkBaixa");
        }
        private DataTable AddItensDataTableTiposBaixasTelaAtual(DataTable dt, string DataBaixa, string DescricaoBaixa, string LinkBaixa)
        {
            object[] Objeto = { DataBaixa, DescricaoBaixa, LinkBaixa };
            object[] o = { "Ravi", 500 };
            dt.Rows.Add(o);
            return dt;
        }

        private void SeparaLinksDosObjetosRastreados()
        {
            string CidadeAgenciaLocal = Configuracoes.DadosAgencia.Tables[0].Rows[0]["CidadeAgenciaLocal"].ToString().RemoveAcentos().ToUpper().Trim();
            string UFAgenciaLocal = Configuracoes.DadosAgencia.Tables[0].Rows[0]["UFAgenciaLocal"].ToString().RemoveAcentos().ToUpper().Trim();
            string NomeAgenciaLocal = Configuracoes.DadosAgencia.Tables[0].Rows[0]["NomeAgenciaLocal"].ToString().RemoveAcentos().ToUpper().Trim();

            bool objetoJaEntregue = false;
            ListaLinksJavaScript = new List<string>();

            var itensss = webBrowser1.Document.All.Cast<HtmlElement>().AsEnumerable();
            var linhas = itensss.Cast<HtmlElement>().AsEnumerable().Where(x => x.TagName.ToUpper() == "TR");

            foreach (HtmlElement item in linhas)
            {
                if (item.InnerText == null || string.IsNullOrEmpty(item.InnerText.Trim())) continue;
                string LinhaAtual = item.InnerText.Trim().ToUpper().RemoveAcentos();
                if (!VerificaSeELinhaDesejada(LinhaAtual)) continue;                

                //daqui para baixo sai imediatamente break
                #region "Disponível em Caixa Postal"
                if (LinhaAtual.Contains("Disponivel em Caixa Postal".RemoveAcentos().ToUpper()) ||
                    LinhaAtual.Contains("Aguardando retirada em Caixa Postal".RemoveAcentos().ToUpper()))
                {
                    CidadeAgenciaLinha = string.Empty;
                    UFAgenciaLinha = string.Empty;
                    NomeAgenciaLinha = string.Empty;
                    NomeAgenciaLinha = RetornaDadosAgencia(LinhaAtual, ref CidadeAgenciaLinha, ref UFAgenciaLinha);
                    //break;
                }
                #endregion

                #region "Aguardando retirada"
                if (LinhaAtual.Contains("Aguardando retirada".RemoveAcentos().ToUpper()))
                {
                    CidadeAgenciaLinha = string.Empty;
                    UFAgenciaLinha = string.Empty;
                    NomeAgenciaLinha = string.Empty;
                    NomeAgenciaLinha = RetornaDadosAgencia(LinhaAtual, ref CidadeAgenciaLinha, ref UFAgenciaLinha);
                    //break;
                }
                #endregion

                #region "Aguardando retirada - Área sem entrega"
                if (LinhaAtual.Contains("Aguardando retirada - Area sem entrega".RemoveAcentos().ToUpper()))
                {
                    CidadeAgenciaLinha = string.Empty;
                    UFAgenciaLinha = string.Empty;
                    NomeAgenciaLinha = string.Empty;
                    NomeAgenciaLinha = RetornaDadosAgencia(LinhaAtual, ref CidadeAgenciaLinha, ref UFAgenciaLinha);
                    //break;
                }
                #endregion

                #region "Endereço"
                if (LinhaAtual.Contains("Endereço".RemoveAcentos().ToUpper()))
                {
                    EnderecoAgenciaLinha = LinhaAtual.Replace("ENDERECO::","").Trim().ToUpper();
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

        private string RetornaDadosAgencia(string linhaAtual, ref string cidadeAgenciaLinha, ref string uFAgenciaLinha)
        {
            string agenciaLinha = string.Empty;
            string cidadeLinha = string.Empty;
            string estadoLinha = string.Empty;

            if (linhaAtual.Length >= 19)
            {
                //string agenciaCidadeEstadoLinha = linhaAtual.Remove(0, 19).Trim().ToUpper();
                //agenciaLinha = agenciaCidadeEstadoLinha.Split('-')[0].Trim().ToUpper();
                //cidadeLinha = agenciaCidadeEstadoLinha.Split('/')[0].Trim().ToUpper();
                //cidadeLinha = cidadeLinha.Split('-')[1].Trim().ToUpper();
                //estadoLinha = agenciaCidadeEstadoLinha.Split('/')[1].Trim().ToUpper();
                //estadoLinha = estadoLinha.Substring(0, 2);

                string agenciaCidadeEstadoLinha = linhaAtual.Remove(0, 19).Trim().ToUpper();
                agenciaLinha = agenciaCidadeEstadoLinha.Split('-')[0].Trim().ToUpper();
                string tempCidadeLinha = agenciaCidadeEstadoLinha.Split('-')[1].Trim().ToUpper();
                cidadeLinha = tempCidadeLinha.Split('/')[0].Trim().ToUpper();
                //cidadeLinha = tempCidadeLinha.Split('-')[1].Trim().ToUpper();
                estadoLinha = tempCidadeLinha.Split('/')[1].Trim().ToUpper();
                //estadoLinha = agenciaCidadeEstadoLinha.Split('/')[1].Trim().ToUpper();
                estadoLinha = estadoLinha.Substring(0, 2);
            }

            cidadeAgenciaLinha = cidadeLinha;
            uFAgenciaLinha = estadoLinha;
            return agenciaLinha;
        }

        private bool VerificaAgenciaAtual(string linhaAtual, string cidadeAgenciaLocal, string uFAgenciaLocal, string nomeAgenciaLocal)
        {
            bool retono = false;
            string agenciaLinha = string.Empty;
            string cidadeLinha = string.Empty;
            string estadoLinha = string.Empty;

            if (linhaAtual.Length >= 19)
            {
                string agenciaCidadeEstadoLinha = linhaAtual.Remove(0, 19).Trim().ToUpper();
                agenciaLinha = agenciaCidadeEstadoLinha.Split('-')[0].Trim().ToUpper();
                cidadeLinha = agenciaCidadeEstadoLinha.Split('/')[0].Trim().ToUpper();
                cidadeLinha = cidadeLinha.Split('-')[1].Trim().ToUpper();
                estadoLinha = agenciaCidadeEstadoLinha.Split('/')[1].Trim().ToUpper();
                estadoLinha = estadoLinha.Substring(0, 2);
            }

            if (nomeAgenciaLocal == agenciaLinha &&
                cidadeAgenciaLocal == cidadeLinha &&
                uFAgenciaLocal == estadoLinha)
            {
                retono = true;
            }
            else
            {
                retono = false;
            }

            return retono;
        }

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
                                    if (linhaAtual.Contains("Objeto expedido".RemoveAcentos().ToUpper()) == false)
                                    {
                                        if (linhaAtual.Contains("Não procurado pelo destinatário".RemoveAcentos().ToUpper()) == false)
                                        {
                                            if (linhaAtual.Contains("Não procurado pelo remetente".RemoveAcentos().ToUpper()) == false)
                                            {
                                                if (linhaAtual.Contains("Endereço:".RemoveAcentos().ToUpper()) == false)
                                                {
                                                    return false;
                                                }
                                            }
                                        }
                                    }
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