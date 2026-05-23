namespace AES
{
    partial class ExecuteSQLForm
    {
        private System.ComponentModel.IContainer components = null;

        // 控件声明 - 将 TextBox 改为 RichTextBox
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.ListView listViewScripts;
        private System.Windows.Forms.RichTextBox txtLog;  // 改为 RichTextBox
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox groupBoxScripts;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblConn;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colExecuteTime;
        private System.Windows.Forms.ColumnHeader colErrorMessage;
        private System.Windows.Forms.ColumnHeader colFilePath;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuScripts;
        private System.Windows.Forms.ToolStripMenuItem executeSelectedToolStripMenuItem;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.CheckBox chkStopOnError;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.ComboBox cmbDbType;
        private System.Windows.Forms.Label lblDbType;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnSelectFolder = new Button();
            btnExecute = new Button();
            listViewScripts = new ListView();
            colFileName = new ColumnHeader();
            colStatus = new ColumnHeader();
            colExecuteTime = new ColumnHeader();
            colErrorMessage = new ColumnHeader();
            colFilePath = new ColumnHeader();
            contextMenuScripts = new ContextMenuStrip(components);
            executeSelectedToolStripMenuItem = new ToolStripMenuItem();
            txtLog = new RichTextBox();
            lblStatus = new Label();
            groupBoxScripts = new GroupBox();
            groupBoxLog = new GroupBox();
            btnClearLog = new Button();
            btnTestConnection = new Button();
            progressBar = new ProgressBar();
            lblConn = new Label();
            toolTip = new ToolTip(components);
            chkStopOnError = new CheckBox();
            numTimeout = new NumericUpDown();
            lblTimeout = new Label();
            cmbDbType = new ComboBox();
            lblDbType = new Label();
            cmbRegion = new ComboBox();
            label1 = new Label();
            txtConn = new TextBox();
            contextMenuScripts.SuspendLayout();
            groupBoxScripts.SuspendLayout();
            groupBoxLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTimeout).BeginInit();
            SuspendLayout();
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Font = new Font("微软雅黑", 9F);
            btnSelectFolder.Location = new Point(12, 12);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(110, 35);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "📁 选择文件夹";
            toolTip.SetToolTip(btnSelectFolder, "选择包含SQL脚本文件的文件夹");
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += BtnSelectFolder_Click;
            // 
            // btnExecute
            // 
            btnExecute.BackColor = Color.LightGreen;
            btnExecute.Enabled = false;
            btnExecute.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnExecute.Location = new Point(12, 530);
            btnExecute.Name = "btnExecute";
            btnExecute.Size = new Size(776, 45);
            btnExecute.TabIndex = 1;
            btnExecute.Text = "▶ 执行所有脚本";
            toolTip.SetToolTip(btnExecute, "执行列表中所有待执行的SQL脚本");
            btnExecute.UseVisualStyleBackColor = false;
            btnExecute.Click += BtnExecute_Click;
            // 
            // listViewScripts
            // 
            listViewScripts.Columns.AddRange(new ColumnHeader[] { colFileName, colStatus, colExecuteTime, colErrorMessage, colFilePath });
            listViewScripts.ContextMenuStrip = contextMenuScripts;
            listViewScripts.Dock = DockStyle.Fill;
            listViewScripts.Font = new Font("Consolas", 9F);
            listViewScripts.FullRowSelect = true;
            listViewScripts.GridLines = true;
            listViewScripts.Location = new Point(3, 19);
            listViewScripts.Name = "listViewScripts";
            listViewScripts.Size = new Size(770, 251);
            listViewScripts.TabIndex = 2;
            listViewScripts.UseCompatibleStateImageBehavior = false;
            listViewScripts.View = View.Details;
            // 
            // colFileName
            // 
            colFileName.Text = "文件名";
            colFileName.Width = 200;
            // 
            // colStatus
            // 
            colStatus.Text = "执行状态";
            colStatus.TextAlign = HorizontalAlignment.Center;
            colStatus.Width = 100;
            // 
            // colExecuteTime
            // 
            colExecuteTime.Text = "执行时间";
            colExecuteTime.TextAlign = HorizontalAlignment.Center;
            colExecuteTime.Width = 100;
            // 
            // colErrorMessage
            // 
            colErrorMessage.Text = "错误信息";
            colErrorMessage.Width = 250;
            // 
            // colFilePath
            // 
            colFilePath.Text = "文件路径";
            colFilePath.Width = 250;
            // 
            // contextMenuScripts
            // 
            contextMenuScripts.Items.AddRange(new ToolStripItem[] { executeSelectedToolStripMenuItem });
            contextMenuScripts.Name = "contextMenuScripts";
            contextMenuScripts.Size = new Size(161, 26);
            // 
            // executeSelectedToolStripMenuItem
            // 
            executeSelectedToolStripMenuItem.Name = "executeSelectedToolStripMenuItem";
            executeSelectedToolStripMenuItem.Size = new Size(160, 22);
            executeSelectedToolStripMenuItem.Text = "执行选中的脚本";
            executeSelectedToolStripMenuItem.Click += ExecuteSelectedToolStripMenuItem_Click;
            // 
            // txtLog
            // 
            txtLog.BackColor = Color.Black;
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("Consolas", 9F);
            txtLog.ForeColor = Color.LightGreen;
            txtLog.Location = new Point(3, 19);
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.Size = new Size(770, 151);
            txtLog.TabIndex = 3;
            txtLog.Text = "";
            txtLog.WordWrap = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("微软雅黑", 9F);
            lblStatus.ForeColor = Color.Gray;
            lblStatus.Location = new Point(128, 20);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(80, 17);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "未选择文件夹";
            // 
            // groupBoxScripts
            // 
            groupBoxScripts.Controls.Add(listViewScripts);
            groupBoxScripts.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            groupBoxScripts.Location = new Point(12, 80);
            groupBoxScripts.Name = "groupBoxScripts";
            groupBoxScripts.Size = new Size(776, 273);
            groupBoxScripts.TabIndex = 5;
            groupBoxScripts.TabStop = false;
            groupBoxScripts.Text = "📜 SQL脚本列表";
            // 
            // groupBoxLog
            // 
            groupBoxLog.Controls.Add(txtLog);
            groupBoxLog.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            groupBoxLog.Location = new Point(12, 359);
            groupBoxLog.Name = "groupBoxLog";
            groupBoxLog.Size = new Size(776, 173);
            groupBoxLog.TabIndex = 6;
            groupBoxLog.TabStop = false;
            groupBoxLog.Text = "📋 执行日志";
            // 
            // btnClearLog
            // 
            btnClearLog.Location = new Point(791, 359);
            btnClearLog.Name = "btnClearLog";
            btnClearLog.Size = new Size(47, 23);
            btnClearLog.TabIndex = 4;
            btnClearLog.Text = "清空";
            btnClearLog.UseVisualStyleBackColor = true;
            // 
            // btnTestConnection
            // 
            btnTestConnection.Font = new Font("微软雅黑", 9F);
            btnTestConnection.Location = new Point(833, 80);
            btnTestConnection.Name = "btnTestConnection";
            btnTestConnection.Size = new Size(90, 27);
            btnTestConnection.TabIndex = 8;
            btnTestConnection.Text = "测试连接";
            toolTip.SetToolTip(btnTestConnection, "测试数据库连接是否正常");
            btnTestConnection.UseVisualStyleBackColor = true;
            btnTestConnection.Click += BtnTestConnection_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 581);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(776, 23);
            progressBar.TabIndex = 9;
            progressBar.Visible = false;
            // 
            // lblConn
            // 
            lblConn.AutoSize = true;
            lblConn.Font = new Font("微软雅黑", 9F);
            lblConn.Location = new Point(344, 54);
            lblConn.Name = "lblConn";
            lblConn.Size = new Size(71, 17);
            lblConn.TabIndex = 10;
            lblConn.Text = "连接字符串:";
            // 
            // chkStopOnError
            // 
            chkStopOnError.AutoSize = true;
            chkStopOnError.Font = new Font("微软雅黑", 9F);
            chkStopOnError.Location = new Point(634, 26);
            chkStopOnError.Name = "chkStopOnError";
            chkStopOnError.Size = new Size(87, 21);
            chkStopOnError.TabIndex = 11;
            chkStopOnError.Text = "出错时停止";
            chkStopOnError.UseVisualStyleBackColor = true;
            // 
            // numTimeout
            // 
            numTimeout.Font = new Font("微软雅黑", 9F);
            numTimeout.Location = new Point(514, 25);
            numTimeout.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numTimeout.Minimum = new decimal(new int[] { 30, 0, 0, 0 });
            numTimeout.Name = "numTimeout";
            numTimeout.Size = new Size(60, 23);
            numTimeout.TabIndex = 12;
            numTimeout.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // lblTimeout
            // 
            lblTimeout.AutoSize = true;
            lblTimeout.Font = new Font("微软雅黑", 9F);
            lblTimeout.Location = new Point(577, 27);
            lblTimeout.Name = "lblTimeout";
            lblTimeout.Size = new Size(55, 17);
            lblTimeout.TabIndex = 13;
            lblTimeout.Text = "超时(秒):";
            // 
            // cmbDbType
            // 
            cmbDbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDbType.Font = new Font("微软雅黑", 9F);
            cmbDbType.Items.AddRange(new object[] { "Dm", "SqlServer", "MySql", "Oracle", "Sqlite", "PostgreSQL" });
            cmbDbType.Location = new Point(238, 51);
            cmbDbType.Name = "cmbDbType";
            cmbDbType.Size = new Size(100, 25);
            cmbDbType.TabIndex = 14;
            cmbDbType.SelectedIndexChanged += CmbDbType_SelectedIndexChanged;
            // 
            // lblDbType
            // 
            lblDbType.AutoSize = true;
            lblDbType.Font = new Font("微软雅黑", 9F);
            lblDbType.Location = new Point(164, 54);
            lblDbType.Name = "lblDbType";
            lblDbType.Size = new Size(71, 17);
            lblDbType.TabIndex = 15;
            lblDbType.Text = "数据库类型:";
            // 
            // cmbRegion
            // 
            cmbRegion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRegion.Font = new Font("微软雅黑", 9F);
            cmbRegion.Items.AddRange(new object[] { "Dm", "SqlServer", "MySql", "Oracle", "Sqlite", "PostgreSQL" });
            cmbRegion.Location = new Point(52, 51);
            cmbRegion.Name = "cmbRegion";
            cmbRegion.Size = new Size(100, 25);
            cmbRegion.TabIndex = 14;
            cmbRegion.SelectedIndexChanged += cmbRegion_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("微软雅黑", 9F);
            label1.Location = new Point(12, 54);
            label1.Name = "label1";
            label1.Size = new Size(35, 17);
            label1.TabIndex = 15;
            label1.Text = "区域:";
            // 
            // txtConn
            // 
            txtConn.Location = new Point(421, 51);
            txtConn.Name = "txtConn";
            txtConn.Size = new Size(502, 23);
            txtConn.TabIndex = 16;
            // 
            // ExecuteSQLForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(935, 615);
            Controls.Add(txtConn);
            Controls.Add(btnClearLog);
            Controls.Add(label1);
            Controls.Add(cmbRegion);
            Controls.Add(lblDbType);
            Controls.Add(cmbDbType);
            Controls.Add(lblTimeout);
            Controls.Add(numTimeout);
            Controls.Add(chkStopOnError);
            Controls.Add(lblConn);
            Controls.Add(progressBar);
            Controls.Add(btnTestConnection);
            Controls.Add(groupBoxLog);
            Controls.Add(groupBoxScripts);
            Controls.Add(lblStatus);
            Controls.Add(btnExecute);
            Controls.Add(btnSelectFolder);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ExecuteSQLForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SQL脚本批量执行工具";
            Load += ExecuteSQLForm_Load;
            contextMenuScripts.ResumeLayout(false);
            groupBoxScripts.ResumeLayout(false);
            groupBoxLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numTimeout).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private ComboBox cmbRegion;
        private Label label1;
        private TextBox txtConn;
    }
}