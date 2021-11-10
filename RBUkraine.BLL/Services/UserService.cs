using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.User;
using RBUkraine.DAL.Contexts;
using RBUkraine.DAL.Entities;

namespace RBUkraine.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _googleClientId;
        
        public UserService(
            AppDbContext context, 
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _googleClientId = configuration["GoogleAuth:ClientId"];
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
                new(ClaimTypes.Email, user.Email)
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

        public async Task<ICollection<Claim>> LoginWithGoogle(string token)
        {
            var googlePayload = await GoogleJsonWebSignature.ValidateAsync(token,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new List<string>
                    {
                        _googleClientId
                    }
                });

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == googlePayload.Email) 
                       ?? await CreateUserFromGoogle(googlePayload.Email, googlePayload.Name);

            user.UserRoles = await _context.UserRoles
                .Include(userRole => userRole.Role)
                .Where(userRole => userRole.UserId == user.Id)
                .ToListAsync();

            var claims = new List<Claim>
            {
                new("Id", user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
            };

            claims.AddRange(user.UserRoles
                .Select(userRole => new Claim(ClaimTypes.Role, userRole.Role.Name)));

            return claims;
        }

        public async Task Update(int id, UserEditorModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
            {
                return;
            }

            user.Nickname = model.Nickname;
            user.IncludeInRating = model.IncludeInRating;

            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        private async Task<User> CreateUserFromGoogle(string email, string nickname)
        {
            var userRole = _context.Roles.FirstOrDefault(role => role.Name == Roles.User);

            if (userRole is null)
            {
                throw new ArgumentException("User role not found");
            }

            var user = new User
            {
                Email = email,
                Nickname = nickname,
                UserRoles = new List<UserRole>
                {
                    new()
                    {
                        RoleId = userRole.Id
                    }
                }
            };

            _context.Users.Add(user);

            user.UserRoles.Add(new UserRole
            {
                RoleId = userRole.Id
            });

            await _context.SaveChangesAsync();

            return user;
        }
    }
}