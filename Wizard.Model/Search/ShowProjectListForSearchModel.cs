using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Model.Search
{
    public class ShowProjectListForSearchModel
    {
        public int Id { get; set; }
        public int WebWizardId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectImagePath { get; set; }
        public bool IsPublishNow { get; set; }
        public bool IsOnlyRegisteredUserCanSee { get; set; }
        public string Technology { get; set; }
        public string ProjectType { get; set; }
        public string BackendLanguage { get; set; }
        public string RunOnServer { get; set; }
        public string LiveDemoLink { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsDelete { get; set; }
    }
}
