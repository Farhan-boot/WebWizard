using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWizard.Models
{
    public class WebWizardNotificationModel
    {
        //User Informetion
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        //Notification Informetion
        public string InformetionTitle { get; set; }
        public string InformetionDescription { get; set; }
        public string InformetionOption { get; set; }
    }
}