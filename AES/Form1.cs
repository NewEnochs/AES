using AES.DataBase;
using AES.Helper;
using AES.Model;
using AES.Util;
using Newtonsoft.Json.Linq;
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

        public Form1()
        {
            InitializeComponent();
            // 加载配置
            config = AppConfig.LoadConfig();
            addressCount = config.Addresses.Count;

            txtRootPath.Text = config.Addresses[0];
            lblStatus.Text = $"当前地址 ({1}/{addressCount})";

            txtZH.Text = "superAdmin";
            txtPwd.Text = "Estoom@?2023";
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

                var mw = CHISAES.AESDEncrypt(MingW);
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
                        string formattedJson = jsonObject.ToString((Newtonsoft.Json.Formatting)Formatting.Indented);
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
                        string formattedJson = jsonObject.ToString((Newtonsoft.Json.Formatting)Formatting.Indented);
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
                txtRootPath.Text = "http://localhost:8055";

                txtMY.Text = "GVvVJyrsFRKms8XKhwfwpgB47DtIaZ2p";
                txtPYL.Text = "4XEUxWxkTSGcEZxe";
                txtMW.Text = string.Empty;
            }
            else if (btn.Name == "btnGW")
            {
                type = 2;
                btnMB.BackColor = SystemColors.Window;
                btnGW.BackColor = Color.FromArgb(134, 124, 228);

                txtZH.Text = "5203211101";
                txtPwd.Text = "1234ASDF";
                txtRootPath.Text = "http://localhost:5000";
                txtUrl.Text = "/api/JKTJB/Postjktjb";

                txtMY.Text = "C0D2ACC1205B4028A4888CAC475FBE35";
                txtPYL.Text = "";
            }

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

                    string? url = txtRootPath.Text + txtUrl.Text;
                    string? token = txtToken.Text;

                    using var context = new SqliteContext();

                    info = await ApiHelper.HttpApi(url, json, token, isCS: !chkParam.Checked);
                    var item = txtRootPath.Text + "||" + txtUrl.Text + "||" + txtMW.Text;
                    if (!listBox1.Items.Contains(item))
                    {
                        //历史记录
                        listBox1.Items.Add(item);
                    }

                    if (info != null && info.code > 0)
                    {
                        txtMingW.Text = formatJson(info.ToJson());
                    }


                }
                else if (type == 2)     //公卫
                {
                    string json = txtMW.Text;
                    string? url = txtRootPath.Text + txtUrl.Text;
                    string? token = txtToken.Text;

                    info = await ApiHelper.HttpApi(url, json, token, 2, isCS: !chkParam.Checked);
                    if (info != null && info.code > 0)
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
                string decyptData = FilterAES.FileterDecrypt(info.data);
                txtMingW.Text = formatJson(decyptData);
            }
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
                txtRootPath.Text = config.Addresses[currentIndex];
                lblStatus.Text = $"当前地址 ({currentIndex + 1}/{addressCount})";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            string formattedJson = jsonObject.ToString((Newtonsoft.Json.Formatting)Formatting.Indented);
            return formattedJson;
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
            var chk = (sender as CheckBox)?.Checked;
            if (chk.Value)
            {
                // 加载配置
                config = AppConfig.LoadWebConfig();
            }
            else
            {
                // 加载配置
                config = AppConfig.LoadConfig();
            }

            addressCount = config.Addresses.Count;

            txtRootPath.Text = config.Addresses[0];
            lblStatus.Text = $"当前地址 ({1}/{addressCount})";
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
            // 移除当前选中的项
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }
    }
}
