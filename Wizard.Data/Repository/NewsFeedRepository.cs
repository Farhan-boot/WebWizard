using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Data;
using Wizard.Data.Data.Entities;
using Wizard.Model.Clients;
using Wizard.Model.WebWizard;
using Wizard.Models;

namespace Wizard.Data.Repository
{
    public interface INewsFeedRepository
    {
        Data.Entities.NewsFeed AddWebWizardNewsFeed(NewsFeedModel newsFeedModel);
        NewsFeed AddClientNewsFeed(NewsFeedModel newsFeedModel);
        List<ShowNewsFeedModel> GetNewsFeed();
        List<ShowNewsFeedModel> GetNewsFeedForClient();
        NewsFeedCounter GetNewsFeedCounter();
        List<ShowAllWizardExceptMeModel> GetAllWizardExceptMe(int webWizardId);
        IEnumerable<Data.Entities.WebWizardPortfolio> GetAllWizardProjecExceptMyProjecs(int webWizardId);
        List<ShowAllClientModel> GetAllClient(int clientId);
        Wizard.Data.Data.Entities.WebWizardBid AddBidForClient(Wizard.Data.Data.Entities.WebWizardBid newsFeedBid);
        Wizard.Data.Data.Entities.NewsFeed ClientNewsFeedDetails(int id);
        Wizard.Data.Data.Entities.WebWizardBid UpdateNewsFeedBidForClient(Wizard.Data.Data.Entities.WebWizardBid newsFeedBid);
    }

    public class NewsFeedRepository : INewsFeedRepository
    {
        public NewsFeed AddClientNewsFeed(NewsFeedModel newsFeedModel)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                Data.Entities.NewsFeed newsObject = new NewsFeed();
                newsObject.IsWebWizard = false;
                newsObject.PostContent = newsFeedModel.Content;
                newsObject.Title = newsFeedModel.Title;
                newsObject.UserId = newsFeedModel.UserId;
                newsObject.PostDate = DateTime.Now;
                newsObject.ProjectType = newsFeedModel.ProjectType;
                newsObject.BackendLanguage = newsFeedModel.BackendLanguage;
                newsObject.RunOnServer = newsFeedModel.RunOnServer;
                newsObject.Technology = newsFeedModel.Technology;
                newsObject.GroupName = newsFeedModel.GroupName;
                newsObject.Amount = newsFeedModel.YourAmount;
                db.NewsFeed.Add(newsObject);
                db.SaveChanges();
                return newsObject;
            }
        }

        public NewsFeed AddWebWizardNewsFeed(NewsFeedModel newsFeedModel)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                Data.Entities.NewsFeed newsObject = new NewsFeed();
                newsObject.IsWebWizard = true;
                newsObject.PostContent = newsFeedModel.Content;
                newsObject.Title = newsFeedModel.Title;
                newsObject.UserId = newsFeedModel.UserId;
                newsObject.PostDate = DateTime.Now;
                db.NewsFeed.Add(newsObject);
                db.SaveChanges();
                return newsObject;
            }
        }

        public List<ShowAllClientModel> GetAllClient(int clientId)
        {
            List<ShowAllClientModel> showAllClientList = new List<ShowAllClientModel>();
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var allClient = db.ClientDetails.Join(db.ClientRegistration,
                 x => x.ClientId,
                 y => y.ClientId,
                (x, y) => new { y.FirstName, y.LastName, y.Email, x.ClientProfileImageUrl, x.AboutClient, x.ClientMobileNo, y.ClientId }).ToList();

                foreach (var allClientOneByOne in allClient)
                {
                    ShowAllClientModel ShowAllClientModelObj = new ShowAllClientModel();
                    ShowAllClientModelObj.ClientId = allClientOneByOne.ClientId;
                    ShowAllClientModelObj.FirstName = allClientOneByOne.FirstName;
                    ShowAllClientModelObj.LastName = allClientOneByOne.LastName;
                    ShowAllClientModelObj.ClientEmail = allClientOneByOne.Email;
                    if (allClientOneByOne.ClientProfileImageUrl == null || allClientOneByOne.ClientProfileImageUrl == "")
                    {
                        ShowAllClientModelObj.ClientProfileImageUrl = "user-demo.png";
                    }
                    else
                    {
                        ShowAllClientModelObj.ClientProfileImageUrl = allClientOneByOne.ClientProfileImageUrl;
                    }
                    ShowAllClientModelObj.AboutClient = allClientOneByOne.AboutClient;
                    ShowAllClientModelObj.ClientMobileNo = allClientOneByOne.ClientMobileNo;
                    showAllClientList.Add(ShowAllClientModelObj);
                }
                return showAllClientList.ToList();
            }
        }

        public List<ShowAllWizardExceptMeModel> GetAllWizardExceptMe(int webWizardId)
        {
            List<ShowAllWizardExceptMeModel> showAllWizardExceptMeList = new List<ShowAllWizardExceptMeModel>();
            using (WebWizardConnection db = new WebWizardConnection())
            {

                var allWizardExceptMe = db.WebWizardRegistration.Join(db.WebWizardDetails,
                 x => x.WebWizardId,
                 y => y.WebWizardId,
                (x, y) => new { x.FirstName, x.LastName, x.Email, y.WebWizardProfileImageUrl, y.AboutWebWizard, y.WebWizardMobileNo, y.ExperienceYearFrom, y.ExperienceYearTo, y.WebWizardId }).Where(x => x.WebWizardId != webWizardId).ToList();

                foreach (var allWizardExceptMeOneByOne in allWizardExceptMe)
                {
                    ShowAllWizardExceptMeModel ShowAllWizardExceptMeModelObj = new ShowAllWizardExceptMeModel();
                    ShowAllWizardExceptMeModelObj.WebWizardId = allWizardExceptMeOneByOne.WebWizardId;
                    ShowAllWizardExceptMeModelObj.Email = allWizardExceptMeOneByOne.Email;
                    ShowAllWizardExceptMeModelObj.FirstName = allWizardExceptMeOneByOne.FirstName;
                    ShowAllWizardExceptMeModelObj.LastName = allWizardExceptMeOneByOne.LastName;
                    if (allWizardExceptMeOneByOne.WebWizardProfileImageUrl == null || allWizardExceptMeOneByOne.WebWizardProfileImageUrl == "")
                    {
                        ShowAllWizardExceptMeModelObj.WebWizardProfileImageUrl = "user-demo.png";
                    }
                    else
                    {
                        ShowAllWizardExceptMeModelObj.WebWizardProfileImageUrl = allWizardExceptMeOneByOne.WebWizardProfileImageUrl;
                    }
                    ShowAllWizardExceptMeModelObj.AboutWebWizard = allWizardExceptMeOneByOne.AboutWebWizard;
                    ShowAllWizardExceptMeModelObj.WebWizardMobileNo = allWizardExceptMeOneByOne.WebWizardMobileNo;
                    ShowAllWizardExceptMeModelObj.ExperienceYearFrom = allWizardExceptMeOneByOne.ExperienceYearFrom;
                    ShowAllWizardExceptMeModelObj.ExperienceYearTo = allWizardExceptMeOneByOne.ExperienceYearTo;
                    showAllWizardExceptMeList.Add(ShowAllWizardExceptMeModelObj);
                }

                return showAllWizardExceptMeList.ToList();
            }
        }

        public IEnumerable<Data.Entities.WebWizardPortfolio> GetAllWizardProjecExceptMyProjecs(int webWizardId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var AllWizardProjecExceptMyProjecs = db.WebWizardPortfolio.Where(x => x.WebWizardId != webWizardId).ToList();
                return AllWizardProjecExceptMyProjecs.OrderByDescending(x => x.Id).ToList();
            }
        }

        public List<ShowNewsFeedModel> GetNewsFeed()
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var result = db.WebWizardRegistration.Join(db.WebWizardDetails,
                  x => x.WebWizardId,
                  y => y.WebWizardId,
                 (x, y) => new { x.FirstName, x.LastName, x.Email, y.WebWizardProfileImageUrl, y.WebWizardId }).ToList();

                var NewsFeed = result.Join(db.NewsFeed,
                           x => x.WebWizardId,
                           y => y.UserId,
                          (x, y) => new { x.FirstName, x.LastName, x.Email, x.WebWizardProfileImageUrl, x.WebWizardId, y.Title, y.PostDate, y.PostContent, y.IsWebWizard, y.ProjectType, y.BackendLanguage, y.RunOnServer, y.Technology, y.GroupName, y.Id }).ToList();


                List<ShowNewsFeedModel> showNewsFeedList = new List<ShowNewsFeedModel>();
                foreach (var newsFeed in NewsFeed)
                {
                    ShowNewsFeedModel showNewsFeedModel = new ShowNewsFeedModel();
                    showNewsFeedModel.Id = newsFeed.Id;
                    showNewsFeedModel.Email = newsFeed.Email;
                    showNewsFeedModel.FirstName = newsFeed.FirstName;
                    showNewsFeedModel.LastName = newsFeed.LastName;
                    showNewsFeedModel.IsWebWizard = newsFeed.IsWebWizard;
                    showNewsFeedModel.PostContent = newsFeed.PostContent;
                    showNewsFeedModel.PostDate = newsFeed.PostDate;
                    showNewsFeedModel.Title = newsFeed.Title;
                    showNewsFeedModel.WebWizardId = newsFeed.WebWizardId;

                    if (newsFeed.ProjectType != null)
                    {
                        showNewsFeedModel.ProjectType = newsFeed.ProjectType;
                    }
                    else
                    {
                        showNewsFeedModel.ProjectType = "***";
                    }
                    if (newsFeed.BackendLanguage != null)
                    {
                        showNewsFeedModel.BackendLanguage = newsFeed.BackendLanguage;
                    }
                    else
                    {
                        showNewsFeedModel.BackendLanguage = "***";
                    }
                    if (newsFeed.RunOnServer != null)
                    {
                        showNewsFeedModel.RunOnServer = newsFeed.RunOnServer;
                    }
                    else
                    {
                        showNewsFeedModel.RunOnServer = "***";
                    }
                    if (newsFeed.Technology != null)
                    {
                        showNewsFeedModel.Technology = newsFeed.Technology;
                    }
                    else
                    {
                        showNewsFeedModel.Technology = "***";
                    }
                    if (newsFeed.GroupName != null)
                    {
                        showNewsFeedModel.GroupName = newsFeed.GroupName;
                    }
                    else
                    {
                        showNewsFeedModel.GroupName = "***";
                    }

                    if (newsFeed.WebWizardProfileImageUrl == null || newsFeed.WebWizardProfileImageUrl == "")
                    {
                        showNewsFeedModel.WebWizardProfileImageUrl = "/Assets/WebWizardDashboard/ProfileImage/user-demo.png";
                    }
                    else
                    {
                        showNewsFeedModel.WebWizardProfileImageUrl = "/Assets/WebWizardDashboard/ProfileImage/" + newsFeed.WebWizardProfileImageUrl;
                    }
                    showNewsFeedList.Add(showNewsFeedModel);
                }

                return showNewsFeedList.OrderByDescending(x => x.Id).ToList();
            }
        }
        public List<ShowNewsFeedModel> GetNewsFeedForClient()
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var result = db.ClientRegistration.Join(db.ClientDetails,
                  x => x.ClientId,
                  y => y.ClientId,
                 (x, y) => new { x.FirstName, x.LastName, x.Email, y.ClientProfileImageUrl, y.ClientId }).ToList();

                var NewsFeed = result.Join(db.NewsFeed,
                           x => x.ClientId,
                           y => y.UserId,
                          (x, y) => new { x.FirstName, x.LastName, x.Email, x.ClientProfileImageUrl, x.ClientId, y.Title, y.PostDate, y.PostContent, y.IsWebWizard, y.ProjectType, y.BackendLanguage, y.RunOnServer, y.Technology, y.GroupName, y.Amount, y.Id }).ToList();


                List<ShowNewsFeedModel> showNewsFeedList = new List<ShowNewsFeedModel>();
                foreach (var newsFeed in NewsFeed)
                {
                    ShowNewsFeedModel showNewsFeedModel = new ShowNewsFeedModel();
                    showNewsFeedModel.Id = newsFeed.Id;
                    showNewsFeedModel.Email = newsFeed.Email;
                    showNewsFeedModel.FirstName = newsFeed.FirstName;
                    showNewsFeedModel.LastName = newsFeed.LastName;
                    showNewsFeedModel.IsWebWizard = newsFeed.IsWebWizard;
                    showNewsFeedModel.PostContent = newsFeed.PostContent;
                    showNewsFeedModel.PostDate = newsFeed.PostDate;
                    showNewsFeedModel.Title = newsFeed.Title;
                    showNewsFeedModel.WebWizardId = newsFeed.ClientId;
                    showNewsFeedModel.Amount = newsFeed.Amount;

                    if (newsFeed.ProjectType != null)
                    {
                        showNewsFeedModel.ProjectType = newsFeed.ProjectType;
                    }
                    else
                    {
                        showNewsFeedModel.ProjectType = "***";
                    }
                    if (newsFeed.BackendLanguage != null)
                    {
                        showNewsFeedModel.BackendLanguage = newsFeed.BackendLanguage;
                    }
                    else
                    {
                        showNewsFeedModel.BackendLanguage = "***";
                    }
                    if (newsFeed.RunOnServer != null)
                    {
                        showNewsFeedModel.RunOnServer = newsFeed.RunOnServer;
                    }
                    else
                    {
                        showNewsFeedModel.RunOnServer = "***";
                    }
                    if (newsFeed.Technology != null)
                    {
                        showNewsFeedModel.Technology = newsFeed.Technology;
                    }
                    else
                    {
                        showNewsFeedModel.Technology = "***";
                    }
                    if (newsFeed.GroupName != null)
                    {
                        showNewsFeedModel.GroupName = newsFeed.GroupName;
                    }
                    else
                    {
                        showNewsFeedModel.GroupName = "***";
                    }

                    if (newsFeed.ClientProfileImageUrl == null || newsFeed.ClientProfileImageUrl == "")
                    {
                        showNewsFeedModel.WebWizardProfileImageUrl = "/ClientAssets/ClientDashboard/ProfileImage/user-demo.png";
                    }
                    else
                    {
                        showNewsFeedModel.WebWizardProfileImageUrl = "/ClientAssets/ClientDashboard/ProfileImage/" + newsFeed.ClientProfileImageUrl;
                    }
                    showNewsFeedList.Add(showNewsFeedModel);
                }

                return showNewsFeedList.OrderByDescending(x => x.Id).ToList();
            }
        }


        public NewsFeedCounter GetNewsFeedCounter()
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                int totalWizard = db.WebWizardRegistration.Count();
                int totalClient = db.ClientRegistration.Count();
                int totalProject = db.WebWizardPortfolio.Count();
                NewsFeedCounter newsFeedCounter = new NewsFeedCounter();
                newsFeedCounter.TotalWizard = totalWizard;
                newsFeedCounter.TotalClient = totalClient;
                newsFeedCounter.TotalProject = totalProject;
                return newsFeedCounter;
            }
        }

        public WebWizardBid AddBidForClient(WebWizardBid newsFeedBid)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
              var check=  db.WebWizardBid.SingleOrDefault(x => x.NewsfeedId == newsFeedBid.NewsfeedId && x.WebWizardId == newsFeedBid.WebWizardId&&x.Status==true);
                if (check!=null)
                {
                    return check;
                }
                else
                {
                    if (newsFeedBid.Id == 0)
                    {
                        newsFeedBid.PostDate = DateTime.Now;
                        newsFeedBid.Status = false;
                        db.WebWizardBid.Add(newsFeedBid);
                        db.SaveChanges();
                    }
                    else
                    {
                        var updateBid = db.WebWizardBid.SingleOrDefault(x => x.Id == newsFeedBid.Id);
                        updateBid.BidAmount = newsFeedBid.BidAmount;
                        updateBid.BidContent = newsFeedBid.BidContent;
                        db.SaveChanges();
                    }
                    return newsFeedBid;
                }

      
            }
        }

        public NewsFeed ClientNewsFeedDetails(int id)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var dtls = db.NewsFeed.SingleOrDefault(x => x.Id == id);
                return dtls;
            }
        }

        public WebWizardBid UpdateNewsFeedBidForClient(WebWizardBid newsFeedBid)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var updateBid = db.WebWizardBid.SingleOrDefault(x => x.NewsfeedId == newsFeedBid.NewsfeedId && x.WebWizardId == newsFeedBid.WebWizardId && x.Status == false);
                return updateBid;
            }
        }
    }
}
