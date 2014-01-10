using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ReflectionExamples
{
    class GetAssemblyMethods
    {
        Assembly assembly;
        Assembly assembly1;
        StreamWriter fs;
        string className;
        public GetAssemblyMethods()
        {

        }
        public GetAssemblyMethods(string assemblyPath, string fileLocation, string name)
        {
            //this.assembly1 = Assembly.LoadFrom(@"C:\Share\ZFP\bin\Debug\GEHealthcare.ZFP.DicomObjectInterface.dll");
            
            this.assembly = Assembly.LoadFrom(assemblyPath);
            fs = new StreamWriter(fileLocation, true);
            className = name;
            
        }
        public void GenerateMethodsMetaData()
        {
            try
            {
                WriteLine(0, "Assembly: {0}", assembly);
                // Find Types in the assembly. This particular block of code gets all the types defined in the assnblies including the ones that are defined in the other assenblies
                //foreach (Type t in assembly.GetExportedTypes())
                //{
                //    WriteLine(1, "Type: {0}", t);
                //    if (t.Name != className) continue;
                //    // Discover the type's members
                //    const BindingFlags bf = BindingFlags.DeclaredOnly |
                //    BindingFlags.NonPublic | BindingFlags.Public |
                //    BindingFlags.Instance | BindingFlags.Static;
                //    foreach (MemberInfo mi in t.GetMembers(bf))
                //    {
                //        String typeName = String.Empty;
                //        if (mi is Type) typeName = "(Nested) Type";
                //        else if (mi is FieldInfo) typeName = "FieldInfo";
                //        else if (mi is MethodInfo) typeName = "MethodInfo";
                //        else if (mi is ConstructorInfo) typeName = "ConstructoInfo";
                //        else if (mi is PropertyInfo) typeName = "PropertyInfo";
                //        else if (mi is EventInfo) typeName = "EventInfo";
                //        WriteLine(2, "{0}: {1}", typeName, mi);
                //    }
                //}


                WriteLine(1, "Type: {0}", className);
                Type t = assembly.GetType(className,true,true);
                // Discover the type's members
                const BindingFlags bf = BindingFlags.DeclaredOnly |
                BindingFlags.NonPublic | BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.Static;
                foreach (MemberInfo mi in t.GetMembers(bf))
                {
                    String typeName = String.Empty;
                    if (mi is Type) typeName = "(Nested) Type";
                    else if (mi is FieldInfo) typeName = "FieldInfo";
                    else if (mi is MethodInfo) typeName = "MethodInfo";
                    else if (mi is ConstructorInfo) typeName = "ConstructoInfo";
                    else if (mi is PropertyInfo) typeName = "PropertyInfo";
                    else if (mi is EventInfo) typeName = "EventInfo";
                    WriteLine(2, "{0}: {1}", typeName, mi);
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                //Close the string writer
                if (fs != null)
                    fs.Close();
            }
        }
        private void WriteLine(Int32 indent, String format, params Object[] args)
        {
            fs.WriteLine(new String(' ', 3 * indent) + format, args);

        }

    }
}

