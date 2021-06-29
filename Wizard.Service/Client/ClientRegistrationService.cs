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
    public interface IClientRegistrationService
    {
        ClientRegistration ClientRegistration(ClientRegistration clientRegistration);
        Wizard.Data.Data.Entities.ClientRegistration GetClientByEmail(string email);
        Wizard.Data.Data.Entities.ClientRegistration GetRegisteredClientByEmail(string email, string password);
    }
    public class ClientRegistrationService : IClientRegistrationService
    {
        private IClientRegistrationRepository _clientRegistrationRepository;
        public ClientRegistrationService(ClientRegistrationRepository clientRegistrationRepository)
        {
            this._clientRegistrationRepository = clientRegistrationRepository;
        }

        public ClientRegistration ClientRegistration(ClientRegistration clientRegistration)
        {
            return this._clientRegistrationRepository.ClientRegistration(clientRegistration);
        }

        public ClientRegistration GetClientByEmail(string email)
        {
            return this._clientRegistrationRepository.GetClientByEmail(email);
        }

        public ClientRegistration GetRegisteredClientByEmail(string email, string password)
        {
            return this._clientRegistrationRepository.GetRegisteredClientByEmail(email, password);
        }
    }
}
