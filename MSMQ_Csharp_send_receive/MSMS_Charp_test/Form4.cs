using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Messaging;
using System.IO;

namespace MSMS_Charp_test
{
    //自訂訊息內容(要發送/接收的資料格式)
   
    public partial class Form4 : Form
    {

        string IP = "10.110.125.3";
        string queuePath = @".\private$\myqueue";
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        [Serializable]
        public class MillPlcMessage
        {
            public string PacketTime;
            public string SourceIP;
            public string SourcePort;
            public string DestinationIP;
            public string DestinationPort;
            public string PacketData;
            public byte[] bPacketData;
        }
        public class MyData
        {
            public string text;
            public DateTime now;
            public double unm
            { get; set; }
        }
        int SendMessage()
        {
            //string queuePath = @"FormatName:DIRECT=TCP:10.125.110.3\private$\myqueue";// 使用遠程IP指定訊息佇列位置
            //string queuePath = @".\private$\myqueue";//使用本機方式指定訊息佇列位置
            //string queuePath = textBox3.Text;

            if (cb_src.Text == "local")
            {
                queuePath = @".\private$\" + tb_queuename.Text;
                if (!MessageQueue.Exists(queuePath))//判斷 myqueue訊息佇列是否存在
                {
                    MessageQueue.Create(queuePath);//建立用來接受/發送的訊息佇列
                }

            }
            else if (cb_src.Text == "remote")
            {
                while (tb_ip.Text=="")
                {
                    MessageBox.Show("Please enter ip");
                    return -1;
                }
                IP = tb_ip.Text.ToString();
                queuePath = @"FormatName:DIRECT=TCP:" + IP+@"\private$\" + tb_queuename.Text;
                
            }

            MessageQueue myQueue = new MessageQueue(queuePath);
            if (cb_sndMsgType.Text =="Plain text")
            {
                string snd_txt = "";
                //要發送的內容
                if (tb_body_snd.Text == "")
                {
                    MessageBox.Show("send \"Test Message\"");

                    snd_txt = "Test Message";
                }
                else
                {
                    snd_txt = tb_body_snd.Text;
                }
                try
                {
                    //發送訊息
                    var msg = new System.Messaging.Message();
                    msg.BodyStream = new MemoryStream(Encoding.Unicode.GetBytes(snd_txt));
                    //msg.Body = "12345";
                    myQueue.Send(msg, tb_title_snd.Text);
                    tb_body_rcv.Text = "";
                    MessageBox.Show("Send Successful");
                }
                catch (Exception)
                {
                    MessageBox.Show("Send fail");
                }
            }
            else if (cb_sndMsgType.Text == "XML")
            {
                //要發送的內容
                MyData data = new MyData();
                if (tb_body_snd.Text == "")
                {
                    MessageBox.Show("send \"Test Message\"");

                    data.text = "Test Message";
                }
                else
                {
                    data.text = tb_body_snd.Text;
                }
                data.now = DateTime.Now;
                data.unm = DateTime.Now.Second;
                try
                {
                    //發送訊息
                    myQueue.Send(data, tb_title_snd.Text);
                    tb_body_rcv.Text = "";
                    MessageBox.Show("Send Succeeful");

                }
                catch (Exception)
                {

                    MessageBox.Show("Send fail");

                }

            }
            return 0;

        }

        void ReceiveMessage()
        {
            //string queuePath = @"FormatName:DIRECT=TCP:10.125.110.3\private$\myqueue";// 使用遠程IP指定訊息佇列位置

            string queuePath = @".\private$\"+ tb_queuename.Text;//使用本機方式指定訊息佇列位置
            if (cb_src.Text == "local")
            {
                if (cb_sndMsgType.Text == "Plain text")
                {
                    try
                    {

                        MessageQueue myQueue = new MessageQueue(queuePath);

                        myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(MyData) });//設定接收訊息內容的型別
                        TimeSpan timeout = new TimeSpan(0, 0, 1);
                        System.Messaging.Message message = myQueue.Receive(timeout);//接收訊息佇列內的訊息

                        MyData data = (MyData)message.Body;//將訊息內容轉成正確型別
                        tb_body_rcv.Text = data.text.ToString();
                        tb_title_rcv.Text = message.Label.ToString();
                        //MessageBox.Show(data.text.ToString());
                    }
                    catch
                    {
                        MessageBox.Show("No message");
                        tb_body_rcv.Text = "";
                        tb_title_rcv.Text = "";
                    }
                }
                else if (cb_sndMsgType.Text == "XML")
                {
                    try
                    {

                        MessageQueue myQueue = new MessageQueue(queuePath);

                        myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(MyData) });//設定接收訊息內容的型別
                        TimeSpan timeout = new TimeSpan(0, 0, 1);
                        System.Messaging.Message message = myQueue.Receive(timeout);//接收訊息佇列內的訊息

                        MyData data = (MyData)message.Body;//將訊息內容轉成正確型別
                        tb_body_rcv.Text = data.text.ToString();
                        tb_title_rcv.Text = message.Label.ToString();
                        // MessageBox.Show(data.text.ToString());
                    }
                    catch
                    {
                        MessageBox.Show("No message");
                        tb_body_rcv.Text = "";
                        tb_title_rcv.Text = "";
                    }
                }
                

            }
            else if (cb_src.Text == "remote")
            {
                MessageBox.Show("Remote mode, No receive");
            }
           

        }

        void ReceiveMessage_2()
        {
            string queuePath = @".\private$\" + tb_queuename.Text;//使用本機方式指定訊息佇列位置
            MessageQueue myQueue = new MessageQueue(queuePath);
            if (cb_src.Text == "local")
            {
                if (cb_sndMsgType.Text == "Plain text")
                {
                    try
                    {
                        System.Messaging.Message received;
                        string lastReceived;
                        Stream bodyStream = null;
                        int bufLength = 512;
                        byte[] buffer = new byte[bufLength];
                        TimeSpan timeout = new TimeSpan(0, 0, 1);

                        received = myQueue.Receive(timeout);
                        bodyStream = received.BodyStream;
                        bodyStream.Read(buffer, 0, bufLength);
                        lastReceived = Encoding.Unicode.GetString(buffer, 0, buffer.Length);

                        tb_body_rcv.Text = lastReceived;
                        tb_title_rcv.Text = received.Label.ToString();
                        //MessageBox.Show(lastReceived);

                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("No message");
                        tb_body_rcv.Text = "";
                        tb_title_rcv.Text = "";
                        //MessageBox.Show(exc.ToString(), "Exception");
                    }
                }
                else if (cb_sndMsgType.Text == "XML")
                {
                    try
                    {

                        myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(MyData) });//設定接收訊息內容的型別
                        TimeSpan timeout = new TimeSpan(0, 0, 1);
                        System.Messaging.Message message = myQueue.Receive(timeout);//接收訊息佇列內的訊息

                        MyData data = (MyData)message.Body;//將訊息內容轉成正確型別
                        tb_body_rcv.Text = data.text.ToString();
                        tb_title_rcv.Text = message.Label.ToString();
                        // MessageBox.Show(data.text.ToString());
                    }
                    catch
                    {
                        MessageBox.Show("No message");
                        tb_body_rcv.Text = "";
                        tb_title_rcv.Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Remote mode, No receive");

            }
        }
        void PeekMessage()
        {
            string queuePath = @".\private$\" + tb_queuename.Text;//使用本機方式指定訊息佇列位置
            MessageQueue myQueue = new MessageQueue(queuePath);
            if (cb_src.Text == "local")
            {
                if (cb_sndMsgType.Text == "Plain text")
                {
                    try
                    {
                        System.Messaging.Message received;
                        string lastReceived;
                        Stream bodyStream = null;
                        int bufLength = 512;
                        byte[] buffer = new byte[bufLength];
                        TimeSpan timeout = new TimeSpan(0, 0, 1);
                        received = myQueue.Peek(timeout);
                        //received = myQueue.Receive(timeout);
                        bodyStream = received.BodyStream;
                        bodyStream.Read(buffer, 0, bufLength);
                        lastReceived = Encoding.Unicode.GetString(buffer, 0, buffer.Length);

                        tb_body_peek.Text = lastReceived;
                        tb_title_peek.Text = received.Label.ToString();
                        //MessageBox.Show(lastReceived);

                    }
                    catch (Exception exc)
                    {
                        //MessageBox.Show("No message");
                        tb_body_peek.Text = "";
                        tb_title_peek.Text = "";
                        //MessageBox.Show(exc.ToString(), "Exception");
                    }
                }
                else if (cb_sndMsgType.Text == "XML")
                {
                    try
                    {

                        myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(MyData) });//設定接收訊息內容的型別
                        TimeSpan timeout = new TimeSpan(0, 0, 1);
                        System.Messaging.Message message = myQueue.Receive(timeout);//接收訊息佇列內的訊息

                        MyData data = (MyData)message.Body;//將訊息內容轉成正確型別
                        tb_body_peek.Text = data.text.ToString();
                        tb_title_peek.Text = message.Label.ToString();
                        // MessageBox.Show(data.text.ToString());
                    }
                    catch
                    {
                        //MessageBox.Show("No message");
                        tb_body_peek.Text = "";
                        tb_title_peek.Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Remote mode, No receive");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReceiveMessage_2();
        }

        private void btn_form1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1(); // This is bad
            this.Hide();
            f.ShowDialog();
            this.Close();

        }

        private void cb_src_SelectedValueChanged(object sender, EventArgs e)
        {
            if (true)
            {
                tb_ip.Visible = true;
            }
        }

        private void btn_peek_Click(object sender, EventArgs e)
        {
            PeekMessage();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PeekMessage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("usage: {0} {1} {2} {3}", "[LOCAL_IP] ", "[REMOTE_IP] ", "[socket_PORT] ", "[MSMQ_NAME]");
            Console.ReadLine();
        }
    }
}
