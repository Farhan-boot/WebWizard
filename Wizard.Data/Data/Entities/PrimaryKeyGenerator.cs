using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.Data.Entities
{
    [Table("PrimaryKeyGenerator")]
    public class PrimaryKeyGenerator
    {
        [Key]
        public int SetPrimaryKey { get; set; }
        public string Code { get; set; }
    }
}
