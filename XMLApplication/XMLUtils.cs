using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Text;

namespace XMLApplication
{
    internal class XMLUtils
    {
        public static void GetSchemaFromXML()
        {

        }

        //Get the correct XSD to validate the xml
        public static void VerifyXmlFile(string xml)
        {
            xml.Replace("\n", "");
            string xsdFilepath = "StudentEntity.xsd";
            using (FileStream stream = File.OpenRead(xsdFilepath))
            {
                XmlReaderSettings settings = new XmlReaderSettings();

                XmlSchema schema = XmlSchema.Read(stream, OnXsdSyntaxError);
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas.Add(schema);
                settings.ValidationEventHandler += OnXmlSyntaxError;

                System.IO.MemoryStream ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml));
                ms.Position = 0;
                XmlDocument xs = new XmlDocument();
                xs.Load(ms);
                using (XmlReader validator = XmlReader.Create(ms, settings))
                {
                    try
                    {
                        // Validate the entire xml file
                        while (validator.Read())
                        {
                        }
                    }
                    catch (Exception)
                    {
                        

                        throw;
                    }
                    
                }
            }
            
        }

        private static void OnXsdSyntaxError(object sender, ValidationEventArgs e)
        {
             Console.WriteLine("\tValidation error: {0}", "The schema validation failed");
        }

        private static void OnXmlSyntaxError(object sender, ValidationEventArgs e)
        {
            Console.WriteLine("The schema is invalid");
        }
        
    }
}

