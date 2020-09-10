using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DemoMongoDbUT.Models
{
    public class BaseModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
