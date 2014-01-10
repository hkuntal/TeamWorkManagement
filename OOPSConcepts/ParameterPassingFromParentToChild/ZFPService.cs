using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPSConcepts.ParameterPassingFromParentToChild
{
    class ZFPService
    {
        //Define some properties
        public int Id { get; set; }
        public string Name { get; set; }

        public ZFPPatientDataService MyPatientDataService { get; set; }

        public ZFPService()
        {
            Id = 5;
            Name = "Hariom";
            MyPatientDataService = new ZFPPatientDataService();
            MyPatientDataService.ParentZFPService = this;
        }

        public void PrintData()
        {
            MyPatientDataService.PrintNames();
        }

    }
}
