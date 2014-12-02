using System;
using System.Linq;
using System.Collections.Generic;
using Framework.Security;
using Lib.Data;
using Lib.Systems;
using RemsLogic.Model.Ecommerce;
using RemsLogic.Services;
using StructureMap;

namespace Site.App
{
	public partial class Default : Lib.Web.AppPage
    {
        private readonly IAccountService _accountSvc;
        private Account _account;

        protected struct Notifys
        {
            public Lib.Data.Notification notification;
            public bool read;
        }

        public bool IsEcommerceUser
        {
            get
            {
                return _account != null;
            }
        }

        protected List<Notifys> notifications;
        protected int NumUnread;
        protected long UserId;

        public Default()
        {
            _accountSvc = ObjectFactory.GetInstance<IAccountService>();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            notifications = new List<Notifys>();
            NumUnread = 0;

            User user = Manager.GetUser();

            if(user == null)
                return;

            UserProfile userProfile = Security.GetCurrentProfile();

            // if it's an ecommerce user we need to ensure the account is enabled and not expired
            _account = _accountSvc.GetByUserProfileId(userProfile.ID ?? 0);

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