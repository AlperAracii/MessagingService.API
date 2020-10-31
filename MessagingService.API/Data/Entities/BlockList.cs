using MessagingService.API.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessagingService.API.Data.Entities
{
    public class BlockList : MongoBaseModel
    {
        [BsonElement("UserId")]
        public ObjectId UserId { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("BlockedUserId")]
        public ObjectId BlockedUserId { get; set; }

        [BsonElement("BlockedUserName")]
        public string BlockedUserName { get; set; }
    }
}
