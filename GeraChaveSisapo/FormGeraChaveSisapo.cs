using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace GeraChaveSisapo
{
    public partial class FormGeraChaveSisapo : Form
    {
        public FormGeraChaveSisapo()
        {
            InitializeComponent();
        }

        private void FormGeraChaveSisapo_Load(object sender, EventArgs e)
        {
            Data_dateTimePicker.Focus();
        }

        private void BtnAcesso_Click(object sender, EventArgs e)
        {

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormGeraChaveSisapo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void BtnGerarAcesso_Click(object sender, EventArgs e)
        {
            //TxtAcesso.Text = CriptografiaHelper.Criptografa("30-09-2019");
            TxtAcesso.Text = CriptografiaHelper.Criptografa(Convert.ToDateTime(Data_dateTimePicker.Text).ToString("dd-MM-yyyy"));
            string DataRetornadaBancoDados = CriptografiaHelper.Descriptografa(TxtAcesso.Text);
        }

        private void BtnCopiarFechar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtAcesso.Text))
            {
                Clipboard.SetText(TxtAcesso.Text);
            }

            var processo = System.Diagnostics.Process.GetCurrentProcess();
            foreach (Process itens in System.Diagnostics.Process.GetProcessesByName(processo.ProcessName).Where(p => p.Id != processo.Id))
            {
                itens.Kill();
            }
            foreach (Process itens in Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName))
            {
                itens.Kill();
            }
        }

        private void TxtAcesso_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.KeyCode == Keys.Enter)
            //{
            //    e.SuppressKeyPress = true;
            //    BtnCopiarFechar_Click(sender, e);
            //}
        }


    }
}
