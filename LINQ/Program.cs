using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            TestLinq();
        }

        public static  void TestLinq()
        {
            //craete a collection
            List<Person> lstPerson = new List<Person>
                {
                    new Person{Id = 1, FirstName = "Hariom" , LastName = "Kuntal"},
                    new Person{Id = 2, FirstName = "Ruby" , LastName = "Malik"},
                    new Person{Id = 3, FirstName = "Gajendra" , LastName = "Singh"}
                };
            var s = lstPerson.FirstOrDefault(p => p.FirstName == "Hariom12");
            Console.WriteLine(s.LastName);
            Console.ReadLine();
        }
    }
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
    }
}
