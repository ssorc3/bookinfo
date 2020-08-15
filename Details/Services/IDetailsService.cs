using System.Collections.Generic;
using System.Threading.Tasks;
using Details.Models;

namespace Details.Services
{
    public interface IDetailsService
    {
        Task<BookDetails> GetDetails(int id);
        Task<IEnumerable<BookDetails>> GetAllDetails();
    }
}