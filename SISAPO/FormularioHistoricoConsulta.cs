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
    public partial class FormularioHistoricoConsulta : Form
    {
        public string CodigoRetorno = string.Empty;

        public FormularioHistoricoConsulta()
        {
            InitializeComponent();
            CodigoRetorno = "";            
        }

        private void FormularioHistoricoConsulta_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = RetornaListaUltimasConsultas();
            listBox1.Focus();
            listBox1.SelectedIndex = 0;

        }

        private List<string> RetornaListaUltimasConsultas()
        {
            if (!DAO.TestaConexao(ClassesDiversas.Configuracoes.strConexao, TipoBanco.OleDb))
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao;
                return null;
            }
            DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao);

            DataSet ds = dao.RetornaDataSet(@"SELECT Top 50 CodigoObjeto, DataConsulta FROM TabelaHistoricoConsulta ORDER BY DataConsulta DESC");
            List<string> lista = new List<string>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                //DY123456789BR - 03-05-2019 09:35:45
                lista.Add(string.Format("{0} - {1}", item["CodigoObjeto"], item["DataConsulta"]));
            }

            return lista;
        }

        private void FormularioHistoricoConsulta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //e.SuppressKeyPress = true;
                if (listBox1.SelectedItem.ToString().Length <= 13)
                    CodigoRetorno = CodigoRetorno = listBox1.SelectedItem.ToString().Substring(0, listBox1.SelectedItem.ToString().Length);
                else
                    CodigoRetorno = listBox1.SelectedItem.ToString().Substring(0, 13);
                this.Close();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem.ToString().Length <= 13)
                CodigoRetorno = CodigoRetorno = listBox1.SelectedItem.ToString().Substring(0, listBox1.SelectedItem.ToString().Length);
            else
                CodigoRetorno = listBox1.SelectedItem.ToString().Substring(0, 13);
            this.Close();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem.ToString().Length <= 13)
                CodigoRetorno = CodigoRetorno = listBox1.SelectedItem.ToString().Substring(0, listBox1.SelectedItem.ToString().Length);
            else
                CodigoRetorno = listBox1.SelectedItem.ToString().Substring(0, 13);
            this.Close();
        }


    }
}
