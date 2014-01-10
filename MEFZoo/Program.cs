using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MEFZoo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Zoo z = new Zoo();
            Program.LoadAnimals(z);
            foreach (var s in z.Animals)
            {
                Console.WriteLine(s.Name);
                
            }
        }

        private static void LoadAnimals(Zoo zoo)
        {
            try
            {
                //A catalog that can aggregate other catalogs 
                var aggrCatalog = new AggregateCatalog();
                //A directory catalog, to load parts from dlls in the Extensions folder 
                var dirCatalog =
                    new DirectoryCatalog(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Extensions", "*.dll");
                //An assembly catalog to load information about part from this assembly 
                var asmCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
                aggrCatalog.Catalogs.Add(dirCatalog);
                aggrCatalog.Catalogs.Add(asmCatalog);
                //Create a container 
                var container = new CompositionContainer(aggrCatalog);
                //Composing the parts. It will compose the parts with in the Zoo instance.
                container.ComposeParts(zoo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    
}

