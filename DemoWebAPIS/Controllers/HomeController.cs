using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWebAPIS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public string GetNumber(int id)
        {
            //Simulate deply by some sleep
            System.Threading.Thread.Sleep(200);
            return id.ToString();
        }
    }
}
