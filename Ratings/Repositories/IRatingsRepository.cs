using System.Collections.Generic;
using System.Threading.Tasks;
using Ratings.Models;

namespace Ratings.Repositories
{
    public interface IRatingsRepository
    {
        Task<List<RatingModel>> Get();
    }
}