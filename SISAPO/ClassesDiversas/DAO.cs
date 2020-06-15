using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Npgsql;
using NpgsqlTypes;
using System.Data.OleDb;

public class DAO : IDisposable
{
    public string Aplicacao { get; set; }

    public string Banco { get; set; }

    public string Senha { get; set; }

    public string Servidor { get; set; }

    public string StrConexa { get; set; }

    public TipoBanco TipoBanco { get; set; }

    public string Usuario { get; set; }

    /// <summary>
    /// Metodo utilizado para inicializar uma conexão.
    /// Por padrão ele recebe as variaveis setadas na class DAO.
    /// </summary>
    public DAO()
    {
        this.CarregaStrConexao();
    }

    /// <summary>
    /// Metodo utilizado para iniciar uma conexão.
    /// </summary>
    /// <param name="TipoBD">Informe o Tipo do Banco de Dados.</param>
    /// <param name="Servidor">Informe o Servidor do Banco.</param>
    /// <param name="Usuario">Informe o Usuario do Banco.</param>
    /// <param name="Senha">Informe a Senha do Banco.</param>
    /// <param name="Banco">Inform o nome do Banco de Dados.</param>
    public DAO(TipoBanco TipoBD, string Servidor, string Usuario, string Senha, string Banco)
    {
        this.CarregaStrConexao(TipoBD, Servidor, Usuario, Senha, Banco);
    }

    /// <summary>
    /// Metodo utilizado para iniciar uma conexão.
    /// </summary>
    /// <param name="Tipo">Informe o Tipo do Banco de Dados Ex: SqlServer, Firebird, Mysql e etc.</param>
    /// <param name="StringConexao">Chama uma strig de conexao ja inializada. ex: DAO.StrConexa ou outra que queira chamar como do config.</param>
    public DAO(TipoBanco Tipo, string StringConexao)
    {
        this.TipoBanco = Tipo;
        this.StrConexa = StringConexao;
        //this.CarregaStrConexao();
    }

    /// <summary>
    /// Método utilizado para Carregar uma conexao.
    /// Esse método eu coloco manualmente os dados da minha conexão caso queira.
    /// </summary>
    public void CarregaStrConexao()
    {
        using (DataSet Ds = new DataSet("CONFIG"))
        {
            string ArquivoDS = (string.Compare(this.Aplicacao, "ServidorLicenca", false) == 0) ? string.Format(@"{0}\{1}", Application.StartupPath, "CONFIG.XML") : "CONFIG.XML";
            FileInfo ArqDS = new FileInfo(ArquivoDS);
            if (!ArqDS.Exists)
            {
                this.TipoBanco = TipoBanco.SqlServer;
                this.Servidor = @"222.222.3.2\SQL2008";
                this.Usuario = "SUPERSCERG";
                this.Senha = "S35SUP5RSRG";
                this.Banco = "BANCOWEB_NOVO";
                this.StrConexa = MontaStringConexao(Servidor, Usuario, Senha, Banco, TipoBanco);
                this.GravaStrConexao(this.TipoBanco, this.Servidor, this.Banco, "", "");
            }
            else
            {
                Ds.ReadXml(ArquivoDS);
                if ((Ds.Tables.Count > 0) && (Ds.Tables[0].Rows.Count > 0))
                {
                    //this.StrConexa = Ds.Tables[0].Rows[0]["STRCONEXAO"].ToString();
                    this.TipoBanco = Ds.Tables[0].Rows[0]["TIPOBANCO"].ToString().Equals("SQLSERVER") ? TipoBanco.SqlServer : TipoBanco.FireBird;
                    this.Servidor = Ds.Tables[0].Rows[0]["SERVIDOR"].ToString();
                    this.Banco = Ds.Tables[0].Rows[0]["BANCO"].ToString();
                    //this.Usuario = Ds.Tables[0].Rows[0]["USUARIO"].ToString();
                    //this.Senha = Ds.Tables[0].Rows[0]["SENHA"].ToString();
                    this.Usuario = "SUPERSCERG";
                    this.Senha = "S35SUP5RSRG";
                    this.StrConexa = MontaStringConexao(Servidor, Usuario, Senha, Banco, TipoBanco);
                }
            }
        }
    }

    /// <summary>
    /// Método utilizado para carregar uma conexão.
    /// </summary>
    /// <param name="TipoBD">Informe o Tipo do Banco Ex: SqlServer, MySql, Firebird, e etc.</param>
    /// <param name="Servidor">Informe o Servido onde se encontra o Banco de dados.</param>
    /// <param name="Usuario">Informe o usuário do Banco de Dados.</param>
    /// <param name="Senha">Informe a Senha do Banco de Dados.</param>
    /// <param name="Banco">Informe o nome do Banco que deseja utilizar.</param>
    public void CarregaStrConexao(TipoBanco TipoBD, string Servidor, string Usuario, string Senha, string Banco)
    {
        using (DataSet Ds = new DataSet("CONFIG"))
        {
            string ArquivoDS = (string.Compare(this.Aplicacao, "ServidorLicenca", false) == 0) ? string.Format(@"{0}\{1}", Application.StartupPath, "CONFIG.XML") : "CONFIG.XML";
            FileInfo ArqDS = new FileInfo(ArquivoDS);
            if (!ArqDS.Exists)
            {
                this.TipoBanco = TipoBD;
                this.Servidor = Servidor;
                this.Usuario = Usuario;
                this.Senha = Senha;
                this.Banco = Banco;
                this.StrConexa = MontaStringConexao(Servidor, Usuario, Senha, Banco, TipoBanco);
                this.GravaStrConexao(this.TipoBanco, this.Servidor, this.Banco, "", "");

            }
            else
            {
                Ds.ReadXml(ArquivoDS);
                if ((Ds.Tables.Count > 0) && (Ds.Tables[0].Rows.Count > 0))
                {
                    //this.StrConexa = Ds.Tables[0].Rows[0]["STRCONEXAO"].ToString();
                    this.TipoBanco = Ds.Tables[0].Rows[0]["TIPOBANCO"].ToString().Equals("SQLSERVER") ? TipoBanco.SqlServer : TipoBanco.FireBird;
                    this.Servidor = Ds.Tables[0].Rows[0]["SERVIDOR"].ToString();
                    this.Banco = Ds.Tables[0].Rows[0]["BANCO"].ToString();
                    //this.Usuario = Ds.Tables[0].Rows[0]["USUARIO"].ToString();
                    //this.Senha = Ds.Tables[0].Rows[0]["SENHA"].ToString();
                    this.Usuario = "SUPERSCERG";
                    this.Senha = "S35SUP5RSRG";
                    this.StrConexa = MontaStringConexao(Servidor,Usuario,Senha,Banco,TipoBanco);
                }
            }
        }
    }

    /// <summary>
    /// Retorna a Data e Hora do servidor de Banco de Dados.
    /// </summary>
    /// <returns></returns>
    public DateTime DataHoraServidor()
    {
        DateTime Retorno = DateTime.Now;
        #region FireBird
        if (this.TipoBanco == TipoBanco.FireBird)
        {
            DataTable Dt = this.RetornaDataTable("select current_time,current_date from rdb$database;");
            DateTime Temp1 = Convert.ToDateTime(Dt.Rows[0][0].ToString());
            return Convert.ToDateTime(Dt.Rows[0][1].ToString()).Date.AddHours((double)Temp1.Hour).AddMinutes((double)Temp1.Minute).AddSeconds((double)Temp1.Second).AddMilliseconds((double)Temp1.Millisecond);
        }
        #endregion
        #region SqlServer
        if (this.TipoBanco == TipoBanco.SqlServer)
        {
            Retorno = Convert.ToDateTime(this.RetornaValor("SELECT GETDATE()").ToString());
        }
        #endregion
        #region MySql
        if (this.TipoBanco == TipoBanco.MySql)
        {
            Retorno = Convert.ToDateTime(this.RetornaValor("SELECT NOW()").ToString());
        }
        #endregion
        #region PostgreeSql
        if (this.TipoBanco == TipoBanco.PostgreeSQL)
        {
            Retorno = Convert.ToDateTime(this.RetornaValor("SELECT NOW()").ToString());
        }
        #endregion
        return Retorno;
    }

    public void Dispose() { }

    public int ExecutaSQL(string Query)
    {
        Exception ex;
        int Retorno = 0;
        #region FireBird
        if (this.TipoBanco == TipoBanco.FireBird)
        {
            FbConnection Con = this.RetornaConexaoFireBird();
            Con.Open();
            FbTransaction Trans = Con.BeginTransaction();
            FbCommand Cmd = new FbCommand(Query, Con, Trans);
            try
            {
                try
                {
                    Retorno = Cmd.ExecuteNonQuery();
                    Trans.Commit();
                }
                catch (Exception exception1)
                {
                    ex = exception1;
                    Trans.Rollback();
                    throw ex;
                }
                return Retorno;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                Trans.Dispose();
                Cmd.Dispose();
            }
            //return Retorno;
        }
        #endregion
        #region SqlServer
        if (this.TipoBanco == TipoBanco.SqlServer)
        {
            SqlConnection Con = this.RetornaConexaoSQLServer();
            Con.Open();
            SqlTransaction Trans = Con.BeginTransaction();
            SqlCommand Cmd = new SqlCommand(Query, Con, Trans);
            try
            {
                Retorno = Cmd.ExecuteNonQuery();
                Trans.Commit();
            }
            catch (Exception exception2)
            {
                ex = exception2;
                Trans.Rollback();
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                Trans.Dispose();
                Cmd.Dispose();
            }
            return Retorno;
        }
        #endregion
        #region MySql
        if (this.TipoBanco == TipoBanco.MySql)
        {
            MySqlConnection Con = this.RetornaConexaoMySql();
            Con.Open();
            MySqlTransaction Trans = Con.BeginTransaction();
            MySqlCommand Cmd = new MySqlCommand(Query, Con, Trans);
            try
            {
                Retorno = Cmd.ExecuteNonQuery();
                Trans.Commit();
            }
            catch (Exception exception3)
            {
                ex = exception3;
                Trans.Rollback();
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                Trans.Dispose();
                Cmd.Dispose();
            }
            return Retorno;
        }
        #endregion
        #region PostgreeSQL
        if (this.TipoBanco == TipoBanco.PostgreeSQL)
        {
            NpgsqlConnection Con = this.RetornaConexaoPostgreeSQL();
            Con.Open();
            NpgsqlTransaction Trans = Con.BeginTransaction();
            NpgsqlCommand Cmd = new NpgsqlCommand(Query, Con, Trans);
            try
            {
                Retorno = Cmd.ExecuteNonQuery();
                Trans.Commit();
            }
            catch (Exception exception4)
            {
                ex = exception4;
                Trans.Rollback();
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                Trans.Dispose();
                Cmd.Dispose();
            }
            return Retorno;
        }
        #endregion
        #region OleDB
        if (this.TipoBanco == TipoBanco.OleDb)
        {
            OleDbConnection Con = this.RetornaConexaoOleDb();
            Con.Open();
            OleDbTransaction Trans = Con.BeginTransaction();
            OleDbCommand Cmd = new OleDbCommand(Query, Con, Trans);
            try
            {
                Retorno = Cmd.ExecuteNonQuery();
                Trans.Commit();
            }
            catch (Exception exception5)
            {
                ex = exception5;
                Trans.Rollback();
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                Trans.Dispose();
                Cmd.Dispose();
            }
            return Retorno;
        }
        #endregion
        return Retorno;
    }

    public void ExecutaSQL(string Query, List<Parametros> Parametros)
    {
        int i;
        Exception ex;
        #region FireBird
        if (this.TipoBanco == TipoBanco.FireBird)
        {
            FbConnection Con = this.RetornaConexaoFireBird();
            Con.Open();
            FbTransaction Trans = Con.BeginTransaction();
            FbCommand Cmd = new FbCommand(Query, Con, Trans)
            {
                CommandType = CommandType.Text
            }; ;
            for (i = 0; i < Parametros.Count; i++)
            {
                switch (Parametros[i].Tipo)
                {
                    case TipoCampo.Varchar:
                        Cmd.Parameters.Add(Parametros[i].Nome, FbDbType.VarChar, Parametros[i].Tamanho);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.Int:
                        Cmd.Parameters.Add(Parametros[i].Nome, FbDbType.Integer);
                        Cmd.Parameters[i].Value = Convert.ToInt32(Parametros[i].Valor);
                        break;

                    case TipoCampo.Double:
                        Cmd.Parameters.Add(Parametros[i].Nome, FbDbType.Float);
                        Cmd.Parameters[i].Value = Convert.ToDouble(Parametros[i].Valor);
                        break;

                    case TipoCampo.Text:
                        Cmd.Parameters.Add(Parametros[i].Nome, FbDbType.Text);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.DateTime:
                        Cmd.Parameters.Add(Parametros[i].Nome, FbDbType.Date);
                        Cmd.Parameters[i].Value = Convert.ToDateTime(Parametros[i].Valor);
                        break;
                }
            }
            try
            {
                Cmd.ExecuteNonQuery();
                Trans.Commit();
            }
            catch (Exception exception1)
            {
                ex = exception1;
                Trans.Rollback();
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                Trans.Dispose();
                Cmd.Dispose();
            }
        }
        #endregion
        #region SqlServer
        else if (this.TipoBanco == TipoBanco.SqlServer)
        {
            SqlConnection Con = this.RetornaConexaoSQLServer();
            Con.Open();
            SqlTransaction Trans = Con.BeginTransaction();
            SqlCommand Cmd = new SqlCommand(Query, Con, Trans)
            {
                CommandType = CommandType.Text
            }; ;
            for (i = 0; i < Parametros.Count; i++)
            {
                switch (Parametros[i].Tipo)
                {
                    case TipoCampo.Varchar:
                        Cmd.Parameters.Add(Parametros[i].Nome, SqlDbType.VarChar, Parametros[i].Tamanho);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.Int:
                        Cmd.Parameters.Add(Parametros[i].Nome, SqlDbType.Int);
                        Cmd.Parameters[i].Value = Convert.ToInt32(Parametros[i].Valor);
                        break;

                    case TipoCampo.Double:
                        Cmd.Parameters.Add(Parametros[i].Nome, SqlDbType.Float);
                        Cmd.Parameters[i].Value = Convert.ToDouble(Parametros[i].Valor);
                        break;

                    case TipoCampo.Text:
                        Cmd.Parameters.Add(Parametros[i].Nome, SqlDbType.NText);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.DateTime:
                        Cmd.Parameters.Add(Parametros[i].Nome, SqlDbType.DateTime);
                        Cmd.Parameters[i].Value = Convert.ToDateTime(Parametros[i].Valor);
                        break;
                }
            }
            try
            {
                Cmd.ExecuteNonQuery();
                Trans.Commit();
            }
            catch (Exception exception2)
            {
                ex = exception2;
                Trans.Rollback();
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                Trans.Dispose();
                Cmd.Dispose();
            }
        }
        #endregion
        #region MySql
        else if (this.TipoBanco == TipoBanco.MySql)
        {
            MySqlConnection Con = this.RetornaConexaoMySql();
            Con.Open();
            MySqlTransaction Trans = Con.BeginTransaction();
            MySqlCommand Cmd = new MySqlCommand(Query, Con, Trans)
            {
                CommandType = CommandType.Text
            }; ;
            for (i = 0; i < Parametros.Count; i++)
            {
                switch (Parametros[i].Tipo)
                {
                    case TipoCampo.Varchar:
                        Cmd.Parameters.Add(Parametros[i].Nome, MySqlDbType.VarChar, Parametros[i].Tamanho);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.Int:
                        Cmd.Parameters.Add(Parametros[i].Nome, MySqlDbType.Int32);
                        Cmd.Parameters[i].Value = Convert.ToInt32(Parametros[i].Valor);
                        break;

                    case TipoCampo.Double:
                        Cmd.Parameters.Add(Parametros[i].Nome, MySqlDbType.Float);
                        Cmd.Parameters[i].Value = Convert.ToDouble(Parametros[i].Valor);
                        break;

                    case TipoCampo.Text:
                        Cmd.Parameters.Add(Parametros[i].Nome, MySqlDbType.Text);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.DateTime:
                        Cmd.Parameters.Add(Parametros[i].Nome, MySqlDbType.DateTime);
                        Cmd.Parameters[i].Value = Convert.ToDateTime(Parametros[i].Valor);
                        break;
                }
            }
            try
            {
                Cmd.ExecuteNonQuery();
                Trans.Commit();
            }
            catch (Exception exception3)
            {
                ex = exception3;
                Trans.Rollback();
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                Trans.Dispose();
                Cmd.Dispose();
            }
        }
        #endregion
        #region PostgreeSQL
        else if (this.TipoBanco == TipoBanco.PostgreeSQL)
        {
            NpgsqlConnection Con = this.RetornaConexaoPostgreeSQL();
            Con.Open();
            NpgsqlTransaction Trans = Con.BeginTransaction();
            NpgsqlCommand Cmd = new NpgsqlCommand(Query, Con, Trans)
            {
                CommandType = CommandType.Text
            }; ;
            for (i = 0; i < Parametros.Count; i++)
            {
                switch (Parametros[i].Tipo)
                {
                    case TipoCampo.Varchar:
                        Cmd.Parameters.Add(Parametros[i].Nome, NpgsqlDbType.Char, Parametros[i].Tamanho);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.Int:
                        Cmd.Parameters.Add(Parametros[i].Nome, NpgsqlDbType.Integer);
                        Cmd.Parameters[i].Value = Convert.ToInt32(Parametros[i].Valor);
                        break;

                    case TipoCampo.Double:
                        Cmd.Parameters.Add(Parametros[i].Nome, NpgsqlDbType.Double);
                        Cmd.Parameters[i].Value = Convert.ToDouble(Parametros[i].Valor);
                        break;

                    case TipoCampo.Text:
                        Cmd.Parameters.Add(Parametros[i].Nome, NpgsqlDbType.Text);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.DateTime:
                        Cmd.Parameters.Add(Parametros[i].Nome, NpgsqlDbType.Timestamp);
                        Cmd.Parameters[i].Value = Convert.ToDateTime(Parametros[i].Valor);
                        break;
                }
            }
            try
            {
                Cmd.ExecuteNonQuery();
                Trans.Commit();
            }
            catch (Exception exception4)
            {
                ex = exception4;
                Trans.Rollback();
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                Trans.Dispose();
                Cmd.Dispose();
            }
        }
        #endregion
        #region OleDb
        else if (this.TipoBanco == TipoBanco.OleDb)
        {
            OleDbConnection Con = this.RetornaConexaoOleDb();
            Con.Open();
            OleDbTransaction Trans = Con.BeginTransaction();
            OleDbCommand Cmd = new OleDbCommand(Query, Con, Trans)
            {
                CommandType = CommandType.Text
            }; ;
            for (i = 0; i < Parametros.Count; i++)
            {
                switch (Parametros[i].Tipo)
                {
                    case TipoCampo.Varchar:
                        Cmd.Parameters.Add(Parametros[i].Nome, OleDbType.Char, Parametros[i].Tamanho);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.Int:
                        Cmd.Parameters.Add(Parametros[i].Nome, OleDbType.Integer);
                        Cmd.Parameters[i].Value = Convert.ToInt32(Parametros[i].Valor);
                        break;

                    case TipoCampo.Double:
                        Cmd.Parameters.Add(Parametros[i].Nome, OleDbType.Double);
                        Cmd.Parameters[i].Value = Convert.ToDouble(Parametros[i].Valor);
                        break;

                    case TipoCampo.Text:
                        Cmd.Parameters.Add(Parametros[i].Nome, OleDbType.VarChar);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.DateTime:
                        Cmd.Parameters.Add(Parametros[i].Nome, OleDbType.DBTimeStamp);
                        Cmd.Parameters[i].Value = Convert.ToDateTime(Parametros[i].Valor);
                        break;
                }
            }
            try
            {
                Cmd.ExecuteNonQuery();
                Trans.Commit();
            }
            catch (Exception exception5)
            {
                ex = exception5;
                Trans.Rollback();
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                Trans.Dispose();
                Cmd.Dispose();
            }
        }
        #endregion

    }

    public void GravaStrConexao(TipoBanco Tp, string Servidor, string Banco, string Usuario, string Senha)
    {
        RetornaDataSet("CONFIG", new string[] { "STRCONEXAO", "TIPOBANCO", "SERVIDOR", "BANCO", "USUARIO", "SENHA" }, new string[] { MontaStringConexao(Servidor, Usuario, Senha, Banco, Tp), Tp.ToString().ToUpper(), Servidor, Banco, Usuario, Senha }).WriteXml("CONFIG.XML");
    }

    public static string MontaStringConexao(string Servidor, string Usuario, string Senha, string Banco, TipoBanco TipoDB)
    {
        string Resultado = string.Empty;
        switch (TipoDB)
        {
            case TipoBanco.FireBird:
                return string.Format("User={0};Password={1};Database={2};DataSource={3};", new object[] { Usuario, Senha, Banco, Servidor });

            case TipoBanco.SqlServer:
                return string.Format("Data Source={0};User ID={1};Initial Catalog={2};Password={3};Application Name=ServidorLicenca", new object[] { Servidor, Usuario, Banco, Senha });

            case TipoBanco.MySql:
                return string.Format("server={0};User Id={1};Password={2};database={3}", new object[] { Servidor, Usuario, Senha, Banco });

            case TipoBanco.PostgreeSQL:
                return string.Format("server={0};User Id={1};Password={2};database={3}", new object[] { Servidor, Usuario, Senha, Banco });

            case TipoBanco.OleDb:
                return string.Format("{0}", new object[] { Banco });
        }
        return Resultado;
    }

    public void PreencheComboBox(ComboBox Cbb, string StrSelect)
    {
        this.PreencheComboBox(Cbb, StrSelect, false);
    }

    public void PreencheComboBox(ComboBox Cbb, string StrSelect, bool AdicionaLimpo)
    {
        using (DataTable Tbl = this.RetornaDataTable(StrSelect))
        {
            if (AdicionaLimpo)
            {
                Cbb.Items.Add("");
            }
            foreach (DataRow Row in Tbl.Rows)
            {
                Cbb.Items.Add(Row[0].ToString());
            }
        }
    }

    public FbConnection RetornaConexaoFireBird()
    {
        return new FbConnection(this.StrConexa);
    }

    public SqlConnection RetornaConexaoSQLServer()
    {
        return new SqlConnection(this.StrConexa.Replace("ServidorLicenca", string.Format("ServidorLicenca-{0}", Environment.MachineName)));
    }

    public MySqlConnection RetornaConexaoMySql()
    {
        return new MySqlConnection(this.StrConexa);
    }

    public NpgsqlConnection RetornaConexaoPostgreeSQL()
    {
        return new NpgsqlConnection(this.StrConexa);
    }

    public OleDbConnection RetornaConexaoOleDb()
    {
        return new OleDbConnection(this.StrConexa);
    }

    public int RetornaCount(string Tabela)
    {
        return Convert.ToInt32(this.RetornaValor(string.Format("SELECT COUNT(*) FROM {0}", Tabela)));
    }

    public DataRow RetornaDataRow(string Query)
    {
        return this.RetornaDataSet(Query).Tables[0].Rows[0];
    }

    public DataSet RetornaDataSet(string Query)
    {
        using (DataSet Ds = new DataSet())
        {
            #region FireBird
            if (this.TipoBanco == TipoBanco.FireBird)
            {
                FbConnection Con = this.RetornaConexaoFireBird();
                FbDataAdapter Adpt = new FbDataAdapter(Query, Con);
                try
                {
                    Adpt.Fill(Ds);
                }
                finally
                {
                    Adpt.Dispose();
                    Con.Dispose();
                }
            }
            #endregion
            #region SqlServer
            else if (this.TipoBanco == TipoBanco.SqlServer)
            {
                SqlConnection Con = this.RetornaConexaoSQLServer();
                SqlDataAdapter Adpt = new SqlDataAdapter(Query, Con);
                try
                {
                    Adpt.Fill(Ds);
                }
                finally
                {
                    Adpt.Dispose();
                    Con.Dispose();
                }
            }
            #endregion
            #region MySql
            else if (this.TipoBanco == TipoBanco.MySql)
            {
                MySqlConnection Con = this.RetornaConexaoMySql();
                MySqlDataAdapter Adpt = new MySqlDataAdapter(Query, Con);
                try
                {
                    Adpt.Fill(Ds);
                }
                finally
                {
                    Adpt.Dispose();
                    Con.Dispose();
                }
            }
            #endregion
            #region PostgreeSQL
            else if (this.TipoBanco == TipoBanco.PostgreeSQL)
            {
                NpgsqlConnection Con = this.RetornaConexaoPostgreeSQL();
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(Query, Con);
                try
                {
                    Adpt.Fill(Ds);
                }
                finally
                {
                    Adpt.Dispose();
                    Con.Dispose();
                }
            }
            #endregion
            #region OleDB
            else if (this.TipoBanco == TipoBanco.OleDb)
            {
                OleDbConnection Con = this.RetornaConexaoOleDb();
                OleDbDataAdapter Adpt = new OleDbDataAdapter(Query, Con);
                try
                {
                    Adpt.Fill(Ds);
                }
                finally
                {
                    Adpt.Dispose();
                    Con.Dispose();
                }
            }
            #endregion

            return Ds;
        }
    }

    public DataSet RetornaDataSet(string Query, params Parametros[] Parametros)
    {
        List<Parametros> Pr = new List<Parametros>();
        for (int i = 0; i < Parametros.Length; i++)
        {
            Pr.Add(Parametros[i]);
        }
        return this.RetornaDataSet(Query, Pr);
    }

    public DataSet RetornaDataSet(string Query, List<Parametros> Parametros)
    {
        int i;
        DataSet DsRetorno = new DataSet();
        #region FireBird
        if (this.TipoBanco == TipoBanco.FireBird)
        {
            FbConnection Con = this.RetornaConexaoFireBird();
            FbCommand Cmd = new FbCommand(Query, Con);
            for (i = 0; i < Parametros.Count; i++)
            {
                switch (Parametros[i].Tipo)
                {
                    case TipoCampo.Varchar:
                        Cmd.Parameters.Add(Parametros[i].Nome, FbDbType.VarChar, Parametros[i].Tamanho);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.Int:
                        Cmd.Parameters.Add(Parametros[i].Nome, FbDbType.Integer);
                        Cmd.Parameters[i].Value = Convert.ToInt32(Parametros[i].Valor);
                        break;

                    case TipoCampo.Double:
                        Cmd.Parameters.Add(Parametros[i].Nome, FbDbType.Float);
                        Cmd.Parameters[i].Value = Convert.ToDouble(Parametros[i].Valor);
                        break;

                    case TipoCampo.Text:
                        Cmd.Parameters.Add(Parametros[i].Nome, FbDbType.Text);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.DateTime:
                        Cmd.Parameters.Add(Parametros[i].Nome, FbDbType.Date);
                        Cmd.Parameters[i].Value = Convert.ToDateTime(Parametros[i].Valor);
                        break;
                }
            }
            FbDataAdapter Adpt = new FbDataAdapter(Cmd);
            try
            {
                Adpt.Fill(DsRetorno);
            }
            finally
            {
                Con.Dispose();
                Cmd.Dispose();
                Adpt.Dispose();
            }
            return DsRetorno;
        }
        #endregion
        #region SqlServer
        if (this.TipoBanco == TipoBanco.SqlServer)
        {
            SqlConnection Con = this.RetornaConexaoSQLServer();
            SqlCommand Cmd = new SqlCommand(Query, Con);
            for (i = 0; i < Parametros.Count; i++)
            {
                switch (Parametros[i].Tipo)
                {
                    case TipoCampo.Varchar:
                        Cmd.Parameters.Add(Parametros[i].Nome, SqlDbType.VarChar, Parametros[i].Tamanho);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.Int:
                        Cmd.Parameters.Add(Parametros[i].Nome, SqlDbType.Int);
                        Cmd.Parameters[i].Value = Convert.ToInt32(Parametros[i].Valor);
                        break;

                    case TipoCampo.Double:
                        Cmd.Parameters.Add(Parametros[i].Nome, SqlDbType.Float);
                        Cmd.Parameters[i].Value = Convert.ToDouble(Parametros[i].Valor);
                        break;

                    case TipoCampo.Text:
                        Cmd.Parameters.Add(Parametros[i].Nome, SqlDbType.Text);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.DateTime:
                        Cmd.Parameters.Add(Parametros[i].Nome, SqlDbType.Date);
                        Cmd.Parameters[i].Value = Convert.ToDateTime(Parametros[i].Valor);
                        break;
                }
            }
            SqlDataAdapter Adpt = new SqlDataAdapter(Cmd);
            try
            {
                Adpt.Fill(DsRetorno);
            }
            finally
            {
                Con.Dispose();
                Cmd.Dispose();
                Adpt.Dispose();
            }
            return DsRetorno;
        }
        #endregion
        #region MySql
        if (this.TipoBanco == TipoBanco.MySql)
        {
            MySqlConnection Con = this.RetornaConexaoMySql();
            MySqlCommand Cmd = new MySqlCommand(Query, Con);
            for (i = 0; i < Parametros.Count; i++)
            {
                switch (Parametros[i].Tipo)
                {
                    case TipoCampo.Varchar:
                        Cmd.Parameters.Add(Parametros[i].Nome, MySqlDbType.VarChar, Parametros[i].Tamanho);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.Int:
                        Cmd.Parameters.Add(Parametros[i].Nome, MySqlDbType.Int32);
                        Cmd.Parameters[i].Value = Convert.ToInt32(Parametros[i].Valor);
                        break;

                    case TipoCampo.Double:
                        Cmd.Parameters.Add(Parametros[i].Nome, MySqlDbType.Float);
                        Cmd.Parameters[i].Value = Convert.ToDouble(Parametros[i].Valor);
                        break;

                    case TipoCampo.Text:
                        Cmd.Parameters.Add(Parametros[i].Nome, MySqlDbType.Text);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.DateTime:
                        Cmd.Parameters.Add(Parametros[i].Nome, MySqlDbType.Date);
                        Cmd.Parameters[i].Value = Convert.ToDateTime(Parametros[i].Valor);
                        break;
                }
            }
            MySqlDataAdapter Adpt = new MySqlDataAdapter(Cmd);
            try
            {
                Adpt.Fill(DsRetorno);
            }
            finally
            {
                Con.Dispose();
                Cmd.Dispose();
                Adpt.Dispose();
            }
            return DsRetorno;
        }
        #endregion
        #region PostgreeSQL
        if (this.TipoBanco == TipoBanco.PostgreeSQL)
        {
            NpgsqlConnection Con = this.RetornaConexaoPostgreeSQL();
            NpgsqlCommand Cmd = new NpgsqlCommand(Query, Con);
            for (i = 0; i < Parametros.Count; i++)
            {
                switch (Parametros[i].Tipo)
                {
                    case TipoCampo.Varchar:
                        Cmd.Parameters.Add(Parametros[i].Nome, NpgsqlDbType.Char, Parametros[i].Tamanho);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.Int:
                        Cmd.Parameters.Add(Parametros[i].Nome, NpgsqlDbType.Integer);
                        Cmd.Parameters[i].Value = Convert.ToInt32(Parametros[i].Valor);
                        break;

                    case TipoCampo.Double:
                        Cmd.Parameters.Add(Parametros[i].Nome, NpgsqlDbType.Double);
                        Cmd.Parameters[i].Value = Convert.ToDouble(Parametros[i].Valor);
                        break;

                    case TipoCampo.Text:
                        Cmd.Parameters.Add(Parametros[i].Nome, NpgsqlDbType.Text);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.DateTime:
                        Cmd.Parameters.Add(Parametros[i].Nome, NpgsqlDbType.Date);
                        Cmd.Parameters[i].Value = Convert.ToDateTime(Parametros[i].Valor);
                        break;
                }
            }
            NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(Cmd);
            try
            {
                Adpt.Fill(DsRetorno);
            }
            finally
            {
                Con.Dispose();
                Cmd.Dispose();
                Adpt.Dispose();
            }
            return DsRetorno;
        }
        #endregion
        #region OleDB
        if (this.TipoBanco == TipoBanco.OleDb)
        {
            OleDbConnection Con = this.RetornaConexaoOleDb();
            OleDbCommand Cmd = new OleDbCommand(Query, Con);
            for (i = 0; i < Parametros.Count; i++)
            {
                switch (Parametros[i].Tipo)
                {
                    case TipoCampo.Varchar:
                        Cmd.Parameters.Add(Parametros[i].Nome, OleDbType.Char, Parametros[i].Tamanho);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.Int:
                        Cmd.Parameters.Add(Parametros[i].Nome, OleDbType.Integer);
                        Cmd.Parameters[i].Value = Convert.ToInt32(Parametros[i].Valor);
                        break;

                    case TipoCampo.Double:
                        Cmd.Parameters.Add(Parametros[i].Nome, OleDbType.Double);
                        Cmd.Parameters[i].Value = Convert.ToDouble(Parametros[i].Valor);
                        break;

                    case TipoCampo.Text:
                        Cmd.Parameters.Add(Parametros[i].Nome, OleDbType.VarChar);
                        Cmd.Parameters[i].Value = Parametros[i].Valor.ToString();
                        break;

                    case TipoCampo.DateTime:
                        Cmd.Parameters.Add(Parametros[i].Nome, OleDbType.DBTimeStamp);
                        Cmd.Parameters[i].Value = Convert.ToDateTime(Parametros[i].Valor);
                        break;
                }
            }
            OleDbDataAdapter Adpt = new OleDbDataAdapter(Cmd);
            try
            {
                Adpt.Fill(DsRetorno);
            }
            finally
            {
                Con.Dispose();
                Cmd.Dispose();
                Adpt.Dispose();
            }
            return DsRetorno;
        }
        #endregion

        return DsRetorno;
    }

    public static DataSet RetornaDataSet(string Nome, string[] NomesColunas, string[] ValoresColunas)
    {
        int i;
        DataSet DsRetorno = new DataSet(Nome);
        DataTable TblTemp = new DataTable(Nome);
        for (i = 0; i < NomesColunas.Length; i++)
        {
            TblTemp.Columns.Add(new DataColumn(NomesColunas[i], Type.GetType("System.String")));
        }
        DataRow Linha = TblTemp.NewRow();
        for (i = 0; i < Linha.Table.Columns.Count; i++)
        {
            Linha[i] = ValoresColunas[i];
        }
        TblTemp.Rows.Add(Linha);
        DsRetorno.Tables.Add(TblTemp);
        return DsRetorno;
    }

    public DataTable RetornaDataTable(string Query)
    {
        return this.RetornaDataSet(Query).Tables[0];
    }

    public static Parametros RetornaParametro(string Nome, TipoCampo Tipo, object Valor)
    {
        return RetornaParametro(Nome, Tipo, Valor, 100);
    }

    public static Parametros RetornaParametro(string Nome, TipoCampo Tipo, object Valor, int Tamanho)
    {
        return new Parametros { Nome = Nome, Tipo = Tipo, Valor = Valor, Tamanho = Tamanho };
    }

    public object RetornaValor(string Query)
    {
        object Retorno = string.Empty;
        switch (this.TipoBanco)
        {
            #region Firebird
            case TipoBanco.FireBird:
                {
                    FbConnection Con = this.RetornaConexaoFireBird();
                    FbCommand Cmd = new FbCommand(Query, Con);
                    try
                    {
                        Con.Open();
                        Retorno = Cmd.ExecuteScalar();
                    }
                    finally
                    {
                        Con.Close();
                        Con.Dispose();
                        Cmd.Dispose();
                    }
                    return Retorno;
                }
            #endregion
            #region SqlServer
            case TipoBanco.SqlServer:
                {
                    SqlConnection Con1 = this.RetornaConexaoSQLServer();
                    SqlCommand Cmd1 = new SqlCommand(Query, Con1);
                    try
                    {
                        Con1.Open();
                        Retorno = Cmd1.ExecuteScalar();
                    }
                    finally
                    {
                        Con1.Close();
                        Con1.Dispose();
                        Cmd1.Dispose();
                    }
                    return Retorno;
                }
            #endregion
            #region MySql
            case TipoBanco.MySql:
                {
                    MySqlConnection Con = this.RetornaConexaoMySql();
                    MySqlCommand Cmd = new MySqlCommand(Query, Con);
                    try
                    {
                        Con.Open();
                        Retorno = Cmd.ExecuteScalar();
                    }
                    finally
                    {
                        Con.Close();
                        Con.Dispose();
                        Cmd.Dispose();
                    }
                    return Retorno;
                }
            #endregion
            #region PostgreeSql
            case TipoBanco.PostgreeSQL:
                {
                    NpgsqlConnection Con = this.RetornaConexaoPostgreeSQL();
                    NpgsqlCommand Cmd = new NpgsqlCommand(Query, Con);
                    try
                    {
                        Con.Open();
                        Retorno = Cmd.ExecuteScalar();
                    }
                    finally
                    {
                        Con.Close();
                        Con.Dispose();
                        Cmd.Dispose();
                    }
                    return Retorno;
                }
            #endregion
            #region PostgreeSql
            case TipoBanco.OleDb:
                {
                    OleDbConnection Con = this.RetornaConexaoOleDb();
                    OleDbCommand Cmd = new OleDbCommand(Query, Con);
                    try
                    {
                        Con.Open();
                        Retorno = Cmd.ExecuteScalar();
                    }
                    finally
                    {
                        Con.Close();
                        Con.Dispose();
                        Cmd.Dispose();
                    }
                    return Retorno;
                }
            #endregion
        }
        return Retorno;
    }

    public bool TestaConexao()
    {
        string Temp = string.Empty;
        return TestaConexao(this.StrConexa, this.TipoBanco, ref Temp);
    }

    public bool TestaConexao(ref string Retorno)
    {
        return TestaConexao(this.StrConexa, this.TipoBanco, ref Retorno);
    }

    public static bool TestaConexao(string StrConexao, TipoBanco TipoBD)
    {
        string Temp = string.Empty;
        return TestaConexao(StrConexao, TipoBD, ref Temp);
    }

    public static bool TestaConexao(string StrConexao, TipoBanco TipoBD, ref string Resultado)
    {
        Exception ex;
        bool Retorno = false;
        Resultado = "Conexão realizada com sucesso.";
        #region FireBird
        if (TipoBD == TipoBanco.FireBird)
        {
            using (FbConnection Con = new FbConnection(StrConexao))
            {
                try
                {
                    Con.Open();
                    return true;
                }
                catch (Exception exception1)
                {
                    ex = exception1;
                    Resultado = ex.Message;
                    return false;
                }
            }
        }
        #endregion
        #region SqlServer
        else if (TipoBD == TipoBanco.SqlServer)
        {
            using (SqlConnection Con = new SqlConnection(StrConexao))
            {
                try
                {
                    Con.Open();
                    return true;
                }
                catch (Exception exception2)
                {
                    ex = exception2;
                    Resultado = ex.Message;
                    return false;
                }
                finally
                {
                    Con.Dispose();
                }
            }
        }
        #endregion
        #region MySql
        if (TipoBD == TipoBanco.MySql)
        {
            using (MySqlConnection Con = new MySqlConnection(StrConexao))
            {
                try
                {
                    Con.Open();
                    return true;
                }
                catch (Exception exception3)
                {
                    ex = exception3;
                    Resultado = ex.Message;
                    return false;
                }
            }
        }
        #endregion
        #region PostgreeSQL
        if (TipoBD == TipoBanco.PostgreeSQL)
        {
            using (NpgsqlConnection Con = new NpgsqlConnection(StrConexao))
            {
                try
                {
                    Con.Open();
                    return true;
                }
                catch (Exception exception4)
                {
                    ex = exception4;
                    Resultado = ex.Message;
                    return false;
                }
            }
        }
        #endregion
        #region OleDb
        if (TipoBD == TipoBanco.OleDb)
        {
            using (OleDbConnection Con = new OleDbConnection(StrConexao))
            {
                try
                {
                    Con.Open();
                    return true;
                }
                catch (Exception exception5)
                {
                    ex = exception5;
                    Resultado = ex.Message;
                    return false;
                }
            }
        }
        #endregion
        return Retorno;
    }
}

