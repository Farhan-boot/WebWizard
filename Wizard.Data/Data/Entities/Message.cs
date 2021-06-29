using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.Data.Entities
{
    [Table("Message")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public string UserType { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }
        public bool IsDelete { get; set; }
        public DateTime SendDate { get; set; }
    }
}
