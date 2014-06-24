using System;
using System.Collections.Generic;
using System.Linq;
using Lib.Data;

namespace Site.App.Views.common.notifications
{
    public partial class archive : Lib.Web.AppControlPage
    {
        #region Properties
        public List<Notification> Notifications {get; set;}
        #endregion

        #region Page Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            IList<NotificationInstance> instances = NotificationInstance.FindAllForUser(Framework.Security.Manager.GetUser());
            Notifications = (from n in instances
                             select n.Notification).ToList();
        }
        #endregion
    }
}