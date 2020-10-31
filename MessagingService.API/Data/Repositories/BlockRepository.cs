using MessagingService.API.Data.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MessagingService.API.Data.Repositories
{
    public class BlockRepository : MongoRepository<BlockList>
    {
        public BlockRepository(string mongoDBConnectionString, string dbName, string collectionName) : base(mongoDBConnectionString, dbName, collectionName)
        {
        }

        public async Task<BlockList> IsBlocked(BlockList model)
        {
            return await _mongoCollection.Find(aa => aa.UserId == model.UserId && aa.BlockedUserId == model.BlockedUserId && aa.IsDeleted == false).FirstOrDefaultAsync();
        }
    }
}
