using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lib.Data;

namespace Site.App
{
    public partial class SignUp : System.Web.UI.Page
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
        }
    }
}