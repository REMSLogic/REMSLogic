using System;
using System.Linq;
using System.Collections.Generic;
using Framework.Security;
using Lib.Systems.Notifications;

namespace Site.App
{
	public partial class Default : Lib.Web.AppPage
    {
        protected struct Notifys
        {
            public Lib.Data.Notification notification;
            public bool read;
        }

        protected List<Notifys> notifications;
        protected int NumUnread;
        protected long UserId;

        protected void Page_Init(object sender, EventArgs e)
        {
            notifications = new List<Notifys>();
            NumUnread = 0;

            User user = Manager.GetUser();

            if(user == null)
                return;

            var nis = Lib.Data.NotificationInstance.FindNewForUser(user);

            UserId = user.ID ?? 0;
            NumUnread = (
                from ni in nis
                where !ni.Read.HasValue
                select ni).Count();

            var shortList = nis
                .OrderByDescending(x => x.Notification.Sent)
                .Take(5);

            foreach (var ni in shortList)
            {
                notifications.Add(new Notifys
                {
                    notification = ni.Notification,
                    read = ni.Read.HasValue
                });
            }
        }
    }
}