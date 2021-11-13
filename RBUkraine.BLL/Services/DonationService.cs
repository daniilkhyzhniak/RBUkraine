using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Models.CharitableContribution;
using RBUkraine.DAL.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RBUkraine.BLL.Models.Donation;
using RBUkraine.DAL.Entities;

namespace RBUkraine.BLL.Services
{
    public class DonationService : IDonationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DonationService(
            AppDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task Create(DonationModel model)
        {
            _context.Donations.Add(new Donation
            {
                UserId = model.UserId,
                AnimalId = model.AnimalId,
                CharitableOrganizationId = model.CharitableOrganizationId,
                Amount = model.Amount
            });

            return _context.SaveChangesAsync();
        }

        public IEnumerable<RatingItemModel> GetRating()
        {
            var ratingItems = _context.RatingItems.FromSqlRaw(
                @"select isnull(u.Nickname, u.Email) as UserName, d.Amount
                from (
	                select UserId, SUM(Amount) as Amount
	                from Donations
	                group by UserId
                ) as d
                inner join Users u
                on u.Id = d.UserId
                order by d.Amount desc");

            return _mapper.Map<IEnumerable<RatingItemModel>>(ratingItems);
        }
    }
}
