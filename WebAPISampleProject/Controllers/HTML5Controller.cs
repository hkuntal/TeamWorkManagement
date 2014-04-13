using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using WebAPISampleProject.Models;
using Newtonsoft.Json;
using XMLUtilities;

namespace WebAPISampleProject.Controllers
{
    public class HTML5Controller : Controller
    {
        //
        // GET: /HTML5/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Canvas()
        {
            return View();
        }

        public ActionResult DisplayImageOnCanvas()
        {
            //Get the file info into byte array
            byte[] fileData = System.IO.File.ReadAllBytes(@"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\WebAPISampleProject\DataFiles\16bitUnsignedImage.txt");
            HTML5Model obj = new HTML5Model();
            obj.Base64EncodedImage += "data:image/png;base64, " + Convert.ToBase64String(fileData);

            //obj.Base64EncodedImage =
            //    "data:image/png;base64, iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAABGdBTUEAALGP C/xhBQAAAAlwSFlzAAALEwAACxMBAJqcGAAAAAd0SU1FB9YGARc5KB0XV+IA AAAddEVYdENvbW1lbnQAQ3JlYXRlZCB3aXRoIFRoZSBHSU1Q72QlbgAAAF1J REFUGNO9zL0NglAAxPEfdLTs4BZM4DIO4C7OwQg2JoQ9LE1exdlYvBBeZ7jq ch9//q1uH4TLzw4d6+ErXMMcXuHWxId3KOETnnXXV6MJpcq2MLaI97CER3N0 vr4MkhoXe0rZigAAAABJRU5ErkJggg==";
            //ViewBag.image =
            //    @"data:image/png;base64, iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAABGdBTUEAALGP C/xhBQAAAAlwSFlzAAALEwAACxMBAJqcGAAAAAd0SU1FB9YGARc5KB0XV+IA AAAddEVYdENvbW1lbnQAQ3JlYXRlZCB3aXRoIFRoZSBHSU1Q72QlbgAAAF1J REFUGNO9zL0NglAAxPEfdLTs4BZM4DIO4C7OwQg2JoQ9LE1exdlYvBBeZ7jq ch9//q1uH4TLzw4d6+ErXMMcXuHWxId3KOETnnXXV6MJpcq2MLaI97CER3N0 vr4MkhoXe0rZigAAAABJRU5ErkJggg==";
            return View(obj);
        }

        public FileContentResult GetImg()
        {
            byte[] byteArray = System.IO.File.ReadAllBytes(@"C:\Users\502230035\Documents\Visual Studio 2012\Projects\TeamWorkManagement\WebAPISampleProject\DataFiles\16bitUnsignedImage.txt"); ;
            
                return new FileContentResult(byteArray, "image/jpeg");
            
        }

        public FileContentResult GetLosslessImage(string patientName)
        {
            try
            {

                var obj =
                    new ImageHeaderXmlReader(
                        @"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\WebAPISampleProject\DataFiles\ImageHeader.xml");
                byte[] byteArray = obj.GetImageFileContent(patientName);


                //byte[] byteArray = System.IO.File.ReadAllBytes(@"C:\Users\502230035\Documents\Visual Studio 2012\Projects\TeamWorkManagement\WebAPISampleProject\DataFiles\imageData.txt"); ;
                //byte[] byteArray = System.IO.File.ReadAllBytes(@"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\WebAPISampleProject\DataFiles\Valencia_Lossless.txt");

                //return new FileContentResult(byteArray, "application/pdf");
                return new FileContentResult(byteArray, "application/octet-stream");
            }          
            catch (Exception ex)
            {
                LoggingFramework.Logger.LogException("An exception has occured." + ex.Message);
            }

            return null;
        }

        public FileContentResult GetLossyImageForWebGL()
        {
            byte[] byteArray = System.IO.File.ReadAllBytes(@"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\WebAPISampleProject\DataFiles\WebGLData_Lossy.txt"); ;

            //return new FileContentResult(byteArray, "application/pdf");
            return new FileContentResult(byteArray, "application/octet-stream");

        }
        public string GetImageHeaderInfoForWebGL()
        {
            //byte[] byteArray = System.IO.File.ReadAllBytes(@"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\WebAPISampleProject\DataFiles\WebGLData_Lossy.txt"); ;

            //return new FileContentResult(byteArray, "application/pdf");
            //return new FileContentResult(byteArray, "application/octet-stream");
            string xmldata = System.IO.File.ReadAllText(
                @"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\WebAPISampleProject\DataFiles\WebGLImageHeaderInfo.txt");
            //var dic = XDocument
            //.Parse(xmldata)
            //.Descendants("Column")
            //.ToDictionary(
            //    c => c.Attribute("Name").Value,
            //    c => c.Value
            //);
            //var json = new JavaScriptSerializer().Serialize(dic);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmldata);
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            return jsonText;

        }
        public FileContentResult GetLosslessImageForWebGL()
        {
            byte[] byteArray = System.IO.File.ReadAllBytes(@"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\WebAPISampleProject\DataFiles\WebGLDataLossless.txt"); ;

            //return new FileContentResult(byteArray, "application/pdf");
            return new FileContentResult(byteArray, "application/octet-stream");

        }
        public ActionResult WebGL()
        {
            return View();
        }
        public string GetPaitentImageHederInfo(string patientName)
        {
            var obj = new ImageHeaderXmlReader(@"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\WebAPISampleProject\DataFiles\ImageHeader.xml");
            return obj.GetImageHeader(patientName);
            
        }
    }
}
