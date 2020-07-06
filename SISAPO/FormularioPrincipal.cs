using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SISAPO.ClassesDiversas;

namespace SISAPO
{
    public partial class FormularioPrincipal : Form
    {
        public static string dataSource = string.Empty;

        public FormularioPrincipal()
        {
            InitializeComponent();
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1.Text = string.Format("Imprimir lista de entrega lançados hoje [{0}]", DateTime.Now.GetDateTimeFormats()[14]);
            dataSource = new System.Data.OleDb.OleDbConnection(ClassesDiversas.Configuracoes.strConexao).DataSource.ToString();

            //min ---- seg
            //1min --  60s
            //10min - Xseg
            //Xseg = 60 x 10 = 600 segundos
            //
            //então 
            //1000 = 1 segundo
            //Xmil = 600segundos
            //Xmil = 600 x 1000 = 600.000

            timerAtualizacaoNovosRegistros.Interval = 600000; //10 mimutos
            //timerAtualizacaoNovosRegistros.Interval = 10000;
            timerAtualizacaoNovosRegistros.Enabled = true;
            timerAtualizacaoNovosRegistros.Start();
        }

        private void FormularioPrincipal_Load(object sender, EventArgs e)
        {
            //não posso usar o banco para as configurações pois o usuário tem habito diferente dos demais. portante tenho que deixar a configuraçao por maquina...
            //using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
            //{
            //    if (!dao.TestaConexao()) { toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

            //    ExibirCaixaPostalPesquisa_toolStripMenuItem.Checked = Convert.ToBoolean(dao.RetornaValor("SELECT TOP 1 ExibirObjetosEmCaixaPostalNaPesquisa FROM TabelaConfiguracoesSistema"));
            //    ExibirItensJaEntreguesToolStripMenuItem.Checked = Convert.ToBoolean(dao.RetornaValor("SELECT TOP 1 ExibirObjetosJaEntreguesNaPesquisa FROM TabelaConfiguracoesSistema"));
            //    manterConsultaSempreAtualizadaToolStripMenuItem.Checked = Convert.ToBoolean(dao.RetornaValor("SELECT TOP 1 ManterConsultaSempreAtualizada FROM TabelaConfiguracoesSistema"));

            //    PermiriBuscarPorLDINaPesquisaToolStripMenuItem.Checked = Convert.ToBoolean(dao.RetornaValor("SELECT TOP 1 PermitirBuscarPorLDINaPesquisa FROM TabelaConfiguracoesSistema"));
            //    habilitarCapturaDeDadosDePostagemToolStripMenuItem.Checked = Convert.ToBoolean(dao.RetornaValor("SELECT TOP 1 HabilitarCapturaDeDadosDePostagem FROM TabelaConfiguracoesSistema"));
            //    habilitarCapturaDeDadosDeSaiuParaEntregaAoDestinatárioToolStripMenuItem.Checked = Convert.ToBoolean(dao.RetornaValor("SELECT TOP 1 HabilitarCapturaDeDadosDeSaiuParaEntregaAoDestinatario FROM TabelaConfiguracoesSistema"));
            //    habilitarCapturaDeDadosDeDestinatárioAusenteToolStripMenuItem.Checked = Convert.ToBoolean(dao.RetornaValor("SELECT TOP 1 HabilitarCapturaDeDadosDeDestinatarioAusente FROM TabelaConfiguracoesSistema"));
            //}

            ExibirCaixaPostalPesquisa_toolStripMenuItem.Checked = true;
            ExibirItensJaEntreguesToolStripMenuItem.Checked = true;
            manterConsultaSempreAtualizadaToolStripMenuItem.Checked = true;

            PermiriBuscarPorLDINaPesquisaToolStripMenuItem.Checked = false;
            habilitarCapturaDeDadosDePostagemToolStripMenuItem.Checked = true;
            habilitarCapturaDeDadosDeSaiuParaEntregaAoDestinatárioToolStripMenuItem.Checked = true;
            habilitarCapturaDeDadosDeDestinatárioAusenteToolStripMenuItem.Checked = true;

            VisualizarListaObjetos_toolStripButton_Click(sender, e);

            BuscaNovoStatusQuantidadeNaoAtualizados();
        }

        public void BuscaNovoStatusQuantidadeNaoAtualizados()
        {
            int quantidade = RetornaQuantidadeObjetoNaoAtualizado();
            if (quantidade > 0)
            {
                toolStripStatusLabel.Text = string.Format("Existem '{0}' objetos faltando atualizar!", quantidade);
                toolStripStatusLabel.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                toolStripStatusLabel.Text = string.Format("Status - {0} - {1}", Configuracoes.TipoAmbiente, dataSource);
                toolStripStatusLabel.ForeColor = System.Drawing.SystemColors.Desktop;
            }
        }

        public int RetornaQuantidadeObjetoNaoAtualizado()
        {
            int quantidade = 0;
            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return 0; }

                    DataSet ObjetosConsultaRastreamento = dao.RetornaDataSet("SELECT distinct CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 });
                    quantidade = ObjetosConsultaRastreamento.Tables[0].Rows.Count;
                    return quantidade;
                }
            }
            catch (Exception ex)
            {
                Mensagens.Erro("Ocorreu o seguinte erro: " + ex.Message);
                throw;
            }

        }

        public static FormularioPrincipal RetornaComponentesFormularioPrincipal()
        {
            FormularioPrincipal formularioPrincipal;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioPrincipal")
                {
                    formularioPrincipal = (FormularioPrincipal)item;
                    return (FormularioPrincipal)item;
                }
            }
            return null;
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        //usando
        private void CadastrarObjetos_toolStripButton_Click(object sender, EventArgs e)
        {
            foreach (Form item in MdiChildren)
            {
                if (item.Name == "FormularioCadastroObjetos")
                {
                    item.WindowState = FormWindowState.Maximized;
                    item.Activate();
                    return;
                }
            }

            FormularioCadastroObjetos formularioCadastroObjetos = new FormularioCadastroObjetos();
            formularioCadastroObjetos.MdiParent = this;
            formularioCadastroObjetos.Show();
            formularioCadastroObjetos.WindowState = FormWindowState.Normal;
            formularioCadastroObjetos.WindowState = FormWindowState.Maximized;
            formularioCadastroObjetos.Activate();
        }

        private void VisualizarListaObjetos_toolStripButton_Click(object sender, EventArgs e)
        {
            foreach (Form item in MdiChildren)
            {
                if (item.Name == "FormularioConsulta")
                {
                    item.Activate();
                    return;
                }
            }

            FormularioConsulta formularioConsulta = new FormularioConsulta();
            formularioConsulta.MdiParent = this;
            formularioConsulta.Show();
            formularioConsulta.WindowState = FormWindowState.Maximized;
            ExibirItensJaEntreguesToolStripMenuItem_Click(sender, e);
        }

        private void RastreamentoSRO_toolStripButton_Click(object sender, EventArgs e)
        {
            bool estaAberto = false;
            foreach (Form item in MdiChildren)
            {
                if (item.Name == "FormularioSRORastreamentoUnificado" && !item.Text.Contains("Rastreamento Unificado Detalhado"))
                {
                    estaAberto = true;
                    item.Activate();
                    break;
                }
            }
            if (estaAberto) return;

            FormularioSRORastreamentoUnificado formularioSRORastreamentoUnificado = new FormularioSRORastreamentoUnificado();
            formularioSRORastreamentoUnificado.MdiParent = this;
            formularioSRORastreamentoUnificado.Show();
            //formularioSRORastreamentoUnificado.WindowState = FormWindowState.Normal;
            formularioSRORastreamentoUnificado.WindowState = FormWindowState.Maximized;
            formularioSRORastreamentoUnificado.Activate();
        }

        private void timerAtualizacaoNovosRegistros_Tick(object sender, EventArgs e)
        {
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1.Text = string.Format("Imprimir lista de entrega lançados hoje [{0}]", DateTime.Now.GetDateTimeFormats()[14]);

            timerAtualizacaoNovosRegistros.Stop();

            Configuracoes.VerificaSeFecharAplicacaoParaAtualizacao();

            if (Application.OpenForms["FormularioConsulta"] != null)
            {
                FormularioConsulta.RetornaComponentesFormularioConsulta().LblCodigoForaDoPadraoBrasileiro.Visible = false;
                if (manterConsultaSempreAtualizadaToolStripMenuItem.Checked == true)
                {
                    //comentado antes de apresentar para o Dario. dia 18/09/2019 pois acredito que essa atualização está causando interrupção no banco de dados fazendo travamento e ter que fechar...

                    //using (FormWaiting frm = new FormWaiting(FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome)) { frm.ShowDialog(this); }
                    FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();

                    //toolStripStatusLabel.Text = "Atualizando informações novas na base de dados...";
                }
                //FormularioConsulta.RetornaComponentesFormularioConsulta().AlterarDataAoIniciarODIa();
            }
            BuscaNovoStatusQuantidadeNaoAtualizados();
            timerAtualizacaoNovosRegistros.Start();
        }

        public void sRORastreamentoUnificadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RastreamentoSRO_toolStripButton_Click(sender, e);
        }

        private void cadastrarNovosObjetosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in MdiChildren)
            {
                if (item.Name == "FormularioCadastroObjetos")
                {
                    item.Activate();
                    return;
                }
            }

            FormularioCadastroObjetos formularioCadastroObjetos = new FormularioCadastroObjetos();
            formularioCadastroObjetos.MdiParent = this;
            formularioCadastroObjetos.Show();
            //formularioCadastroObjetos.WindowState = FormWindowState.Normal;
            formularioCadastroObjetos.WindowState = FormWindowState.Maximized;






            //return;

            foreach (Form item in MdiChildren)
            {
                if (item.Name != "FormularioConsulta" && item.Name != "FormularioCadastroObjetos")
                {
                    //fecha todos que não for "FormularioConsulta" e "FormularioCadastroObjetos"
                    item.Close();
                }
            }

            bool abertoConsulta = false;
            bool abertoCadastro = false;
            foreach (Form item in MdiChildren)
            {
                if (item.Name == "FormularioConsulta") abertoConsulta = true;
                if (item.Name != "FormularioCadastroObjetos") abertoCadastro = true;
            }

            if (abertoConsulta && abertoCadastro)
            {
                LayoutMdi(MdiLayout.TileVertical);
                return;
            }
            else
            {
                if (abertoCadastro && !abertoConsulta)
                {
                    foreach (Form item in MdiChildren)
                    {
                        if (item.Name == "FormularioCadastroObjetos")
                        {
                            item.WindowState = FormWindowState.Maximized;
                            item.Activate();
                            return;
                        }
                    }
                }
            }
        }

        private void atualizarNovosObjetosPostadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormularioPrincipal.RetornaComponentesFormularioPrincipal().habilitarCapturaDeDadosDePostagemToolStripMenuItem.Checked)
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                    DataSet ObjetosConsultaRastreamento = dao.RetornaDataSet("SELECT DISTINCT CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } });
                    if (ObjetosConsultaRastreamento == null || ObjetosConsultaRastreamento.Tables[0].Rows.Count == 0) return;

                    bool abortarAtualizacao = false;
                    foreach (DataRow row in ObjetosConsultaRastreamento.Tables[0].Rows)
                    {
                        //BuscaNovoStatusQuantidadeNaoAtualizados();

                        //string codigoObjeto = dao.RetornaDataSet("SELECT TOP 1 CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } }).Tables[0].Rows[0]["CodigoObjeto"].ToString();
                        string codigoObjeto = row["CodigoObjeto"].ToString();

                        FormularioAtualizacaoObjetosPostados formularioAtualizacaoObjetosPostados = new FormularioAtualizacaoObjetosPostados(codigoObjeto);
                        formularioAtualizacaoObjetosPostados.WindowState = FormWindowState.Normal;
                        formularioAtualizacaoObjetosPostados.ShowDialog();
                        abortarAtualizacao = formularioAtualizacaoObjetosPostados.abortarAtualizacao;
                        if (abortarAtualizacao) break;

                        BuscaNovoStatusQuantidadeNaoAtualizados();
                    }
                    if (Application.OpenForms["FormularioConsulta"] != null)
                    {
                        //using (FormWaiting frm = new FormWaiting(FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome)) { frm.ShowDialog(this); }
                        FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();
                    }
                }
            }
        }

        private void atualizarNovosObjetosSaiuParaEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormularioPrincipal.RetornaComponentesFormularioPrincipal().habilitarCapturaDeDadosDeSaiuParaEntregaAoDestinatárioToolStripMenuItem.Checked)
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                    DataSet ObjetosConsultaRastreamento = dao.RetornaDataSet("SELECT DISTINCT CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } });
                    if (ObjetosConsultaRastreamento == null || ObjetosConsultaRastreamento.Tables[0].Rows.Count == 0) return;

                    bool abortarAtualizacao = false;
                    foreach (DataRow row in ObjetosConsultaRastreamento.Tables[0].Rows)
                    {
                        //BuscaNovoStatusQuantidadeNaoAtualizados();

                        //string codigoObjeto = dao.RetornaDataSet("SELECT TOP 1 CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } }).Tables[0].Rows[0]["CodigoObjeto"].ToString();
                        string codigoObjeto = row["CodigoObjeto"].ToString();

                        FormularioAtualizacaoObjetosSaiuParaEntrega formularioAtualizacaoObjetosSaiuParaEntrega = new FormularioAtualizacaoObjetosSaiuParaEntrega(codigoObjeto);
                        formularioAtualizacaoObjetosSaiuParaEntrega.WindowState = FormWindowState.Normal;
                        formularioAtualizacaoObjetosSaiuParaEntrega.ShowDialog();
                        abortarAtualizacao = formularioAtualizacaoObjetosSaiuParaEntrega.abortarAtualizacao;
                        if (abortarAtualizacao) break;

                        BuscaNovoStatusQuantidadeNaoAtualizados();
                    }
                    if (Application.OpenForms["FormularioConsulta"] != null)
                    {
                        //using (FormWaiting frm = new FormWaiting(FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome)) { frm.ShowDialog(this); }
                        FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();
                    }
                }
            }
        }

        private void atualizarNovosObjetosDestinatárioAusenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormularioPrincipal.RetornaComponentesFormularioPrincipal().habilitarCapturaDeDadosDeDestinatárioAusenteToolStripMenuItem.Checked)
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                    DataSet ObjetosConsultaRastreamento = dao.RetornaDataSet("SELECT DISTINCT CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } });
                    if (ObjetosConsultaRastreamento == null || ObjetosConsultaRastreamento.Tables[0].Rows.Count == 0) return;

                    bool abortarAtualizacao = false;
                    foreach (DataRow row in ObjetosConsultaRastreamento.Tables[0].Rows)
                    {
                        //BuscaNovoStatusQuantidadeNaoAtualizados();

                        //string codigoObjeto = dao.RetornaDataSet("SELECT TOP 1 CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } }).Tables[0].Rows[0]["CodigoObjeto"].ToString();
                        string codigoObjeto = row["CodigoObjeto"].ToString();

                        FormularioAtualizacaoObjetosDestinatarioAusente formularioAtualizacaoObjetosDestinatarioAusente = new FormularioAtualizacaoObjetosDestinatarioAusente(codigoObjeto);
                        formularioAtualizacaoObjetosDestinatarioAusente.WindowState = FormWindowState.Normal;
                        formularioAtualizacaoObjetosDestinatarioAusente.ShowDialog();
                        abortarAtualizacao = formularioAtualizacaoObjetosDestinatarioAusente.abortarAtualizacao;
                        if (abortarAtualizacao) break;

                        BuscaNovoStatusQuantidadeNaoAtualizados();
                    }
                    if (Application.OpenForms["FormularioConsulta"] != null)
                    {
                        //using (FormWaiting frm = new FormWaiting(FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome)) { frm.ShowDialog(this); }
                        FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();
                    }
                }
            }
        }

        public void atualizarNovosObjetosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                DataSet ObjetosConsultaRastreamento = dao.RetornaDataSet("SELECT DISTINCT CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } });
                if (ObjetosConsultaRastreamento == null || ObjetosConsultaRastreamento.Tables[0].Rows.Count == 0) return;

                bool abortarAtualizacao = false;
                foreach (DataRow row in ObjetosConsultaRastreamento.Tables[0].Rows)
                {
                    BuscaNovoStatusQuantidadeNaoAtualizados();

                    //string codigoObjeto = dao.RetornaDataSet("SELECT TOP 1 CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } }).Tables[0].Rows[0]["CodigoObjeto"].ToString();
                    string codigoObjeto = row["CodigoObjeto"].ToString();

                    if (FormularioPrincipal.RetornaComponentesFormularioPrincipal().habilitarCapturaDeDadosDePostagemToolStripMenuItem.Checked)
                    {
                        FormularioAtualizacaoObjetosPostados formularioAtualizacaoObjetosPostados = new FormularioAtualizacaoObjetosPostados(codigoObjeto);
                        formularioAtualizacaoObjetosPostados.WindowState = FormWindowState.Normal;
                        formularioAtualizacaoObjetosPostados.ShowDialog();
                        abortarAtualizacao = formularioAtualizacaoObjetosPostados.abortarAtualizacao;
                        if (abortarAtualizacao) break;
                    }

                    if (FormularioPrincipal.RetornaComponentesFormularioPrincipal().habilitarCapturaDeDadosDeSaiuParaEntregaAoDestinatárioToolStripMenuItem.Checked)
                    {
                        FormularioAtualizacaoObjetosSaiuParaEntrega formularioAtualizacaoObjetosSaiuParaEntrega = new FormularioAtualizacaoObjetosSaiuParaEntrega(codigoObjeto);
                        formularioAtualizacaoObjetosSaiuParaEntrega.WindowState = FormWindowState.Normal;
                        formularioAtualizacaoObjetosSaiuParaEntrega.ShowDialog();
                        abortarAtualizacao = formularioAtualizacaoObjetosSaiuParaEntrega.abortarAtualizacao;
                        if (abortarAtualizacao) break;
                    }

                    if (FormularioPrincipal.RetornaComponentesFormularioPrincipal().habilitarCapturaDeDadosDeDestinatárioAusenteToolStripMenuItem.Checked)
                    {
                        FormularioAtualizacaoObjetosDestinatarioAusente formularioAtualizacaoObjetosDestinatarioAusente = new FormularioAtualizacaoObjetosDestinatarioAusente(codigoObjeto);
                        formularioAtualizacaoObjetosDestinatarioAusente.WindowState = FormWindowState.Normal;
                        formularioAtualizacaoObjetosDestinatarioAusente.ShowDialog();
                        abortarAtualizacao = formularioAtualizacaoObjetosDestinatarioAusente.abortarAtualizacao;
                        if (abortarAtualizacao) break;
                    }

                    FormularioAtualizacaoObjetosAguardandoRetirada formularioAtualizacaoObjetosAguardandoRetirada = new FormularioAtualizacaoObjetosAguardandoRetirada(codigoObjeto);
                    formularioAtualizacaoObjetosAguardandoRetirada.WindowState = FormWindowState.Normal;
                    formularioAtualizacaoObjetosAguardandoRetirada.ShowDialog();
                    abortarAtualizacao = formularioAtualizacaoObjetosAguardandoRetirada.abortarAtualizacao;
                    if (abortarAtualizacao) break;

                    BuscaNovoStatusQuantidadeNaoAtualizados();
                }
                if (Application.OpenForms["FormularioConsulta"] != null)
                {
                    //using (FormWaiting frm = new FormWaiting(FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome)) { frm.ShowDialog(this); }
                    FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();
                }
            }
        }

        private void atualizarNovosObjetosCompletoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                DataSet ObjetosConsultaRastreamento = dao.RetornaDataSet("SELECT DISTINCT CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } });
                if (ObjetosConsultaRastreamento == null || ObjetosConsultaRastreamento.Tables[0].Rows.Count == 0) return;

                bool abortarAtualizacao = false;
                foreach (DataRow row in ObjetosConsultaRastreamento.Tables[0].Rows)
                {
                    BuscaNovoStatusQuantidadeNaoAtualizados();

                    //string codigoObjeto = dao.RetornaDataSet("SELECT TOP 1 CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } }).Tables[0].Rows[0]["CodigoObjeto"].ToString();
                    string codigoObjeto = row["CodigoObjeto"].ToString();

                    FormularioAtualizacaoObjetosCompleto formularioAtualizacaoObjetosCompleto = new FormularioAtualizacaoObjetosCompleto();
                    formularioAtualizacaoObjetosCompleto.WindowState = FormWindowState.Normal;
                    formularioAtualizacaoObjetosCompleto.ShowDialog();
                    abortarAtualizacao = formularioAtualizacaoObjetosCompleto.abortarAtualizacao;
                    if (abortarAtualizacao) break;
                    BuscaNovoStatusQuantidadeNaoAtualizados();
                }

                if (Application.OpenForms["FormularioConsulta"] != null)
                {
                    //using (FormWaiting frm = new FormWaiting(FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome)) { frm.ShowDialog(this); }
                    FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();
                }
            }
        }

        public void visualizarListaDeObjetosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VisualizarListaObjetos_toolStripButton_Click(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioSobre formularioSobre = new FormularioSobre();
            formularioSobre.ShowDialog();
        }

        public void ExibirCaixaPostalPesquisa_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////GRAVA NO BANCO 
            //using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            //{
            //    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

            //    dao.ExecutaSQL("UPDATE TabelaConfiguracoesSistema SET ExibirObjetosEmCaixaPostalNaPesquisa = @ExibirObjetosEmCaixaPostalNaPesquisa", new List<Parametros>(){
            //                                                new Parametros("@ExibirObjetosEmCaixaPostalNaPesquisa", TipoCampo.Int, ExibirCaixaPostalPesquisa_toolStripMenuItem.Checked)});
            //}

            FormularioConsulta formularioConsulta;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioConsulta")
                {
                    formularioConsulta = (FormularioConsulta)item;
                    formularioConsulta.MontaFiltro();
                    formularioConsulta.Activate();
                    break;
                }
            }
        }

        public void ExibirItensJaEntreguesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////GRAVA NO BANCO 
            //using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            //{
            //    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

            //    dao.ExecutaSQL("UPDATE TabelaConfiguracoesSistema SET ExibirObjetosJaEntreguesNaPesquisa = @ExibirObjetosJaEntreguesNaPesquisa", new List<Parametros>(){
            //                                                new Parametros("@ExibirObjetosJaEntreguesNaPesquisa", TipoCampo.Int, ExibirItensJaEntreguesToolStripMenuItem.Checked)});
            //}

            FormularioConsulta formularioConsulta;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioConsulta")
                {
                    formularioConsulta = (FormularioConsulta)item;
                    formularioConsulta.MontaFiltro();
                    formularioConsulta.Activate();
                    break;
                }
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioOpcoes formularioOpcoes = new FormularioOpcoes();
            formularioOpcoes.ShowDialog();
        }

        private void FormularioPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                RastreamentoSRO_toolStripButton_Click(sender, e);
            }
            if (e.KeyCode == Keys.F12)
            {
                VisualizarListaObjetos_toolStripButton_Click(sender, e);
            }
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Mensagens.Pergunta("Deseja fechar o sistema?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        this.Close();
            //    }
            //}
        }

        public void imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioConsulta formularioConsulta;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioConsulta")
                {
                    formularioConsulta = (FormularioConsulta)item;
                    formularioConsulta.GeraImpressaoItensSelecionados();
                    break;
                }
            }
        }

        private void imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormularioConsulta formularioConsulta;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioConsulta")
                {
                    formularioConsulta = (FormularioConsulta)item;

                    FormularioImpressaoEntregaObjetosOpcoesImpressao formularioImpressaoEntregaObjetosOpcoesImpressao = new FormularioImpressaoEntregaObjetosOpcoesImpressao();
                    formularioImpressaoEntregaObjetosOpcoesImpressao.ShowDialog();
                    if (formularioImpressaoEntregaObjetosOpcoesImpressao.Cancelou == true) break;
                    formularioConsulta.GeraImpressaoItensLancadosNoDiaHoje(formularioImpressaoEntregaObjetosOpcoesImpressao.IncluirItensJaEntregues, formularioImpressaoEntregaObjetosOpcoesImpressao.IncluirItensCaixaPostal);
                    break;
                }
            }
        }

        private void auxílioÀGestaoDoDiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormularioAuxilioGestaoDia frm = new FormularioAuxilioGestaoDia();
            //frm.ShowDialog();

            foreach (Form item in MdiChildren)
            {
                if (item.Name == "FormularioAuxilioGestaoDia")
                {
                    item.WindowState = FormWindowState.Maximized;
                    item.Activate();
                    return;
                }
            }

            FormularioAuxilioGestaoDia formularioAuxilioGestaoDia = new FormularioAuxilioGestaoDia();
            formularioAuxilioGestaoDia.MdiParent = this;
            formularioAuxilioGestaoDia.Show();
            formularioAuxilioGestaoDia.WindowState = FormWindowState.Normal;
            formularioAuxilioGestaoDia.WindowState = FormWindowState.Maximized;
            formularioAuxilioGestaoDia.Activate();
        }

        private void exibirMensagensAoDesevolvedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuracoes.ExibicaoMensagensParaDesenvolvedor = exibirMensagensAoDesevolvedorToolStripMenuItem.Checked;
        }

        private void marcarSelecionadosComoAtualizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioConsulta formularioConsulta;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioConsulta")
                {
                    formularioConsulta = (FormularioConsulta)item;
                    timerAtualizacaoNovosRegistros.Stop();
                    formularioConsulta.MarcarSelecionadosComoAtualizado();
                    BuscaNovoStatusQuantidadeNaoAtualizados();
                    timerAtualizacaoNovosRegistros.Start();
                    break;
                }
            }
        }

        private void marcarSelecionadosComoNaoAtualizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioConsulta formularioConsulta;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioConsulta")
                {
                    formularioConsulta = (FormularioConsulta)item;
                    timerAtualizacaoNovosRegistros.Stop();
                    formularioConsulta.MarcarSelecionadosComoNaoAtualizado();
                    BuscaNovoStatusQuantidadeNaoAtualizados();
                    timerAtualizacaoNovosRegistros.Start();
                    break;
                }
            }
        }

        public void solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult pergunta = Mensagens.Pergunta("Deseja realmente solicitar uma verificação\npara todos os objetos ainda não entregues?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (pergunta == System.Windows.Forms.DialogResult.No)
            {

                return;
            }
            if (pergunta == System.Windows.Forms.DialogResult.Yes)
            {
                //grava no banco de dados
                using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                    dao.ExecutaSQL("UPDATE TabelaObjetosSROLocal SET Atualizado = @Atualizado WHERE ObjetoEntregue = @ObjetoEntregue ", new List<Parametros>(){
                                            new Parametros("@Atualizado", TipoCampo.Int, 0),
                                            new Parametros("@ObjetoEntregue", TipoCampo.Int, 0)});

                    DataSet ObjetosConsultaRastreamento = dao.RetornaDataSet("SELECT DISTINCT CodigoObjeto, Atualizado, DataModificacao FROM TabelaObjetosSROLocal WHERE Atualizado = @Atualizado OR DataModificacao = @DataModificacao", new List<Parametros>() {
                        new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 },
                        new Parametros { Nome = "@DataModificacao", Tipo = TipoCampo.Text, Valor = "" }
                    });
                    if (ObjetosConsultaRastreamento == null || ObjetosConsultaRastreamento.Tables[0].Rows.Count == 0) return;

                    bool abortarAtualizacao = false;
                    foreach (DataRow row in ObjetosConsultaRastreamento.Tables[0].Rows)
                    {
                        BuscaNovoStatusQuantidadeNaoAtualizados();

                        //string codigoObjeto = dao.RetornaDataSet("SELECT TOP 1 CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } }).Tables[0].Rows[0]["CodigoObjeto"].ToString();
                        string codigoObjeto = row["CodigoObjeto"].ToString();

                        FormularioAtualizacaoObjetosRequeridosJaEntregues formularioAtualizacaoObjetosRequeridosJaEntregues = new FormularioAtualizacaoObjetosRequeridosJaEntregues(codigoObjeto);
                        formularioAtualizacaoObjetosRequeridosJaEntregues.WindowState = FormWindowState.Normal;
                        formularioAtualizacaoObjetosRequeridosJaEntregues.ShowDialog();
                        abortarAtualizacao = formularioAtualizacaoObjetosRequeridosJaEntregues.abortarAtualizacao;
                        if (abortarAtualizacao) break;

                        BuscaNovoStatusQuantidadeNaoAtualizados();
                    }
                    if (Application.OpenForms["FormularioConsulta"] != null)
                    {
                        //using (FormWaiting frm = new FormWaiting(FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome)) { frm.ShowDialog(this); }
                        FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();
                    }
                }
            }
        }

        private void formularioAtualizacaoObjetosRequeridosJaEntreguesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                DataSet ObjetosConsultaRastreamento = dao.RetornaDataSet("SELECT DISTINCT CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } });
                if (ObjetosConsultaRastreamento == null || ObjetosConsultaRastreamento.Tables[0].Rows.Count == 0) return;

                bool abortarAtualizacao = false;
                foreach (DataRow row in ObjetosConsultaRastreamento.Tables[0].Rows)
                {
                    BuscaNovoStatusQuantidadeNaoAtualizados();

                    //string codigoObjeto = dao.RetornaDataSet("SELECT TOP 1 CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } }).Tables[0].Rows[0]["CodigoObjeto"].ToString();
                    string codigoObjeto = row["CodigoObjeto"].ToString();

                    FormularioAtualizacaoObjetosRequeridosJaEntregues formularioAtualizacaoObjetosRequeridosJaEntregues = new FormularioAtualizacaoObjetosRequeridosJaEntregues(codigoObjeto);
                    formularioAtualizacaoObjetosRequeridosJaEntregues.WindowState = FormWindowState.Normal;
                    formularioAtualizacaoObjetosRequeridosJaEntregues.ShowDialog();
                    abortarAtualizacao = formularioAtualizacaoObjetosRequeridosJaEntregues.abortarAtualizacao;
                    if (abortarAtualizacao) break;

                    BuscaNovoStatusQuantidadeNaoAtualizados();
                }
                if (Application.OpenForms["FormularioConsulta"] != null)
                {
                    //using (FormWaiting frm = new FormWaiting(FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome)) { frm.ShowDialog(this); }
                    FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();
                }
            }
        }

        private void atualizaNovosObjetosAguardandoRetiradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
            {
                if (!dao.TestaConexao()) { toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                DataSet ObjetosConsultaRastreamento = dao.RetornaDataSet("SELECT DISTINCT CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } });
                if (ObjetosConsultaRastreamento == null || ObjetosConsultaRastreamento.Tables[0].Rows.Count == 0) return;

                bool abortarAtualizacao = false;
                foreach (DataRow row in ObjetosConsultaRastreamento.Tables[0].Rows)
                {
                    BuscaNovoStatusQuantidadeNaoAtualizados();

                    //string codigoObjeto = dao.RetornaDataSet("SELECT TOP 1 CodigoObjeto FROM TabelaObjetosSROLocal WHERE (Atualizado = @Atualizado)", new List<Parametros>() { new Parametros { Nome = "@Atualizado", Tipo = TipoCampo.Int, Valor = 0 } }).Tables[0].Rows[0]["CodigoObjeto"].ToString();
                    string codigoObjeto = row["CodigoObjeto"].ToString();
                    Mensagens.InformaDesenvolvedor("Vou me preparar para iniciar o processo de atualização do objeto \n:" + codigoObjeto);

                    FormularioAtualizacaoObjetosAguardandoRetirada formularioAtualizacaoObjetosAguardandoRetirada = new FormularioAtualizacaoObjetosAguardandoRetirada(codigoObjeto);
                    formularioAtualizacaoObjetosAguardandoRetirada.WindowState = FormWindowState.Normal;
                    formularioAtualizacaoObjetosAguardandoRetirada.ShowDialog();
                    abortarAtualizacao = formularioAtualizacaoObjetosAguardandoRetirada.abortarAtualizacao;
                    if (abortarAtualizacao) break;

                    BuscaNovoStatusQuantidadeNaoAtualizados();
                }
                if (Application.OpenForms["FormularioConsulta"] != null)
                {
                    //using (FormWaiting frm = new FormWaiting(FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome)) { frm.ShowDialog(this); }
                    FormularioConsulta.RetornaComponentesFormularioConsulta().ConsultaTodosNaoEntreguesOrdenadoNome();
                }
            }
        }

        private void removerLinhasSelecionadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //removerItemToolStripMenuItem_Click
            FormularioConsulta formularioConsulta;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioConsulta")
                {
                    formularioConsulta = (FormularioConsulta)item;
                    timerAtualizacaoNovosRegistros.Stop();
                    //formularioConsulta.MarcarSelecionadosComoNaoAtualizado();
                    formularioConsulta.removerItemToolStripMenuItem_Click(sender, e);
                    BuscaNovoStatusQuantidadeNaoAtualizados();
                    timerAtualizacaoNovosRegistros.Start();
                    break;
                }
            }
        }

        private void manterConsultaSempreAtualizadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////GRAVA NO BANCO 
            //using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            //{
            //    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

            //    dao.ExecutaSQL("UPDATE TabelaConfiguracoesSistema SET ManterConsultaSempreAtualizada = @ManterConsultaSempreAtualizada", new List<Parametros>(){
            //                                                new Parametros("@ManterConsultaSempreAtualizada", TipoCampo.Int, manterConsultaSempreAtualizadaToolStripMenuItem.Checked)});
            //    dao.ExecutaSQL("UPDATE TabelaConfiguracoesSistema SET TempoAtualizacaoConsultaSempreAtualizada = @TempoAtualizacaoConsultaSempreAtualizada", new List<Parametros>(){
            //                                                new Parametros("@TempoAtualizacaoConsultaSempreAtualizada", TipoCampo.Text, "600000")});                
            //}
        }

        private void PermiriBuscarPorLDINaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////GRAVA NO BANCO 
            //using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            //{
            //    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

            //    dao.ExecutaSQL("UPDATE TabelaConfiguracoesSistema SET PermitirBuscarPorLDINaPesquisa = @PermitirBuscarPorLDINaPesquisa", new List<Parametros>(){
            //                                                new Parametros("@PermitirBuscarPorLDINaPesquisa", TipoCampo.Int, PermiriBuscarPorLDINaPesquisaToolStripMenuItem.Checked)});
            //}

            FormularioConsulta formularioConsulta;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "FormularioConsulta")
                {
                    formularioConsulta = (FormularioConsulta)item;
                    formularioConsulta.MontaFiltro();
                    formularioConsulta.Activate();
                    break;
                }
            }
        }

        private void habilitarCapturaDeDadosDePostagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////GRAVA NO BANCO 
            //using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            //{
            //    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

            //    dao.ExecutaSQL("UPDATE TabelaConfiguracoesSistema SET HabilitarCapturaDeDadosDePostagem = @HabilitarCapturaDeDadosDePostagem", new List<Parametros>(){
            //                                                new Parametros("@HabilitarCapturaDeDadosDePostagem", TipoCampo.Int, habilitarCapturaDeDadosDePostagemToolStripMenuItem.Checked)});
            //}
        }

        private void habilitarCapturaDeDadosDeSaiuParaEntregaAoDestinatárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////GRAVA NO BANCO 
            //using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            //{
            //    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

            //    dao.ExecutaSQL("UPDATE TabelaConfiguracoesSistema SET HabilitarCapturaDeDadosDeSaiuParaEntregaAoDestinatario = @HabilitarCapturaDeDadosDeSaiuParaEntregaAoDestinatario", new List<Parametros>(){
            //                                                new Parametros("@HabilitarCapturaDeDadosDeSaiuParaEntregaAoDestinatario", TipoCampo.Int, habilitarCapturaDeDadosDeSaiuParaEntregaAoDestinatárioToolStripMenuItem.Checked)});
            //}
        }

        private void habilitarCapturaDeDadosDeDestinatárioAusenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////GRAVA NO BANCO 
            //using (DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao))
            //{
            //    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

            //    dao.ExecutaSQL("UPDATE TabelaConfiguracoesSistema SET HabilitarCapturaDeDadosDeDestinatarioAusente = @HabilitarCapturaDeDadosDeDestinatarioAusente", new List<Parametros>(){
            //                                                new Parametros("@HabilitarCapturaDeDadosDeDestinatarioAusente", TipoCampo.Int, habilitarCapturaDeDadosDeDestinatárioAusenteToolStripMenuItem.Checked)});
            //}
        }
    }
}
