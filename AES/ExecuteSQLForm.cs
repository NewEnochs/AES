using AES.Helper;
using AES.Helper.ItemValueHelper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AES
{
    /// <summary>
    /// 执行文件夹中的SQL脚本
    /// </summary>
    public partial class ExecuteSQLForm : Form
    {
        private List<ScriptFile> scriptFiles = new List<ScriptFile>();
        private List<ConnStringData> connList = new List<ConnStringData>();
        private SqlSugarClient db;
        private string connectionString = "Server=172.2.3.232;PORT=5236;User Id=HC_REGIONCDS;PWD=2x!31xGW#$pgOwBg";
        private CancellationTokenSource cancellationTokenSource;

        public ExecuteSQLForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            SetupEventHandlers();
            //LoadDefaultConnectionStrings();   
        }

        private void InitializeCustomComponents()
        {
            // 设置ListView的排序功能
            listViewScripts.ColumnClick += ListViewScripts_ColumnClick;

            // 设置右键菜单
            executeSelectedToolStripMenuItem.Click += ExecuteSelectedToolStripMenuItem_Click;
            btnClearLog.Click += BtnClearLog_Click;

            // 设置提示信息
            toolTip.SetToolTip(numTimeout, "设置SQL命令执行的超时时间（秒）");
            toolTip.SetToolTip(chkStopOnError, "勾选后，遇到错误将停止执行后续脚本");
            toolTip.SetToolTip(cmbDbType, "选择数据库类型");
        }

        private void SetupEventHandlers()
        {
            //btnSelectFolder.Click += BtnSelectFolder_Click;
            //btnTestConnection.Click += BtnTestConnection_Click;
            //btnExecute.Click += BtnExecute_Click;
            btnClearLog.Click += BtnClearLog_Click;
            //cmbDbType.SelectedIndexChanged += CmbDbType_SelectedIndexChanged;
        }

        private async Task InitializeSqlSugarClient()
        {
            var dbType = GetDbTypeFromComboBox();

            // 如果是 SQL Server，强制添加连接超时
            if (dbType == DbType.SqlServer)
            {
                // 检查是否已有 Connection Timeout 设置
                if (!connectionString.Contains("Connection Timeout") &&
                    !connectionString.Contains("Connect Timeout"))
                {
                    connectionString += ";Connection Timeout=3;"; // 设置 3 秒超时
                }

                // 同时添加 SSL 参数（解决证书问题）
                if (!connectionString.Contains("Encrypt="))
                {
                    connectionString += ";Encrypt=True;TrustServerCertificate=True;";
                }
            }

            var connectionConfig = new ConnectionConfig
            {
                ConnectionString = connectionString,
                DbType = dbType,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                MoreSettings = new ConnMoreSettings
                {
                    IsAutoRemoveDataCache = true,
                    IsWithNoLockQuery = true,
                }
            };


            db = new SqlSugarClient(connectionConfig);

            // 添加SQL执行日志
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke(new Action(() =>
                        AppendLog($"执行SQL: {sql}", Color.Gray)));
                }
                else
                {
                    AppendLog($"执行SQL: {sql}", Color.Gray);
                }
            };

            db.Aop.OnError = (exp) =>
            {
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke(new Action(() =>
                        AppendLog($"SQL错误: {exp.Message}", Color.Red)));
                }
                else
                {
                    AppendLog($"SQL错误: {exp.Message}", Color.Red);
                }
            };
        }

        private DbType GetDbTypeFromComboBox()
        {
            switch (cmbDbType.SelectedValue?.ToString().ToUpper())
            {
                case "DM":
                    return DbType.Dm;
                case "SQL SERVER":
                    return DbType.SqlServer;
                case "Oracle":
                    return DbType.Oracle;
                case "Sqlite":
                    return DbType.Sqlite;
                case "PostgreSQL":
                    return DbType.PostgreSQL;
                default:
                    return DbType.SqlServer;
            }
        }

        private void CmbDbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 当数据库类型改变时，更新连接字符串示例
            string name = cmbRegion.Text;
            var dbType = cmbDbType.SelectedValue?.ToString();

            var model = connList.FirstOrDefault(r => r.Name == name);
            if (model != null && model.connData != null)
            {
                var conn = model.connData.FirstOrDefault(r => r.DbType == dbType);
                if (conn != null)
                {
                    txtConn.Text = conn.ConnString;
                }
            }
        }

        private async void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "请选择包含SQL脚本文件的文件夹";
                dialog.ShowNewFolderButton = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = dialog.SelectedPath;
                    lblStatus.Text = $"已选择: {folderPath}";
                    lblStatus.ForeColor = Color.Green;

                    await LoadScripts(folderPath);
                    btnExecute.Enabled = scriptFiles.Any(s => s.Status != "成功");
                }
            }
        }

        private async Task LoadScripts(string folderPath)
        {
            try
            {
                var sqlFiles = Directory.GetFiles(folderPath, "*.sql", SearchOption.AllDirectories);
                scriptFiles.Clear();
                listViewScripts.Items.Clear();

                foreach (var file in sqlFiles.OrderBy(f => f))
                {
                    var script = new ScriptFile
                    {
                        FilePath = file,
                        FileName = Path.GetFileName(file),
                        Status = "待执行",
                        Content = await File.ReadAllTextAsync(file, Encoding.GetEncoding("gb2312"))
                    };
                    scriptFiles.Add(script);
                    AddScriptToListView(script);
                }

                AppendLog($"✓ 找到 {scriptFiles.Count} 个SQL脚本文件", Color.Green);

                if (scriptFiles.Count == 0)
                {
                    AppendLog("⚠ 所选文件夹中没有找到.sql文件", Color.Orange);
                }
            }
            catch (Exception ex)
            {
                AppendLog($"✗ 加载脚本失败: {ex.Message}", Color.Red);
                MessageBox.Show($"加载脚本失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddScriptToListView(ScriptFile script)
        {
            var item = new ListViewItem(script.FileName);
            item.SubItems.Add(script.Status);
            item.SubItems.Add(script.ExecuteTime);
            item.SubItems.Add(script.ErrorMessage);
            item.SubItems.Add(script.FilePath);
            item.Tag = script;

            UpdateListItemColor(item, script.Status);

            listViewScripts.Items.Add(item);
        }

        private void UpdateScriptInListView(ScriptFile script)
        {
            foreach (ListViewItem item in listViewScripts.Items)
            {
                if (item.Tag == script)
                {
                    item.SubItems[1].Text = script.Status;
                    item.SubItems[2].Text = script.ExecuteTime;
                    item.SubItems[3].Text = script.ErrorMessage;
                    UpdateListItemColor(item, script.Status);
                    break;
                }
            }
        }

        private void UpdateListItemColor(ListViewItem item, string status)
        {
            switch (status)
            {
                case "成功":
                    item.ForeColor = Color.Green;
                    break;
                case "失败":
                    item.ForeColor = Color.Red;
                    break;
                case "执行中":
                    item.ForeColor = Color.Blue;
                    break;
                default:
                    item.ForeColor = Color.Black;
                    break;
            }
        }

        private async void BtnExecute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConn.Text))
            {
                MessageBox.Show("请先设置数据库连接字符串", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            connectionString = txtConn.Text;

            var dbType = GetDbTypeFromComboBox();

            // 先快速检测网络和端口是否可达
            if (dbType == DbType.SqlServer)
            {
                bool isReachable = await QuickNetworkCheckAsync(connectionString);
                if (!isReachable)
                {
                    MessageBox.Show("网络不可达或端口不通，请检查服务器地址和端口", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 重新初始化 SqlSugarClient
            await InitializeSqlSugarClient();

            // 测试连接
            if (!await TestDatabaseConnection())
            {
                MessageBox.Show("无法连接到数据库，请检查连接字符串", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var unexecutedScripts = scriptFiles.Where(s => s.Status != "成功").ToList();
            if (unexecutedScripts.Count == 0)
            {
                MessageBox.Show("所有脚本都已成功执行！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"确定要执行 {unexecutedScripts.Count} 个SQL脚本吗？\n\n" +
                $"数据库类型: {cmbDbType.SelectedItem}\n" +
                $"超时时间: {numTimeout.Value}秒\n" +
                $"出错时停止: {(chkStopOnError.Checked ? "是" : "否")}",
                "确认执行", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            await ExecuteAllScripts();
        }

        private async Task ExecuteAllScripts()
        {
            cancellationTokenSource = new CancellationTokenSource();
            btnExecute.Enabled = false;
            btnSelectFolder.Enabled = false;
            progressBar.Visible = true;

            var scriptsToExecute = scriptFiles.Where(s => s.Status != "成功").ToList();
            progressBar.Maximum = scriptsToExecute.Count;
            progressBar.Value = 0;

            AppendLog("═══════════════════════════════════════════════════════════", Color.Cyan);
            AppendLog($"开始执行SQL脚本 - {DateTime.Now:yyyy-MM-dd HH:mm:ss}", Color.Cyan);
            AppendLog($"数据库类型: {cmbDbType.SelectedItem}", Color.Cyan);
            AppendLog($"超时设置: {numTimeout.Value}秒", Color.Cyan);
            AppendLog($"出错处理: {(chkStopOnError.Checked ? "停止执行" : "继续执行")}", Color.Cyan);
            AppendLog("═══════════════════════════════════════════════════════════", Color.Cyan);

            int successCount = 0;
            int failCount = 0;

            for (int i = 0; i < scriptsToExecute.Count; i++)
            {
                var script = scriptsToExecute[i];
                script.Status = "执行中";
                script.ErrorMessage = "";
                UpdateScriptInListView(script);

                progressBar.Value = i + 1;
                AppendLog($"\n[{i + 1}/{scriptsToExecute.Count}] 执行: {script.FileName}", Color.Yellow);

                try
                {
                    var startTime = DateTime.Now;
                    await ExecuteScriptWithSqlSugar(script);
                    var endTime = DateTime.Now;

                    script.ExecuteTime = $"{(endTime - startTime).TotalMilliseconds:F0}ms";
                    script.Status = "成功";
                    successCount++;

                    AppendLog($"  ✓ 成功: {script.FileName} ({script.ExecuteTime})", Color.Green);
                }
                catch (OperationCanceledException)
                {
                    script.ExecuteTime = DateTime.Now.ToString("HH:mm:ss");
                    script.Status = "失败";
                    script.ErrorMessage = "执行被取消";
                    failCount++;
                    AppendLog($"  ✗ 取消: {script.FileName}", Color.Orange);
                    break;
                }
                catch (Exception ex)
                {
                    script.ExecuteTime = DateTime.Now.ToString("HH:mm:ss");
                    script.Status = "失败";
                    script.ErrorMessage = ex.Message;
                    failCount++;
                    AppendLog($"  ✗ 失败: {script.FileName}", Color.Red);
                    AppendLog($"     错误信息: {ex.Message}", Color.Red);

                    if (ex.InnerException != null)
                    {
                        AppendLog($"     内部错误: {ex.InnerException.Message}", Color.Red);
                    }

                    if (chkStopOnError.Checked)
                    {
                        AppendLog("\n⚠ 根据设置，停止执行后续脚本", Color.Orange);
                        break;
                    }
                }

                UpdateScriptInListView(script);
                listViewScripts.EnsureVisible(i);
            }

            AppendLog("\n═══════════════════════════════════════════════════════════", Color.Cyan);
            AppendLog($"执行完成 - {DateTime.Now:HH:mm:ss}", Color.Cyan);
            AppendLog($"成功: {successCount} 个, 失败: {failCount} 个",
                failCount == 0 ? Color.Green : Color.Orange);
            AppendLog("═══════════════════════════════════════════════════════════", Color.Cyan);

            progressBar.Visible = false;
            btnExecute.Enabled = scriptFiles.Any(s => s.Status != "成功");
            btnSelectFolder.Enabled = true;

            if (failCount == 0 && successCount > 0)
            {
                MessageBox.Show($"所有脚本执行成功！\n共执行 {successCount} 个脚本",
                    "执行完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task ExecuteScriptWithSqlSugar(ScriptFile script)
        {
            try
            {
                // 开始事务
                db.Ado.BeginTran();

                // 设置命令超时时间
                db.Ado.CommandTimeOut = (int)numTimeout.Value;

                // 分割SQL语句（按GO分隔符）
                var sqlCommands = SplitSqlScript(script.Content);

                foreach (var sql in sqlCommands)
                {
                    if (string.IsNullOrWhiteSpace(sql)) continue;

                    // 使用 SqlSugar 执行SQL
                    await Task.Run(() => db.Ado.ExecuteCommand(sql));
                }

                // 提交事务
                db.Ado.CommitTran();

                AppendLog($"    使用 SqlSugar 执行完成", Color.Gray);
            }
            catch (Exception)
            {
                // 回滚事务
                db.Ado.RollbackTran();
                throw;
            }
        }

        private List<string> SplitSqlScript(string script)
        {
            var commands = new List<string>();
            var lines = script.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var currentCommand = new StringBuilder();

            foreach (var line in lines)
            {
                if (line.Trim().ToUpper() == "GO")
                {
                    if (currentCommand.Length > 0)
                    {
                        commands.Add(currentCommand.ToString());
                        currentCommand.Clear();
                    }
                }
                else
                {
                    currentCommand.AppendLine(line);
                }
            }

            if (currentCommand.Length > 0)
            {
                commands.Add(currentCommand.ToString());
            }

            return commands;
        }

        private async Task<bool> TestDatabaseConnection()
        {
            // 使用 SqlSugar 测试连接
            var result = await Task.Run(() => db.Ado.GetInt("SELECT 1"));
            //AppendLog($"✓ 数据库连接测试成功", Color.Green);
            return true;
        }

        private async void BtnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                connectionString = txtConn.Text;

                var dbType = GetDbTypeFromComboBox();

                // 先快速检测网络和端口是否可达
                if (dbType == DbType.SqlServer)
                {
                    bool isReachable = await QuickNetworkCheckAsync(connectionString);
                    if (!isReachable)
                    {
                        MessageBox.Show("网络不可达或端口不通，请检查服务器地址和端口", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // 重新初始化 SqlSugarClient
                await InitializeSqlSugarClient();

                db.Ado.ExecuteCommand("select 1");
                //if (await TestDatabaseConnection())
                //{
                MessageBox.Show("数据库连接成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    MessageBox.Show("数据库连接失败，请检查连接字符串", "错误",
                //        MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            catch (Exception ex)
            {
                AppendLog($"✗ 数据库连接测试失败: {ex.Message}", Color.Red);
            }
        }

        private void BtnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            AppendLog("日志已清空", Color.Gray);
        }

        private void AppendLog(string message, Color color)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => AppendLog(message, color)));
                return;
            }

            txtLog.SelectionStart = txtLog.TextLength;
            txtLog.SelectionLength = 0;
            txtLog.SelectionColor = color;
            txtLog.AppendText($"{DateTime.Now:HH:mm:ss.fff} - {message}{Environment.NewLine}");
            txtLog.SelectionColor = txtLog.ForeColor;
            txtLog.ScrollToCaret();
        }

        private void ListViewScripts_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewScripts.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        private async void ExecuteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewScripts.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择要执行的脚本", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedScripts = listViewScripts.SelectedItems
                .Cast<ListViewItem>()
                .Select(item => item.Tag as ScriptFile)
                .Where(script => script.Status != "成功")
                .ToList();

            if (selectedScripts.Count == 0)
            {
                MessageBox.Show("选中的脚本都已成功执行", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AppendLog($"选中 {selectedScripts.Count} 个脚本待执行", Color.Yellow);

            await ExecuteSelectedScripts(selectedScripts);
        }

        private async Task ExecuteSelectedScripts(List<ScriptFile> selectedScripts)
        {
            if (!await TestDatabaseConnection())
            {
                MessageBox.Show("无法连接到数据库，请检查连接字符串", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();
            int successCount = 0;
            int failCount = 0;

            AppendLog($"\n开始执行选中的 {selectedScripts.Count} 个脚本", Color.Cyan);

            for (int i = 0; i < selectedScripts.Count; i++)
            {
                var script = selectedScripts[i];
                script.Status = "执行中";
                script.ErrorMessage = "";
                UpdateScriptInListView(script);

                AppendLog($"\n执行: {script.FileName}", Color.Yellow);

                try
                {
                    var startTime = DateTime.Now;
                    await ExecuteScriptWithSqlSugar(script);
                    var endTime = DateTime.Now;

                    script.ExecuteTime = $"{(endTime - startTime).TotalMilliseconds:F0}ms";
                    script.Status = "成功";
                    successCount++;

                    AppendLog($"  ✓ 成功: {script.FileName} ({script.ExecuteTime})", Color.Green);
                }
                catch (Exception ex)
                {
                    script.ExecuteTime = DateTime.Now.ToString("HH:mm:ss");
                    script.Status = "失败";
                    script.ErrorMessage = ex.Message;
                    failCount++;
                    AppendLog($"  ✗ 失败: {script.FileName} - {ex.Message}", Color.Red);
                }

                UpdateScriptInListView(script);
            }

            AppendLog($"\n选中脚本执行完成 - 成功: {successCount}, 失败: {failCount}",
                failCount == 0 ? Color.Green : Color.Orange);
        }

        // ListView排序辅助类
        private class ListViewItemComparer : System.Collections.IComparer
        {
            private int column;

            public ListViewItemComparer(int column)
            {
                this.column = column;
            }

            public int Compare(object x, object y)
            {
                return string.Compare(
                    ((ListViewItem)x).SubItems[column].Text,
                    ((ListViewItem)y).SubItems[column].Text
                );
            }
        }

        private void ExecuteSQLForm_Load(object sender, EventArgs e)
        {
            connList = new ItemValueConfig().GetData<ConnStringData>("connStringData");
            cmbRegion.DataSource = connList;
            cmbRegion.ValueMember = "Id";
            cmbRegion.DisplayMember = "Name";

            var defaultRegionName = new ItemValueConfig().GetData("defaultRegion");
            // 直接设置选中的对象
            var defaultItem = connList.FirstOrDefault(x => x.Name == defaultRegionName);
            if (defaultItem != null)
            {
                cmbRegion.SelectedItem = defaultItem;
            }
            else
            {
                cmbRegion.SelectedIndex = 0;
            }
        }

        private void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = cmbRegion.Text;
            var conn = connList.FirstOrDefault(r => r.Name == name);
            if (conn != null)
            {
                txtConn.Text = conn.ConnString;

                loadDbType(conn.Name);
            }
            //toolTip.SetToolTip(cmbConnectionString, conn.ConnString);
        }

        private void loadDbType(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var dataList = connList.Where(r => r.Name == name).ToList();

                if (dataList != null && dataList.Count > 0)
                {
                    var model = dataList[0];
                    cmbDbType.DataSource = model.connData == null ? dataList : model.connData;
                    cmbDbType.ValueMember = "DbType";
                    cmbDbType.DisplayMember = "DbTypeName";
                    cmbDbType.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 快速网络检测（仅用于 SQL Server）
        /// </summary>
        private async Task<bool> QuickNetworkCheckAsync(string connectionString)
        {
            try
            {
                // 解析连接字符串获取服务器和端口
                var serverMatch = Regex.Match(connectionString, @"(?:Server|Data Source)=([^;]+)");
                if (!serverMatch.Success) return false;

                var serverPart = serverMatch.Groups[1].Value;
                string host;
                int port = 1433; // 默认端口

                if (serverPart.Contains(","))
                {
                    var parts = serverPart.Split(',');
                    host = parts[0];
                    int.TryParse(parts[1], out port);
                }
                else
                {
                    host = serverPart;
                }

                // 使用 TcpClient 快速检测端口是否开放（超时 1 秒）
                using (var tcpClient = new TcpClient())
                {
                    var connectTask = tcpClient.ConnectAsync(host, port);
                    if (await Task.WhenAny(connectTask, Task.Delay(1000)) == connectTask)
                    {
                        await connectTask;
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

    }

    public class ScriptFile
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string ExecuteTime { get; set; } = "";
        public string ErrorMessage { get; set; } = "";
        public string Content { get; set; }
    }
}