using System.Collections.Generic;
using System.Threading.Tasks;
using Reviews.Models;

namespace Gateway
{
    public class MemoryReviewsService : IReviewsService
    {
        public async Task<List<Review>> GetReviews(int productId)
        {
            return new List<Review>
            {
                new Review
                {
                    Rating = 5,
                    Reviewer = "Ben Cross",
                    Text = "This is great"
                }
            };
        }
    }
}