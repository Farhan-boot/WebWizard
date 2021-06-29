using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.CustomModel;
using Wizard.Data.Data.Entities;
using Wizard.Data.Repository;
using Wizard.Model.WebWizard;
using Wizard.Models;

namespace Wizard.Service.Admin
{
    public interface IAdminService
    {
        Wizard.Data.Data.Entities.AdminProfile LogIn(LogInModel logIn);
        Wizard.Data.Data.Entities.AdminProfile SaveProfilePicture(int adminId,string fileName);
        IEnumerable<Wizard.Data.Data.Entities.WebWizardPortfolio> GetWizardPortfolioListbyStatus();
        Wizard.Data.Data.Entities.WebWizardPortfolio AcceptedRequest(int Id);
    }
    public class AdminService : IAdminService
    {
        private IAdminRepository _adminRepository;
        public AdminService(AdminRepository adminRepository)
        {
            this._adminRepository = adminRepository;
        }

        public Data.Data.Entities.WebWizardPortfolio AcceptedRequest(int Id)
        {
            return _adminRepository.AcceptedRequest(Id);
        }

        public IEnumerable<Data.Data.Entities.WebWizardPortfolio> GetWizardPortfolioListbyStatus()
        {
            return _adminRepository.GetWizardPortfolioListbyStatus();
        }

        public AdminProfile LogIn(LogInModel logIn)
        {
            return _adminRepository.LogIn(logIn);
        }

        public AdminProfile SaveProfilePicture(int adminId, string fileName)
        {
            return _adminRepository.SaveProfilePicture(adminId, fileName);
        }
    }
}
