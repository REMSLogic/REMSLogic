using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Framework.Security;
using Lib.Systems.Notifications;

namespace Lib.Systems
{
    public static class Links
    {
        // 2013-11-19 MJL - This is an excellent opportunity to use convention over configuration; however,
        // at the moment there is nothing that makes the jump from user groups to "admin" or "common" easy
        // to figure out.
        private static Dictionary<NotificationService.DataType, Dictionary<string, string>> _routes = new Dictionary<NotificationService.DataType,Dictionary<string,string>>{
            {NotificationService.DataType.Drug, new Dictionary<string,string>{
                    {"admin", "Default.aspx#admin/dsq/edit"},
                    {"common", "Default.aspx#common/drugs/detail"}
            }},
            {NotificationService.DataType.DrugCompany, new Dictionary<string,string>{
                    {"admin", "Default.aspx#admin/drugs/companies/edit"},
                    {"common", "Default.aspx#common/drugs/companies/detail"}
            }}
        };

        public static string View(NotificationService.DataType dataType, long? dataId)
        {
            User user = Framework.Security.Manager.GetUser();

            if(!_routes.ContainsKey(dataType))
                throw new ArgumentException("No route exists for the requested data type.", "dataType");

            Dictionary<string,string> dataTypeRoutes = _routes[dataType];
            IList<Group> userGroups = user.GetGroups();

            // 2013-11-19 MJL - Hard coding group IDs is not a good idea.  A better long term
            // solutions should be discussed.
            string group = (from g in userGroups
                            where g.ID == 1 || g.ID == 5 || g.ID == 6 || g.ID ==7
                            select g).Any()
                            ? "admin"
                            : "common";

            if(dataTypeRoutes == null || !dataTypeRoutes.ContainsKey(group))
                throw new ApplicationException("There are not routes defined for the requested data type and current user group memeberships.");

            StringBuilder route = new StringBuilder(dataTypeRoutes[group]);
            
            if(dataId.HasValue)
            {
                route.Append("?id=");
                route.Append(dataId.Value.ToString(CultureInfo.InvariantCulture));
            }

            return route.ToString();
        }
    }
}
