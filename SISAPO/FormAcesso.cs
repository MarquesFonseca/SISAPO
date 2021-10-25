using SISAPO.ClassesDiversas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SISAPO
{
    public partial class FormAcesso : Form
    {
        public bool Autenticado = false;

        public string CodigoUsuario = string.Empty;
        public string NomeCompletoUsuario = string.Empty;
        public string CPFUsuarioLogado = string.Empty;
        public string MatriculaUsuario = string.Empty;
        public string EmailCorporativoUsuario = string.Empty;
        public string EmailAlternativoUsuario = string.Empty;
        public string UsuarioCriacaoCadastro = string.Empty;
        public string UsuarioLogado = string.Empty;
        public string SenhaUsuario = string.Empty;
        public bool LoginAtivo = false;
        public string DataAlteracao = string.Empty;

        public FormAcesso()
        {
            InitializeComponent();
        }

        private void FormAcesso_Load(object sender, EventArgs e)
        {
            this.Autenticado = false;
            TxtUsuario.Focus();

#if DEBUG
            TxtUsuario.Text = "marquesfonseca";
            TxtSenha.Text = "9342456";
#endif
        }

        private void TxtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                TxtSenha.Focus();
                TxtSenha.SelectAll();
            }
        }

        private void TxtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnAcesso_Click(sender, e);
            }
        }

        private void BtnAcesso_Click(object sender, EventArgs e)
        {
            Autenticado = false;
            if (string.IsNullOrEmpty(TxtUsuario.Text))
            {
                Mensagens.Informa("Informe o Usuário.");
                TxtUsuario.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TxtSenha.Text))
            {
                Mensagens.Informa("Informe a Senha.");
                TxtSenha.Focus();
                return;
            }

            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                DataTable RetornoUsuario = dao.RetornaDataTable("SELECT top 1 * FROM TabelaUsuario where LoginUsuario = '" + TxtUsuario.Text + "' and SenhaUsuario = '" + TxtSenha.Text + "' and LoginAtivo = true");
                if (RetornoUsuario.Rows.Count == 0)
                {
                    Mensagens.Erro("Usuário ou Senha incorreto!");
                    TxtUsuario.Focus();
                    return;
                }
                if (RetornoUsuario.Rows.Count == 1)
                {
                    CodigoUsuario = RetornoUsuario.Rows[0]["Codigo"].ToString();
                    NomeCompletoUsuario = RetornoUsuario.Rows[0]["NomeCompletoUsuario"].ToString();
                    CPFUsuarioLogado = RetornoUsuario.Rows[0]["CPFUsuario"].ToString();
                    MatriculaUsuario = RetornoUsuario.Rows[0]["MatriculaUsuario"].ToString();
                    EmailCorporativoUsuario = RetornoUsuario.Rows[0]["EmailCorporativoUsuario"].ToString();
                    EmailAlternativoUsuario = RetornoUsuario.Rows[0]["EmailAlternativoUsuario"].ToString();
                    UsuarioCriacaoCadastro = RetornoUsuario.Rows[0]["UsuarioCriacaoCadastro"].ToString();
                    UsuarioLogado = RetornoUsuario.Rows[0]["LoginUsuario"].ToString();
                    SenhaUsuario = RetornoUsuario.Rows[0]["SenhaUsuario"].ToString();
                    LoginAtivo = (bool)RetornoUsuario.Rows[0]["LoginAtivo"];
                    DataAlteracao = RetornoUsuario.Rows[0]["DataAlteracao"].ToString();

                    Autenticado = true;
                    this.Close();
                }
            }

            //if (TxtUsuario.Text.ToUpper() == "marquesfonseca".ToUpper())
            //{
            //    if (TxtSenha.Text.ToUpper() == "9342456".ToUpper())
            //    {
            //        Autenticado = true;
            //        NomeUsuarioLogado = "Marques Silva Fonseca";
            //        UsuarioLogado = "marquesfonseca";
            //        CPFUsuarioLogado = "834.862.672-72";
            //        this.Close();
            //    }
            //    else
            //    {
            //        Mensagens.Erro("Senha incorreta!");
            //        TxtSenha.Focus();
            //        return;
            //    }
            //}
            //else if (TxtUsuario.Text.ToUpper() == "08327991140".ToUpper()) //Ariely A. Seles
            //{
            //    if (TxtSenha.Text.ToUpper() == "13112002".ToUpper())
            //    {
            //        Autenticado = true;
            //        NomeUsuarioLogado = "Ariely Almeida Seles";
            //        UsuarioLogado = "08327991140";
            //        CPFUsuarioLogado = "083.279.911-40";
            //        this.Close();
            //    }
            //    else
            //    {
            //        Mensagens.Erro("Senha incorreta!");
            //        TxtSenha.Focus();
            //        return;
            //    }
            //}
            //else if (TxtUsuario.Text.ToUpper() == "96793376349".ToUpper()) //Ester de Oliveira Souza
            //{
            //    if (TxtSenha.Text.ToUpper() == "96793376349".ToUpper())
            //    {
            //        Autenticado = true;
            //        NomeUsuarioLogado = "Ester de Oliveira Souza";
            //        UsuarioLogado = "96793376349";
            //        CPFUsuarioLogado = "967.933.763-49";
            //        this.Close();
            //    }
            //    else
            //    {
            //        Mensagens.Erro("Senha incorreta!");
            //        TxtSenha.Focus();
            //        return;
            //    }
            //}
            //else
            //{
            //    Mensagens.Erro("Usuário ou Senha incorreto!");
            //    TxtUsuario.Focus();
            //    return;
            //}


        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAcesso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }


    }
}
