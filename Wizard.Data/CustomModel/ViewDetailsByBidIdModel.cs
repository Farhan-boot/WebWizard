using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.CustomModel
{
    public class ViewDetailsByBidIdModel
    {
        public int Id { get; set; }
        public int NewsFeedId { get; set; }
        public int WizardId { get; set; }
        public string NewsFeedTitle { get; set; }
        public string WizardFarstName { get; set; }
        public string WizardLastName { get; set; }
        public string WizardEmail { get; set; }
        public string WizardImagePath { get; set; }
        public decimal? BidAmount { get; set; }
        public Single? ProposalAmount { get; set; }
    }
}
