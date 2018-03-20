using System;
using System.Web.Script.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace LibreOOPWeb.Models
{
    public class UptimeModel
    {
        public UptimeModel()
        {
        }

       

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? LastPing { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Now { get; set; }
        public string LastPingSecondsAgo { get; set; }


    }
}
