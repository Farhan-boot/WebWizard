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
    public interface IAdminRepository
    {
        Wizard.Data.Data.Entities.AdminProfile LogIn(LogInModel logIn);
        Wizard.Data.Data.Entities.AdminProfile SaveProfilePicture(int adminId, string fileName);
        IEnumerable<Wizard.Data.Data.Entities.WebWizardPortfolio> GetWizardPortfolioListbyStatus();
        Wizard.Data.Data.Entities.WebWizardPortfolio AcceptedRequest(int Id);
    }

    public class AdminRepository : IAdminRepository
    {
        public Data.Entities.WebWizardPortfolio AcceptedRequest(int Id)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var request = db.WebWizardPortfolio.SingleOrDefault(x=>x.Id==Id);
                request.Status = true;
                db.SaveChanges();
                return request;
            }
        }

        public IEnumerable<Data.Entities.WebWizardPortfolio> GetWizardPortfolioListbyStatus()
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var wizardPortfolioList = db.WebWizardPortfolio.Where(x=>x.Status==false).ToList();
                return wizardPortfolioList;
            }
        }

        public AdminProfile LogIn(LogInModel logIn)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var log = db.AdminProfile.SingleOrDefault(x => x.Email == logIn.Email && x.Password == logIn.Password);
                return log;
            }
        }

        public AdminProfile SaveProfilePicture(int adminId, string fileName)
        {
            using (WebWizardConnection db = new WebWizardConnection())
            {
                var adminDtls = db.AdminProfile.SingleOrDefault(x=>x.Id==adminId);
                adminDtls.ImageUrl = fileName;
                db.SaveChanges();
                return adminDtls;
            }
        }
    }
}
