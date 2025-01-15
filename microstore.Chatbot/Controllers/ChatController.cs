using Microsoft.AspNetCore.Mvc;
using microstore.Chatbot.Services;
using OpenAIApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OpenAIApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Endpoints for the ChatGPT chat model")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IAIService _openAIService;

        public ChatController(ILogger<ChatController> logger, IAIService completionService)
        {
            _logger = logger;
            _openAIService = completionService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCompletionFromPrompt([FromQuery] ChatPrompt prompt)
        {
            try
            {
                var response = await _openAIService.GetChatResponseAsync(prompt.Message);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("FromRequest")]
        public async Task<IActionResult> GetCompletionFromRequest([FromBody] ChatRequest chatRequest)
        {
            try
            {
                var response = await _openAIService.GetChatResponseAsync(chatRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}