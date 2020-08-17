using System.Collections.Generic;
using System.Threading.Tasks;
using Reviews.Models;

namespace Reviews.Services
{
    public interface IReviewsService
    {
        Task<IEnumerable<Review>> GetReviews(int productId);
    }
}