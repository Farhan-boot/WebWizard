using System;
using System.Collections.Generic;
using System.Text;
using WebWizard.Data.Repository;
using Wizard.Models;
using System.Data.SqlClient;
using Wizard.Model.WebWizard;
using Wizard.Data.Data.Entities;
using Wizard.Data.CustomModel;

namespace WebWizard.Service.WebWizard
{
    public interface IWebWizardProfileService
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
        WorkOrderModel WorkOrder(int id, int webWizardId);
        Wizard.Data.Data.Entities.SubmitProject SubmitProject(SubmitProject submitProject);
        IEnumerable<Wizard.Data.Data.Entities.SubmitProject> GetSubmittedProjectByNewsFeedId(int NewsFeedId);
        IEnumerable<Wizard.Data.CustomModel.ClientAcceptedBidModel> GetNotificationByWebWizardId(int webWizardId);
    }

    public class WebWizardProfileService : IWebWizardProfileService
    {
        private IWebWizardProfileRepository _webWizardProfileRepository;
        public WebWizardProfileService(WebWizardProfileRepository webWizardProfileRepository)
        {
            this._webWizardProfileRepository = webWizardProfileRepository;
        }

        public Wizard.Data.Data.Entities.WebWizardDetails UpdateWebWizardDetails(WebWizardDetailsModel newWebWizardDetail, Wizard.Data.Data.Entities.WebWizardDetails OldWebWizardDetail)
        {
            return _webWizardProfileRepository.UpdateWebWizardDetails(newWebWizardDetail, OldWebWizardDetail);
        }

        public Wizard.Data.Data.Entities.Location AddLocation(Wizard.Model.WebWizard.Location location)
        {
            return _webWizardProfileRepository.AddLocation(location);
        }

        public Wizard.Data.Data.Entities.WebWizardDetails GetWebWizardDetailsByWebWizardId(int webWizardId)
        {
            return _webWizardProfileRepository.GetWebWizardDetailsByWebWizardId(webWizardId);
        }

        public WebWizardDetailsModel AddWebWizardDetails(WebWizardDetailsModel webWizardDetails)
        {
            return _webWizardProfileRepository.AddWebWizardDetails(webWizardDetails);
        }

        public bool AddWebWizardSkills(List<int> Skills, int webWizardId)
        {
            return _webWizardProfileRepository.AddWebWizardSkills(Skills, webWizardId);
        }

        public IEnumerable<DesignationModel> DesignationList()
        {
            return _webWizardProfileRepository.DesignationList();
        }

        public IEnumerable<EducationModel> EducationList()
        {
            return _webWizardProfileRepository.EducationList();
        }

        public IEnumerable<SkillModel> SkillList()
        {
            return _webWizardProfileRepository.SkillList();
        }

        public Wizard.Data.Data.Entities.Designation GetWebWizardDesignationByDesignationId(int designationId)
        {
            return _webWizardProfileRepository.GetWebWizardDesignationByDesignationId(designationId);
        }

        public Wizard.Data.Data.Entities.Education GetWebWizardEducationByEducationId(int educationId)
        {
            return _webWizardProfileRepository.GetWebWizardEducationByEducationId(educationId);
        }
        public IEnumerable<Wizard.Data.Data.Entities.WebWizardSkills> GetWebWizardSkillsIdByWebWizardId(int webWizardId)
        {
            return _webWizardProfileRepository.GetWebWizardSkillsIdByWebWizardId(webWizardId);
        }

        public IEnumerable<Wizard.Data.Data.Entities.Skills> GetSkillListBywebWizardSkillsId(IEnumerable<Wizard.Data.Data.Entities.WebWizardSkills> webWizardId)
        {
            return _webWizardProfileRepository.GetSkillListBywebWizardSkillsId(webWizardId);
        }

        public Wizard.Data.Data.Entities.WebWizardDetails AddWebWizardProfilePicture(Wizard.Data.Data.Entities.WebWizardDetails addWebWizardProfilePicture)
        {
            return _webWizardProfileRepository.AddWebWizardProfilePicture(addWebWizardProfilePicture);
        }

        public List<Wizard.Data.Data.Entities.NewsFeed> GetNewsFeedListByWebWizardId(int WebWizardId)
        {
            return _webWizardProfileRepository.GetNewsFeedListByWebWizardId(WebWizardId);
        }

        public Wizard.Data.Data.Entities.NewsFeed DeleteWebWizardNewsFeed(int Id)
        {
            return _webWizardProfileRepository.DeleteWebWizardNewsFeed(Id);
        }

        public List<ShowNewsFeedModel> NotificationListForWebWizard()
        {
            return _webWizardProfileRepository.NotificationListForWebWizard();
        }

        public List<WebWizardBid> WebWizardBidList(int webWizardId)
        {
            return _webWizardProfileRepository.WebWizardBidList(webWizardId);
        }

        public WorkOrderModel WorkOrder(int id, int webWizardId)
        {
            return _webWizardProfileRepository.WorkOrder(id,webWizardId);
        }

        public SubmitProject SubmitProject(SubmitProject SubmitProject)
        {
            return _webWizardProfileRepository.SubmitProject(SubmitProject);
        }

        public IEnumerable<SubmitProject> GetSubmittedProjectByNewsFeedId(int NewsFeedId)
        {
            return _webWizardProfileRepository.GetSubmittedProjectByNewsFeedId(NewsFeedId);
        }

        public IEnumerable<ClientAcceptedBidModel> GetNotificationByWebWizardId(int webWizardId)
        {
            return _webWizardProfileRepository.GetNotificationByWebWizardId(webWizardId);
        }
    }
}
