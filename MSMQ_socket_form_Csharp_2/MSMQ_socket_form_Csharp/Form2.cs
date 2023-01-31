using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Sockets;
using System.Net;
using System.Messaging;
using System.IO;

namespace MSMQ_socket_form_Csharp
{
    public partial class Form2 : Form
    {
        public class MyData
        {
            public string text;
            public DateTime now;
            public double unm
            { get; set; }
        }
        string strMqName = @".\Private$\samplequeue";
        private TcpClient m_client;
        public MyData pktMsg;
        public Form2()
        {
            InitializeComponent();
        }

        private void DetectQueue()
        {

            MessageQueue msgQ = new System.Messaging.MessageQueue(strMqName);
            msgQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(MyData) });

            msgQ.ReceiveCompleted +=
            new ReceiveCompletedEventHandler(MyReceiveCompleted);

            msgQ.BeginReceive();
        }
        private void MyReceiveCompleted(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            //ObjectTextExport objTxt = new ObjectTextExport();
            StringBuilder sbFilName = new StringBuilder("");
            StringBuilder sbTemp = new StringBuilder("");

            StringBuilder sbPktMsg = new StringBuilder();

            //myGlobal.MillPlcMessage pktMsg = new myGlobal.MillPlcMessage();
           // MyData pktMsg = "";
            MessageQueue msgQ = (MessageQueue)source;

           //msgQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(myGlobal.MillPlcMessage) });
            msgQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(MyData) });

            System.Messaging.Message mg = msgQ.EndReceive(asyncResult.AsyncResult);



            //pktMsg = (myGlobal.MillPlcMessage)mg.Body;
            //pktMsg = mg.Body.ToString();
            //pktMsg = mg.Body.ToString();
            pktMsg = (MyData)mg.Body;
            //Console.WriteLine("Message: " + (string)mg.Body);
            MessageBox.Show("Message: " + pktMsg.text.ToString());
            //Send(Encoding.UTF8.GetBytes(pktMsg));
            //Code Snippet
            MethodInvoker mi = new MethodInvoker(this.UpdateUI);
            this.BeginInvoke(mi, null);
            //sbPktMsg.Remove(0, sbPktMsg.Length);
            //sbPktMsg.Append(pktMsg.PacketTime);
            //sbPktMsg.Append(",");
            //sbPktMsg.Append(pktMsg.SourceIP);
            //sbPktMsg.Append(",:,");
            //sbPktMsg.Append(pktMsg.SourcePort);
            //sbPktMsg.Append(",");
            //sbPktMsg.Append(pktMsg.DestinationIP);
            //sbPktMsg.Append(",:,");
            //sbPktMsg.Append(pktMsg.DestinationPort);
            //sbPktMsg.Append(",");
            //sbPktMsg.Append(pktMsg.PacketData);
            //sbPktMsg.Append(",");
            //sbPktMsg.Append(BitConverter.ToString(pktMsg.bPacketData));
            //sbPktMsg.Append("\n");

            //Console.WriteLine(sbPktMsg.ToString());

            //sbFilName.Remove(0, sbFilName.Length);
            //sbFilName.Append(@"c:\hbm_gw\export\");
            //sbFilName.Append(DateTime.Now.ToString("yyyyMMdd_HH"));
            //sbFilName.Append("_Plc_Result_Send_");
            //sbFilName.Append(pktMsg.SourceIP);
            //sbFilName.Append(".txt");

            //objTxt.writeFile(sbFilName.ToString(), System.Text.ASCIIEncoding.ASCII.GetBytes(sbPktMsg.ToString()));

            msgQ.BeginReceive();

        }
        private void UpdateUI()
        {
            txtData.Text = pktMsg.text.ToString();
        }



        public void Send(byte[] buffer)
        {
            try
            {
                m_client.GetStream().Write(buffer, 0, buffer.Length);
                m_client.GetStream().Flush();
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Create Tcp client.
                int nPort = 12345;
                m_client = new TcpClient("192.168.1.109", nPort);
            }
            catch (ArgumentNullException a)
            {
                Console.WriteLine("ArgumentNullException:{0}", a);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException:{0}", ex);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] btData = System.Text.Encoding.ASCII.GetBytes(txtData.Text); // Convert string to byte array.
            try
            {
                NetworkStream stream = m_client.GetStream();
                stream.Write(btData, 0, btData.Length); // Write data to server.
            }
            catch (Exception ex)
            {
                Console.WriteLine("Write Exception:{0}", ex);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            m_client.Close();
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            DetectQueue();
        }
    }
}

