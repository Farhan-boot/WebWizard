using System;
using System.Collections.Generic;
using System.Text;
using Wizard.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Wizard.Model.WebWizard;
using Wizard.Data.Data;
using System.Linq;
using Wizard.Data.Data.Entities;
using Wizard.Data.CustomModel;

namespace WebWizard.Data.Repository
{
    public interface IWebWizardProfileRepository
    {
        IEnumerable<EducationModel> EducationList();
        IEnumerable<DesignationModel> DesignationList();
        IEnumerable<SkillModel> SkillList();
        Wizard.Data.Data.Entities.Location AddLocation(Wizard.Model.WebWizard.Location location);
        bool AddWebWizardSkills(List<int> Skills, int webWizardId);
        WebWizardDetailsModel AddWebWizardDetails(WebWizardDetailsModel webWizardDetails);
        Wizard.Data.Data.Entities.WebWizardDetails GetWebWizardDetailsByWebWizardId(int webWizardId);
        Wizard.Data.Data.Entities.WebWizardDetails UpdateWebWizardDetails(WebWizardDetailsModel newWebWizardDetail, Wizard.Data.Data.Entities.WebWizardDetails OldWebWizardDetail);
        Wizard.Data.Data.Entities.Designation GetWebWizardDesignationByDesignationId(int designationId);
        Wizard.Data.Data.Entities.Education GetWebWizardEducationByEducationId(int educationId);
        IEnumerable<Wizard.Data.Data.Entities.WebWizardSkills> GetWebWizardSkillsIdByWebWizardId(int webWizardId);
        IEnumerable<Wizard.Data.Data.Entities.Skills> GetSkillListBywebWizardSkillsId(IEnumerable<Wizard.Data.Data.Entities.WebWizardSkills> webWizardId);
        Wizard.Data.Data.Entities.WebWizardDetails AddWebWizardProfilePicture(Wizard.Data.Data.Entities.WebWizardDetails addWebWizardProfilePicture);
        List<Wizard.Data.Data.Entities.NewsFeed> GetNewsFeedListByWebWizardId(int WebWizardId);
        Wizard.Data.Data.Entities.NewsFeed DeleteWebWizardNewsFeed(int Id);
        List<ShowNewsFeedModel> NotificationListForWebWizard();
        List<Wizard.Data.Data.Entities.WebWizardBid> WebWizardBidList(int webWizardId);
        WorkOrderModel WorkOrder(int id,int webWizardId);
        Wizard.Data.Data.Entities.SubmitProject SubmitProject(SubmitProject submitProject);
        IEnumerable<Wizard.Data.Data.Entities.SubmitProject> GetSubmittedProjectByNewsFeedId(int NewsFeedId);
        IEnumerable<Wizard.Data.CustomModel.ClientAcceptedBidModel> GetNotificationByWebWizardId(int webWizardId);
    }

    public class WebWizardProfileRepository : IWebWizardProfileRepository
    {
        public WebWizardProfileRepository()
        {

        }
        private SqlConnection Conn;
        public IEnumerable<EducationModel> EducationList()
        {
            List<EducationModel> EducationList = new List<EducationModel>();
            string ConnStr =
                ConfigurationManager.ConnectionStrings["WebWizardConnection"].ConnectionString;
            Conn = new SqlConnection(ConnStr);
            try
            {
                Conn.Open();
                string sqlquery = "select * from Education";
                SqlCommand command = new SqlCommand(sqlquery, Conn);
                SqlDataReader sReader;
                sReader = command.ExecuteReader();
                while (sReader.Read())
                {
                    EducationModel Education = new EducationModel();
                    Education.Id = Convert.ToInt32(sReader["Id"].ToString());
                    Education.EducationName = sReader["EducationName"].ToString();
                    EducationList.Add(Education);
                }
            }
            catch (SqlException ex)
            {
            }
            finally
            {
                Conn.Close();
            }
            return EducationList;
        }

        public IEnumerable<DesignationModel> DesignationList()
        {
            List<DesignationModel> DesignationList = new List<DesignationModel>();
            string ConnStr =
                ConfigurationManager.ConnectionStrings["WebWizardConnection"].ConnectionString;
            Conn = new SqlConnection(ConnStr);
            try
            {
                Conn.Open();
                string sqlquery = "select * from Designation";
                SqlCommand command = new SqlCommand(sqlquery, Conn);
                SqlDataReader sReader;
                sReader = command.ExecuteReader();
                while (sReader.Read())
                {
                    DesignationModel Designation = new DesignationModel();
                    Designation.Id = Convert.ToInt32(sReader["Id"].ToString());
                    Designation.DesignationTitle = sReader["DesignationTitle"].ToString();
                    DesignationList.Add(Designation);
                }
            }
            catch (SqlException ex)
            {
            }
            finally
            {
                Conn.Close();
            }
            return DesignationList;
        }

        public IEnumerable<SkillModel> SkillList()
        {
            List<SkillModel> SkillList = new List<SkillModel>();
            string ConnStr =
                ConfigurationManager.ConnectionStrings["WebWizardConnection"].ConnectionString;
            Conn = new SqlConnection(ConnStr);
            try
            {
                Conn.Open();
                string sqlquery = "select * from Skills";
                SqlCommand command = new SqlCommand(sqlquery, Conn);
                SqlDataReader sReader;
                sReader = command.ExecuteReader();
                while (sReader.Read())
                {
                    SkillModel Skill = new SkillModel();
                    Skill.Id = Convert.ToInt32(sReader["Id"].ToString());
                    Skill.NameOfSkill = sReader["NameOfSkill"].ToString();
                    SkillList.Add(Skill);
                }
            }
            catch (SqlException ex)
            {
            }
            finally
            {
                Conn.Close();
            }
            return SkillList;
        }

        public Wizard.Data.Data.Entities.WebWizardDetails UpdateWebWizardDetails(WebWizardDetailsModel newWebWizardDetail, Wizard.Data.Data.Entities.WebWizardDetails OldWebWizardDetail)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var GetOldWebWizardDetail = db.WebWizardDetails.SingleOrDefault(x => x.WebWizardId == OldWebWizardDetail.WebWizardId);
                // update locetion
                var locetionUpdate = db.Location.SingleOrDefault(x => x.Id == GetOldWebWizardDetail.LocationId);
                locetionUpdate.Latitude = newWebWizardDetail.Latitude;
                locetionUpdate.Longitude = newWebWizardDetail.Longitude;
                db.SaveChanges();
                // update WebWizardSkills
                var removeSkills = db.WebWizardSkills.Where(x => x.WebWizardId == OldWebWizardDetail.WebWizardId).ToList();
                if (removeSkills != null)
                {
                    db.WebWizardSkills.RemoveRange(removeSkills);
                    db.SaveChanges();
                    foreach (int skillId in newWebWizardDetail.Skills)
                    {
                        Wizard.Data.Data.Entities.WebWizardSkills updateWebWizardSkills = new Wizard.Data.Data.Entities.WebWizardSkills();
                        updateWebWizardSkills.SkillId = skillId;
                        updateWebWizardSkills.WebWizardId = OldWebWizardDetail.WebWizardId;
                        db.WebWizardSkills.Add(updateWebWizardSkills);
                        db.SaveChanges();
                    }
                }
                //OldWebWizardDetail.Id = newWebWizardDetail.Id;
                GetOldWebWizardDetail.WebWizardId = newWebWizardDetail.WebWizardId;
                GetOldWebWizardDetail.AboutWebWizard = newWebWizardDetail.About;
                GetOldWebWizardDetail.DateOfBarth = Convert.ToDateTime(newWebWizardDetail.DateOfBarth.ToString("d"));
                GetOldWebWizardDetail.EducationId = newWebWizardDetail.Education;
                GetOldWebWizardDetail.LocationId = GetOldWebWizardDetail.LocationId;
                GetOldWebWizardDetail.WebWizardMobileNo = newWebWizardDetail.MobileNo;
                GetOldWebWizardDetail.WebWizardProfileImageUrl = GetOldWebWizardDetail.WebWizardProfileImageUrl;
                GetOldWebWizardDetail.ExperienceYearFrom = Convert.ToDateTime(newWebWizardDetail.ExperienceFrom.ToString("d"));
                GetOldWebWizardDetail.ExperienceYearTo = Convert.ToDateTime(newWebWizardDetail.ExperienceTo.ToString("d"));
                GetOldWebWizardDetail.DesignationId = newWebWizardDetail.Designation;
                GetOldWebWizardDetail.Status = true;
                GetOldWebWizardDetail.CreateDate = GetOldWebWizardDetail.CreateDate;
                GetOldWebWizardDetail.UpdateDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
                GetOldWebWizardDetail.CreateBy = OldWebWizardDetail.WebWizardId;
                GetOldWebWizardDetail.UpdateBy = OldWebWizardDetail.WebWizardId;
                db.SaveChanges();
                return GetOldWebWizardDetail;
            }
        }

        public Wizard.Data.Data.Entities.Location AddLocation(Wizard.Model.WebWizard.Location location)
        {
            Wizard.Data.Data.Entities.Location returnLocation = new Wizard.Data.Data.Entities.Location();
            using (WebWizardConnection db = new WebWizardConnection())
            {
                Wizard.Data.Data.Entities.Location EntitiesLocation = new Wizard.Data.Data.Entities.Location();
                EntitiesLocation.Latitude = location.Latitude;
                EntitiesLocation.Longitude = location.Longitude;
                db.Location.Add(EntitiesLocation);
                db.SaveChanges();
                //set locetion object
                returnLocation.Id = EntitiesLocation.Id;
                returnLocation.Latitude = EntitiesLocation.Latitude;
                returnLocation.Longitude = EntitiesLocation.Longitude;
            }
            return returnLocation;
        }

        public Wizard.Data.Data.Entities.WebWizardDetails GetWebWizardDetailsByWebWizardId(int webWizardId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                return db.WebWizardDetails.SingleOrDefault(x => x.WebWizardId == webWizardId);
            }
        }

        public bool AddWebWizardSkills(List<int> Skills, int webWizardId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                foreach (var Skill in Skills)
                {
                    Wizard.Data.Data.Entities.WebWizardSkills webWizardSkills = new Wizard.Data.Data.Entities.WebWizardSkills();
                    webWizardSkills.WebWizardId = webWizardId;
                    webWizardSkills.SkillId = Skill;
                    db.WebWizardSkills.Add(webWizardSkills);
                    db.SaveChanges();
                }
                return true;
            }
        }

        public WebWizardDetailsModel AddWebWizardDetails(WebWizardDetailsModel webWizardDetails)
        {
            Wizard.Data.Data.Entities.WebWizardDetails webWizardDetail = new Wizard.Data.Data.Entities.WebWizardDetails();
            webWizardDetail.Id = webWizardDetails.Id;
            webWizardDetail.WebWizardId = webWizardDetails.WebWizardId;
            webWizardDetail.AboutWebWizard = webWizardDetails.About;
            webWizardDetail.DateOfBarth = Convert.ToDateTime(webWizardDetails.DateOfBarth.ToString("d"));
            webWizardDetail.DesignationId = webWizardDetails.Designation;
            webWizardDetail.EducationId = webWizardDetails.Education;
            webWizardDetail.ExperienceYearFrom = Convert.ToDateTime(webWizardDetails.ExperienceFrom.ToString("d"));
            webWizardDetail.ExperienceYearTo = Convert.ToDateTime(webWizardDetails.ExperienceTo.ToString("d"));
            webWizardDetail.LocationId = webWizardDetails.LocationId;
            webWizardDetail.WebWizardMobileNo = webWizardDetails.MobileNo;
            webWizardDetail.WebWizardProfileImageUrl = webWizardDetails.ProfilePicture;
            webWizardDetail.Status = true;
            webWizardDetail.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
            webWizardDetail.UpdateDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
            webWizardDetail.CreateBy = webWizardDetail.WebWizardId;
            webWizardDetail.UpdateBy = webWizardDetail.WebWizardId;
            using (WebWizardConnection db = new WebWizardConnection())
            {
                try
                {
                    db.WebWizardDetails.Add(webWizardDetail);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {


                }

            }
            return webWizardDetails;
        }
        public Wizard.Data.Data.Entities.Designation GetWebWizardDesignationByDesignationId(int designationId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                return db.Designation.SingleOrDefault(x => x.Id == designationId);
            }
        }
        public Wizard.Data.Data.Entities.Education GetWebWizardEducationByEducationId(int educationId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                return db.Education.SingleOrDefault(x => x.Id == educationId);
            }
        }

        public IEnumerable<Wizard.Data.Data.Entities.WebWizardSkills> GetWebWizardSkillsIdByWebWizardId(int webWizardId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var webWizardSkills = db.WebWizardSkills.Where(x => x.WebWizardId == webWizardId).ToList();
                return webWizardSkills;
            }
        }
        public IEnumerable<Wizard.Data.Data.Entities.Skills> GetSkillListBywebWizardSkillsId(IEnumerable<Wizard.Data.Data.Entities.WebWizardSkills> webWizardId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                List<Wizard.Data.Data.Entities.Skills> SkillsList = new List<Wizard.Data.Data.Entities.Skills>();
                foreach (var wizardId in webWizardId)
                {
                    Wizard.Data.Data.Entities.Skills skill = new Wizard.Data.Data.Entities.Skills();
                    var nameOfSkill = db.Skills.Where(x => x.Id == wizardId.SkillId).ToList();
                    foreach (var dbSkill in nameOfSkill)
                    {
                        skill.Id = dbSkill.Id;
                        skill.NameOfSkill = dbSkill.NameOfSkill;
                    }
                    SkillsList.Add(skill);
                }
                return SkillsList;
            }
        }
        public Wizard.Data.Data.Entities.WebWizardDetails AddWebWizardProfilePicture(Wizard.Data.Data.Entities.WebWizardDetails addWebWizardProfilePicture)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var addProfilePicture = db.WebWizardDetails.SingleOrDefault(x => x.WebWizardId == addWebWizardProfilePicture.WebWizardId);
                addProfilePicture.WebWizardProfileImageUrl = addWebWizardProfilePicture.WebWizardProfileImageUrl;
                db.SaveChanges();
                return addProfilePicture;
            }
        }

        public List<Wizard.Data.Data.Entities.NewsFeed> GetNewsFeedListByWebWizardId(int WebWizardId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var NewsFeedList = db.NewsFeed.Where(x => x.UserId == WebWizardId && x.IsWebWizard == true).OrderByDescending(x => x.Id).ToList();

                return NewsFeedList;
            }
        }
        public Wizard.Data.Data.Entities.NewsFeed DeleteWebWizardNewsFeed(int Id)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var deletedItem = db.NewsFeed.SingleOrDefault(x => x.Id == Id);
                if (deletedItem != null)
                {
                    db.NewsFeed.Remove(deletedItem);
                    db.SaveChanges();
                }
                return deletedItem;
            }
        }

        public List<ShowNewsFeedModel> NotificationListForWebWizard()
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

                return showNewsFeedList.ToList();
            }


        }

        public List<WebWizardBid> WebWizardBidList(int webWizardId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var bidList = db.WebWizardBid.Where(x=>x.WebWizardId== webWizardId).ToList();
                return bidList;
            }
        }

        public WorkOrderModel WorkOrder(int id,int webWizardId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var newsFeed = db.NewsFeed.Where(x=>x.IsWebWizard==false).SingleOrDefault(x => x.Id == id);
                var webWizardBid = db.WebWizardBid.SingleOrDefault(x => x.NewsfeedId == newsFeed.Id&&x.WebWizardId== webWizardId);
                var clientReg = db.ClientRegistration.SingleOrDefault(x => x.ClientId == newsFeed.UserId);
                var clientDtls = db.ClientDetails.SingleOrDefault(x => x.ClientId == clientReg.ClientId);
                var address = db.Countries.SingleOrDefault(x => x.Id == clientReg.StateId);
                WorkOrderModel wom = new WorkOrderModel();
                wom.Id = webWizardBid.Id;
                wom.BidPrice = newsFeed.Amount;
                wom.NewsFeedId = newsFeed.Id;
                wom.ClientAddress = address.Name;
                wom.ClientGmail = clientReg.Email;
                wom.ClientId = clientReg.ClientId;
                wom.ClientImagePath = clientDtls.ClientProfileImageUrl;
                wom.ClientName = clientReg.FirstName+" "+ clientReg.LastName;
                wom.ClientNumber = clientDtls.ClientMobileNo;
                wom.Date = webWizardBid.PostDate;
                wom.ProjectType = newsFeed.ProjectType;
                wom.ProposalPrice = webWizardBid.BidAmount;
                return wom;
            }
        }

        public SubmitProject SubmitProject(SubmitProject submitProject)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                db.SubmitProject.Add(submitProject);
                db.SaveChanges();
                return submitProject;
            }
        }
  
        public IEnumerable<SubmitProject> GetSubmittedProjectByNewsFeedId(int NewsFeedId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var getSubmittedProject = db.SubmitProject.Where(x => x.NewsFeedId == NewsFeedId).ToList();
                return getSubmittedProject;
            }
        }

        public IEnumerable<ClientAcceptedBidModel> GetNotificationByWebWizardId(int webWizardId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                // var notifications = db.WebWizardBid.Where(x => x.WebWizardId == webWizardId && x.Status == true).ToList();
                List<ClientAcceptedBidModel> clientAcceptedBidModel = new List<ClientAcceptedBidModel>();

                var result = db.WebWizardBid.Join(db.NewsFeed,
                 x => x.NewsfeedId,
                 y => y.Id,
                 (x, y) => new {x.Id, x.NewsfeedId, x.WebWizardId,y.UserId, x.BidAmount, x.BidContent, x.Status,y.Amount }).Where(x => x.WebWizardId == webWizardId && x.Status == true).ToList();

                var result2 = result.Join(db.ClientRegistration,
                x => x.UserId,
                y => y.ClientId,
                (x, y) => new {x.Id, x.NewsfeedId,x.UserId, x.WebWizardId, x.BidAmount, x.BidContent, x.Status, x.Amount,y.FirstName,y.LastName,y.Email }).ToList();

                var result3 = result2.Join(db.ClientDetails,
                 x => x.UserId,
                 y => y.ClientId,
                (x, y) => new {x.Id, x.NewsfeedId, x.UserId, x.WebWizardId, x.BidAmount, x.BidContent, x.Status, x.Amount, x.FirstName, x.LastName, x.Email,y.ClientProfileImageUrl }).ToList();

                foreach (var acceptedBid in result3)
                {
                    ClientAcceptedBidModel obj = new ClientAcceptedBidModel();
                    obj.Id = acceptedBid.Id;
                    obj.BidAmount = acceptedBid.BidAmount;
                    obj.BidContent = acceptedBid.BidContent;
                    obj.ClientEmail = acceptedBid.Email;
                    obj.ClientId = acceptedBid.UserId;
                    obj.ClientImagePath = acceptedBid.ClientProfileImageUrl;
                    obj.FullName = acceptedBid.FirstName+" "+ acceptedBid.LastName;
                    obj.NewsfeedId = acceptedBid.NewsfeedId;
                    obj.Status= acceptedBid.Status;
                    obj.WebWizardId = acceptedBid.WebWizardId;
                    clientAcceptedBidModel.Add(obj);
                }




                return clientAcceptedBidModel;
            }
        }
    }
}
