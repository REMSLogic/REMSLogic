using System;
using System.Linq;
using Lib.Data;
using Lib.Systems;

namespace Site.App.Controls.Widgets
{
    public partial class ComplianceGraph : System.Web.UI.UserControl
    {
        public long FacilityId {get; set;}

        protected void Page_Init(object sender, EventArgs e)
        {
            Provider provider = Security.GetCurrentProvider();
            ProviderUser providerUser = ProviderUser.FindByProvider(provider).First();

            FacilityId = (provider != null)
                ? providerUser.PrimaryFacilityID ?? 0
                : 0;
        }
    }
}