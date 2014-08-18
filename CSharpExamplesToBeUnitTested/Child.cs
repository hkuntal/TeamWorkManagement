using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExamplesToBeUnitTested
{
    public class Child:Parent
    {
        public Child():base("Hariom")
        {
            Console.WriteLine("Child Constructor called");
        }
        public Child(string name) : base(name)
        {
            Console.WriteLine("Child Constructor called with name as: "+ name);
        }
        public Child(int age):this("Kuntal")
        {
            System.Diagnostics.Debug.WriteLine("Age = "+ age);
        }
        
    }
}
