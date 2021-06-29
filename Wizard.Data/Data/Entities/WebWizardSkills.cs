using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.Data.Entities
{
    [Table("WebWizardSkills")]
    public class WebWizardSkills
    {
        [Key]
        public int Id { get; set; }
        public int SkillId { get; set; }
        public int WebWizardId { get; set; }
    }
}