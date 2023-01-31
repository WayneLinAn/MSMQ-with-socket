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

namespace MSMS_Charp_test
{
    //自訂訊息內容(要發送/接收的資料格式)
   
    public partial class Form1 : Form
    {
        string IP = "10.110.125.3";
        string queuePath = @".\private$\myqueue";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMessage();
        }
        public class MyData
        {
            public string text;
            public DateTime now;            public double unm
            { get; set; }
        }
        void SendMessage()
        {
            //string queuePath = @"FormatName:DIRECT=TCP:10.125.110.3\private$\myqueue";// 使用遠程IP指定訊息佇列位置
            //string queuePath = @".\private$\myqueue";//使用本機方式指定訊息佇列位置
            //string queuePath = textBox3.Text;

            if (cb_src.Text == "local")
            {
                queuePath = @".\private$\" + textBox3.Text;
                if (!MessageQueue.Exists(queuePath))//判斷 myqueue訊息佇列是否存在
                {
                    MessageQueue.Create(queuePath);//建立用來接受/發送的訊息佇列
                }

            }
            else if (cb_src.Text == "remote")
            {
               queuePath = @"FormatName:DIRECT=TCP:" + IP+@"\private$\" + textBox3.Text;
            }
            
            MessageQueue myQueue = new MessageQueue(queuePath);
            
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
            }
            catch (Exception)
            {

               // MessageBox.Show("Send Succeeful"");

            }


        }

        void ReceiveMessage()
        {
            //string queuePath = @"FormatName:DIRECT=TCP:10.125.110.3\private$\myqueue";// 使用遠程IP指定訊息佇列位置

            //string queuePath = @".\private$\myqueue";//使用本機方式指定訊息佇列位置
            if (cb_src.Text == "local")
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
            else if (cb_src.Text == "remote")
            {
                MessageBox.Show("Remote mode, No receive");
            }
           

        }

        void ReceiveMessage_2()
        {

            //string queuePath = @".\private$\samplequeue";//使用本機方式指定訊息佇列位置
            //MessageQueue myQueue = new MessageQueue(queuePath);

            //myQueue.Formatter = new XmlMessageFormatter(new String[] { "System.string,mscorlib" });//設定接收訊息內容的型別
            //System.Messaging.Message message = myQueue.Receive();//接收訊息佇列內的訊息
            ////MyData data = (MyData)message.Body;//將訊息內容轉成正確型別
            //System.Messaging.Message.body data = message.Body;//將訊息內容轉成正確型別
            //textBox1.Text = data.;
            //MessageBox.Show(data);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReceiveMessage();
        }

        private void btn_form2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(); // This is bad
            this.Hide();
            f.ShowDialog();
            this.Close();

        }
    }
}
