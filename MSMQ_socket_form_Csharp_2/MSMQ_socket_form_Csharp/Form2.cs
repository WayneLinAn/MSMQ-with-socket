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
using System.Threading;

namespace MSMQ_socket_form_Csharp
{
    public partial class Form2 : Form
    {
        private delegate void delUpdateUI(string sMessage);

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
        MessageQueue msgQueue;

        public Form2()
        {
            InitializeComponent();
            UpdateStatus("Status: Waiting for connection...");

        }

        private void DetectQueue()
        {
            msgQueue = new System.Messaging.MessageQueue(strMqName);
            msgQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(MyData) });

            msgQueue.ReceiveCompleted +=
            new ReceiveCompletedEventHandler(MyReceiveCompleted);

            msgQueue.BeginReceive();
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
            //MessageBox.Show("Message: " + pktMsg.text.ToString());
           // Send(Encoding.UTF8.GetBytes(pktMsg));
            send_2(pktMsg.text.ToString());

            //Code Snippet
            MethodInvoker mi = new MethodInvoker(this.UpdateUI);
            this.BeginInvoke(mi, pktMsg);
            Thread.Sleep(200);
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

            //txtData.Text = pktMsg.text.ToString();
            //send_2(pktMsg.text.ToString());
            txtData.AppendText(pktMsg.text.ToString()+"\n");
            //txtData.BringToFront();
            //btnSend.PerformClick();
            //btnSend_Click(new object(), new EventArgs());

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
               // Console.WriteLine(e.Message.ToString());
                MessageBox.Show(e.Message.ToString());

            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Create Tcp client.
                //int nPort = 12345;
                int nPort = Int32.Parse(txtPort.Text);
                m_client = new TcpClient(txtIP.Text, nPort);
                DetectQueue();
                UpdateStatus("Status: Connect to server and start detecting MSMQ!");

            }
            catch (ArgumentNullException a)
            {
                //Console.WriteLine("ArgumentNullException:{0}", a);
                MessageBox.Show("ArgumentNullException:" + a);
            }
            catch (SocketException ex)
            {
                //Console.WriteLine("SocketException:{0}", ex);
                MessageBox.Show("SocketException:" + ex);

            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //byte[] btData = System.Text.Encoding.ASCII.GetBytes(txtData.Text); // Convert string to byte array.
            byte[] btData = System.Text.Encoding.ASCII.GetBytes(txtData.Text); // Convert string to byte array.
            try
            {
                NetworkStream stream = m_client.GetStream();
                stream.Write(btData, 0, btData.Length); // Write data to server.
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Write Exception:{0}", ex);
                MessageBox.Show("Write Exception:" + ex);

            }
        }
        private void send_2(string txt)
        {
            byte[] btData = System.Text.Encoding.ASCII.GetBytes(txt); // Convert string to byte array.
            try
            {
                NetworkStream stream = m_client.GetStream();
                stream.Write(btData, 0, btData.Length); // Write data to server.
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Write Exception:{0}", ex);
                MessageBox.Show("Write Exception:" + ex);

            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            m_client.Close();
            msgQueue.ReceiveCompleted -=
             new ReceiveCompletedEventHandler(MyReceiveCompleted);
            msgQueue.Dispose();
            UpdateStatus("Status: Waiting for connection...");
        }
        private void closeReceive(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            msgQueue.EndReceive(asyncResult.AsyncResult);
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            DetectQueue();
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

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtData.Text = "";
        }
    }
}

