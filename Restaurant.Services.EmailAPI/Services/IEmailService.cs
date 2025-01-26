using Restaurant.Services.EmailAPI.Message;
using Restaurant.Services.EmailAPI.Models.Dto;

namespace Restaurant.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
        Task RegisterUserEmailAndLog(string email);

        Task LogOrderPlaced(RewardMessage rewardMessage);
    }
}
