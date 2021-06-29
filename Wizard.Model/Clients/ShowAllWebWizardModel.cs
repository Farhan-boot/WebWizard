using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Model.Clients
{
    public class ShowAllWebWizardModel
    {
        public int WebWizardId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NameTitle { get; set; }
        public string Email { get; set; }
        public int StateId { get; set; }
        public string Password { get; set; }
        public bool StartAsCompany { get; set; }
        public int NoOfEmployees { get; set; }
        public bool StartAsFreelancer { get; set; }
        public int TermsAndConditionsId { get; set; }
        public string VerificationCode { get; set; }
        public bool Status { get; set; }
        public string AboutWebWizard { get; set; }
        public DateTime? DateOfBarth { get; set; }
        public int EducationId { get; set; }
        public int LocationId { get; set; }
        public string WebWizardMobileNo { get; set; }
        public string WebWizardProfileImageUrl { get; set; }
        public DateTime? ExperienceYearFrom { get; set; }
        public DateTime? ExperienceYearTo { get; set; }
        public int DesignationId { get; set; }
    }
}
