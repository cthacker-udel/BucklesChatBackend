using MongoDB.Driver;

namespace BucklesChatBackend.Database.Helpers
{
    public static class MongoDBHelpers
    {
        public static IMongoDatabase GetMongoDatabase(string databaseName, IMongoClient client)
        {
            return client.GetDatabase(databaseName);
        }

        public static IMongoCollection<T> GetMongoCollection<T>(string collectionName, IMongoDatabase database)
        {
            return database.GetCollection<T>(collectionName);
        }
    }
}
