using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models;
using RBUkraine.BLL.Models.User;
using RBUkraine.DAL.Contexts;
using RBUkraine.DAL.Entities;

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

        public async Task CreateUserAsync(UserCreationModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == model.Email);

            if (user is not null)
            {
                throw new ArgumentException("User already exists");
            }

            var userRole = _context.Roles.FirstOrDefault(role => role.Name == Roles.User);

            if (userRole is null)
            {
                throw new ArgumentException("User role not found");
            }
            
            var newUser = _mapper.Map<User>(model);
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            newUser.UserRoles.Add(new UserRole
            {
                RoleId = userRole.Id
            });

            _context.Add(newUser);
            await _context.SaveChangesAsync();
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
            var user =  await _context.Users
                .FirstOrDefaultAsync(user => user.Email == authModel.Email);

            if (user is null)
            {
                return ArraySegment<Claim>.Empty;
            }
            
            user.UserRoles = _context.UserRoles
                .Include(userRole => userRole.Role)
                .Where(userRole => userRole.UserId == user.Id)
                .ToList();
            
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
        
        public async Task SetNewPasswordAsync(string email, string newPassword)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.Email == email);

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}