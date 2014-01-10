using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamWorkManagement.Models;

namespace TeamWorkManagement.Controllers
{
    public class WorkCaptureController : Controller
    {
        //
        // GET: /WorkCapture/
        TeamMgmtDBEntities objTeamMgmtDBEntities = new TeamMgmtDBEntities();
        public ViewResult Index()
        {
            //Create a new collecction of objects to be displayed
            var obj = new Dictionary<int, string>();
            obj[1]="Hariom";
            obj[2]="Kuntal";
            obj[3]="Singh";

            ViewBag.HasPermission = true;
            //ViewData["hasPermission"] = true;
            ViewData["CurrentTime"] = DateTime.Now.ToString();

            //Populate the Model object
            WorkCaptureModel objWorkCaptureModel = new WorkCaptureModel();
            objWorkCaptureModel.SelectNameItem = new SelectListItem();
            objWorkCaptureModel.SelectNameItem.Text = "HariomText";
            objWorkCaptureModel.SelectNameItem.Value = "HariomValue";

            //Create the list to be populated
            Dictionary<string,string> objCountry = new Dictionary<string,string>();
            objCountry.Add("I","India");
            objCountry.Add("N","Nepal");
            objCountry.Add("S","SriLanka");
            //objWorkCaptureModel.SelectNamesList = new SelectList(objCountry.Values);

            TeamMgmtModel objTeamMgmtModel = new TeamMgmtModel();
            objWorkCaptureModel.SelectNamesList = new SelectList(objTeamMgmtModel.RolesSelectList,"EmpName","EmpId");
            //return View(obj);
            return View(objWorkCaptureModel);
        }

        public ActionResult GetStudentDetails()
        {
            //Get the required data
            WorkCaptureModel objWorkCaptureModel = new WorkCaptureModel();
            Student obj = new Student();
            SchoolDBEntities obj1 = new SchoolDBEntities();
            
                //var ls = from std in obj.Students
                //         select std;
            
            objWorkCaptureModel.SelectNamesList = new SelectList(obj1.Students, "StudentName", "StudentID");
            
            //return View(obj);
            return View(objWorkCaptureModel);
        }
        public ActionResult ConfigureTeamMgmt()
        {
            return View();
        }

        public ActionResult Submit(string submit)
        {
            switch (submit)
            {
                    
                case "Add":
                    return RedirectToAction("CreateRole", new { RoleName = Request.Form["RoleName"]});
                case "Delete":
                    return RedirectToAction("DeleteRole", new { IdsToBeDeleted = Request.Form["Roles"] });
                default:
                    throw new Exception("Bad Request");
            }

        }
        public ActionResult SubmitTeamMember(string submit)
        {
           if(submit == "AddTeamMember")
           {
               return RedirectToAction("CreateTeamMember");
           }
           return View("Index");
        }
        public ActionResult CreateRole(LkpRole role)
        {
            TempData["data"] = "Hariom";
            objTeamMgmtDBEntities.LkpRoles.Add(role);
            objTeamMgmtDBEntities.SaveChanges();

            return RedirectToAction("ConfigureTeamMgmt");

            //return Content("Role Successfully Added");
            
        }
        public ActionResult DeleteRole(string IdsToBeDeleted)
        {
            String[] Ids = IdsToBeDeleted.Split(',');
            var col = from a in Ids where (a != "") select a;
            var coll = col.ToList().ConvertAll<int>(x => Convert.ToByte(x));
            //Identify the object that needs to be deleted from the collection
            var rolesToBeDeleted = objTeamMgmtDBEntities.LkpRoles.Where(x => coll.Contains(x.Id));
            //Loop and remove and each item individually
            foreach(var item in rolesToBeDeleted)
            {
                objTeamMgmtDBEntities.LkpRoles.Remove(item);
            }
            
            objTeamMgmtDBEntities.SaveChanges();

            return RedirectToAction("ConfigureTeamMgmt");
        }

        public ActionResult CreateTeamMember(string submit)
        {
            return RedirectToAction("ConfigureTeamMgmt");
        }
        public ViewResult ConfigureNames()
        {
            return View("/Views/PartialViews/Partial_ConfigureTeamMembers.cshtml");
        }
        public PartialViewResult ConfigureRoles()
        {
            TeamMgmtModel objTeamMgmtModel = new TeamMgmtModel();
            objTeamMgmtModel.ObjTeamMgmtDBEntities = objTeamMgmtDBEntities;
            objTeamMgmtModel.RolesSelectList = new SelectList(objTeamMgmtDBEntities.LkpRoles, "Id", "RoleName");

            return PartialView("/Views/PartialViews/Partial_ConfigureRoles.cshtml", objTeamMgmtModel);
        }
    }
}
