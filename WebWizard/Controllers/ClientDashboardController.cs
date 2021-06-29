using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWizard.Helper;
using WebWizard.Models;
using Wizard.Model.Client;

namespace WebWizard.Controllers
{
    public class ClientDashboardController : Controller
    {
        // Pagination proparty start for Client
        public int? Page { get; set; }
        public int TotalDishesCount { get; set; }
        public IPagedList<Wizard.Data.Data.Entities.NewsFeed> DishesPageList { get; set; }
        // Pagination proparty end for Client

        ServiceAccess Access = new ServiceAccess();
        // GET: ClientDashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
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
        public ActionResult SaveProfilePicture(ClientDetailsModel clientProfilePicture)
        {


            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;

            var file = Request.Files[0] as HttpPostedFileBase;
            actualFileName = file.FileName;
            fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            int size = file.ContentLength;
            // ResizeImage.ResizeStream(500, file.InputStream, Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "UserAssets\\ProfilePhotos\\", Path.GetFileName(fileName)));
            file.SaveAs(Path.Combine(Server.MapPath("~/ClientAssets/ClientDashboard/ProfileImage"), fileName));
            int clientId = (int)Session["ClientId"];

            if (fileName != null)
            {
                clientProfilePicture.ClientProfileImageUrl = fileName;
                var oldClientDetails = Access.ClientProfileService.GetClientDetailsByClientId(clientId);
                ClientDetailsModel newClientDetails = new ClientDetailsModel();

                if (oldClientDetails == null)
                {
                    clientProfilePicture.ClientId = clientId;
                    clientProfilePicture.AboutClient = null;
                    clientProfilePicture.DateOfBarth = null;
                    clientProfilePicture.LocationId = null;
                    clientProfilePicture.EducationId = null;
                    clientProfilePicture.ClientMobileNo = null;
                    clientProfilePicture.DesignationId = null;
                    clientProfilePicture.Status = 1;
                    clientProfilePicture.CreateDate = DateTime.Now;
                    clientProfilePicture.UpdateDate = DateTime.Now;
                    clientProfilePicture.CreateBy = clientId;
                    clientProfilePicture.UpdateBy = clientId;
                   var addProfilePicture= Access.ClientProfileService.AddClientDetails(clientProfilePicture);
                   Session["ClientProfileImageUrl"] = "/ClientAssets/ClientDashboard/ProfileImage/" + addProfilePicture.ClientProfileImageUrl;
                }
                else
                {
                    oldClientDetails.ClientProfileImageUrl = fileName;
                    Access.ClientProfileService.UpdateClientProfilePicture(oldClientDetails, clientId);
                    Session["ClientProfileImageUrl"] = "/ClientAssets/ClientDashboard/ProfileImage/" + clientProfilePicture.ClientProfileImageUrl;
                }

                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }



            //return Json("", JsonRequestBehavior.AllowGet);
         
        }

        [HttpPost]
        public ActionResult ClientDetails(ClientDetailsModel clientDetails)
        {
            int clientId = (int)Session["ClientId"];
            var oldClientDetail = Access.ClientProfileService.GetClientDetailsByClientId(clientId);
            if (oldClientDetail!= null)
            {
                //update
                var updateDetails = Access.ClientProfileService.UpdateClientDetails(oldClientDetail, clientDetails);
            }
            else
            {
                //Add
                var addDetails = Access.ClientProfileService.AddClientDetailInfo(clientDetails, clientId);
            }




            return Json("",JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetEightWebWizardList()
        {
            var eightWebWizardList = Access.ClientProfileService.GetEightWebWizardList();

            return Json(eightWebWizardList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetClientInfo()
        {
            int clientId = (int)Session["ClientId"];
            var clientinformetion = Access.ClientProfileService.GetClientInformetionByClientId(clientId);
            return Json(clientinformetion, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetClientInfoForChat()
        {
            UserClient clientinformetionForChat=null;
            try
            {
                if (Session["ClientId"].ToString() != "" || Session["ClientId"].ToString() != null)
                {
                    clientinformetionForChat = UserClientForChat.SetValueForClientChatUser(Session["ClientId"].ToString(), Session["ClientFullName"].ToString(), Session["ClientEmail"].ToString(), Session["ClientProfileImageUrl"].ToString(), "User");
                }
            }
            catch (Exception ex)
            {

                clientinformetionForChat = UserClientForChat.SetValueForClientChatUser(Session["WebWizardId"].ToString(), Session["FullName"].ToString(), Session["Email"].ToString(), Session["WebWizardProfileImageUrl"].ToString(), "Wizard");
            }
           
            return Json(clientinformetionForChat, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult MyNewsFeed()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetMyNewsFeedItems(int? Page)
        {
            ClientDashboardController clientDashboardController = new ClientDashboardController();
            int clientId = (int)Session["ClientId"];
            var myNewsFeedList = Access.ClientProfileService.GetMyNewsFeedListByClientId(clientId);
            int pageSize = 3;
            int pageNumber = (Page ?? 1);
            if (Page > 0)
            {
                clientDashboardController.Page = Page;
            }
            clientDashboardController.TotalDishesCount = myNewsFeedList.Count();
            var DishesPageList = myNewsFeedList.ToPagedList(pageNumber, pageSize);
            return Json(DishesPageList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteClientNewsFeed(Wizard.Data.Data.Entities.NewsFeed NewsFeed)
        {
            var deleteClientNewsFeed = Access.ClientProfileService.DeleteClientNewsFeed(NewsFeed.Id);
            return Json(deleteClientNewsFeed, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProposalList(Wizard.Data.Data.Entities.NewsFeed NewsFeed)
        {
            var ProposalList = Access.ClientProfileService.GetProposalList(NewsFeed.Id);
            return Json(ProposalList, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult DeleteBid(Wizard.Data.Data.Entities.NewsFeed NewsFeed)
        {
            var deleteBidByClient = Access.ClientProfileService.DeleteBidByClient(NewsFeed.Id);
            return Json(deleteBidByClient, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ViewDetailsByBidId(string Id)
        {
            int id = Convert.ToInt32(Id);
            var detailsByBidId = Access.ClientProfileService.GetViewDetailsByBidId(id);
            ViewDetailsByBidIdModel myModel = new ViewDetailsByBidIdModel();
            myModel.BidAmount = detailsByBidId.BidAmount;
            myModel.Id = detailsByBidId.Id;
            myModel.NewsFeedId = detailsByBidId.NewsFeedId;
            myModel.NewsFeedTitle = detailsByBidId.NewsFeedTitle;
            myModel.ProposalAmount = detailsByBidId.ProposalAmount;
            myModel.WizardEmail = detailsByBidId.WizardEmail;
            myModel.WizardFarstName = detailsByBidId.WizardFarstName;
            myModel.WizardId = detailsByBidId.WizardId;
            myModel.WizardImagePath = detailsByBidId.WizardImagePath;
            myModel.WizardLastName = detailsByBidId.WizardLastName;

            return View(myModel);
        }


        [HttpPost]
        public JsonResult ApprovedByClient(Wizard.Data.Data.Entities.NewsFeed NewsFeed)
        {
            var approved = Access.ClientProfileService.ApprovedByClient(NewsFeed.Id);
            ChatHub.ShowWizardNotification(approved.WebWizardId);
            return Json(approved, JsonRequestBehavior.AllowGet);
        }

        public static int setClientId { get; set; }
        public JsonResult GetClientNotificationForNewsFeedBid()
        {
            int clientId = (int)Session["ClientId"];
            setClientId = clientId;
            var acceptNewsFeedBidNotification = Access.ClientProfileService.GetClientNotificationForAcceptNewsFeedBid(clientId);
            return Json(acceptNewsFeedBidNotification.OrderByDescending(x => x.Id).Take(3), JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetClientMessageList(int pageNumber, int pageSize)
        {
            int clientId = (int)Session["ClientId"];
            var messageListByClientId = Access.MessageService.GetClientMessageList(clientId).ToPagedList(pageNumber, pageSize);

            return Json(messageListByClientId.OrderByDescending(x => x.MessageId).Where(x => x.UserId != clientId).ToList(), JsonRequestBehavior.AllowGet);
        }

        bool isFirstTime = true;
        [HttpPost]
        public JsonResult LoadMessageList(string sender, string senderType, string Receiver, string ReceiverType, int pageNumber, int pageSize)
        {
            IEnumerable<Wizard.Data.Data.Entities.Message> msgList;
            if (isFirstTime == true)
            {
                msgList = Access.MessageService.GetUserMessageList(sender, senderType, Receiver, ReceiverType).OrderBy(x => x.Id).Reverse().ToPagedList(pageNumber, pageSize);
                isFirstTime = false;
            }
            else
            {
                msgList = Access.MessageService.GetUserMessageList(sender, senderType, Receiver, ReceiverType).OrderBy(x => x.Id).Reverse().ToPagedList(pageNumber, pageSize).Reverse();
            }


            return Json(msgList, JsonRequestBehavior.AllowGet);
        }






    }
}