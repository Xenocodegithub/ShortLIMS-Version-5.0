using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LIMS_DEMO.DAL
{
    public class CopyingProvider : DbConfiguration
    {
        public CopyingProvider()
        {
            var sqlProvider = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
