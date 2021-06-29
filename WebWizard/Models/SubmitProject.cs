using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWizard.Models
{
    public class SubmitProject
    {
        public int Id { get; set; }
        public int NewsFeedId { get; set; }
        public int ClientId { get; set; }
        public int WebWizardId { get; set; }
        public string Comment { get; set; }
        public string ProjectUrl { get; set; }
        public HttpPostedFileBase File { get; set; }
        public DateTime PostDate { get; set; }
        public string SubmitWorkStatus { get; set; }
    }
}