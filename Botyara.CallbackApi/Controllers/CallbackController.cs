using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VkNet.Model;
using VkNet.Utils;

using Botyara.Core;

namespace Botyara.CallbackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        private readonly IConfiguration _configuration;

        private CallbackAnswerer Answerer { get; }

        public CallbackController(IConfiguration configuration)
        {
            _configuration = configuration;
            Answerer = new CallbackAnswerer()
        }

        [HttpPost("/")]
        public IActionResult Callback([FromBody] Updates updates)
        {
            // Тип события
            switch (updates.Type)
            {
                // Ключ-подтверждение
                case "confirmation":
                {
                    return Ok(_configuration["Config:Confirmation"]);
                }

                // Новое сообщение
                case "message_new":
                {
                    // Десериализация
                    var msg = Message.FromJson(new VkResponse(updates.Object));
                    break;
                }
            }

            return Ok("ok");
        }
    }
}
