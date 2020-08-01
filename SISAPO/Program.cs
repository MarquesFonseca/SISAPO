using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using SISAPO.ClassesDiversas;
using System.Data;

namespace SISAPO
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var processo = System.Diagnostics.Process.GetCurrentProcess();
            var jaEstaRodando = System.Diagnostics.Process.GetProcessesByName(processo.ProcessName).Any(p => p.Id != processo.Id);
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
#if DEBUG
            Configuracoes.TipoAmbiente = TipoAmbiente.Desenvolvimento;
#endif
#if !DEBUG
            Configuracoes.TipoAmbiente = TipoAmbiente.Producao;
#endif


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");

            string data = ClassesDiversas.CriptografiaHelper.Criptografa("30-06-2020");

            Configuracoes.GeraArquivoConfig();
            Configuracoes.VerificaSeFecharAplicacaoParaAtualizacao();

            if (Configuracoes.VerificaChaveAcesso())
            {
                //LIMPA TABELAS 
                //using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                //{
                //    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                //    dao.ExecutaSQL("DELETE FROM TabelaHistoricoConsulta");
                //    dao.ExecutaSQL("ALTER TABLE TabelaHistoricoConsulta ALTER COLUMN Codigo COUNTER(1, 1)");

                //    dao.ExecutaSQL("DELETE FROM TabelaObjetosSROLocal");
                //    dao.ExecutaSQL("ALTER TABLE TabelaObjetosSROLocal ALTER COLUMN Codigo COUNTER(1, 1)");
                //}

                Configuracoes.VerificaSquemaBancoDados();

                //string data = ClassesDiversas.CriptografiaHelper.Criptografa("30-09-2019");
                Application.Run(new FormularioPrincipal());
            }
            else
            {
                Mensagens.Alerta("Versão demonstração.\nFavor contactar o administrador do sistema. \nMarques Fonseca (63) 99208-2269");
                return;
            }
        }

        
    }
}
