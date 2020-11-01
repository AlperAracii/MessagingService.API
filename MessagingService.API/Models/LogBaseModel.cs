using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MessagingService.API.Models
{
    public abstract class LogBaseModel
    {
        public ObjectId Id { get; set; }

        [BsonElement]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
