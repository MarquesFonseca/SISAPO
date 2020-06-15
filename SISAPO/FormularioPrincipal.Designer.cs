using System;
namespace SISAPO
{
    partial class FormularioPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioPrincipal));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.sRORastreamentoUnificadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualizarListaDeObjetosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cadastrarNovosObjetosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.atualizarNovosObjetosCompletoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atualizarNovosObjetosPostadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atualizarNovosObjetosSaiuParaEntregaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atualizaNovosObjetosAguardandoRetiradaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formularioAtualizacaoObjetosRequeridosJaEntreguesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atualizarNovosObjetosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.auxílioÀGestaoDoDiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IncluirCaixaPostalPesquisa_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExibirItensJaEntreguesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manterConsultaSempreAtualizadaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manterDataFinalEmHojeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.marcarSelecionadosComoAtualizadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marcarSelecionadosComoNaoAtualizadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exibirMensagensAoDesevolvedorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removerLinhasSelecionadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.RastreamentoSRO_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.VisualizarListaObjetos_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timerAtualizacaoNovosRegistros = new System.Windows.Forms.Timer(this.components);
            this.atualizarNovosObjetosDestinatarioAusenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsMenu,
            this.opçõesToolStripMenuItem,
            this.viewMenu,
            this.windowsMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(739, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sRORastreamentoUnificadoToolStripMenuItem,
            this.visualizarListaDeObjetosToolStripMenuItem,
            this.toolStripSeparator7,
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem,
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1,
            this.toolStripSeparator6,
            this.cadastrarNovosObjetosToolStripMenuItem,
            this.toolStripSeparator1,
            this.atualizarNovosObjetosCompletoToolStripMenuItem,
            this.atualizarNovosObjetosPostadosToolStripMenuItem,
            this.atualizarNovosObjetosSaiuParaEntregaToolStripMenuItem,
            this.atualizarNovosObjetosDestinatarioAusenteToolStripMenuItem,
            this.atualizaNovosObjetosAguardandoRetiradaToolStripMenuItem,
            this.formularioAtualizacaoObjetosRequeridosJaEntreguesToolStripMenuItem,
            this.atualizarNovosObjetosToolStripMenuItem,
            this.solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem,
            this.toolStripSeparator5,
            this.auxílioÀGestaoDoDiaToolStripMenuItem});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(84, 20);
            this.toolsMenu.Text = "&Ferramentas";
            // 
            // sRORastreamentoUnificadoToolStripMenuItem
            // 
            this.sRORastreamentoUnificadoToolStripMenuItem.Image = global::SISAPO.Properties.Resources.LogoCorreios;
            this.sRORastreamentoUnificadoToolStripMenuItem.Name = "sRORastreamentoUnificadoToolStripMenuItem";
            this.sRORastreamentoUnificadoToolStripMenuItem.Size = new System.Drawing.Size(459, 22);
            this.sRORastreamentoUnificadoToolStripMenuItem.Text = "&SRO - Rastreamento Unificado";
            this.sRORastreamentoUnificadoToolStripMenuItem.Visible = false;
            this.sRORastreamentoUnificadoToolStripMenuItem.Click += new System.EventHandler(this.sRORastreamentoUnificadoToolStripMenuItem_Click);
            // 
            // visualizarListaDeObjetosToolStripMenuItem
            // 
            this.visualizarListaDeObjetosToolStripMenuItem.Image = global::SISAPO.Properties.Resources.VisualizarListaObjetos;
            this.visualizarListaDeObjetosToolStripMenuItem.Name = "visualizarListaDeObjetosToolStripMenuItem";
            this.visualizarListaDeObjetosToolStripMenuItem.Size = new System.Drawing.Size(459, 22);
            this.visualizarListaDeObjetosToolStripMenuItem.Text = "&Visualizar lista de objeto(s)";
            this.visualizarListaDeObjetosToolStripMenuItem.Visible = false;
            this.visualizarListaDeObjetosToolStripMenuItem.Click += new System.EventHandler(this.visualizarListaDeObjetosToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(456, 6);
            // 
            // imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem
            // 
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem.Image = global::SISAPO.Properties.Resources.if_BT_printer_905556;
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem.Name = "imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem";
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem.Size = new System.Drawing.Size(459, 22);
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem.Text = "Imprimir lista de entrega (Selecionados)";
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem.Click += new System.EventHandler(this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem_Click);
            // 
            // imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1
            // 
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1.Image = global::SISAPO.Properties.Resources.if_BT_printer_905556;
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1.Name = "imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1";
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1.Size = new System.Drawing.Size(459, 22);
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1.Text = "Imprimir lista de entrega lançados hoje [quinta-feira, 11 de julho de 2019]";
            this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1.Click += new System.EventHandler(this.imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(456, 6);
            // 
            // cadastrarNovosObjetosToolStripMenuItem
            // 
            this.cadastrarNovosObjetosToolStripMenuItem.Image = global::SISAPO.Properties.Resources.CadastroObjetos;
            this.cadastrarNovosObjetosToolStripMenuItem.Name = "cadastrarNovosObjetosToolStripMenuItem";
            this.cadastrarNovosObjetosToolStripMenuItem.Size = new System.Drawing.Size(459, 22);
            this.cadastrarNovosObjetosToolStripMenuItem.Text = "&Cadastrar Novo(s) Objeto(s)";
            this.cadastrarNovosObjetosToolStripMenuItem.Click += new System.EventHandler(this.cadastrarNovosObjetosToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(456, 6);
            // 
            // atualizarNovosObjetosCompletoToolStripMenuItem
            // 
            this.atualizarNovosObjetosCompletoToolStripMenuItem.Image = global::SISAPO.Properties.Resources.AtualizarListaObjetos;
            this.atualizarNovosObjetosCompletoToolStripMenuItem.Name = "atualizarNovosObjetosCompletoToolStripMenuItem";
            this.atualizarNovosObjetosCompletoToolStripMenuItem.Size = new System.Drawing.Size(459, 22);
            this.atualizarNovosObjetosCompletoToolStripMenuItem.Text = "0 - Atualizar Novo(s) Objetos Completo";
            this.atualizarNovosObjetosCompletoToolStripMenuItem.Visible = false;
            this.atualizarNovosObjetosCompletoToolStripMenuItem.Click += new System.EventHandler(this.atualizarNovosObjetosCompletoToolStripMenuItem_Click);
            // 
            // atualizarNovosObjetosPostadosToolStripMenuItem
            // 
            this.atualizarNovosObjetosPostadosToolStripMenuItem.Image = global::SISAPO.Properties.Resources.AtualizarListaObjetos;
            this.atualizarNovosObjetosPostadosToolStripMenuItem.Name = "atualizarNovosObjetosPostadosToolStripMenuItem";
            this.atualizarNovosObjetosPostadosToolStripMenuItem.Size = new System.Drawing.Size(459, 22);
            this.atualizarNovosObjetosPostadosToolStripMenuItem.Text = "1 - Atualizar Novo(s) Objetos Postados";
            this.atualizarNovosObjetosPostadosToolStripMenuItem.Click += new System.EventHandler(this.atualizarNovosObjetosPostadosToolStripMenuItem_Click);
            // 
            // atualizarNovosObjetosSaiuParaEntregaToolStripMenuItem
            // 
            this.atualizarNovosObjetosSaiuParaEntregaToolStripMenuItem.Image = global::SISAPO.Properties.Resources.AtualizarListaObjetos;
            this.atualizarNovosObjetosSaiuParaEntregaToolStripMenuItem.Name = "atualizarNovosObjetosSaiuParaEntregaToolStripMenuItem";
            this.atualizarNovosObjetosSaiuParaEntregaToolStripMenuItem.Size = new System.Drawing.Size(459, 22);
            this.atualizarNovosObjetosSaiuParaEntregaToolStripMenuItem.Text = "2 - Atualizar Novo(s) Objetos Saiu para Entrega";
            this.atualizarNovosObjetosSaiuParaEntregaToolStripMenuItem.Click += new System.EventHandler(this.atualizarNovosObjetosSaiuParaEntregaToolStripMenuItem_Click);
            // 
            // atualizaNovosObjetosAguardandoRetiradaToolStripMenuItem
            // 
            this.atualizaNovosObjetosAguardandoRetiradaToolStripMenuItem.Image = global::SISAPO.Properties.Resources.AtualizarListaObjetos;
            this.atualizaNovosObjetosAguardandoRetiradaToolStripMenuItem.Name = "atualizaNovosObjetosAguardandoRetiradaToolStripMenuItem";
            this.atualizaNovosObjetosAguardandoRetiradaToolStripMenuItem.Size = new System.Drawing.Size(459, 22);
            this.atualizaNovosObjetosAguardandoRetiradaToolStripMenuItem.Text = "4 - Atualiza Novo(s) Objetos Aguardando Retirada";
            this.atualizaNovosObjetosAguardandoRetiradaToolStripMenuItem.Click += new System.EventHandler(this.atualizaNovosObjetosAguardandoRetiradaToolStripMenuItem_Click);
            // 
            // formularioAtualizacaoObjetosRequeridosJaEntreguesToolStripMenuItem
            // 
            this.formularioAtualizacaoObjetosRequeridosJaEntreguesToolStripMenuItem.Image = global::SISAPO.Properties.Resources.AtualizarListaObjetos;
            this.formularioAtualizacaoObjetosRequeridosJaEntreguesToolStripMenuItem.Name = "formularioAtualizacaoObjetosRequeridosJaEntreguesToolStripMenuItem";
            this.formularioAtualizacaoObjetosRequeridosJaEntreguesToolStripMenuItem.Size = new System.Drawing.Size(459, 22);
            this.formularioAtualizacaoObjetosRequeridosJaEntreguesToolStripMenuItem.Text = "5 - Formulário verificação de Objetos ainda não Entregues";
            this.formularioAtualizacaoObjetosRequeridosJaEntreguesToolStripMenuItem.Visible = false;
            this.formularioAtualizacaoObjetosRequeridosJaEntreguesToolStripMenuItem.Click += new System.EventHandler(this.formularioAtualizacaoObjetosRequeridosJaEntreguesToolStripMenuItem_Click);
            // 
            // atualizarNovosObjetosToolStripMenuItem
            // 
            this.atualizarNovosObjetosToolStripMenuItem.Image = global::SISAPO.Properties.Resources.AtualizarListaObjetos;
            this.atualizarNovosObjetosToolStripMenuItem.Name = "atualizarNovosObjetosToolStripMenuItem";
            this.atualizarNovosObjetosToolStripMenuItem.Size = new System.Drawing.Size(459, 22);
            this.atualizarNovosObjetosToolStripMenuItem.Text = "Atualizar Novo(s) Objeto(s)";
            this.atualizarNovosObjetosToolStripMenuItem.Click += new System.EventHandler(this.atualizarNovosObjetosToolStripMenuItem_Click);
            // 
            // solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem
            // 
            this.solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem.Image = global::SISAPO.Properties.Resources.AtualizarListaObjetos;
            this.solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem.Name = "solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem";
            this.solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem.Size = new System.Drawing.Size(459, 22);
            this.solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem.Text = "&Solicitar verificação de Objetos ainda não Entregues";
            this.solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem.Click += new System.EventHandler(this.solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(456, 6);
            // 
            // auxílioÀGestaoDoDiaToolStripMenuItem
            // 
            this.auxílioÀGestaoDoDiaToolStripMenuItem.Image = global::SISAPO.Properties.Resources.CadastroObjetos;
            this.auxílioÀGestaoDoDiaToolStripMenuItem.Name = "auxílioÀGestaoDoDiaToolStripMenuItem";
            this.auxílioÀGestaoDoDiaToolStripMenuItem.Size = new System.Drawing.Size(459, 22);
            this.auxílioÀGestaoDoDiaToolStripMenuItem.Text = "Auxílio à gestão do dia";
            this.auxílioÀGestaoDoDiaToolStripMenuItem.Click += new System.EventHandler(this.auxílioÀGestaoDoDiaToolStripMenuItem_Click);
            // 
            // opçõesToolStripMenuItem
            // 
            this.opçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IncluirCaixaPostalPesquisa_toolStripMenuItem,
            this.ExibirItensJaEntreguesToolStripMenuItem,
            this.manterConsultaSempreAtualizadaToolStripMenuItem,
            this.manterDataFinalEmHojeToolStripMenuItem,
            this.toolStripSeparator3,
            this.marcarSelecionadosComoAtualizadoToolStripMenuItem,
            this.marcarSelecionadosComoNaoAtualizadoToolStripMenuItem,
            this.toolStripSeparator4,
            this.exibirMensagensAoDesevolvedorToolStripMenuItem,
            this.removerLinhasSelecionadasToolStripMenuItem,
            this.toolStripSeparator2,
            this.optionsToolStripMenuItem});
            this.opçõesToolStripMenuItem.Name = "opçõesToolStripMenuItem";
            this.opçõesToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.opçõesToolStripMenuItem.Text = "&Opções";
            // 
            // IncluirCaixaPostalPesquisa_toolStripMenuItem
            // 
            this.IncluirCaixaPostalPesquisa_toolStripMenuItem.CheckOnClick = true;
            this.IncluirCaixaPostalPesquisa_toolStripMenuItem.Name = "IncluirCaixaPostalPesquisa_toolStripMenuItem";
            this.IncluirCaixaPostalPesquisa_toolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.IncluirCaixaPostalPesquisa_toolStripMenuItem.Text = "&Exibir Objetos em Caixa Postal";
            this.IncluirCaixaPostalPesquisa_toolStripMenuItem.Click += new System.EventHandler(this.IncluirCaixaPostalPesquisa_toolStripMenuItem_Click);
            // 
            // ExibirItensJaEntreguesToolStripMenuItem
            // 
            this.ExibirItensJaEntreguesToolStripMenuItem.CheckOnClick = true;
            this.ExibirItensJaEntreguesToolStripMenuItem.Name = "ExibirItensJaEntreguesToolStripMenuItem";
            this.ExibirItensJaEntreguesToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.ExibirItensJaEntreguesToolStripMenuItem.Text = "&Incluir Objetos já entregues";
            this.ExibirItensJaEntreguesToolStripMenuItem.Click += new System.EventHandler(this.ExibirItensJaEntreguesToolStripMenuItem_Click);
            // 
            // manterConsultaSempreAtualizadaToolStripMenuItem
            // 
            this.manterConsultaSempreAtualizadaToolStripMenuItem.Checked = true;
            this.manterConsultaSempreAtualizadaToolStripMenuItem.CheckOnClick = true;
            this.manterConsultaSempreAtualizadaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.manterConsultaSempreAtualizadaToolStripMenuItem.Name = "manterConsultaSempreAtualizadaToolStripMenuItem";
            this.manterConsultaSempreAtualizadaToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.manterConsultaSempreAtualizadaToolStripMenuItem.Text = "Manter consulta sempre atualizada";
            // 
            // manterDataFinalEmHojeToolStripMenuItem
            // 
            this.manterDataFinalEmHojeToolStripMenuItem.Checked = true;
            this.manterDataFinalEmHojeToolStripMenuItem.CheckOnClick = true;
            this.manterDataFinalEmHojeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.manterDataFinalEmHojeToolStripMenuItem.Name = "manterDataFinalEmHojeToolStripMenuItem";
            this.manterDataFinalEmHojeToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.manterDataFinalEmHojeToolStripMenuItem.Text = "Manter Data Final em \"Hoje\"";
            this.manterDataFinalEmHojeToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(298, 6);
            // 
            // marcarSelecionadosComoAtualizadoToolStripMenuItem
            // 
            this.marcarSelecionadosComoAtualizadoToolStripMenuItem.Name = "marcarSelecionadosComoAtualizadoToolStripMenuItem";
            this.marcarSelecionadosComoAtualizadoToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.marcarSelecionadosComoAtualizadoToolStripMenuItem.Text = "Marcar Selecionados como Atualizado";
            this.marcarSelecionadosComoAtualizadoToolStripMenuItem.Click += new System.EventHandler(this.marcarSelecionadosComoAtualizadoToolStripMenuItem_Click);
            // 
            // marcarSelecionadosComoNaoAtualizadoToolStripMenuItem
            // 
            this.marcarSelecionadosComoNaoAtualizadoToolStripMenuItem.Name = "marcarSelecionadosComoNaoAtualizadoToolStripMenuItem";
            this.marcarSelecionadosComoNaoAtualizadoToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.marcarSelecionadosComoNaoAtualizadoToolStripMenuItem.Text = "Marcar Selecionados como Não Atualizado";
            this.marcarSelecionadosComoNaoAtualizadoToolStripMenuItem.Click += new System.EventHandler(this.marcarSelecionadosComoNaoAtualizadoToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(298, 6);
            // 
            // exibirMensagensAoDesevolvedorToolStripMenuItem
            // 
            this.exibirMensagensAoDesevolvedorToolStripMenuItem.CheckOnClick = true;
            this.exibirMensagensAoDesevolvedorToolStripMenuItem.Name = "exibirMensagensAoDesevolvedorToolStripMenuItem";
            this.exibirMensagensAoDesevolvedorToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.exibirMensagensAoDesevolvedorToolStripMenuItem.Text = "Exibir Mensagens ao desevolvedor";
            this.exibirMensagensAoDesevolvedorToolStripMenuItem.Visible = false;
            this.exibirMensagensAoDesevolvedorToolStripMenuItem.Click += new System.EventHandler(this.exibirMensagensAoDesevolvedorToolStripMenuItem_Click);
            // 
            // removerLinhasSelecionadasToolStripMenuItem
            // 
            this.removerLinhasSelecionadasToolStripMenuItem.Name = "removerLinhasSelecionadasToolStripMenuItem";
            this.removerLinhasSelecionadasToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.removerLinhasSelecionadasToolStripMenuItem.Text = "Remover Linhas Selecionadas";
            this.removerLinhasSelecionadasToolStripMenuItem.Click += new System.EventHandler(this.removerLinhasSelecionadasToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(298, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.optionsToolStripMenuItem.Text = "&Opções do sistema";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(83, 20);
            this.viewMenu.Text = "&Visualização";
            this.viewMenu.Visible = false;
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.toolBarToolStripMenuItem.Text = "&Toolbar";
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolBarToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.statusBarToolStripMenuItem.Text = "&Status Bar";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.StatusBarToolStripMenuItem_Click);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(56, 20);
            this.windowsMenu.Text = "&Janelas";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.cascadeToolStripMenuItem.Text = "&Cascata";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.tileVerticalToolStripMenuItem.Text = "Telha &Vestical";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.tileHorizontalToolStripMenuItem.Text = "Telha &Horizontal";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.closeAllToolStripMenuItem.Text = "F&echar Todas";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(50, 20);
            this.helpMenu.Text = "&Ajuda";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.aboutToolStripMenuItem.Text = "&Sobre";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RastreamentoSRO_toolStripButton,
            this.toolStripSeparator9,
            this.VisualizarListaObjetos_toolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(739, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // RastreamentoSRO_toolStripButton
            // 
            this.RastreamentoSRO_toolStripButton.Image = global::SISAPO.Properties.Resources.LogoCorreios;
            this.RastreamentoSRO_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RastreamentoSRO_toolStripButton.Name = "RastreamentoSRO_toolStripButton";
            this.RastreamentoSRO_toolStripButton.Size = new System.Drawing.Size(218, 22);
            this.RastreamentoSRO_toolStripButton.Text = "SRO - Rastreamento Unificado - [F9]";
            this.RastreamentoSRO_toolStripButton.Click += new System.EventHandler(this.RastreamentoSRO_toolStripButton_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // VisualizarListaObjetos_toolStripButton
            // 
            this.VisualizarListaObjetos_toolStripButton.Image = global::SISAPO.Properties.Resources.VisualizarListaObjetos;
            this.VisualizarListaObjetos_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.VisualizarListaObjetos_toolStripButton.Name = "VisualizarListaObjetos_toolStripButton";
            this.VisualizarListaObjetos_toolStripButton.Size = new System.Drawing.Size(203, 22);
            this.VisualizarListaObjetos_toolStripButton.Text = "Visualizar lista de objeto(s) - [F12]";
            this.VisualizarListaObjetos_toolStripButton.Click += new System.EventHandler(this.VisualizarListaObjetos_toolStripButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 518);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(739, 26);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel.ForeColor = System.Drawing.SystemColors.Desktop;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(57, 21);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // timerAtualizacaoNovosRegistros
            // 
            this.timerAtualizacaoNovosRegistros.Tick += new System.EventHandler(this.timerAtualizacaoNovosRegistros_Tick);
            // 
            // atualizarNovosObjetosDestinatarioAusenteToolStripMenuItem
            // 
            this.atualizarNovosObjetosDestinatarioAusenteToolStripMenuItem.Name = "atualizarNovosObjetosDestinatarioAusenteToolStripMenuItem";
            this.atualizarNovosObjetosDestinatarioAusenteToolStripMenuItem.Size = new System.Drawing.Size(459, 22);
            this.atualizarNovosObjetosDestinatarioAusenteToolStripMenuItem.Text = "3 - Atualizar Novo(s) Objetos Destinatario Ausente";
            this.atualizarNovosObjetosDestinatarioAusenteToolStripMenuItem.Click += new System.EventHandler(this.atualizarNovosObjetosDestinatarioAusenteToolStripMenuItem_Click);
            // 
            // FormularioPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 544);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormularioPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SISAPO-SRO - Sistema de apoio ao rastreamento unificado";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormularioPrincipal_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormularioPrincipal_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripButton VisualizarListaObjetos_toolStripButton;
        private System.Windows.Forms.ToolStripButton RastreamentoSRO_toolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem sRORastreamentoUnificadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastrarNovosObjetosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atualizarNovosObjetosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visualizarListaDeObjetosToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opçõesToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem IncluirCaixaPostalPesquisa_toolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ExibirItensJaEntreguesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.Timer timerAtualizacaoNovosRegistros;
        private System.Windows.Forms.ToolStripMenuItem imprimirListaDeEntregaParaConsultaSelecionadaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem manterConsultaSempreAtualizadaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manterDataFinalEmHojeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem auxílioÀGestaoDoDiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exibirMensagensAoDesevolvedorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atualizarNovosObjetosPostadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atualizarNovosObjetosSaiuParaEntregaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atualizarNovosObjetosCompletoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marcarSelecionadosComoAtualizadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marcarSelecionadosComoNaoAtualizadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem solicitarVerificacaoDeObjetosAindaNaoEntreguesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem formularioAtualizacaoObjetosRequeridosJaEntreguesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atualizaNovosObjetosAguardandoRetiradaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removerLinhasSelecionadasToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem atualizarNovosObjetosDestinatarioAusenteToolStripMenuItem;
    }
}



