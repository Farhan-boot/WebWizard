﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Model.WebWizard
{
    public class DesignationModel
    {
        [Key]
        public int Id { get; set; }
        public string DesignationTitle { get; set; }
    }
}
