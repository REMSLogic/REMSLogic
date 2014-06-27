using System;
using System.Collections.Generic;

namespace Site.App.Controls.Widgets
{
    public partial class PrescriberUpdates : System.Web.UI.UserControl
    {
        public IList<Lib.Data.PrescriberUpdate> PrescriberUpdateItems;

        protected void Page_Init(object sender, EventArgs e)
        {
            var profile = Lib.Data.UserProfile.FindByUser(Framework.Security.Manager.GetUser());
            var providerUser = Lib.Data.ProviderUser.FindByProfile(profile);

            if(providerUser != null)
                PrescriberUpdateItems = Lib.Data.PrescriberUpdate.FindByFacility(providerUser.PrimaryFacilityID ?? 0);
        }
    }
}