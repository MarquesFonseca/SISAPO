using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SISAPO
{

    [Serializable]
    public class TabelaUsuario
    {

        #region Variables
        protected int m_Codigo;
        protected string m_NomeCompletoUsuario;
        protected string m_CPFUsuario;
        protected string m_MatriculaUsuario;
        protected string m_EmailCorporativoUsuario;
        protected string m_EmailAlternativoUsuario;
        protected string m_UsuarioCriacaoCadastro;
        protected string m_LoginUsuario;
        protected string m_SenhaUsuario;
        protected bool m_LoginAtivo;
        protected DateTime m_DataAlteracao;

        #endregion

        #region Properties

        public int Codigo
        {
            get { return (m_Codigo); }
            set { m_Codigo = value; }
        }
        public string NomeCompletoUsuario
        {
            get { return (m_NomeCompletoUsuario); }
            set { m_NomeCompletoUsuario = value; }
        }
        public string CPFUsuario
        {
            get { return (m_CPFUsuario); }
            set { m_CPFUsuario = value; }
        }
        public string MatriculaUsuario
        {
            get { return (m_MatriculaUsuario); }
            set { m_MatriculaUsuario = value; }
        }
        public string EmailCorporativoUsuario
        {
            get { return (m_EmailCorporativoUsuario); }
            set { m_EmailCorporativoUsuario = value; }
        }
        public string EmailAlternativoUsuario
        {
            get { return (m_EmailAlternativoUsuario); }
            set { m_EmailAlternativoUsuario = value; }
        }
        public string UsuarioCriacaoCadastro
        {
            get { return (m_UsuarioCriacaoCadastro); }
            set { m_UsuarioCriacaoCadastro = value; }
        }
        public string LoginUsuario
        {
            get { return (m_LoginUsuario); }
            set { m_LoginUsuario = value; }
        }
        public string SenhaUsuario
        {
            get { return (m_SenhaUsuario); }
            set { m_SenhaUsuario = value; }
        }
        public bool LoginAtivo
        {
            get { return (m_LoginAtivo); }
            set { m_LoginAtivo = value; }
        }
        public DateTime DataAlteracao
        {
            get { return (m_DataAlteracao); }
            set { m_DataAlteracao = value; }
        }
        #endregion

        public TabelaUsuario()
        {
        }

        public TabelaUsuario(IDataRecord record)
        {
            this.Fill(record);
        }

        internal void Fill(IDataRecord record)
        {
            #region Read Data
            m_Codigo = (int)record["Codigo"];

            if (record["NomeCompletoUsuario"] != DBNull.Value)
            {
                m_NomeCompletoUsuario = (string)record["NomeCompletoUsuario"];
            }
            if (record["CPFUsuario"] != DBNull.Value)
            {
                m_CPFUsuario = (string)record["CPFUsuario"];
            }
            if (record["MatriculaUsuario"] != DBNull.Value)
            {
                m_MatriculaUsuario = (string)record["MatriculaUsuario"];
            }
            if (record["EmailCorporativoUsuario"] != DBNull.Value)
            {
                m_EmailCorporativoUsuario = (string)record["EmailCorporativoUsuario"];
            }
            if (record["EmailAlternativoUsuario"] != DBNull.Value)
            {
                m_EmailAlternativoUsuario = (string)record["EmailAlternativoUsuario"];
            }
            if (record["UsuarioCriacaoCadastro"] != DBNull.Value)
            {
                m_UsuarioCriacaoCadastro = (string)record["UsuarioCriacaoCadastro"];
            }
            if (record["LoginUsuario"] != DBNull.Value)
            {
                m_LoginUsuario = (string)record["LoginUsuario"];
            }
            if (record["SenhaUsuario"] != DBNull.Value)
            {
                m_SenhaUsuario = (string)record["SenhaUsuario"];
            }
            m_LoginAtivo = (bool)record["LoginAtivo"];

            if (record["DataAlteracao"] != DBNull.Value)
            {
                m_DataAlteracao = (DateTime)record["DataAlteracao"];
            }
            #endregion
        }

    }

    [Serializable]
    public class TabelaUsuarioCollection : CollectionBase
    {

        public TabelaUsuarioCollection()
        {

        }

        public TabelaUsuario this[int index]
        {
            get
            {
                return ((TabelaUsuario)List[index]);
            }
        }

        public void Sort(IComparer comparer)
        {
            InnerList.Sort(comparer);
        }

        public int Add(TabelaUsuario p)
        {
            return (List.Add(p));
        }

        public void Remove(TabelaUsuario p)
        {
            List.Remove(p);
        }
    }

}
