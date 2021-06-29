using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Data.Data.Entities
{
    [Table("ClientDetails")]
    public class ClientDetails
    {
        [Key]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string AboutClient { get; set; }
        public DateTime? DateOfBarth { get; set; }
        public int? EducationId { get; set; }
        public int? LocationId { get; set; }
        public string ClientMobileNo { get; set; }
        public string ClientProfileImageUrl { get; set; }
        public int? DesignationId { get; set; }
        public bool Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? CreateBy { get; set; }
        public int? UpdateBy { get; set; }
    }
}
