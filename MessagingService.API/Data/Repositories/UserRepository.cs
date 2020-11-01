using MessagingService.API.Data.Entities;
using MessagingService.API.Models.Request;
using MessagingService.API.Utilities.Extensions;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MessagingService.API.Data.Repositories
{
    public class UserRepository : MongoRepository<User>
    {
        public UserRepository(string mongoDBConnectionString, string dbName, string collectionName) : base(mongoDBConnectionString, dbName, collectionName)
        {
        }
        public async Task<User> LoginAsync(RequestLoginModel request)
        {
            return await _mongoCollection.Find(aa => aa.UserName == request.Username && aa.Password == request.Password.MD5Hash() && aa.IsDeleted == false).FirstOrDefaultAsync();
        }
        public async Task<User> GetByUserNameAsync(string username)
        {
            return await _mongoCollection.Find(aa => aa.UserName == username).FirstOrDefaultAsync();
        }
    }
}
