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
    public interface IClientRegistrationRepository
    {
        ClientRegistration ClientRegistration(ClientRegistration clientRegistration);
        Wizard.Data.Data.Entities.ClientRegistration GetClientByEmail(string email);
        Wizard.Data.Data.Entities.ClientRegistration GetRegisteredClientByEmail(string email, string password);
    }

    public class ClientRegistrationRepository : IClientRegistrationRepository
    {
        public ClientRegistration ClientRegistration(ClientRegistration clientRegistration)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var clientId =  Wizard.Data.KeyGenerator.PrimaryKey.GetKey();
                clientRegistration.ClientId = clientId.SetPrimaryKey;

                db.ClientRegistration.Add(clientRegistration);
                db.SaveChanges();
                return clientRegistration;
            }
        }

        public ClientRegistration GetClientByEmail(string email)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var myEmail= db.ClientRegistration.SingleOrDefault(x => x.Email == email);
                return myEmail;
            }
        }

        public ClientRegistration GetRegisteredClientByEmail(string email, string password)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
               var update= db.ClientRegistration.SingleOrDefault(x=>x.Email==email);
                update.Password = password;
                db.SaveChanges();
                return update;
            }
        }
    }
}
