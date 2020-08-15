using System.Threading.Tasks;
using Details.Models;

namespace Gateway
{
    public interface IDetailsService
    {
        Task<BookDetails> GetBookDetails(int productId);
    }
}