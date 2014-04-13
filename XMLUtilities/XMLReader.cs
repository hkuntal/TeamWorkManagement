using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XMLUtilities
{
    public class ImageHeaderXmlReader
    {
        private string _xmlFilePath;
        public ImageHeaderXmlReader(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
        }
        
        public string GetImageHeader(string patientName)
        {
            XElement root = XElement.Load(_xmlFilePath);
            IEnumerable<XElement> patientImageData =
                from el in root.Elements("ImageHeader")
                where (string) el.Element("PatientName") == patientName 
                select el;
            //foreach (XElement el in patientImageData)
            //{
                
            //}
            // patientImageData.First() returns element of type "Element"
            var da = patientImageData.FirstOrDefault();
            string d = "Image Header is not present";
            if (da != null)
            {
                d = patientImageData.First().Element("Header").Value;
               
            }
            return d;
        }

        public byte[] GetImageFileContent(string patientName)
        {
            byte[] byteArray;
            try
            {
                XElement root = XElement.Load(_xmlFilePath);
                IEnumerable<XElement> patientImageData =
                    from el in root.Elements("ImageHeader")
                    where (string) el.Element("PatientName") == patientName
                    select el;
                //foreach (XElement el in patientImageData)
                //{

                //}
                // patientImageData.First() returns element of type "Element"
                var da = patientImageData.FirstOrDefault();
                string filePath;
                if (da != null)
                {
                    filePath = patientImageData.First().Element("ImagePath").Value;

                }
                else
                {
                    throw new Exception("Image File path is not specified");
                }
                //Read the contents of the file and return it
                byteArray = System.IO.File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return byteArray;
        }
    }
}
