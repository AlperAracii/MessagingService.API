using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MessagingService.API.Models
{
    public abstract class MongoBaseModel
    {
        public ObjectId Id { get; set; }

        [BsonElement]
        public bool IsDeleted { get; set; } = false;

        [BsonElement]
        public int CreatedBy { get; set; }

        [BsonElement]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [BsonElement]
        public int? UpdatedBy { get; set; }

        [BsonElement]
        public DateTime? UpdatedOn { get; set; }
    }
}
