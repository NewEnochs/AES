using AES.DataBase;
using AES.Helper;
using AES.Model;
using AES.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
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

        public Form1()
        {
            InitializeComponent();
            // 加载配置
            config = AppConfig.Load();

            ReCalcCount();

            txtZH.Text = "superAdmin";
            txtPwd.Text = "Estoom@?2023";
            button1_Click(null, null);
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

                txtZH.Text = "5203211101";
                txtPwd.Text = "1234ASDF";
                txtUrl.Text = lastGwUrl;
                txtMW.Text = lastGwMW;
                lastGwUrl = txtUrl.Text;
                lastGwMW = txtMW.Text;

                txtMY.Text = "C0D2ACC1205B4028A4888CAC475FBE35";    //密匙
                txtPYL.Text = "";   // 偏移量
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
        private void button1_Click(object sender, EventArgs e)
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

                    //if (string.IsNullOrEmpty(json))
                    //{
                    //    MessageBox.Show("请输入参数");
                    //    return;
                    //}

                    string? url = txtRootPath.Text + (txtUrl.Text.StartsWith('/') ? txtUrl.Text.Trim() : "/" + txtUrl.Text);
                    string? token = txtToken.Text;

                    using var context = new SqliteContext();

                    info = await ApiHelper.HttpApi(url, json, token, isCS: !chkParam.Checked);
                    var fullUrlParam = txtRootPath.Text + "||" + txtUrl.Text + "||" + txtMW.Text;
                    if (!listBox1.Items.Contains(fullUrlParam))
                    {
                        //历史记录
                        listBox1.Items.Add(fullUrlParam);
                    }

                    if (info != null)
                    {
                        txtMingW.Text = formatJson(info.ToJson());
                    }
                }
                else if (type == 2)     //公卫
                {
                    string json = txtMW.Text;
                    var jsonstr = CHISAES.AESDEncrypt(json);
                    if (btn != null && btn.Text == "明文请求")
                    {
                        jsonstr = txtMingW.Text.Trim();
                    }
                    BeRequest requests = jsonstr.ToObject<BeRequest>();
                    if (requests.Objects != null && requests.Objects.Count > 0)
                    {
                        var oejcts = requests.Objects;
                        requests.Objects = new Hashtable();
                        foreach (DictionaryEntry item in oejcts)
                        {
                            var key = item.Key.ToString();
                            var value = item.Value.ToString();
                            requests.Objects.Add(key, value);
                        }
                    }
                    requests.GH = requests.GH ?? "5203211101";
                    requests.MM = requests.MM ?? "1234ASDF";
                    requests.JGBM = requests.JGBM ?? "10000111001";

                    json = CHISAES.AESEncrypt(requests.ToJson());
                    string? url = txtRootPath.Text + txtUrl.Text;
                    string? token = txtToken.Text;

                    info = await ApiHelper.HttpApi(url, json, token, 2, isCS: !chkParam.Checked);
                    var fullUrlParam = txtRootPath.Text + "||" + txtUrl.Text + "||" + txtMW.Text;
                    if (!listBox1.Items.Contains(fullUrlParam))
                    {
                        //历史记录
                        listBox1.Items.Add(fullUrlParam);
                    }

                    if (info != null)
                    {
                        txtMingW.Text = formatJson(info.ToJson());
                    }
                }


            }
            catch (Exception ex)
            {
                txtMingW.Text = ex.Message;
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
                txtMingW.Text = formatJson(decyptData);
            }
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
            }

            lblStatus.Text = $"当前地址 ({1}/{addressCount})";
        }
        #endregion

        #endregion

        #region 结果格式化
        /// <summary>
        /// 结果格式化
        /// </summary>
        private void bntFormat_Click(object sender, EventArgs e)
        {
            txtMingW.Text = formatJson(txtMingW.Text);
        }
        #endregion

        #region 结果清空
        /// <summary>
        /// 结果清空
        /// </summary>
        private void btnEmptyResult_Click(object sender, EventArgs e)
        {
            txtMingW.Text = string.Empty;
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
                }
                lblStatus.Text = $"当前地址 ({currentIndex + 1}/{addressCount})";
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
                var item = box.SelectedItems[0];
                string[] listArr = item?.ToString().Split("||");
                txtRootPath.Text = listArr[0];
                txtUrl.Text = listArr[1];
                txtMW.Text = listArr[2];
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
            CreateDataBase.CreateDatabaseAndTable();
        }
        #endregion

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            // 使用 BeginInvoke 延迟执行，避免事件冲突
            this.BeginInvoke(new Action(() =>
            {
                if (listBox1.SelectedItem != null)
                {
                    listBox1.Items.Remove(listBox1.SelectedItem);
                }
            }));
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }
    }
}
