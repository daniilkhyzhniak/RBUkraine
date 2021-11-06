using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using RBUkraine.BLL.Models.User;

namespace RBUkraine.BLL.Contracts
{
    public interface IUserService
    {
        Task CreateUserAsync(UserCreationModel model);
        
        Task<UserModel> GetUserByEmailAsync(string email);
        
        Task<ICollection<Claim>> AuthenticateAsync(AuthModel authModel);

        Task SetNewPasswordAsync(string email, string newPassword);

        Task<ICollection<Claim>> LoginWithGoogle(string token);

        Task Update(int id, UserEditorModel model);
    }
}