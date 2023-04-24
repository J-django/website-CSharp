using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Lib60;
using website.helper;
using website.models;

namespace website.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatGptController : ControllerBase
    {
        private readonly ILogger<ChatGptController> _logger;

        public ChatGptController(ILogger<ChatGptController> logger)
        {
            _logger = logger;
        }

        [HttpPost("getChatReply")]
        public async Task<Response<chatResponse>> getChatReply(contentRequest content)
        {
            try
            {
                chatResponse res = await new ChatGptHelper().getChatReply(content);
                return new Response<chatResponse>().SuccessResult(res);
            }
            catch (Exception ex)
            {
                return new Response<chatResponse>().ErrorResult("错误！"+ex.Message);
            }
        }
    }
}
