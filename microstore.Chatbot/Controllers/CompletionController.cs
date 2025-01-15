using Microsoft.AspNetCore.Mvc;
using microstore.Chatbot.Services;
using OpenAIApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OpenAIApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Endpoints for the regular completion models")]
    public class CompletionController : ControllerBase
    {
        private readonly ILogger<CompletionController> _logger;
        private readonly IAIService _openAIService;

        public CompletionController(ILogger<CompletionController> logger, IAIService completionService)
        {
            _logger = logger;
            _openAIService = completionService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCompletionFromPrompt([FromQuery] ChatPrompt prompt)
        {
            try
            {
                var response = await _openAIService.GetCompletionResponseAsync(prompt.Message);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}