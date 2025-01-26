
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.RewardAPI.Data;
using Restaurant.Services.RewardAPI.Message;
using Restaurant.Services.RewardAPI.Models;

namespace Restaurant.Services.RewardAPI.Services
{
    public class RewardService : IRewardService
    {
        private DbContextOptions<AppDbContext> _dboptions;

        public RewardService(DbContextOptions<AppDbContext> dboptions)
        {
            _dboptions = dboptions;
        }

        public async Task UpdateRewards(RewardMessage rewardMessage)
        {
            try
            {
                Reward reward = new()
                {
                    OrderId = rewardMessage.OrderId,
                    RewardsActivity = rewardMessage.RewardsActivity,
                    UserId = rewardMessage.UserId,
                    RewardsDate = DateTime.Now
                };
                await using var _db = new AppDbContext(_dboptions);
                await _db.Rewards.AddAsync(reward);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
        }
    }
}
