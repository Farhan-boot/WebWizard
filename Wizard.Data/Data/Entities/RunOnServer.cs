using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.Data.Entities
{
    [Table("RunOnServer")]
    public class RunOnServer
    {
        [Key]
        public int Id { get; set; }
        public string NameOfServer { get; set; }
    }
}
