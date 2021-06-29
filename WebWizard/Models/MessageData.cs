using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWizard.Models
{
    public class MessageData
    {
        public int myId { get; set; }
        public string myUserType { get; set; }
        public int senderId { get; set; }
        public string senderUserType { get; set; }
    }
}