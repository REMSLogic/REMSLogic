using System;
using System.Collections.Generic;
using System.Text;
using Framework.Security;
using Lib.Data;

namespace Site.App.Views.common.notifications
{
    public partial class edit : System.Web.UI.UserControl
    {
        public Notification Item {get; set;}
        public string BackHash {get; set;}
        public string ParentType {get; set;}
        public string ParentID {get; set;}
        public string SendToId {get; set;}

        //public IList<PrescriberProfile> SendToList {get; set;}
        public string SendToList {get; private set;}

        protected void Page_Init(object sender, EventArgs e)
        {
            BackHash = Request.QueryString["back-hash"];
            ParentType = Request.QueryString["parent-type"];
            ParentID = Request.QueryString["parent-id"];
            SendToId = Request.QueryString["send-to-id"];

            User currentUser = Framework.Security.Manager.GetUser();
            UserProfile userProfile = Lib.Data.UserProfile.FindByUser(currentUser);

            if(Manager.HasRole("view_admin") ||
                Manager.HasRole("manage_users"))
                SendToList = BuildAdminSendToList();

            else if(Manager.HasRole("view_provider"))
                SendToList = BuildProviderSendToList(userProfile);

            string strID = Request.QueryString["id"];
            long id;
            if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
                Item = new Lib.Data.Notification();
            else
                Item = new Lib.Data.Notification(id);
        }

        private string BuildAdminSendToList()
        {
            StringBuilder sb = new StringBuilder();
            IList<User> users = User.FindAll();

            sb.Append("[{label: 'All Users', value: 0},");
            foreach(User u in users)
            {
                if(!u.ID.HasValue)
                    continue;

                UserProfile userProf = UserProfile.FindByUser(u.ID.Value);

                sb.Append(String.Format("{{label: '{0}, {1}', value: {2}}},", 
                    userProf.PrimaryContact.LastName, 
                    userProf.PrimaryContact.FirstName, 
                    userProf.UserID));
            }
            sb.Append("]");

            return sb.ToString();
        }

        private string BuildProviderSendToList(UserProfile userProfile)
        {
            StringBuilder sb = new StringBuilder();

            ProviderUser providerUser = ProviderUser.FindByProfile(userProfile);
            IList<PrescriberProfile> sendTo = PrescriberProfile.FindByProvider(providerUser.Provider);

            sb.Append("[{label: 'All Prescribers', value: 0},");
            foreach(PrescriberProfile prescriberProf in sendTo)
            {
                UserProfile userProf = UserProfile.FindByPrimaryContact(prescriberProf.ContactID);

                sb.Append(String.Format("{{label: '{0}, {1}', value: {2}}},", 
                    prescriberProf.Contact.LastName, 
                    prescriberProf.Contact.FirstName, 
                    userProf.UserID));
            }
            sb.Append("]");

            return sb.ToString();
        }
    }
}