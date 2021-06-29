using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Helper;
using Wizard.Model.Clients;

namespace WebWizard.Controllers
{
    public class WebWizardMemberController : Controller
    {
        ServiceAccess Access = new ServiceAccess();

        // Pagination proparty start, GetAllWebWizard for ClientSite
        public int? Page { get; set; }
        public int TotalDishesCount { get; set; }
        public IPagedList<ShowAllWebWizardModel> DishesPageList { get; set; }
        // Pagination proparty end, GetAllWebWizard for ClientSite

        // Pagination proparty start, GetAllClient for ClientSite
        public int? Page2 { get; set; }
        public int TotalDishesCount2 { get; set; }
        public IPagedList<ShowAllClientModel> DishesPageList2 { get; set; }
        // Pagination proparty end, GetAllClient for ClientSite

        [HttpPost]
        public ActionResult GetWebWizardList(int? Page)
        {
            WebWizardMemberController webWizardMemberController = new WebWizardMemberController();
            var AllWebWizardList = Access.ClientProfileService.GetAllWebWizardList();

            int pageSize = 3;
            int pageNumber = (Page ?? 1);
            if (Page > 0)
            {
                webWizardMemberController.Page = Page;
            }
            webWizardMemberController.TotalDishesCount = AllWebWizardList.Count();
            var DishesPageList = AllWebWizardList.ToPagedList(pageNumber, pageSize);

            return Json(DishesPageList, JsonRequestBehavior.AllowGet);
        }

        // GET: WebWizardMember
        public ActionResult WebWizardMembers()
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


        [HttpPost]
        public ActionResult GetAllClientExceptMe(int? Page2)
        {
            int clientId = (int)Session["ClientId"];
            var AllClientExceptMe = Access.ClientProfileService.GetAllClientExceptMe(clientId);

            WebWizardMemberController webWizardMemberController = new WebWizardMemberController();

            int pageSize2 = 3;
            int pageNumber2 = (Page2 ?? 1);
            if (Page2 > 0)
            {
                webWizardMemberController.Page2 = Page2;
            }
            webWizardMemberController.TotalDishesCount2 = AllClientExceptMe.Count();
            var AllClientExceptMeDishesPageList = AllClientExceptMe.ToPagedList(pageNumber2, pageSize2);

            return Json(AllClientExceptMeDishesPageList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClientMembers()
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
    }
}