using System.Collections.Generic;
using System.Threading.Tasks;
using Details.Models;

namespace Details.Services
{
    public class MemoryDetailsService : IDetailsService
    {
        public Task<BookDetails> GetDetails(int id)
        {
            return Task.FromResult(new BookDetails
            {
                Id = id,
                Author = "William Shakespeare",
                Year = "1595",
                Type = "paperback",
                Pages = 200,
                Publisher = "PublisherA",
                Language = "English",
                Isbn10 = "1234567890",
                Isbn13 = "123-1234567890"
            });
        }

        public Task<IEnumerable<BookDetails>> GetAllDetails()
        {
            throw new System.NotImplementedException();
        }
    }
}