using RBUkraine.BLL.Models.CharitableContribution;
using System.Collections.Generic;
using System.Threading.Tasks;
using RBUkraine.BLL.Models.Donation;

namespace RBUkraine.BLL.Contracts
{
    public interface IDonationService
    {
        Task Create(DonationModel model);

        IEnumerable<RatingItemModel> GetRating();
    }
}
