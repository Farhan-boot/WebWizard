using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.Data.Entities
{
    [Table("WebWizardBid")]
    public class WebWizardBid
    {
        [Key]
        public int Id { get; set; }
        public int NewsfeedId { get; set; }
        [ForeignKey("NewsfeedId")]
        public NewsFeed NewsFeeds { get; set; }

        public int WebWizardId { get; set; }
        public Single? BidAmount { get; set; }
        public string BidContent { get; set; }
        public DateTime PostDate { get; set; }
        public bool Status { get; set; }
    }
}

