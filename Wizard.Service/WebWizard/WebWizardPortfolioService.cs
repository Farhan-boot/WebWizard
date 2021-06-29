using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Data.Entities;
using Wizard.Data.Repository;
using Wizard.Model.WebWizard;
using Wizard.Models;

namespace Wizard.Service.WebWizard
{
    public interface IWebWizardPortfolioService
    {
        IEnumerable<ProjectType> ListOfProjectType();
        IEnumerable<Technology> ListOfTechnology();
        IEnumerable<BackendLanguage> ListOfBackendlanguage();
        IEnumerable<RunOnServer> ListOfRunOnServer();
        Data.Data.Entities.WebWizardPortfolio AddPortfolio(Model.WebWizard.WebWizardPortfolio webWizardPortfolio);
        IEnumerable<Data.Data.Entities.WebWizardPortfolio> GetWebWizardPortfolioListByWizardId(int webWizardId);
    }
    public class WebWizardPortfolioService : IWebWizardPortfolioService
    {
        private IWebWizardPortfolioRepository _webWizardPortfolioRepository;
        public WebWizardPortfolioService(WebWizardPortfolioRepository webWizardPortfolioRepository)
        {
            this._webWizardPortfolioRepository = webWizardPortfolioRepository;
        }

        public Data.Data.Entities.WebWizardPortfolio AddPortfolio(Model.WebWizard.WebWizardPortfolio webWizardPortfolio)
        {
            return _webWizardPortfolioRepository.AddPortfolio(webWizardPortfolio);
        }

        public IEnumerable<Data.Data.Entities.WebWizardPortfolio> GetWebWizardPortfolioListByWizardId(int webWizardId)
        {
            return _webWizardPortfolioRepository.GetWebWizardPortfolioListByWizardId(webWizardId);
        }

        public IEnumerable<BackendLanguage> ListOfBackendlanguage()
        {
            return _webWizardPortfolioRepository.ListOfBackendlanguage();
        }

        public IEnumerable<ProjectType> ListOfProjectType()
        {
            return _webWizardPortfolioRepository.ListOfProjectType();
        }

        public IEnumerable<RunOnServer> ListOfRunOnServer()
        {
            return _webWizardPortfolioRepository.ListOfRunOnServer();
        }

        public IEnumerable<Technology> ListOfTechnology()
        {
            return _webWizardPortfolioRepository.ListOfTechnology();
        }
    }
}
