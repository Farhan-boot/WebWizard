using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Model.WebWizard
{
    public class SkillModel
    {
        [Key]
        public int Id { get; set; }
        public string NameOfSkill { get; set; }
    }
}
