using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Data;
using Wizard.Data.Data.Entities;
using Wizard.Model.WebWizard;
using Wizard.Models;

namespace Wizard.Data.Repository
{
    public interface IWebWizardPortfolioRepository
    {
        IEnumerable<ProjectType> ListOfProjectType();
        IEnumerable<Technology> ListOfTechnology();
        IEnumerable<BackendLanguage> ListOfBackendlanguage();
        IEnumerable<RunOnServer> ListOfRunOnServer();
        Data.Entities.WebWizardPortfolio AddPortfolio(Model.WebWizard.WebWizardPortfolio webWizardPortfolio);
        IEnumerable<Data.Entities.WebWizardPortfolio> GetWebWizardPortfolioListByWizardId(int webWizardId);
    }

    public class WebWizardPortfolioRepository : IWebWizardPortfolioRepository
    {
        public Data.Entities.WebWizardPortfolio AddPortfolio(Model.WebWizard.WebWizardPortfolio webWizardPortfolio)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                Data.Entities.WebWizardPortfolio portfolioObject = new Data.Entities.WebWizardPortfolio();
                portfolioObject.WebWizardId = webWizardPortfolio.WebWizardId;
                portfolioObject.ProjectTitle = webWizardPortfolio.ProjectTitle;
                portfolioObject.ProjectDescription = webWizardPortfolio.ProjectDescription;
                portfolioObject.ProjectSize = webWizardPortfolio.ProjectSize;
                portfolioObject.ProjectZipFilePath = webWizardPortfolio.ProjectZipFilePath;
                portfolioObject.ProjectImagePath = webWizardPortfolio.ProjectImagePath;
                portfolioObject.IsPublishNow = webWizardPortfolio.IsPublishNow;
                portfolioObject.IsFreeDownload = webWizardPortfolio.IsFreeDownload;
                portfolioObject.IsOnlyRegisteredUserCanSee = webWizardPortfolio.IsOnlyRegisteredUserCanSee;
                portfolioObject.TechnologyId = webWizardPortfolio.TechnologyId;
                portfolioObject.ProjectTypeId = webWizardPortfolio.ProjectTypeId;
                portfolioObject.BackendLanguageId = webWizardPortfolio.BackendLanguageId;
                portfolioObject.RunOnServerId = webWizardPortfolio.RunOnServerId;
                portfolioObject.LiveDemoLink = webWizardPortfolio.LiveDemoLink;
                portfolioObject.Status = webWizardPortfolio.Status;

                portfolioObject.CreateDate =Convert.ToDateTime(DateTime.Now.ToString("d"));
                portfolioObject.UpdateDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
                portfolioObject.CreateBy = webWizardPortfolio.WebWizardId;
                portfolioObject.UpdateBy = webWizardPortfolio.WebWizardId;
                portfolioObject.IsDelete = webWizardPortfolio.IsDelete;
                //save data
                db.WebWizardPortfolio.Add(portfolioObject);
                db.SaveChanges();
                return portfolioObject;
            }
        }

        public IEnumerable<Data.Entities.WebWizardPortfolio> GetWebWizardPortfolioListByWizardId(int webWizardId)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                return db.WebWizardPortfolio.Where(x => x.WebWizardId == webWizardId && x.IsDelete ==false&&x.Status==true).OrderByDescending(x=>x.Id).ToList();
            }
        }

        public IEnumerable<BackendLanguage> ListOfBackendlanguage()
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                return db.BackendLanguage.ToList();
            }
        }

        public IEnumerable<ProjectType> ListOfProjectType()
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                return db.ProjectType.ToList();
            }
        }

        public IEnumerable<RunOnServer> ListOfRunOnServer()
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                return db.RunOnServer.ToList();
            }
        }

        public IEnumerable<Technology> ListOfTechnology()
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                return db.Technology.ToList();
            }
        }
    }
}
