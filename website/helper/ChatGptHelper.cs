using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Xml.Linq;
using website.models;

namespace website.helper
{
    public class ChatGptHelper
    {
        public async Task<chatResponse> getChatReply(contentRequest _data)
        {
            try
            {
                // 创建 HttpClientHandler 对象，并设置代理服务器
                var handler = new HttpClientHandler
                {
                    Proxy = new WebProxy("http://127.0.0.1:7891"),
                    UseProxy = true
                };

                // 创建 HttpClient 对象，并设置 HttpClientHandler 对象
                var client = new HttpClient(handler);

                // 设置请求头
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("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");

                // 创建实体类对象
                var data = new chatRequest()
                {
                    model = "gpt-3.5-turbo",
                    messages = new List<message>() { new message() {
                        role="user",
                        content="aaaa"
                    } }
                };

                // 发送 POST 请求，并传递实体类参数
                var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", data);

                // 处理响应
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("结果", result);
                    return JsonConvert.DeserializeObject<chatResponse>(result);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return new chatResponse();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
