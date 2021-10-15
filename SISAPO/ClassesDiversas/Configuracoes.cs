using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
//using Rensoftware.Classes;
//using Rensoftware.Classes.ClassesEstaticas;
using SISAPO.ClassesDiversas;
using System.Collections.Generic;
//using Kemuel.Formularios;
using System.Diagnostics;

namespace SISAPO.ClassesDiversas
{
    public static class Configuracoes
    {

#if DEBUG
        //private static string _strConexao = System.Configuration.ConfigurationManager.ConnectionStrings["cadastroConnectionString"].ConnectionString;
        private static string _strConexao = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source =..\\debug\\cadastro.mdb;Persist Security Info=True";
#endif
#if !DEBUG
        private static string _strConexao = System.Configuration.ConfigurationManager.ConnectionStrings["cadastroConnectionString"].ConnectionString + ";Jet OLEDB:Database Password=9342456;";
#endif

        public static bool GerarQRCodePLRNaLdi { get; internal set; }
        public static bool ACCAgenciaComunitaria { get; internal set; }
        public static bool ReceberObjetosViaQRCodePLRDaAgenciaMae { get; internal set; }
        public static string EmailsAgenciaMae { get; internal set; }
        public static bool GerarTXTPLRNaLdi { get; internal set; }
        public static bool ReceberObjetosViaTXTPLRDaAgenciaMae { get; internal set; }

        public static string strConexao
        {
            get { return _strConexao; }
            set { _strConexao = value; }
        }


        private static TipoAmbiente _tipoAmbiente;
        public static TipoAmbiente TipoAmbiente
        {
            get { return _tipoAmbiente; }
            set { _tipoAmbiente = value; }
        }

        private static string _statusPrincipal = "";
        public static string StatusPrincipal
        {
            get { return _statusPrincipal; }
            set { _statusPrincipal = value; }
        }

        public static DataTable RetornaTiposPostaisPlan()
        {
            DataTable retorno = new DataTable();

            string NomeEndereco = string.Format(@"{0}Tipos_Postais.xls", System.AppDomain.CurrentDomain.BaseDirectory);
            string NomePlanilha = string.Format("{0}$", "Planilha1");
            string ColunasBusca = string.Format(""
                + "[Serviço],"
                + "[Código],"
                + "[Nome],"
                + "[Prazo dias corridos no destino (Caixa Postal)],"
                + "[Prazo dias corridos no destino (Caída/Pedida)],"
                + "[Prazo dias corridos na origem/devolução/remetente (Caixa Postal)],"
                + "[Prazo dias corridos na origem/devolução/remetente (Caída/Pedida)]"
                + "");
            int QtdTopSelect = 0;
            try
            {
                using (DataTable dt = new ImportarArquivos().ImportarXLSXNovo(NomeEndereco, NomePlanilha, ColunasBusca, QtdTopSelect))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        retorno = dt;
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível carregar o arquivo Tipos_Postais.xls.\nIsso acarretará no prazo de cada tipo de objeto importado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Não foi possível carregar o arquivo: {0}", ex.Message));
            }

            return retorno;
        }

        public static DataTable RetornaTiposPostais()
        {
            DataTable retorno = new DataTable();
            //retorna
            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return null; }

                    string stringSQL = "SELECT Codigo, Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao FROM TiposPostais";
                    retorno = dao.RetornaDataTable(stringSQL.ToString());
                }
                return retorno;
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
                return retorno;
            }
        }

        public static void MetodoDeTesteQualquer()
        {
            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                    string dataInicial_dateTimePickerAnoMesDiaFormatado = "11/05/2021";
                    List<Parametros> pr3 = new List<Parametros>() {
                                        new Parametros() { Nome = "@PrazoDestinoCaixaPostal", Tipo = TipoCampo.Text, Valor = 30 },
                                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = "QE640052237BR" },
                                        new Parametros() { Nome = "@DataLancamento", Tipo = TipoCampo.Text, Valor = dataInicial_dateTimePickerAnoMesDiaFormatado }
                                    };
                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET TipoPostalPrazoDiasCorridosRegulamentado = @PrazoDestinoCaixaPostal WHERE (CodigoObjeto = @CodigoObjeto) AND (DateValue(DataLancamento) >= DateValue(@DataLancamento))", pr3);
                }
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
            }


            //try
            //{
            //    using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
            //    {
            //        if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
            //        dao.ExecutaSQL(@"UPDATE TabelaObjetosSROLocal SET 
            //                                Atualizado = @Atualizado
            //                                WHERE CodigoObjeto = @CodigoObjeto ", new List<Parametros>(){
            //                                new Parametros("@Atualizado", TipoCampo.Boolean, true),
            //                                new Parametros("@CodigoObjeto", TipoCampo.Text, "BE165192894BR")});
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Mensagens.Erro(ex.Message);
            //}
        }

        public static string ReceberEmailsAgenciaMae()
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return ""; }

                Configuracoes.EmailsAgenciaMae = Convert.ToString(dao.RetornaValor("SELECT TOP 1 EmailsAgenciaMae FROM TabelaConfiguracoesSistema"));
            }
            return Configuracoes.EmailsAgenciaMae;
        }

        public static void SetaConfiguracoesGerarQRCodePLRNaLdi()
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                Configuracoes.GerarQRCodePLRNaLdi = Convert.ToBoolean(dao.RetornaValor("SELECT TOP 1 GerarQRCodePLRNaLdi FROM TabelaConfiguracoesSistema"));
            }
        }

        public static void SetaConfiguracoesGerarTXTPLRNaLdi()
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                Configuracoes.GerarQRCodePLRNaLdi = Convert.ToBoolean(dao.RetornaValor("SELECT TOP 1 GerarTXTPLRNaLdi FROM TabelaConfiguracoesSistema"));
            }
        }

        public static void SetaConfiguracoesACCAgenciaComunitaria()
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                Configuracoes.ACCAgenciaComunitaria = Convert.ToBoolean(dao.RetornaValor("SELECT TOP 1 ACCAgenciaComunitaria FROM TabelaConfiguracoesSistema"));
            }
        }

        public static void SetaConfiguracoesReceberObjetosViaQRCodePLRDaAgenciaMae()
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                Configuracoes.ReceberObjetosViaQRCodePLRDaAgenciaMae = Convert.ToBoolean(dao.RetornaValor("SELECT TOP 1 ReceberObjetosViaQRCodePLRDaAgenciaMae FROM TabelaConfiguracoesSistema"));
            }
        }

        public static void SetaConfiguracoesReceberObjetosViaTXTPLRDaAgenciaMae()
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                Configuracoes.ReceberObjetosViaQRCodePLRDaAgenciaMae = Convert.ToBoolean(dao.RetornaValor("SELECT TOP 1 ReceberObjetosViaTXTPLRDaAgenciaMae FROM TabelaConfiguracoesSistema"));
            }
        }

        public static void VerificaAplicacaoSeAberta()
        {

        }

        public static void VerificaAplicacaoSeAbertaFechar()
        {
            var processo = System.Diagnostics.Process.GetCurrentProcess();
            string nome = Environment.UserName;

            var jaEstaRodando = System.Diagnostics.Process.GetProcessesByName(processo.ProcessName).Any(p => p.Id != processo.Id);
            string lllj = Process.GetProcessById(processo.Id).StartInfo.UserName;
            //Mensagens.Informa(Dns.GetHostEntry(Environment.MachineName).HostName);

            if (jaEstaRodando)
            {
                DialogResult retorno = Mensagens.Pergunta("Já existe uma aplicação aberta.\nFeche a aplicação anterior para abrir uma nova.\n\nDesja fechar a aplicação anterior?");
                if (retorno == DialogResult.Yes)
                {
                    if (processo.Responding)
                    {
                        //fecha todos que nao seja o atual....
                        //((Process)(System.Diagnostics.Process.GetProcessesByName(processo.ProcessName).Where(p => p.Id != processo.Id))).Kill();

                        foreach (Process itens in System.Diagnostics.Process.GetProcessesByName(processo.ProcessName).Where(p => p.Id != processo.Id))
                        {
                            itens.Kill();
                            //itens.CloseMainWindow();
                        }
                        foreach (Process itens in Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName))
                        {
                            itens.Kill();
                            //itens.CloseMainWindow();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Status Processo do NotePad = Não Respondendo");
                        return;
                    }
                    return;
                }
                return;
            }
        }

        public static bool ExibicaoMensagensParaDesenvolvedor = false;

        public static void GeraArquivoConfig()
        {
            string data = ClassesDiversas.CriptografiaHelper.Criptografa("30-06-2019");

            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeArquivo = string.Format("Config");
            if (!Arquivos.Existe(string.Format("{0}\\{1}.XML", curDir, nomeArquivo), false))
            {
                DataTable dt = new DataTable("Config");
                dt.Columns.Add("Nome", typeof(string));
                dt.Columns.Add("Valor", typeof(string));
                DataRow dr = dt.NewRow();
                dr["Nome"] = "FecharAplicacoesAbertas";
                dr["Valor"] = 0;
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Nome"] = "ChaveAcesso";
                //dr["Valor"] = ClassesDiversas.CriptografiaHelper.Criptografa("30-09-2019");
                dr["Valor"] = "tBfC6CeoTmSvL1C7B34jqw=="; //ClassesDiversas.CriptografiaHelper.Criptografa("30-09-2019");

                dt.Rows.Add(dr);

                ClassesDiversas.ArquivoXML.GravaArquivoXML(dt, curDir, nomeArquivo);
            }
        }

        public static void VerificaSeFecharAplicacaoParaAtualizacao()
        {
            #region verifica se é para fechar todas as aplicações para atualizar....
            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeArquivo = string.Format("Config");
            if (!Arquivos.Existe(string.Format("{0}\\{1}.XML", curDir, nomeArquivo), false)) Configuracoes.GeraArquivoConfig();
            DataTable dt = ClassesDiversas.ArquivoXML.AbrirArquivoXML(curDir, nomeArquivo);
            if (dt.Rows.Count >= 1)
            {
                if (dt.Rows[0]["Valor"].ToString() == "1")
                {
                    var processo = System.Diagnostics.Process.GetCurrentProcess();
                    foreach (System.Diagnostics.Process itens in System.Diagnostics.Process.GetProcessesByName(processo.ProcessName).Where(p => p.Id != processo.Id))
                    {
                        itens.Kill();
                    }
                    foreach (System.Diagnostics.Process itens in System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName))
                    {
                        itens.Kill();
                    }
                }
            }
            #endregion
        }

        public static bool VerificaChaveAcesso()
        {
            //string data = "30-09-2019";
            string valorCriptografado = "";

            #region Retorna data do banco
            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string nomeArquivo = string.Format("Config");
            if (!Arquivos.Existe(string.Format("{0}\\{1}.XML", curDir, nomeArquivo), false)) Configuracoes.GeraArquivoConfig();
            DataTable dt = ClassesDiversas.ArquivoXML.AbrirArquivoXML(curDir, nomeArquivo);
            if (dt.Rows.Count >= 1)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item["Nome"].ToString() == "ChaveAcesso")
                    {
                        valorCriptografado = item["Valor"].ToString();
                        break;
                    }
                }
            }
            #endregion

            try
            {
                string DataRetornadaBancoDados = ClassesDiversas.CriptografiaHelper.Descriptografa(valorCriptografado);
                if (DateTime.Now.Date.ToShortDateString().ToDateTime().Date <= string.Format("{0:dd/MM/yyyy}", DataRetornadaBancoDados).ToDateTime())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string RetornaTipoPostalPrazoDiasCorridosRegulamentado(string _CodigoObjetoAtual, bool SeEAoRemetente, bool SeECaixaPostal, ref string TipoPostalServico, ref string TipoPostalSiglaCodigo, ref string TipoPostalNomeSiglaCodigo)
        {
            string TipoPostalPrazoDiasCorridosRegulamentado = "";
            try
            {
                if (FormularioPrincipal.TiposPostais.Rows.Count > 0)
                {
                    if (_CodigoObjetoAtual == "") return "";

                    bool existe = FormularioPrincipal.TiposPostais.AsEnumerable().Any(T => T["Sigla"].Equals(_CodigoObjetoAtual.Substring(0, 2)));
                    if (!existe) return "";
                    // existe
                    DataRow drTipoPostal = FormularioPrincipal.TiposPostais.AsEnumerable().First(T => T["Sigla"].Equals(_CodigoObjetoAtual.Substring(0, 2)));
                    if (drTipoPostal == null) return "";

                    //Exemplo "LB327263658SE"
                    //[0] - Serviço: NAO URGENTE 
                    //[1] - Código: LB 
                    //[2] - Nome: OBJETO INTERNACIONAL PRIME 
                    //[3] - Prazo dias corridos no destino (Caixa Postal): 30 
                    //[4] - Prazo dias corridos no destino (Caída/Pedida): 20 
                    //[5] - Prazo dias corridos na origem/devolução/remetente (Caixa Postal): 20 
                    //[6] - Prazo dias corridos na origem/devolução/remetente (Caída/Pedida): 20

                    TipoPostalServico = drTipoPostal["Servico"].ToString();
                    TipoPostalSiglaCodigo = drTipoPostal["Sigla"].ToString();
                    TipoPostalNomeSiglaCodigo = drTipoPostal["Descricao"].ToString();

                    #region Se for Caixa Postal e Não for Ao remetente
                    if (SeECaixaPostal && !SeEAoRemetente)
                    {
                        // Pega campo "Prazo dias corridos no destino (Caixa Postal)"
                        TipoPostalPrazoDiasCorridosRegulamentado = drTipoPostal["PrazoDestinoCaixaPostal"].ToString();
                    }
                    #endregion
                    #region Se for Caixa Postal e Se for Ao remetente
                    if (SeECaixaPostal && SeEAoRemetente)
                    {
                        // Pega campo "Prazo dias corridos na origem/devolução/remetente (Caixa Postal)"
                        TipoPostalPrazoDiasCorridosRegulamentado = drTipoPostal["PrazoRemetenteCaixaPostal"].ToString();
                    }
                    #endregion
                    #region Se Não for Caixa Postal && Não for Ao remetente
                    if (!SeECaixaPostal && !SeEAoRemetente)
                    {
                        // Pega campo "Prazo dias corridos no destino (Caída/Pedida)"
                        TipoPostalPrazoDiasCorridosRegulamentado = drTipoPostal["PrazoDestinoCaidaPedida"].ToString();
                    }
                    #endregion
                    #region Se Não for Caixa Postal && Se for Ao remetente
                    if (!SeECaixaPostal && SeEAoRemetente)
                    {
                        // Pega campo "Prazo dias corridos na origem/devolução/remetente (Caída/Pedida)"
                        TipoPostalPrazoDiasCorridosRegulamentado = drTipoPostal["PrazoRemetenteCaidaPedida"].ToString();
                    }
                    #endregion
                }
                return TipoPostalPrazoDiasCorridosRegulamentado;
            }
            catch (Exception)
            {
                return TipoPostalPrazoDiasCorridosRegulamentado;
            }
        }

        /// <summary>
        /// Mensagem 'A conexão com o banco de dados foi perdida.'
        /// </summary>
        public static string MensagemPerdaConexao = "A conexão com o banco de dados foi perdida.";

        public static void LimpaBancoTornaBancoVazio()
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao())
                {
                    FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao;
                    return;
                }
                dao.ExecutaSQL("DELETE FROM TabelaHistoricoConsulta");
                dao.ExecutaSQL("ALTER TABLE TabelaHistoricoConsulta ALTER COLUMN Codigo COUNTER(1, 1)");

                dao.ExecutaSQL("DELETE FROM TabelaObjetosSROLocal");
                dao.ExecutaSQL("ALTER TABLE TabelaObjetosSROLocal ALTER COLUMN Codigo COUNTER(1, 1)");
            }
        }

        public static void VerificaSquemaBancoDados()
        {
            //CriaColuna("TabelaConfiguracoesSistema", "Codigo", "INTEGER");//Codigo
            CriaColuna("TabelaConfiguracoesSistema", "NomeAgenciaLocal", "TEXT(255) NULL DEFAULT NULL");//NomeAgenciaLocal
            CriaColuna("TabelaConfiguracoesSistema", "EnderecoAgenciaLocal", "TEXT(255) NULL DEFAULT NULL");//EnderecoAgenciaLocal
            CriaColuna("TabelaConfiguracoesSistema", "DataInicialPeriodoExibicaoConsulta", "DATETIME NULL DEFAULT NULL");//DataInicialPeriodoExibicaoConsulta
            CriaColuna("TabelaConfiguracoesSistema", "DataFinalPeriodoExibicaoConsulta", "DATETIME NULL DEFAULT NULL");//DataFinalPeriodoExibicaoConsulta
            CriaColuna("TabelaConfiguracoesSistema", "EnderecoSRO", "TEXT(255) NULL DEFAULT NULL");//EnderecoSRO
            CriaColuna("TabelaConfiguracoesSistema", "EnderecoSROPorObjeto", "TEXT(255) NULL DEFAULT NULL");//EnderecoSROPorObjeto
            SetaCamposEnderecoSRO();



            CriaColuna("TabelaConfiguracoesSistema", "ExibirObjetosEmCaixaPostalNaPesquisa", "YESNO NULL DEFAULT 1");//ExibirObjetosEmCaixaPostalNaPesquisa
            CriaColuna("TabelaConfiguracoesSistema", "ExibirObjetosJaEntreguesNaPesquisa", "YESNO NULL DEFAULT 1");//ExibirObjetosJaEntreguesNaPesquisa
            CriaColuna("TabelaConfiguracoesSistema", "ManterConsultaSempreAtualizada", "YESNO NULL DEFAULT 1");//ManterConsultaSempreAtualizada
            CriaColuna("TabelaConfiguracoesSistema", "TempoAtualizacaoConsultaSempreAtualizada", "TEXT(255) NULL DEFAULT 600000");//TempoAtualizacaoConsultaSempreAtualizada
            CriaColuna("TabelaConfiguracoesSistema", "PermitirBuscarPorLDINaPesquisa", "YESNO NULL DEFAULT 0");//PermitirBuscarPorLDINaPesquisa
            CriaColuna("TabelaConfiguracoesSistema", "HabilitarCapturaDeDadosDePostagem", "YESNO NULL DEFAULT 0");//HabilitarCapturaDeDadosDePostagem
            CriaColuna("TabelaConfiguracoesSistema", "HabilitarCapturaDeDadosDeSaiuParaEntregaAoDestinatario", "YESNO NULL DEFAULT 0");//HabilitarCapturaDeDadosDeSaiuParaEntregaAoDestinatario
            CriaColuna("TabelaConfiguracoesSistema", "HabilitarCapturaDeDadosDeDestinatarioAusente", "YESNO NULL DEFAULT 0");//HabilitarCapturaDeDadosDeDestinatarioAusente
            CriaColuna("TabelaConfiguracoesSistema", "DataHoraUltimaAtualizacaoImportacao", "DATETIME NULL DEFAULT NULL");//DataHoraUltimaAtualizacaoImportacao
            CriaColuna("TabelaConfiguracoesSistema", "SuperintendenciaEstadual", "TEXT(255) NULL DEFAULT NULL");//DataInicialPeriodoExibicaoConsulta
            CriaColuna("TabelaConfiguracoesSistema", "CepUnidade", "TEXT(255) NULL DEFAULT NULL");//DataFinalPeriodoExibicaoConsulta

            CriaColuna("TabelaConfiguracoesSistema", "CidadeAgenciaLocal", "TEXT(255) NULL DEFAULT NULL");//CidadeAgenciaLocal
            CriaColuna("TabelaConfiguracoesSistema", "UFAgenciaLocal", "TEXT(255) NULL DEFAULT NULL");//UFAgenciaLocal
            CriaColuna("TabelaConfiguracoesSistema", "TelefoneAgenciaLocal", "TEXT(255) NULL DEFAULT NULL");//TelefoneAgenciaLocal
            CriaColuna("TabelaConfiguracoesSistema", "HorarioFuncionamentoAgenciaLocal", "TEXT(255) NULL DEFAULT NULL");//HorarioFuncionamentoAgenciaLocal
            CriaColuna("TabelaConfiguracoesSistema", "GerarQRCodePLRNaLdi", "YESNO NULL DEFAULT 0");//GerarQRCodePLRNaLdi
            CriaColuna("TabelaConfiguracoesSistema", "ACCAgenciaComunitaria", "YESNO NULL DEFAULT 0");//ACCAgenciaComunitaria
            CriaColuna("TabelaConfiguracoesSistema", "ReceberObjetosViaQRCodePLRDaAgenciaMae", "YESNO NULL DEFAULT 0");//ReceberObjetosViaQRCodePLRDaAgenciaMae
            CriaColuna("TabelaConfiguracoesSistema", "EmailsAgenciaMae", "TEXT(255) NULL DEFAULT NULL");//EmailsAgenciaMae
            CriaColuna("TabelaConfiguracoesSistema", "GerarTXTPLRNaLdi", "YESNO NULL DEFAULT 0");//GerarTXTPLRNaLdi
            CriaColuna("TabelaConfiguracoesSistema", "ReceberObjetosViaTXTPLRDaAgenciaMae", "YESNO NULL DEFAULT 0");//ReceberObjetosViaTXTPLRDaAgenciaMae


            //CriaColuna("TabelaHistoricoConsulta", "Codigo", "INTEGER");//Codigo
            CriaColuna("TabelaHistoricoConsulta", "CodigoObjeto", "TEXT(255) NULL DEFAULT NULL");//CodigoObjeto
            CriaColuna("TabelaHistoricoConsulta", "DataConsulta", "DATETIME NULL DEFAULT NULL");//DataConsulta
            CriaColuna("TabelaHistoricoConsulta", "DataCadastro", "DATETIME NULL DEFAULT NULL");//


            //CriaColuna("TabelaObjetosSROLocal", "Codigo", "INTEGER");//Codigo
            CriaColuna("TabelaObjetosSROLocal", "CodigoObjeto", "TEXT(255) NULL DEFAULT NULL");//CodigoObjeto
            CriaColuna("TabelaObjetosSROLocal", "CodigoLdi", "TEXT(255) NULL DEFAULT NULL");//CodigoLdi
            CriaColuna("TabelaObjetosSROLocal", "NomeCliente", "TEXT(255) NULL DEFAULT NULL");//NomeCliente
            CriaColuna("TabelaObjetosSROLocal", "DataLancamento", "TEXT(255) NULL DEFAULT NULL");//DataLancamento
            CriaColuna("TabelaObjetosSROLocal", "DataModificacao", "TEXT(255) NULL DEFAULT NULL");//DataModificacao
            CriaColuna("TabelaObjetosSROLocal", "Situacao", "TEXT(255) NULL DEFAULT NULL");//Situacao
            CriaColuna("TabelaObjetosSROLocal", "Atualizado", "YESNO NULL DEFAULT 0");//Atualizado
            CriaColuna("TabelaObjetosSROLocal", "ObjetoEntregue", "YESNO NULL DEFAULT 0");//ObjetoEntregue
            CriaColuna("TabelaObjetosSROLocal", "CaixaPostal", "YESNO NULL DEFAULT 0");//CaixaPostal
            CriaColuna("TabelaObjetosSROLocal", "UnidadePostagem", "TEXT(255) NULL DEFAULT NULL");//UnidadePostagem
            CriaColuna("TabelaObjetosSROLocal", "MunicipioPostagem", "TEXT(255) NULL DEFAULT NULL");//MunicipioPostagem
            CriaColuna("TabelaObjetosSROLocal", "CriacaoPostagem", "TEXT(255) NULL DEFAULT NULL");//CriacaoPostagem
            CriaColuna("TabelaObjetosSROLocal", "CepDestinoPostagem", "TEXT(255) NULL DEFAULT NULL");//CepDestinoPostagem
            CriaColuna("TabelaObjetosSROLocal", "ARPostagem", "TEXT(255) NULL DEFAULT NULL");//ARPostagem
            CriaColuna("TabelaObjetosSROLocal", "MPPostagem", "TEXT(255) NULL DEFAULT NULL");//MPPostagem
            CriaColuna("TabelaObjetosSROLocal", "DataMaxPrevistaEntregaPostagem", "TEXT(255) NULL DEFAULT NULL");//DataMaxPrevistaEntregaPostagem
            CriaColuna("TabelaObjetosSROLocal", "UnidadeLOEC", "TEXT(255) NULL DEFAULT NULL");//UnidadeLOEC
            CriaColuna("TabelaObjetosSROLocal", "MunicipioLOEC", "TEXT(255) NULL DEFAULT NULL");//MunicipioLOEC
            CriaColuna("TabelaObjetosSROLocal", "CriacaoLOEC", "TEXT(255) NULL DEFAULT NULL");//CriacaoLOEC
            CriaColuna("TabelaObjetosSROLocal", "CarteiroLOEC", "TEXT(255) NULL DEFAULT NULL");//CarteiroLOEC
            CriaColuna("TabelaObjetosSROLocal", "DistritoLOEC", "TEXT(255) NULL DEFAULT NULL");//DistritoLOEC
            CriaColuna("TabelaObjetosSROLocal", "NumeroLOEC", "TEXT(255) NULL DEFAULT NULL");//NumeroLOEC
            CriaColuna("TabelaObjetosSROLocal", "EnderecoLOEC", "TEXT(255) NULL DEFAULT NULL");//EnderecoLOEC
            CriaColuna("TabelaObjetosSROLocal", "BairroLOEC", "TEXT(255) NULL DEFAULT NULL");//BairroLOEC
            CriaColuna("TabelaObjetosSROLocal", "LocalidadeLOEC", "TEXT(255) NULL DEFAULT NULL");//LocalidadeLOEC
            CriaColuna("TabelaObjetosSROLocal", "SituacaoDestinatarioAusente", "TEXT(255) NULL DEFAULT NULL");//SituacaoDestinatarioAusente
            CriaColuna("TabelaObjetosSROLocal", "AgrupadoDestinatarioAusente", "TEXT(255) NULL DEFAULT NULL");//AgrupadoDestinatarioAusente
            CriaColuna("TabelaObjetosSROLocal", "CoordenadasDestinatarioAusente", "TEXT(255) NULL DEFAULT NULL");//CoordenadasDestinatarioAusente
            CriaColuna("TabelaObjetosSROLocal", "Comentario", "TEXT(255) NULL DEFAULT NULL");//Comentario
            CriaColuna("TabelaObjetosSROLocal", "TipoPostalServico", "TEXT(255) NULL DEFAULT NULL");//TipoPostalServico adicionado vindo do excel Tipos_Postais.xls filtrado pelo objeto Atual
            CriaColuna("TabelaObjetosSROLocal", "TipoPostalSiglaCodigo", "TEXT(255) NULL DEFAULT NULL");//TipoPostalSiglaCodigo  adicionado vindo do excel Tipos_Postais.xls filtrado pelo objeto Atual
            CriaColuna("TabelaObjetosSROLocal", "TipoPostalNomeSiglaCodigo", "TEXT(255) NULL DEFAULT NULL");//TipoPostalNomeSiglaCodigo  adicionado vindo do excel Tipos_Postais.xls filtrado pelo objeto Atual
            CriaColuna("TabelaObjetosSROLocal", "TipoPostalPrazoDiasCorridosRegulamentado", "TEXT(255) NULL DEFAULT NULL");//TipoPostalNomeSiglaCodigo adicionado vindo do excel Tipos_Postais.xls filtrado pelo objeto Atual

            if (VerificaSeTabelaExiste("TiposPostais") == false)//não existe a tabela... criar tabela
            {
                CriaTabela("TiposPostais");

                CriaColuna("TiposPostais", "Codigo", "AUTOINCREMENT PRIMARY KEY NOT NULL");//Codigo
                CriaColuna("TiposPostais", "Servico", "TEXT(255) NULL DEFAULT NULL");//Servico
                CriaColuna("TiposPostais", "Sigla", "TEXT(255) NULL DEFAULT NULL");//Sigla
                CriaColuna("TiposPostais", "Descricao", "TEXT(255) NULL DEFAULT NULL");//Descricao
                CriaColuna("TiposPostais", "PrazoDestinoCaidaPedida", "TEXT(255) NULL DEFAULT NULL");//PrazoDestinoCaidaPedida
                CriaColuna("TiposPostais", "PrazoDestinoCaixaPostal", "TEXT(255) NULL DEFAULT NULL");//PrazoDestinoCaixaPostal
                CriaColuna("TiposPostais", "PrazoRemetenteCaidaPedida", "TEXT(255) NULL DEFAULT NULL");//PrazoRemetenteCaidaPedida
                CriaColuna("TiposPostais", "PrazoRemetenteCaixaPostal", "TEXT(255) NULL DEFAULT NULL");//PrazoRemetenteCaixaPostal
                CriaColuna("TiposPostais", "TipoClassificacao", "TEXT(255) NULL DEFAULT NULL");//TipoClassificacao
                CriaColuna("TiposPostais", "DataAlteracao", "TEXT(255) NULL DEFAULT NULL");//DataAlteracao

                CriaTiposPostaisIniciais();
            }
        }

        public static DataSet DadosAgencia { get; set; }
        public static DataSet RetornaDadosAgencia()
        {
            DataSet ds = new DataSet();
            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    ds = dao.RetornaDataSet("SELECT TOP 1 Codigo, NomeAgenciaLocal, EnderecoAgenciaLocal, SuperintendenciaEstadual, CepUnidade, CidadeAgenciaLocal, UFAgenciaLocal, TelefoneAgenciaLocal, HorarioFuncionamentoAgenciaLocal FROM TabelaConfiguracoesSistema");
                }
                return ds;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

        }

        public static DataRow EnderecosSRO { get; set; }
        public static DataRow RetornaEnderecosSRO()
        {
            DataRow drRetornoEnderecosSRO;
            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return null; }

                    drRetornoEnderecosSRO = dao.RetornaDataRow("SELECT EnderecoSRO, EnderecoSROPorObjeto FROM TabelaConfiguracoesSistema");
                }
                return drRetornoEnderecosSRO;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        private static void CriaTiposPostaisIniciais()
        {
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"AA\", \"ETIQUETA LOGICA SEDEX AA\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"AB\", \"ETIQUETA LOGICA SEDEX AB\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"AK\", \"ETIQUETA LOGICA PAC AK\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"AL\", \"ETIQUETA LOGICA PAC AL\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"AR\", \"AVISO DE RECEBIMENTO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"AS\", \"ENCOMENDA PAC - ACAO SOCIAL\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"BE\", \"REMESSA ECONÔMICA SEM AR\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"BF\", \"REMESSA EXPRESSA S/ AR DIGITAL\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"BG\", \"ETIQUETA LOG REM ECON C/AR BG\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"BH\", \"MENSAGEM FÍSICO-DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"BI\", \"ETIQUETA LÓG REGIST URG\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"BJ\", \"ETIQ LOG REM EXPRESSA C/AR\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"BK\", \"ETIQ LÓG REM ECONÔMICA S/AR\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"BL\", \"ETIQUETA LOG REM ECON C/AR\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"BN\", \"ETIQUETA FIS REGIST URG\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"BO\", \"ETIQUETA LOG REGIST URG\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"BP\", \"ETIQUETA LOGICA REG ADM\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"BR\", \"ETIQUETA FIS REGIST URG\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"BT\", \"ETIQUETA LOG REM ECON C/ AR\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"BV\", \"ETIQUETA LÓGICA VPOST\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"BY\", \"ETIQUETA LÓGICA CARTA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"BZ\", \"ETIQUETA LÓGICA CARTA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CA\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CB\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CC\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CD\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CE\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CF\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CG\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CH\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CI\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CJ\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CK\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CL\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CM\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CN\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CO\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CP\", \"OBJETO INTERNACIONAL MERCADOR. ECONOMICA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CQ\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CR\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CS\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CT\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CU\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CV\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CW\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CX\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CY\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"CZ\", \"OBJETO INTERNACIONAL COLIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DA\", \"ENCOMENDA SEDEX C/ AR DIGITAL\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DB\", \"REMESSA EXPRESSA C/ AR DIGITAL-BRADESCO\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DC\", \"REMESSA EXPRESSA (ORGAO TRANSITO)\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DD\", \"DEVOLUÇÃO DE DOCUMENTOS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DE\", \"REMESSA EXPRESSA C/ AR DIGITAL\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DF\", \"ENCOMENDA SEDEX (ETIQ LOGICA)\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DG\", \"ENCOMENDA SEDEX (ETIQ LOGICA)\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DI\", \"REM EXPRES COM AR DIGITAL ITAU\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DJ\", \"ENCOMENDA SEDEX\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DK\", \"ETIQUETA SEDEX EXTRA GRANDE\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DL\", \"ETIQUETA SEDEX\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DM\", \"ETIQUETA FISICA SEDEX DM\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DN\", \"ENCOMENDA SEDEX\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DO\", \"ETIQ LOG SEDEX ITAU UNIB\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DP\", \"SEDEX PAGAMENTO ENTREGA\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DQ\", \"ETIQ LOG SEDEX BRADESCO\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DR\", \"REM EXPRES COM AR DIGITAL SANTANDER\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DS\", \"REM EXPRES COM AR DIGITAL SANTANDER\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"DT\", \"REGISTRADO DETRAN COM AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DU\", \"ENCOMENDA SEDEX\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DV\", \"SEDEX COM AR DIGITAL\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DW\", \"ENCOMENDA SEDEX (ETIQ LÓGICA)\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DX\", \"SEDEX 10 LÓGICO\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DY\", \"ENCOMENDA SEDEX (ETIQ FÍSICA)\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"DZ\", \"ENCOMENDA SEDEX (ETIQ LOGICA)\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EA\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EB\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EC\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"ED\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EE\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EF\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EG\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EH\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EI\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EJ\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EK\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EL\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EM\", \"OBJETO INTERNACIONAL SEDEX MUNDI\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EN\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EO\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EP\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"EQ\", \"ENCOMENDA SERVIÇO NÃO EXPRESSA ECT\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"ER\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"ES\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"ET\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EU\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EV\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EW\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EX\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EY\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"EZ\", \"OBJETO INTERNACIONAL EMS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"FA\", \"FAC REGISTRADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"FB\", \"FAC REGISTRADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"FC\", \"FAC REGISTRADO (5 DIAS)\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"FD\", \"FAC REGISTRADO (10 DIAS)\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"FE\", \"ENCOMENDA FNDE\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"FF\", \"ETIQUETA LOG FAC REGISTRADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"FH\", \"FAC REGISTRADO C/ AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"FJ\", \"REMESSA ECONÔMICA C/ AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"FM\", \"FAC REGISTRADO (MONITORADO)\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"FR\", \"FAC REGISTRADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"IA\", \"INTEGRADA AVULSA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"IC\", \"INTEGRADA A COBRAR\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"ID\", \"INTEGRADA DEVOLUCAO DE DOCUMENTO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"IE\", \"INTEGRADA ESPECIAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"IF\", \"CPF\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"II\", \"INTEGRADA INTERNO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"IK\", \"INTEGRADA COM COLETA SIMULTANEA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"IM\", \"INTEGRADA MEDICAMENTOS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"IN\", \"OBJ DE CORRESP E EMS REC EXTERIOR\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"IP\", \"INTEGRADA PROGRAMADA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"IR\", \"INTEGRADA RECEBEDOR\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"IS\", \"INTEGRADA STANDARD MEDICAMENTO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"IT\", \"INTEGRADA TERMOLÁBIL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"IU\", \"INTEGRADA URGENTE\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"IX\", \"EDEI ENCOMENDA EXPRESSA\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"JA\", \"REMESSA ECONOMICA C/AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"JB\", \"REMESSA ECONOMICA C/AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JC\", \"REGISTRADO COM AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"JD\", \"REMESSA ECONOMICA S/AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"JE\", \"REMESSA ECONOMICA C/AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"JF\", \"REMESSA ECONOMICA C/AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JG\", \"REGISTRADO PRIORITÁRIO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JH\", \"REGISTRADO PRIORITÁRIO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"JI\", \"REMESSA ECONOMICA S/AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JJ\", \"REGISTRADO JUSTIÇA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"JK\", \"ETQ REM ECON TALAO/CARTAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JL\", \"REGISTRADO LÓGICO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JM\", \"ETIQUETA MD POSTAL ESPECIAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"JN\", \"MALA DIRETA BASICA/IMPRESSO NAO URGENTE\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JO\", \"REGISTRADO PRIORITÁRIO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JP\", \"OBJETO RECEITA FEDERAL (EXCLUSIVO)\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"JQ\", \"REMESSA ECONOMICA C/AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JR\", \"REGISTRADO PRIORITÁRIO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JS\", \"REGISTRADO LÓGICO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JT\", \"REGISTRADO URGENTE\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JU\", \"ETIQUETA FIS REGIST URG\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"JV\", \"REMESSA ECONÔMICA C/AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JW\", \"CARTA COMERCIAL A FATURAR (5 DIAS)\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"JX\", \"CARTA CML A FAT 10 DIAS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"JY\", \"REMESSA ECONOMICA (5 DIAS)\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"JZ\", \"REMESSA ECONOMICA 10 DIAS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LA\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LB\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LC\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LD\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LE\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LF\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LG\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LH\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LI\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LJ\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LK\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LL\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LM\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LN\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LO\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LP\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LQ\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LR\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LS\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LT\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LU\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LV\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LW\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LX\", \"OBJETO INTERNACIONAL PACKET ECONOMIC\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LY\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"LZ\", \"OBJETO INTERNACIONAL PRIME\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MA\", \"TELEGRAMA - SERVICOS ADICIONAIS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MB\", \"TELEGRAMA DE BALCAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MC\", \"TELEGRAMA FONADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"MD\", \"MAQUINA DE FRANQUEAR (LOGICA)\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"ME\", \"TELEGRAMA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MF\", \"TELEGRAMA FONADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MH\", \"CARTA VIA INTERNET\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MI\", \"CARTA VIA INTERNET (SMT)\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MK\", \"TELEGRAMA CORPORATIVO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MM\", \"TELEGRAMA GRANDES CLIENTES\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MN\", \"ETIQ LÓG TELEGRAMA CORPORATIVO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MP\", \"TELEGRAMA PRÉ-PAGO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MS\", \"ENCOMENDA SAUDE\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MT\", \"TELEGRAMA VIA TELEMAIL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MY\", \"TELEGRAMA INTERNACIONAL ENTRANTE\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"MZ\", \"TELEGRAMA VIA CORREIOS ON LINE\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"NE\", \"TELE SENA RESGATADA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"NX\", \"EDEI ENCOMENDA NAO URGENTE\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"OA\", \"ENCOMENDA SEDEX (ETIQ LOGICA)\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"OB\", \"ENCOMENDA SEDEX (ETIQ LOGICA)\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"OC\", \"ENCOMENDA SEDEX (ETIQ LOGICA)\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"OD\", \"ETIQUETA SEDEX (FISICA)\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"OE\", \"ETIQUETA LÓGICA SEDEX\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"OF\", \"ETIQUETA LÓGICA SEDEX\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"OG\", \"ETIQUETA LÓGICA SEDEX\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"OH\", \"ETIQUETA LOGICA SEDEX\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"OI\", \"ETIQUETA LOGICA SEDEX\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"OJ\", \"ETIQUETA LOGICA SEDEX OJ\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"OK\", \"ETIQUETA LOGICA SEDEX OK\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"OM\", \"ETIQUETA LOGICA SEDEX OM\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"PA\", \"PASSAPORTE\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PB\", \"ENCOMENDA PAC - NÃO URGENTE\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PC\", \"ENCOMENDA PAC A COBRAR\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PD\", \"ENCOMENDA PAC\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PE\", \"ENCOMENDA PAC (ETIQUETA FISICA)\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"PF\", \"PASSAPORTE\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PG\", \"ENCOMENDA PAC (ETIQUETA FISICA)\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PH\", \"ENCOMENDA PAC (ETIQUETA LOGICA)\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PI\", \"ENCOMENDA PAC\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PJ\", \"ENCOMENDA PAC\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PK\", \"ETIQUETA PAC EXTRA GRANDE\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PL\", \"ENCOMENDA PAC\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PM\", \"ETIQUETA PAC (FÍSICA)\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PN\", \"ENCOMENDA PAC (ETIQ LOGICA)\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PO\", \"ENCOMENDA PAC (ETIQ LOGICA)\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PP\", \"ETIQUETA LÓGICA PAC PP\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PQ\", \"ETIQUETA LOGICA CORREIOS MINI ENVIOS\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PR\", \"REEMBOLSO POSTAL - CLIENTE AVULSO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PS\", \"ETIQUETA LÓGICA PAC\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PT\", \"ENCOMENDA PAC\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PU\", \"ETIQUETA LOGICA PAC\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PV\", \"ETIQUETA LOG PAC ADMINIST\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PW\", \"ETIQUETA LOGICA PAC - PW\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PX\", \"ENCOMENDA PAC (ETIQUETA LOGICA)\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PY\", \"ETIQUETA LOGICA PAC - PY\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"PZ\", \"ETIQUETA LOGICA PAC\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"QC\", \"ETIQUETA PAC (FÍSICA)\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RA\", \"REGISTRADO PRIORITÁRIO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RB\", \"CARTA REGISTRADA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RC\", \"CARTA REGISTRADA COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"RD\", \"REMESSA ECONOMICA DETRAN\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RE\", \"MALA DIRETA POSTAL ESP/NORM/IMPR URGENTE\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RF\", \"OBJETO DA RECEITA FEDERAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RG\", \"REGISTRADO DO SISTEMA SARA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RH\", \"REGISTRADO COM AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RI\", \"OBJETO INTERNACIONAL REGISTRADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RJ\", \"CARTA REGISTRADA AGENCIA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RK\", \"CARTA REGISTRADA AGENCIA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RL\", \"REGISTRADO LÓGICO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RM\", \"REGISTRADO AGÊNCIA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RN\", \"CARTA REGISTRADA AGENCIA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RO\", \"CARTA REGISTRADA AGENCIA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"RP\", \"REEMBOLSO POSTAL - CLIENTE INSCRITO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RQ\", \"CARTA REGISTRADA AGENCIA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RR\", \"OBJETO INTERNACIONAL REGISTRADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"RS\", \"ETIQUETA REGISTRADO DETRAN\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"RT\", \"REM ECON TALAO/CARTAO SEM AR DIGITA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RU\", \"OBJETO INTERNACIONAL REGISTRADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"RV\", \"REM ECON CRLV/CRV/CNH COM AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RW\", \"OBJETO INTERNACIONAL REGISTRADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RX\", \"OBJETO INTERNACIONAL REGISTRADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"RY\", \"REM ECON TALAO/CARTAO COM AR DIGITAL\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"RZ\", \"REGISTRADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SA\", \"ETIQUETA SEDEX AGÊNCIA\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SB\", \"ETIQ SEDEX 10 FÍSICA\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SC\", \"SEDEX A COBRAR\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SD\", \"REMESSA EXPRESSA DETRAN\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SE\", \"ENCOMENDA SEDEX\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SF\", \"SEDEX AGENCIA\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SG\", \"SEDEX DO SISTEMA SARA\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SH\", \"SEDEX COM AR DIGITAL\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SI\", \"ENCOMENDA SEDEX (ETIQ LOGICA)\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SJ\", \"SEDEX HOJE\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SK\", \"SEDEX AGÊNCIA\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SL\", \"SEDEX LÓGICO\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SM\", \"SEDEX 12\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SN\", \"SEDEX AGÊNCIA\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SO\", \"SEDEX AGÊNCIA\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SP\", \"SEDEX PRÉ-FRANQUEADO\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SQ\", \"SEDEX\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SR\", \"SEDEX\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SS\", \"SEDEX FÍSICO\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"ST\", \"REM EXPRES TALAO/CARTAO SEM AR DIGITAL\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SU\", \"ENCOMENDA SERVIÇO EXPRESSA ECT\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SV\", \"REM EXPRES CRLV/CRV/CNH COM AR DIGITAL\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SW\", \"ENCOMENDA SEDEX\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SX\", \"ETIQUETA LOGICA SEDEX 10\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SY\", \"REM EXPRES TALAO/CARTAO COM AR DIGITAL\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"SZ\", \"SEDEX AGÊNCIA\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"TC\", \"TESTE (OBJETO PARA TREINAMENTO)\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"TE\", \"TESTE (OBJETO PARA TREINAMENTO)\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"TR\", \"OBJETO TREINAMENTO - NÃO GERA PRÉ-ALERTA\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"TS\", \"TESTE (OBJETO PARA TREINAMENTO)\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UA\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UB\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UC\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UD\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UE\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UF\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UG\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UH\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UI\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UJ\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UK\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UL\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UM\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UN\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UO\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UP\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UQ\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UR\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"US\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UT\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UU\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UV\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UW\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UX\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UY\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"UZ\", \"PPS IMPORTACAO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VA\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VB\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VC\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VD\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VE\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VF\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VG\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VH\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VI\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VJ\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VK\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VL\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VM\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VN\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VO\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VP\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VQ\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VR\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VS\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VT\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VU\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VV\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VW\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VX\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VY\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"VZ\", \"OBJETO INTERNACIONAL COM VALOR DECLARADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"XA\", \"AVISO CHEGADA OBJETO INT TRIBUTADO\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"XM\", \"SEDEX MUNDI\", \"7\", \"30\", \"7\", \"20\", \"SEDEX\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"XP\", \"CORREIOS PACKET MINI\", \"7\", \"30\", \"7\", \"20\", \"PAC\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"NAO URGENTE\", \"XR\", \"OBJETO INTERNACIONAL (PPS TRIBUTADO)\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
            CriaTipoPostalInicial("INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) VALUES(\"URGENTE\", \"XX\", \"OBJETO INTERNACIONAL (PPS TRIBUTADO)\", \"20\", \"30\", \"20\", \"20\", \"DIVERSOS\", \"02/05/2021 22:41:51\")");
        }

        public static bool RetornaSeECaixaPostal(string texto)
        {
            string textoFormatado = string.Empty;
            textoFormatado = texto.RemoveAcentos();
            textoFormatado = textoFormatado.ToUpper();

            if (textoFormatado.Contains(" - CAIXA POSTAL")) return true;
            if (textoFormatado.Contains(" - CAIXA POSTA")) return true;
            if (textoFormatado.Contains(" - CAIXA POST")) return true;
            if (textoFormatado.Contains(" - CAIXA POS")) return true;
            if (textoFormatado.Contains(" - CAIXA PO")) return true;
            if (textoFormatado.Contains(" - CAIXA P")) return true;
            if (textoFormatado.Contains(" - CX POSTAL")) return true;
            if (textoFormatado.Contains(" - CX POSTA")) return true;
            if (textoFormatado.Contains(" - CX POST")) return true;
            if (textoFormatado.Contains(" - CX POS")) return true;
            if (textoFormatado.Contains(" - CX PO")) return true;
            if (textoFormatado.Contains(" - CX P")) return true;
            if (textoFormatado.Contains(" - CP")) return true;
            return false;
        }

        public static string RetornaCaixaPostalCorrigidaDefeitoString(string texto)
        {
            #region Possíveis Palavras Erradas
            string[] Cadeia = new string[]
                {
                    "CAIXA PSTAL",
                    "CAIXA POSTA",
                    "CAIXA POST",
                    "CAIXA POS",
                    "CAIXA PO",
                    "CAIXA P",
                    "CAIXA PSTA",
                    "CAIXA PSTAL",
                    "CX POSTAL",
                    "CX POSTA",
                    "CX POST",
                    "CX POS",
                    "CX PO",
                    "CX P",
                    "CX PSTA",
                    "CX PSTAL"
                };
            #endregion

            string TextoDesejado = "CAIXA POSTAL";

            foreach (string item in Cadeia)
            {
                string itemAtual = item.RemoveAcentos().ToUpper();
                texto = texto.RemoveAcentos().ToUpper();

                if (texto.Contains(itemAtual))
                {
                    texto = texto.Replace(itemAtual, TextoDesejado);
                    break;
                }
            }
            return texto;
        }

        public static string RetornaAoRemetenteCorrigidaDefeitoString(string texto)
        {
            #region Possíveis Palavras Erradas
            string[] Cadeia = new string[]
                {
                    "ORIGEM",
                    "DEVOLUCAO",
                    "DEVOLUCA",
                    "DEVOLUC",
                    "DEVOLU",
                    "DEVOL",
                    "REMETENTE",
                    "REMETENT",
                    "REMETEN",
                    "REMETE",
                    "REMET",
                    "REME",
                    "REM"
                };
            #endregion

            string TextoDesejado = "REMETENTE";

            foreach (string item in Cadeia)
            {
                string itemAtual = item.RemoveAcentos().ToUpper();
                texto = texto.RemoveAcentos().ToUpper();

                if (texto.Contains(itemAtual))
                {
                    texto = texto.Replace(itemAtual, TextoDesejado).Trim();
                    texto = texto.Replace("AO", "").Trim();
                    texto = texto.Replace(TextoDesejado, "AO REMETENTE");
                    break;
                }
            }
            return texto;
        }

        public static bool RetornaSeEAoRemetente(string texto)
        {
            string textoFormatado = string.Empty;
            textoFormatado = texto.RemoveAcentos();
            textoFormatado = textoFormatado.ToUpper();

            if (textoFormatado.Replace("  ", " ").Contains(" - ORIGEM")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - DEVOLUCAO")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - DEVOLUCA")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - DEVOLUC")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - DEVOLU")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - DEVOL")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - REMETENTE")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - REMETENT")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - REMETEN")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - REMETE")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - REMET")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - REME")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - REM")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - AO REMETENTE")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - AO REMETENT")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - AO REMETEN")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - AO REMETE")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - AO REMET")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - AO REME")) return true;
            if (textoFormatado.Replace("  ", " ").Contains(" - AO REM")) return true;
            return false;
        }

        private static void CriaTipoPostalInicial(string SQL)
        {
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ClassesDiversas.Configuracoes.strConexao);
            try
            {
                string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
                if (!Arquivos.Existe(string.Format("{0}\\cadastro.mdb", curDir), false))
                {
                    throw new Exception("Arquivo 'cadastro.mdb' não encontrado na raiz do sistema.!");
                }
                using (conn)
                {
                    conn.Open();

                    System.Data.OleDb.OleDbCommand criaCampo = new System.Data.OleDb.OleDbCommand();
                    criaCampo.Connection = conn;
                    criaCampo.CommandText = SQL;
                    criaCampo.ExecuteNonQuery();
                    criaCampo.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(@"Houve uma falha ao executar a ação: " + ex.Message);
                Mensagens.Erro(@"Houve uma falha ao executar a ação: " + ex.Message);
                var processo = System.Diagnostics.Process.GetCurrentProcess();
                foreach (System.Diagnostics.Process itens in System.Diagnostics.Process.GetProcessesByName(processo.ProcessName).Where(p => p.Id != processo.Id))
                {
                    itens.Kill();
                }
                foreach (System.Diagnostics.Process itens in System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName))
                {
                    itens.Kill();
                }
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        private static void CriaTabela(string NomeTabela)
        {
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ClassesDiversas.Configuracoes.strConexao);
            try
            {
                string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
                if (!Arquivos.Existe(string.Format("{0}\\cadastro.mdb", curDir), false))
                {
                    throw new Exception("Arquivo 'cadastro.mdb' não encontrado na raiz do sistema.!");
                }
                using (conn)
                {
                    conn.Open();

                    System.Data.OleDb.OleDbCommand criaCampo = new System.Data.OleDb.OleDbCommand();
                    criaCampo.Connection = conn;
                    criaCampo.CommandText = "CREATE TABLE [" + NomeTabela + "]";
                    //criaCampo.CommandText = "CREATE TABLE [" + NomeTabela + "]( [Codigo] AUTOINCREMENT PRIMARY KEY NOT NULL)";
                    criaCampo.ExecuteNonQuery();
                    criaCampo.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(@"Houve uma falha ao executar a ação: " + ex.Message);
                Mensagens.Erro(@"Houve uma falha ao executar a ação: " + ex.Message);
                var processo = System.Diagnostics.Process.GetCurrentProcess();
                foreach (System.Diagnostics.Process itens in System.Diagnostics.Process.GetProcessesByName(processo.ProcessName).Where(p => p.Id != processo.Id))
                {
                    itens.Kill();
                }
                foreach (System.Diagnostics.Process itens in System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName))
                {
                    itens.Kill();
                }
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        private static void CriaColuna(string tabela, string coluna, string tipo)
        {
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ClassesDiversas.Configuracoes.strConexao);
            try
            {
                string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
                if (!Arquivos.Existe(string.Format("{0}\\cadastro.mdb", curDir), false))
                {
                    throw new Exception("Arquivo 'cadastro.mdb' não encontrado na raiz do sistema.!");
                }
                using (conn)
                {
                    conn.Open();

                    var schema = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Columns,
                        new object[] { null, null, tabela, null });

                    if (schema != null && schema.Rows
                        .OfType<DataRow>()
                        .Any(r => r.ItemArray[3].ToString().ToLower().Equals(coluna.ToLower())))
                    {
                        //MessageBox.Show(@"Existe");
                    }
                    else
                    {
                        System.Data.OleDb.OleDbCommand criaCampo = new System.Data.OleDb.OleDbCommand();
                        criaCampo.Connection = conn;
                        criaCampo.CommandText = string.Concat("ALTER TABLE [", tabela, "] ADD COLUMN ", coluna, " ", tipo);
                        criaCampo.ExecuteNonQuery();
                        criaCampo.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(@"Houve uma falha ao executar a ação: " + ex.Message);
                Mensagens.Erro(@"Houve uma falha ao executar a ação: " + ex.Message);
                var processo = System.Diagnostics.Process.GetCurrentProcess();
                foreach (System.Diagnostics.Process itens in System.Diagnostics.Process.GetProcessesByName(processo.ProcessName).Where(p => p.Id != processo.Id))
                {
                    itens.Kill();
                }
                foreach (System.Diagnostics.Process itens in System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName))
                {
                    itens.Kill();
                }
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        private static void CriaLinhaTiposPostais(string Servico, string Sigla, string Descricao, string PrazoDestinoCaidaPedida, string PrazoDestinoCaixaPostal, string PrazoRemetenteCaidaPedida, string PrazoRemetenteCaixaPostal, string TipoClassificacao, string DataAlteracao)
        {
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ClassesDiversas.Configuracoes.strConexao);
            try
            {
                string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
                if (!Arquivos.Existe(string.Format("{0}\\cadastro.mdb", curDir), false))
                {
                    throw new Exception("Arquivo 'cadastro.mdb' não encontrado na raiz do sistema.!");
                }
                using (conn)
                {
                    conn.Open();

                    System.Data.OleDb.OleDbCommand criaCampo = new System.Data.OleDb.OleDbCommand();
                    criaCampo.Connection = conn;
                    criaCampo.CommandText = "INSERT INTO TiposPostais(Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao) " +
                        "VALUES (" + string.Format("\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\")", Servico, Sigla, Descricao, PrazoDestinoCaidaPedida, PrazoDestinoCaixaPostal, PrazoRemetenteCaidaPedida, PrazoRemetenteCaixaPostal, TipoClassificacao, DataAlteracao);
                    criaCampo.ExecuteNonQuery();
                    criaCampo.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(@"Houve uma falha ao executar a ação: " + ex.Message);
                Mensagens.Erro(@"Houve uma falha ao executar a ação: " + ex.Message);
                var processo = System.Diagnostics.Process.GetCurrentProcess();
                foreach (System.Diagnostics.Process itens in System.Diagnostics.Process.GetProcessesByName(processo.ProcessName).Where(p => p.Id != processo.Id))
                {
                    itens.Kill();
                }
                foreach (System.Diagnostics.Process itens in System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName))
                {
                    itens.Kill();
                }
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        private static bool VerificaSeTabelaExiste(string NomeTabela)
        {
            // Variable to return that defines if the table exists or not.
            bool TableExists = false;
            System.Data.OleDb.OleDbConnection conn;

            // Try the database logic
            try
            {
                // Make the Database Connection
                conn = new System.Data.OleDb.OleDbConnection(ClassesDiversas.Configuracoes.strConexao);
                conn.Open();
                // Get the datatable information
                DataTable dt = conn.GetSchema("Tables");

                // Loop throw the rows in the datatable
                foreach (DataRow row in dt.Rows)
                {
                    // If we have a table name match, make our return true
                    // and break the looop
                    if (row.ItemArray[2].ToString() == NomeTabela)
                    {
                        TableExists = true;
                        conn.Close();
                        break;
                    }
                }

                //close database connections!
                conn.Close();
                return TableExists;
            }
            catch (Exception)
            {
                // Handle your ERRORS!
                return false;
            }
        }

        private static void SetaCamposEnderecoSRO()
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                DataRow RetornoEnderecosSRO = dao.RetornaDataRow("SELECT EnderecoSRO, EnderecoSROPorObjeto FROM TabelaConfiguracoesSistema");
                string EnderecoSRO = RetornoEnderecosSRO["EnderecoSRO"].ToString();
                string EnderecoSROPorObjeto = RetornoEnderecosSRO["EnderecoSROPorObjeto"].ToString();

                if (string.IsNullOrEmpty(EnderecoSRO))
                {
                    EnderecoSRO = "http://websro2/rastreamento/sro";
                    List<Parametros> pr = new List<Parametros>() {
                            new Parametros() { Nome = "@EnderecoSRO", Tipo = TipoCampo.Text, Valor = EnderecoSRO }
                        };
                    dao.ExecutaSQL("UPDATE TabelaConfiguracoesSistema SET EnderecoSRO = @EnderecoSRO", pr);
                }
                if (string.IsNullOrEmpty(EnderecoSROPorObjeto))
                {
                    EnderecoSROPorObjeto = "http://websro2.correiosnet.int/rastreamento/sro?opcao=PESQUISA&objetos=";
                    List<Parametros> pr = new List<Parametros>() {
                            new Parametros() { Nome = "@EnderecoSROPorObjeto", Tipo = TipoCampo.Text, Valor = EnderecoSROPorObjeto }
                        };
                    dao.ExecutaSQL("UPDATE TabelaConfiguracoesSistema SET EnderecoSROPorObjeto = @EnderecoSROPorObjeto", pr);
                }
            }
        }

        /// <summary>
        /// Codigo da versão do leiaute da EFD
        /// </summary>
        public static string COD_VER = string.Empty;

        /// <summary>
        /// Ano da geração do arquivo EFD
        /// </summary>
        public static int AnoGeracaoArquivo = 0;

        //Local do arquivo de conexão
        private static string lLocalArquivoConexao = String.Format("{0}\\Rensoftware\\SEDsce\\SEDFiscal", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
        public static string LocalArquivoConexao
        {
            get { return lLocalArquivoConexao; }
        }

        //Local com o nome do arquivo de conexão
        private static string LNomeELocalArquivoConexao = String.Format("{0}\\Rensoftware\\SendEMail Rensoftware 2.0\\Conexao.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
        public static string NomeELocalArquivoConexao
        {
            get { return LNomeELocalArquivoConexao; }
            set { LNomeELocalArquivoConexao = value; }
        }

        public static string LocalArquivoAtualizacao { get; set; }

        //Indica o nome do usuario do  servidor
        private static string LNomeUsuarioServido = "SA";//"SUPERSCERG";
        public static string NomeUsuarioServido
        {
            get { return LNomeUsuarioServido; }
        }

        //Indica a senha do usuario do servidor
        private static string LSenhaUsuarioServidor = "S35SUP5RSRG";
        public static string SenhaUsuarioServidor
        {
            get { return LSenhaUsuarioServidor; }
        }

        //Indica o servidor que o sistema esta conectado 
        private static string LServidorLogado = string.Empty;
        public static string ServidorLogado
        {
            get { return LServidorLogado; }
            set { LServidorLogado = value; }
        }

        //Indica o banco que o sistema esta conectado 
        private static string LBancoLogado = string.Empty;
        public static string BancoLogado
        {
            get { return LBancoLogado; }
            set { LBancoLogado = value; }
        }

        //private static string LStringConexao = String.Format(@"Data Source=222.222.3.2;Initial Catalog=EnvioEmail;Persist Security Info=True;User ID={0};Password={1}", LNomeUsuarioServido, LSenhaUsuarioServidor);
        //public static string StringConexao
        //{
        //    get { return LStringConexao; }
        //    set
        //    {
        //        LStringConexao = value;
        //    }
        //}

        public static int CodigoLoja { get; set; }

        public static string NomeEmpresa { get; set; }

        public static string UFEmpresa { get; set; }

        //public static TipoPerfilEFD PerfilEFD { get; set; }

        //Indica o nome do usuarioLogado
        private static string LUsuarioLogado = string.Empty;
        public static string UsuarioLogado
        {
            get { return LUsuarioLogado; }
            set { LUsuarioLogado = value; }
        }

        public static string CodigoUsuarioLogado { get; set; }

        public static bool FecharApplication { get; set; }

        /// <summary>
        /// Seta OU Retorna o Valor AoRemetente exclusivamente na tela FormularioAtualizacaoObjetosSaiuParaEntrega
        /// String: Código do Objeto Atual;
        /// Bool: Se é Ao Remetente ou não.
        /// Obs. Só será Ao Remetente se existir o item "Saiu para entrega ao remetente".
        public static Dictionary<string, bool> SeEAoRemetenteAtualizacaoObjetosSaiuParaEntrega { get; internal set; }
        //public static bool SeEAoDestinatario { get; internal set; }

        static Configuracoes()
        {

            //TODO: CASO O NOME DA EMPRESA NÃO ESTEJA SENDO POPULADO AO EXECUTAR A FUNÇÃO DE LOGAR, DÊ UM JEITO DE FAZER ISSO AQUI...
            //Configuracoes.NomeEmpresa = "Buscar o nome da empresa";

            //LocalArquivoAtualizacao = String.Format("{0}\\Rensoftware\\SEDsce\\SEDFiscal\\AtualizaBanco\\AtualizaBancoEFD.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            Configuracoes.Conf = ConfigurationManager.OpenMachineConfiguration();
        }

        /// <summary>
        /// Inicia configuração da string de conexão.
        /// </summary>
        /// <param name="PassouUmaVez">
        /// Como o método é recursivo, há necessidade de condições de parada
        /// caso ainda não tenha passado nele ou já passou e não configurou uma
        /// conexão e optou por configurar, o método torna a chamar a si mesmo
        /// caso contrario, ele sai da cadeia recursiva
        /// </param>
        /// <Modificado por>Thiago Nery Macêdo</Modificado>
        //public static void ConfiguraConexao(bool PassouUmaVez)
        //{
        //    FecharApplication = false;
        //    if (!Directory.Exists(LocalArquivoConexao))
        //        Directory.CreateDirectory(LocalArquivoConexao);

        //    //Verifica a existencia do arquivo de conexão. caso o arquivo exista prepara a conexão.
        //    //Se o arquivo não existe é chamando o formulario de configuração de conexão.
        //    if (File.Exists(NomeELocalArquivoConexao))
        //    {
        //        BancoLogado = string.Empty;
        //        ServidorLogado = string.Empty;

        //        DataSet DsStringsConexao = new DataSet();
        //        //ConfiguraConexao ConfConexao = new ConfiguraConexao();

        //        try
        //        {
        //            DsStringsConexao.ReadXml(NomeELocalArquivoConexao);

        //                //Opção quando tem apenas uma conexão feita
        //                ServidorLogado = DsStringsConexao.Tables["Estrutura"].Rows[0]["SERVIDOR"].ToString();
        //                BancoLogado = DsStringsConexao.Tables["Estrutura"].Rows[0]["BANCO"].ToString();

        //            //    LStringConexao = ConfConexao.MontaEstrigConexao(ServidorLogado, BancoLogado, LNomeUsuarioServido, LSenhaUsuarioServidor);
        //            LStringConexao = DAO.MontaStringConexao(ServidorLogado,LNomeUsuarioServido,LSenhaUsuarioServidor,BancoLogado,TipoBanco.SqlServer);

        //            if (!DAO.TestaConexao(LStringConexao))
        //            {

        //            }
        //                //if (!ConfConexao.TestaConexao(LStringConexao))
        //                //{
        //                //    //Deu erro na conexão.
        //                //    ConfConexao.Dispose();
        //                //    PFormManutencaoConexao = new FormManutencaoConexao();
        //                //    PFormManutencaoConexao.ShowDialog();
        //                //    ConfiguraConexao(PassouUmaVez);
        //                //    return;
        //                //}
        //                //else
        //                //{
        //                //    ConfConexao.Dispose();
        //                //}

        //        }
        //        catch (Exception Ex)
        //        {
        //            //Mensagens.MsgErroGenerico(Ex.Message);
        //            //PFormManutencaoConexao = new FormManutencaoConexao();
        //            //PFormManutencaoConexao.ShowDialog();

        //            //if (PFormManutencaoConexao.BSDados.Count > 0)
        //            //    return;
        //            //else if (Mensagens.Pergunta("A conexão não foi configurada corretamente, deseja continuar tentando?") == DialogResult.Yes)
        //            //    ConfiguraConexao(PassouUmaVez);
        //            //else
        //            //{
        //            //    FecharApplication = true;
        //            //    Application.Exit();
        //            //    return;
        //            //}
        //        }
        //    }
        //    else
        //    {
        //        //if (!PassouUmaVez || (PassouUmaVez && Mensagens.Pergunta("A conexão não foi configurada corretamente, deseja continuar tentando?") == DialogResult.Yes))
        //        //{
        //        //    PFormManutencaoConexao = new FormManutencaoConexao();
        //        //    PFormManutencaoConexao.ShowDialog();
        //        //    PassouUmaVez = true;
        //        //    ConfiguraConexao(PassouUmaVez);
        //        //}
        //        //else
        //        //{
        //        //    FecharApplication = true;
        //        //    Application.Exit();
        //        //}

        //        return;
        //    }
        //}

        #region Configuracoes no app.config

        /// <summary>
        /// Carrega o arquivo de configurações (app.config)
        /// </summary>
        public static Configuration Conf { get; set; }

        #region Metodos de gravação e recuperação de configurações no app.config
        /// <summary>
        /// Grava o valor de uma configuração no arquivo app.config
        /// </summary>
        /// <param name="Chave">Chave do registro</param>
        /// <param name="Valor">Valor do registro</param>
        public static void GravaConfig(string Chave, string Valor)
        {
            try
            {
                Chave = Chave.RemoveTodoEspaco().RemoveAcento_DICIONARIO().ToUpper();
                //Se não existir a chave, cria uma nova e atribui o valor
                Configuracoes.Conf.AppSettings.Settings[Chave].Value = Valor;
                Configuracoes.Conf.Save();
            }
            catch
            {
                //Se ja existir, atualiza o valor
                Configuracoes.Conf.AppSettings.Settings.Add(Chave, Valor);
                Configuracoes.Conf.Save();
            }
        }

        /// <summary>
        /// Retorna o valor de uma configuração
        /// </summary>
        /// <param name="Chave">Chave que contém o valor</param>
        /// <returns>Valor gravado no arquivo app.config</returns>
        public static string RetornaConfig(string Chave, string DefaultValue)
        {
            try
            {
                Chave = Chave.RemoveTodoEspaco().RemoveAcento_DICIONARIO().ToUpper();
                //Caso a chave exista, retorna o valor dela
                return Configuracoes.Conf.AppSettings.Settings[Chave].Value.ToString();
            }
            catch
            {
                //Caso não exista, cria uma chave e retorna valor uma string vazia
                Configuracoes.Conf.AppSettings.Settings.Add(Chave, DefaultValue);
                Configuracoes.Conf.Save();
                return DefaultValue;
            }
        }
        #endregion

        #region Metodos de apoio
        /// <summary>
        /// Retorna um valor boleano para a string passada como parametro,
        /// Caso a string seja igual a 1 ou 'S' ou 's' retorno = True
        /// , senão retorno = False;
        /// </summary>
        public static bool GetBoolByName(string SimNao)
        {
            if (SimNao.ToUpper().Contains("S"))
                return true;
            else
                return false;
        }

        public static FormWindowState GetWindowStateByName(string formWindowState)
        {
            if (formWindowState.ToUpper() == FormWindowState.Maximized.ToString().ToUpper())
                return FormWindowState.Maximized;
            else if (formWindowState.ToUpper() == FormWindowState.Minimized.ToString().ToUpper())
                return FormWindowState.Minimized;
            else if (formWindowState.ToUpper() == FormWindowState.Normal.ToString().ToUpper())
                return FormWindowState.Normal;
            else
                return FormWindowState.Maximized;
        }

        public static System.Drawing.Size StringToSize(string _Size)
        {
            string[] Tamanhos = _Size.Replace("{", string.Empty).Replace("}", string.Empty).ToUpper().Trim().Split(',');
            int Width = 0, Height = 0;
            string Val = string.Empty;

            try
            {
                Val = Tamanhos.ToList().Where(T => T.ToUpper().Contains("WIDTH")).First();
                Width = Convert.ToInt32(Val.Substring(Val.IndexOf("=") + 1));

                Val = Tamanhos.ToList().Where(T => T.ToUpper().Contains("HEIGHT")).First();
                Height = Convert.ToInt32(Val.Substring(Val.IndexOf("=") + 1));
            }
            catch { }

            return new System.Drawing.Size(Width, Height);
        }

        public static System.Drawing.Point StringToPoint(string _Size)
        {
            string[] Tamanhos = _Size.Replace("{", string.Empty).Replace("}", string.Empty).ToUpper().Trim().Split(',');
            int X = 0, Y = 0;
            string Z = string.Empty;

            try
            {
                Z = Tamanhos.ToList().Where(T => T.ToUpper().Contains("X")).First();
                X = Convert.ToInt32(Z.Substring(Z.IndexOf("=") + 1));

                Z = Tamanhos.ToList().Where(T => T.ToUpper().Contains("Y")).First();
                Y = Convert.ToInt32(Z.Substring(Z.IndexOf("=") + 1));
            }
            catch { }

            return new System.Drawing.Point(X, Y);
        }
        #endregion

        #region Propriedades de configuração
        public static FormWindowState EstadoFormPrincipal
        {
            get
            {
                return GetWindowStateByName(RetornaConfig("EstadoFormPrincipal", FormWindowState.Maximized.ToString()));
            }
            set
            {
                GravaConfig("EstadoFormPrincipal", value.ToString());
            }
        }

        public static System.Drawing.Size TamanhoFormPrincipal
        {
            get
            {
                return StringToSize(RetornaConfig("TamanhoFormPrincipal", Screen.PrimaryScreen.WorkingArea.Size.ToString()));
            }
            set
            {
                GravaConfig("TamanhoFormPrincipal", value.ToString());
            }
        }

        public static System.Drawing.Point PosicaoFormPrincipal
        {
            get
            {
                return StringToPoint(RetornaConfig("PosicaoFormPrincipal", new System.Drawing.Point(0, 0).ToString()));
            }
            set
            {
                GravaConfig("PosicaoFormPrincipal", value.ToString());
            }
        }

        public static string NomeMenuSelecionadoFormPrincipal
        {
            get
            {
                return RetornaConfig("MenuSelecionadoFormPrincipal", string.Empty);
            }
            set
            {
                GravaConfig("MenuSelecionadoFormPrincipal", value);
            }
        }

        /// <summary>
        /// Estilo, Skim ou esquema de cores da aplicação
        /// </summary>
        public static string EstiloAplicacao
        {
            get
            {
                return RetornaConfig("EstiloAplicacao", "Black");
            }
            set
            {
                GravaConfig("EstiloAplicacao", value);
            }
        }


        #endregion

        #endregion
    }
}
