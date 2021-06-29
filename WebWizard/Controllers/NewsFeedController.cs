using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Helper;
using WebWizard.Models;
using Wizard.Model.Clients;
using Wizard.Model.WebWizard;

namespace WebWizard.Controllers
{
    public class NewsFeedController : Controller
    {  // Pagination proparty start for GetMyNewsFeed
        public int? Page { get; set; }
        public int TotalDishesCount { get; set; }
        public IPagedList<ShowNewsFeedModel> DishesPageList { get; set; }
        // Pagination proparty end for GetMyNewsFeed

        // Pagination proparty start for GetMyNewsFeedForClient
        public int? PageForClient { get; set; }
        public int TotalDishesCountForClient { get; set; }
        public IPagedList<ShowNewsFeedModel> DishesPageListForClient { get; set; }
        // Pagination proparty end for GetMyNewsFeedForClient

        // Pagination proparty start for GetAllWizardExceptMe
        public int? AllWizardExceptMePage { get; set; }
        public int AllWizardExceptMeTotalDishesCount { get; set; }
        public IPagedList<ShowAllWizardExceptMeModel> AllWizardExceptMeDishesPageList { get; set; }
        // Pagination proparty end for GetAllWizardExceptMe

        // Pagination proparty start for GetAllClient
        public int? AllClientPage { get; set; }
        public int AllClientTotalDishesCount { get; set; }
        public IPagedList<ShowAllClientModel> AllClientDishesPageList { get; set; }
        // Pagination proparty end for GetAllClient

        // Pagination proparty start for GetAllWizardProjecExceptMyProjecs
        public int? AllWizardProjecExceptMyProjecsPage { get; set; }
        public int AllWizardProjecExceptMyProjecsCount { get; set; }
        public IPagedList<WebWizardPortfolio> AllWizardProjecExceptMyProjecsDishesPageList { get; set; }
        // Pagination proparty end for GetAllWizardProjecExceptMyProjecs


        ServiceAccess Access = new ServiceAccess();
        // GET: NewsFeed
        public ActionResult MyNewsFeed()
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
        public ActionResult GetMyNewsFeed(int? Page)
        {
            NewsFeedController newsFeedController = new NewsFeedController();
            var AllNewsFeed = Access.NewsFeedService.GetNewsFeed().Where(x=>x.IsWebWizard==true);

            int pageSize = 3;
            int pageNumber = (Page ?? 1);
            if (Page > 0)
            {
                newsFeedController.Page = Page;
            }
            newsFeedController.TotalDishesCount = AllNewsFeed.Count();
            var DishesPageList = AllNewsFeed.ToPagedList(pageNumber, pageSize);

            return Json(DishesPageList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetMyNewsFeedForClient(int? PageForClient)
        {
            NewsFeedController newsFeedController = new NewsFeedController();
            var AllNewsFeed = Access.NewsFeedService.GetNewsFeedForClient().Where(x => x.IsWebWizard == false);

            int pageSize = 3;
            int pageNumber = (PageForClient ?? 1);
            if (PageForClient > 0)
            {
                newsFeedController.PageForClient = PageForClient;
            }
            newsFeedController.TotalDishesCountForClient = AllNewsFeed.Count();
            var DishesPageListForClient = AllNewsFeed.ToPagedList(pageNumber, pageSize);

            return Json(DishesPageListForClient, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNewsFeedCounter()
        {
            var NewsFeedCounter = Access.NewsFeedService.GetNewsFeedCounter();
            return Json(NewsFeedCounter, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SaveNewsFeed(NewsFeedModel newsFeed)
        {
            if (newsFeed.Title == null || newsFeed.Content == null)
            {
                return RedirectToAction("Profile", "WebWizardDashboard");
            }
            else
            {
                //post into db
                int WebWizardId = (int)Session["WebWizardId"];
                newsFeed.UserId = WebWizardId;
                var myNewsFeed = Access.NewsFeedService.AddWebWizardNewsFeed(newsFeed);
                return RedirectToAction("MyNewsFeed", "NewsFeed");
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SaveNewsFeedForClient(NewsFeedModel newsFeed)
        {
            if (newsFeed.Title == null || newsFeed.Content == null)
            {
                return RedirectToAction("Profile", "ClientDashboard");
            }
            else
            {
                //post into db
                int clientId = (int)Session["ClientId"];
                newsFeed.UserId = clientId;
                var myNewsFeed = Access.NewsFeedService.AddClientNewsFeed(newsFeed);
                ChatHub.Show();
                return RedirectToAction("Profile", "ClientDashboard");
            }
        }

        [HttpPost]
        public ActionResult GetAllWizardExceptMe(int? Page2)
        {
            int webWizardId = (int)Session["WebWizardId"];
            var AllWizardExceptMe = Access.NewsFeedService.GetAllWizardExceptMe(webWizardId);

            NewsFeedController newsFeedController = new NewsFeedController();

            int pageSize2 = 3;
            int pageNumber2 = (Page2 ?? 1);
            if (Page2 > 0)
            {
                newsFeedController.AllWizardExceptMePage = Page2;
            }
            newsFeedController.AllWizardExceptMeTotalDishesCount = AllWizardExceptMe.Count();
            var AllWizardExceptMeDishesPageList = AllWizardExceptMe.ToPagedList(pageNumber2, pageSize2);

            return Json(AllWizardExceptMeDishesPageList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetAllClient(int? Page4)
        {
            int clientId = 0;// (int)Session["ClientId"];
            var AllClient = Access.NewsFeedService.GetAllClient(clientId);

            NewsFeedController newsFeedController = new NewsFeedController();

            int pageSize4 = 3;
            int pageNumber4 = (Page4 ?? 1);
            if (Page4 > 0)
            {
                newsFeedController.AllClientPage = Page4;
            }
            newsFeedController.AllClientTotalDishesCount = AllClient.Count();
            var AllClientDishesPageList = AllClient.ToPagedList(pageNumber4, pageSize4);

            return Json(AllClientDishesPageList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAllWizardProjecExceptMyProjecs(int? Page3)
        {
            int webWizardId = (int)Session["WebWizardId"];
            var AllWizardProjecExceptMyProjecs = Access.NewsFeedService.GetAllWizardProjecExceptMyProjecs(webWizardId);
            NewsFeedController newsFeedController = new NewsFeedController();
            int pageSize3 = 3;
            int pageNumber3 = (Page3 ?? 1);
            if (Page3 > 0)
            {
                newsFeedController.AllWizardProjecExceptMyProjecsPage = Page3;
            }
            newsFeedController.AllWizardProjecExceptMyProjecsCount = AllWizardProjecExceptMyProjecs.Count();
            var AllWizardProjecExceptMyProjecsDishesPageList = AllWizardProjecExceptMyProjecs.ToPagedList(pageNumber3, pageSize3);
            return Json(AllWizardProjecExceptMyProjecsDishesPageList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WebWizardListForClient()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddNewsFeedBidForClient(Wizard.Data.Data.Entities.WebWizardBid newsFeedBid)
        {
            int WebWizardId = (int)Session["WebWizardId"];
            newsFeedBid.WebWizardId = WebWizardId;
            var bid = Access.NewsFeedService.AddBidForClient(newsFeedBid);
           
            ChatHub.NotificationForNewsFeedBid();

            return Json(bid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClientNewsFeedDetails(string id)
        {
            var details = Access.NewsFeedService.ClientNewsFeedDetails(Convert.ToInt32(id));
            ClientNewsFeedDetails dtls = new ClientNewsFeedDetails();
            dtls.Amount = details.Amount;
            dtls.BackendLanguage = details.BackendLanguage;
            dtls.GroupName = details.GroupName;
            dtls.Id = details.Id;
            dtls.IsWebWizard = details.IsWebWizard;
            dtls.PostContent = details.PostContent;
            dtls.PostDate = details.PostDate;
            dtls.ProjectType = details.ProjectType;
            dtls.RunOnServer = details.RunOnServer;
            dtls.Technology = details.Technology;
            dtls.Title = details.Title;
            dtls.UserId = details.UserId;
            return View(dtls);
        }


        [HttpPost]
        public JsonResult UpdateNewsFeedBidForClient(Wizard.Data.Data.Entities.WebWizardBid newsFeedBid)
        {
            int WebWizardId = (int)Session["WebWizardId"];
            newsFeedBid.WebWizardId = WebWizardId;
           var updateBid = Access.NewsFeedService.UpdateNewsFeedBidForClient(newsFeedBid);
            return Json(updateBid, JsonRequestBehavior.AllowGet);
        }

    }
}