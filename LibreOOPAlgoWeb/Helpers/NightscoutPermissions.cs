using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LibreOOPWeb.Models;
using LibreOOPWeb.Utils;
using Newtonsoft.Json;

namespace LibreOOPWeb.Helpers
{
    public class NightscoutPermissions
    {
        public static string site = Config.NsHost;
        public static string siteAndPath = $"{site}/api/v1/status.json";

        //public static string site = "https://vg.no";
        //public static string siteAndPath = site;    

        public NightscoutPermissions()
        {
        }

        public async static Task<bool> CheckUploadPermissions(string token){
            try
            {
                
                var response = await GetPermissions(token);
                var ok = response?.Where(x => x.Any(y => y == "libreoop")).Count() > 0;
                return ok || false;
            } catch(Exception){
                return false;
            }
    
        }
        public async static Task<bool> CheckProcessPermissions(string process_token)
        {
            try
            {

                var response = await GetPermissions(process_token);
                var ok = response?.Where(x => x.Any(y => y == "libreoopprocessor")).Count() > 0;
                return ok || false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async static Task<List<List<string>>> GetPermissions(string token){
            
           
            var client = new HttpClient();



            //var response = await client.GetAsync(siteAndPath, content);
            var builder = new UriBuilder(siteAndPath);
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["token"] = token;
            //query["bar"] = "bazinga";
            builder.Query = query.ToString();
            string url = builder.ToString();

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            //return await  

            var content = await response.Content.ReadAsStringAsync();
            try{
                var permissions = JsonConvert.DeserializeObject<NightscoutPermissionsModel>(content);
                return permissions.authorized.permissionGroups;
            } catch(Exception){
                return null;
            }


        }

    }
}
