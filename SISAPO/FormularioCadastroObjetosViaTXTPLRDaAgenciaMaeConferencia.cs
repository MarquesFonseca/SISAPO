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
    public partial class FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferencia : Form
    {
        DataTable dtListaObjetosTXT = new DataTable();
        DataTable dtListaObjetosConferencia = new DataTable();
        int itemConferencia = 1;

        int TempLblQtdTotalSemPLR = 0;
        int TempLblQdtTotalComPLR = 0;

        int TempLblQtdTotalDePLR = 0;
        int TempLblQtdTotalItensEmPLRs = 0;
        int TempLblTotalValidados = 0;
        int TempLblQtdTotalFaltantes = 0;

        string PastaEnderecoInicialArquivo = "C:\\";

        public FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferencia()
        {
            InitializeComponent();
        }

        private void FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferencia_Load(object sender, EventArgs e)
        {
            dtListaObjetosTXT = CriaDataTableListaObjetosTXT();
            dtListaObjetosConferencia = CriaDataTableListaObjetosConferencia();

            LblLinkVisualizarObjetosFaltantes.Visible = false;
        }

        private DataTable CriaDataTableListaObjetosTXT()
        {
            dtListaObjetosTXT = new DataTable();
            dtListaObjetosTXT.Clear();
            dtListaObjetosTXT.Columns.Add("CodigoObjeto", typeof(string));
            dtListaObjetosTXT.Columns.Add("CodigoLdi", typeof(string));
            dtListaObjetosTXT.Columns.Add("NomeCliente", typeof(string));
            dtListaObjetosTXT.Columns.Add("DataLancamento", typeof(string));
            dtListaObjetosTXT.Columns.Add("DataModificacao", typeof(string));
            dtListaObjetosTXT.Columns.Add("Situacao", typeof(string));
            dtListaObjetosTXT.Columns.Add("Atualizado", typeof(string));
            dtListaObjetosTXT.Columns.Add("ObjetoEntregue", typeof(string));
            dtListaObjetosTXT.Columns.Add("CaixaPostal", typeof(string));
            dtListaObjetosTXT.Columns.Add("UnidadePostagem", typeof(string));
            dtListaObjetosTXT.Columns.Add("MunicipioPostagem", typeof(string));
            dtListaObjetosTXT.Columns.Add("CriacaoPostagem", typeof(string));
            dtListaObjetosTXT.Columns.Add("CepDestinoPostagem", typeof(string));
            dtListaObjetosTXT.Columns.Add("ARPostagem", typeof(string));
            dtListaObjetosTXT.Columns.Add("MPPostagem", typeof(string));
            dtListaObjetosTXT.Columns.Add("DataMaxPrevistaEntregaPostagem", typeof(string));
            dtListaObjetosTXT.Columns.Add("UnidadeLOEC", typeof(string));
            dtListaObjetosTXT.Columns.Add("MunicipioLOEC", typeof(string));
            dtListaObjetosTXT.Columns.Add("CriacaoLOEC", typeof(string));
            dtListaObjetosTXT.Columns.Add("CarteiroLOEC", typeof(string));
            dtListaObjetosTXT.Columns.Add("DistritoLOEC", typeof(string));
            dtListaObjetosTXT.Columns.Add("NumeroLOEC", typeof(string));
            dtListaObjetosTXT.Columns.Add("EnderecoLOEC", typeof(string));
            dtListaObjetosTXT.Columns.Add("BairroLOEC", typeof(string));
            dtListaObjetosTXT.Columns.Add("LocalidadeLOEC", typeof(string));
            dtListaObjetosTXT.Columns.Add("SituacaoDestinatarioAusente", typeof(string));
            dtListaObjetosTXT.Columns.Add("AgrupadoDestinatarioAusente", typeof(string));
            dtListaObjetosTXT.Columns.Add("CoordenadasDestinatarioAusente", typeof(string));
            dtListaObjetosTXT.Columns.Add("Comentario", typeof(string));
            dtListaObjetosTXT.Columns.Add("TipoPostalServico", typeof(string));
            dtListaObjetosTXT.Columns.Add("TipoPostalSiglaCodigo", typeof(string));
            dtListaObjetosTXT.Columns.Add("TipoPostalNomeSiglaCodigo", typeof(string));
            dtListaObjetosTXT.Columns.Add("TipoPostalPrazoDiasCorridosRegulamentado", typeof(string));
            dtListaObjetosTXT.Columns.Add("DataListaAtual", typeof(string));
            dtListaObjetosTXT.Columns.Add("NumeroListaAtual", typeof(string));
            dtListaObjetosTXT.Columns.Add("ItemAtual", typeof(int));
            dtListaObjetosTXT.Columns.Add("QtdTotal", typeof(int));

            return dtListaObjetosTXT;
        }

        private DataTable CriaDataTableListaObjetosConferencia()
        {
            dtListaObjetosConferencia = new DataTable();
            dtListaObjetosConferencia.Clear();
            dtListaObjetosConferencia.Columns.Add("Item");
            dtListaObjetosConferencia.Columns.Add("CodigoObjeto");
            dtListaObjetosConferencia.Columns.Add("Resultado");

            return dtListaObjetosConferencia;
        }

        //private void BtnLerArquivoPLR_Click(object sender, EventArgs e)
        //{
        //    //string curDirTemp = System.IO.Path.GetTempPath();
        //    var filePath = string.Empty;

        //    using (OpenFileDialog openFileDialog = new OpenFileDialog())
        //    {
        //        openFileDialog.InitialDirectory = PastaEnderecoInicialArquivo;
        //        openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        //        openFileDialog.FilterIndex = 2;
        //        openFileDialog.RestoreDirectory = true;

        //        if (openFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            //Get the path of specified file
        //            filePath = openFileDialog.FileName;
        //            PastaEnderecoInicialArquivo = System.IO.Path.GetDirectoryName(filePath);
        //        }
        //    }

        //    TxtEnderecoPLR.Text = filePath;





        //    string NumeroPLR = string.Empty;
        //    string ClienteRemetente = string.Empty;
        //    string Contrato = string.Empty;
        //    string CodigoAdministrativo = string.Empty;
        //    string CartaoAdministrativo = string.Empty;
        //    string EnderecoRemetente = string.Empty;
        //    string CodigoObjeto = string.Empty;
        //    string Destinatario = string.Empty;
        //    string CepDestino = string.Empty;

        //    if (string.IsNullOrWhiteSpace(TxtEnderecoPLR.Text))
        //    {
        //        MessageBox.Show("Informe o relatório de finalização de lançamento de objetos pelo sara em PLR");
        //        return;
        //    }

        //    string endereco = @"" + TxtEnderecoPLR.Text;
        //    endereco = endereco.Replace("file:///", "");
        //    endereco = endereco.Replace("/", "\\");

        //    //ConvertePDF pdftxt = new ConvertePDF();
        //    //List<string> linhas = pdftxt.ExtrairTexto_PDF_List(@endereco);
        //    List<string> linhas = new List<string>();

        //    if (linhas.AsEnumerable().Any(M => M.Contains("LISTA DE POSTAGEM")) == false)
        //    {
        //        MessageBox.Show("Informe o relatório de finalização de lançamento de objetos pelo sara em PLR");
        //        return;
        //    }

        //    NumeroPLR = linhas[3].ToUpper();
        //    NumeroPLR = NumeroPLR.Replace("Nº DA LISTA:", "").Trim();
        //    NumeroPLR = NumeroPLR.Remove(NumeroPLR.IndexOf("REMETENTE:"), NumeroPLR.Length - NumeroPLR.IndexOf("REMETENTE:")).Trim();
        //    bool existe = dtListaObjetosPLR.AsEnumerable().Any(T => T["NumeroPLR"].ToString().Contains(NumeroPLR));
        //    if (existe)
        //    {
        //        MessageBox.Show(string.Format("A PLR '{0}' já existe.\nInforme outra PLR.", NumeroPLR));
        //        return;
        //    }

        //    ClienteRemetente = linhas[3].ToUpper();
        //    ClienteRemetente = ClienteRemetente.Remove(0, ClienteRemetente.IndexOf("REMETENTE:") + 11);
        //    ClienteRemetente = ClienteRemetente.Remove(ClienteRemetente.IndexOf("TELEFONE:"), ClienteRemetente.Length - ClienteRemetente.IndexOf("TELEFONE:")).Trim();

        //    Contrato = linhas[4].ToUpper();
        //    Contrato = Contrato.Replace("CONTRATO:", "").Trim();
        //    Contrato = Contrato.Remove(Contrato.IndexOf("CLIENTE:"), Contrato.Length - Contrato.IndexOf("CLIENTE:")).Trim();

        //    CodigoAdministrativo = linhas[5].ToUpper();
        //    CodigoAdministrativo = CodigoAdministrativo.Replace("CODIGO ADM.:", "").Trim();
        //    CodigoAdministrativo = CodigoAdministrativo.Remove(CodigoAdministrativo.IndexOf("ENDERECO:"), CodigoAdministrativo.Length - CodigoAdministrativo.IndexOf("ENDERECO:")).Trim();

        //    CartaoAdministrativo = linhas[6].ToUpper();
        //    CartaoAdministrativo = CartaoAdministrativo.Replace("CARTÃO:", "").Trim();
        //    CartaoAdministrativo = CartaoAdministrativo.Substring(0, 11).Trim();

        //    EnderecoRemetente = linhas[5].ToUpper();
        //    EnderecoRemetente = EnderecoRemetente.Substring(EnderecoRemetente.IndexOf("ENDERECO:"), EnderecoRemetente.Length - EnderecoRemetente.IndexOf("ENDERECO:")).Trim();
        //    EnderecoRemetente = EnderecoRemetente.Replace("ENDERECO:", "").Trim();

        //    string EnderecoRemetenteParte2 = linhas[6].ToUpper();
        //    EnderecoRemetenteParte2 = EnderecoRemetenteParte2.Replace("CARTÃO:", "").Trim();
        //    EnderecoRemetenteParte2 = EnderecoRemetenteParte2.Replace(CartaoAdministrativo, "").Trim();

        //    EnderecoRemetente = string.Format("{0} {1}", EnderecoRemetente, EnderecoRemetenteParte2);

        //    int contador = 1;
        //    int Tbitem = 1;
        //    for (int i = 10; i < linhas.Count(); i++)
        //    {
        //        string linha = linhas[i];
        //        if (linhas[i].Contains("Quantidade de objetos:"))
        //        {
        //            contador = 0;
        //            break;
        //        }
        //        if (contador == 1)
        //        {
        //            if (linha.Length == 3) continue;
        //            //Pega Código
        //            CodigoObjeto = linhas[i];
        //            CodigoObjeto = CodigoObjeto.Substring(0, 13).Trim().ToUpper();
        //            CepDestino = linhas[i];
        //            CepDestino = CepDestino.Remove(0, 13).Trim();
        //            CepDestino = CepDestino.Substring(0, 9).Trim();
        //            CepDestino = string.Format("{0}-{1}", CepDestino.Substring(0, 5), CepDestino.Substring(5, 3));
        //            contador++;
        //            continue;
        //        }
        //        if (contador == 2)
        //        {
        //            //Pega Destinatario
        //            Destinatario = linhas[i];
        //            Destinatario = Destinatario.Replace("Destinatário:", "").Trim().ToUpper();
        //            contador = 1;
        //            //grava dataTable
        //            //cria linha
        //            DataRow linhaDtListaObjetos = dtListaObjetosPLR.NewRow();
        //            linhaDtListaObjetos["Item"] = Tbitem++;
        //            linhaDtListaObjetos["NumeroPLR"] = NumeroPLR;
        //            linhaDtListaObjetos["ClienteRemetente"] = ClienteRemetente;
        //            linhaDtListaObjetos["Contrato"] = Contrato;
        //            linhaDtListaObjetos["CodigoAdministrativo"] = CodigoAdministrativo;
        //            linhaDtListaObjetos["CartaoAdministrativo"] = CartaoAdministrativo;
        //            linhaDtListaObjetos["EnderecoRemetente"] = EnderecoRemetente;
        //            linhaDtListaObjetos["CodigoObjeto"] = CodigoObjeto;
        //            linhaDtListaObjetos["Destinatario"] = Destinatario;
        //            linhaDtListaObjetos["CepDestino"] = CepDestino;
        //            dtListaObjetosPLR.Rows.Add(linhaDtListaObjetos);
        //            dataGridView1.DataSource = dtListaObjetosPLR;

        //            int QtdTotalPLRs = dtListaObjetosPLR.AsEnumerable().GroupBy(T => T["NumeroPLR"]).Count();
        //            TempLblQtdTotalDePLRs = QtdTotalPLRs;
        //            TempLblQtdTotalItensEmPLRs = dtListaObjetosPLR.Rows.Count;

        //            bool existeNaListaConferencia = dtListaObjetosConferencia.AsEnumerable().Any(T => T["CodigoObjeto"].ToString().Contains(CodigoObjeto));
        //            if (existeNaListaConferencia)
        //            {
        //                TempLblTotalValidados = 0;
        //                foreach (DataGridViewRow row in dataGridView1.Rows)
        //                {
        //                    string codigoLinha = row.Cells["CodigoObjeto"].Value.ToString();
        //                    //existe na PLR
        //                    if (codigoLinha == CodigoObjeto)
        //                    {
        //                        // Se existir, fica verde
        //                        row.DefaultCellStyle.BackColor = Color.LightGreen;
        //                        TempLblTotalValidados++;
        //                    }
        //                }
        //                TempLblQtdTotalFaltantes = dataGridView1.Rows.Count - TempLblTotalValidados;
        //                if (TempLblQtdTotalFaltantes > 0) LblLinkVisualizarObjetosFaltantes.Visible = true;
        //                else LblLinkVisualizarObjetosFaltantes.Visible = false;
        //            }
        //            else
        //            {
        //                TempLblTotalValidados = 0;
        //                foreach (DataGridViewRow row in dataGridView1.Rows)
        //                {
        //                    string codigoLinha = row.Cells["CodigoObjeto"].Value.ToString();
        //                    //existe na PLR
        //                    if (codigoLinha == CodigoObjeto)
        //                    {
        //                        // Se existir, fica verde
        //                        row.DefaultCellStyle.BackColor = Color.LightSalmon;
        //                        TempLblQtdTotalFaltantes++;
        //                    }
        //                }
        //                if (TempLblQtdTotalFaltantes > 0) LblLinkVisualizarObjetosFaltantes.Visible = true;
        //                else LblLinkVisualizarObjetosFaltantes.Visible = false;
        //            }

        //            CarregaRelatorio();


        //            string proximo = string.Empty;
        //            if (linhas[i + 1].Contains("Quantidade de objetos:")) break;
        //            else continue;
        //        }
        //    }


        //}

        private void BtnLerArquivoPLR_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = PastaEnderecoInicialArquivo;
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    PastaEnderecoInicialArquivo = System.IO.Path.GetDirectoryName(filePath);
                }
                else return;
            }

            LblEnderecoBuscado.Text = filePath;

            try
            {
                dtListaObjetosTXT = CarregaRetornaListaObjetosTXT(LblEnderecoBuscado.Text);


                dataGridView1.DataSource = dtListaObjetosTXT;
                dtListaObjetosTXT.DefaultView.Sort = "NumeroListaAtual DESC, ItemAtual ASC, QtdTotal ASC";
                dtListaObjetosTXT = dtListaObjetosTXT.DefaultView.ToTable();





                int QtdTotalPLRs = dtListaObjetosTXT.AsEnumerable().GroupBy(T => T["NumeroListaAtual"]).Count();
                TempLblQtdTotalDePLR = QtdTotalPLRs;
                TempLblQtdTotalItensEmPLRs = dtListaObjetosTXT.Rows.Count;

                for (int i = 0; i < dtListaObjetosTXT.Rows.Count; i++)
                {
                    string CodigoObjeto = dtListaObjetosTXT.Rows[i]["CodigoObjeto"].ToString();
                    bool existeNaListaConferencia = dtListaObjetosConferencia.AsEnumerable().Any(T => T["CodigoObjeto"].ToString().Contains(CodigoObjeto));
                    if (existeNaListaConferencia)
                    {
                        TempLblTotalValidados = 0;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            string codigoLinha = row.Cells["CodigoObjeto"].Value.ToString();
                            //existe na PLR
                            if (codigoLinha == CodigoObjeto)
                            {
                                // Se existir, fica verde
                                row.DefaultCellStyle.BackColor = Color.LightGreen;
                                TempLblTotalValidados++;
                            }
                        }
                        TempLblQtdTotalFaltantes = dataGridView1.Rows.Count - TempLblTotalValidados;
                        if (TempLblQtdTotalFaltantes > 0) LblLinkVisualizarObjetosFaltantes.Visible = true;
                        else LblLinkVisualizarObjetosFaltantes.Visible = false;
                    }
                    else
                    {
                        TempLblTotalValidados = 0;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            string codigoLinha = row.Cells["CodigoObjeto"].Value.ToString();
                            //existe na PLR
                            if (codigoLinha == CodigoObjeto)
                            {
                                // Se existir, fica verde
                                row.DefaultCellStyle.BackColor = Color.LightSalmon;
                                TempLblQtdTotalFaltantes++;
                            }
                        }
                        if (TempLblQtdTotalFaltantes > 0) LblLinkVisualizarObjetosFaltantes.Visible = true;
                        else LblLinkVisualizarObjetosFaltantes.Visible = false;
                    }

                    CarregaRelatorio();
                }

            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
            }
            finally
            {

            }




        }


        private DataTable CarregaRetornaListaObjetosTXT(string EnderecoArquvoTexto)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(EnderecoArquvoTexto);

                foreach (string linha in lines)
                {
                    if (string.IsNullOrWhiteSpace(linha)) continue;
                    string[] CelulasCampos = linha.Split(new string[] { "[TAB]" }, StringSplitOptions.None);

                    #region Carrega variaveis da leitura
                    string CodigoObjeto = CelulasCampos[0];
                    string CodigoLdi = CelulasCampos[1];
                    string NomeCliente = CelulasCampos[2];
                    string DataLancamento = CelulasCampos[3];
                    string DataModificacao = CelulasCampos[4];
                    string Situacao = CelulasCampos[5];
                    string Atualizado = CelulasCampos[6];
                    string ObjetoEntregue = CelulasCampos[7];
                    string CaixaPostal = CelulasCampos[8];
                    string UnidadePostagem = CelulasCampos[9];
                    string MunicipioPostagem = CelulasCampos[10];
                    string CriacaoPostagem = CelulasCampos[11];
                    string CepDestinoPostagem = CelulasCampos[12];
                    string ARPostagem = CelulasCampos[13];
                    string MPPostagem = CelulasCampos[14];
                    string DataMaxPrevistaEntregaPostagem = CelulasCampos[15];
                    string UnidadeLOEC = CelulasCampos[16];
                    string MunicipioLOEC = CelulasCampos[17];
                    string CriacaoLOEC = CelulasCampos[18];
                    string CarteiroLOEC = CelulasCampos[19];
                    string DistritoLOEC = CelulasCampos[20];
                    string NumeroLOEC = CelulasCampos[21];
                    string EnderecoLOEC = CelulasCampos[22];
                    string BairroLOEC = CelulasCampos[23];
                    string LocalidadeLOEC = CelulasCampos[24];
                    string SituacaoDestinatarioAusente = CelulasCampos[25];
                    string AgrupadoDestinatarioAusente = CelulasCampos[26];
                    string CoordenadasDestinatarioAusente = CelulasCampos[27];
                    string Comentario = CelulasCampos[28];
                    string TipoPostalServico = CelulasCampos[29];
                    string TipoPostalSiglaCodigo = CelulasCampos[30];
                    string TipoPostalNomeSiglaCodigo = CelulasCampos[31];
                    string TipoPostalPrazoDiasCorridosRegulamentado = CelulasCampos[32];
                    string DataListaAtual = CelulasCampos[33];
                    string NumeroListaAtual = CelulasCampos[34];
                    int ItemAtual = CelulasCampos[35].ToInt();
                    int QtdTotal = CelulasCampos[36].ToInt();
                    #endregion

                    #region Cria novo DataTable SE Vazio
                    if (dtListaObjetosTXT == null || dtListaObjetosTXT.Rows.Count == 0)
                    {
                        dtListaObjetosTXT = CriaDataTableListaObjetosTXT();
                    }
                    #endregion

                    dtListaObjetosTXT.Rows.Add(
                    CodigoObjeto,
                    CodigoLdi,
                    NomeCliente,
                    DataLancamento,
                    DataModificacao,
                    Situacao,
                    Atualizado,
                    ObjetoEntregue,
                    CaixaPostal,
                    UnidadePostagem,
                    MunicipioPostagem,
                    CriacaoPostagem,
                    CepDestinoPostagem,
                    ARPostagem,
                    MPPostagem,
                    DataMaxPrevistaEntregaPostagem,
                    UnidadeLOEC,
                    MunicipioLOEC,
                    CriacaoLOEC,
                    CarteiroLOEC,
                    DistritoLOEC,
                    NumeroLOEC,
                    EnderecoLOEC,
                    BairroLOEC,
                    LocalidadeLOEC,
                    SituacaoDestinatarioAusente,
                    AgrupadoDestinatarioAusente,
                    CoordenadasDestinatarioAusente,
                    Comentario,
                    TipoPostalServico,
                    TipoPostalSiglaCodigo,
                    TipoPostalNomeSiglaCodigo,
                    TipoPostalPrazoDiasCorridosRegulamentado,
                    DataListaAtual,
                    NumeroListaAtual,
                    ItemAtual,
                    QtdTotal);
                }

                return dtListaObjetosTXT;
            }
            catch (Exception ex)
            {
                Mensagens.Erro(ex.Message);
                return dtListaObjetosTXT;
            }
        }


        private void TxtLeituraConferencia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //verifica se não está vazio
                if (string.IsNullOrWhiteSpace(TxtLeituraConferencia.Text))
                    return;
                //verifica se a tabela está vazia

                bool existeNaListaConferencia = dtListaObjetosConferencia.AsEnumerable().Any(T => T["CodigoObjeto"].ToString().Contains(TxtLeituraConferencia.Text));
                if (!existeNaListaConferencia)
                {
                    bool existeNaListaPLR = dtListaObjetosTXT.AsEnumerable().Any(T => T["CodigoObjeto"].ToString().Contains(TxtLeituraConferencia.Text));

                    DataRow linhaDtListaObjetosConferencia = dtListaObjetosConferencia.NewRow();
                    linhaDtListaObjetosConferencia["Item"] = itemConferencia++;
                    linhaDtListaObjetosConferencia["CodigoObjeto"] = TxtLeituraConferencia.Text;
                    linhaDtListaObjetosConferencia["Resultado"] = existeNaListaPLR;
                    dtListaObjetosConferencia.Rows.Add(linhaDtListaObjetosConferencia);

                    DataTable Temp = new DataTable();
                    Temp.Columns.Add("Item");
                    Temp.Columns.Add("CodigoObjeto");
                    Temp.Columns.Add("Resultado");

                    int contador = 0;
                    foreach (DataRow itemNovaOrdem in dtListaObjetosConferencia.Rows)
                    {
                        contador++;

                        existeNaListaPLR = dtListaObjetosTXT.AsEnumerable().Any(T => T["CodigoObjeto"].ToString().Contains(itemNovaOrdem["CodigoObjeto"].ToString()));

                        DataRow DtTempRowConferencia = Temp.NewRow();
                        DtTempRowConferencia["Item"] = contador;
                        DtTempRowConferencia["CodigoObjeto"] = itemNovaOrdem["CodigoObjeto"].ToString();
                        DtTempRowConferencia["Resultado"] = existeNaListaPLR;
                        Temp.Rows.Add(DtTempRowConferencia);
                    }

                    dtListaObjetosConferencia.Clear();
                    dtListaObjetosConferencia = Temp;

                    dataGridView2.DataSource = dtListaObjetosConferencia;
                    MudaCorLinhasGridView1();
                    MudaCorLinhasGridView2();
                    CarregaRelatorio();

                    TxtLeituraConferencia.Text = string.Empty;
                    TxtLeituraConferencia.Focus();
                    TxtLeituraConferencia.ScrollToCaret();
                }
                else
                {
                    TxtLeituraConferencia.Text = string.Empty;
                    TxtLeituraConferencia.Focus();
                    TxtLeituraConferencia.ScrollToCaret();
                    return;
                }
            }
        }

        private void CarregaRelatorio()
        {

            //LblQtdTotalDePLRs.Text = TempLblQtdTotalDePLR.ToString();//linha 1
            //LblQtdTotalItensEmPLRs.Text = TempLblQtdTotalItensEmPLRs.ToString();//linha 2
            //LblTotalValidados.Text = TempLblTotalValidados.ToString();//linha 3
            //LblQtdTotalFaltantes.Text = TempLblQtdTotalFaltantes.ToString();//linha 4

            //LblObjetosLidos.Text = dtListaObjetosConferencia.Rows.Count.ToString();//linha 5
            //LblQdtTotalComPLR.Text = TempLblQdtTotalComPLR.ToString();//linha 6
            //LblQtdTotalSemPLR.Text = TempLblQtdTotalSemPLR.ToString();//linha 7

            DataTable dtListaRelatorioPLR = new DataTable();
            dtListaRelatorioPLR.Clear();
            dtListaRelatorioPLR.Columns.Add("Descricao", typeof(string));
            dtListaRelatorioPLR.Columns.Add("Qtd", typeof(string));

            dtListaRelatorioPLR.Rows.Add("Quantidade de PLRs", TempLblQtdTotalDePLR.ToString());//linha 1
            dtListaRelatorioPLR.Rows.Add("Objetos listados", TempLblQtdTotalItensEmPLRs.ToString());//linha 2
            dtListaRelatorioPLR.Rows.Add("Objetos validados", TempLblTotalValidados.ToString());//linha 3
            dtListaRelatorioPLR.Rows.Add("Objetos faltantes", TempLblQtdTotalFaltantes.ToString());//linha 4
            dataGridView3.DataSource = dtListaRelatorioPLR;


            DataTable dtListaRelatorioConferencia = new DataTable();
            dtListaRelatorioConferencia.Clear();
            dtListaRelatorioConferencia.Columns.Add("Descricao", typeof(string));
            dtListaRelatorioConferencia.Columns.Add("Qtd", typeof(string));
            dtListaRelatorioConferencia.Rows.Add("Objetos lidos", dtListaObjetosConferencia.Rows.Count.ToString());//linha 5
            dtListaRelatorioConferencia.Rows.Add("Objetos em PLR", TempLblQdtTotalComPLR.ToString());//linha 6
            dtListaRelatorioConferencia.Rows.Add("Objetos sem PLR", TempLblQtdTotalSemPLR.ToString());//linha 7
            dataGridView4.DataSource = dtListaRelatorioConferencia;
        }

        private void MudaCorLinhasGridView1()
        {
            TempLblTotalValidados = 0;

            foreach (DataGridViewRow rowGrid1 in dataGridView1.Rows)
                rowGrid1.DefaultCellStyle.BackColor = Color.LightSalmon;

            foreach (DataGridViewRow rowGrid2 in dataGridView2.Rows)
            {
                string codigoLinhaGrid2 = rowGrid2.Cells["dataGridViewTextBoxColumnCodigoObjeto"].Value.ToString();

                foreach (DataGridViewRow rowGrid1 in dataGridView1.Rows)
                {
                    string codigoLinhaGrid1 = rowGrid1.Cells["CodigoObjeto"].Value.ToString();
                    if (codigoLinhaGrid2 == codigoLinhaGrid1)
                    {
                        rowGrid1.DefaultCellStyle.BackColor = Color.LightGreen;
                        TempLblTotalValidados++;
                    }
                }
            }

            TempLblQtdTotalFaltantes = dataGridView1.Rows.Count - TempLblTotalValidados;
            if (TempLblQtdTotalFaltantes > 0) LblLinkVisualizarObjetosFaltantes.Visible = true;
            else LblLinkVisualizarObjetosFaltantes.Visible = false;
        }

        private void MudaCorLinhasGridView2()
        {
            TempLblQtdTotalSemPLR = 0;
            TempLblQdtTotalComPLR = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string codigoLinha = row.Cells["dataGridViewTextBoxColumnCodigoObjeto"].Value.ToString();
                //existe na PLR
                if (Convert.ToBoolean(row.Cells["dataGridViewTextBoxColumnResultado"].Value) == true)
                {
                    // Se for negativo, fica verde
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    TempLblQdtTotalComPLR++;
                }
                //Não existe na PLR
                if (Convert.ToBoolean(row.Cells["dataGridViewTextBoxColumnResultado"].Value) == false)
                {
                    // Se for negativo, fica vermelho
                    row.DefaultCellStyle.BackColor = Color.LightSalmon;
                    TempLblQtdTotalSemPLR++;
                }
            }
        }

        private void LblLinkVisualizarObjetosFaltantes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataTable dtListaObjetosFaltantes = new DataTable();
            dtListaObjetosFaltantes.Columns.Add("CodigoObjeto", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("CodigoLdi", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("NomeCliente", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("DataLancamento", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("DataModificacao", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("Situacao", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("Atualizado", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("ObjetoEntregue", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("CaixaPostal", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("UnidadePostagem", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("MunicipioPostagem", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("CriacaoPostagem", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("CepDestinoPostagem", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("ARPostagem", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("MPPostagem", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("DataMaxPrevistaEntregaPostagem", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("UnidadeLOEC", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("MunicipioLOEC", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("CriacaoLOEC", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("CarteiroLOEC", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("DistritoLOEC", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("NumeroLOEC", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("EnderecoLOEC", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("BairroLOEC", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("LocalidadeLOEC", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("SituacaoDestinatarioAusente", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("AgrupadoDestinatarioAusente", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("CoordenadasDestinatarioAusente", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("Comentario", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("TipoPostalServico", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("TipoPostalSiglaCodigo", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("TipoPostalNomeSiglaCodigo", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("TipoPostalPrazoDiasCorridosRegulamentado", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("DataListaAtual", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("NumeroListaAtual", typeof(string));
            dtListaObjetosFaltantes.Columns.Add("ItemAtual", typeof(int));
            dtListaObjetosFaltantes.Columns.Add("QtdTotal", typeof(int));

            //int item = 1;
            foreach (DataGridViewRow rowGrid1 in dataGridView1.Rows)
            {
                string codigoLinhaGrid1 = rowGrid1.Cells["CodigoObjeto"].Value.ToString();
                bool existeNaConferencia = dtListaObjetosConferencia.AsEnumerable().Any(T => T["CodigoObjeto"].ToString().Contains(codigoLinhaGrid1));
                if (!existeNaConferencia)
                {
                    //cria Nova Tabela somente com os não conferidos/faltantes
                    //cria linha
                    DataRow linhaDtListaObjetos = dtListaObjetosFaltantes.NewRow();
                    linhaDtListaObjetos["CodigoObjeto"] = rowGrid1.Cells["CodigoObjeto"].Value.ToString();
                    linhaDtListaObjetos["CodigoLdi"] = rowGrid1.Cells["CodigoLdi"].Value.ToString();
                    linhaDtListaObjetos["NomeCliente"] = rowGrid1.Cells["NomeCliente"].Value.ToString();
                    linhaDtListaObjetos["DataLancamento"] = rowGrid1.Cells["DataLancamento"].Value.ToString();
                    linhaDtListaObjetos["DataModificacao"] = rowGrid1.Cells["DataModificacao"].Value.ToString();
                    linhaDtListaObjetos["Situacao"] = rowGrid1.Cells["Situacao"].Value.ToString();
                    linhaDtListaObjetos["Atualizado"] = rowGrid1.Cells["Atualizado"].Value.ToString();
                    linhaDtListaObjetos["ObjetoEntregue"] = rowGrid1.Cells["ObjetoEntregue"].Value.ToString();
                    linhaDtListaObjetos["CaixaPostal"] = rowGrid1.Cells["CaixaPostal"].Value.ToString();
                    linhaDtListaObjetos["UnidadePostagem"] = rowGrid1.Cells["UnidadePostagem"].Value.ToString();
                    linhaDtListaObjetos["MunicipioPostagem"] = rowGrid1.Cells["MunicipioPostagem"].Value.ToString();
                    linhaDtListaObjetos["CriacaoPostagem"] = rowGrid1.Cells["CriacaoPostagem"].Value.ToString();
                    linhaDtListaObjetos["CepDestinoPostagem"] = rowGrid1.Cells["CepDestinoPostagem"].Value.ToString();
                    linhaDtListaObjetos["ARPostagem"] = rowGrid1.Cells["ARPostagem"].Value.ToString();
                    linhaDtListaObjetos["MPPostagem"] = rowGrid1.Cells["MPPostagem"].Value.ToString();
                    linhaDtListaObjetos["DataMaxPrevistaEntregaPostagem"] = rowGrid1.Cells["DataMaxPrevistaEntregaPostagem"].Value.ToString();
                    linhaDtListaObjetos["UnidadeLOEC"] = rowGrid1.Cells["UnidadeLOEC"].Value.ToString();
                    linhaDtListaObjetos["MunicipioLOEC"] = rowGrid1.Cells["MunicipioLOEC"].Value.ToString();
                    linhaDtListaObjetos["CriacaoLOEC"] = rowGrid1.Cells["CriacaoLOEC"].Value.ToString();
                    linhaDtListaObjetos["CarteiroLOEC"] = rowGrid1.Cells["CarteiroLOEC"].Value.ToString();
                    linhaDtListaObjetos["DistritoLOEC"] = rowGrid1.Cells["DistritoLOEC"].Value.ToString();
                    linhaDtListaObjetos["NumeroLOEC"] = rowGrid1.Cells["NumeroLOEC"].Value.ToString();
                    linhaDtListaObjetos["EnderecoLOEC"] = rowGrid1.Cells["EnderecoLOEC"].Value.ToString();
                    linhaDtListaObjetos["BairroLOEC"] = rowGrid1.Cells["BairroLOEC"].Value.ToString();
                    linhaDtListaObjetos["LocalidadeLOEC"] = rowGrid1.Cells["LocalidadeLOEC"].Value.ToString();
                    linhaDtListaObjetos["SituacaoDestinatarioAusente"] = rowGrid1.Cells["SituacaoDestinatarioAusente"].Value.ToString();
                    linhaDtListaObjetos["AgrupadoDestinatarioAusente"] = rowGrid1.Cells["AgrupadoDestinatarioAusente"].Value.ToString();
                    linhaDtListaObjetos["CoordenadasDestinatarioAusente"] = rowGrid1.Cells["CoordenadasDestinatarioAusente"].Value.ToString();
                    linhaDtListaObjetos["Comentario"] = rowGrid1.Cells["Comentario"].Value.ToString();
                    linhaDtListaObjetos["TipoPostalServico"] = rowGrid1.Cells["TipoPostalServico"].Value.ToString();
                    linhaDtListaObjetos["TipoPostalSiglaCodigo"] = rowGrid1.Cells["TipoPostalSiglaCodigo"].Value.ToString();
                    linhaDtListaObjetos["TipoPostalNomeSiglaCodigo"] = rowGrid1.Cells["TipoPostalNomeSiglaCodigo"].Value.ToString();
                    linhaDtListaObjetos["TipoPostalPrazoDiasCorridosRegulamentado"] = rowGrid1.Cells["TipoPostalPrazoDiasCorridosRegulamentado"].Value.ToString();
                    linhaDtListaObjetos["DataListaAtual"] = rowGrid1.Cells["DataListaAtual"].Value.ToString();
                    linhaDtListaObjetos["NumeroListaAtual"] = rowGrid1.Cells["NumeroListaAtual"].Value.ToString();
                    linhaDtListaObjetos["ItemAtual"] = rowGrid1.Cells["ItemAtual"].Value.ToInt();
                    linhaDtListaObjetos["QtdTotal"] = rowGrid1.Cells["QtdTotal"].Value.ToInt();

                    dtListaObjetosFaltantes.Rows.Add(linhaDtListaObjetos);
                }
            }

            if (dtListaObjetosFaltantes.Rows.Count > 0)
            {
                using (FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes = new FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes(dtListaObjetosFaltantes))
                {
                    FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes.ShowDialog();
                }
            }
        }

        private void LblLinkLimparListaConferencia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtListaObjetosConferencia.Clear();
            dataGridView2.DataSource = dtListaObjetosConferencia;

            itemConferencia = 1;

            MudaCorLinhasGridView1();

            MudaCorLinhasGridView2();




            CarregaRelatorio();

            TxtLeituraConferencia.Text = string.Empty;
            TxtLeituraConferencia.Focus();
            TxtLeituraConferencia.ScrollToCaret();
        }

        private void LblLinkLimparListaPLRs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtListaObjetosTXT.Clear();
            dataGridView1.DataSource = dtListaObjetosTXT;
            TempLblQtdTotalDePLR = 0;
            TempLblQtdTotalItensEmPLRs = 0;
            TempLblTotalValidados = 0;
            TempLblQtdTotalFaltantes = 0;


            MudaCorLinhasGridView1();

            MudaCorLinhasGridView2();

            CarregaRelatorio();

            LblEnderecoBuscado.Text = string.Empty;
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MudaCorLinhasGridView1();
        }

        private void dataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MudaCorLinhasGridView2();
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (var item in dataGridView2.SelectedCells)
                {
                    int sss = ((System.Windows.Forms.DataGridViewCell)item).OwningRow.Index;
                    dtListaObjetosConferencia.Rows[sss].Delete();
                    dtListaObjetosConferencia.AcceptChanges();
                    break;
                }

                DataTable Temp = new DataTable();
                Temp.Columns.Add("Item");
                Temp.Columns.Add("CodigoObjeto");
                Temp.Columns.Add("Resultado");

                int contador = 0;
                foreach (DataRow item in dtListaObjetosConferencia.Rows)
                {
                    contador++;

                    bool existeNaListaPLR = dtListaObjetosTXT.AsEnumerable().Any(T => T["CodigoObjeto"].ToString().Contains(item["CodigoObjeto"].ToString()));

                    DataRow DtTempRowConferencia = Temp.NewRow();
                    DtTempRowConferencia["Item"] = contador;
                    DtTempRowConferencia["CodigoObjeto"] = item["CodigoObjeto"].ToString();
                    DtTempRowConferencia["Resultado"] = existeNaListaPLR;
                    Temp.Rows.Add(DtTempRowConferencia);
                }

                dtListaObjetosConferencia.Clear();
                dtListaObjetosConferencia = Temp;

                dataGridView2.DataSource = dtListaObjetosConferencia;
                MudaCorLinhasGridView1();
                MudaCorLinhasGridView2();
                CarregaRelatorio();
            }
        }
    }
}
