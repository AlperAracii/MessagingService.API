using MessagingService.API.Models;
using MongoDB.Driver;

namespace MessagingService.API.Data.Repositories
{
    public class LoggingRepository
    {
        public readonly IMongoCollection<LogModel> _mongoCollection;

        public LoggingRepository(string mongoDBConnectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(mongoDBConnectionString);
            var database = client.GetDatabase(dbName);
            _mongoCollection = database.GetCollection<LogModel>(collectionName);
        }

        public void InsertAsync(LogModel document)
        {
            _mongoCollection.InsertOneAsync(document);
        }
    }
}
