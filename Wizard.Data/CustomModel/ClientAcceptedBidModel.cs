using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.CustomModel
{
   public class ClientAcceptedBidModel
    {
        public int Id { get; set; }
        public int NewsfeedId { get; set; }
        public int WebWizardId { get; set; }
        public Single? BidAmount { get; set; }
        public string BidContent { get; set; }
        public bool Status { get; set; }
        // Client Informetion
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientImagePath { get; set; }

    }
}
