using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.Data.Entities
{
    [Table("NewsFeed")]
    public class NewsFeed
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
        public bool IsWebWizard { get; set; }
        public DateTime PostDate { get; set; }
        public string ProjectType { get; set; }
        public string BackendLanguage { get; set; }
        public string RunOnServer { get; set; }
        public string Technology { get; set; }
        public string GroupName { get; set; }
        public Decimal? Amount { get; set; }
    }
}
