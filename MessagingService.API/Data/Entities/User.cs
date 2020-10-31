using MessagingService.API.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessagingService.API.Data.Entities
{
    public class User : MongoBaseModel
    {
        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("Passsword")]
        public string Password { get; set; }
    }
}
