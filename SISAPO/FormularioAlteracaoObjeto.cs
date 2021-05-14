﻿using System;
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
    public partial class FormularioAlteracaoObjeto : Form
    {


        public string CodigoObjeto = string.Empty;
        public string NomeCliente = string.Empty;
        public string NumeroLDI = string.Empty;
        public string DataLancamento = string.Empty;
        public string Situacao = string.Empty;
        public string DataModificacao = string.Empty;
        public bool ObjetoEmCaixaPostal = false;
        public bool ObjetoJaEntregue = false;
        public bool ObjetoJaAtualizado = false;

        public string UnidadePostagem = string.Empty;
        public string MunicipioPostagem = string.Empty;
        public string CriacaoPostagem = string.Empty;
        public string CepDestinoPostagem = string.Empty;
        public string ARPostagem = string.Empty;
        public string MPPostagem = string.Empty;
        public string DataMaxPrevistaEntregaPostagem = string.Empty;

        public string UnidadeLOEC = string.Empty;
        public string MunicipioLOEC = string.Empty;
        public string CriacaoLOEC = string.Empty;
        public string CarteiroLOEC = string.Empty;
        public string DistritoLOEC = string.Empty;
        public string NumeroLOEC = string.Empty;
        public string EnderecoLOEC = string.Empty;
        public string BairroLOEC = string.Empty;
        public string LocalidadeLOEC = string.Empty;

        public string SituacaoDestinatarioAusente = string.Empty;
        public string AgrupadoDestinatarioAusente = string.Empty;
        public string CoordenadasDestinatarioAusente = string.Empty;

        public bool Cancelando = false;

        public FormularioAlteracaoObjeto()
        {
            Cancelando = false;
            InitializeComponent();

            TxtNomeCliente.Focus();
        }

        private void FormularioAlteracaoObjeto_Load(object sender, EventArgs e)
        {
            RetornaDadosPrincipal();
            RetornaDadosPostagem();
            RetornaDadosSaiuParaEntrega();
            RetornaDadosDestinatarioAusente();

            tabControlPrincipal.TabPages[0].Focus();
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            TxtNomeCliente.ScrollToCaret();
            TxtNomeCliente.ScrollToCaret();

            TxtNomeCliente.Select(TxtNomeCliente.Text.Length, 0);
        }

        private void RetornaDadosPrincipal()
        {
            TxtCodigoObjeto.Text = CodigoObjeto;
            TxtNomeCliente.Text = NomeCliente;
            TxtNumeroLDI.Text = NumeroLDI;
            TxtDataLancamento.Text = DataLancamento;
            TxtSituacao.Text = Situacao;
            TxtDataBaixa.Text = DataModificacao;
            checkBoxObjetoCaixaPostal.Checked = ObjetoEmCaixaPostal;
            checkBoxObjetoJaEntregue.Checked = ObjetoJaEntregue;
            checkBoxObjetoJaAtualizado.Checked = ObjetoJaAtualizado;
        }

        private void RetornaDadosPostagem()
        {
            TxtUnidadePostagem.Text = UnidadePostagem;
            TxtMunicipioPostagem.Text = MunicipioPostagem;
            TxtCriacaoPostagem.Text = CriacaoPostagem;
            //TxtCepDestinoPostagem.Text = CepDestinoPostagem;
            TxtCepDestinoPostagem.Text = CepDestinoPostagem.Length >= 8 ? string.Format("{0}{1}", CepDestinoPostagem.Substring(0, 5), CepDestinoPostagem.Substring(5, CepDestinoPostagem.Length - 5)) : CepDestinoPostagem;
            //TxtDataMaxPrevistaEntregaPostagem.Text = DataMaxPrevistaEntregaPostagem;
            DateTime dataValida; //Verifica Se a data for valida
            TxtDataMaxPrevistaEntregaPostagem.Text = (DateTime.TryParse(DataMaxPrevistaEntregaPostagem, out dataValida)) ?
                            DataMaxPrevistaEntregaPostagem.ToDateTime().GetDateTimeFormats()[14].ToUpper() : DataMaxPrevistaEntregaPostagem;
            if (string.IsNullOrEmpty(TxtDataMaxPrevistaEntregaPostagem.Text))
            {
                TxtDataMaxPrevistaEntregaPostagem.Text = "DADO INDISPONÍVEL";
            }
            TxtARPostagem.Text = ARPostagem;
            if (ARPostagem == "S")
                TxtARPostagem.Text = "SIM";
            if (ARPostagem == "N")
                TxtARPostagem.Text = "NÃO";

            TxtMPPostagem.Text = MPPostagem;
            if (MPPostagem == "S")
                TxtMPPostagem.Text = "SIM";
            if (MPPostagem == "N")
                TxtMPPostagem.Text = "NÃO";

            return;
        }

        private void RetornaDadosSaiuParaEntrega()
        {
            TxtUnidadeLOEC.Text = UnidadeLOEC;
            TxtMunicipioLOEC.Text = MunicipioLOEC;
            TxtCriacaoLOEC.Text = CriacaoLOEC;
            TxtCarteiroLOEC.Text = CarteiroLOEC;
            TxtDistritoLOEC.Text = DistritoLOEC;
            TxtNumeroLOEC.Text = NumeroLOEC;
            TxtEnderecoLOEC.Text = string.Format("{0} - {1} - {2} - {3}", EnderecoLOEC, BairroLOEC, MunicipioLOEC, LocalidadeLOEC);
        }

        private void RetornaDadosDestinatarioAusente()
        {
            TxtSituacaoDestinatarioAusente.Text = SituacaoDestinatarioAusente;
            TxtAgrupadoDestinatarioAusente.Text = AgrupadoDestinatarioAusente;
            TxtCoordenadasDestinatarioAusente.Text = CoordenadasDestinatarioAusente;
            TxtEnderecoCoordenadasDestinatarioAusente.Text = string.Format("https://www.google.com.br/maps/search/{0}", CoordenadasDestinatarioAusente);
            if (string.IsNullOrEmpty(CoordenadasDestinatarioAusente))
                TxtEnderecoCoordenadasDestinatarioAusente.Text = CoordenadasDestinatarioAusente;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            CodigoObjeto = TxtCodigoObjeto.Text;
            NomeCliente = TxtNomeCliente.Text.RemoveAcentos();
            NumeroLDI = TxtNumeroLDI.Text;
            DataLancamento = TxtDataLancamento.Text;
            Situacao = TxtSituacao.Text;
            DataModificacao = TxtDataBaixa.Text;
            ObjetoEmCaixaPostal = checkBoxObjetoCaixaPostal.Checked;
            ObjetoJaEntregue = checkBoxObjetoJaEntregue.Checked;
            ObjetoJaAtualizado = checkBoxObjetoJaAtualizado.Checked;

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelando = true;
            this.Close();
        }

        private void FormularioAlteracaoObjeto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                btnCancelar_Click(sender, e);
            }
            if(e.KeyCode == Keys.F5)
            {
                btnAlterar_Click(sender, e);
            }
        }

        private void BtnCoordenadas_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CoordenadasDestinatarioAusente)) return;

            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

            if (File.Exists(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            }
            else if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            }
            else if (File.Exists(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            }
            else if (File.Exists(@"C:\Program Files\Google\Chrome\Application\chrome.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            }
            else
            {
                return;
            }

            //pProcess.StartInfo.Arguments = "https://maps.google.com/maps?t=k&q=loc:-10.22285+-48.34052";
            pProcess.StartInfo.Arguments = string.Format("https://www.google.com.br/maps/search/{0}", CoordenadasDestinatarioAusente);
            pProcess.Start();
            //pProcess.WaitForExit();
        }

        private void BtnAlterarEndereco_Click(object sender, EventArgs e)
        {
            //TxtEnderecoLOEC.Text = string.Format("{0} - {1} - {2} - {3}", EnderecoLOEC, BairroLOEC, MunicipioLOEC, LocalidadeLOEC);
            //string EnderecoDestino = EnderecoLOEC;
            //string BairroDestino = BairroLOEC;
            //string municipio[] = MunicipioLOEC.Split('/')[0].Trim();

            string MunicipioDestino = MunicipioLOEC.Contains("/") ? MunicipioLOEC.Split('/')[0].Trim() : "";
            string EstadoDestino = MunicipioLOEC.Contains("/") ? MunicipioLOEC.Split('/')[1].Trim() : "";
            //string CepDestino = LocalidadeLOEC;
            //string CodigoObjetoDestino = CodigoObjeto;
            //string NomeClienteDestino = NomeCliente;
            //string NumeroLDIDestino = NumeroLDI;

            FormularioAlteracaoEnderecoObjeto formularioAlteracaoEnderecoObjeto = new FormularioAlteracaoEnderecoObjeto();
            formularioAlteracaoEnderecoObjeto.TxtCodigoObjeto.Text = CodigoObjeto;
            formularioAlteracaoEnderecoObjeto.TxtNomeCliente.Text = NomeCliente;
            formularioAlteracaoEnderecoObjeto.TxtNumeroLDI.Text = NumeroLDI;
            formularioAlteracaoEnderecoObjeto.TxtEndereco.Text = EnderecoLOEC;
            formularioAlteracaoEnderecoObjeto.TxtBairro.Text = BairroLOEC;
            formularioAlteracaoEnderecoObjeto.TxtCidade.Text = MunicipioDestino;
            formularioAlteracaoEnderecoObjeto.TxtUF.Text = EstadoDestino;
            formularioAlteracaoEnderecoObjeto.TxtCep.Text = LocalidadeLOEC;

            formularioAlteracaoEnderecoObjeto.ShowDialog();

            if (formularioAlteracaoEnderecoObjeto.cancelar) return;//não faz nada. Cancelou

            EnderecoLOEC = formularioAlteracaoEnderecoObjeto.TxtEndereco.Text;
            BairroLOEC = formularioAlteracaoEnderecoObjeto.TxtBairro.Text;
            MunicipioDestino = formularioAlteracaoEnderecoObjeto.TxtCidade.Text;
            EstadoDestino = formularioAlteracaoEnderecoObjeto.TxtUF.Text;
            MunicipioLOEC = MunicipioDestino + " / " + EstadoDestino;
            LocalidadeLOEC = formularioAlteracaoEnderecoObjeto.TxtCep.Text;

            TxtEnderecoLOEC.Text = string.Format("{0} - {1} - {2} - {3}", EnderecoLOEC, BairroLOEC, MunicipioDestino + " / " + EstadoDestino, LocalidadeLOEC);
            TxtMunicipioLOEC.Text = MunicipioLOEC;
        }
    }
}
