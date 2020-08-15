using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Reviews.Configuration;
using Reviews.Models;

namespace Reviews.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IRatingsService _ratings;
        private readonly IFeatureManager _featureManager;
        private readonly ILogger _logger;

        public ReviewsService(IRatingsService ratings, IFeatureManager featureManager, ILogger<ReviewsService> logger)
        {
            _ratings = ratings;
            _featureManager = featureManager;
            _logger = logger;
        }
        
        public async Task<IEnumerable<Review>> GetReviews(int productId)
        {
            var starsReviewer1 = -1;
            var starsReviewer2 = -1;
            
            if (await _featureManager.IsEnabledAsync(nameof(FeatureFlags.RatingsEnabled)))
            {
                _logger.LogDebug("RatingsEnabled flag is set to true");
                var ratings = await _ratings.GetRatings(productId);

                starsReviewer1 = ratings.GetValueOrDefault("Reviewer1", -1);
                starsReviewer2 = ratings.GetValueOrDefault("Reviewer2", -1);
            }

            return new List<Review>
            {
                new Review
                {
                    Reviewer = "Reviewer1",
                    Text = "An extremely entertaining play by Shakespeare. The slapstick humour is refreshing!",
                    Rating = starsReviewer1
                },
                new Review
                {
                    Reviewer = "Reviewer2",
                    Text = "Absolutely fun and entertaining. The play lacks thematic depth when compared to other plays by Shakespeare.",
                    Rating = starsReviewer2
                }
            };
        }
    }
}