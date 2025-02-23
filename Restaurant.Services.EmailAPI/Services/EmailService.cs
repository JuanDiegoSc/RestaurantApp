﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.EmailAPI.Data;
using Restaurant.Services.EmailAPI.Message;
using Restaurant.Services.EmailAPI.Models;
using Restaurant.Services.EmailAPI.Models.Dto;
using System.Text;

namespace Restaurant.Services.EmailAPI.Services
{
    public class EmailService : IEmailService
    {
        private DbContextOptions<AppDbContext> _dboptions;

        public EmailService(DbContextOptions<AppDbContext> dboptions)
        {
            _dboptions = dboptions;
        }

        public async Task EmailCartAndLog(CartDto cartDto)
        {
            StringBuilder message = new StringBuilder();

            message.AppendLine("<br/>Cart Email Requested ");
            message.AppendLine("<br/>Total " + cartDto.CartHeader.CartTotal);
            message.Append("<br/>");
            message.Append("<ul>");
            foreach (var item in cartDto.CartDetails)
            {
                message.Append("<li>");
                message.Append(item.Product.Name + " x " + item.Count);
                message.Append("</li>");
            }
            message.Append("</ul>");

            await LogAndEmail(message.ToString(), cartDto.CartHeader.Email);
        }

        public async Task LogOrderPlaced(RewardMessage rewardMessage)
        {
            string message = "New Order Placed. <br/> Order Id : " + rewardMessage.OrderId;
            await LogAndEmail(message, "restaurant@gmail.com");
        }

        public async Task RegisterUserEmailAndLog(string email)
        {
            string message = "User Registeration Successful. <br/> Email : " + email;
            await LogAndEmail(message, "restaurant@gmail.com");
        }

        private async Task<bool> LogAndEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLog = new()
                {
                    Email = email,
                    EmailSent = DateTime.Now,
                    Message = message
                };
                await using var _db = new AppDbContext(_dboptions);
                await _db.EmailLoggers.AddAsync(emailLog);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
