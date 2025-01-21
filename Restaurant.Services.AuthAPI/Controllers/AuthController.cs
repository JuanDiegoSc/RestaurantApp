using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Restaurant.MessageBus;
using Restaurant.Services.AuthAPI.Models.Dto;
using Restaurant.Services.AuthAPI.Service.IService;

namespace Restaurant.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _config;

        protected ResponseDto _responseDto;
        public AuthController(IAuthService authService, IMessageBus messageBus, IConfiguration config)
        {
            _authService = authService;
            _responseDto = new();
            _messageBus = messageBus;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {

            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                return BadRequest(_responseDto);
            }
            await _messageBus.PublishMessage(model.Email, _config.GetValue<string>("TopicAndQueuesNames:RegisterUserQueue"));
            return Ok(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);

         if(loginResponse.User == null)
         {
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Username or password is incorrect";
            return BadRequest(_responseDto);
          }

          _responseDto.Result = loginResponse;
          return Ok(_responseDto);
        }

        [HttpPost("AssingRole")]
        public async Task<IActionResult> AssingRole([FromBody] RegistrationRequestDto model)
        {
            var AssingRoleSuccessful = await _authService.AssingRole(model.Email, model.Role.ToUpper() );

            if (!AssingRoleSuccessful)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error";
                return BadRequest(_responseDto);
            }

            _responseDto.Result = AssingRoleSuccessful;
            return Ok(_responseDto);
        }
    }
}
