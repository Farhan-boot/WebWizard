using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.Data.Entities
{
    [Table("WebWizardDetails")]
    public class WebWizardDetails
    {
        [Key]
        public int Id { get; set; }
        public int WebWizardId { get; set; }
        public string AboutWebWizard { get; set; }
        public DateTime? DateOfBarth { get; set; }
        public int EducationId { get; set; }
        public int LocationId { get; set; }
        public string WebWizardMobileNo { get; set; }
        public string WebWizardProfileImageUrl { get; set; }
        public DateTime? ExperienceYearFrom { get; set; }
        public DateTime? ExperienceYearTo { get; set; }
        public int DesignationId { get; set; }
        public bool Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreateBy { get; set; }
        public int UpdateBy { get; set; }
    }
}