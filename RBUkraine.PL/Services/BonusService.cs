using System;
using System.Threading.Tasks;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Models.User;
using RBUkraine.PL.EmailSender;
using RBUkraine.PL.EmailSender.Models;

namespace RBUkraine.PL.Services
{
    public class BonusService : IBonusService
    {
        private const decimal BonusValue = 5000;

        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;
        private readonly IDonationService _donationService;

        public BonusService(
            IEmailSender emailSender, 
            IUserService userService, 
            IDonationService donationService)
        {
            _emailSender = emailSender;
            _userService = userService;
            _donationService = donationService;
        }

        public async Task SendBonus(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);

            if (user.IsBonusReceived)
            {
                return;
            }

            if (user.TotalDonationAmount < BonusValue)
            {
                return;
            }

            await _userService.Update(user.Id, new UserEditorModel
            {
                Nickname = user.Nickname,
                IncludeInRating = user.IncludeInRating,
                IsBonusReceived = true
            });

            await _emailSender.SendEmailAsync(new EmailModel
            {
                Email = user.Email,
                Subject = "RBUkraine. Бонус",
                Message = $@"<p>Вы получили бонус за благотворительные взносы.</p>
                             <p>В ближайшее время с Вами свяжется менеджер компании.</p>"
            });
        }
    }
}
