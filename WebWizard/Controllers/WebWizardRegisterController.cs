using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Helper;
using WebWizard;
using static WebWizard.Entities.MyWebWizard;
using Wizard.Models;
using WebWizard.Entities;
using Wizard.Model.WebWizard;
using WebWizard.Models;

namespace WebWizard.Controllers
{
    public class WebWizardRegisterController : Controller
    {
        WebWizard.Entities.MailVerification mailVerification = new Entities.MailVerification();
        ServiceAccess Access = new ServiceAccess();
        // GET: WebWizardRegister
        public ActionResult Index()
        {
            // MailVerification mail = new MailVerification();
            // mail.Email("fsjesy@gmail.com");
            return View();
        }


        public JsonResult GetTermAndCondition()
        {
            var GetTermAndConditionList = Access.WebWizardRegisteredService.TermAndConditionList();
            return Json(GetTermAndConditionList.ToList(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCountry()
        {
            var GetCountryList = Access.WebWizardRegisteredService.GetCountryList();
            return Json(GetCountryList.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(WebWizardRegisterModel WebWizardRegister)
        {
            WebWizardRegisterModel wizardRegisterOneObj = new WebWizardRegisterModel();
            wizardRegisterOneObj.NameTitle = WebWizardRegister.NameTitle;
            wizardRegisterOneObj.FirstName = WebWizardRegister.FirstName;
            wizardRegisterOneObj.LastName = WebWizardRegister.LastName;
            wizardRegisterOneObj.Email = WebWizardRegister.Email;

            bool webWizardEmail = Access.WebWizardRegisteredService.GetByEmail(WebWizardRegister);
            if (webWizardEmail == false)
            {
                wizardRegisterOneObj.Status = true;
                return Json(wizardRegisterOneObj, JsonRequestBehavior.AllowGet);
            }
            else
                wizardRegisterOneObj.Status = false;
            return Json(wizardRegisterOneObj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Register_02()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register_02(WebWizardRegisterModel WebWizardRegister)
        {
            WebWizardRegisterModel wizardRegisterTwoObj = new WebWizardRegisterModel();
            wizardRegisterTwoObj.StateId = WebWizardRegister.StateId;
            wizardRegisterTwoObj.Password = PasswordEncryptOrDecrypt.EncodePassword(WebWizardRegister.Password);
            wizardRegisterTwoObj.StartAsFreelancer = WebWizardRegister.StartAsFreelancer;
            wizardRegisterTwoObj.TermsAndConditionsId = WebWizardRegister.TermsAndConditionsId;
            wizardRegisterTwoObj.Email = WebWizardRegister.Email;

            var code = Guid.NewGuid();
            Session["VerificationCodeForWebWizard"] = code.ToString();
            mailVerification.Email(WebWizardRegister.Email, code.ToString());

            return Json(wizardRegisterTwoObj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Register_03()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register_03(WebWizardRegisterModel WebWizardRegister)
        {
            string myVerificationCode = Session["VerificationCodeForWebWizard"].ToString();

            if (myVerificationCode != WebWizardRegister.VerificationCode)
            {
                WebWizardRegister.VerificationCode = null;
                return Json(WebWizardRegister, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var webWizard = Access.WebWizardRegisteredService.Save(WebWizardRegister);
                webWizard.Status = true;
                return Json(webWizard, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Register_04()
        {
            return View();
        }

        //Advanced Settings
        [HttpPost]
        public ActionResult UpdateAdvancedSettings(Wizard.Data.Data.Entities.WebWizardRegistration advancedSettings)
        {
            int webWizardId = (int)Session["WebWizardId"];
            if (webWizardId != 0)
            {
                advancedSettings.Password = PasswordEncryptOrDecrypt.EncodePassword(advancedSettings.Password);
                var updateAdvancedSettings = Access.WebWizardRegisteredService.UpdateAdvanced(webWizardId, advancedSettings);
                Session["FirstName"] = updateAdvancedSettings.FirstName;
                Session["LastName"] = updateAdvancedSettings.LastName;
                Session["FullName"] = updateAdvancedSettings.FirstName + " " + updateAdvancedSettings.LastName;
                Session["Email"] = updateAdvancedSettings.Email;
                return Json(updateAdvancedSettings, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }


        public ActionResult ForgotPasswordForWebWizard()
        {
            return View();
        }

        public ActionResult ForgotPasswordForWizardCode(ForgotPasswordModel forgotPassword)
        {
            var checkEmail = Access.WebWizardRegisteredService.GetWebWizardByEmail(forgotPassword.Email);

            if (checkEmail != null)
            {
                var code = Guid.NewGuid();
                Session["ForgotPasswordForWebWizardCode"] = code.ToString();
                mailVerification.Email(checkEmail.Email, code.ToString());
                Session["ForgotPasswordForWebWizardByEmail"] = checkEmail.Email;
                return Json(checkEmail, JsonRequestBehavior.AllowGet);
            }
            else
            {
                forgotPassword.Email = null;
                return Json(forgotPassword, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult ForgotPasswordForWebWizardCodeAndPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeWizardPassword(ForgotPasswordModel changePassword)
        {
            string code = Session["ForgotPasswordForWebWizardCode"].ToString();
            if (code == changePassword.Code)
            {
                string email = Session["ForgotPasswordForWebWizardByEmail"].ToString();
                changePassword.Password = PasswordEncryptOrDecrypt.EncodePassword(changePassword.Password);
                var changePass= Access.WebWizardRegisteredService.GetRegisteredWebWizardByEmail(email, changePassword.Password);
                changePassword.Code = changePass.VerificationCode;
                return Json(changePassword, JsonRequestBehavior.AllowGet);
            }
            else
            {
                changePassword.Code = null;
                return Json(changePassword, JsonRequestBehavior.AllowGet);
            }

        }

    }
}