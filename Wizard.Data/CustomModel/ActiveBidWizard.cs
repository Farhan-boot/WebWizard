using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Data.Entities;

namespace Wizard.Data.CustomModel
{
    public class ActiveBidWizard
    {
        public int WebWizardId { get; set; }
        public int NewsfeedId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WebWizardProfileImageUrl { get; set; }
    }
}
