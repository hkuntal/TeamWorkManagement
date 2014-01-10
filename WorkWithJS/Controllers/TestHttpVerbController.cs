using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkWithJS.Controllers
{
    public class TestHttpVerbController : Controller
    {
        //
        // GET: /TestHttpVerb/

        public string Get(string receivedString)
        {
            return "The string received from the browser is " + receivedString;
        }

    }
}
