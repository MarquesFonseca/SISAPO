using Microsoft.Win32;
using System;

public static class Registro
{
    //public static void GravarRegistro(string Chave, object Valor)
    //{
    //    RegistryKey Registro = Registry.LocalMachine.OpenSubKey("Software", true);
    //    try
    //    {
    //        Registro = Registro.CreateSubKey("Rensoftware Exportador de Arquivos");
    //        Registro.SetValue(Chave, Valor.ToString());
    //    }
    //    catch
    //    {
    //    }
    //    finally
    //    {
    //        Registro.Close();
    //    }
    //}

    //public static object RecuperaValor(string Chave)
    //{
    //    object Retorno;
    //    RegistryKey Registro = Registry.LocalMachine.OpenSubKey("Software", true);
    //    try
    //    {
    //        Retorno = Registro.OpenSubKey("Rensoftware Exportador de Arquivos", true).GetValue(Chave).ToString();
    //    }
    //    catch
    //    {
    //        Retorno = string.Empty;
    //    }
    //    finally
    //    {
    //        Registro.Close();
    //    }
    //    return Retorno;
    //}

    /// <summary>
    /// Raizes do registro do windows
    /// </summary>
    public enum RaizRegistro
    {
        HKEY_CLASSES_ROOT,
        HKEY_CURRENT_USER,
        HKEY_LOCAL_MACHINE,
        HKEY_USERS,
        HKEY_CURRENT_CONFIG
    }
    
    /// <summary>
    /// Tras uma informação anteriormente gravada no registro do windows
    /// ATENÇÃO: Cuidado com o que faz no registro do windows,
    /// dependendo da operação execultada vc vai danificar o Sistema Operacional,
    /// trazendo vários transtornos, então muita atenção no que mandar execultar no registro.
    /// </summary>
    /// <param name="_RaizRegistro">Informe a raíz do registro.</param>
    /// <param name="Caminho">Informe o caminho que contem o valor desejado.</param>
    /// <param name="NomeChave">Informe o nome da chave onde existe o valor desejado.</param>
    public static string LerRegistro(RaizRegistro _RaizRegistro, string Caminho, string NomeChave)
    {
        try
        {
            string Retorno;
            // cria uma referêcnia para a chave de registro Software
            if (_RaizRegistro == RaizRegistro.HKEY_LOCAL_MACHINE)
            {
                RegistryKey Registro = Registry.LocalMachine.OpenSubKey(Caminho, true);
                Retorno = Convert.ToString(Registro.GetValue(NomeChave));
                Registro.Close();
            }
            else if (_RaizRegistro == RaizRegistro.HKEY_CLASSES_ROOT)
            {
                RegistryKey Registro = Registry.ClassesRoot.OpenSubKey(Caminho, true);
                Retorno = Convert.ToString(Registro.GetValue(NomeChave));
                Registro.Close();
            }
            else if (_RaizRegistro == RaizRegistro.HKEY_CURRENT_CONFIG)
            {
                RegistryKey Registro = Registry.CurrentConfig.OpenSubKey(Caminho, true);
                Retorno = Convert.ToString(Registro.GetValue(NomeChave));
                Registro.Close();
            }
            else if (_RaizRegistro == RaizRegistro.HKEY_CURRENT_USER)
            {
                RegistryKey Registro = Registry.CurrentUser.OpenSubKey(Caminho, true);
                Retorno = Convert.ToString(Registro.GetValue(NomeChave));
                Registro.Close();
            }
            else //if(_RaizRegistro == RaizRegistro.HKEY_USERS)
            {
                RegistryKey Registro = Registry.Users.OpenSubKey(Caminho, true);
                Retorno = Convert.ToString(Registro.GetValue(NomeChave));
                Registro.Close();
            }

            return Retorno;
        }
        catch (Exception erro)
        {
            CriarChaveRegedit(_RaizRegistro, Caminho, NomeChave, "");
            return ("Erro no leitura do Registro.\n" + erro.Message);
        }

    }

    /// <summary>
    /// Grava uma informação no registro do windows.
    /// ATENÇÃO: Cuidado com o que faz no registro do windows,
    /// dependendo da operação execultada vc vai danificar o Sistema Operacional,
    /// trazendo vários transtornos, então muita atenção no que mandar execultar no registro.
    /// </summary>
    /// <param name="_RaizRegistro">Informe a raíz do registro.</param>
    /// <param name="Caminho">Informe o caminho que será gravado o valor.</param>
    /// <param name="NomeChave">Informe o nome da chave onde existe o valor a ser gravado.</param>
    /// <param name="NomeValorSequencia">Informe o nome da sequencia onde o valor será gravado.</param>
    /// <param name="Valor">Informe o valor a ser gravado.</param>
    /// <returns>Retorna uma mensagem dizendo se a operação foi realizada com sucesso ou não.</returns>
    public static string CriarChaveRegedit(RaizRegistro _RaizRegistro, string Caminho, string NomeChave, string Valor)
    {
        try
        {
            if (_RaizRegistro == RaizRegistro.HKEY_LOCAL_MACHINE)
            {
                RegistryKey Registro = Registry.LocalMachine.CreateSubKey(Caminho);
                Registro.SetValue(NomeChave, Valor);
                Registro.Close();
            }
            else if (_RaizRegistro == RaizRegistro.HKEY_CLASSES_ROOT)
            {
                RegistryKey Registro = Registry.ClassesRoot.CreateSubKey(Caminho);
                Registro.SetValue(NomeChave, Valor);
                Registro.Close();
            }
            else if (_RaizRegistro == RaizRegistro.HKEY_CURRENT_CONFIG)
            {
                RegistryKey Registro = Registry.CurrentConfig.CreateSubKey(Caminho);
                Registro.SetValue(NomeChave, Valor);
                Registro.Close();
            }
            else if (_RaizRegistro == RaizRegistro.HKEY_CURRENT_USER)
            {
                RegistryKey Registro = Registry.CurrentUser.CreateSubKey(Caminho);
                Registro.SetValue(NomeChave, Valor);
                Registro.Close();
            }
            else //if(_RaizRegistro == RaizRegistro.HKEY_USERS)
            {
                RegistryKey Registro = Registry.Users.CreateSubKey(Caminho);
                Registro.SetValue(NomeChave, Valor);
                Registro.Close();
            }

            return "Chave criada com sucesso!";
        }
        catch (Exception erro)
        {
            throw erro; //("Erro ao tentar criar chave no registro do windows" + erro.Message);
        }
    }
}

