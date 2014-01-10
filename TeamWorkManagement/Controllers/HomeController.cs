using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamWorkManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string ReturnSampleString()
        {
            return "Hariom is trying to test MVC";
        }
        public string Get(string a)
        {
            return "TestReturnData1" + a;
        }
        public string TestReturnData2(string b)
        {
            return "TestReturnData2" + b;
        }
    }
}
