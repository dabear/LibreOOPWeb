using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using LibreOOPWeb.Utils;
using System.IO;
using System.Globalization;
using System.Web.Hosting;

namespace LibreOOPWeb.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            //database indexes, run it via mongo shell: 
            //db.librereadings.createIndex({ uuid: 1});
            //db.librereadings.createIndex({ status: 1});
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";
            Logger.LogInfo("Accesing Home");
            return View();
        }

        public ActionResult ViewLogs()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";
            Logger.LogInfo("Accesing Logs");
            return View("LogsIndex");
        }

        [HttpPost]
        public ActionResult VerifyViewLog(string logsecret)
        {
            if(logsecret != null && logsecret.Length > 11 && Config.DebugViewLogSecret == logsecret) {
                var today = DateTime.Now.ToString("dddd", CultureInfo.InvariantCulture);
                var logfile = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Logs",$"libreoop.log");

                try
                {
                    ViewBag.logdata = System.IO.File.ReadAllText(logfile);
                    return View("LogsIndex");
                } catch (Exception ex) {
                    return Content($"Could not read log file due to exception: {ex}");
                }
            }
            return View("Error");
        }


    }
}