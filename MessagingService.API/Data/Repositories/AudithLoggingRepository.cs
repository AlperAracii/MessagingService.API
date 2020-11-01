using MessagingService.API.Models;
using MongoDB.Driver;

namespace MessagingService.API.Data.Repositories
{
    public class AudithLoggingRepository
    {
        public readonly IMongoCollection<AudithLogModel> _mongoCollection;

        public AudithLoggingRepository(string mongoDBConnectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(mongoDBConnectionString);
            var database = client.GetDatabase(dbName);
            _mongoCollection = database.GetCollection<AudithLogModel>(collectionName);
        }

        public void InsertAsync(AudithLogModel document)
        {
             _mongoCollection.InsertOneAsync(document);
        }
    }
}
