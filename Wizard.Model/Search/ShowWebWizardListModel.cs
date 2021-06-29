using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Model.Search
{
    public class ShowWebWizardListModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutWebWizard { get; set; }
        public string Email { get; set; }
        public string WebWizardProfileImageUrl { get; set; }
        public DateTime? DateOfBarth { get; set; }
        public int WebWizardId { get; set; }
        public int StateId { get; set; }
        public string EducationName { get; set; }
        public string Name { get; set; }
        public int? PhoneCode { get; set; }
    }
}