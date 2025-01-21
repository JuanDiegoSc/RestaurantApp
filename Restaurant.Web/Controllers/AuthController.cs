using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Restaurant.Web.Models;
using Restaurant.Web.Service.IService;
using Restaurant.Web.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Restaurant.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider; 
        }

        [HttpGet]
        public IActionResult LoginView()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> LoginView(LoginRequestDto obj)
        {
            ResponseDto responseDto = await _authService.LoginAsync(obj);
            if (responseDto != null && responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto = 
                    JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                await SignInUser(loginResponseDto);

                _tokenProvider.SetToken(loginResponseDto.Token);
                TempData["Success"] = "Login Successful";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = responseDto.Message;
                return View(obj);
            }
        }


        [HttpGet]
        public IActionResult RegisterView()
        {
            var roleList = new List<SelectListItem>() {
                new SelectListItem{Text = SD.RoleAdmin, Value = SD.RoleAdmin},
                new SelectListItem{Text = SD.RoleCustomer, Value = SD.RoleCustomer}
            };
            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterView(RegistrationRequestDto obj)
        {
            ResponseDto result = await _authService.RegisterAsync(obj);
            ResponseDto roleResult;
            if(result!=null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleCustomer;
                }
                roleResult = await _authService.AssingRoleAsync(obj);
                if(roleResult!=null && roleResult.IsSuccess)
                {
                    TempData["Success"] = "Registration Successful";
                    return RedirectToAction("LoginView");
                }
            }
            else
            {
                TempData["error"] = result.Message;
               
            }
            var roleList = new List<SelectListItem>() {
                new SelectListItem{Text = SD.RoleAdmin, Value = SD.RoleAdmin},
                new SelectListItem{Text = SD.RoleCustomer, Value = SD.RoleCustomer}
            };
            ViewBag.RoleList = roleList;
            return View(obj);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }
        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim
                (
                new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value)
                );

            identity.AddClaim
              (
              new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value)
              );

            identity.AddClaim
              (
              new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value)
              );

            identity.AddClaim
             (
             new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value)
             );

            identity.AddClaim
            (
            new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value)
            );

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
