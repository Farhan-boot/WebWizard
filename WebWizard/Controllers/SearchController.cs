using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Helper;
using PagedList;
using Wizard.Model.Search;

namespace WebWizard.Controllers
{
    public class SearchController : Controller
    {
        ServiceAccess Access = new ServiceAccess();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WebWizard()
        {
            return View();
        }
        public ActionResult Clients()
        {
            return View();
        }
        public ActionResult Projects()
        {
            return View();
        }
        [HttpPost]
        public ActionResult List(string fullname,string searchtype)
        {
            if (searchtype== "freelancer"&& fullname==""|| fullname==null)
            {
                return RedirectToAction("WebWizard");
            }
            else if(searchtype == "Clients" && fullname == "" || fullname == null)
            {
                return RedirectToAction("Clients");
            }
            else
            {
                return RedirectToAction("Projects");
            }
        }



        //web api start
        [HttpPost]
        public JsonResult GetWebWizardListForSearch(int Page,string Name)
        {
            IPagedList<ShowWebWizardListModel> WebWizardList;
            if (Name!=null&& Page<=0)
            {
                    WebWizardList = Access.SearchService.GetWebWizardListForSearch().Where(x=>x.Name==Name).ToPagedList(1,20);
            }
            else
            {
                 WebWizardList = Access.SearchService.GetWebWizardListForSearch().ToPagedList(Page, 3);
            }

            return Json(WebWizardList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetClientsListForSearch(int Page, string Name)
        {
            IPagedList<ShowWebWizardListModel> ClientsList;
            if (Name != null && Page <= 0)
            {
                ClientsList = Access.SearchService.GetClientsListForSearch().Where(x => x.Name == Name).ToPagedList(1, 20);
            }
            else
            {
                ClientsList = Access.SearchService.GetClientsListForSearch().ToPagedList(Page, 3);
            }

            return Json(ClientsList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProjectsListForSearch(int Page)
        {
           var projectsList = Access.SearchService.GetProjectsListForSearch().ToPagedList(Page, 3);

            return Json(projectsList, JsonRequestBehavior.AllowGet);
        }
        //web api end
    }
}