using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SISAPO.ClassesDiversas;
using System.IO;

namespace SISAPO
{
    public partial class FormularioOpcoes : Form
    {
        public FormularioOpcoes()
        {
            InitializeComponent();
        }

        private void FormularioOpcoes_Load(object sender, EventArgs e)
        {
            this.tabelaConfiguracoesSistemaTableAdapter.Connection.ConnectionString = ClassesDiversas.Configuracoes.strConexao;
            this.tabelaConfiguracoesSistemaTableAdapter.Fill(this.dataSetConfiguracoes.TabelaConfiguracoesSistema);
            comboBoxSupEst.Text = ((DataRowView)bindingSourceTabelaConfiguracoesSistema.Current).Row["SuperintendenciaEstadual"].ToString().Replace("SE/", "");
            checkBoxExibirObjetosEmCaixaPostal.Checked = FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirCaixaPostalPesquisa_toolStripMenuItem.Checked;
            checkBoxExibirItensJaEntregues.Checked = FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirItensJaEntreguesToolStripMenuItem.Checked;
            if (Configuracoes.TipoAmbiente == TipoAmbiente.Desenvolvimento) BtnMarcarTodosAtualizados.Visible = true;
            else BtnMarcarTodosAtualizados.Visible = false;

            buscaEnderecoSRO();
        }

        private void buscaEnderecoSRO()
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                DataRow RetornoEnderecosSRO = dao.RetornaDataRow("SELECT EnderecoSRO, EnderecoSROPorObjeto FROM TabelaConfiguracoesSistema");

                if (RetornoEnderecosSRO["EnderecoSRO"].ToString().Contains("websro2"))
                {
                    radioButtonEnderecoSROWebsro2.Checked = true;
                }
                if (RetornoEnderecosSRO["EnderecoSRO"].ToString().Contains("app"))
                {
                    radioButtonEnderecoApp.Checked = true;
                }
                TxtEnderecoSRO.Text = RetornoEnderecosSRO["EnderecoSRO"].ToString();

                if (RetornoEnderecosSRO["EnderecoSROPorObjeto"].ToString().Contains("websro2"))
                {
                    radioButtonEnderecoSROWebsro2oCampo2.Checked = true;
                }
                if (RetornoEnderecosSRO["EnderecoSROPorObjeto"].ToString().Contains("app"))
                {
                    radioButtonEnderecoAppCampo2.Checked = true;
                }
                TxtEnderecoSROEspecificoObjeto.Text = RetornoEnderecosSRO["EnderecoSROPorObjeto"].ToString() + "QB378038055BR";
            }
        }

        private void BtnRequererVerificacaoDeObjetosJaEntregues_Click(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem_Click(sender, e);
        }

        private void checkBoxExibirObjetosEmCaixaPostal_CheckedChanged(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirCaixaPostalPesquisa_toolStripMenuItem.Checked = checkBoxExibirObjetosEmCaixaPostal.Checked;
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirCaixaPostalPesquisa_toolStripMenuItem_Click(sender, e);
        }

        private void checkBoxExibirItensJaEntregues_CheckedChanged(object sender, EventArgs e)
        {
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirItensJaEntreguesToolStripMenuItem.Checked = checkBoxExibirItensJaEntregues.Checked;
            FormularioPrincipal.RetornaComponentesFormularioPrincipal().ExibirItensJaEntreguesToolStripMenuItem_Click(sender, e);
        }

        private void BtnMarcarTodosAtualizados_Click(object sender, EventArgs e)
        {
            DialogResult pergunta = Mensagens.Pergunta("Deseja realmente marcar/desmarcar objetos como atualizado?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (pergunta == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            if (pergunta == System.Windows.Forms.DialogResult.Yes)
            {
                //grava no banco de dados
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Atualizado = @Atualizado", new List<Parametros>(){
                                            new Parametros("@Atualizado", TipoCampo.Boolean, true)});
                }
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().BuscaNovoStatusQuantidadeNaoAtualizados();
            }
        }

        private void FormularioOpcoes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            if (e.KeyCode == Keys.F9)
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().sRORastreamentoUnificadoToolStripMenuItem_Click(sender, e);
            }
            if (e.KeyCode == Keys.F12)
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().visualizarListaDeObjetosToolStripMenuItem_Click(sender, e);
            }
        }

        private void BtnAtualizarConfiguracoesAgencia_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNomeAgencia.Text))
                {
                    Mensagens.Erro("O campo \"Nome da agência\" se encontra vazio.");
                    return;
                }
                if (string.IsNullOrEmpty(comboBoxSupEst.Text))
                {
                    Mensagens.Erro("O campo \"Sup. Est.\" se encontra vazio.");
                    return;
                }
                if (string.IsNullOrEmpty(txtCepUnidade.Text))
                {
                    Mensagens.Erro("O campo \"CEP Unidade\" se encontra vazio.");
                    return;
                }
                if (string.IsNullOrEmpty(txtEnderecoAgencia.Text))
                {
                    Mensagens.Erro("O campo \"Endereço da agência\" se encontra vazio.");
                    return;
                }
                if (string.IsNullOrEmpty(txtCidadeAgenciaLocal.Text))
                {
                    Mensagens.Erro("O campo \"Cidade\" se encontra vazio.");
                    return;
                }
                if (string.IsNullOrEmpty(comboBoxUFAgenciaLocal.Text))
                {
                    Mensagens.Erro("O campo \"Estado\" se encontra vazio.");
                    return;
                }
                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }
                    dao.ExecutaSQL(string.Format("UPDATE TabelaConfiguracoesSistema SET NomeAgenciaLocal = @NomeAgenciaLocal, EnderecoAgenciaLocal = @EnderecoAgenciaLocal, SuperintendenciaEstadual = @SuperintendenciaEstadual, CepUnidade = @CepUnidade, CidadeAgenciaLocal = @CidadeAgenciaLocal, UFAgenciaLocal = @UFAgenciaLocal, TelefoneAgenciaLocal = @TelefoneAgenciaLocal, HorarioFuncionamentoAgenciaLocal = @HorarioFuncionamentoAgenciaLocal Where Codigo = @Codigo"), new List<Parametros>(){
                                            new Parametros("@NomeAgenciaLocal", TipoCampo.Text, txtNomeAgencia.Text),
                                            new Parametros("@EnderecoAgenciaLocal", TipoCampo.Text, txtEnderecoAgencia.Text),
                                            new Parametros("@SuperintendenciaEstadual", TipoCampo.Text, string.Format("{0}", comboBoxSupEst.Text)),
                                            new Parametros("@CepUnidade", TipoCampo.Text, txtCepUnidade.Text),

                                            new Parametros("@CidadeAgenciaLocal", TipoCampo.Text, txtCidadeAgenciaLocal.Text),
                                            new Parametros("@UFAgenciaLocal", TipoCampo.Text, comboBoxUFAgenciaLocal.Text),
                                            new Parametros("@TelefoneAgenciaLocal", TipoCampo.Text, txtTelefoneAgenciaLocal.Text),
                                            new Parametros("@HorarioFuncionamentoAgenciaLocal", TipoCampo.Text, txtHorarioFuncionamentoAgenciaLocal.Text),

                                            new Parametros("@Codigo", TipoCampo.Int, 2)});

                }
                Configuracoes.DadosAgencia = Configuracoes.RetornaDadosAgencia();
                Mensagens.Informa("Gravado com sucesso!", MessageBoxIcon.Information, MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
            }
        }

        private void BtnAtualizarEnderecoSRO_Click(object sender, EventArgs e)
        {
            try
            {
                string EnderecoSRO = TxtEnderecoSRO.Text;
                if (string.IsNullOrEmpty(EnderecoSRO))
                {
                    Mensagens.Informa("Para atualizar é necessário informar um endereço válido.");
                    return;
                }

                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                    //verifica se tem BR e se é no final 
                    if (EnderecoSRO.Contains("BR") && (EnderecoSRO.IndexOf("BR") == EnderecoSRO.Length - 2))
                    {
                        EnderecoSRO = EnderecoSRO.Substring(0, EnderecoSRO.Length - 13);
                    }

                    if (!string.IsNullOrEmpty(EnderecoSRO))
                    {
                        List<Parametros> pr = new List<Parametros>() {
                            new Parametros() { Nome = "@EnderecoSRO", Tipo = TipoCampo.Text, Valor = EnderecoSRO }
                        };
                        dao.ExecutaSQL("UPDATE TabelaConfiguracoesSistema SET EnderecoSRO = @EnderecoSRO", pr);
                        Configuracoes.EnderecosSRO = Configuracoes.RetornaEnderecosSRO();
                        Mensagens.Informa("Atualizado com sucesso!");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensagens.Erro("Ocorreu um erro ao tentar atualizar. \n" + ex.Message);
            }
        }

        private void BtnAtualizarEnderecoSROObjetoEspecifico_Click(object sender, EventArgs e)
        {
            try
            {
                string EnderecoSROPorObjeto = TxtEnderecoSROEspecificoObjeto.Text;
                if (string.IsNullOrEmpty(EnderecoSROPorObjeto))
                {
                    Mensagens.Informa("Para atualizar é necessário informar um endereço válido.");
                    return;
                }

                using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                    //verifica se tem BR e se é no final 
                    if (EnderecoSROPorObjeto.Contains("BR") && (EnderecoSROPorObjeto.IndexOf("BR") == EnderecoSROPorObjeto.Length - 2))
                    {
                        EnderecoSROPorObjeto = EnderecoSROPorObjeto.Substring(0, EnderecoSROPorObjeto.Length - 13);
                    }

                    if (!string.IsNullOrEmpty(EnderecoSROPorObjeto))
                    {
                        List<Parametros> pr = new List<Parametros>() {
                            new Parametros() { Nome = "@EnderecoSROPorObjeto", Tipo = TipoCampo.Text, Valor = EnderecoSROPorObjeto }
                        };
                        dao.ExecutaSQL("UPDATE TabelaConfiguracoesSistema SET EnderecoSROPorObjeto = @EnderecoSROPorObjeto", pr);
                        Configuracoes.EnderecosSRO = Configuracoes.RetornaEnderecosSRO();
                        Mensagens.Informa("Atualizado com sucesso!");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensagens.Erro("Ocorreu um erro ao tentar atualizar. \n" + ex.Message);
            }

        }

        private void radioButtonEnderecoSROWebsro2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEnderecoSROWebsro2.Checked)
            {
                TxtEnderecoSRO.Text = "http://websro2.correiosnet.int/rastreamento/sro";
                label10.Text = "Exemplo: http://websro2.correiosnet.int/rastreamento/sro";
            }
        }

        private void radioButtonEnderecoApp_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEnderecoApp.Checked)
            {
                TxtEnderecoSRO.Text = "http://app.correiosnet.int/rastreamento/sro";
                label10.Text = "Exemplo: http://app.correiosnet.int/rastreamento/sro";
            }
        }

        private void radioButtonEnderecoSROWebsro2oCampo2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEnderecoSROWebsro2oCampo2.Checked)
            {
                TxtEnderecoSROEspecificoObjeto.Text = "http://websro2.correiosnet.int/rastreamento/sro?opcao=PESQUISA&objetos=QB378038055BR";
                label11.Text = "Exemplo: http://websro2.correiosnet.int/rastreamento/sro?opcao=PESQUISA&objetos=QB378038055BR";
            }
        }

        private void radioButtonEnderecoAppCampo2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEnderecoAppCampo2.Checked)
            {
                TxtEnderecoSROEspecificoObjeto.Text = "http://app.correiosnet.int/rastreamento/sro?opcao=PESQUISA&objetos=QB378038055BR";
                label11.Text = "Exemplo: http://app.correiosnet.int/rastreamento/sro?opcao=PESQUISA&objetos=QB378038055BR";
            }
        }

        private void BtnTornarBancoVazio_Click(object sender, EventArgs e)
        {
            if (Mensagens.Pergunta("Realmente quer limpar banco e torná-lo um Banco vazio?") == DialogResult.Yes)
            {
                using (FormAcesso acesso = new FormAcesso())
                {
                    acesso.ShowDialog();
                    if (!acesso.Autenticado) return;
                    if (acesso.Autenticado)
                    {
                        Configuracoes.LimpaBancoTornaBancoVazio();
                        Mensagens2.MsgSucesso();
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void BtnBuscarEnderecoParaBackup_Click(object sender, EventArgs e)
        {
            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string nomeEnderecoArquivo = string.Format("Backup_cadastro_{0}.mdb", DateTime.Now).Replace("/", "").Replace(":", "");

            Arquivos.CriarDiretorio(curDir + @"\Backup");

            folderBrowserDialog1.Description = "Selecione uma pasta para realizar o Backup";
            //folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog1.SelectedPath = curDir + @"\Backup";
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                label13.Text = "Endereço de backup selecionado";
                labelResultadoFolderBackup.Text = string.Format(@"{0}\{1}", folderBrowserDialog1.SelectedPath, nomeEnderecoArquivo);
            }
            else
            {
                label13.Text = "Selecione o endereço desejado para backup";
                labelResultadoFolderBackup.Text = "";
            }
        }

        private void BtnSalvarBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(labelResultadoFolderBackup.Text))
                return;
            else
            {
                string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
                string arquivo = "cadastro.mdb";
                string nomeEnderecoArquivo = string.Format(@"{0}\{1}", curDir, arquivo);
                File.Copy(nomeEnderecoArquivo, labelResultadoFolderBackup.Text, true);
                Mensagens2.MsgSucesso();
            }
        }

        private void BtnRestaurarBackup_Click(object sender, EventArgs e)
        {
            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Selecione um arquivo para backup";
            openFileDialog1.Filter = "Microsoft Access File | *.mdb"; //"Microsoft Access File|*.mdb"
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = curDir + @"\Backup";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Multiselect = false;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;

                try
                {
                    string arquivo = "cadastro.mdb";
                    string nomeEnderecoArquivo = string.Format(@"{0}\{1}", curDir, arquivo);
                    File.Copy(file, nomeEnderecoArquivo, true);
                    Mensagens2.MsgSucesso();
                }
                catch (IOException) { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mensagens.Informa(string.Format("UserName: {0} \nUserDomainName: {1} \nHostName: {2}", Environment.UserName, Environment.UserDomainName, System.Net.Dns.GetHostEntry(Environment.MachineName).HostName));
        }

        private void BtnBuscarDadosAgenciaCodigoInformado_Click(object sender, EventArgs e)
        {
            string CodigoRetornado = string.Empty;
            using (FormularioInformarCodigo formularioInformarCodigo = new FormularioInformarCodigo())
            {
                formularioInformarCodigo.ShowDialog();
                if (formularioInformarCodigo.ClicouCancelar) return;
                if (formularioInformarCodigo.ClicouConfirmar)
                {
                    CodigoRetornado = formularioInformarCodigo.CodigoObjetoInformado;
                }
            }

            if (string.IsNullOrWhiteSpace(CodigoRetornado)) return;

            FormularioRetornaDadosInicialAgenciaBancoVazio formularioRetornaDadosInicialAgenciaBancoVazio = new FormularioRetornaDadosInicialAgenciaBancoVazio(CodigoRetornado);
            formularioRetornaDadosInicialAgenciaBancoVazio.ShowDialog();
            string CidadeAgenciaLinha = formularioRetornaDadosInicialAgenciaBancoVazio.CidadeAgenciaLinha;
            string UFAgenciaLinha = formularioRetornaDadosInicialAgenciaBancoVazio.UFAgenciaLinha;
            string NomeAgenciaLinha = formularioRetornaDadosInicialAgenciaBancoVazio.NomeAgenciaLinha;
            string EnderecoAgenciaLinha = formularioRetornaDadosInicialAgenciaBancoVazio.EnderecoAgenciaLinha;


            txtNomeAgencia.Text = NomeAgenciaLinha;
            comboBoxSupEst.Text = UFAgenciaLinha;
            txtEnderecoAgencia.Text = EnderecoAgenciaLinha;
            txtCidadeAgenciaLocal.Text = CidadeAgenciaLinha;
            comboBoxUFAgenciaLocal.Text = UFAgenciaLinha;

            Configuracoes.DadosAgencia = Configuracoes.RetornaDadosAgencia();
        }
    }
}
