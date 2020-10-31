using MessagingService.API.Data.Entities;
using MessagingService.API.Models.Request;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MessagingService.API.Data.Repositories
{
    public class UserRepository : MongoRepository<User>
    {
        public UserRepository(string mongoDBConnectionString, string dbName, string collectionName) : base(mongoDBConnectionString, dbName, collectionName)
        {
        }
        public async Task<User> GetByLoginInfo(string username)
        {
            return await _mongoCollection.Find(aa => aa.UserName == username && aa.IsDeleted == false).FirstOrDefaultAsync();
        }
        public async Task<User> GetByUserNameAsync(string username)
        {
            return await _mongoCollection.Find(aa => aa.UserName == username).FirstOrDefaultAsync();
        }
    }
}
