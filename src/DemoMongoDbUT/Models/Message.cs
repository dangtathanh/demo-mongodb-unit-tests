using MongoDB.Bson;
using System;

namespace DemoMongoDbUT.Models
{
    public class Message : BaseModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Body { get; set; }
    }
}
