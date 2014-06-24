using System;
using System.Collections.Generic;
using System.Text;
using Framework.Security;
using Lib.Data;
using Lib.Systems.Notifications;

namespace Site.App.Views.common.notifications
{
    public partial class edit : Lib.Web.AppControlPage
    {
        public string BackHash {get; set;}
        public string ParentType {get; set;}
        public string ParentID {get; set;}
        public string SendToId {get; set;}

        public string SendToList {get; private set;}

        //public IList<DistributionList> DistributionLists {get; set;}

        protected void Page_Init(object sender, EventArgs e)
        {
            BackHash = Request.QueryString["back-hash"];
            ParentType = Request.QueryString["parent-type"];
            ParentID = Request.QueryString["parent-id"];
            SendToId = Request.QueryString["send-to-id"];

            //DistributionLists = NotificationService.GetDistributionLists(Manager.GetUser());
        }
    }
}