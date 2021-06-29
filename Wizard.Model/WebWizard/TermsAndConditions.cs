using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Model.WebWizard
{
    public class TermsAndConditions
    {
        [Key]
        public int Id { get; set; }
        public string TermsAndConditionsTitle { get; set; }
        public string TermsAndConditionsDescription { get; set; }
    }
}
