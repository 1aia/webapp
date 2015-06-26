using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Test()
        {
            var files = Directory.EnumerateFiles(HttpRuntime.AppDomainAppPath).ToList();

            var webConfig = files.FirstOrDefault(x => x.ToLower().Contains("web.config"));

            if (webConfig != null)
            {
                return File(System.IO.File.OpenRead(webConfig), "web.config");
            }

            return Content("");
        }
    }
}
