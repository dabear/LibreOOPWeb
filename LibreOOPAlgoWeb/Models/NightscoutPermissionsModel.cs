using System;
using System.Collections.Generic;

namespace LibreOOPWeb.Models
{

    public class NightscoutAuthorizedModel
    {
        public string token { get; set; }
        public string sub { get; set; }
        public List<List<string>> permissionGroups { get; set; }
        public int iat { get; set; }
        public int exp { get; set; }
    }

    public class NightscoutPermissionsModel
    {
        /*public string status { get; set; }
        public string name { get; set; }
        public string version { get; set; }
        public DateTime serverTime { get; set; }
        public long serverTimeEpoch { get; set; }
        public bool apiEnabled { get; set; }
        public bool careportalEnabled { get; set; }
        public bool boluscalcEnabled { get; set; }
        public string head { get; set; }
        public Settings settings { get; set; }
        public ExtendedSettings extendedSettings { get; set; }*/
        public NightscoutAuthorizedModel authorized { get; set; }
    }
}
