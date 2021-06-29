using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Data;

namespace Wizard.Data.KeyGenerator
{
    public static class PrimaryKey
    {
        public static Wizard.Data.Data.Entities.PrimaryKeyGenerator GetKey()
        {
            var code = Guid.NewGuid();
            Wizard.Data.Data.Entities.PrimaryKeyGenerator generator = new Wizard.Data.Data.Entities.PrimaryKeyGenerator();
            generator.Code = code.ToString();
            using (WebWizardConnection db = new WebWizardConnection())
            {
                db.PrimaryKeyGenerator.Add(generator);
                db.SaveChanges();
                return generator;
            }
        }
    }
}
