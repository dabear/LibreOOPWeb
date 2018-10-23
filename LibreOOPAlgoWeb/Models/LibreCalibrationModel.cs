using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace LibreOOPWeb.Models
{
    public class LibreCalibrationModel
    {
        public LibreCalibrationModel()
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
        public AlgorithmMetadata metadata{ get; set; }
        public List<String> requestids { get; set; }


    }
}
