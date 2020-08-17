using System.Threading.Tasks;
using Details.Models;

namespace Gateway
{
    public class MemoryDetailsService : IDetailsService
    {
        public async Task<BookDetails> GetBookDetails(int productId)
        {
            return new BookDetails
            {
                Author = "Ben Cross"
            };
        }
    }
}