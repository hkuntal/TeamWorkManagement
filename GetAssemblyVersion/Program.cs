using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GetAssemblyVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {        
            if (args != null && args.Length > 0)
            {
                var assemblyPath = args[0];
                var assembly = Assembly.LoadFrom(assemblyPath);
                Console.WriteLine("Assembly Path: " + assemblyPath);
                Console.WriteLine("Assembly Version: " + assembly.GetName().Version.ToString());
                Console.WriteLine("Public Key Token: " + assembly.GetName().GetPublicKeyToken());
            }
            else
            {
                Console.WriteLine("Assembly path not specified");
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
