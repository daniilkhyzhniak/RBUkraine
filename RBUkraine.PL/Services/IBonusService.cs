using System.Threading.Tasks;

namespace RBUkraine.PL.Services
{
    public interface IBonusService
    {
        Task SendBonus(string email);
    }
}
