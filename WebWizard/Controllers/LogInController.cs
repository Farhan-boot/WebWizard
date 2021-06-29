using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Helper;
using Wizard.Model.WebWizard;

namespace WebWizard.Controllers
{
    public class LogInController : Controller
    {
        ServiceAccess Access = new ServiceAccess();
        // GET: LogIn
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LogInModel logIn)
        {
            if (logIn != null)
            {
                if (!string.IsNullOrEmpty(logIn.Email) && !string.IsNullOrEmpty(logIn.Password))
                {
                    logIn.Password = PasswordEncryptOrDecrypt.EncodePassword(logIn.Password);
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
            return View();
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            Session["WebWizardId"] = "";
            Session["FirstName"] = "";
            Session["LastName"] = "";
            Session["FullName"] = "";
            Session["Email"] = "";
            Session["NameTitle"] = "";
            Session["Status"] = "";
            Session["WebWizardProfileImageUrl"] = null;

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult HowItWorks()
        {
            return View();
        }

    }
}