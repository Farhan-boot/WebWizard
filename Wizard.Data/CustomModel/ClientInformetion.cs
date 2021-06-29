using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Data.Entities;

namespace Wizard.Data.CustomModel
{
    public class ClientInformetion
    {
        public ClientRegistration ClientRegistration { get; set; }
        public ClientDetails ClientDetails { get; set; }
        public Education Education { get; set; }
    }
}
