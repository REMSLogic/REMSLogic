using System;
using System.Collections.Generic;
using Lib.Data;
using RemsLogic.Model;
using RemsLogic.Services;
using StructureMap;

namespace Site.App.Views.admin.security.providers
{
    public partial class edit : Lib.Web.AdminControlPage
    {
        private readonly IOrganizationService _orgSvc;

        public Organization Organization {get; set;}
        public IList<ProviderUser> AdministrativeUsers {get; set;}
        public IList<PrescriberProfile> Prescribers {get; set;}
        public IList<PrescriberProfile> PendingInvites {get; set;}

        public edit()
        {
            _orgSvc = ObjectFactory.GetInstance<IOrganizationService>();
        }
        
        protected void Page_Init(object sender, EventArgs e)
        {
            string strId = Request.QueryString["id"];
            long id;

            if (string.IsNullOrEmpty(strId) || !long.TryParse(strId, out id))
            {
                Organization = new Organization();
                Organization.PrimaryFacility = new Facility();
                Organization.PrimaryFacility.Address = new RemsLogic.Model.Address();

                AdministrativeUsers = new List<ProviderUser>();
                Prescribers = new List<PrescriberProfile>();
                PendingInvites = new List<PrescriberProfile>();
            }
            else
            {
                Organization = _orgSvc.Get(id);

                AdministrativeUsers = ProviderUser.FindByOrganization(id);
                Prescribers = PrescriberProfile.FindByProvider(id);
                PendingInvites = PrescriberProfile.FindPendingInvitesByProvider(id);
            }
        }
    }
}