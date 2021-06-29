using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Model.WebWizard
{
    public class WebWizardDetailsModel
    {
        [Key]
        public int Id { get; set; }
        public int WebWizardId { get; set; }
        public string About { get; set; }
        public DateTime DateOfBarth { get; set; }
        public int Education { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MobileNo { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime ExperienceFrom { get; set; }
        public DateTime ExperienceTo { get; set; }
        public int Designation { get; set; }
        public int Status { get; set; }
        public int LocationId { get; set; }
        public List<int> Skills { get; set; }

    }
}
