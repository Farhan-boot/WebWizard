using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Wizard.Data.Data.Entities;
using Wizard.Models;

namespace WebWizard.Entities
{
    public class MyWebWizard
    {
        public Wizard.Models.Countries Country { get; set; }
    }
}