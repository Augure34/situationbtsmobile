using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Xml;

namespace situationWebBTS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Debug db seeder
            Database.SetInitializer(new DataAccessLayer.SchoolInitializer());


            //Class diagram
            //using (var ctx = new DataAccessLayer.SchoolContext())
            //{
            //    using (var writer = new XmlTextWriter(@"c:\Model.edmx", System.Text.Encoding.Default))
            //    {
            //        EdmxWriter.WriteEdmx(ctx, writer);
            //    }
            //}
        }
    }
}
