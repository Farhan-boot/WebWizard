using System;
using System.Collections.Generic;
using System.Text;
using WebWizard.Data.Repository;
using Wizard.Models;
using System.Data.SqlClient;
using Wizard.Model.WebWizard;
using Wizard.Data.Data.Entities;

namespace WebWizard.Service.WebWizard
{
    public interface IWebWizardRegisteredService
    {
        IEnumerable<Wizard.Data.Data.Entities.Countries> GetCountryList();
        IEnumerable<Wizard.Data.Data.Entities.TermsAndConditions> TermAndConditionList();
        WebWizardRegisterModel Save(WebWizardRegisterModel obj);
        bool GetByEmail(WebWizardRegisterModel WebWizardRegister);
        Wizard.Data.Data.Entities.WebWizardRegistration GetWebWizardRegistrationDetailByWebWizardId(int webWizardId);
        Wizard.Data.Data.Entities.Countries GetWebWizardLocationDetailByStateId(int stateId);
        Wizard.Data.Data.Entities.WebWizardRegistration UpdateAdvanced(int webWizardId, Wizard.Data.Data.Entities.WebWizardRegistration advancedSettings);
        Wizard.Data.Data.Entities.WebWizardRegistration GetWebWizardByEmail(string email);
        Wizard.Data.Data.Entities.WebWizardRegistration GetRegisteredWebWizardByEmail(string email,string password);
    }

    public class WebWizardRegisteredService : IWebWizardRegisteredService
    {
        private IWebWizardRegisteredRepository _webWizardRegisteredRepository;
        public WebWizardRegisteredService(WebWizardRegisteredRepository webWizardRegisteredRepository)
        {
            this._webWizardRegisteredRepository = webWizardRegisteredRepository;
        }

        public bool GetByEmail(WebWizardRegisterModel WebWizardRegister)
        {
            return _webWizardRegisteredRepository.GetByEmail(WebWizardRegister);
        }

        public WebWizardRegisterModel Save(WebWizardRegisterModel obj)
        {
            return _webWizardRegisteredRepository.Save(obj);
        }

        public IEnumerable<Wizard.Data.Data.Entities.TermsAndConditions> TermAndConditionList()
        {
            return _webWizardRegisteredRepository.TermAndConditionList();
        }

        IEnumerable<Wizard.Data.Data.Entities.Countries> IWebWizardRegisteredService.GetCountryList()
        {
            return _webWizardRegisteredRepository.GetCountryList();
        }

        public Wizard.Data.Data.Entities.WebWizardRegistration GetWebWizardRegistrationDetailByWebWizardId(int webWizardId)
        {
            return _webWizardRegisteredRepository.GetWebWizardRegistrationDetailByWebWizardId(webWizardId);
        }

        public Wizard.Data.Data.Entities.Countries GetWebWizardLocationDetailByStateId(int stateId)
        {
            return _webWizardRegisteredRepository.GetWebWizardLocationDetailByStateId(stateId);
        }

        public Wizard.Data.Data.Entities.WebWizardRegistration UpdateAdvanced(int webWizardId, Wizard.Data.Data.Entities.WebWizardRegistration advancedSettings)
        {
            return _webWizardRegisteredRepository.UpdateAdvanced(webWizardId, advancedSettings);
        }

        public WebWizardRegistration GetWebWizardByEmail(string email)
        {
            return _webWizardRegisteredRepository.GetWebWizardByEmail(email);
        }

        public WebWizardRegistration GetRegisteredWebWizardByEmail(string email, string password)
        {
            return _webWizardRegisteredRepository.GetRegisteredWebWizardByEmail(email, password);
        }
    }
}
