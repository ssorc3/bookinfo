using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ratings.Services
{
    public interface IRatingsService
    {
        Task<Dictionary<string, int>> Get();
    }
}