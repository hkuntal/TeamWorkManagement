using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPSConcepts.ParameterPassingFromParentToChild;

namespace OOPSConcepts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ZFPService obj = new ZFPService();
            obj.MyPatientDataService.PrintNames();
            //Test Virtual Override
            //VOParent obj = new VOGrandChild();
            //Console.WriteLine(obj.Display());
            //Console.ReadKey();
            Console.WriteLine("Calling the main program from another exe");
            Console.ReadLine();
            //Test the NVI Pattern
            OOPSConcepts.NormalPattern.EntryPoint.Main1();
            OOPSConcepts.NVIPattern.EntryPoint.Main1();
            Console.ReadKey();

            //Test the Interface method
            IParent p = new InterfaceDerived();
            InterfaceDerived p1 = new InterfaceDerived();
            
        }
        public static string DoSomething()
        {
            return "Dosming something";
        }

        
    }
}
