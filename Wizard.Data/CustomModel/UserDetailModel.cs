using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.CustomModel
{
    public class UserDetailModel
    {
        public string UserDbId { get; set; }
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public string UserUrl { get; set; }
        public string UserEmail { get; set; }
        public string UserType { get; set; }
    }
}
