using System;
using System.Collections.Generic;
using System.Linq;
using Lib.Data;
using Lib.Systems;
using RemsLogic.Model;
using RemsLogic.Services;
using StructureMap;

namespace Site.App.Controls.Widgets
{
    public partial class OrganizationSummary : System.Web.UI.UserControl
    {
        public int PrescriberCount {get; set;}
        public int FacilityCount {get; set;}

        protected void Page_Init(object sender, EventArgs e)
        {
            Provider provider = Security.GetCurrentProvider();
            ProviderUser providerUser = ProviderUser.FindByProvider(provider.ID ?? 0).First();

            IOrganizationService orgSvc = ObjectFactory.GetInstance<IOrganizationService>();
         
            // this ensures we don't have to worry about null references
            Organization organization = orgSvc.Get(providerUser.OrganizationID) ?? new Organization()
            {
                Facilities = new List<Facility>()
            };

            // Get the prescribers for the organization
            IList<PrescriberProfile> prescribers = PrescriberProfile.FindByOrganization(organization.Id);

            PrescriberCount = prescribers.Count;
            FacilityCount = organization.Facilities.Count;
        }
    }
}