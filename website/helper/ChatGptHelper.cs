using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
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
                Console.WriteLine("开始设置代理");
                // 创建HttpClientHandler实例，设置代理
                var handler = new HttpClientHandler
                {
                    Proxy = new Socks5ProxyClient("127.0.0.1", 7891)
                };

                // 创建HttpClient实例，使用HttpClientHandler
                var client = new HttpClient(handler);
                Console.WriteLine("代理配置完成");

                // 设置请求头
                Console.WriteLine("开始设置请求头");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");
                Console.WriteLine("请求头设置完成");

                Console.WriteLine("创建实体类对象");
                // 创建实体类参数
                var data = new chatRequest()
                {
                    model = "gpt-3.5-turbo",
                    messages = new message[]
                    {
                        new message(){
                            role="user",
                            content=_data.content
                        }
                    }
                };
                Console.WriteLine("实体类对象创建完成");

                // 将实体类参数序列化为JSON字符串
                Console.WriteLine("开始序列化实体类对象");
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                Console.WriteLine("序列化实体类对象完成");

                // 创建HttpContent实例，设置请求体
                Console.WriteLine("将数据已UTF-8格式转换存储" + json);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                Console.WriteLine("将数据已UTF-8格式转换存储完成");

                // 发送POST请求
                Console.WriteLine("开始发送请求");
                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                Console.WriteLine("请求结束");

                // 获取响应内容
                Console.WriteLine("开始解析返回结果");
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("解析返回结果完成");

                Console.WriteLine(@$"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}请求结果：{result}");
                return JsonConvert.DeserializeObject<chatResponse>(result)!;

            }
            catch (Exception ex)
            {
                Console.WriteLine(@$"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}发生错误：{ex.ToString()}");
                throw;
            }
        }

        public class Socks5ProxyClient : IWebProxy
        {
            private readonly Uri _proxyUri;

            public Socks5ProxyClient(string host, int port)
            {
                _proxyUri = new Uri($"socks5://{host}:{port}");
            }
            public ICredentials Credentials { get; set; }

            public Uri GetProxy(Uri destination)
            {
                return _proxyUri;
            }

            public bool IsBypassed(Uri host)
            {
                return false;
            }
        }
    }
}
