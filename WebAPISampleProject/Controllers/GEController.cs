using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPISampleProject.Controllers
{
    public class GEController : Controller
    {
        //
        // GET: /GE/

        public ActionResult Index()
        {
            return View();
        }
        //Simulating Annotation Right click functionality
        public ActionResult Annotation()
        {
            return View();
        }
        public ActionResult LaunchZFP()
        {
            return View();
        }
    }
}
