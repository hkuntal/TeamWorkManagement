using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Give the full path of the Assembly");
                string a = Console.ReadLine();
                //a = @"C:\Share\ZFP\bin\Debug\GEHealthcare.ZFP.DataService.dll";
                a = @"C:\Hariom\Work\GE\Development\GEHealthcare.ZFP.DataService.dll";
                Console.WriteLine("Give the class name whose members you want to find out");
                string b = Console.ReadLine();
                //b = @"GEHealthcare.ZFP.DataService.ZFPService";
                //b = @"GEHealthcare.ZFP.DataService.ZFPPersonName";
                b = @"GEHealthcare.ZFP.DataService.ZFPService";
                Console.WriteLine("Give the complete file location where the data needs to be exported");
                string c = Console.ReadLine();
                c = @"C:\Hariom\Work\GE\Development\ZFPService.txt";

                GetAssemblyMethods obj = new GetAssemblyMethods(a, c, b);
                obj.GenerateMethodsMetaData();

                Console.WriteLine("The data has been exported. Please verify...!! \n");

                Console.WriteLine("Do you wich yo continue (Y/N)?");

                string resposne = Console.ReadLine();

                if (resposne == "Y")
                    Main(new string[0]);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press Any key to exit");
                Console.Read();
            }
            
        }
    }
}
