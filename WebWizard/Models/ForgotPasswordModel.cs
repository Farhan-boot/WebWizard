﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWizard.Models
{
    public class ForgotPasswordModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
}