using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ratings.Models
{
    public class RatingModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("rating")] public int Rating { get; set; }
    }
}