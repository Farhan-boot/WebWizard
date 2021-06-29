using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.Data.Entities
{
    [Table("TermsAndConditions")]
    public class TermsAndConditions
    {
        [Key]
        public int Id { get; set; }
        public string TermsAndConditionsTitle { get; set; }
        public string TermsAndConditionsDescription { get; set; }
    }
}
