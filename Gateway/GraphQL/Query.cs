using System.Collections.Generic;
using System.Threading.Tasks;
using Details.Models;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Reviews.Models;

namespace Gateway.GraphQL
{
    public class Query
    {
       public Product GetProduct(int id) => new Product(id); 
    }

    public class Product
    {
        private readonly int _id;

        public Product(int id)
        {
            _id = id;
        }

        public Task<BookDetails> Details([Service] IDetailsService details) => details.GetBookDetails(_id);
        
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public Task<List<Review>> Reviews([Service] IReviewsService reviews) => reviews.GetReviews(_id);
    }
}