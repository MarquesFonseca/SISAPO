using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SISAPO.ClassesDiversas
{
    /// <summary>
    /// Executa comandos no SQL Server.
    /// </summary>
    public class SQLServer : IDisposable
    {
        public string StringConexao { get; set; }

        public SQLServer()
        {

        }

        public SQLServer(string _stringConexao)
        {
            StringConexao = _stringConexao;
        }

        /// <summary>
        /// Executa um comando SQL com número variado de parâmetros. Os objetos passados por parâmetro devem seguir a sequência
        /// dos parâmetros contidos na instrução SQL. O comando será executado utilizando uma transação para manter a segurança.
        /// </summary>
        /// <param name="comandoSql">Comando SQL a ser executado.</param>
        /// <param name="parametros">Lista com os objetos que contém os valores correspondentes aos parâmetros do comando passado.</param>
        /// <param name="nomeAplicacao">Nome do aplicativo para ser usado na transação.</param>
        /// <returns>Resultado do comando SQL.</returns>
        public object ExecutaComandoSQL(string comandoSql, List<object> valoresDosParametros,
            string nomeAplicacao)
        {
            object retorno = 0;
            SqlConnection conexao;
            SqlTransaction transacao;
            SqlCommand comando;

            try
            {
                if (string.IsNullOrEmpty(StringConexao)) throw new Exception("String de conexão vazia.");

                conexao = new SqlConnection(StringConexao);

                using (conexao)
                {
                    conexao.Open();

                    transacao = conexao.BeginTransaction(nomeAplicacao.ToUpper());

                    comando = new SqlCommand(comandoSql.ToUpper(), conexao);
                    comando.CommandType = CommandType.Text;
                    comando.Transaction = transacao;

                    List<string> listaParametros = RetornaParametrosComandoSql(comandoSql);
                    listaParametros.ForEach(item => comando.Parameters.Add(new SqlParameter(item, valoresDosParametros[listaParametros.IndexOf(item)])));

                    using (comando)
                    {
                        try
                        {
                            switch (RetornaTipoDeComandoSql(comando.CommandText))
                            {
                                case "SELECT":
                                    using (DataTable tabela = new DataTable())
                                    {
                                        using (SqlDataAdapter adaptadorSQL = new SqlDataAdapter(comando))
                                        {
                                            adaptadorSQL.Fill(tabela);
                                        }

                                        if (tabela.Columns.Count > 1)
                                        {
                                            retorno = tabela;
                                        }
                                        else if (tabela.Columns.Count == 1)
                                        {
                                            if (tabela.Rows.Count > 1)
                                            {
                                                retorno = tabela;
                                            }
                                            else if (tabela.Rows.Count == 1)
                                            {
                                                retorno = tabela.Rows[0][0];
                                            }
                                            else
                                            {
                                                retorno = null;
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    retorno = comando.ExecuteScalar();
                                    break;
                            }

                            transacao.Commit();
                        }
                        catch (Exception Ex)
                        {
                            transacao.Rollback(nomeAplicacao.ToUpper());
                            throw Ex;
                        }
                    }

                    conexao.Close();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return retorno;
        }

        /// <summary>
        /// Executa um comando SQL. Os objetos passados por parâmetro devem seguir a sequência
        /// dos parâmetros contidos na instrução SQL. O comando será executado utilizando uma 
        /// transação para manter a segurança.
        /// </summary>
        /// <param name="comandoSql">Comando SQL a ser executado.</param>
        /// <param name="nomeAplicacao">Nome do aplicativo para ser usado na transação.</param>
        /// <returns>Resultado do comando SQL.</returns>
        public object ExecutaComandoSQL(string comandoSql, string nomeAplicacao)
        {
            object retorno = 0;
            SqlConnection conexao;
            SqlTransaction transacao;
            SqlCommand comando;

            try
            {
                if (string.IsNullOrEmpty(StringConexao)) throw new Exception("String de conexão vazia.");
                conexao = new SqlConnection(StringConexao);

                using (conexao)
                {
                    conexao.Open();

                    transacao = conexao.BeginTransaction(nomeAplicacao.ToUpper());

                    comando = new SqlCommand(comandoSql.ToUpper(), conexao);
                    comando.CommandType = CommandType.Text;
                    comando.Transaction = transacao;

                    using (comando)
                    {
                        try
                        {
                            switch (RetornaTipoDeComandoSql(comando.CommandText))
                            {
                                case "SELECT":
                                    using (DataTable tabela = new DataTable())
                                    {
                                        using (SqlDataAdapter adaptadorSQL = new SqlDataAdapter(comando))
                                        {
                                            adaptadorSQL.Fill(tabela);
                                        }

                                        if (tabela.Columns.Count > 1)
                                        {
                                            retorno = tabela;
                                        }
                                        else if (tabela.Columns.Count == 1)
                                        {
                                            if (tabela.Rows.Count > 1)
                                            {
                                                retorno = tabela;
                                            }
                                            else if (tabela.Rows.Count == 1)
                                            {
                                                retorno = tabela.Rows[0][0];
                                            }
                                            else
                                            {
                                                retorno = null;
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    retorno = comando.ExecuteScalar();
                                    break;
                            }

                            transacao.Commit();
                        }
                        catch (Exception Ex)
                        {
                            transacao.Rollback(nomeAplicacao.ToUpper());
                            throw Ex;
                        }
                    }

                    conexao.Close();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return retorno;
        }

        /// <summary>
        /// Executa um comando SQL com número variado de parâmetros. Os objetos passados por parâmetro devem seguir a sequência
        /// dos parâmetros contidos na instrução SQL. O comando será executado utilizando uma transação para manter a segurança.
        /// </summary>
        /// <param name="comandoSql">Comando SQL a ser executado.</param>
        /// <param name="parametros">Lista com os objetos que contém os valores correspondentes aos parâmetros do comando passado.</param>
        /// <returns>Resultado do comando SQL.</returns>
        public object ExecutaComandoSQL(string comandoSql, List<object> valoresDosParametros)
        {
            object retorno = 0;
            SqlConnection conexao;
            SqlTransaction transacao;
            SqlCommand comando;

            try
            {
                if (string.IsNullOrEmpty(StringConexao)) throw new Exception("String de conexão vazia.");
                conexao = new SqlConnection(StringConexao);

                using (conexao)
                {
                    conexao.Open();

                    transacao = conexao.BeginTransaction();

                    comando = new SqlCommand(comandoSql.ToUpper(), conexao);
                    comando.CommandType = CommandType.Text;
                    comando.Transaction = transacao;

                    List<string> listaParametros = RetornaParametrosComandoSql(comandoSql);
                    listaParametros.ForEach(item => comando.Parameters.Add(new SqlParameter(item, valoresDosParametros[listaParametros.IndexOf(item)])));

                    using (comando)
                    {
                        try
                        {
                            switch (RetornaTipoDeComandoSql(comando.CommandText))
                            {
                                case "SELECT":
                                    using (DataTable tabela = new DataTable())
                                    {
                                        using (SqlDataAdapter adaptadorSQL = new SqlDataAdapter(comando))
                                        {
                                            adaptadorSQL.Fill(tabela);
                                        }

                                        if (tabela.Columns.Count > 1)
                                        {
                                            retorno = tabela;
                                        }
                                        else if (tabela.Columns.Count == 1)
                                        {
                                            if (tabela.Rows.Count > 1)
                                            {
                                                retorno = tabela;
                                            }
                                            else if (tabela.Rows.Count == 1)
                                            {
                                                retorno = tabela.Rows[0][0];
                                            }
                                            else
                                            {
                                                retorno = null;
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    retorno = comando.ExecuteScalar();
                                    break;
                            }

                            transacao.Commit();
                        }
                        catch (Exception Ex)
                        {
                            transacao.Rollback();
                            throw Ex;
                        }
                    }

                    conexao.Close();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return retorno;
        }

        /// <summary>
        /// Executa um comando SQL com número variado de parâmetros. Os objetos passados por parâmetro devem seguir a sequência
        /// dos parâmetros contidos na instrução SQL. O comando será executado utilizando uma transação para manter a segurança.
        /// </summary>
        /// <param name="comandoSql">Comando SQL a ser executado.</param>
        /// <param name="parametros">Lista com os objetos que contém os valores correspondentes aos parâmetros do comando passado.</param>
        /// <returns>Resultado do comando SQL.</returns>
        public object ExecutaComandoSQLSemTransacao(string comandoSql, List<object> valoresDosParametros)
        {
            object retorno = 0;
            SqlConnection conexao;
            SqlCommand comando;

            if (string.IsNullOrEmpty(StringConexao)) throw new Exception("String de conexão vazia.");
            conexao = new SqlConnection(StringConexao);

            using (conexao)
            {
                conexao.Open();
                comando = new SqlCommand(comandoSql.ToUpper(), conexao);
                comando.CommandType = CommandType.Text;

                List<string> listaParametros = RetornaParametrosComandoSql(comandoSql);
                listaParametros.ForEach(item => comando.Parameters.Add(new SqlParameter(item, valoresDosParametros[listaParametros.IndexOf(item)])));

                using (comando)
                {
                    switch (RetornaTipoDeComandoSql(comando.CommandText))
                    {
                        case "SELECT":
                            using (DataTable tabela = new DataTable())
                            {
                                using (SqlDataAdapter adaptadorSQL = new SqlDataAdapter(comando))
                                {
                                    adaptadorSQL.Fill(tabela);
                                }

                                if (tabela.Columns.Count > 1)
                                {
                                    retorno = tabela;
                                }
                                else if (tabela.Columns.Count == 1)
                                {
                                    if (tabela.Rows.Count > 1)
                                    {
                                        retorno = tabela;
                                    }
                                    else if (tabela.Rows.Count == 1)
                                    {
                                        retorno = tabela.Rows[0][0];
                                    }
                                    else
                                    {
                                        retorno = null;
                                    }
                                }
                            }
                            break;
                        default:
                            retorno = comando.ExecuteScalar();
                            break;
                    }
                }

                conexao.Close();
            }
            return retorno;
        }

        /// <summary>
        /// Executa um comando SQL. Os objetos passados por parâmetro devem seguir a sequência
        /// dos parâmetros contidos na instrução SQL. O comando será executado utilizando uma 
        /// transação para manter a segurança.
        /// </summary>
        /// <param name="comandoSql">Comando SQL a ser executado.</param>
        /// <returns>Resultado do comando SQL.</returns>
        public object ExecutaComandoSQL(string comandoSql)
        {
            object retorno = 0;
            if (string.IsNullOrEmpty(StringConexao)) throw new Exception("String de conexão vazia.");
            using (SqlConnection conexao = new SqlConnection(StringConexao))
            {
                try
                {
                    conexao.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Não foi possível se conectar ao banco de dados.\nMotivo: {0}", ex.Message));
                }

                SqlTransaction transacao = conexao.BeginTransaction();

                try
                {
                    SqlCommand comando = new SqlCommand(comandoSql.ToUpper(), conexao)
                    {
                        CommandType = CommandType.Text,
                        Transaction = transacao
                    };

                    if (RetornaTipoDeComandoSql(comando.CommandText) == "SELECT")
                    {
                        DataTable tabela = new DataTable();
                        SqlDataAdapter adaptadorSQL = new SqlDataAdapter(comando);
                        adaptadorSQL.Fill(tabela);

                        if (tabela.Columns.Count > 1)
                            retorno = tabela;
                        else if (tabela.Columns.Count == 1 && tabela.Rows.Count > 1)
                            retorno = tabela;
                        else if (tabela.Columns.Count == 1 && tabela.Rows.Count == 1)
                            retorno = tabela.Rows[0][0];
                        else
                            retorno = null;

                        tabela.Dispose();
                        adaptadorSQL.Dispose();
                    }
                    else
                        retorno = comando.ExecuteScalar();

                    transacao.Commit();
                    comando.Dispose();
                    conexao.Close();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw ex;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Retorna os parâmetros de um comando SQL.
        /// </summary>
        /// <param name="comandoSql">Comando SQL.</param>
        /// <returns>Lista de parâmetros do comando.</returns>
        /// <by>Ricardo Santos Cruz</by>
        /// <date>09-10-2009</date>
        private List<string> RetornaParametrosComandoSql(string comandoSql)
        {
            //Seleciona os parâmetros do comando passado
            string[] palavasDoComando = comandoSql.Split(new char[] { ',', ' ', '=', '(', ')', '+' });
            var parametrosComando = from p in palavasDoComando where p != string.Empty && p[0] == '@' select p;
            List<string> listaParametros = parametrosComando.Distinct().ToList();
            return listaParametros;
        }

        /// <summary>
        /// Retorna o tipo do comando SQL. (SELECT, INSERT, UPDATE, DELETE).
        /// Se não encontar retornará vazio.
        /// </summary>
        /// <param name="comandoSql">Comando SQL.</param>
        /// <returns>Tipo de comando.</returns>
        private string RetornaTipoDeComandoSql(string comandoSql)
        {
            comandoSql = comandoSql.Trim();
            //Seleciona os parâmetros do comando passado
            string[] palavasDoComando = comandoSql.Split(new char[] { ',', ' ', '=', '(', ')' });

            if (palavasDoComando.Count() > 0)
            {
                return palavasDoComando[0].ToUpper();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Retorna uma conexão aberta com o banco de dados
        /// </summary>
        /// <returns>SqlConnection estado ABERTO</returns>
        private SqlConnection ConnConexaoAberta()
        {
            SqlConnection conn = new SqlConnection(StringConexao);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conn;
        }

        /// <summary>
        /// POPULA UM DATATABLE DE UM DATASET COM UM INSTRUCAO T_SQL
        /// </summary>
        /// <param name="TabelaDataSet">TABELA DO SEU DATASET</param>
        /// <param name="T_SQL">INSTRUCAO T_SQL</param>
        /// <returns>RETURNA O NUMERO DE ROWS</returns>
        public void Consulta(DataTable Tabela, string T_SQL)
        {
            Consulta(Tabela, T_SQL, false);
        }

        public DataTable Consulta(string T_SQL)
        {
            DataTable dt = new DataTable();
            Consulta(dt, T_SQL, false);
            return dt;
        }

        public void Consulta(DataTable Tabela, string T_SQL, bool LimparTabela)
        {
            SqlDataAdapter adpt;
            if (LimparTabela)
                Tabela.Clear();
            try
            {
                adpt = new SqlDataAdapter(T_SQL, ConnConexaoAberta());
                adpt.Fill(Tabela);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// POPULA UM DATATABLE DE UM DATASET COM UM INSTRUCAO T_SQL
        /// </summary>
        /// <param name="TabelaDataSet">TABELA DO SEU DATASET</param>
        /// <param name="LimparTabela">LIMPAR OU NÃO A TABELA PASSADA ANTES DE PESQUISAR</param>
        /// <param name="T_SQL">INSTRUCAO T_SQL</param>
        /// <returns>RETURNA O NUMERO DE ROWS</returns>
        public void Consulta(DataSet DataSetConsulta, string T_SQL)
        {
            SqlDataAdapter adpt;

            try
            {
                string nomeTabela;
                adpt = new SqlDataAdapter(NomeTabela(T_SQL, out nomeTabela), ConnConexaoAberta());
                adpt.Fill(DataSetConsulta, nomeTabela);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Captura o nome da primeira tabela na Query. No caso de existit Alias no nome 
        /// da tabela especificado pela palavra "AS", o comando da Query será atualizado
        /// para substituir os filtros que são montados automaticamente pelo sistema.
        /// </summary>
        /// <param name="T_SQL">Comando SQL</param>
        /// <param name="_nomeTabela">Variável para receber o nome da tabela</param>
        /// <returns>Comando SQL atualizado se necessário</returns>
        public static string NomeTabela(string T_SQL, out string _nomeTabela)
        {
            ///Armazena o comando SQL palavra por palavra
            string[] comando = T_SQL.ToUpper().RemoveExcessoEspaco().Split(new char[] { ',', ' ', '=', '(', ')', ';' });

            string nomeTabela = comando[comando.ToList().IndexOf("FROM") + 1]; //Nome da tabela na Query
            string nomeAlias = string.Empty; //Alias da Query se existir
            //bool temAlias = false; //Indica se existe Alias na Query

            if (comando.Contains("AS")) // Se tiver alias na consulta
            {
                //bool temAlias = true;
                _nomeTabela =
                    nomeAlias = comando[comando.ToList().IndexOf("FROM") + 3];
            }
            else
            {
                _nomeTabela = nomeTabela;
            }

            return T_SQL;
        }

        public void ExecutaConsultaSQL(string comandoSql, List<object> valoresDosParametros,
            ref DataSet DataSetConsulta)
        {
            SqlConnection conexao;
            SqlCommand comando;

            try
            {
                if (string.IsNullOrEmpty(StringConexao)) throw new Exception("String de conexão vazia.");
                conexao = new SqlConnection(StringConexao);

                using (conexao)
                {
                    conexao.Open();

                    comando = new SqlCommand(comandoSql.ToUpper(), conexao);
                    comando.CommandType = CommandType.Text;

                    List<string> listaParametros = RetornaParametrosComandoSql(comandoSql);
                    listaParametros.ForEach(item => comando.Parameters.Add(new SqlParameter(item, valoresDosParametros[listaParametros.IndexOf(item)])));

                    using (comando)
                    {
                        try
                        {
                            if (RetornaTipoDeComandoSql(comando.CommandText) == "SELECT")
                            {
                                using (SqlDataAdapter adaptadorSQL = new SqlDataAdapter(comando))
                                {
                                    adaptadorSQL.Fill(DataSetConsulta);
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            throw Ex;
                        }
                    }
                    conexao.Close();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void ExecutaConsultaSQL(string comandoSql, object valorDoParametro, out DataSet DataSetConsulta,
            TipoInformacao TipoCampo)
        {
            DataSetConsulta = new DataSet();

            SqlConnection conexao;
            SqlCommand comando;

            if (string.IsNullOrEmpty(StringConexao)) throw new Exception("String de conexão vazia.");
            conexao = new SqlConnection(StringConexao);

            using (conexao)
            {
                conexao.Open();

                comando = new SqlCommand(comandoSql.ToUpper(), conexao);
                comando.CommandType = CommandType.Text;

                string Parametro = RetornaParametrosComandoSql(comandoSql).First();

                switch (TipoCampo)
                {
                    case TipoInformacao.Texto:
                    case TipoInformacao.CEP:
                    case TipoInformacao.CNPJ:
                    case TipoInformacao.CPF:
                    case TipoInformacao.IE:
                    case TipoInformacao.Telefone:
                        comando.Parameters.Add(new SqlParameter(Parametro, valorDoParametro) { SqlDbType = SqlDbType.VarChar });
                        break;
                    case TipoInformacao.Decimal:
                        comando.Parameters.Add(new SqlParameter(Parametro, valorDoParametro.ToString().Replace(".", string.Empty).Replace(',', '.')) { SqlDbType = SqlDbType.Decimal });
                        break;
                    case TipoInformacao.Data:
                        comando.Parameters.Add(new SqlParameter(Parametro, Convert.ToDateTime(valorDoParametro.ToString())) { SqlDbType = SqlDbType.DateTime });
                        break;
                    case TipoInformacao.Numerico:
                        if (valorDoParametro.ToString().Length > 9)
                            comando.Parameters.Add(new SqlParameter(Parametro, valorDoParametro) { SqlDbType = SqlDbType.VarChar });
                        else
                            comando.Parameters.Add(new SqlParameter(Parametro, valorDoParametro) { SqlDbType = SqlDbType.Int });
                        break;
                }

                using (comando)
                    if (RetornaTipoDeComandoSql(comando.CommandText) == "SELECT")
                        using (SqlDataAdapter adaptadorSQL = new SqlDataAdapter(comando))
                            adaptadorSQL.Fill(DataSetConsulta);

                conexao.Close();
            }
        }

        public void ExecutaConsultaSQL(string comandoSql, out DataSet DataSetConsulta)
        {
            DataSetConsulta = new DataSet();

            SqlConnection conexao;
            SqlCommand comando;

            try
            {
                if (string.IsNullOrEmpty(StringConexao)) throw new Exception("String de conexão vazia.");
                conexao = new SqlConnection(StringConexao);

                using (conexao)
                {
                    conexao.Open();

                    comando = new SqlCommand(comandoSql.ToUpper(), conexao);
                    comando.CommandType = CommandType.Text;

                    using (comando)
                    {
                        try
                        {
                            if (RetornaTipoDeComandoSql(comando.CommandText) == "SELECT")
                            {
                                using (SqlDataAdapter adaptadorSQL = new SqlDataAdapter(comando))
                                {
                                    adaptadorSQL.Fill(DataSetConsulta);
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            throw Ex;
                        }
                    }
                    conexao.Close();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// POPULA UM DATATABLE DE UM DATASET COM UM INSTRUCAO T_SQL
        /// </summary>
        /// <param name="TabelaDataSet">TABELA DO SEU DATASET</param>
        /// <param name="LimparTabela">LIMPAR OU NÃO A TABELA PASSADA ANTES DE PESQUISAR</param>
        /// <param name="T_SQL">INSTRUCAO T_SQL</param>
        /// <returns>RETURNA O NUMERO DE ROWS</returns>
        public void Consulta(DataSet DataSetConsulta, string[] T_SQL)
        {
            try
            {
                T_SQL.ToList().ForEach(r => Consulta(DataSetConsulta, r.ToUpper().RemoveExcessoEspaco()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Verrifica se um registro ja se encontra no banco de dados do sistema
        /// </summary>
        /// <param name="Campo">Nome do campo a ser verificado</param>
        /// <param name="Tabela">Tabela que será verificada</param>
        /// <param name="Condicao">Condição para ser colocada no where da consulta</param>
        /// <returns>retorna "True" se o registro já existir</returns>        
        public bool Existe(string Campo, string Tabela, string Condicao)
        {
            bool Retorno = true;
            string operador = string.Empty;

            //Monta a Query de consulta de acordo com os parâmetros
            StringBuilder Query = new StringBuilder("if exists(select " + Campo + " from " + Tabela);
            if (Condicao != "")
            {
                Query.Append(" where " + Condicao + ")");
            }
            else
            {
                Query.Append(")");
            }

            Query.Append(" select 1 else select 0");

            //Executa a consulta e captura seu retorno
            using (SQLServer SQL = new SQLServer(StringConexao))
            {
                int valor = (int)SQL.ExecutaComandoSQL(Query.ToString());

                if (valor == 0)
                {
                    Retorno = false;
                }
            }

            return Retorno;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
