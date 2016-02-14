using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoWebAPIS.Areas.Dicom.Controllers
{
    public class StudyController : ApiController
    {
        [HttpGet]
        public string PassTheMessage(int id)
        {
            //Simulate deply by some sleep
            System.Threading.Thread.Sleep(200);
            // return the data from the session and return to user
            //return Convert.ToString(HttpContext.Current.Session["Hariom"]);
            return "Hariom Kuntal" + id.ToString();
        }
    }
}
