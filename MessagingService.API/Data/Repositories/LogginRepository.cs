using MessagingService.API.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MessagingService.API.Data.Repositories
{
    public class LogginRepository
    {
        public readonly IMongoCollection<LogModel> _mongoCollection;

        public LogginRepository(string mongoDBConnectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(mongoDBConnectionString);
            var database = client.GetDatabase(dbName);
            _mongoCollection = database.GetCollection<LogModel>(collectionName);
        }

        public async Task<LogModel> InsertAsync(LogModel document)
        {
            await _mongoCollection.InsertOneAsync(document);
            return document;
        }
    }
}
