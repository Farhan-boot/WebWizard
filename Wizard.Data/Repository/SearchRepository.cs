using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Data;
using Wizard.Model.Search;
using Wizard.Model.WebWizard;
using Wizard.Models;

namespace Wizard.Data.Repository
{
    public interface ISearchRepository
    {
        List<ShowWebWizardListModel> GetWebWizardListForSearch();
        List<ShowWebWizardListModel> GetClientsListForSearch();
        List<ShowProjectListForSearchModel> GetProjectsListForSearch();
    }

    public class SearchRepository : ISearchRepository
    {
        public List<ShowWebWizardListModel> GetClientsListForSearch()
        {
            List<ShowWebWizardListModel> ShowClientList = new List<ShowWebWizardListModel>();
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var result = db.ClientRegistration.Join(db.ClientDetails,
                  x => x.ClientId,
                  y => y.ClientId,
                 (x, y) => new { x.FirstName, x.LastName, x.Email, x.StateId, y.ClientProfileImageUrl, y.DateOfBarth, y.EducationId, y.AboutClient, y.ClientId }).ToList();

                var clients = result.Join(db.Education,
                           x => x.EducationId,
                           y => y.Id,
                          (x, y) => new { x.FirstName, x.LastName, x.Email, x.AboutClient, x.ClientProfileImageUrl, x.DateOfBarth, x.ClientId, x.StateId, y.Id, y.EducationName }).ToList();

                var clientsList = clients.Join(db.Countries,
                           x => x.StateId,
                           y => y.Id,
                          (x, y) => new { x.FirstName, x.LastName, x.AboutClient, x.Email, x.ClientProfileImageUrl, x.DateOfBarth, x.ClientId, x.StateId, x.EducationName, y.Name, y.PhoneCode }).ToList();

                foreach (var client in clientsList)
                {
                    ShowWebWizardListModel obj = new ShowWebWizardListModel();
                    obj.AboutWebWizard = client.AboutClient;
                    obj.DateOfBarth = client.DateOfBarth;
                    obj.EducationName = client.EducationName;
                    obj.Email = client.Email;
                    obj.FirstName = client.FirstName;
                    obj.LastName = client.LastName;
                    obj.Name = client.Name;
                    obj.PhoneCode = client.PhoneCode;
                    obj.StateId = client.StateId;
                    obj.WebWizardId = client.ClientId;
                    obj.WebWizardProfileImageUrl = client.ClientProfileImageUrl;
                    ShowClientList.Add(obj);
                }


                return ShowClientList;
            }
        }

        public List<ShowProjectListForSearchModel> GetProjectsListForSearch()
        {
            List<ShowProjectListForSearchModel> projectList = new List<ShowProjectListForSearchModel>();
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var result = db.WebWizardPortfolio.Join(db.Technology,
                  x => x.TechnologyId,
                  y => y.Id,
                 (x, y) => new {x.Id,x.IsDelete,x.CreateDate,x.WebWizardId,x.IsPublishNow,x.IsOnlyRegisteredUserCanSee, x.ProjectTitle, x.ProjectDescription, x.LiveDemoLink, x.ProjectImagePath,x.TechnologyId,x.ProjectTypeId,x.BackendLanguageId,x.RunOnServerId, y.NameOfTechnology}).ToList();
                var result2 = result.Join(db.ProjectType,
                           x => x.ProjectTypeId,
                           y => y.Id,
                          (x, y) => new { x.Id, x.IsDelete, x.CreateDate, x.WebWizardId, x.IsPublishNow, x.IsOnlyRegisteredUserCanSee, x.ProjectTitle, x.ProjectDescription, x.LiveDemoLink, x.ProjectImagePath, x.TechnologyId, x.ProjectTypeId, x.BackendLanguageId, x.RunOnServerId, x.NameOfTechnology,y.NameOfProject }).ToList();
                var result3 = result2.Join(db.BackendLanguage,
                           x => x.BackendLanguageId,
                           y => y.Id,
                          (x, y) => new { x.Id, x.IsDelete, x.CreateDate, x.WebWizardId, x.IsPublishNow, x.IsOnlyRegisteredUserCanSee, x.ProjectTitle, x.ProjectDescription, x.LiveDemoLink, x.ProjectImagePath, x.TechnologyId, x.ProjectTypeId, x.BackendLanguageId, x.RunOnServerId, x.NameOfTechnology, x.NameOfProject,y.NameOfBackendLanguage }).ToList();
                var ProjectList = result3.Join(db.RunOnServer,
                         x => x.RunOnServerId,
                         y => y.Id,
                        (x, y) => new { x.Id, x.IsDelete, x.CreateDate, x.WebWizardId, x.IsPublishNow, x.IsOnlyRegisteredUserCanSee, x.ProjectTitle, x.ProjectDescription, x.LiveDemoLink, x.ProjectImagePath, x.TechnologyId, x.ProjectTypeId, x.BackendLanguageId, x.RunOnServerId, x.NameOfTechnology, x.NameOfProject, x.NameOfBackendLanguage,y.NameOfServer }).ToList();

               var myProjectList= ProjectList.Where(x => x.IsOnlyRegisteredUserCanSee == false && x.IsPublishNow == true&&x.IsDelete==false).ToList();


                foreach (var Project in myProjectList)
                {
                    ShowProjectListForSearchModel obj = new ShowProjectListForSearchModel();
                    obj.Id = Project.Id;
                    obj.IsDelete = Project.IsDelete;
                    obj.CreateDate = Project.CreateDate;
                    obj.WebWizardId = Project.WebWizardId;
                    obj.IsPublishNow = Project.IsPublishNow;
                    obj.IsOnlyRegisteredUserCanSee = Project.IsOnlyRegisteredUserCanSee;
                    obj.ProjectTitle = Project.ProjectTitle;
                    obj.ProjectDescription = Project.ProjectDescription;
                    obj.LiveDemoLink = Project.LiveDemoLink;
                    obj.ProjectImagePath = Project.ProjectImagePath;
                    obj.Technology = Project.NameOfTechnology;
                    obj.ProjectType = Project.NameOfProject;
                    obj.BackendLanguage = Project.NameOfBackendLanguage;
                    obj.RunOnServer = Project.NameOfServer;
                    projectList.Add(obj);
                }
                return projectList;
            }
        }

        public List<ShowWebWizardListModel> GetWebWizardListForSearch()
        {
            List<ShowWebWizardListModel> ShowWebWizardList = new List<ShowWebWizardListModel>();
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var result = db.WebWizardRegistration.Join(db.WebWizardDetails,
                  x => x.WebWizardId,
                  y => y.WebWizardId,
                 (x, y) => new { x.FirstName, x.LastName, x.Email, x.StateId, y.WebWizardProfileImageUrl, y.DateOfBarth, y.EducationId, y.AboutWebWizard, y.WebWizardId }).ToList();

                var webWizard = result.Join(db.Education,
                           x => x.EducationId,
                           y => y.Id,
                          (x, y) => new { x.FirstName, x.LastName, x.Email, x.AboutWebWizard, x.WebWizardProfileImageUrl, x.DateOfBarth, x.WebWizardId, x.StateId, y.Id, y.EducationName }).ToList();

                var WizardList = webWizard.Join(db.Countries,
                           x => x.StateId,
                           y => y.Id,
                          (x, y) => new { x.FirstName, x.LastName, x.AboutWebWizard, x.Email, x.WebWizardProfileImageUrl, x.DateOfBarth, x.WebWizardId, x.StateId, x.EducationName, y.Name, y.PhoneCode }).ToList();

                foreach (var wizard in WizardList)
                {
                    ShowWebWizardListModel obj = new ShowWebWizardListModel();
                    obj.AboutWebWizard = wizard.AboutWebWizard;
                    obj.DateOfBarth = wizard.DateOfBarth;
                    obj.EducationName = wizard.EducationName;
                    obj.Email = wizard.Email;
                    obj.FirstName = wizard.FirstName;
                    obj.LastName = wizard.LastName;
                    obj.Name = wizard.Name;
                    obj.PhoneCode = wizard.PhoneCode;
                    obj.StateId = wizard.StateId;
                    obj.WebWizardId = wizard.WebWizardId;
                    obj.WebWizardProfileImageUrl = wizard.WebWizardProfileImageUrl;
                    ShowWebWizardList.Add(obj);
                }


                return ShowWebWizardList;
            }

        }
    }
}
