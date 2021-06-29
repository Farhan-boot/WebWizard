using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Model.WebWizard
{
    public class NewsFeedModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsWebWizard { get; set; }
        public string ProjectType { get; set; }
        public string Technology { get; set; }
        public string BackendLanguage { get; set; }
        public string RunOnServer { get; set; }
        public string GroupName { get; set; }
        public Decimal? YourAmount { get; set; }
        
    }
}
