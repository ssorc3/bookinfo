﻿namespace Ratings.Repositories.Mongo
{
    public class MongoDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}