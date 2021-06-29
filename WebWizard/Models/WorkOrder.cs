using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWizard.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }
        public int NewsFeedId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientGmail { get; set; }
        public string ClientNumber { get; set; }
        public string ClientImagePath { get; set; }
        public string ProjectType { get; set; }
        public double? ProposalPrice { get; set; }
        public decimal? BidPrice { get; set; }
        public DateTime Date { get; set; }
    }
}