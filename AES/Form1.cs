using AES.DataBase;
using AES.Helper;
using AES.Model;
using AES.Util;
using API.DBEntity.Model;
using NetTaste;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlSugar;
using SqlSugar.Extensions;
using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AES
{
    public partial class Form1 : Form
    {

        #region 页面参数  构造函数
        private AppConfig config;
        private int currentIndex;
        private int addressCount;
        int type = 1; //1.慢病 2.公卫
        MessageInfo info = new MessageInfo();   //请求相应内容

        private string lastMbUrl = "/grjkdaGroup/getTopInfo";
        private string lastGwUrl = "/BSApp/GetList";
        private string lastMBMW = "begp++A6RxZY/R5h31KNZHcY43C1DKDz7j4JQll7xv2s9ezAc8+9LSDk2nimdJg7OFR1fyTSsS6zs0UFeYr3w4yGsJGS336uwS/uYzgzrkA=";
        private string lastGwMW = "ZZs63zQI5L0DlzA3t7/7NZK9o8q160ajdAxEJnkxYVrQY4p/xKk/3BSrOFI1Avb+PPmiEQBBw3uk9/dmrHM/1CfX6AqASd0Z+NYsdTHiegEIRhUoJia+PlvyUlilRcIxxQT+YwWwp8jLEnYbMakZdpR8ypDhk7xyUOOawTt0QPLr+0fBf4w4K+WlSBC7ujY5PNXGi0aq4ga9RM5RONXALtjM8gzZi5z29pXspcoDLaPhr3oh4U3Uv4QXykpQo+LPW4EuW0j2GhZuJtZP9p2fxiC9Z2ZXDDzruPtawn/f8yEbC00b4nL916Wr90IJ66dqXBSUJ8vh3Ta8rAYyuUnlQZoMsL1jIghqRHfZpgn0jlgQDp8K/CRCNrajOop7vdN3wROaRJaNghRQEgA0GZRvec246I1y19NgoVFwBFoVkYzAPaLPRBY6Zf/ozF+UgR8U1lrwPaXXQxOpRR2smLY25d5og8a1uAk8sXWHzwW+s94=";

        private string gwToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiLllrvmnKzlhYgiLCJqdGkiOiI2MmE2NTg1NC00NmYyLTRlOTYtYmJiMS0zMTc0YTIzMTYxY2MiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6WyIxMDAwMDEiLCIxMDAwMDExMTAwMSJdLCJuYmYiOjE3Njk1ODAzNjksImV4cCI6MTc2OTY2Njc2OSwiaXNzIjoiZXN0IiwiYXVkIjoiY2hpc3VpIn0.Wn1B9GQVR6qetpZO5G_lUrjlJsxkIsG7pgunrH5nl0M";
        public Form1()
        {
            InitializeComponent();
            // 加载配置
            config = AppConfig.Load();

            ReCalcCount();

            txtZH.Text = "superAdmin";
            txtPwd.Text = "Estoom@?2023";
            button1_Click(null, null);

            try
            {
                BindHistory();
            }
            catch { CreateDataBase.RecreateDatabase(); }
        }
        #endregion

        #region 加密
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJiaMi_Click(object sender, EventArgs e)
        {
            FilterAES.key = txtMY.Text;
            FilterAES.iv = txtPYL.Text;

            if (type == 1)
            {

                string MingW = txtMingW.Text;

                var mw = FilterAES.FileterEncrypt(MingW);
                txtMW.Text = mw;
            }
            else if (type == 2)
            {
                string MingW = txtMingW.Text;

                var mw = CHISAES.AESEncrypt(MingW);
                txtMW.Text = mw;
            }
        }
        #endregion

        #region 解密
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJiemi_Click(object sender, EventArgs e)
        {
            FilterAES.key = txtMY.Text;
            FilterAES.iv = txtPYL.Text;

            try
            {
                string mw = txtMW.Text;

                if (type == 1)
                {
                    var mingw = FilterAES.FileterDecrypt(mw);
                    try
                    {
                        var jsonObject = JObject.Parse(mingw);
                        string formattedJson = jsonObject.ToString((Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented);
                        txtMingW.Text = formattedJson;
                    }
                    catch
                    {
                        txtMingW.Text = mingw;
                    }
                }
                else if (type == 2)
                {
                    var mingw = CHISAES.AESDEncrypt(mw);
                    try
                    {
                        var jsonObject = JObject.Parse(mingw);
                        string formattedJson = jsonObject.ToString((Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented);
                        txtMingW.Text = formattedJson;
                    }
                    catch
                    {
                        txtMingW.Text = mingw;
                    }
                }

                HZColor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }

        }
        #endregion

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            txtMW.MaxLength = int.MaxValue;
            txtMingW.MaxLength = int.MaxValue;

            btnMB.BackColor = Color.FromArgb(134, 124, 228);
            SetButtonStyle(btnMB);
            SetButtonStyle(btnGW);
            SetButtonStyle(btnSMS);
        }
        #endregion

        #region 系统切换  慢病 / 公卫
        /// <summary>
        /// type 1.慢病 2.公卫
        /// </summary>
        private void btnGW_Click(object sender, EventArgs e)
        {
            var btn = (sender as Button);
            if (btn.Name == "btnMB")
            {
                type = 1;
                btnMB.BackColor = Color.FromArgb(134, 124, 228);
                btnGW.BackColor = SystemColors.Window;
                btnSMS.BackColor = SystemColors.Window;

                txtZH.Text = "superAdmin";
                txtPwd.Text = "Estoom@?203";
                txtUrl.Text = lastMbUrl;
                txtMW.Text = lastMBMW;
                lastMbUrl = txtUrl.Text;
                lastMBMW = txtMW.Text;

                txtMY.Text = "GVvVJyrsFRKms8XKhwfwpgB47DtIaZ2p";    //密匙
                txtPYL.Text = "4XEUxWxkTSGcEZxe";   // 偏移量
            }
            else if (btn.Name == "btnGW")
            {
                type = 2;
                btnMB.BackColor = SystemColors.Window;
                btnGW.BackColor = Color.FromArgb(134, 124, 228);
                btnSMS.BackColor = SystemColors.Window;

                txtZH.Text = "5203211101";
                txtPwd.Text = "1234ASDF";
                txtUrl.Text = lastGwUrl;
                txtMW.Text = lastGwMW;
                lastGwUrl = txtUrl.Text;
                lastGwMW = txtMW.Text;

                txtMY.Text = "C0D2ACC1205B4028A4888CAC475FBE35";    //密匙
                txtPYL.Text = "";   // 偏移量
            }
            else if (btn.Name == "btnSMS")
            {
                type = 3;
                btnMB.BackColor = SystemColors.Window;
                btnGW.BackColor = SystemColors.Window;
                btnSMS.BackColor = Color.FromArgb(134, 124, 228);

                txtMW.Text = "{\r\n  \"BODY\": {\r\n    \"HM\": \"13637748963\",\r\n    \"DXNR\": \"【医事通】验证码：992041 。该验证码仅用于身份验证，请勿泄露给他人使用。\",\r\n    \"JGBM\": \"52000\",\r\n    \"GH\": \"001\"\r\n  },\r\n  \"ZH\": \"qdndzxwsjkjhdsc\",\r\n  \"MM\": \"co55EZ#HVuv64\"\r\n}";

                txtRootPath.Text = "http://36.111.166.130:40024";
                txtUrl.Text = "/SendSMS/do";

            }

            ReCalcCount();

        }
        #endregion

        #region 点击生成token
        /// <summary>
        /// 点击生成Token
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object? sender, EventArgs? e)
        {
            if (type == 1)
            {
                var db = new DbContext().Db;
                var account = txtZH.Text.Trim();
                SYS_USER user = db.Queryable<SYS_USER>().First(r => r.Account == account);
                var token = TokenHelper.SetToken(user);
                txtToken.Text = token;
                //MessageBox.Show("生成Token成功");
            }
        }
        #endregion

        #region 请求
        /// <summary>
        /// 请求api方法
        /// </summary>
        private async void btnRequest_Click(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            ApiHistory history = new ApiHistory();
            try
            {
                var btn = (sender as Button);

                if (type == 1)      //慢病
                {
                    var dercyptData = txtMingW.Text;       //解密的参数
                    var encyptData = txtMW.Text;        //加密的参数

                    string? json = string.Empty; ;
                    if (btn != null && btn.Text == "明文请求")
                    {
                        json = dercyptData;
                        if (string.IsNullOrEmpty(json))
                        {
                            json = FilterAES.FileterDecrypt(encyptData);
                        }
                    }
                    else
                    {
                        json = encyptData;
                        if (string.IsNullOrEmpty(json))
                        {
                            json = FilterAES.FileterEncrypt(dercyptData);
                        }
                    }

                    string? url = txtRootPath.Text + (txtUrl.Text.StartsWith('/') ? txtUrl.Text.Trim() : "/" + txtUrl.Text);
                    string? token = txtToken.Text;

                    history = InsertHistory();
                    history.Token = token;
                    info = await ApiHelper.HttpApi(url, json, token, isCS: !chkParam.Checked);

                    if (info != null)
                    {
                        txtMingW.Text = formatJson(info.ToJson());
                        history.ResponseBody = txtMingW.Text;
                        history.StatusCode = info.code;
                    }
                }
                else if (type == 2)     //公卫
                {
                    string json = txtMW.Text;
                    string? url = txtRootPath.Text + txtUrl.Text;
                    txtToken.Text = gwToken;
                    string? token = txtToken.Text;

                    history = InsertHistory();
                    info = await ApiHelper.HttpApi(url, json, token, 2, isCS: !chkParam.Checked);

                    if (info != null)
                    {
                        txtMingW.Text = formatJson(info.ToJson());
                        history.ResponseBody = txtMingW.Text;
                    }
                }
                else if (type == 3) //短信平台
                {
                    var json = txtMW.Text;
                    string? url = txtRootPath.Text + (txtUrl.Text.StartsWith('/') ? txtUrl.Text.Trim() : "/" + txtUrl.Text);

                    history = InsertHistory();
                    SmsResult info = ApiHelper.WebRequests(url, json);

                    if (info != null)
                    {
                        txtMingW.Text = formatJson(info.ToJson());
                    }
                }

                if (!string.IsNullOrWhiteSpace(info.data))
                {
                    string decyptData = string.Empty;
                    if (type == 1)
                    {
                        decyptData = FilterAES.FileterDecrypt(info.data);
                    }
                    else if (type == 2)
                    {
                        decyptData = CHISAES.DecompressString(info.data);
                    }
                    txtMingwIn.Text = formatJson(decyptData);
                }
                HZColor();
                var new_now = DateTime.Now;
                var second = new_now.Subtract(now).TotalSeconds;
                var secondText = $"{txtUrl.Text} 响应时长:{second}秒";
                lblXYSC.Text = secondText;
            }
            catch (Exception ex)
            {
                txtMingW.Text = ex.Message;
                history.StatusCode = 0;
                history.ResponseBody = txtMingW.Text;
            }
            finally
            {
                BindHistory(history);
            }
        }
        #endregion

        #region 提取内部加密数据  解密
        /// <summary>
        /// 提取内部加密数据  解密
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(info.data))
            {
                string decyptData = string.Empty;
                if (type == 1)
                {
                    decyptData = FilterAES.FileterDecrypt(info.data);
                }
                else if (type == 2)
                {
                    decyptData = CHISAES.DecompressString(info.data);
                }
                txtMingwIn.Text = formatJson(decyptData);
            }

            HZColor();
        }
        #endregion

        #region 内部方法

        #region 去按钮边框
        /// <summary>
        /// 去掉边框
        /// </summary>
        /// <param name="btn"></param>
        private void SetButtonStyle(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
        }
        #endregion

        #region 转json
        public string formatJson(string jsonText)
        {
            var jsonObject = JToken.Parse(jsonText);
            string formattedJson = jsonObject.ToString((Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented);
            return formattedJson;
        }
        #endregion

        #region 切换公卫/慢病 重算数量
        public void ReCalcCount()
        {
            if (chkWeb.Checked)
            {
                if (type == 1)
                {
                    txtRootPath.Text = config.WebAddress[0];
                    addressCount = config.WebAddress.Count;
                }
                else if (type == 2)
                {
                    txtRootPath.Text = config.ChisWebAddress[0];
                    addressCount = config.ChisWebAddress.Count;
                }
                else if (type == 3)
                {
                    txtRootPath.Text = config.SMSWebAddress[0];
                    addressCount = config.SMSWebAddress.Count;
                }
            }
            else
            {
                if (type == 1)
                {
                    txtRootPath.Text = config.Addresses[0];
                    addressCount = config.Addresses.Count;
                }
                else if (type == 2)
                {
                    txtRootPath.Text = config.ChisAddress[0];
                    addressCount = config.ChisAddress.Count;
                }
                else if (type == 3)
                {
                    txtRootPath.Text = config.SMSAddress[0];
                    addressCount = config.SMSAddress.Count;
                }
            }

            lblStatus.Text = $"当前地址 ({1}/{addressCount})";
        }
        #endregion

        #endregion

        #region 结果格式化
        /// <summary>
        /// 结果格式化
        /// </summary>
        private void bntFormat_Click(object? sender, EventArgs? e)
        {
            txtMingW.Text = formatJson(txtMingW.Text);
            HZColor();
        }
        #endregion

        #region 结果清空
        /// <summary>
        /// 结果清空
        /// </summary>
        private void btnEmptyResult_Click(object sender, EventArgs e)
        {
            txtMingW.Text = string.Empty;
            txtMingwIn.Text = string.Empty;
        }
        #endregion

        #region 全部清空
        /// <summary>
        /// 全部清空
        /// </summary>
        private void btnEmptyAll_Click(object sender, EventArgs e)
        {
            txtMW.Text = string.Empty;
            txtMingW.Text = string.Empty;
        }
        #endregion

        #region 勾选前端
        /// <summary>
        /// 勾选前端
        /// </summary>
        private void chkWeb_CheckedChanged(object sender, EventArgs e)
        {
            ReCalcCount();
        }
        #endregion

        #region 切换环境 本地/测试/正式环境
        /// <summary>
        /// 切换环境
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                currentIndex += 1;
                if (currentIndex + 1 > addressCount)
                {
                    currentIndex = 0;
                }

                if (chkWeb.Checked)
                {
                    if (type == 1)
                    {
                        txtRootPath.Text = config.WebAddress[currentIndex];
                    }
                    else if (type == 2)
                    {
                        txtRootPath.Text = config.ChisWebAddress[currentIndex];
                    }
                    else if (type == 3)
                    {
                        txtRootPath.Text = config.SMSWebAddress[currentIndex];
                    }
                }
                else
                {
                    if (type == 1)
                    {
                        txtRootPath.Text = config.Addresses[currentIndex];
                    }
                    else if (type == 2)
                    {
                        txtRootPath.Text = config.ChisAddress[currentIndex];
                    }
                    else if (type == 3)
                    {
                        txtRootPath.Text = config.SMSAddress[currentIndex];
                    }
                }
                lblStatus.Text = $"当前地址 ({currentIndex + 1}/{addressCount})";

                HZColor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 点击listBox访问历史记录
        /// <summary>
        /// 点击listBox访问历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var box = (sender as ListBox);
                var item = box == null ? null : box.SelectedItems[0];
                if (item != null)
                {
                    string[]? listArr = item.ToString().Split("||");
                    txtRootPath.Text = listArr[0];
                    txtUrl.Text = listArr[1];
                    txtMW.Text = listArr[2];

                    txtMingW.Text = FilterAES.FileterDecrypt(txtMW.Text);
                    bntFormat_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 初始化数据库
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button7_Click(object sender, EventArgs e)
        {
            CreateDataBase.RecreateDatabase();
        }
        #endregion

        #region 插入记录

        public ApiHistory InsertHistory()
        {
            // 插入一条记录
            var history = new ApiHistory
            {
                RequestRootPath = txtRootPath.Text,
                RequestUrl = txtUrl.Text,
                RequestMethod = "POST",
                RequestBody = txtMW.Text,
                ResponseBody = "",
                CJSJ = DateTime.Now,
                GXSJ = DateTime.Now,
                Token = txtToken.Text,

            };
            return history;
        }

        #endregion

        #region 查询历史记录  绑定

        public void BindHistory(ApiHistory? history = null)
        {
            using var context = new SqliteContext();

            bool isRefresh = false;
            if (history != null)
            {
                var historyInfo = context.Db.Queryable<ApiHistory>().First(r => r.RequestRootPath == history.RequestRootPath && r.RequestUrl == history.RequestUrl && r.RequestBody == history.RequestBody);

                if (historyInfo == null)
                {
                    historyInfo = context.Db.Insertable(history).ExecuteReturnEntity();
                    var fullUrlParam = historyInfo.RequestRootPath + "||" + historyInfo.RequestUrl + "||" + historyInfo.RequestBody + "||" + historyInfo.Id;
                    listBox1.Items.Insert(0, fullUrlParam);
                }
                else
                {
                    historyInfo.GXSJ = DateTime.Now;
                    context.Db.Updateable(historyInfo).UpdateColumns(r => r.GXSJ).ExecuteCommand();
                    isRefresh = true;
                }

                if (!isRefresh)
                {
                    return;
                }
            }


            listBox1.Items.Clear();
            var list = context.Db.Queryable<ApiHistory>().OrderByDescending(r => r.GXSJ).Take(50).ToList();
            foreach (var item in list)
            {
                var fullUrlParam = item.RequestRootPath + "||" + item.RequestUrl + "||" + item.RequestBody + "||" + item.Id;
                if (!listBox1.Items.Contains(fullUrlParam))
                {
                    //历史记录
                    listBox1.Items.Add(fullUrlParam);
                }
            }

        }

        #endregion

        #region 其他方法
        /// <summary>
        /// 双击移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            // 使用 BeginInvoke 延迟执行，避免事件冲突
            this.BeginInvoke(new Action(async () =>
            {
                var box = (sender as ListBox);
                var item = box.SelectedItems[0];
                DelItem(item);
            }));
        }


        private void HZColor()
        {
            // 定义颜色方案
            Color keyColor = Color.Blue;           // 键的颜色
            Color stringColor = Color.Brown;       // 字符串值的颜色
            Color numberColor = Color.Green;       // 数字的颜色
            Color booleanColor = Color.Magenta;    // 布尔值的颜色

            // 使用正则表达式匹配并着色
            HighlightPattern(@"""[^""\\]*(?:\\.[^""\\]*)*""\s*:", keyColor); // 匹配 "key":
            HighlightPattern(@":\s*""[^""\\]*(?:\\.[^""\\]*)*""", stringColor); // 匹配 : "value"
            HighlightPattern(@":\s*\d+\.?\d*", numberColor); // 匹配 : 123 或 : 12.34
            HighlightPattern(@":\s*(true|false)", booleanColor); // 匹配 : true 或 : false

            // 恢复重绘
            txtMingW.ResumeLayout();
            txtMingwIn.ResumeLayout();
        }

        /// <summary>
        /// 使用正则表达式查找匹配项并设置颜色
        /// </summary>
        /// <param name="pattern">正则表达式模式</param>
        /// <param name="color">要应用的颜色</param>
        private void HighlightPattern(string pattern, Color color)
        {
            Regex regex = new Regex(pattern);
            string text = txtMingW.Text;
            MatchCollection matches = regex.Matches(text);

            foreach (Match match in matches)
            {
                // 选中匹配到的文本范围
                txtMingW.Select(match.Index, match.Length);
                // 设置选中部分的颜色
                txtMingW.SelectionColor = color;
            }

            // 重置选择，避免光标停留在最后一个匹配项
            txtMingW.Select(0, 0);

            Regex regex1 = new Regex(pattern);
            string text1 = txtMingwIn.Text;
            MatchCollection matches1 = regex1.Matches(text1);

            foreach (Match match in matches1)
            {
                // 选中匹配到的文本范围
                txtMingwIn.Select(match.Index, match.Length);
                // 设置选中部分的颜色
                txtMingwIn.SelectionColor = color;
            }

            // 重置选择，避免光标停留在最后一个匹配项
            txtMingwIn.Select(0, 0);
        }

        #endregion

        #region 刷新 请求记录
        /// <summary>
        /// 刷新 请求记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button9_Click(object sender, EventArgs e)
        {
            BindHistory();
        }
        #endregion

        #region 删除请求记录

        private async Task DelItem(object item)
        {
            string[] listArr = item?.ToString().Split("||");
            var id = listArr[3].ToString().ObjToInt();

            await new SqliteContext().Db.Deleteable<ApiHistory>().Where(r => r.Id == id).ExecuteCommandAsync();
            BindHistory();
        }

        private void DleItemOp_Click(object sender, EventArgs e)
        {
            var selItem = listBox1.SelectedItem;
            DelItem(selItem);
        }

        #endregion

        #region 执行SQL窗体 弹出
        /// <summary>
        /// 执行SQL窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            ExecuteSQLForm frm = new ExecuteSQLForm();
            frm.ShowDialog();
        }
        #endregion

        #region 文本筛选
       
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string text = textBox1.Text.Trim();

            if (e.KeyValue == 13)
            {
                using var context = new SqliteContext();
                var query = context.Db.Queryable<ApiHistory>();
                List<ApiHistory> list = new List<ApiHistory>();
                if (string.IsNullOrEmpty(text))
                {
                    list = query.ToList();
                }
                else
                {

                    list = context.Db.Queryable<ApiHistory>().Where(r => r.RequestUrl.Contains(text) || r.RequestRootPath.Contains(text)).OrderByDescending(r => r.GXSJ).Take(50).ToList();
                }

                listBox1.Items.Clear();
                foreach (var item in list)
                {
                    var fullUrlParam = item.RequestRootPath + "||" + item.RequestUrl + "||" + item.RequestBody + "||" + item.Id;
                    if (!listBox1.Items.Contains(fullUrlParam))
                    {
                        //历史记录
                        listBox1.Items.Add(fullUrlParam);
                    }
                }
            }
        }

        #endregion
    }
}
