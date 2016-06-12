using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace situationWebBTS.DataAccessLayer
{    public class SchoolConfiguration : DbConfiguration
    {
        public SchoolConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());

            //Interception for error generation
            //DbInterception.Add(new SchoolInterceptorTransientErrors());

            //Interception logging
            DbInterception.Add(new SchoolInterceptorLogging());
        }

    }
}
