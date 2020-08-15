using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reviews.Services
{
    public interface IRatingsService
    {
        Task<Dictionary<string, int>> GetRatings(int productId);
    }
}