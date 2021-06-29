using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Repository;
using Wizard.Model.WebWizard;
using Wizard.Models;

namespace Wizard.Service.WebWizard
{
    public interface IWebWizardLogInService
    {
        WebWizardRegisterModel GetLogIn(LogInModel logIn);
    }
    public class WebWizardLogInService : IWebWizardLogInService
    {
        private IWebWizardLogInRepository _webWizardLogInRepository;
        public WebWizardLogInService(WebWizardLogInRepository webWizardLogInRepository)
        {
            this._webWizardLogInRepository = webWizardLogInRepository;
        }

        public WebWizardRegisterModel GetLogIn(LogInModel logIn)
        {
            return _webWizardLogInRepository.GetLogIn(logIn);
        }
    }
}
