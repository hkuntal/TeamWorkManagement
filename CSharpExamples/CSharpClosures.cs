using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace WebApiServicesClient
{
    //This is to test if the closure concept applies the C# the way it applies in Javascript
    class School
    {
        public string SchoolName { get; set; }
        public string GetName(int id)
        {
            //Based on the Id return the name of teh student
            string name = "";
            switch (id)
            {
                case 1:
                    name = "hariom";
                    break;
                case 2:
                    name = "singh";
                    break;
                case 3:
                    name = "kuntal";
                    break;
                default:
                    name = "No name";
                    break;

            }
            return name + " is in School" + SchoolName.ToUpper();
        }
        
    }

    class Student
    {
        public void PrintStudentName(Func<int, string> fn)
        {
            //call the delegate
            Console.WriteLine(fn(3));
        } 
    }

    class CSharpClosures
    {
        public static void Test()
        {
            //Create objects of School and Student
            var objSchool = new School();
            var objStudent = new Student();

            objSchool.SchoolName = "Jay Bajrang";
            objStudent.PrintStudentName(objSchool.GetName);
        }
    }
}
