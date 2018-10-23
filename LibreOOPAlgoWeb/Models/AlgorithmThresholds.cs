using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace LibreOOPWeb.Models
{
    public struct DefaultAlgorithmThresholds
    {
        public static readonly ushort GLUCOSE_LOWER_BOUND = 1000;
        public static readonly ushort GLUCOSE_UPPER_BOUND = 3000;
        public static readonly ushort RAW_TEMP1 = 6000;
        public static readonly ushort RAW_TEMP2 = 9000;
    }

    public class AlgorithmMetadata
    {
        public AlgorithmMetadata()
        {
        }


        public ushort GLUCOSE_LOWER_BOUND { get; set; }
        public ushort GLUCOSE_UPPER_BOUND { get; set; }
        public ushort RAW_TEMP1 { get; set; }
        public ushort RAW_TEMP2 { get; set; }


    }


}