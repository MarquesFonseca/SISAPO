using SISAPO.ClassesDiversas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SISAPO
{
    public partial class RelatorioPrincipal : Form
    {


        public RelatorioPrincipal()
        {
            InitializeComponent();
        }

        private void RelatorioPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void AbrirArquivoNoNavegador(string nomeEnderecoArquivo)
        {
            string curDirTemp = System.IO.Path.GetTempPath();
            string NomeEnderecoCompletoABrir = string.Format(@"{0}\{1}", curDirTemp, nomeEnderecoArquivo);

            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

            if (System.IO.File.Exists(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            }
            else if (System.IO.File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            }
            else if (System.IO.File.Exists(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            }
            else if (System.IO.File.Exists(@"C:\Program Files\Google\Chrome\Application\chrome.exe"))
            {
                pProcess.StartInfo.FileName = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            }
            else
            {
                return;
            }

            pProcess.StartInfo.Arguments = string.Format("\"{0}\"", NomeEnderecoCompletoABrir);
            pProcess.Start();
        }

        public void CopiaArquivoDesejadoPastaTemSistema(string pastaOrigem, string nomeArquivo, bool sobrecrever = false)
        {
            string curDirTemp = System.IO.Path.GetTempPath();

            if (sobrecrever)
            {
                if (Arquivos.Existe(string.Format(@"{0}\{1}", curDirTemp, nomeArquivo), false))
                {
                    Arquivos.DeletarArquivo(curDirTemp, string.Format(@"{0}\{1}", curDirTemp, nomeArquivo));
                }
            }
            if (!Arquivos.Existe(string.Format(@"{0}\{1}", curDirTemp, nomeArquivo), false))
            {
                System.IO.File.Copy(string.Format(@"{0}\{1}", pastaOrigem, nomeArquivo), string.Format(@"{0}{1}", curDirTemp, nomeArquivo));
            }
        }

        private void BtnTiposPostaisMaisUtilizados_Click(object sender, EventArgs e)
        {
            StringBuilder resultado = ConstroiRelatorioTiposPostaisMaisUtilizados();

            string ArquivoASerCopiadoTemp = string.Empty;
            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            SalvarArquivoPasta(resultado, curDir, "TiposPostaisMaisUsadosTeste.cs");

            #region Copia e cola os arquivos na TEMP
            ArquivoASerCopiadoTemp = string.Empty;
            curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string PastaResouce = string.Format(@"{0}\{1}", curDir, "Resources");
#if DEBUG
            //private static string _strConexao = System.Configuration.ConfigurationManager.ConnectionStrings["cadastroConnectionString"].ConnectionString;
            curDir = @"C:\Projetos\SISAPO\SISAPO";
#endif
            string EnderecoDoArquivoOrigem = string.Format(@"{0}\{1}", curDir, "Relatorios\\TiposPostaisMaisUsados");

            ArquivoASerCopiadoTemp = "animated.js";
            CopiaArquivoDesejadoPastaTemSistema(PastaResouce, ArquivoASerCopiadoTemp, true);
            ArquivoASerCopiadoTemp = "charts.js";
            CopiaArquivoDesejadoPastaTemSistema(PastaResouce, ArquivoASerCopiadoTemp, true);
            ArquivoASerCopiadoTemp = "core.js";
            CopiaArquivoDesejadoPastaTemSistema(PastaResouce, ArquivoASerCopiadoTemp, true);

            ArquivoASerCopiadoTemp = "TiposPostaisMaisUsados.css";
            CopiaArquivoDesejadoPastaTemSistema(EnderecoDoArquivoOrigem, ArquivoASerCopiadoTemp, true);
            ArquivoASerCopiadoTemp = "TiposPostaisMaisUsados.js";
            CopiaArquivoDesejadoPastaTemSistema(EnderecoDoArquivoOrigem, ArquivoASerCopiadoTemp, true);
            ArquivoASerCopiadoTemp = "TiposPostaisMaisUsados.html";
            CopiaArquivoDesejadoPastaTemSistema(EnderecoDoArquivoOrigem, ArquivoASerCopiadoTemp, true);
            #endregion

            AbrirArquivoNoNavegador("TiposPostaisMaisUsados.html");
        }

        

        private StringBuilder ConstroiRelatorioTiposPostaisMaisUtilizados()
        {
            StringBuilder texto = new StringBuilder();
            texto.AppendLine("am4core.useTheme(am4themes_animated);");

            texto.AppendLine("// create chart");
            texto.AppendLine("var chart = am4core.create(\"chartdiv\", am4charts.TreeMap);");
            texto.AppendLine("");
            //SELECT
            //TiposPostais.Sigla, 
            //COUNT(TiposPostais.Sigla) AS Qtd
            //FROM TabelaObjetosSROLocal INNER JOIN TiposPostais 
            //ON Mid(TabelaObjetosSROLocal.CodigoObjeto, 1, 2) = TiposPostais.Sigla 
            //WHERE TiposPostais.TipoClassificacao = 'PAC'
            //GROUP BY TiposPostais.Sigla
            //ORDER BY 2

            if (!DAO.TestaConexao(ClassesDiversas.Configuracoes.strConexao, TipoBanco.OleDb))
            {
                FormularioPrincipal.RetornaComponentesFormularioPrincipal().toolStripStatusLabel.Text = Configuracoes.MensagemPerdaConexao;
                return null;
            }
            DAO dao = new DAO(TipoBanco.OleDb, ClassesDiversas.Configuracoes.strConexao);


            //SELECT distinct TipoClassificacao from TiposPostais
            DataSet dsTipoClassificacao = dao.RetornaDataSet(@"SELECT distinct TipoClassificacao from TiposPostais");

            texto.AppendLine("");
            texto.AppendLine("//PC	6");
            texto.AppendLine("chart.data = [{");

            for (int i = 0; i < dsTipoClassificacao.Tables[0].Rows.Count; i++)
            {
                texto.AppendLine("    name: \"" + dsTipoClassificacao.Tables[0].Rows[i]["TipoClassificacao"].ToString() + "\",");
                texto.AppendLine("children:");
                texto.AppendLine("[");
                DataSet dsTiposPostais = dao.RetornaDataSet(
                "SELECT "
                + "TiposPostais.Sigla, "
                + "COUNT(TiposPostais.Sigla) AS Qtd"
                + "FROM TabelaObjetosSROLocal INNER JOIN TiposPostais" 
                + "ON Mid(TabelaObjetosSROLocal.CodigoObjeto, 1, 2) = TiposPostais.Sigla" 
                + "WHERE TiposPostais.TipoClassificacao = '"+ dsTipoClassificacao.Tables[0].Rows[i]["TipoClassificacao"].ToString() + "'"
                + "GROUP BY TiposPostais.Sigla"
                + "ORDER BY 2");

                for (int z = 0; z < dsTiposPostais.Tables[0].Rows.Count; z++)
                {
                    texto.AppendLine("{ name: \""+ dsTiposPostais.Tables[0].Rows[z]["Sigla"].ToString() +"\", value: "+ dsTiposPostais.Tables[0].Rows[z]["Qtd"].ToString() + " }");
                    if (z == z + 1)//final
                    {
                        texto.AppendLine("}");
                    }
                    else
                    {
                        texto.AppendLine("},");
                    }
                }

                texto.AppendLine("]");


                if (i == i + 1)//final
                {
                    texto.AppendLine("}");
                }
                else
                {
                    texto.AppendLine("},");
                }
            }
            texto.AppendLine("];");

            
            texto.AppendLine("");
            texto.AppendLine("chart.colors.step = 2;");
            texto.AppendLine("");
            texto.AppendLine("// define data fields");
            texto.AppendLine("chart.dataFields.value = \"value\";");
            texto.AppendLine("chart.dataFields.name = \"name\";");
            texto.AppendLine("chart.dataFields.children = \"children\";");
            texto.AppendLine("");
            texto.AppendLine("chart.zoomable = false;");
            texto.AppendLine("");
            texto.AppendLine("// level 0 series template");
            texto.AppendLine("var level0SeriesTemplate = chart.seriesTemplates.create(\"0\");");
            texto.AppendLine("var level0ColumnTemplate = level0SeriesTemplate.columns.template;");
            texto.AppendLine("");
            texto.AppendLine("level0ColumnTemplate.column.cornerRadius(10, 10, 10, 10);");
            texto.AppendLine("level0ColumnTemplate.fillOpacity = 0;");
            texto.AppendLine("level0ColumnTemplate.strokeWidth = 4;");
            texto.AppendLine("level0ColumnTemplate.strokeOpacity = 0;");
            texto.AppendLine("");
            texto.AppendLine("// level 1 series template");
            texto.AppendLine("var level1SeriesTemplate = chart.seriesTemplates.create(\"1\");");
            texto.AppendLine("var level1ColumnTemplate = level1SeriesTemplate.columns.template;");
            texto.AppendLine("");
            texto.AppendLine("level1SeriesTemplate.tooltip.animationDuration = 0;");
            texto.AppendLine("level1SeriesTemplate.strokeOpacity = 1;");
            texto.AppendLine("");
            texto.AppendLine("level1ColumnTemplate.column.cornerRadius(10, 10, 10, 10)");
            texto.AppendLine("level1ColumnTemplate.fillOpacity = 1;");
            texto.AppendLine("level1ColumnTemplate.strokeWidth = 4;");
            texto.AppendLine("level1ColumnTemplate.stroke = am4core.color(\"#ffffff\");");
            texto.AppendLine("");
            texto.AppendLine("var bullet1 = level1SeriesTemplate.bullets.push(new am4charts.LabelBullet());");
            texto.AppendLine("bullet1.locationY = 0.5;");
            texto.AppendLine("bullet1.locationX = 0.5;");
            texto.AppendLine("bullet1.label.text = \"{name}\");");
            texto.AppendLine("bullet1.label.fill = am4core.color(\"#ffffff\");");
            texto.AppendLine("");
            texto.AppendLine("chart.maxLevels = 2;");



            string teste = texto.ToString();

            

            return texto;
        }

        private void SalvarArquivoPasta(StringBuilder texto, string pastaOrigem, string nomeArquivo)
        {

        }
    }
}
