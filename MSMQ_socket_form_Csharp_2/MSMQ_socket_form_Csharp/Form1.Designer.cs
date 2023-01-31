
namespace MSMQ_socket_form_Csharp
{
    partial class Form1
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
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.labIP = new System.Windows.Forms.Label();
            this.labPort = new System.Windows.Forms.Label();
            this.labStatus = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(162, 91);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(217, 29);
            this.txtIP.TabIndex = 0;
            this.txtIP.Text = "192.168.1.123";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(167, 149);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 29);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "23456";
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(162, 211);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(100, 33);
            this.btnStartServer.TabIndex = 2;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // labIP
            // 
            this.labIP.AutoSize = true;
            this.labIP.Location = new System.Drawing.Point(128, 94);
            this.labIP.Name = "labIP";
            this.labIP.Size = new System.Drawing.Size(33, 18);
            this.labIP.TabIndex = 3;
            this.labIP.Text = "IP :";
            // 
            // labPort
            // 
            this.labPort.AutoSize = true;
            this.labPort.Location = new System.Drawing.Point(115, 149);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(46, 18);
            this.labPort.TabIndex = 4;
            this.labPort.Text = "Port :";
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Location = new System.Drawing.Point(159, 45);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(90, 18);
            this.labStatus.TabIndex = 5;
            this.labStatus.Text = "Status: Stop";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(162, 266);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(325, 29);
            this.txtMessage.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.labStatus);
            this.Controls.Add(this.labPort);
            this.Controls.Add(this.labIP);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIP);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Label labIP;
        private System.Windows.Forms.Label labPort;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.TextBox txtMessage;
    }
}

