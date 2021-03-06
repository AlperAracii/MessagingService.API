﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MessagingService.API.Models
{
    public class LogModel  : LogBaseModel
    {
        [BsonElement("Id")]
        public string UserId { get; set; }

        [BsonElement("Message")]
        public string Message { get; set; }
    }
}
