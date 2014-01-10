using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkWithJS.Controllers
{
    public class TestJSController : Controller
    {
        //
        // GET: /TestJS/

        public ActionResult Index()
        {
            return View();
        }
        public void Hariom()
        {
            Response.Write("This is Hariom testing my stuff");
        }

        public string Get(string receivedString)
        {
            return "The string received from the browser is " + receivedString;
        }
        public ActionResult TestJSAnonymousFunction()
        {
            return View();
        }
        public ActionResult TestConstructorCall()
        {
            return View("TestConstructorCall");
        }

    }
}
