using System;
using System.Web.Script.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace LibreOOPWeb.Models
{
    public class LibreReadingModel
    {
        public LibreReadingModel()
        {
        }
        [ScriptIgnore]
        [JsonIgnore]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedOn { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ModifiedOn { get; set; }

        public string uuid { get; set; }
        public string b64contents { get; set; }
        public string status { get; set; }
        public string result { get; set; }
        public string oldState { get; set; }
        public string newState { get; set; }
        public string sensorStartTimestamp { get; set; }
        public string sensorScanTimestamp { get; set; }
        public string currentUtcOffset { get; set; }
    }
}
