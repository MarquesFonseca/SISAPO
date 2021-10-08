using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
//using Classes.Enumerador;

namespace SISAPO.ClassesDiversas
{
    /// <summary>
    /// Classe para envio de emails via SMTP.
    /// </summary>
    public class Email : IDisposable
    {
        //[NotNullValidator()]
        public string Assunto { get; set; }

        //[NotNullValidator()]
        public string Texto { get; set; }

        //[NotNullValidator()]
        public bool IsBodyHtml { get; set; }

        //[NotNullValidator()]
        public List<string> Destinatario { get; set; }

        //[NotNullValidator()]
        public string Remetente { get; set; }

        //[NotNullValidator()]
        public string Senha { get; set; }

        //[NotNullValidator()]
        public string ServidorEnvio { get; set; }

        public List<AnexoStream> AnexoStream { get; set; }

        /// <summary>
        /// Envia um email para um ou varios destinatarios com anexos a partir de um Stream
        /// </summary>
        /// <param name="Assunto">Assunto do e-mail</param>
        /// <param name="Texto">Text para o corpo do e-mail</param>
        /// <param name="IsBodyHtml">Se o texto enviado é no formato HTML ou não</param>
        /// <param name="Destinatario">Lista de destinatários</param>
        /// <param name="Remetente">Remetente do email (endereço de e-mail do remetente)</param>
        /// <param name="Senha">Senha do remetente informado</param>
        /// <param name="ServidorEnvio">Servidor SMTP para envio do email referente ao remetente informado</param>
        /// <param name="LocalArquivosAtachamento">Lista de caminhos para arquivos que devem ser anexadas ao e-mail</param>
        public void EnviaEmail()
        {
            if (ValidarEnvioEmail())
            {
                //Cria o servidor para envio da mensagem
                SmtpClient smtpServidor = new SmtpClient()
                {
                    Host = ServidorEnvio,
                    Port = 587, //Porta padrão sem autenticação
                    Credentials = new NetworkCredential(Remetente, Senha),
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false
                };

                //Cria e-mail do emitente
                MailAddress emailEmitente = new MailAddress(Remetente, string.Empty,
                    System.Text.Encoding.UTF8);

                //Cria emails dos destinatários
                List<MailAddress> emailsDestinatario = new List<MailAddress> { };
                Destinatario.ForEach(r => emailsDestinatario.Add(new MailAddress(r)));

                //Declaração da mensagem a ser enviada
                MailMessage Mensagem;

                try
                {
                    foreach (MailAddress item in emailsDestinatario)
                    {
                        //Popula a mensagem
                        Mensagem = new MailMessage(emailEmitente, item);
                        Mensagem.From = emailEmitente;
                        Mensagem.Subject = Assunto;
                        Mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
                        Mensagem.Body = Texto;
                        Mensagem.BodyEncoding = System.Text.Encoding.UTF8;
                        Mensagem.IsBodyHtml = IsBodyHtml;
                        Mensagem.Priority = MailPriority.Normal;


                        //Adiciona os anexos à mensagem
                        AnexoStream.ForEach(r => Mensagem.Attachments.Add(new Attachment(r.StreamArquivo, r.NomeAnexo)));

                        try
                        {
                            //Envia a mensagem
                            smtpServidor.Send(Mensagem);
                        }
                        catch (SmtpException Ex)
                        {
                            #region Trata erro de SMTP

                            if (Ex.StatusCode == SmtpStatusCode.MustIssueStartTlsFirst)
                            {
                                smtpServidor.Port = 587; //Porta padrão com autenticação
                                smtpServidor.EnableSsl = true; //Utiliza conexão segura

                                try
                                {
                                    //Envia usando porta com autenticação
                                    smtpServidor.Send(Mensagem);
                                    Mensagem.To.Clear();
                                }
                                catch (Exception Exx)
                                {
                                    throw Exx;
                                }
                                finally
                                {
                                    smtpServidor.Port = 25; //Retorna a porta padrão
                                    smtpServidor.EnableSsl = false;
                                }
                            }

                            #endregion
                        }
                        catch (Exception Ex)
                        {
                            throw Ex;
                        }

                        //Limpa a mensagem
                        Mensagem.To.Clear();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    Destinatario.Clear();
                    AnexoStream.Clear();
                }
            }
            else
            {
                throw new Exception("O e-mail não foi enviado por falta de dados.");
            }
        }

        private bool ValidarEnvioEmail()
        {
            try
            {
                ValidationResults resultado = Validation.Validate(this);
                return resultado.IsValid;
            }
            catch //(Exception ex)
            {
                return true;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}