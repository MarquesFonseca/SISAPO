using SISAPO.ClassesDiversas;
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
    public partial class FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes : Form
    {
        private DataTable DtLista;

        public FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes(DataTable dtLista)
        {
            InitializeComponent();

            DtLista = dtLista;
            dataGridView1.DataSource = dtLista;
            if (dtLista.Rows.Count == 1)
                LblQtdFaltantes.Text = string.Format("{0} Objeto", dtLista.Rows.Count);
            if (dtLista.Rows.Count > 1)
                LblQtdFaltantes.Text = string.Format("{0} Objetos", dtLista.Rows.Count);
        }

        private void FormularioCadastroObjetosViaTXTPLRDaAgenciaMaeConferenciaObjetosFaltantes_Load(object sender, EventArgs e)
        {

        }

        private void BtnEnviarEmailAgenciaMae_Click(object sender, EventArgs e)
        {
            string mensagem = "Resumo\n";
            string NumeroListaAtual = string.Empty;
            string DataListaAtual = string.Empty;
            string QtdTotaListaAtual = string.Empty;
            string QtdTotal = string.Empty;

            //verifica e informa que a possibilidade de falta de objetos lidos na lista / nas listas(varias listas)
            var ListasAgrupadas = DtLista.AsEnumerable().GroupBy(T => T["NumeroListaAtual"]);//agrupo por lista
            foreach (var itemDataTableNumeroLista in ListasAgrupadas)
            {
                StringBuilder itensHTMListaLido = new StringBuilder();
                DataTable DataTableNumeroLista = itemDataTableNumeroLista.AsEnumerable().OrderBy(T => T["ItemAtual"]).OrderBy(T => T["QtdTotal"]).CopyToDataTable();
                foreach (DataRow item in DataTableNumeroLista.Rows)
                {
                    NumeroListaAtual = item["NumeroListaAtual"].ToString();
                    DataListaAtual = item["DataListaAtual"].ToString();
                    QtdTotaListaAtual = DataTableNumeroLista.Rows.Count.ToString();
                    QtdTotal = item["QtdTotal"].ToString();
                    string ItemAtual = item["ItemAtual"].ToString();
                    string CodigoObjeto = item["CodigoObjeto"].ToString();
                    string CodigoLdi = item["CodigoLdi"].ToString();
                    string NomeCliente = item["NomeCliente"].ToString();
                    string Comentario = item["Comentario"].ToString();
                    string TipoPostalNomeSiglaCodigo = item["TipoPostalNomeSiglaCodigo"].ToString();

                    itensHTMListaLido = RetornaItensSegundaParteHTMListaLido(itensHTMListaLido, ItemAtual, QtdTotal, CodigoObjeto, CodigoLdi, NomeCliente, Comentario, TipoPostalNomeSiglaCodigo);
                    string teste = itensHTMListaLido.ToString();
                }

                string PrimeiraParte = retornaPrimeiraParteHTMLListaLido();
                string CabecalhoParte = RetornaCabecalhoSegundaParteHTMListaLido(NumeroListaAtual, DataListaAtual, QtdTotaListaAtual, QtdTotal);
                string SegundaParte = retornaSegundaParteHTMLListaFaltantes();
                string SegundaParteCentral = itensHTMListaLido.ToString();
                string TerceiraParteFinal = retornaTerceiraParteHTMLListaFaltantes();

                StringBuilder html = new StringBuilder();
                html.AppendLine(PrimeiraParte);
                html.AppendLine(CabecalhoParte);
                html.AppendLine(SegundaParte);
                html.AppendLine(SegundaParteCentral);
                html.AppendLine(TerceiraParteFinal);
                string htmlFinal = html.ToString();
                EnviaEmailHTMLConsolidadoListaRecebimentoPLR(NumeroListaAtual, DateTime.Now.ToString(), html.ToString());
                mensagem += string.Format("Número da lista: '{0}'\nQtd. faltantes....: '{1}'\n\n", NumeroListaAtual, QtdTotaListaAtual);
            }

            Mensagens.Informa(mensagem);
            this.Close();
        }

        private string retornaPrimeiraParteHTMLListaLido()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("<!DOCTYPE html>                               ");
            str.AppendLine("<html>                                        ");
            str.AppendLine("<head>                                        ");
            str.AppendLine("	<title>HTML Table Generator</title>       ");
            str.AppendLine("	<style>                                   ");
            str.AppendLine("		table {                               ");
            str.AppendLine("			border:1px solid #b3adad;         ");
            str.AppendLine("			font-family:Arial, sans-serif;    ");
            str.AppendLine("			font-size:12px;                   ");
            str.AppendLine("			border-collapse:collapse;         ");
            str.AppendLine("			padding:5px;                      ");
            str.AppendLine("		}                                     ");
            str.AppendLine("		table th {                            ");
            str.AppendLine("			border:1px solid #b3adad;         ");
            str.AppendLine("			text-align:center;                ");
            str.AppendLine("			padding:5px;                      ");
            str.AppendLine("			background: #f0f0f0;              ");
            str.AppendLine("			color: #313030;                   ");
            str.AppendLine("		}                                     ");
            str.AppendLine("		table td {                            ");
            str.AppendLine("			border:1px solid #b3adad;         ");
            str.AppendLine("			text-align:left;                  ");
            str.AppendLine("			padding:5px;                      ");
            str.AppendLine("			background: #ffffff;              ");
            str.AppendLine("			color: #313030;                   ");
            str.AppendLine("		}                                     ");
            str.AppendLine("	</style>                                  ");
            str.AppendLine("</head>                                       ");
            str.AppendLine("<body>                                        ");
            str.AppendLine("	<table>                                   ");
            return str.ToString();
        }

        private string RetornaCabecalhoSegundaParteHTMListaLido(string NumeroListaAtual, string DataListaAtual, string QtdTotaListaAtual, string QtdTotal)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("		<thead>                               ");
            str.AppendLine("			<tr>                             ");
            str.AppendLine("				<th>Núm. lista</th>          ");//
            str.AppendLine("				<th>Data emissão lista</th>  ");//
            str.AppendLine("				<th>Data recebimento</th>    ");
            str.AppendLine("				<th>Qtd. Total de itens</th> ");
            str.AppendLine("				<th>Qtd. lidas/recebidas</th>");
            str.AppendLine("				<th>Qtd. faltantes</th>      ");
            str.AppendLine("			</tr>                            ");
            str.AppendLine("		</thead>                              ");
            str.AppendLine("			<tr>                             ");
            str.AppendLine("				<td>" + NumeroListaAtual + "</th>          ");
            str.AppendLine("				<td>" + DataListaAtual + "</th>  ");
            str.AppendLine("				<td>" + DateTime.Now.ToString() + "</th>    ");
            str.AppendLine("				<td>" + QtdTotal + "</th>");
            str.AppendLine("				<td>" + (QtdTotal.ToInt() - QtdTotaListaAtual.ToInt()) + "</th>      ");
            str.AppendLine("				<td>" + QtdTotaListaAtual + "</th> ");
            str.AppendLine("			</tr>                            ");
            return str.ToString();
        }

        private string retornaSegundaParteHTMLListaFaltantes()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine("		<thead>                               ");
            str.AppendLine("			<tr>                              ");
            str.AppendLine("				<th>Item/Qtd.</th>            ");
            str.AppendLine("				<th>Código Objeto</th>        ");
            str.AppendLine("				<th>Núm. LDI</th>             ");
            str.AppendLine("				<th>Nome cliente</th>         ");
            str.AppendLine("				<th>Comentário</th>           ");
            str.AppendLine("				<th>Serviço</th>                ");
            str.AppendLine("			</tr>                             ");
            str.AppendLine("		</thead>                              ");
            str.AppendLine("		<tbody>                               ");
            return str.ToString();
        }

        private StringBuilder RetornaItensSegundaParteHTMListaLido(StringBuilder itensHTMListaLido, string ItemAtual, string QtdTotal, string CodigoObjeto, string CodigoLdi, string NomeCliente, string Comentario, string TipoPostalNomeSiglaCodigo)
        {
            itensHTMListaLido.AppendLine("			<tr>                  ");
            itensHTMListaLido.AppendLine("				<td>" + string.Format("{0:00000}/{1}", ItemAtual, QtdTotal) + "</td>   ");//Item/Qtd.
            itensHTMListaLido.AppendLine("				<td>" + CodigoObjeto + "</td>  ");//Código Objeto
            itensHTMListaLido.AppendLine("				<td>" + CodigoLdi + "</td>   ");//CodigoLdi
            itensHTMListaLido.AppendLine("				<td>" + NomeCliente + "</td>  ");//Nome cliente
            itensHTMListaLido.AppendLine("				<td>" + Comentario + "</td>  ");//Comentário
            itensHTMListaLido.AppendLine("				<td>" + TipoPostalNomeSiglaCodigo + "</td>   ");//Prazo
            itensHTMListaLido.AppendLine("			</tr>                 ");
            return itensHTMListaLido;
        }

        private string retornaTerceiraParteHTMLListaFaltantes()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("		</tbody>                              ");
            str.AppendLine("	</table>                                  ");
            str.AppendLine("</body>                                       ");
            str.AppendLine("</html>                                       ");
            return str.ToString();
        }

        private void EnviaEmailHTMLConsolidadoListaRecebimentoPLR(string numeroListaAtual, string horaRecebimentoPLR, string Html)
        {
            try
            {
                Configuracoes.EmailsAgenciaMae = Configuracoes.ReceberEmailsAgenciaMae();

                if (string.IsNullOrWhiteSpace(Configuracoes.EmailsAgenciaMae))
                    return;

                string[] listaEmails = Configuracoes.EmailsAgenciaMae.Split(';');

                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                cliente.Port = Convert.ToInt32("587");
                cliente.Host = "smtp.gmail.com";
                cliente.EnableSsl = true;
                cliente.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                cliente.UseDefaultCredentials = false;
                cliente.Credentials = new System.Net.NetworkCredential("accluzimangues@gmail.com", "oxmt9212");

                System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();
                email.From = new System.Net.Mail.MailAddress("AGC LUZIMANGUES <accluzimangues@gmail.com>");
                foreach (string item in listaEmails)
                {
                    //valida email
                    if (!Uteis.IsValidEmail(item.Trim())) continue;
                    email.To.Add(item.ToLowerInvariant());
                    //email.To.Add("accluzimangues@gmail.com");
                    //email.To.Add("marques-fonseca@hotmail.com");
                }
                email.Subject = "Resumo PLR [" + numeroListaAtual + "] Itens faltantes por Luzimangues às " + horaRecebimentoPLR + "";
                email.IsBodyHtml = true;
                email.Body = Html;

                cliente.Send(email);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
