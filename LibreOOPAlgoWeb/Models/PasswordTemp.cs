using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibreOOPWeb.Models
{
    public class PasswordTemp
    {
        public PasswordTemp()
        {
        }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedOn { get; set; }
        public string HashedPassword
        {
            get; set;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}

