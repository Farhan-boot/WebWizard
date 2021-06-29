using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Helper;
using Wizard.Model.WebWizard;

namespace WebWizard.Controllers
{
    public class ClientLogInController : Controller
    {
        ServiceAccess Access = new ServiceAccess();
        // GET: ClientLogIn
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
                    var clientlogInInfo = Access.ClientLogInService.GetLogIn(logIn);

                    if (clientlogInInfo != null)
                    {
                        if (clientlogInInfo.Status == true)
                        {
                            Session["ClientId"] = clientlogInInfo.ClientId;
                            Session["ClientFirstName"] = clientlogInInfo.FirstName;
                            Session["ClientLastName"] = clientlogInInfo.LastName;
                            Session["ClientFullName"] = clientlogInInfo.FirstName + " " + clientlogInInfo.LastName;
                            Session["ClientEmail"] = clientlogInInfo.Email;
                            Session["ClientNameTitle"] = clientlogInInfo.NameTitle.ToLower();
                            Session["ClientStatus"] = clientlogInInfo.Status;

                            int ClientId = (int)Session["ClientId"];
                            var clientDetail = Access.ClientProfileService.GetClientDetailsByClientId(ClientId);
                            if (clientDetail != null)
                            {
                                if (clientDetail.ClientProfileImageUrl == null)
                                {
                                    Session["ClientProfileImageUrl"] = "/ClientAssets/ClientDashboard/ProfileImage/" + "user-demo.png";
                                    return Json(clientlogInInfo, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    Session["ClientProfileImageUrl"] = "/ClientAssets/ClientDashboard/ProfileImage/" + clientDetail.ClientProfileImageUrl.ToString();
                                    return Json(clientlogInInfo, JsonRequestBehavior.AllowGet);
                                }
                                

                            }
                            else
                            {
                                Session["clientProfileImageUrl"] = "/Assets/images/user-login.png";
                                return Json(clientlogInInfo, JsonRequestBehavior.AllowGet);
                            }
                            //return Json(clientlogInInfo, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            clientlogInInfo.Status = false;
                            return Json(clientlogInInfo, JsonRequestBehavior.AllowGet);
                        }
                    }
                    }
            }
            return View();
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            Session["ClientId"] = "";
            Session["ClientFirstName"] = "";
            Session["ClientLastName"] = "";
            Session["ClientFullName"] = "";
            Session["ClientEmail"] = "";
            Session["ClientNameTitle"] = "";
            Session["ClientStatus"] = "";
            Session["clientProfileImageUrl"] = null;

            return Json("",JsonRequestBehavior.AllowGet);
        }
    }
}