using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Data.Entities;
using Wizard.Model.WebWizard;
using Wizard.Models;
using Wizard.Data.Data;
using Wizard.Model.Client;
using Wizard.Data.CustomModel;
using Wizard.Model.Clients;

namespace Wizard.Data.Repository
{
    public interface IClientProfileRepository
    {
        ClientDetails GetClientDetailsByClientId(int clientId);
        ClientDetailsModel AddClientDetails(ClientDetailsModel clientProfilePicture);
        ClientDetails UpdateClientProfilePicture(ClientDetails oldClientDetails, int clientId);
        Wizard.Data.Data.Entities.ClientDetails UpdateClientDetails(Wizard.Data.Data.Entities.ClientDetails oldClientDetail, ClientDetailsModel clientDetails);
        Wizard.Data.Data.Entities.ClientDetails AddClientDetailInfo(ClientDetailsModel clientDetails, int clientId);
        object GetEightWebWizardList();
        object GetClientInformetionByClientId(int clientId);
        List<Wizard.Model.Clients.ShowAllWebWizardModel> GetAllWebWizardList();
        List<ShowAllProjectForClientModel> GetAllProjectList();
        Wizard.Data.Data.Entities.WebWizardPortfolio GetSingleProjectById(int id);
        List<ShowAllClientModel> GetAllClientExceptMe(int clientId);
        List<NewsFeed> GetMyNewsFeedListByClientId(int clientId);
        Wizard.Data.Data.Entities.NewsFeed DeleteClientNewsFeed(int id);
        object GetProposalList(int newsFeedId);
        Wizard.Data.Data.Entities.WebWizardBid DeleteBidByClient(int Id);
        Wizard.Data.Data.Entities.WebWizardBid ApprovedByClient(int Id);
        IEnumerable<WebWizardNotificationModel> GetClientNotificationForAcceptNewsFeedBid(int clientId);
        Wizard.Data.CustomModel.ViewDetailsByBidIdModel GetViewDetailsByBidId(int Id);
    }

    public class ClientProfileRepository : IClientProfileRepository
    {
        public ClientDetails AddClientDetailInfo(ClientDetailsModel clientDetails, int clientId)
        {
            Wizard.Data.Data.Entities.ClientDetails clientDetailObj = new Wizard.Data.Data.Entities.ClientDetails();
            using (WebWizardConnection db = new WebWizardConnection())
            {
                clientDetailObj.AboutClient = clientDetails.AboutClient;
                clientDetailObj.ClientId = clientId;
                clientDetailObj.ClientMobileNo = clientDetails.ClientMobileNo;
                clientDetailObj.ClientProfileImageUrl = clientDetails.ClientProfileImageUrl;
                clientDetailObj.CreateBy = clientId;
                clientDetailObj.CreateDate = DateTime.Now;
                clientDetailObj.DateOfBarth = clientDetails.DateOfBarth;
                clientDetailObj.DesignationId = null;
                clientDetailObj.EducationId = clientDetails.EducationId;
                clientDetailObj.LocationId = null;
                clientDetailObj.Status = true;
                clientDetailObj.UpdateBy = clientId;
                clientDetailObj.UpdateDate = DateTime.Now;
                db.ClientDetails.Add(clientDetailObj);
                db.SaveChanges();
                return clientDetailObj;
            }
        }

        public ClientDetailsModel AddClientDetails(ClientDetailsModel clientProfilePicture)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                Wizard.Data.Data.Entities.ClientDetails obj = new Wizard.Data.Data.Entities.ClientDetails();
                obj.ClientId = clientProfilePicture.ClientId;
                obj.AboutClient = clientProfilePicture.AboutClient;
                obj.ClientMobileNo = clientProfilePicture.ClientMobileNo;
                obj.ClientProfileImageUrl = clientProfilePicture.ClientProfileImageUrl;
                obj.CreateBy = clientProfilePicture.CreateBy;
                obj.UpdateBy = clientProfilePicture.UpdateBy;
                obj.CreateDate = clientProfilePicture.CreateDate;
                obj.UpdateDate = clientProfilePicture.UpdateDate;
                obj.DateOfBarth = clientProfilePicture.DateOfBarth;
                obj.DesignationId = clientProfilePicture.DesignationId;
                obj.EducationId = clientProfilePicture.EducationId;
                obj.LocationId = clientProfilePicture.LocationId;
                obj.Status = true;

                db.ClientDetails.Add(obj);
                db.SaveChanges();
                return clientProfilePicture;
            }
        }

        public WebWizardBid ApprovedByClient(int Id)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var approvedByClient = db.WebWizardBid.SingleOrDefault(x=>x.Id==Id);
                approvedByClient.Status = true;
                db.SaveChanges();
                return approvedByClient;
            }
        }

        public WebWizardBid DeleteBidByClient(int Id)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var deletedItem = db.WebWizardBid.SingleOrDefault(x => x.Id == Id);
                if (deletedItem != null)
                {
                    db.WebWizardBid.Remove(deletedItem);
                    db.SaveChanges();
                }
                return deletedItem;
            }
        }

        public NewsFeed DeleteClientNewsFeed(int id)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var deletedItem = db.NewsFeed.SingleOrDefault(x => x.Id == id && x.IsWebWizard == false);
                if (deletedItem != null)
                {
                    db.NewsFeed.Remove(deletedItem);
                    db.SaveChanges();
                }
                return deletedItem;
            }
        }

        public List<ShowAllClientModel> GetAllClientExceptMe(int clientId)
        {
            List<ShowAllClientModel> showAllClientModelList = new List<ShowAllClientModel>();
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var allClientExceptMe = db.ClientRegistration.Join(db.ClientDetails,
                 x => x.ClientId,
                 y => y.ClientId,
                (x, y) => new { x.FirstName, x.LastName, x.Email, y.ClientProfileImageUrl, y.AboutClient, y.ClientMobileNo, y.ClientId }).Where(x => x.ClientId != clientId).ToList();

                foreach (var allClientExceptMeOneByOne in allClientExceptMe)
                {
                    ShowAllClientModel ShowAllClientExceptMeModelObj = new ShowAllClientModel();
                    ShowAllClientExceptMeModelObj.ClientId = allClientExceptMeOneByOne.ClientId;
                    ShowAllClientExceptMeModelObj.ClientEmail = allClientExceptMeOneByOne.Email;
                    ShowAllClientExceptMeModelObj.FirstName = allClientExceptMeOneByOne.FirstName;
                    ShowAllClientExceptMeModelObj.LastName = allClientExceptMeOneByOne.LastName;
                    ShowAllClientExceptMeModelObj.AboutClient = allClientExceptMeOneByOne.AboutClient;
                    ShowAllClientExceptMeModelObj.ClientMobileNo = allClientExceptMeOneByOne.ClientMobileNo;
                    if (allClientExceptMeOneByOne.ClientProfileImageUrl == null || allClientExceptMeOneByOne.ClientProfileImageUrl == "")
                    {
                        ShowAllClientExceptMeModelObj.ClientProfileImageUrl = "user-demo.png";
                    }
                    else
                    {
                        ShowAllClientExceptMeModelObj.ClientProfileImageUrl = allClientExceptMeOneByOne.ClientProfileImageUrl;
                    }
                    showAllClientModelList.Add(ShowAllClientExceptMeModelObj);
                }
                return showAllClientModelList.ToList();
            }
        }

        public List<ShowAllProjectForClientModel> GetAllProjectList()
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                //var myProjectList = db.WebWizardPortfolio.Where(x => x.IsOnlyRegisteredUserCanSee == true).ToList();
                try
                {
                    List<ShowAllProjectForClientModel> ShowAllProjectForClientModel = new List<ShowAllProjectForClientModel>();
                    var myProjectList = (from s in db.WebWizardPortfolio
                                         join cs in db.Technology on s.TechnologyId equals cs.Id
                                         join os in db.RunOnServer on s.RunOnServerId equals os.Id
                                         join bc in db.BackendLanguage on s.BackendLanguageId equals bc.Id
                                         join pt in db.ProjectType on s.ProjectTypeId equals pt.Id
                                         join pw in db.WebWizardRegistration on s.WebWizardId equals pw.WebWizardId
                                         where s.IsOnlyRegisteredUserCanSee == true
                                         select new
                                         {
                                             Id = s.Id,
                                             ProjectTitle = s.ProjectTitle,
                                             ProjectDescription = s.ProjectDescription,
                                             LiveDemoLink = s.LiveDemoLink,
                                             ProjectImagePath = s.ProjectImagePath,
                                             ProjectZipFilePath = s.ProjectZipFilePath,
                                             IsFreeDownload = s.IsFreeDownload,
                                             ProjectSize = s.ProjectSize,
                                             WebWizardId = s.WebWizardId,
                                             NameOfTechnology = cs.NameOfTechnology,
                                             NameOfServer = os.NameOfServer,
                                             NameOfBackendLanguage = bc.NameOfBackendLanguage,
                                             NameOfProject = pt.NameOfProject,
                                             NameTitle = pw.NameTitle,
                                             FirstName = pw.FirstName,
                                             LastName = pw.LastName,
                                         }).ToList();


                    foreach (var project in myProjectList)
                    {
                        ShowAllProjectForClientModel obj = new ShowAllProjectForClientModel();
                        obj.FirstName = project.FirstName;
                        obj.Id = project.Id;
                        obj.IsFreeDownload = project.IsFreeDownload;
                        obj.LastName = project.LastName;
                        obj.LiveDemoLink = project.LiveDemoLink;
                        obj.NameOfBackendLanguage = project.NameOfBackendLanguage;
                        obj.NameOfProject = project.NameOfProject;
                        obj.NameOfServer = project.NameOfServer;
                        obj.NameOfTechnology = project.NameOfTechnology;
                        obj.NameTitle = project.NameTitle;
                        obj.ProjectDescription = project.ProjectDescription;
                        obj.ProjectImagePath = project.ProjectImagePath;
                        obj.ProjectSize = project.ProjectSize;
                        obj.ProjectTitle = project.ProjectTitle;
                        obj.ProjectZipFilePath = project.ProjectZipFilePath;
                        obj.WebWizardId = project.WebWizardId;
                        ShowAllProjectForClientModel.Add(obj);
                    }

                    return ShowAllProjectForClientModel;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<ShowAllWebWizardModel> GetAllWebWizardList()
        {
            List<ShowAllWebWizardModel> allWebWizardList = new List<ShowAllWebWizardModel>();

            using (WebWizardConnection db = new WebWizardConnection())
            {
                var result = db.WebWizardRegistration.Join(db.WebWizardDetails,
                  x => x.WebWizardId,
                  y => y.WebWizardId,
                 (x, y) => new { x.FirstName, x.LastName, x.Email, x.NameTitle, y.WebWizardProfileImageUrl, y.DateOfBarth, y.WebWizardId }).ToList();
                foreach (var oneByOne in result)
                {
                    ShowAllWebWizardModel obj = new ShowAllWebWizardModel();
                    obj.WebWizardId = oneByOne.WebWizardId;
                    obj.FirstName = oneByOne.FirstName;
                    obj.LastName = oneByOne.LastName;
                    obj.Email = oneByOne.Email;
                    obj.NameTitle = oneByOne.NameTitle;
                    obj.DateOfBarth = oneByOne.DateOfBarth;

                    if (oneByOne.WebWizardProfileImageUrl != null)
                    {
                        obj.WebWizardProfileImageUrl = oneByOne.WebWizardProfileImageUrl;
                    }
                    else
                    {
                        obj.WebWizardProfileImageUrl = "user-demo.png";
                    }
                    allWebWizardList.Add(obj);
                }
                return allWebWizardList;
            }
        }

        public ClientDetails GetClientDetailsByClientId(int clientId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                return db.ClientDetails.SingleOrDefault(x => x.ClientId == clientId);
            }
        }

        public object GetClientInformetionByClientId(int clientId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                Data.Entities.ClientRegistration clientRegistration = db.ClientRegistration.SingleOrDefault(x => x.ClientId == clientId);
                ClientDetails clientDetails = db.ClientDetails.SingleOrDefault(x => x.ClientId == clientRegistration.ClientId);
                if (clientDetails != null)
                {
                    Education education = db.Education.SingleOrDefault(x => x.Id == clientDetails.EducationId);
                    ClientInformetion clientInfoObject = new ClientInformetion();
                    clientInfoObject.ClientRegistration = clientRegistration;
                    clientInfoObject.ClientDetails = clientDetails;
                    clientInfoObject.Education = education;
                    return clientInfoObject;
                }
                else
                {
                    return clientDetails;
                }

            }
        }

        public IEnumerable<WebWizardNotificationModel> GetClientNotificationForAcceptNewsFeedBid(int clientId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var acceptNewsFeedBidNotification = db.NewsFeed.Join(db.WebWizardBid,
                 x => x.Id,
                 y => y.NewsfeedId,
                (x, y) => new {y.NewsfeedId,y.WebWizardId,x.UserId, x.IsWebWizard,y.Status,x.Title,x.Amount,y.BidAmount,y.BidContent,y.Id }).Where(x=>x.IsWebWizard==false&&x.Status==false&&x.UserId== clientId).ToList();

                var newsFeedBidNotification = acceptNewsFeedBidNotification.Join(db.WebWizardRegistration,
                 x => x.WebWizardId,
                 y => y.WebWizardId,
                (x, y) => new {y.WebWizardId, x.NewsfeedId,y.Email,y.FirstName,y.LastName, x.UserId, x.IsWebWizard, y.Status, x.Title, x.Amount, x.BidAmount, x.BidContent, x.Id }).ToList();

                var bidNotification = newsFeedBidNotification.Join(db.WebWizardDetails,
                x => x.WebWizardId,
                y => y.WebWizardId,
               (x, y) => new {x.FirstName,x.LastName,x.Email, y.WebWizardProfileImageUrl,y.WebWizardId, x.NewsfeedId, x.UserId, x.IsWebWizard, y.Status, x.Title, x.Amount, x.BidAmount, x.BidContent, x.Id }).ToList();


                List<WebWizardNotificationModel> webWizardNotificationModel = new List<WebWizardNotificationModel>();
                foreach (var notification in bidNotification)
                {
                    WebWizardNotificationModel obj = new WebWizardNotificationModel();
                    obj.Id = notification.Id;
                    obj.NewsfeedId = notification.NewsfeedId;
                    obj.InformetionTitle = notification.Title;
                    obj.InformetionDescription = notification.BidContent;
                    obj.UserId = notification.UserId;
                    obj.InformetionOption = notification.BidAmount.ToString();
                    obj.Email = notification.Email;
                    obj.UserName = notification.FirstName+" "+notification.LastName;
                    obj.ImagePath = notification.WebWizardProfileImageUrl;
                    webWizardNotificationModel.Add(obj);
                }

                return webWizardNotificationModel;
            }
        }

        public object GetEightWebWizardList()
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var eightWebWizardList = db.WebWizardDetails.Join(db.WebWizardRegistration,
                 x => x.WebWizardId,
                 y => y.WebWizardId,
                (x, y) => new { y.FirstName, y.LastName, y.Email, x.WebWizardProfileImageUrl, x.AboutWebWizard, x.WebWizardMobileNo, y.WebWizardId }).ToList();

                return eightWebWizardList.Take(8).Reverse();
            }
        }

        public List<NewsFeed> GetMyNewsFeedListByClientId(int clientId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var myNewsFeedList = db.NewsFeed.Where(x => x.UserId == clientId && x.IsWebWizard == false).OrderByDescending(x => x.Id).ToList();
                return myNewsFeedList;
            }
        }

        public object GetProposalList(int newsFeedId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var wizardBid = db.WebWizardBid.Where(x => x.NewsfeedId == newsFeedId).ToList();

                var lList = wizardBid.Join(db.WebWizardRegistration,
                 x => x.WebWizardId,
                 y => y.WebWizardId,
                (x, y) => new { y.WebWizardId, y.FirstName, y.LastName, y.Email, x.PostDate, x.BidContent, x.Status, x.BidAmount, x.Id }).ToList();

                var ProposalList = lList.Join(db.WebWizardDetails,
                x => x.WebWizardId,
                y => y.WebWizardId,
               (x, y) => new { x.Id, x.Status, x.WebWizardId, x.FirstName, x.LastName, x.Email, x.PostDate, x.BidContent, x.BidAmount, y.WebWizardProfileImageUrl }).ToList();


                return ProposalList;
            }
        }

        public Data.Entities.WebWizardPortfolio GetSingleProjectById(int id)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var single = db.WebWizardPortfolio.SingleOrDefault(x => x.Id == id);
                return single;
            }
        }

        public ViewDetailsByBidIdModel GetViewDetailsByBidId(int Id)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                //var wizardBid = db.WebWizardBid.SingleOrDefault(x=>x.Id==Id);

                var lList = db.WebWizardBid.Join(db.WebWizardRegistration,
                 x => x.WebWizardId,
                 y => y.WebWizardId,
                (x, y) => new {x.Id, y.WebWizardId, y.FirstName, y.LastName, y.Email, x.PostDate, x.BidContent, x.Status, x.BidAmount, x.NewsfeedId }).ToList();

                var ProposalList = lList.Join(db.WebWizardDetails,
                x => x.WebWizardId,
                y => y.WebWizardId,
               (x, y) => new { x.Id, x.Status, x.WebWizardId, x.FirstName, x.LastName, x.Email, x.PostDate, x.BidContent, x.BidAmount, y.WebWizardProfileImageUrl,x.NewsfeedId }).ToList();

                var details = ProposalList.Join(db.NewsFeed,
                x => x.NewsfeedId,
                y => y.Id,
               (x, y) => new { x.Id, x.Status, x.WebWizardId, x.FirstName, x.LastName, x.Email, x.PostDate, x.BidContent, x.BidAmount, x.WebWizardProfileImageUrl, x.NewsfeedId,y.Amount }).ToList();

                var detail = details.SingleOrDefault(x => x.Id == Id);

                ViewDetailsByBidIdModel obj = new ViewDetailsByBidIdModel();
                obj.Id = detail.Id;
                obj.WizardId = detail.WebWizardId;
                obj.WizardImagePath = detail.WebWizardProfileImageUrl;
                obj.NewsFeedId = detail.NewsfeedId;
                obj.NewsFeedTitle = detail.BidContent;
                obj.BidAmount = detail.Amount;
                obj.ProposalAmount = detail.BidAmount;
                obj.WizardEmail = detail.Email;
                obj.WizardFarstName = detail.FirstName;
                obj.WizardLastName = detail.LastName;


                return obj;
            }
        }

        public ClientDetails UpdateClientDetails(ClientDetails oldClientDetail, ClientDetailsModel clientDetails)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var updateDetails = db.ClientDetails.SingleOrDefault(x => x.ClientId == oldClientDetail.ClientId);
                updateDetails.AboutClient = clientDetails.AboutClient;
                //updateDetails.ClientId = clientDetails.ClientId;
                updateDetails.ClientMobileNo = clientDetails.ClientMobileNo;
                updateDetails.CreateBy = clientDetails.ClientId;
                updateDetails.CreateDate = clientDetails.CreateDate;
                updateDetails.UpdateBy = clientDetails.ClientId;
                updateDetails.UpdateDate = DateTime.Now;
                updateDetails.DateOfBarth = clientDetails.DateOfBarth;
                updateDetails.DesignationId = null;
                updateDetails.EducationId = clientDetails.EducationId;
                updateDetails.LocationId = null;
                updateDetails.Status = true;
                db.SaveChanges();
                return updateDetails;
            }
        }

        public ClientDetails UpdateClientProfilePicture(ClientDetails oldClientDetails, int clientId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var oldProfilePicture = db.ClientDetails.SingleOrDefault(x => x.ClientId == clientId);
                oldProfilePicture.ClientProfileImageUrl = oldClientDetails.ClientProfileImageUrl;
                db.SaveChanges();
                return oldProfilePicture;
            }
        }
    }
}
