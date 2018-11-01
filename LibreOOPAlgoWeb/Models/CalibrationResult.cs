using System;
using System.Web.Script.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace LibreOOPWeb.Models
{
    public class CalibrationResult
    {
        public CalibrationResult()
        {
        }

        public string status { get; set; }
        public double? slope_slope { get; set; }
        public double? slope_offset { get; set; }
        public double? offset_offset { get; set; }
        public double? offset_slope { get; set; }
        public string uuid { get; set; }
        public double? isValidForFooterWithReverseCRCs { get; set; }

}
}
