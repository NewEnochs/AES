using AES;
using AES.Helper;
using AES.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace AES
{

    public static class ApiHelper
    {
        // 推荐：为整个应用复用一个 HttpClient 实例（避免端口耗尽）
        private static readonly HttpClient _httpClient = new HttpClient();


        /// <summary>
        /// 公卫请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonstr">加密的参数/有的方法 白名单 明文请求不需要加密</param>
        /// <param name="token"></param>
        /// <param name="type">类型 1.慢病 2.公卫</param>
        /// <param name="isJM">是否在此方法给参数加密 默认不加密</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<MessageInfo?> HttpApi(string url, string jsonstr, string token, int type = 1, bool isJM = false, bool isCS = true)
        {
            try
            {
                url = url.Replace("//", "/").Replace("http:/", "http://");
                if (type == 2)
                {
                   var resData= await HttpApi(url, jsonstr);
                    return resData;
                }

                
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

                request.Headers.Accept.ParseAdd("text/html,application/xhtml+xml,*/*");
                // 只有当 token 非空时才添加 Authorization 头（更灵活）
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                string requestJson = jsonstr;

                if (isJM)
                {
                    requestJson = FilterAES.FileterEncrypt(jsonstr);
                }
                //string requestJson = JsonSerializer.Serialize(requestData);

                if (isCS)
                {
                    request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                }
                HttpResponseMessage response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();
                if (type == 1)
                {
                    MessageInfo messageinfo = JsonSerializer.Deserialize<MessageInfo>(result);
                    return messageinfo;
                }
                else if (type == 2)
                {
                    var data = CHISAES.AESDEncrypt(result);
                    MessageInfo messageinfo = JsonSerializer.Deserialize<MessageInfo>(data);
                    return messageinfo;
                }
                return null;
            }
            catch (Exception ex)
            {
                // 这里可以根据需要处理异常
                throw new Exception($"HTTP请求失败: {ex.Message}", ex);
            }
        }


        /// <summary>
        /// 慢病平台请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonstr"></param>
        /// <param name="ymdz"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<MessageInfo> HttpApi(string url, string jsonstr = "", int ymdz = 0, string type = "POST")
        {
            string requestUrl = url;

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(
                    new HttpMethod(type.ToUpper()),
                    requestUrl);

                request.Headers.Accept.ParseAdd("text/html,application/xhtml+xml,*/*");
                request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiLllrvmnKzlhYgiLCJqdGkiOiI2MmE2NTg1NC00NmYyLTRlOTYtYmJiMS0zMTc0YTIzMTYxY2MiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6WyIxMDAwMDEiLCIxMDAwMDExMTAwMSJdLCJuYmYiOjE3Njk1ODAzNjksImV4cCI6MTc2OTY2Njc2OSwiaXNzIjoiZXN0IiwiYXVkIjoiY2hpc3VpIn0.Wn1B9GQVR6qetpZO5G_lUrjlJsxkIsG7pgunrH5nl0M");
                request.Headers.Add("JTI", "62a65854-46f2-4e96-bbb1-3174a23161cc");
                var requestData = new { RSASTR = jsonstr };
                string requestJson = JsonSerializer.Serialize(requestData);

                request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();
                var result = EncryptionHelper.AESDEncrypt(responseContent);
                MessageInfo messageinfo = JsonSerializer.Deserialize<MessageInfo>(result);
                return messageinfo;
            }
            catch (Exception ex)
            {
                // 这里可以根据需要处理异常
                throw new Exception($"HTTP请求失败: {ex.Message}", ex);
            }
        }


        public static SmsResult WebRequests(string url, string postModel = "", string type = "POST")
        {
            Encoding encoding = Encoding.UTF8;

            HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(url);//webrequest请求api地址  
            request.Timeout = 180000;
            request.Accept = "application/json, text/plain, */*";
            request.ContentType = "application/json; charset=utf-8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";
            request.Method = type.ToUpper().ToString();//get或者post
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiLllrvmnKzlhYgiLCJqdGkiOiI2MmE2NTg1NC00NmYyLTRlOTYtYmJiMS0zMTc0YTIzMTYxY2MiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6WyIxMDAwMDEiLCIxMDAwMDExMTAwMSJdLCJuYmYiOjE3Njk1ODAzNjksImV4cCI6MTc2OTY2Njc2OSwiaXNzIjoiZXN0IiwiYXVkIjoiY2hpc3VpIn0.Wn1B9GQVR6qetpZO5G_lUrjlJsxkIsG7pgunrH5nl0M");

            if (type.ToUpper() == "POST")
            {
                byte[] buffer = encoding.GetBytes(postModel);
                request.ContentType = "application/json";
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                var retM = reader.ReadToEnd();

                SmsResult messageinfo = retM.DeserializeObject<SmsResult>();
                return messageinfo;
            }
        }


        // 可选：释放 HttpClient（一般不需要手动调用，除非程序退出）
        public static void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
