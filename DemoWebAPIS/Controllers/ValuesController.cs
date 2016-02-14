using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace DemoWebAPIS.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
          

            // put some data in the session
            HttpContext.Current.Session["Hariom"] = "Kuntal";
            return new string[] { "value1", "value2" };

            
        }

        // GET api/values/5
        public string Get(int id)
        {
            //Simulate deply by some sleep
            System.Threading.Thread.Sleep(200);
            // return the data from the session and return to user
            //return Convert.ToString(HttpContext.Current.Session["Hariom"]);
            return "Hariom" + id.ToString();
        }

        
        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }


    }
}