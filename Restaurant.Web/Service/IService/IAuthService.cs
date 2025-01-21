using Microsoft.AspNetCore.Identity.Data;
using Restaurant.Web.Models;

namespace Restaurant.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> AssingRoleAsync(RegistrationRequestDto registrationRequestDto);
    }
}
