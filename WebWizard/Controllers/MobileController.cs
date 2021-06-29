using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Helper;
using Wizard.Model.WebWizard;

namespace WebWizard.Controllers
{
    public class MobileController : Controller
    {
        ServiceAccess Access = new ServiceAccess();
        // GET: Mobile
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ClientLogIn()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ClientDashboard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult WizerdLogIn()
        {
            var id = Session["WebWizardId"];
            if (id==null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("WizerdDashboard", "Mobile");
            }


            
        }
        [HttpPost]
        public ActionResult WizerdLogIn(LogInModel logIn)
        {
            var id = Session["WebWizardId"];
            if (id != null) {
                return RedirectToAction("WizerdLogIn", "Mobile");
            }
            else {

            if (logIn != null)
            {
                if (!string.IsNullOrEmpty(logIn.Email) && !string.IsNullOrEmpty(logIn.Password))
                {
                    var webWizardlogInInfo = Access.WebWizardLogInService.GetLogIn(logIn);

                    if (webWizardlogInInfo != null)
                    {
                        if (webWizardlogInInfo.Status == true)
                        {
                            Session["WebWizardId"] = webWizardlogInInfo.WebWizardId;
                            Session["FirstName"] = webWizardlogInInfo.FirstName;
                            Session["LastName"] = webWizardlogInInfo.LastName;
                            Session["FullName"] = webWizardlogInInfo.FirstName + " " + webWizardlogInInfo.LastName;
                            Session["Email"] = webWizardlogInInfo.Email;
                            Session["NameTitle"] = webWizardlogInInfo.NameTitle.ToLower();
                            Session["Status"] = webWizardlogInInfo.Status;

                            int webWizardId = (int)Session["WebWizardId"];
                            var webWizardDetail = Access.WebWizardProfileService.GetWebWizardDetailsByWebWizardId(webWizardId);
                            if (webWizardDetail != null)
                            {
                                if (webWizardDetail.WebWizardProfileImageUrl == null)
                                {
                                    Session["WebWizardProfileImageUrl"] = null;
                                    return Json(webWizardlogInInfo, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    Session["WebWizardProfileImageUrl"] = "/Assets/WebWizardDashboard/ProfileImage/" + webWizardDetail.WebWizardProfileImageUrl;
                                    return Json(webWizardlogInInfo, JsonRequestBehavior.AllowGet);
                                }


                            }
                            else
                            {
                                Session["WebWizardProfileImageUrl"] = "/Assets/images/user-login.png";
                                return Json(webWizardlogInInfo, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(webWizardlogInInfo, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }

            }
            return View();
        }
        [HttpGet]

       
        public ActionResult WizerdLogOut()
        {
            Session["WebWizardId"] = null;
            Session["FirstName"] = "";
            Session["LastName"] = "";
            Session["FullName"] = "";
            Session["Email"] = "";
            Session["NameTitle"] = "";
            Session["Status"] = "";
            Session["WebWizardProfileImageUrl"] = null;

            return Json("",JsonRequestBehavior.AllowGet);
        }
        public ActionResult WizerdDashboard()
        {
            try
            {
                if (Convert.ToBoolean(Session["Status"]) == true && Convert.ToInt32(Session["WebWizardId"]) != 0)
                {
                    if (Session["FirstName"].ToString() != "" && Session["LastName"].ToString() != "" && Session["Email"].ToString() != "" && Session["NameTitle"].ToString() != "")
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Mobile", "WizerdLogIn");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "LogIn");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "LogIn");
            }
        }

        

    }
}