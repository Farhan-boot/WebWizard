using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Helper;
using Wizard.Model.WebWizard;

namespace WebWizard.Controllers
{
    public class AdminController : Controller
    {
        ServiceAccess Access = new ServiceAccess();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(LogInModel logIn)
        {
            var adminLog = Access.AdminService.LogIn(logIn);
            if (adminLog!=null)
            {
                Session["AdminId"] = adminLog.Id;
                Session["AdminFirstName"] = adminLog.FirstName;
                Session["AdminLastName"] = adminLog.LastName;
                Session["AdminFullName"] = adminLog.FirstName+" "+adminLog.LastName;
                Session["AdminEmail"] = adminLog.Email;
                Session["AdminImagePath"] = adminLog.ImageUrl;
                return Json(adminLog, JsonRequestBehavior.AllowGet);
            }
            else
            {
                adminLog.Status = false;
                return Json(adminLog, JsonRequestBehavior.AllowGet);
            }
           
            
        }
        [HttpPost]
        public ActionResult LogOut()
        {
            Session["AdminId"] = "";
            Session["AdminFirstName"] = "";
            Session["AdminLastName"] = "";
            Session["AdminFullName"] = "";
            Session["AdminEmail"] = "";
            Session["AdminImagePath"] = "";

            return Json("",JsonRequestBehavior.AllowGet);
        }
        public ActionResult Dashboard()
        {
            if (Convert.ToString(Session["AdminEmail"])=="")
            {
                return RedirectToAction("LogIn", "Admin");
            }
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult SaveProfilePicture(HttpPostedFileBase adminProfile)
        {
            int adminId =Convert.ToInt32(Session["AdminId"].ToString());

            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;

            var file = Request.Files[0] as HttpPostedFileBase;
            actualFileName = file.FileName;
            fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            int size = file.ContentLength;
            file.SaveAs(Path.Combine(Server.MapPath("~/AdminAssets/AdminDashboard/ProfileImage"), fileName));

            var adminLog = Access.AdminService.SaveProfilePicture(adminId,fileName);
            Session["AdminImagePath"] = adminLog.ImageUrl;
            return Json(adminLog, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetWizardPortfolioListbyStatus()
        {
            var wizardPortfolioList = Access.AdminService.GetWizardPortfolioListbyStatus();

            return Json(wizardPortfolioList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AcceptedRequest(Wizard.Data.Data.Entities.WebWizardPortfolio portfolio)
        {
            var request = Access.AdminService.AcceptedRequest(portfolio.Id);
            return Json(request, JsonRequestBehavior.AllowGet);
        }
    }
}