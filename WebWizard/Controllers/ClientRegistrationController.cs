using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Helper;
using WebWizard.Models;
using Wizard.Data.Data.Entities;
using Wizard.Model.Client;

namespace WebWizard.Controllers
{
    public class ClientRegistrationController : Controller
    {
        WebWizard.Entities.MailVerification mailVerification = new Entities.MailVerification();
        ServiceAccess Access = new ServiceAccess();
        public JsonResult GetCountry()
        {
            var GetCountryList = Access.WebWizardRegisteredService.GetCountryList();
            return Json(GetCountryList.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: ClientRegistration
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(ClientRegistration clientRegistration)
        {
            var checkEmail = Access.ClientRegistrationService.GetClientByEmail(clientRegistration.Email);
            if (checkEmail!=null)
            {
                clientRegistration.Status = false;
                return Json(clientRegistration, JsonRequestBehavior.AllowGet);
            }
            else
            {
                clientRegistration.Status = true;
                var code = Guid.NewGuid();
                Session["VerificationCode"] = code.ToString();
                mailVerification.Email(clientRegistration.Email, code.ToString());
                return Json(clientRegistration, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Register2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClient(ClientRegistration clientRegistration)
        {
            string code=Session["VerificationCode"].ToString();
            if (code==clientRegistration.VerificationCode)
            {
            clientRegistration.Status = true;
            clientRegistration.CreateDate = DateTime.Now;
            clientRegistration.UpdateDate = DateTime.Now;
            clientRegistration.CreateBy = 0;
            clientRegistration.UpdateBy = 0;
            clientRegistration.TermsAndConditionsId = 0;
            var registration= Access.ClientRegistrationService.ClientRegistration(clientRegistration);

                ClientDetailsModel clientDetails = new ClientDetailsModel();
                Access.ClientProfileService.AddClientDetailInfo(clientDetails, registration.ClientId);
                return Json(registration, JsonRequestBehavior.AllowGet);
            }
            else
            {
                clientRegistration.VerificationCode = null;
                return Json(clientRegistration, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult ForgotPasswordForClient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPasswordForClientCode(ForgotPasswordModel forgotPassword)
        {

            var checkEmail = Access.ClientRegistrationService.GetClientByEmail(forgotPassword.Email);
            if (checkEmail!=null)
            {
                var code = Guid.NewGuid();
                Session["ForgotPasswordForClientCode"] = code.ToString();
                mailVerification.Email(checkEmail.Email, code.ToString());
                Session["ForgotPasswordForClientByEmail"] = checkEmail.Email;
                return Json(checkEmail, JsonRequestBehavior.AllowGet);
            }
            else
            {
                forgotPassword.Email = null;
                return Json(forgotPassword, JsonRequestBehavior.AllowGet);
            }
           
        }


        public ActionResult ForgotPasswordForClientCodeAndPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeClientPassword(ForgotPasswordModel changePassword)
        {
           string code= Session["ForgotPasswordForClientCode"].ToString();
            if (code== changePassword.Code)
            {
                string email = Session["ForgotPasswordForClientByEmail"].ToString();
                var changePass = Access.ClientRegistrationService.GetRegisteredClientByEmail(email, changePassword.Password);
                changePassword.Code = changePass.VerificationCode;
                return Json(changePassword, JsonRequestBehavior.AllowGet);
            }
            else
            {
                changePassword.Code = null;
                return Json(changePassword,JsonRequestBehavior.AllowGet);   
            }

        }
    }
}