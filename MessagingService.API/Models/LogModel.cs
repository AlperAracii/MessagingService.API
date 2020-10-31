using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MessagingService.API.Models
{
    public class LogModel 
    {
        [BsonElement("Id")]
        public ObjectId Id { get; set; }

        [BsonElement("Message")]
        public string Message { get; set; }

        [BsonElement]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
