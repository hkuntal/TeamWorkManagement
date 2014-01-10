using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPISampleProject.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }
        public string ReturnData(string data)
        {
            return "the data is " + data;
        }

        public string ReceiveAjxCall(string data)
        {
            return "The Ajax call was successfully received and the data I received was: " + data;
        }
    }
}
