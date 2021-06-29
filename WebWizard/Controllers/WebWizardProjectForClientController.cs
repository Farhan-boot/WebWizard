using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Entities;
using WebWizard.Helper;
using Wizard.Model.Clients;

namespace WebWizard.Controllers
{
    public class WebWizardProjectForClientController : Controller
    {
        ServiceAccess Access = new ServiceAccess();

        // Pagination proparty start, GetAllWebWizard for ClientSite
        public int? Page1 { get; set; }
        public string Type { get; set; }
        public int TotalDishesCount1 { get; set; }
        public IPagedList<ShowAllProjectForClientModel> DishesPageList1 { get; set; }
        // Pagination proparty end, GetAllWebWizard for ClientSite

        // GET: WebWizardProjectForClient
        public ActionResult AllProject()
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

        public ActionResult GetAllProject(int? Page1,string Type)
        {
            var allProjectList = Access.ClientProfileService.GetAllProjectList();

            WebWizardProjectForClientController webWizardProjectForClientController = new WebWizardProjectForClientController();

            int pageSize1 = 3;
            int pageNumber1 = (Page1 ?? 1);
            if (Page1 > 0)
            {
                webWizardProjectForClientController.Page1 = Page1;
            }
            webWizardProjectForClientController.TotalDishesCount1 = allProjectList.Count();
            if (Type != null && Type != "")
            {
                var DishesPageList = allProjectList.Where(x => x.NameOfProject == Type).ToPagedList(pageNumber1, pageSize1);
                return Json(DishesPageList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var DishesPageList = allProjectList.ToPagedList(pageNumber1, pageSize1);
                return Json(DishesPageList, JsonRequestBehavior.AllowGet);
            }

          //  return Json(DishesPageList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Single(int id)
        {
            var ProjectDetail = Access.ClientProfileService.GetSingleProjectById(id);
            MyWebWizardPortfolio myWebWizardPortfolioObj = new MyWebWizardPortfolio();
            myWebWizardPortfolioObj.BackendLanguageId = ProjectDetail.BackendLanguageId;
            myWebWizardPortfolioObj.CreateBy = ProjectDetail.CreateBy;
            myWebWizardPortfolioObj.CreateDate = ProjectDetail.CreateDate;
            myWebWizardPortfolioObj.Id = ProjectDetail.Id;
            myWebWizardPortfolioObj.IsDelete = ProjectDetail.IsDelete;
            myWebWizardPortfolioObj.IsFreeDownload = ProjectDetail.IsFreeDownload;
            myWebWizardPortfolioObj.IsOnlyRegisteredUserCanSee = ProjectDetail.IsOnlyRegisteredUserCanSee;
            myWebWizardPortfolioObj.IsPublishNow = ProjectDetail.IsPublishNow;
            myWebWizardPortfolioObj.LiveDemoLink = ProjectDetail.LiveDemoLink;
            myWebWizardPortfolioObj.ProjectDescription = ProjectDetail.ProjectDescription;
            myWebWizardPortfolioObj.ProjectImagePath = ProjectDetail.ProjectImagePath;
            myWebWizardPortfolioObj.ProjectSize = ProjectDetail.ProjectSize;
            myWebWizardPortfolioObj.ProjectTitle = ProjectDetail.ProjectTitle;
            myWebWizardPortfolioObj.ProjectTypeId = ProjectDetail.ProjectTypeId;
            myWebWizardPortfolioObj.ProjectZipFilePath = ProjectDetail.ProjectZipFilePath;
            myWebWizardPortfolioObj.RunOnServerId = ProjectDetail.RunOnServerId;
            myWebWizardPortfolioObj.Status = ProjectDetail.Status;
            myWebWizardPortfolioObj.TechnologyId = ProjectDetail.TechnologyId;
            myWebWizardPortfolioObj.UpdateBy = ProjectDetail.UpdateBy;
            myWebWizardPortfolioObj.UpdateDate = ProjectDetail.UpdateDate;
            myWebWizardPortfolioObj.WebWizardId = ProjectDetail.WebWizardId;
            return View(myWebWizardPortfolioObj);
        }
    }
}