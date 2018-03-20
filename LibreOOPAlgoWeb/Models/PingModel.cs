using System;
using System.Web.Script.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace LibreOOPWeb.Models
{
    public class PingModel
    {
        public PingModel()
        {
        }
        [ScriptIgnore]
        [JsonIgnore]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ModifiedOn { get; set; }

        public string Desc { get; set; }
        
    }
}
