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
        public string Comentario = string.Empty;

        //public string UnidadePostagem = string.Empty;
        //public string MunicipioPostagem = string.Empty;
        //public string CriacaoPostagem = string.Empty;
        //public string CepDestinoPostagem = string.Empty;
        //public string ARPostagem = string.Empty;
        //public string MPPostagem = string.Empty;
        //public string DataMaxPrevistaEntregaPostagem = string.Empty;

        //public string UnidadeLOEC = string.Empty;
        public string MunicipioLOEC = string.Empty;
        //public string CriacaoLOEC = string.Empty;
        //public string CarteiroLOEC = string.Empty;
        //public string DistritoLOEC = string.Empty;
        //public string NumeroLOEC = string.Empty;
        public string EnderecoLOEC = string.Empty;
        public string BairroLOEC = string.Empty;
        public string LocalidadeLOEC = string.Empty;

        //public string SituacaoDestinatarioAusente = string.Empty;
        //public string AgrupadoDestinatarioAusente = string.Empty;
        public string CoordenadasDestinatarioAusente = string.Empty;

        public bool Cancelando = false;
        private bool clicouEmAlterar = false;

        public FormularioAlteracaoObjeto()
        {
            Cancelando = false;
            clicouEmAlterar = false;
            InitializeComponent();
        }

        private void FormularioAlteracaoObjeto_Load(object sender, EventArgs e)
        {
            DataRow dr = RetornaDadosBanco(CodigoObjeto);
            RetornaDadosPrincipal(dr);
            RetornaDadosPostagem(dr);
            RetornaDadosSaiuParaEntrega(dr);
            RetornaDadosDestinatarioAusente(dr);
            CarregaComboBoxComentatio();

            tabControlPrincipal.TabPages[0].Focus();
            //SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            TxtNomeCliente.ScrollToCaret();
            TxtNomeCliente.ScrollToCaret();

            TxtNomeCliente.Select(TxtNomeCliente.Text.Length, 0);
        }

        private DataRow RetornaDadosBanco(string codigoObjeto)
        {
            DataRow dataRow = null;
            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return null; }

                    List<Parametros> pr = new List<Parametros>() {
                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjeto }
                    };

                    dataRow = dao.RetornaDataSet(
                    @"SELECT  Codigo, CodigoObjeto, CodigoLdi, NomeCliente, DataLancamento, DataModificacao, Situacao, Atualizado, ObjetoEntregue, CaixaPostal, UnidadePostagem, MunicipioPostagem, CriacaoPostagem, CepDestinoPostagem, ARPostagem, MPPostagem, DataMaxPrevistaEntregaPostagem, UnidadeLOEC, MunicipioLOEC, CriacaoLOEC, CarteiroLOEC, DistritoLOEC, NumeroLOEC, EnderecoLOEC, BairroLOEC, LocalidadeLOEC, SituacaoDestinatarioAusente, AgrupadoDestinatarioAusente, CoordenadasDestinatarioAusente, Comentario, TipoPostalServico, TipoPostalSiglaCodigo, TipoPostalNomeSiglaCodigo, TipoPostalPrazoDiasCorridosRegulamentado
                        FROM TabelaObjetosSROLocal WHERE CodigoObjeto IN(@CodigoObjeto)", pr).Tables[0].Rows[0];
                }
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
            }
            return dataRow;
        }

        private void RetornaDadosPrincipal(DataRow dr)
        {
            TxtCodigoObjeto.Text = dr["CodigoObjeto"].ToString();
            TxtNomeCliente.Text = dr["NomeCliente"].ToString();
            TxtNumeroLDI.Text = dr["CodigoLdi"].ToString();
            TxtDataLancamento.Text = dr["DataLancamento"].ToString();
            TxtSituacao.Text = dr["Situacao"].ToString();
            TxtDataBaixa.Text = dr["DataModificacao"].ToString();
            checkBoxObjetoCaixaPostal.Checked = Convert.ToBoolean(dr["CaixaPostal"]);
            checkBoxObjetoJaEntregue.Checked = Convert.ToBoolean(dr["ObjetoEntregue"]);
            checkBoxObjetoJaAtualizado.Checked = Convert.ToBoolean(dr["Atualizado"]);
            comboBoxComentario.SelectedText = dr["Comentario"].ToString();
            checkBoxAoRemetente.Checked = Configuracoes.RetornaSeEAoRemetente(dr["NomeCliente"].ToString());
        }

        private void CarregaComboBoxComentatio()
        {
            //List<string> ListaItensMotivoBaixa = new List<string>();

            //ListaItensMotivoBaixa = new List<string>();
            //ListaItensMotivoBaixa.Add("PCT");
            //ListaItensMotivoBaixa.Add("PCT INT");
            //ListaItensMotivoBaixa.Add("ENV");

            //ListaItensMotivoBaixa.Add("PCT AO REMETENTE");
            //ListaItensMotivoBaixa.Add("PCT INT AO REMETENTE");
            //ListaItensMotivoBaixa.Add("ENV AO REMETENTE");

            //ListaItensMotivoBaixa.Add("PCT TERMO CONSTATACAO");
            //ListaItensMotivoBaixa.Add("PCT INT TERMO CONSTATACAO");
            //ListaItensMotivoBaixa.Add("ENV TERMO CONSTATACAO");


            //comboBoxComentario.DataSource = ListaItensMotivoBaixa;
        }

        private void RetornaDadosPostagem(DataRow dr)
        {
            TxtUnidadePostagem.Text = dr["UnidadePostagem"].ToString();
            TxtMunicipioPostagem.Text = dr["MunicipioPostagem"].ToString();
            TxtCriacaoPostagem.Text = dr["CriacaoPostagem"].ToString();
            //TxtCepDestinoPostagem.Text = dr["CepDestinoPostagem"].ToString();
            string CepDestinoPostagem = dr["CepDestinoPostagem"].ToString();
            TxtCepDestinoPostagem.Text = CepDestinoPostagem.Length >= 8 ? string.Format("{0}{1}", CepDestinoPostagem.Substring(0, 5), CepDestinoPostagem.Substring(5, CepDestinoPostagem.Length - 5)) : CepDestinoPostagem;
            //TxtDataMaxPrevistaEntregaPostagem.Text = dr["DataMaxPrevistaEntregaPostagem"].ToString(;
            DateTime dataValida; //Verifica Se a data for valida
            TxtDataMaxPrevistaEntregaPostagem.Text = (DateTime.TryParse(dr["DataMaxPrevistaEntregaPostagem"].ToString(), out dataValida)) ?
                            dr["DataMaxPrevistaEntregaPostagem"].ToDateTime().GetDateTimeFormats()[14].ToUpper() : dr["DataMaxPrevistaEntregaPostagem"].ToString();
            if (string.IsNullOrEmpty(TxtDataMaxPrevistaEntregaPostagem.Text))
            {
                TxtDataMaxPrevistaEntregaPostagem.Text = "DADO INDISPONÍVEL";
            }
            TxtARPostagem.Text = dr["ARPostagem"].ToString();
            if (dr["ARPostagem"].ToString() == "S")
                TxtARPostagem.Text = "SIM";
            if (dr["ARPostagem"].ToString() == "N")
                TxtARPostagem.Text = "NÃO";

            TxtMPPostagem.Text = dr["MPPostagem"].ToString();
            if (dr["MPPostagem"].ToString() == "S")
                TxtMPPostagem.Text = "SIM";
            if (dr["MPPostagem"].ToString() == "N")
                TxtMPPostagem.Text = "NÃO";

            return;
        }

        private void RetornaDadosSaiuParaEntrega(DataRow dr)
        {
            TxtUnidadeLOEC.Text = dr["UnidadeLOEC"].ToString();
            TxtMunicipioLOEC.Text = dr["MunicipioLOEC"].ToString();
            TxtCriacaoLOEC.Text = dr["CriacaoLOEC"].ToString();
            TxtCarteiroLOEC.Text = dr["CarteiroLOEC"].ToString();
            TxtDistritoLOEC.Text = dr["DistritoLOEC"].ToString();
            TxtNumeroLOEC.Text = dr["NumeroLOEC"].ToString();
            TxtEnderecoLOEC.Text = string.Format("{0} - {1} - {2} - {3}", dr["EnderecoLOEC"].ToString(), dr["BairroLOEC"].ToString(), dr["MunicipioLOEC"].ToString(), dr["LocalidadeLOEC"].ToString());

            TxtEndereco.Text = dr["EnderecoLOEC"].ToString();
            TxtBairro.Text = dr["BairroLOEC"].ToString();
            TxtCidade.Text = dr["MunicipioLOEC"].ToString().Contains(" / ") ? dr["MunicipioLOEC"].ToString().Split('/')[0].Trim() : "";
            TxtUF.Text = dr["MunicipioLOEC"].ToString().Contains(" / ") ? dr["MunicipioLOEC"].ToString().Split('/')[1].Trim() : "";
            TxtCep.Text = dr["LocalidadeLOEC"].ToString();
        }

        private void RetornaDadosDestinatarioAusente(DataRow dr)
        {
            TxtSituacaoDestinatarioAusente.Text = dr["SituacaoDestinatarioAusente"].ToString();
            TxtAgrupadoDestinatarioAusente.Text = dr["AgrupadoDestinatarioAusente"].ToString();
            TxtCoordenadasDestinatarioAusente.Text = dr["CoordenadasDestinatarioAusente"].ToString();
            TxtEnderecoCoordenadasDestinatarioAusente.Text = string.Format("https://www.google.com.br/maps/search/{0}", dr["CoordenadasDestinatarioAusente"].ToString());
            if (string.IsNullOrEmpty(dr["CoordenadasDestinatarioAusente"].ToString()))
                TxtEnderecoCoordenadasDestinatarioAusente.Text = dr["CoordenadasDestinatarioAusente"].ToString();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //if(string.IsNullOrWhiteSpace(TxtNomeCliente))


            clicouEmAlterar = true;
            Cancelando = false;
            CodigoObjeto = TxtCodigoObjeto.Text.ToUpper();
            NomeCliente = TxtNomeCliente.Text.RemoveAcentos().ToUpper();
            NumeroLDI = TxtNumeroLDI.Text.ToUpper();
            DataLancamento = TxtDataLancamento.Text.ToUpper();
            Situacao = TxtSituacao.Text.ToUpper();
            DataModificacao = TxtDataBaixa.Text.ToUpper();
            ObjetoEmCaixaPostal = checkBoxObjetoCaixaPostal.Checked;
            ObjetoJaEntregue = checkBoxObjetoJaEntregue.Checked;
            ObjetoJaAtualizado = checkBoxObjetoJaAtualizado.Checked;
            Comentario = comboBoxComentario.Text.RemoveAcentos().ToUpper();

            EnderecoLOEC = TxtEndereco.Text.RemoveAcentos().ToUpper();
            BairroLOEC = TxtBairro.Text.RemoveAcentos().ToUpper();
            MunicipioLOEC = string.Format("{0} / {1}", TxtCidade.Text.RemoveAcentos().ToUpper(), TxtUF.Text.RemoveAcentos().ToUpper());
            //TxtUF.Text = MunicipioLOEC.Contains("/") ? MunicipioLOEC.Split('/')[1].Trim() : "";
            LocalidadeLOEC = TxtCep.Text.RemoveAcentos().ToUpper();

            #region TiposPostais
            bool SeEAoRemetente = checkBoxAoRemetente.Checked;

            string TipoPostalServico = string.Empty;
            string TipoPostalSiglaCodigo = string.Empty;
            string TipoPostalNomeSiglaCodigo = string.Empty;
            string TipoPostalPrazoDiasCorridosRegulamentado = string.Empty;

            TipoPostalPrazoDiasCorridosRegulamentado = Configuracoes.RetornaTipoPostalPrazoDiasCorridosRegulamentado(CodigoObjeto, SeEAoRemetente, ObjetoEmCaixaPostal, ref TipoPostalServico, ref TipoPostalSiglaCodigo, ref TipoPostalNomeSiglaCodigo);
            if (string.IsNullOrEmpty(TipoPostalPrazoDiasCorridosRegulamentado))
            {
                Mensagens.Erro(string.Format("Não foi encontrado o Tipo Postal [ {0} ].\nUma gestão de tipos postais é necessário.", CodigoObjeto.Substring(0, 2)));
                //continua mesmo não tendo o tipo postal desejado....
            }

            //currentRow["TipoPostalServico"] = TipoPostalServico;//string TipoPostalServico,
            //currentRow["TipoPostalSiglaCodigo"] = TipoPostalSiglaCodigo;//string TipoPostalSiglaCodigo,
            //currentRow["TipoPostalNomeSiglaCodigo"] = TipoPostalNomeSiglaCodigo;//string TipoPostalNomeSiglaCodigo,
            //currentRow["TipoPostalPrazoDiasCorridosRegulamentado"] = TipoPostalPrazoDiasCorridosRegulamentado;//string TipoPostalPrazoDiasCorridosRegulamentado,

            #endregion

            try
            {
                using (DAO dao = new DAO(TipoBanco.OleDb, Configuracoes.strConexao))
                {
                    if (!dao.TestaConexao()) { FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao; return; }

                    List<Parametros> pr = new List<Parametros>() {
                        new Parametros() { Nome = "@NomeCliente", Tipo = TipoCampo.Text, Valor = NomeCliente },
                        new Parametros() { Nome = "@CodigoLdi", Tipo = TipoCampo.Text, Valor = NumeroLDI },
                        new Parametros() { Nome = "@DataLancamento", Tipo = TipoCampo.Text, Valor = DataLancamento },
                        new Parametros() { Nome = "@Comentario", Tipo = TipoCampo.Text, Valor = Comentario },
                        new Parametros() { Nome = "@CaixaPostal", Tipo = TipoCampo.Boolean, Valor = ObjetoEmCaixaPostal },
                        new Parametros() { Nome = "@ObjetoEntregue", Tipo = TipoCampo.Boolean, Valor = ObjetoJaEntregue },
                        new Parametros() { Nome = "@Atualizado", Tipo = TipoCampo.Boolean, Valor = ObjetoJaAtualizado },
                        new Parametros() { Nome = "@EnderecoLOEC", Tipo = TipoCampo.Text, Valor = EnderecoLOEC },
                        new Parametros() { Nome = "@BairroLOEC", Tipo = TipoCampo.Text, Valor = BairroLOEC },
                        new Parametros() { Nome = "@MunicipioLOEC", Tipo = TipoCampo.Text, Valor = MunicipioLOEC },
                        new Parametros() { Nome = "@LocalidadeLOEC", Tipo = TipoCampo.Text, Valor = LocalidadeLOEC },
                        new Parametros() { Nome = "@TipoPostalServico", Tipo = TipoCampo.Text, Valor = TipoPostalServico },
                        new Parametros() { Nome = "@TipoPostalSiglaCodigo", Tipo = TipoCampo.Text, Valor = TipoPostalSiglaCodigo },
                        new Parametros() { Nome = "@TipoPostalNomeSiglaCodigo", Tipo = TipoCampo.Text, Valor = TipoPostalNomeSiglaCodigo },
                        new Parametros() { Nome = "@TipoPostalPrazoDiasCorridosRegulamentado", Tipo = TipoCampo.Text, Valor = TipoPostalPrazoDiasCorridosRegulamentado },

                        new Parametros() { Nome = "@CodigoObjeto", Tipo = TipoCampo.Text, Valor = CodigoObjeto }
                    };

                    dao.ExecutaSQL(
                    @"UPDATE TabelaObjetosSROLocal SET
                    NomeCliente = @NomeCliente, 
                    CodigoLdi = @CodigoLdi, 
                    DataLancamento = @DataLancamento, 
                    Comentario = @Comentario, 
                    CaixaPostal = @CaixaPostal, 
                    ObjetoEntregue = @ObjetoEntregue, 
                    Atualizado = @Atualizado, 
                    EnderecoLOEC = @EnderecoLOEC, 
                    BairroLOEC = @BairroLOEC, 
                    MunicipioLOEC = @MunicipioLOEC,
                    LocalidadeLOEC = @LocalidadeLOEC, 
                    TipoPostalServico = @TipoPostalServico,
                    TipoPostalSiglaCodigo = @TipoPostalSiglaCodigo, 
                    TipoPostalNomeSiglaCodigo = @TipoPostalNomeSiglaCodigo, 
                    TipoPostalPrazoDiasCorridosRegulamentado = @TipoPostalPrazoDiasCorridosRegulamentado
                    WHERE(CodigoObjeto = @CodigoObjeto)", pr);
                }
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
            }
            finally
            {
                this.Close();
            }
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
            if (e.KeyCode == Keys.F5)
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

            TxtEndereco.Text = EnderecoLOEC = formularioAlteracaoEnderecoObjeto.TxtEndereco.Text;
            TxtBairro.Text = BairroLOEC = formularioAlteracaoEnderecoObjeto.TxtBairro.Text;
            TxtCidade.Text = MunicipioDestino = formularioAlteracaoEnderecoObjeto.TxtCidade.Text;
            TxtUF.Text = EstadoDestino = formularioAlteracaoEnderecoObjeto.TxtUF.Text;
            MunicipioLOEC = MunicipioDestino + " / " + EstadoDestino;
            TxtCep.Text = LocalidadeLOEC = formularioAlteracaoEnderecoObjeto.TxtCep.Text;

            TxtEnderecoLOEC.Text = string.Format("{0} - {1} - {2} - {3}", EnderecoLOEC, BairroLOEC, MunicipioDestino + " / " + EstadoDestino, LocalidadeLOEC);
            TxtMunicipioLOEC.Text = MunicipioLOEC;
        }

        private void FormularioAlteracaoObjeto_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (clicouEmAlterar)
                Cancelando = false;
            if (!clicouEmAlterar)
                Cancelando = true;
        }
    }
}
