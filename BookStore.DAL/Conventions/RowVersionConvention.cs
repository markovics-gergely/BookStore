using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookStore.DAL.Conventions
{
    public class RowVersionConvention : Convention
    {
        public RowVersionConvention()
        {   this
            .Properties()
            .Where(p => p.Name == p.DeclaringType.Name + "_ID")
            .Configure(p => p.IsKey());
            this.Properties().Where(p => p.Name == "RowVersion").Configure(p => p.IsRowVersion());
        }
    }
}
