using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ratings.Repositories;

namespace Ratings.Services
{
    public class RatingsService : IRatingsService
    {
        private readonly IRatingsRepository _ratingsRepository;

        public RatingsService(IRatingsRepository ratingsRepository)
        {
            _ratingsRepository = ratingsRepository;
        } 
        
        public async Task<Dictionary<string, int>> Get()
        {
            var ratings = await _ratingsRepository.Get();

            return ratings
                .Select((x, i) => (x, i + 1))
                .ToDictionary(t => $"Reviewer{t.Item2}", x => x.x.Rating);
        }
    }
}