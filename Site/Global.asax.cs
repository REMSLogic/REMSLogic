using System;
using System.Web;
using RemsLogic.Wiring;
using StructureMap;

namespace Site
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // init StructureMap
            ObjectFactory.Initialize(o => o.Scan(s =>
                {
                    s.AssemblyContainingType(typeof(RemsLogicRegistry));
                    s.LookForRegistries();
                }));

            Lib.Manager.Init();
            Framework.Manager.Init();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Framework.Manager.HandleError(HttpContext.Current);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Request.Headers["DBUNIQUEID"] = Guid.NewGuid().ToString();
        }

        protected void Application_EndRequest( object sender, EventArgs e )
        {
            Framework.Data.Database.CloseConnections( HttpContext.Current.Request.Headers["DBUNIQUEID"] );
        }
    }
}