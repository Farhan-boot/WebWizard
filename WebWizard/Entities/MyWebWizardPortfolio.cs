using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebWizard.Entities
{
    public class MyWebWizardPortfolio
    {
        [Key]
        public int Id { get; set; }
        public int WebWizardId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public int ProjectSize { get; set; }
        public string ProjectZipFilePath { get; set; }
        public string ProjectImagePath { get; set; }
        public bool IsPublishNow { get; set; }
        public bool IsFreeDownload { get; set; }
        public bool IsOnlyRegisteredUserCanSee { get; set; }
        public int TechnologyId { get; set; }
        public int ProjectTypeId { get; set; }
        public int BackendLanguageId { get; set; }
        public int RunOnServerId { get; set; }
        public string LiveDemoLink { get; set; }
        public bool Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? CreateBy { get; set; }
        public int? UpdateBy { get; set; }
        public bool? IsDelete { get; set; }
    }
}
