using MessagingService.API.Data.Entities;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingService.API.Data.Repositories
{
    public class MessageRepository : MongoRepository<Messages>
    {
        public MessageRepository(string mongoDBConnectionString, string dbName, string collectionName) : base(mongoDBConnectionString, dbName, collectionName)
        {
        }

        public virtual async Task<List<Messages>> GetMyMessagesAsync(ObjectId id)
        {
            return await _mongoCollection.Find(aa => aa.SendToId == id || aa.FromId == id).ToListAsync();
        }
    }
}
