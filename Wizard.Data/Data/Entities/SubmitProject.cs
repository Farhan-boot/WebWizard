using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.Data.Entities
{
    [Table("SubmitProject")]
    public class SubmitProject
    {
        [Key]
        public int Id { get; set; }
        public int NewsFeedId { get; set; }
        public int ClientId { get; set; }
        public int WebWizardId { get; set; }
        public string Comment { get; set; }
        public string ProjectUrl { get; set; }
        public DateTime PostDate { get; set; }
        public string SubmitWorkStatus { get; set; }
    }
}
