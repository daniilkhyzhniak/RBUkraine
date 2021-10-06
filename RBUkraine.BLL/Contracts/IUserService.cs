using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using RBUkraine.BLL.Models;

namespace RBUkraine.BLL.Contracts
{
    public interface IUserService
    {
        Task CreateUserAsync(UserCreationModel model);
        
        Task<UserModel> GetUserByEmailAsync(string email);
        
        Task<ICollection<Claim>> AuthenticateAsync(AuthModel authModel);
    }
}