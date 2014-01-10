using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using WebAPISampleProject.Models;

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
            byte[] fileData = System.IO.File.ReadAllBytes(@"C:\Users\502230035\Documents\Visual Studio 2012\Projects\TeamWorkManagement\WebAPISampleProject\DataFiles\16bitUnsignedImage.txt");
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

        public ActionResult WebGL()
        {
            return View();
        }
    }
}
