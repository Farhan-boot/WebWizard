using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.CustomModel
{
    public class MessageDetailModel
    {
        public int UserId { get; set; }
        public int MessageId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserUrl { get; set; }
        public string UserType { get; set; }
        public string Message { get; set; }
        public int SenderId { get; set; }

    }
}
