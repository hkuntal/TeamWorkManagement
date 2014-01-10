using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPISampleProject.Controllers
{
    public class Person
    {
        public int Id { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class PersonController : ApiController
    {
        
        // GET api/values
        public IEnumerable<Person> Get()
        {
            throw new Exception("Hariom is throwing a message");
            return new Person[]
                {
                    new Person{Id = 1, First = "Hariom", Last = "Kuntal"},
                    new Person{Id=2, First = "Ruby", Last="Kuntal"},
                    new Person{Id=3, First = "Gayatri", Last = "Kuntal"}
                };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        //public void Post([FromBody]List<Person> value)
        //{
        //}

        // POST api/values
        public void Post([FromBody]string value)
        {
        }


        // PUT api/values/5
        public void Put(int id, [FromBody]Person value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}