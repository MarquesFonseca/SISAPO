using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using SISAPO.ClassesDiversas;
using System.Data;

using System.Management;
using System.Runtime.InteropServices;

namespace SISAPO
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        static void Main(string[] args)
        {
            #region TipoAmbiente
#if DEBUG
            Configuracoes.TipoAmbiente = TipoAmbiente.Desenvolvimento;
#endif
#if !DEBUG
            Configuracoes.TipoAmbiente = TipoAmbiente.Producao;            
#endif
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");

            FormularioPrincipal formularioPrincipal = new FormularioPrincipal();

            var processo = System.Diagnostics.Process.GetCurrentProcess();
            var jaEstaRodando = System.Diagnostics.Process.GetProcessesByName(processo.ProcessName).Any(p => p.Id != processo.Id);

            if (jaEstaRodando)
            {
                //Mensagens.Informa(string.Format("Está rodando: {0}", jaEstaRodando));
                formularioPrincipal.Activate();
                return;
            }

            #region inicio teste retorno excel
            //string NomeEndereco = string.Format(@"{0}Tipos_Postais.xls", System.AppDomain.CurrentDomain.BaseDirectory);
            //string NomePlanilha = string.Format("{0}$", "Planilha1");
            //string ColunasBusca = string.Format("" 
            //    + "[Serviço],"
            //    + "[Código],"
            //    + "[Nome],"
            //    + "[Prazo dias corridos no destino (Caixa Postal)],"
            //    + "[Prazo dias corridos no destino (Caída/Pedida)],"
            //    + "[Prazo dias corridos na origem/devolução/remetente (Caixa Postal)],"
            //    + "[Prazo dias corridos na origem/devolução/remetente (Caída/Pedida)]"
            //    + "");
            //int QtdTopSelect = 0;
            //try
            //{
            //    using (DataTable dt = new ImportarArquivos().ImportarXLSXNovo(NomeEndereco, NomePlanilha, ColunasBusca, QtdTopSelect))
            //    {
            //        if (dt != null && dt.Rows.Count > 0)
            //        {
            //            DataRow retorno = null;                        
            //        }
            //        else
            //        {
            //            MessageBox.Show("Não foi possível carregar nenhum registro apartir do .xls informado. Por favor selecione outro arquivo.");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(string.Format("Não foi possível carregar o arquivo: {0}", ex.Message));
            //}
            #endregion fim teste retorno excel

            //Configuracoes.MetodoDeTesteQualquer();

            //Configuracoes.VerificaAplicacaoSeAbertaFechar();
            Configuracoes.GeraArquivoConfig();

            Configuracoes.VerificaSeFecharAplicacaoParaAtualizacao();

            if (!Configuracoes.VerificaChaveAcesso())
            {
                Mensagens.Alerta("Versão demonstração.\nFavor contactar o administrador do sistema. \nMarques Fonseca (63) 99208-2269");
                return;
            }

            using (FormWaiting frm = new FormWaiting(ProcessandoItensBancoBadosInicial))
            {
                frm.LbnMensagem.Text = "Iniciando. Aguarde...";
                frm.ShowDialog(new Form() { WindowState = FormWindowState.Maximized });
            }            

            Application.Run(formularioPrincipal);
        }

        private static void ProcessandoItensBancoBadosInicial()
        {
            //LIMPA TABELAS
            //Configuracoes.LimpaBancoTornaBancoVazio();
            Configuracoes.VerificaSquemaBancoDados();
            Configuracoes.DadosAgencia = Configuracoes.RetornaDadosAgencia();
            Configuracoes.EnderecosSRO = Configuracoes.RetornaEnderecosSRO();
        }



        //[DllImport("user32.dll")]
        //private static extern IntPtr GetForegroundWindow();
        //[DllImport("user32.dll")]
        //private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        //public static string GetProcessOwner(string processName)
        //{
        //    string query = "Select * from Win32_Process Where Name = \"" + processName + "\"";
        //    //query = "Select * from Win32_Process";
        //    ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
        //    ManagementObjectCollection processList = searcher.Get();

        //    foreach (ManagementObject obj in processList)
        //    {
        //        string[] argList = new string[] { string.Empty, string.Empty };
        //        int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
        //        if (returnVal == 0)
        //        {
        //            // return DOMAIN\user
        //            string owner = argList[1] + "\\" + argList[0];
        //            return owner;
        //        }
        //    }

        //    return "NO OWNER";
        //}

        //private static string GetProcessUser(Process process)
        //{
        //    IntPtr processHandle = IntPtr.Zero;
        //    try
        //    {
        //        OpenProcessToken(process.Handle, 8, out processHandle);
        //        System.Security.Principal.WindowsIdentity wi = new System.Security.Principal.WindowsIdentity(processHandle);
        //        string user = wi.Name;
        //        return user.Contains(@"\") ? user.Substring(user.IndexOf(@"\") + 1) : user;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        if (processHandle != IntPtr.Zero)
        //        {
        //            CloseHandle(processHandle);
        //        }
        //    }
        //}

        //[System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
        //private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);
        //[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        //[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        //private static extern bool CloseHandle(IntPtr hObject);

    }
}
