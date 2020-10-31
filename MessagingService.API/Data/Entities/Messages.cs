using MessagingService.API.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessagingService.API.Data.Entities
{
    public class Messages : MongoBaseModel
    {
        [BsonElement("FromUserName")]
        public string FromUserName { get; set; }

        [BsonElement("ToUserName")]
        public string ToUserName { get; set; }

        [BsonElement("FromId")]
        public ObjectId FromId { get; set; }

        [BsonElement("SendToId")]
        public ObjectId SendToId { get; set; }

        [BsonElement("Message")]
        public string Message { get; set; }

    }
}
