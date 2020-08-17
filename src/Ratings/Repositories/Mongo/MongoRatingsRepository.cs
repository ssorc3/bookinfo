using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Ratings.Models;

namespace Ratings.Repositories.Mongo
{
    public class MongoRatingsRepository : IRatingsRepository
    {
        private readonly IMongoCollection<RatingModel> _ratings;

        public MongoRatingsRepository(IOptions<MongoDatabaseSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _ratings = database.GetCollection<RatingModel>(settings.CollectionName);
        }

        public Task<List<RatingModel>> Get() => _ratings.Find(_ => true).ToListAsync();
    }
}