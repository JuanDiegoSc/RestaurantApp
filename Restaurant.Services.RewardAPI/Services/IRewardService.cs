
using Restaurant.Services.RewardAPI.Message;

namespace Restaurant.Services.RewardAPI.Services
{
    public interface IRewardService
    {
        Task UpdateRewards(RewardMessage rewardMessage);
        
    }
}
