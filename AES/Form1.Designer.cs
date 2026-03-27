using System.Resources;

namespace AES
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            txtMY = new TextBox();
            label2 = new Label();
            txtMW = new TextBox();
            label3 = new Label();
            btnJiaMi = new Button();
            btnJiemi = new Button();
            label4 = new Label();
            txtPYL = new TextBox();
            label5 = new Label();
            txtUrl = new TextBox();
            btnRequest = new Button();
            btnMB = new Button();
            btnGW = new Button();
            txtZH = new TextBox();
            txtPwd = new TextBox();
            label6 = new Label();
            label7 = new Label();
            button1 = new Button();
            label8 = new Label();
            txtToken = new TextBox();
            txtRootPath = new TextBox();
            button2 = new Button();
            button3 = new Button();
            lblStatus = new Label();
            button4 = new Button();
            bntFormat = new Button();
            btnEmptyResult = new Button();
            btnEmptyAll = new Button();
            txtMingW = new RichTextBox();
            chkParam = new CheckBox();
            chkWeb = new CheckBox();
            button5 = new Button();
            button6 = new Button();
            listBox1 = new ListBox();
            button7 = new Button();
            btnSMS = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 73);
            label1.Name = "label1";
            label1.Size = new Size(32, 17);
            label1.TabIndex = 0;
            label1.Text = "密钥";
            // 
            // txtMY
            // 
            txtMY.Location = new Point(85, 70);
            txtMY.Name = "txtMY";
            txtMY.Size = new Size(282, 23);
            txtMY.TabIndex = 1;
            txtMY.Text = "GVvVJyrsFRKms8XKhwfwpgB47DtIaZ2p";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 288);
            label2.Name = "label2";
            label2.Size = new Size(64, 17);
            label2.TabIndex = 0;
            label2.Text = "密文(请求)";
            // 
            // txtMW
            // 
            txtMW.Location = new Point(85, 285);
            txtMW.Multiline = true;
            txtMW.Name = "txtMW";
            txtMW.ScrollBars = ScrollBars.Vertical;
            txtMW.Size = new Size(905, 210);
            txtMW.TabIndex = 1;
            txtMW.Text = "begp++A6RxZY/R5h31KNZHcY43C1DKDz7j4JQll7xv2s9ezAc8+9LSDk2nimdJg7OFR1fyTSsS6zs0UFeYr3w4yGsJGS336uwS/uYzgzrkA=";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 515);
            label3.Name = "label3";
            label3.Size = new Size(64, 17);
            label3.TabIndex = 0;
            label3.Text = "明文(响应)";
            // 
            // btnJiaMi
            // 
            btnJiaMi.Location = new Point(712, 68);
            btnJiaMi.Name = "btnJiaMi";
            btnJiaMi.Size = new Size(75, 23);
            btnJiaMi.TabIndex = 2;
            btnJiaMi.Text = "加密";
            btnJiaMi.UseVisualStyleBackColor = true;
            btnJiaMi.Click += btnJiaMi_Click;
            // 
            // btnJiemi
            // 
            btnJiemi.Location = new Point(793, 68);
            btnJiemi.Name = "btnJiemi";
            btnJiemi.Size = new Size(75, 23);
            btnJiemi.TabIndex = 2;
            btnJiemi.Text = "解密";
            btnJiemi.UseVisualStyleBackColor = true;
            btnJiemi.Click += btnJiemi_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(412, 73);
            label4.Name = "label4";
            label4.Size = new Size(44, 17);
            label4.TabIndex = 0;
            label4.Text = "偏移量";
            // 
            // txtPYL
            // 
            txtPYL.Location = new Point(458, 70);
            txtPYL.Name = "txtPYL";
            txtPYL.Size = new Size(248, 23);
            txtPYL.TabIndex = 1;
            txtPYL.Text = "4XEUxWxkTSGcEZxe";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(28, 100);
            label5.Name = "label5";
            label5.Size = new Size(56, 17);
            label5.TabIndex = 0;
            label5.Text = "请求地址";
            // 
            // txtUrl
            // 
            txtUrl.Location = new Point(321, 97);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(521, 23);
            txtUrl.TabIndex = 1;
            txtUrl.Text = "/grjkdaGroup/getTopInfo";
            // 
            // btnRequest
            // 
            btnRequest.Location = new Point(848, 96);
            btnRequest.Name = "btnRequest";
            btnRequest.Size = new Size(75, 23);
            btnRequest.TabIndex = 3;
            btnRequest.Text = "请求";
            btnRequest.UseVisualStyleBackColor = true;
            btnRequest.Click += btnRequest_Click;
            // 
            // btnMB
            // 
            btnMB.Location = new Point(25, 12);
            btnMB.Name = "btnMB";
            btnMB.Size = new Size(75, 37);
            btnMB.TabIndex = 2;
            btnMB.Text = "慢病";
            btnMB.UseVisualStyleBackColor = true;
            btnMB.Click += btnGW_Click;
            // 
            // btnGW
            // 
            btnGW.Location = new Point(106, 12);
            btnGW.Name = "btnGW";
            btnGW.Size = new Size(75, 37);
            btnGW.TabIndex = 2;
            btnGW.Text = "公卫";
            btnGW.UseVisualStyleBackColor = true;
            btnGW.Click += btnGW_Click;
            // 
            // txtZH
            // 
            txtZH.Location = new Point(323, 13);
            txtZH.Name = "txtZH";
            txtZH.Size = new Size(155, 23);
            txtZH.TabIndex = 4;
            // 
            // txtPwd
            // 
            txtPwd.Location = new Point(535, 14);
            txtPwd.Name = "txtPwd";
            txtPwd.Size = new Size(178, 23);
            txtPwd.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(288, 17);
            label6.Name = "label6";
            label6.Size = new Size(32, 17);
            label6.TabIndex = 0;
            label6.Text = "账号";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(500, 16);
            label7.Name = "label7";
            label7.Size = new Size(32, 17);
            label7.TabIndex = 0;
            label7.Text = "密码";
            // 
            // button1
            // 
            button1.Location = new Point(732, 12);
            button1.Name = "button1";
            button1.Size = new Size(94, 23);
            button1.TabIndex = 2;
            button1.Text = "生成Token";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(41, 163);
            label8.Name = "label8";
            label8.Size = new Size(44, 17);
            label8.TabIndex = 0;
            label8.Text = "token:";
            // 
            // txtToken
            // 
            txtToken.Location = new Point(85, 158);
            txtToken.Multiline = true;
            txtToken.Name = "txtToken";
            txtToken.ScrollBars = ScrollBars.Vertical;
            txtToken.Size = new Size(905, 120);
            txtToken.TabIndex = 1;
            // 
            // txtRootPath
            // 
            txtRootPath.Location = new Point(87, 97);
            txtRootPath.Name = "txtRootPath";
            txtRootPath.Size = new Size(228, 23);
            txtRootPath.TabIndex = 4;
            // 
            // button2
            // 
            button2.Location = new Point(243, 747);
            button2.Name = "button2";
            button2.Size = new Size(128, 23);
            button2.TabIndex = 3;
            button2.Text = "提取内部内容";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(1010, 96);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 3;
            button3.Text = "切换环境";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(864, 131);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 17);
            lblStatus.TabIndex = 0;
            // 
            // button4
            // 
            button4.Location = new Point(929, 96);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 3;
            button4.Text = "明文请求";
            button4.UseVisualStyleBackColor = true;
            button4.Click += btnRequest_Click;
            // 
            // bntFormat
            // 
            bntFormat.Location = new Point(377, 747);
            bntFormat.Name = "bntFormat";
            bntFormat.Size = new Size(128, 23);
            bntFormat.TabIndex = 3;
            bntFormat.Text = "结果格式化";
            bntFormat.UseVisualStyleBackColor = true;
            bntFormat.Click += bntFormat_Click;
            // 
            // btnEmptyResult
            // 
            btnEmptyResult.Location = new Point(511, 747);
            btnEmptyResult.Name = "btnEmptyResult";
            btnEmptyResult.Size = new Size(128, 23);
            btnEmptyResult.TabIndex = 3;
            btnEmptyResult.Text = "清空结果";
            btnEmptyResult.UseVisualStyleBackColor = true;
            btnEmptyResult.Click += btnEmptyResult_Click;
            // 
            // btnEmptyAll
            // 
            btnEmptyAll.Location = new Point(645, 747);
            btnEmptyAll.Name = "btnEmptyAll";
            btnEmptyAll.Size = new Size(128, 23);
            btnEmptyAll.TabIndex = 3;
            btnEmptyAll.Text = "全部清空";
            btnEmptyAll.UseVisualStyleBackColor = true;
            btnEmptyAll.Click += btnEmptyAll_Click;
            // 
            // txtMingW
            // 
            txtMingW.Location = new Point(87, 515);
            txtMingW.Name = "txtMingW";
            txtMingW.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtMingW.Size = new Size(895, 220);
            txtMingW.TabIndex = 5;
            txtMingW.Text = "";
            // 
            // chkParam
            // 
            chkParam.AutoSize = true;
            chkParam.Location = new Point(874, 69);
            chkParam.Name = "chkParam";
            chkParam.Size = new Size(63, 21);
            chkParam.TabIndex = 6;
            chkParam.Text = "无参数";
            chkParam.UseVisualStyleBackColor = true;
            // 
            // chkWeb
            // 
            chkWeb.AutoSize = true;
            chkWeb.Location = new Point(941, 68);
            chkWeb.Name = "chkWeb";
            chkWeb.Size = new Size(51, 21);
            chkWeb.TabIndex = 6;
            chkWeb.Text = "前端";
            chkWeb.UseVisualStyleBackColor = true;
            chkWeb.CheckedChanged += chkWeb_CheckedChanged;
            // 
            // button5
            // 
            button5.Location = new Point(81, 747);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 2;
            button5.Text = "加密";
            button5.UseVisualStyleBackColor = true;
            button5.Click += btnJiaMi_Click;
            // 
            // button6
            // 
            button6.Location = new Point(162, 747);
            button6.Name = "button6";
            button6.Size = new Size(75, 23);
            button6.TabIndex = 2;
            button6.Text = "解密";
            button6.UseVisualStyleBackColor = true;
            button6.Click += btnJiemi_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(996, 163);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(348, 650);
            listBox1.TabIndex = 7;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            listBox1.DoubleClick += listBox1_DoubleClick;
            // 
            // button7
            // 
            button7.Location = new Point(995, 126);
            button7.Name = "button7";
            button7.Size = new Size(113, 23);
            button7.TabIndex = 3;
            button7.Text = "初始化数据库";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // btnSMS
            // 
            btnSMS.Location = new Point(187, 14);
            btnSMS.Name = "btnSMS";
            btnSMS.Size = new Size(75, 37);
            btnSMS.TabIndex = 2;
            btnSMS.Text = "短信平台";
            btnSMS.UseVisualStyleBackColor = true;
            btnSMS.Click += btnGW_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1356, 832);
            Controls.Add(listBox1);
            Controls.Add(chkWeb);
            Controls.Add(chkParam);
            Controls.Add(txtMingW);
            Controls.Add(txtPwd);
            Controls.Add(txtRootPath);
            Controls.Add(txtZH);
            Controls.Add(btnEmptyAll);
            Controls.Add(btnEmptyResult);
            Controls.Add(bntFormat);
            Controls.Add(button2);
            Controls.Add(button7);
            Controls.Add(button3);
            Controls.Add(button4);
            Controls.Add(btnRequest);
            Controls.Add(button6);
            Controls.Add(btnJiemi);
            Controls.Add(btnSMS);
            Controls.Add(btnGW);
            Controls.Add(button5);
            Controls.Add(btnMB);
            Controls.Add(btnJiaMi);
            Controls.Add(label3);
            Controls.Add(txtToken);
            Controls.Add(label8);
            Controls.Add(txtMW);
            Controls.Add(label2);
            Controls.Add(txtUrl);
            Controls.Add(txtPYL);
            Controls.Add(lblStatus);
            Controls.Add(label4);
            Controls.Add(txtMY);
            Controls.Add(label5);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label1);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "加解密工具";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnJiaMi;
        private System.Windows.Forms.Button btnJiemi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPYL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.Button btnMB;
        private System.Windows.Forms.Button btnGW;
        private System.Windows.Forms.TextBox txtZH;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.TextBox txtRootPath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblStatus;
        private Button button4;
        private Button bntFormat;
        private Button btnEmptyResult;
        private Button btnEmptyAll;
        private RichTextBox txtMingW;
        private CheckBox chkParam;
        private CheckBox chkWeb;
        private Button button5;
        private Button button6;
        private ListBox listBox1;
        private Button button7;
        private Button btnSMS;
    }
}
