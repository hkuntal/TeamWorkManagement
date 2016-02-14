using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyInformation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the path of the executing assemblies
            Console.WriteLine(AssemblyDirectory);

            if (args == null || args.Length == 0)
            {
                //Get the required parameters from the Console
            }
            else
            {
                
            }
            Console.ReadLine();
        }
         private static string AssemblyDirectory        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

    }
}
