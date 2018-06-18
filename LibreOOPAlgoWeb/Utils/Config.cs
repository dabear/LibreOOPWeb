using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace LibreOOPWeb.Utils
{
    public class Config
    {
#if DEBUG
        private static string homedir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        //we want this to throw exception if file not found, so don't fall back to empty string here!
        public static string NsHost => File.ReadAllText(Path.Combine(homedir, "nshost.txt")).TrimEnd();

        public static string DebugViewLogSecret => "IWannaSeeMyLogs";
        public static string MongoUrl = File.ReadAllText(Path.Combine(homedir, "mongourl.txt")).TrimEnd();

#else
        public static string DebugViewLogSecret => Environment.GetEnvironmentVariable("Log_Secret") ?? "";
        public static string NsHost => (Environment.GetEnvironmentVariable("NS_Host") ?? "").TrimEnd(new[] { '/' });

        public static string MongoUrl => Environment.GetEnvironmentVariable("Mongo_Url");
#endif
        public static int MaxFetchPerAttempt = 20;  
    }

}
