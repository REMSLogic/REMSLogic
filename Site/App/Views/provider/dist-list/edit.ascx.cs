using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Security;
using Lib.Data;

namespace Site.App.Views.provider.dist_list
{
    public partial class edit : System.Web.UI.UserControl
    {
        #region Properties
        public Provider Provider {get; set;}
        public DistributionList DistributionList {get; set;}
        public IList<Prescriber> Prescribers {get; set;}
        #endregion

        #region Page Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            long id;

            if(!long.TryParse(Request.QueryString["id"], out id))
                id = 0;

            Provider = Provider.FindByUser(ProviderUser.FindByProfile(UserProfile.FindByUser(Framework.Security.Manager.GetUser())));
            DistributionList = new DistributionList(id);
            Prescribers = UserListItem.FindByList<Prescriber>(long.Parse(DistributionList.Settings));
        }
        #endregion

        #region Utility Methods
        public string GetPrescriberType(Prescriber prescriber)
        {
            IList<PrescriberProfile> profiles = PrescriberProfile.FindByPrescriber(prescriber);
            PrescriberProfile profile = (from p in profiles
                                         where p.ProviderID == Provider.ID
                                         select p).FirstOrDefault();

            return (profile != null && profile.PrescriberTypeID != null)
                ? profile.PrescriberType.DisplayName
                : String.Empty;
        }
        #endregion
    }
}