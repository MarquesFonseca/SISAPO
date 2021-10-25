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
    public partial class FormularioCadastroUsuario : Form
    {
        public FormularioCadastroUsuario()
        {
            InitializeComponent();
            this.tabelaUsuarioTableAdapter.Connection.ConnectionString = ClassesDiversas.Configuracoes.strConexao;
        }

        private void FormularioCadastroUsuario_Load(object sender, EventArgs e)
        {
            AtivarDesativarControles(false);

            tabelaUsuarioTableAdapter.Fill(this.dataSetTabelaUsuario.TabelaUsuario);
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            if (tabelaUsuarioBindingSource.Current == null)
            {
                AtivarDesativarControles(false);
                return;
            }

            if (string.IsNullOrWhiteSpace(nomeCompletoUsuarioTextBox.Text))
            {
                Mensagens.Erro("Informe o Nome Completo.");
                return;
            }
            if (string.IsNullOrWhiteSpace(cPFUsuarioTextBox.Text))
            {
                Mensagens.Erro("Informe o CPF.");
                return;
            }
            
            if (string.IsNullOrWhiteSpace(loginUsuarioTextBox.Text))
            {
                Mensagens.Erro("Informe o Login.");
                return;
            }
            if (string.IsNullOrWhiteSpace(senhaUsuarioTextBox.Text))
            {
                Mensagens.Erro("Informe a Senha.");
                return;
            }

            if (((DataRowView)tabelaUsuarioBindingSource.Current).Row["Codigo"].ToInt() == 1)
            {
                Mensagens.Informa("Não é possível remover ou alterar o usuário atual", MessageBoxIcon.Error);
                this.tabelaUsuarioBindingSource.CancelEdit();
                AtivarDesativarControles(false);
                return;
            }


            ((DataRowView)tabelaUsuarioBindingSource.Current).Row["DataAlteracao"] = DateTime.Now;
            ((DataRowView)tabelaUsuarioBindingSource.Current).Row["UsuarioCriacaoCadastro"] = Configuracoes.UsuarioLogado;
            ((DataRowView)tabelaUsuarioBindingSource.Current).Row["MatriculaUsuario"] = matriculaUsuarioTextBox.Text;
            ((DataRowView)tabelaUsuarioBindingSource.Current).Row["LoginAtivo"] = loginAtivoCheckBox.Checked;


            this.Validate();
            this.tabelaUsuarioBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSetTabelaUsuario);

            tabelaUsuarioTableAdapter.Fill(this.dataSetTabelaUsuario.TabelaUsuario);

            AtivarDesativarControles(false);

        }

        private void AtivarDesativarControles(bool AtivarControle)
        {
            nomeCompletoUsuarioTextBox.ReadOnly = !AtivarControle;
            cPFUsuarioTextBox.ReadOnly = !AtivarControle;
            matriculaUsuarioTextBox.ReadOnly = !AtivarControle;
            emailCorporativoUsuarioTextBox.ReadOnly = !AtivarControle;
            emailAlternativoUsuarioTextBox.ReadOnly = !AtivarControle;
            loginUsuarioTextBox.ReadOnly = !AtivarControle;
            senhaUsuarioTextBox.ReadOnly = !AtivarControle;
            loginAtivoCheckBox.Enabled = AtivarControle;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (Mensagens.Pergunta("Realmente deseja remover este usuário?", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                tabelaUsuarioTableAdapter.Fill(this.dataSetTabelaUsuario.TabelaUsuario);
                return;
            }
            else
            {
                this.Validate();
                this.tabelaUsuarioBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.dataSetTabelaUsuario);

                tabelaUsuarioTableAdapter.Fill(this.dataSetTabelaUsuario.TabelaUsuario);
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            AtivarDesativarControles(true);

            loginAtivoCheckBox.CheckState = CheckState.Checked;

            nomeCompletoUsuarioTextBox.Focus();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (tabelaUsuarioBindingSource.Current == null)
            {
                AtivarDesativarControles(false);
                return;
            }
            AtivarDesativarControles(true);

            nomeCompletoUsuarioTextBox.Focus();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            tabelaUsuarioTableAdapter.Fill(this.dataSetTabelaUsuario.TabelaUsuario);
        }

        private void tabelaUsuarioBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            loginAtivoCheckBox.Checked = (bool)((DataRowView)tabelaUsuarioBindingSource.Current).Row["LoginAtivo"];

            bindingNavigatorCountItem.Text = string.Format("de {0}", tabelaUsuarioBindingSource.Count);
        }

        private void FormularioCadastroUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
