using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Repository;
using Wizard.Model.WebWizard;
using Wizard.Models;

namespace Wizard.Service.Search
{
    public interface IClientLogInService
    {
        ClientRegisterModel GetLogIn(LogInModel logIn);
    }
    public class ClientLogInService : IClientLogInService
    {
        private IClientLogInRepository _clientLogInRepository;
        public ClientLogInService(ClientLogInRepository clientLogInRepository)
        {
            this._clientLogInRepository = clientLogInRepository;
        }

        public ClientRegisterModel GetLogIn(LogInModel logIn)
        {
            return _clientLogInRepository.GetLogIn(logIn);
        }
    }
}
