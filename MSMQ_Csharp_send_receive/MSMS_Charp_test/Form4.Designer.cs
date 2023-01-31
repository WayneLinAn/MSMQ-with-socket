
namespace MSMS_Charp_test
{
    partial class Form4
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tb_body_rcv = new System.Windows.Forms.TextBox();
            this.tb_title_snd = new System.Windows.Forms.TextBox();
            this.lmsmq = new System.Windows.Forms.Label();
            this.tb_queuename = new System.Windows.Forms.TextBox();
            this.tb_body_snd = new System.Windows.Forms.TextBox();
            this.lqueuePath = new System.Windows.Forms.Label();
            this.cb_src = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_title_rcv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_sndMsgType = new System.Windows.Forms.ComboBox();
            this.tb_ip = new System.Windows.Forms.TextBox();
            this.btn_peek = new System.Windows.Forms.Button();
            this.tb_title_peek = new System.Windows.Forms.TextBox();
            this.tb_body_peek = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 259);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(36, 304);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 22);
            this.button2.TabIndex = 1;
            this.button2.Text = "Recive";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tb_body_rcv
            // 
            this.tb_body_rcv.Location = new System.Drawing.Point(293, 304);
            this.tb_body_rcv.Name = "tb_body_rcv";
            this.tb_body_rcv.Size = new System.Drawing.Size(313, 22);
            this.tb_body_rcv.TabIndex = 2;
            // 
            // tb_title_snd
            // 
            this.tb_title_snd.Location = new System.Drawing.Point(154, 261);
            this.tb_title_snd.Name = "tb_title_snd";
            this.tb_title_snd.Size = new System.Drawing.Size(133, 22);
            this.tb_title_snd.TabIndex = 3;
            this.tb_title_snd.Text = "My Title";
            // 
            // lmsmq
            // 
            this.lmsmq.AutoSize = true;
            this.lmsmq.Font = new System.Drawing.Font("Times New Roman", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lmsmq.Location = new System.Drawing.Point(308, 35);
            this.lmsmq.Name = "lmsmq";
            this.lmsmq.Size = new System.Drawing.Size(115, 35);
            this.lmsmq.TabIndex = 4;
            this.lmsmq.Text = "MSMQ";
            // 
            // tb_queuename
            // 
            this.tb_queuename.Location = new System.Drawing.Point(154, 186);
            this.tb_queuename.Name = "tb_queuename";
            this.tb_queuename.Size = new System.Drawing.Size(275, 22);
            this.tb_queuename.TabIndex = 5;
            this.tb_queuename.Text = "samplequeue";
            // 
            // tb_body_snd
            // 
            this.tb_body_snd.Location = new System.Drawing.Point(293, 261);
            this.tb_body_snd.Name = "tb_body_snd";
            this.tb_body_snd.Size = new System.Drawing.Size(313, 22);
            this.tb_body_snd.TabIndex = 6;
            // 
            // lqueuePath
            // 
            this.lqueuePath.AutoSize = true;
            this.lqueuePath.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lqueuePath.Location = new System.Drawing.Point(46, 189);
            this.lqueuePath.Name = "lqueuePath";
            this.lqueuePath.Size = new System.Drawing.Size(86, 19);
            this.lqueuePath.TabIndex = 7;
            this.lqueuePath.Text = "QueueName";
            // 
            // cb_src
            // 
            this.cb_src.FormattingEnabled = true;
            this.cb_src.Items.AddRange(new object[] {
            "local",
            "remote"});
            this.cb_src.Location = new System.Drawing.Point(154, 113);
            this.cb_src.Name = "cb_src";
            this.cb_src.Size = new System.Drawing.Size(121, 20);
            this.cb_src.TabIndex = 8;
            this.cb_src.Text = "local";
            this.cb_src.SelectedValueChanged += new System.EventHandler(this.cb_src_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(86, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "Mode";
            // 
            // tb_title_rcv
            // 
            this.tb_title_rcv.Location = new System.Drawing.Point(154, 304);
            this.tb_title_rcv.Name = "tb_title_rcv";
            this.tb_title_rcv.Size = new System.Drawing.Size(133, 22);
            this.tb_title_rcv.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(150, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "Title";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(289, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Body";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 19);
            this.label4.TabIndex = 15;
            this.label4.Text = "Send Msg Type";
            // 
            // cb_sndMsgType
            // 
            this.cb_sndMsgType.FormattingEnabled = true;
            this.cb_sndMsgType.Items.AddRange(new object[] {
            "Plain text",
            "XML"});
            this.cb_sndMsgType.Location = new System.Drawing.Point(154, 150);
            this.cb_sndMsgType.Name = "cb_sndMsgType";
            this.cb_sndMsgType.Size = new System.Drawing.Size(121, 20);
            this.cb_sndMsgType.TabIndex = 16;
            this.cb_sndMsgType.Text = "Plain text";
            // 
            // tb_ip
            // 
            this.tb_ip.Location = new System.Drawing.Point(293, 111);
            this.tb_ip.Name = "tb_ip";
            this.tb_ip.Size = new System.Drawing.Size(313, 22);
            this.tb_ip.TabIndex = 17;
            this.tb_ip.Visible = false;
            // 
            // btn_peek
            // 
            this.btn_peek.Location = new System.Drawing.Point(36, 347);
            this.btn_peek.Name = "btn_peek";
            this.btn_peek.Size = new System.Drawing.Size(96, 22);
            this.btn_peek.TabIndex = 18;
            this.btn_peek.Text = "Peek";
            this.btn_peek.UseVisualStyleBackColor = true;
            this.btn_peek.Click += new System.EventHandler(this.btn_peek_Click);
            // 
            // tb_title_peek
            // 
            this.tb_title_peek.Location = new System.Drawing.Point(154, 347);
            this.tb_title_peek.Name = "tb_title_peek";
            this.tb_title_peek.Size = new System.Drawing.Size(133, 22);
            this.tb_title_peek.TabIndex = 20;
            // 
            // tb_body_peek
            // 
            this.tb_body_peek.Location = new System.Drawing.Point(293, 347);
            this.tb_body_peek.Name = "tb_body_peek";
            this.tb_body_peek.Size = new System.Drawing.Size(313, 22);
            this.tb_body_peek.TabIndex = 19;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tb_title_peek);
            this.Controls.Add(this.tb_body_peek);
            this.Controls.Add(this.btn_peek);
            this.Controls.Add(this.tb_ip);
            this.Controls.Add(this.cb_sndMsgType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_title_rcv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_src);
            this.Controls.Add(this.lqueuePath);
            this.Controls.Add(this.tb_body_snd);
            this.Controls.Add(this.tb_queuename);
            this.Controls.Add(this.lmsmq);
            this.Controls.Add(this.tb_title_snd);
            this.Controls.Add(this.tb_body_rcv);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form4";
            this.Text = "Send & Receive";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox tb_body_rcv;
        public System.Windows.Forms.TextBox tb_title_snd;
        private System.Windows.Forms.Label lmsmq;
        public System.Windows.Forms.TextBox tb_queuename;
        public System.Windows.Forms.TextBox tb_body_snd;
        private System.Windows.Forms.Label lqueuePath;
        private System.Windows.Forms.ComboBox cb_src;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox tb_title_rcv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_sndMsgType;
        private System.Windows.Forms.TextBox tb_ip;
        private System.Windows.Forms.Button btn_peek;
        public System.Windows.Forms.TextBox tb_title_peek;
        public System.Windows.Forms.TextBox tb_body_peek;
    }
}

