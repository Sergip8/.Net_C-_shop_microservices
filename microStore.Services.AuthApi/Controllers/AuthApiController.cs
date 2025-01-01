using Microsoft.AspNetCore.Mvc;
using microStore.Services.AuthApi.Models.DTO;
using microStore.Services.AuthApi.Service.IService;

namespace microStore.Services.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : Controller
    {
        private readonly IAuthService _authService;
        protected ResponseDTO _responseDTO;

        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
            _responseDTO = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO requestDTO)
        {
            var res = await _authService.Register(requestDTO);


            return Ok(res);


        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var res = await _authService.Login(loginRequest);


            return Ok(res);

        }

        [HttpPost("role")]
        public async Task<IActionResult> AssingRole([FromBody] RegistrationRequestDTO registrationRequest)
        {
            var res = await _authService.AssingRole(registrationRequest.Email, registrationRequest.Role.ToUpper());

            if (res.Success)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }
        [HttpPost("ids")]
        public IActionResult GetByIds([FromBody] Ids ids)
        {
            var res = _authService.getUsersByIds(ids.UserIds);

            if (res.Success)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }

    }
    public class Ids
    {
        public List<string> UserIds { get; set; }
    }

}