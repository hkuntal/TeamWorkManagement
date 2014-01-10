using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPSConcepts.ParameterPassingFromParentToChild
{
    class ZFPPatientDataService
    {
        //Write a method that will access the data of the parent class
        public ZFPService ParentZFPService { get; set; }

        public void PrintNames()
        {
            Console.WriteLine("The Id of the employee is: " + ParentZFPService.Id);
            Console.WriteLine("The name of the employee is: " + ParentZFPService.Name);
            ParentZFPService.MyPatientDataService.PrintNames();
            System.Threading.Thread.Sleep(20000);
        }
    }
}
