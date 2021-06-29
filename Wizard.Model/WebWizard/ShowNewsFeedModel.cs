using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Model.WebWizard
{
    public class ShowNewsFeedModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string WebWizardProfileImageUrl { get; set; }
        public int WebWizardId { get; set; }
        public string Title { get; set; }
        public DateTime PostDate { get; set; }
        public string PostContent { get; set; }
        public bool IsWebWizard { get; set; }
        public string ProjectType { get; set; }
        public string BackendLanguage { get; set; }
        public string RunOnServer { get; set; }
        public string Technology { get; set; }
        public string GroupName { get; set; }
        public Decimal? Amount { get; set; }
    }
}
