using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace SISAPO.ClassesDiversas
{
    public class SMS
    {
        static AutoResetEvent readNow = new AutoResetEvent(false);
        static SerialPort port;


        public string VerificaPorta(SerialPort Porta)
        {
            string retorno = string.Empty;
            string mensagem = string.Empty;
            port = Porta;
            //verificar se a porta está conectada.
            if (Porta.IsOpen == false)
                mensagem = "É necessário conectat a uma PORTA de comunicação.";
            else
            {
                string recievedData = ExecuteCommand("AT", 300);
                if (recievedData.NulloOuVazio())
                    mensagem = "Não foi possível executar perfeitamente uma conexão com a porta";
                else
                    mensagem = "OK";
            }
            return mensagem;
        }

        public bool Enviar(SerialPort Porta, string Telefone, string MensagemTexto)
        {
            try
            {

                string recievedData = ExecuteCommand("AT", 300);
                recievedData = ExecuteCommand("AT+CMGF=1", 300);
                String command = "AT+CMGS=\"" + Telefone + "\"";
                recievedData = ExecuteCommand(command, 300);
                command = MensagemTexto + char.ConvertFromUtf32(26) + "\r";
                recievedData = ExecuteCommand(command, 300);
                if (recievedData.EndsWith("\r\nOK\r\n")) recievedData = "Message sent successfully";
                if (recievedData.Contains("ERROR"))
                {
                    string recievedError = recievedData;
                    recievedError = recievedError.Trim();
                    recievedData = "Following error occured while sending the message" + recievedError;
                }
            }
            catch (Exception e)
            {
                Mensagens.Informa("Error Message: " + e.Message.Trim() + "\r\nHit any key to Exit");
                return false;
            }
            return true;
        }

        public string ExecuteCommand(string command, int timeout)
        {
            port.DiscardInBuffer();
            port.DiscardOutBuffer();
            readNow.Reset();
            port.Write(command + "\r");
            string recieved = receive(timeout);
            return recieved;
        }
        public string receive(int timeout)
        {
            string buffer = string.Empty;
            int cont = 0;
            do
            {
                if (readNow.WaitOne(timeout, false))
                {
                    string t = port.ReadExisting();
                    buffer += t;
                    cont++;
                }
                else
                {
                    return string.Empty;
                }

            }
            while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\n> ") && !buffer.Contains("ERROR"));
            return buffer;
        }
        public SerialPort EstablishConnectionPort(string portName)
        {
            try
            {
                SerialPort port = new SerialPort();
                port.PortName = portName;
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.StopBits = StopBits.One;
                port.Parity = Parity.None;
                port.ReadTimeout = 300;
                port.WriteTimeout = 300;
                port.Encoding = Encoding.GetEncoding("iso-8859-1");
                port.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
                port.Open();
                port.DtrEnable = true;
                port.RtsEnable = true;
                return port;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Close Port
        public void ClosePort(SerialPort port)
        {
            try
            {
                if (port != null)
                {
                    port.Close();
                    port.DataReceived -= new SerialDataReceivedEventHandler(DataReceived);
                    port = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Chars) readNow.Set();
        }


        //////////////////////////////////////////////////////////////////////////////




    }
}
