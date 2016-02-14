using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiServicesClient
{
    public class Collections
    {
        public static void TestCollection()
        {
            //Test the Collection
            var obj = new List<Person>
                {
                    new Person{FirstName = "hariom", LastName = "kuntal"},
                    new Person{FirstName = "Ruby", LastName = "kuntal"},
                    new Person{FirstName = "Gajendra", LastName = "Rawat"},
                    new Person{FirstName = "Sania", LastName = "Mirza"},
                };    
            //craete a new person class
            var ojPerson = new Person();
            ojPerson.FirstName = "Ruby";
            ojPerson.LastName = "kuntal";

            Console.WriteLine(obj.Contains(ojPerson)
                                  ? "The object is contained in the list"
                                  : "The object is not contained in the list");

            Console.ReadLine();
        }
    }
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
