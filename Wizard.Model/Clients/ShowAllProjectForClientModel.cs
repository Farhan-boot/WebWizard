using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Model.Clients
{
    public class ShowAllProjectForClientModel
    {
        public int Id { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public string LiveDemoLink { get; set; }
        public string ProjectImagePath { get; set; }
        public string ProjectZipFilePath { get; set; }
        public bool IsFreeDownload { get; set; }
        public int ProjectSize { get; set; }
        public int WebWizardId { get; set; }
        public string NameOfTechnology { get; set; }
        public string NameOfServer { get; set; }
        public string NameOfBackendLanguage { get; set; }
        public string NameOfProject { get; set; }
        public string NameTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
