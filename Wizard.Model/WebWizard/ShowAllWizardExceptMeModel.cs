using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Model.WebWizard
{
    public class ShowAllWizardExceptMeModel
    {
        public int WebWizardId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WebWizardProfileImageUrl { get; set; }
        public string AboutWebWizard { get; set; }
        public string Email { get; set; }
        public string WebWizardMobileNo { get; set; }
        public DateTime? ExperienceYearFrom { get; set; }
        public DateTime? ExperienceYearTo { get; set; }
    }
}
