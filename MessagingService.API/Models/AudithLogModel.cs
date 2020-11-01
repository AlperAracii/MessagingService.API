using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MessagingService.API.Models
{
    public class AudithLogModel : LogBaseModel
    {
        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("Message")]
        public string Message { get; set; }

        [BsonElement("ControlerName")]
        public string ControlerName { get; set; }

        [BsonElement("ActionName")]
        public string ActionName { get; set; }
    }
}
