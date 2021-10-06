using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Models;
using RBUkraine.DAL.Contexts;

namespace RBUkraine.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(
            AppDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .Include(user => user.UserRoles)
                .ThenInclude(userRole => userRole.Role)
                .FirstOrDefaultAsync(user => user.Email == email);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<ICollection<Claim>> AuthenticateAsync(AuthModel authModel)
        {
            var user = await _context.Users
                .Include(user => user.UserRoles)
                .ThenInclude(userRole => userRole.Role)
                .FirstOrDefaultAsync(user => user.Email == authModel.Email);

            if (user is null)
            {
                return ArraySegment<Claim>.Empty;
            }
            
            if (!BCrypt.Net.BCrypt.Verify(authModel.Password, user.Password))
            {
                return ArraySegment<Claim>.Empty;
            }

            var claims = new List<Claim>
            {
                new("Id", user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
            };

            claims.AddRange(user.UserRoles
                .Select(userRole => new Claim(ClaimTypes.Role, userRole.Role.Name)));
            
            return claims;
        }
    }
}