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
    public partial class Form3 : Form
    {
        ObjectTextExport txtExport = new ObjectTextExport();
        public enum disconnect_type:int
        {
            click = 0,
            reconnect =1
        }
        disconnect_type reconnect_mode = disconnect_type.reconnect;

        private delegate void delUpdateUI(string sMessage);

        public class MyData //可自訂參數Header以及body格式
        {
            public string text;
            public DateTime now;
            public double unm
            { get; set; }
        }
        string strMqName = @".\Private$\samplequeue"; //可自訂從何主題獲取MSMQ
        private TcpClient m_client;
        string LocalIP = "10.110.125.1";
        string Localport = "8001";
        string log_path = @"c:\MSMQ_Socket\export\";
        string log_file_name = "MSMQ_Socket.log";
        int reconnect_counter = 0;



        public MyData pktMsg;
        MessageQueue msgQueue;

        public struct MillPlcMessage
        {
            public string PacketTime;
            public string SourceIP;
            public string SourcePort;
            public string DestinationIP;
            public string DestinationPort;
            public string PacketData;
            public byte[] bPacketData;
        }

        public enum SendLogType : int
        {
            try_to_send = 0,
            receive_ack = 1,
            send_successful = 2,
            plain_text = 3
        }
        //private void addlog(string log_content)
        //{
        //    StringBuilder sbPktMsg = new StringBuilder("");

        //    StringBuilder sbFilName = new StringBuilder("");
        //    sbFilName.Remove(0, sbFilName.Length);
        //    sbFilName.Append(@"c:\MSMQ_Socket\export\");
        //    sbFilName.Append(DateTime.Now.ToString("yyyyMMdd_HH"));
        //    sbFilName.Append("_MSMQ_Socket.log");

        //    txtExport.writeFile(sbFilName.ToString(),
        //        System.Text.ASCIIEncoding.ASCII.GetBytes(log_content+"\n"));
        //}

        private void addlog(string log_content,SendLogType send_log_type)
        {

            StringBuilder sbPktMsg = new StringBuilder("");
            if (send_log_type== SendLogType.try_to_send)
            {
                sbPktMsg.Remove(0, sbPktMsg.Length);
                sbPktMsg.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff"));
                sbPktMsg.Append(",");
                sbPktMsg.Append("Try to send Data:         ");
                sbPktMsg.Append("From:{");
                sbPktMsg.Append(LocalIP + ":" + Localport);
                sbPktMsg.Append("},");
                sbPktMsg.Append("To:{");
                sbPktMsg.Append(txtIP.Text.ToString() + ":" + txtPort.Text.ToString());
                sbPktMsg.Append("},");
                sbPktMsg.Append("msg content:[");
                sbPktMsg.Append(log_content);
                sbPktMsg.Append("]");
                //sbPktMsg.Append(",");
                //sbPktMsg.Append(strHex);
                sbPktMsg.Append("\n");
            }
            else if (send_log_type==SendLogType.send_successful)
            {
                sbPktMsg.Remove(0, sbPktMsg.Length);
                sbPktMsg.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff"));
                sbPktMsg.Append(",");
                sbPktMsg.Append("Send Data successfully:   ");
                sbPktMsg.Append("From:{");
                sbPktMsg.Append(LocalIP + ":" + Localport);
                sbPktMsg.Append("},");
                sbPktMsg.Append("To:{");
                sbPktMsg.Append(txtIP.Text.ToString() + ":" + txtPort.Text.ToString());
                sbPktMsg.Append("},");
                sbPktMsg.Append("msg content:[");
                sbPktMsg.Append(log_content);
                sbPktMsg.Append("]");
                //sbPktMsg.Append(",");
                //sbPktMsg.Append(strHex);
                sbPktMsg.Append("\n");
            }
            else if(send_log_type== SendLogType.receive_ack)
            {
                sbPktMsg.Remove(0, sbPktMsg.Length);
                sbPktMsg.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff"));
                sbPktMsg.Append(",");
                sbPktMsg.Append("Receive ack successfully: ");
                sbPktMsg.Append("From:{");
                sbPktMsg.Append(txtIP.Text.ToString() + ":" + txtPort.Text.ToString());
                sbPktMsg.Append("},");
                sbPktMsg.Append("To:{");
                sbPktMsg.Append(LocalIP + ":" + Localport);
                sbPktMsg.Append("},");
                sbPktMsg.Append("msg content:[");
                sbPktMsg.Append(log_content);
                sbPktMsg.Append("]");
                //sbPktMsg.Append(",");
                //sbPktMsg.Append(strHex);
                sbPktMsg.Append("\n");
            }
            else if (send_log_type == SendLogType.plain_text)
            {
                sbPktMsg.Remove(0, sbPktMsg.Length);
                sbPktMsg.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff"));
                sbPktMsg.Append(",");
                sbPktMsg.Append(log_content);
                sbPktMsg.Append("\n");
            }


            StringBuilder sbFilName = new StringBuilder("");
            sbFilName.Remove(0, sbFilName.Length);
            sbFilName.Append(log_path);
            sbFilName.Append(DateTime.Now.ToString("yyyyMMdd_HH_"));
            sbFilName.Append(log_file_name);

            txtExport.writeFile(sbFilName.ToString(),
                System.Text.ASCIIEncoding.ASCII.GetBytes(sbPktMsg.ToString()));
            //txtData.AppendText("abv" + "\n");
            //txtData.ScrollToCaret();
        }

       
        public Form3()
        {
            InitializeComponent();
            UpdateStatus("Status: Waiting for connection...");
            //addlog("Program Start");
            addlog("Program Start", SendLogType.plain_text);
        }
        private void PeekQueue()
        {
            reconnect_counter = 0;
            msgQueue = new System.Messaging.MessageQueue(strMqName);
            msgQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(MyData) });
            msgQueue.PeekCompleted += new
                PeekCompletedEventHandler(MyPeekCompleted);

            msgQueue.BeginPeek();
        }
        private void MyPeekCompleted(Object source,
            PeekCompletedEventArgs asyncResult)
        {

            // Connect to the queue.
            MessageQueue mq = (MessageQueue)source;

            // End the asynchronous peek operation.
            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(MyData) });
            System.Messaging.Message m = mq.EndPeek(asyncResult.AsyncResult);

            // Display message information on the screen.
            pktMsg = (MyData)m.Body;
            if (Send_ReadAck(pktMsg.text.ToString()))
            {
                //Code Snippet
                MethodInvoker mi = new MethodInvoker(this.UpdateUI);
                this.BeginInvoke(mi, pktMsg);
                Thread.Sleep(200);
            }
            else
            {

                //reconnect_counter++;
                ////Thread.Sleep(3000);
                //if (!m_client.Connected && reconnect_counter>=3)
                //{
                //    disconnect_with_type(disconnect_type.reconnect);
                //    //goto reconnect;
                //}
                //else
                //{
                //    addlog("Can't send msg, reconnect counter[" + reconnect_counter + "]", SendLogType.plain_text);
                //}
                //MessageBox.Show("Send_ReadAck("+ pktMsg.text.ToString() + ") fail");
                //MessageBox.Show("m_client.Connected(" + m_client.Connected + ")");
                if (!m_client.Connected)
                {
                    disconnect_with_type(disconnect_type.reconnect);
                }
            }
            // Restart the asynchronous peek operation.
            mq.BeginPeek();

            return;
        }
        #region detect Queue 
        private void DetectQueue()
        {
            msgQueue = new System.Messaging.MessageQueue(strMqName);
            msgQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(MyData) });

            msgQueue.ReceiveCompleted +=
            new ReceiveCompletedEventHandler(MyReceiveCompleted);

            //Code Snippet
            MethodInvoker mi = new MethodInvoker(this.UpdateUI);
            this.BeginInvoke(mi, pktMsg);
            Thread.Sleep(200);

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
            //send_2(pktMsg.text.ToString());
            Send_ReadAck(pktMsg.text.ToString());

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
        #endregion 
        private void UpdateUI()
        {

            //txtData.Text = pktMsg.text.ToString();
            //send_2(pktMsg.text.ToString());
            txtData.AppendText(pktMsg.text.ToString() + "\n");
            txtData.ScrollToCaret();
            //txtData.BringToFront();
            //btnSend.PerformClick();
            //btnSend_Click(new object(), new EventArgs());
        }


        public bool Read_Ack_2(NetworkStream ns)
        {
            try
            {
                Byte[] bytes;
                string msg = "";
                if (m_client.ReceiveBufferSize > 0)
                {
                    bytes = new byte[m_client.ReceiveBufferSize];
                    ns.ReadTimeout = 3000;
                    ns.Read(bytes, 0, m_client.ReceiveBufferSize);
                    msg = Encoding.ASCII.GetString(bytes); //the message incoming
                    string[] words = msg.Split(' ');
                    addlog(words[0], SendLogType.receive_ack);
                }
                if (msg.Contains("Ack")) //需要的ack格式在此修改，目前假定字串中有Ack字樣就會通過
                {
                    //addlog("Receive Ack successful");
                    return true;
                }

                return false;



            }
            catch (Exception ex)
            {
                return false;
            }

        }

        
        private bool connect()
        {
            try
            {
                int nPort = Int32.Parse(txtPort.Text);
                //reconnect:
                reconnect:
                try
                {
                    m_client = new TcpClient(txtIP.Text, nPort);
                }
                catch (Exception)
                {
                    addlog("Connect fail, wait for 5 second to reconnect.", SendLogType.plain_text);
                    Thread.Sleep(5000);
                    goto reconnect;
                }
                
                
                PeekQueue();
                reconnect_mode = disconnect_type.reconnect;
                UpdateStatus("Status: Connect to server and start detecting MSMQ!");
                addlog("Connect to server and start detecting MSMQ!", SendLogType.plain_text);

                return true;

            }
            catch (ArgumentNullException a)
            {
                MessageBox.Show("ArgumentNullException:" + a);
                return false;
            }
            catch (SocketException ex)
            {
                MessageBox.Show("SocketException:" + ex);
                return false;

            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    while (!connect())
            //    {
            //        log connect fail try to  reconnect 
            //        disconnect_with_type(disconnect_type.reconnect);
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            connect();
            //StartClient();
            //log connect successfully
        }

        private void StartClient()
        {
            string serverHost = "10.110.125.1";
            int serverPort = 8000;
            TcpClient client = null;
            NetworkStream stream = null;

        connect:
            try
            {
                // connect to the server
                client = new TcpClient(serverHost, serverPort);
                //stream = client.GetStream();
                Console.WriteLine("Connected to server.");

                //// send data to the server
                //string message = "Hello, server!";
                //byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                //stream.Write(data, 0, data.Length);

                //// receive data from the server
                //data = new byte[1024];
                //int bytes = stream.Read(data, 0, data.Length);
                //string responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //Console.WriteLine("Received from server: " + responseData);
                PeekQueue();
                reconnect_mode = disconnect_type.reconnect;
                UpdateStatus("Status: Connect to server and start detecting MSMQ!");
                addlog("Connect to server and start detecting MSMQ!", SendLogType.plain_text);

                // close the client and stream
                //stream.Close();
                //client.Close();
            }
            catch (Exception ex)
            {
                // if an exception occurs, wait 5 seconds and try again
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Retrying in 5 seconds...");
                Thread.Sleep(5000);
                goto connect;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //byte[] btData = System.Text.Encoding.ASCII.GetBytes(txtData.Text); // Convert string to byte array.
            //byte[] btData = System.Text.Encoding.ASCII.GetBytes(txtData.Text); // Convert string to byte array.
            //try
            //{
            //    NetworkStream stream = m_client.GetStream();
            //    stream.Write(btData, 0, btData.Length); // Write data to server.
            //}
            //catch (Exception ex)
            //{
            //    //Console.WriteLine("Write Exception:{0}", ex);
            //    MessageBox.Show("Write Exception:" + ex);

            //}
        }

        private bool Send_ReadAck(string txt)
        {
            
    
            try
            {
                NetworkStream stream = m_client.GetStream();
                byte[] btData = System.Text.Encoding.ASCII.GetBytes(txt); // Convert string to byte array.
                stream.Write(btData, 0, btData.Length); // Write data to server.
                //addlog("Try to send Data:");
                addlog(txt,SendLogType.try_to_send);

                int counter = 0;
                while (!Read_Ack_2(stream) && counter < 3)
                {
                    UpdateStatus("Waitting for Ack");
                    counter++;
                }
                if (counter >= 3)
                {
                    //disconnect();
                    
                    disconnect_with_type(disconnect_type.reconnect);
                    
                    return false;
                }
                else
                {
                   
                    UpdateStatus("Data sent successfully, keep peeking MSMQ");
                    //MessageBox.Show("Data sent successfully");
                    delete_first_queue();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Write Exception:{0}", ex);
                //MessageBox.Show("Write Exception:" + ex);
                return false;


            }
        }
        void delete_first_queue()
        {
            string queuePath = strMqName;//使用本機方式指定訊息佇列位置
            MessageQueue myQueue = new MessageQueue(queuePath);

            try
            {

                myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(MyData) });//設定接收訊息內容的型別
                TimeSpan timeout = new TimeSpan(0, 0, 1);
                System.Messaging.Message message = myQueue.Receive(timeout);//接收訊息佇列內的訊息

                MyData data = (MyData)message.Body;//將訊息內容轉成正確型別
                addlog(data.text.ToString(),SendLogType.send_successful);

                //addlog_msg(data.text.ToString());
                //tb_body_rcv.Text = data.text.ToString();
                //tb_title_rcv.Text = message.Label.ToString();
                //MessageBox.Show(data.text.ToString());
            }
            catch
            {
                MessageBox.Show("No message");
                //tb_body_rcv.Text = "";
                //tb_title_rcv.Text = "";
            }

        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            //disconnect();
            disconnect_with_type(disconnect_type.click);
        }

        private void disconnect_with_type(disconnect_type type)
        {
            try
            {

                if (type == disconnect_type.click)
                {
                    if (m_client!=null)
                    {
                        m_client.Close();
                    }
                    if (msgQueue!=null)
                    {
                        msgQueue.PeekCompleted -= new
                            PeekCompletedEventHandler(MyPeekCompleted);


                        msgQueue.Dispose();
                    }
                    UpdateStatus("Status: Waiting for connection...");
                    addlog("Click disconnect!", SendLogType.plain_text);

                    reconnect_mode = disconnect_type.click;
                    
                    
                }
                else if (type == disconnect_type.reconnect && reconnect_mode == disconnect_type.reconnect)
                {
                    m_client.Close();
                    //msgQueue.ReceiveCompleted -=
                    // new ReceiveCompletedEventHandler(MyReceiveCompleted);
                    msgQueue.PeekCompleted -= new
                        PeekCompletedEventHandler(MyPeekCompleted);
                    msgQueue.Dispose();
                    UpdateStatus("Status: Trying to reconnect...");
                    addlog("Time out waiting for Ack, Trying to reconnect!", SendLogType.plain_text);

                    Thread.Sleep(5000);
                    connect();
                }
                

            }
            catch (Exception)
            {

                MessageBox.Show("disconnect_with_type("+ type + ") fail");
            }

        }
     

        private void btnDetect_Click(object sender, EventArgs e)
        {
            DetectQueue();
        }

        private void UpdateStatus(string sStatus)
        {
            if (reconnect_mode==disconnect_type.reconnect)
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
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtData.Text = "";
        }

        #region async
        //private TcpClient client;
        //int server_port = 12345;
        //public async Task StartAsync()
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            if (client == null)
        //            {
        //                client = new TcpClient();
        //                await client.ConnectAsync("10.110.125.3", server_port);
        //                Console.WriteLine("Connected to server.");

        //                // Start a new task to handle data received from the server
        //                _ = Task.Run(() => HandleServerDataAsync(client));
        //            }

        //            // Do your work here...

        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Connection error: {ex.Message}");
        //            client.Dispose();
        //            client = null;
        //        }

        //        await Task.Delay(1000);
        //    }
        //}

        //private async Task HandleServerDataAsync(TcpClient client)
        //{
        //    try
        //    {
        //        NetworkStream stream = client.GetStream();
        //        byte[] buffer = new byte[1024];
        //        int bytesRead;
        //        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        //        {
        //            // Handle data received from the server
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error handling server data: {ex.Message}");
        //        client.Dispose();
        //        client = null;
        //    }
        //}
        #endregion

        #region chatgpt ans
        //public void Start()
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            if (m_client == null)
        //            {
        //                int nPort = Int32.Parse(txtPort.Text);
        //                m_client = new TcpClient(txtIP.Text, nPort);
        //                //m_client.Connect("server_address", server_port);
        //                //Console.WriteLine("Connected to server.");
        //                MessageBox.Show("Connected to server.");
        //            }

        //            // Do your work here...
        //            DetectQueue();
        //            UpdateStatus("Status: Connect to server and start detecting MSMQ!");

        //        }
        //        catch (SocketException)
        //        {
        //            Console.WriteLine("Lost connection to server.");
        //            m_client = null;
        //        }

        //        Thread.Sleep(1000);
        //    }
        //}
        #endregion

    }
}

