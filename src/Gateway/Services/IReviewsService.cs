using System.Collections.Generic;
using System.Threading.Tasks;
using Reviews.Models;

namespace Gateway
{
    public interface IReviewsService
    {
        Task<List<Review>> GetReviews(int productId);
    }
}