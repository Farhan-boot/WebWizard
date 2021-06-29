using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWizard.Models
{
    public class UserDetail
    {
        public string UserDbId { get; set; }
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public string UserUrl { get; set; }
        public string UserEmail { get; set; }
        public string UserType { get; set; }
        //for make call
        public bool InCall;
    }
}