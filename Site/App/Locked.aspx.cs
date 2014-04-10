using System;
using System.Collections.Generic;
using Lib.Systems.Notifications;

namespace Site.App
{
    public partial class Locked : Lib.Web.AppPage
    {
        protected struct Notifys
        {
            public Lib.Data.Notification notification;
            public bool read;
        }

        protected List<Notifys> notifications;
        protected int NumUnread;

        protected void Page_Init(object sender, EventArgs e)
        {
            notifications = new List<Notifys>();
            NumUnread = 0;
            var nis = Lib.Data.NotificationInstance.FindNewForUser(Framework.Security.Manager.GetUser());

            foreach (var ni in nis)
            {
                var t = new Notifys
                {
                    notification = ni.Notification,
                    read = ni.Read.HasValue
                };

                if (!t.read)
                    NumUnread++;

                notifications.Add(t);
            }
        }
    }
}