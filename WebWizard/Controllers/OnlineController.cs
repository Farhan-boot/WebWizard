using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebWizard.Controllers
{
    public class OnlineController : Controller
    {
        // GET: Online
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClientChat()
        {
            try
            {
                if (Convert.ToBoolean(Session["ClientStatus"]) == true && Convert.ToInt32(Session["ClientId"]) != 0)
                {
                    if (Session["ClientFirstName"].ToString() != "" && Session["ClientLastName"].ToString() != "" && Session["ClientEmail"].ToString() != "" && Session["ClientNameTitle"].ToString() != "")
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index", "LogIn");
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

        public ActionResult WizardChat()
        {
            return View();
        }

        public ActionResult WizardVideoCall()
        {
            return View();
        }

        public ActionResult ClientVideoCall()
        {
            return View();
        }
    }
}