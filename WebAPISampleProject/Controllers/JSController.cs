using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPISampleProject.Controllers
{
    public class JSController : Controller
    {
        //
        // GET: /JS/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Inheritance()
        {
            return View();
        }
        public ActionResult FNO4()
        {
            return View();
        }
        public ActionResult Date()
        {
            return View();
        }
        public ActionResult Augment()
        {
            return View();
        }
        //public ActionResult TestGlobal()
        //{
            
        //}
        public ActionResult JavascriptScopeInNestedFunctions()
        {
            return View();
        }
        public ActionResult JavascriptScope()
        {
            return View();
        }
        public ActionResult JavascriptScopeCallBacks()
        {
            return View();
        }
        public string GetFodderRequired(string time)
        {
            string fodder = "";
            switch (time)
            {
                case "morning":
                    fodder = "100";
                    break;
                case "afternoon":
                    fodder = "200";
                    break;
                case "evening":
                    fodder = "300";
                    break;
                default:
                    fodder = "none";
                    break;
                  
            }
            return fodder;
        }
    }
}
