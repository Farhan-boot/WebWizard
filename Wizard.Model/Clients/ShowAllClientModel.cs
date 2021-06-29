using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard.Model.Clients
{
    public class ShowAllClientModel
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClientProfileImageUrl { get; set; }
        public string AboutClient { get; set; }
        public string ClientEmail { get; set; }
        public string ClientMobileNo { get; set; }

    }
}
