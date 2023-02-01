using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;


namespace MSMQ_socket_form_Csharp
{
    public partial class Server : Form
    {

        private delegate void delUpdateUI(string sMessage);

        TcpListener m_server;
        Thread m_thrListening; // 持續監聽是否有Client連線及收值的執行緒
        public Server()
        {
            InitializeComponent();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            try
            {
                int nPort = Convert.ToInt32(txtPort.Text); // 設定 Port
                IPAddress localAddr = IPAddress.Parse(txtIP.Text); // 設定 IP

                // Create TcpListener 並開始監聽
                m_server = new TcpListener(localAddr, nPort);
                m_server.Start();
                m_thrListening = new Thread(Listening);
                m_thrListening.Start();
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
            }
        }

        private void Listening()
        {
            try
            {
                byte[] btDatas = new byte[256]; // Receive data buffer
                string sData = null;

                while (true)
                {
                    UpdateStatus("Waiting for connection...");

                    TcpClient client = m_server.AcceptTcpClient(); // 要等有Client建立連線後才會繼續往下執行
                    UpdateStatus("Connect to client!");

                    sData = null;
                    NetworkStream stream = client.GetStream();

                    int i;
                    while ((i = stream.Read(btDatas, 0, btDatas.Length)) != 0) // 當有資料傳入時將資料顯示至介面上
                    {
                        sData = System.Text.Encoding.ASCII.GetString(btDatas, 0, i);
                        UdpateMessage("Received Data:" + sData);
                        Thread.Sleep(5);
                    }

                    client.Close();
                    Thread.Sleep(5);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
            }
        }

        private void UpdateStatus(string sStatus)
        {
            if (this.InvokeRequired)
            {
                delUpdateUI del = new delUpdateUI(UpdateStatus);
                this.Invoke(del, sStatus);
            }
            else
            {
                labStatus.Text = sStatus;
            }
        }

        private void UdpateMessage(string sReceiveData)
        {
            if (this.InvokeRequired)
            {
                delUpdateUI del = new delUpdateUI(UdpateMessage);
                this.Invoke(del, sReceiveData);
            }
            else
            {
                txtMessage.Text += sReceiveData + Environment.NewLine;
            }
        }

    }
}

