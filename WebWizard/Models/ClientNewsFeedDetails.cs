using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWizard.Models
{
    public class ClientNewsFeedDetails
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
        public bool IsWebWizard { get; set; }
        public DateTime PostDate { get; set; }
        public string ProjectType { get; set; }
        public string BackendLanguage { get; set; }
        public string RunOnServer { get; set; }
        public string Technology { get; set; }
        public string GroupName { get; set; }
        public Decimal? Amount { get; set; }
    }
}