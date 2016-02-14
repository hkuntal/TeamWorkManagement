using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

namespace DemoWebAPIS.Areas.Dicom
{
    public class StudyRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.Routes.MaphttpRoute()

            //context.MapRoute(
            //    "Dicom",
            //    AreaName + "/{controller}/{action}/{id}",
            //    new { action = "PassTheMessage", id = UrlParameter.Optional });

            context.Routes.MapHttpRoute(
                "Dicom",
                AreaName + "/{controller}/{action}/{id}",
                new { action = "PassTheMessage", id = UrlParameter.Optional });

        }

        public override string AreaName
        {
            get
            {
                return "Dicomapi";
            }
        }
    }
}