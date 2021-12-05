using System;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Models.CharitableContribution;
using RBUkraine.DAL.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Data.SqlClient;
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
                @"select 
	                ISNULL(u.Nickname, u.Email) as UserName,
	                z.Amount
                from (
	                select
		                UserId, 
		                SUM(Amount) as Amount
	                from (
		                select 
			                UserId, 
			                SUM(Amount) as Amount
		                from Donations
		                group by UserId

		                union

		                select 
			                UserId, 
			                SUM(od.Price) as Amount
		                from Orders o
		                inner join OrderDetails od
		                on o.Id = od.OrderId
		                group by o.UserId
	                ) as q
	                group by q.UserId
                ) as z
                inner join Users u
                on u.Id = z.UserId
                where u.IncludeInRating = 1
                order by z.Amount desc").ToList();

            return _mapper.Map<IEnumerable<RatingItemModel>>(ratingItems);
        }

        public async Task<decimal> GetTotalAmount(int userId)
        {
            var parameters = new SqlParameter("@UserId", userId);
            var ratingItem = await _context.RatingItems.FromSqlRaw(
                @"select 
	                ISNULL(u.Nickname, u.Email) as UserName,
	                z.Amount
                from (
	                select
		                UserId, 
		                SUM(Amount) as Amount
	                from (
		                select 
			                UserId, 
			                SUM(Amount) as Amount
		                from Donations
		                where UserId = @UserId
		                group by UserId

		                union

		                select 
			                UserId, 
			                SUM(od.Price) as Amount
		                from Orders o
		                inner join OrderDetails od
		                on o.Id = od.OrderId
		                where o.UserId = @UserId
		                group by o.UserId
	                ) as q
	                group by q.UserId
                ) as z
                inner join Users u
                on u.Id = z.UserId", parameters).FirstOrDefaultAsync();

            return ratingItem?.Amount ?? 0;
        }
    }
}
