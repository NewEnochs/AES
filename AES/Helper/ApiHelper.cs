using AES;
using AES.Helper;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AES
{

    public static class ApiHelper
    {
        // 推荐：为整个应用复用一个 HttpClient 实例（避免端口耗尽）
        private static readonly HttpClient _httpClient = new HttpClient();


        /// <summary>
        /// 
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


        // 可选：释放 HttpClient（一般不需要手动调用，除非程序退出）
        public static void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
