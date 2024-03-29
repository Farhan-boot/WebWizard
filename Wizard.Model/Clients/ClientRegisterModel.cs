﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wizard.Models
{
    public class ClientRegisterModel
    {
        [Key]
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NameTitle { get; set; }
        public int StateId { get; set; }
        public string Password { get; set; }
        public int TermsAndConditionsId { get; set; }
        public string VerificationCode { get; set; }
        public bool Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreateBy { get; set; }
        public int UpdateBy { get; set; }
    }
}