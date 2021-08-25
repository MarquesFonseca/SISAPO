using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SISAPO.Relatorios.TiposPostaisMaisUsados
{
    public partial class FormRelTiposPostaisMaisUsados : Form
    {
        string ArquivoASerCopiadoTemp = string.Empty;
        public FormRelTiposPostaisMaisUsados()
        {
            InitializeComponent();
        }

        private void FormRelTiposPostaisMaisUsados_Load(object sender, EventArgs e)
        {
            string curDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string EnderecoDoArquivoOrigem = string.Format(@"{0}/{1}", curDir, "Relatorios");

            ArquivoASerCopiadoTemp = "animated.js";
            CaregaArquivoPastaTemSistema(EnderecoDoArquivoOrigem, ArquivoASerCopiadoTemp);
            ArquivoASerCopiadoTemp = "charts.js";
            CaregaArquivoPastaTemSistema(EnderecoDoArquivoOrigem, ArquivoASerCopiadoTemp);
            ArquivoASerCopiadoTemp = "core.js";
            CaregaArquivoPastaTemSistema(EnderecoDoArquivoOrigem, ArquivoASerCopiadoTemp);
            ArquivoASerCopiadoTemp = "index.css";
            CaregaArquivoPastaTemSistema(EnderecoDoArquivoOrigem, ArquivoASerCopiadoTemp);
            ArquivoASerCopiadoTemp = "index.js";
            CaregaArquivoPastaTemSistema(EnderecoDoArquivoOrigem, ArquivoASerCopiadoTemp);
            ArquivoASerCopiadoTemp = "TiposPostaisMaisUsados.html";
            CaregaArquivoPastaTemSistema(EnderecoDoArquivoOrigem, ArquivoASerCopiadoTemp);
        }

        public void CaregaArquivoPastaTemSistema(string pastaOrigem, string nomeArquivo)
        {
            string curDirTemp = System.IO.Path.GetTempPath();
            string NomeEnderecoCompletoArquivo = string.Format(@"{0}{1}", pastaOrigem, nomeArquivo);

            System.IO.File.Copy(string.Format(@"{0}\{1}", pastaOrigem, nomeArquivo), string.Format(@"{0}{1}", curDirTemp, nomeArquivo));
        }
    }
}
