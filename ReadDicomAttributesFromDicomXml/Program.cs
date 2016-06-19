using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace ReadDicomAttributesFromDicomXml
{
    class Program
    {
        //private static string inputFileName = @"C:\Hariom\dcmxml.xml";
        private static string outputFileName = @"C:\Hariom\dcmoutput.txt";
        private static int counter = 0;
        static void Main(string[] args)
        {
            // Read the xml file
            //XmlDocument doc = new XmlDocument();
            
            //XPathDocument document = new XPathDocument(@"C:\Hariom\dcmxml.xml");
            //document.CreateNavigator();

            
            // Read the files from the directory and loop over the files
            StartDicomTagExtraction();
            Console.ReadKey();
        }

        private static void StartDicomTagExtraction()
        {
            Console.WriteLine("Enter the location of the folder where the Dicom Xml files are located. The output file will be written at the same location.");
            var inputFolder = Console.ReadLine();
            //string inputFolder =@"C:\Hariom\Work\GE\Development\Data\DCMImages\UPMCDataSets\MultiFrame US - Test Chad\MultiFrame US - Test Chad\iSiteExport\DICOM";

            var xmlDoc = new XmlDocument();
            var files = Directory.GetFiles(inputFolder, "*.xml", SearchOption.AllDirectories);
            outputFileName = Path.Combine(inputFolder, "dcmOutPut.csv");
            using (var sw = new StreamWriter(outputFileName, true))
            {
                sw.WriteLine(
                    "#, fileName, Series Uid, instance number, content date, content time, acquisition datetime, acquisition date, acquisition time, # of frames");

                foreach (var inputFileName in files)
                {
                    Console.WriteLine(inputFileName);
                    //xmlDoc.Load(inputFileName);
                    ReadXmlValues(xmlDoc, inputFileName, sw);
                }
            }

            Console.WriteLine("Do you want to extract another folder (y/n) ?");
            var a = Console.ReadLine();
            if (a == "y")
            {
                StartDicomTagExtraction();
            }
            Console.WriteLine("Enter a key to exit...");
        }

        private static void ReadXmlValues(XmlDocument xmlDoc, string fileName, StreamWriter sw)
        {
            counter++;
            xmlDoc.Load(fileName);
            var line = new StringBuilder();
            line.Append(counter);
            line.Append(",");
            line.Append(fileName);
            
            // Get the series instance Uid
            AddNodeLine(xmlDoc, line, "0020,000e");
            
            // Get the instance number
            AddNodeLine(xmlDoc, line, "0020,0013");
            
            // Get the content date
            AddNodeLine(xmlDoc, line, "0008,0023");
            
            // Get the content time
            AddNodeLine(xmlDoc, line, "0008,0033");
            
            // Get the acquisition date time
            AddNodeLine(xmlDoc, line, "0008,002A");
            
            // Get the acquisition date
            AddNodeLine(xmlDoc, line, "0008,0022");
            
            // Get the acquisition time
            AddNodeLine(xmlDoc, line, "0008,0032");
            
            // Get the number of frames
            AddNodeLine(xmlDoc, line,"0028,0008");

            sw.WriteLine(line.ToString());

        }

        private static void AddNodeLine(XmlDocument xmlDoc, StringBuilder line, string tag)
        {
            XmlNode titleNode;
            titleNode = xmlDoc.SelectSingleNode("//data-set/element[@tag='"+tag+"']");
            line.Append(",");
            if (titleNode != null)
            {
                Console.WriteLine(titleNode.InnerText);
                line.Append(titleNode.InnerText);
                //sw.WriteLine(titleNode.InnerText);
            }
        }
    }
}
