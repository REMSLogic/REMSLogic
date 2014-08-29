using System;
using Lib.Data;
using RemsLogic.Model;
using RemsLogic.Services;
using StructureMap;

namespace Site.Views.admin.security.ecommerce
{
    public partial class edit : Lib.Web.AdminControlPage
    {
        private readonly IOrganizationService _orgSvc;

        public Organization Organization {get; set;}
        public ProviderUser ProviderUser {get; set;}
        public UserProfile UserProfile {get; set;}
        public Contact Contact {get; set;}
        public Lib.Data.Address Address {get; set;}
        public Framework.Security.User User {get; set;}

        public edit()
        {
            _orgSvc = ObjectFactory.GetInstance<IOrganizationService>();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            long providerUserId = long.Parse(Request.QueryString["provider-user-id"]);
            long organizationId = long.Parse(Request.QueryString["organization-id"]);

            Organization = _orgSvc.Get(organizationId);

            if(providerUserId <= 0)
            {
                ProviderUser = new ProviderUser();
                UserProfile = new UserProfile();
                Contact = new Contact();
                Address = new Lib.Data.Address();
                User = new Framework.Security.User();
            }
            else
            {
                ProviderUser = new ProviderUser(providerUserId);
                UserProfile = ProviderUser.Profile;
                User = UserProfile.User;
                Contact = UserProfile.PrimaryContact;
                Address = UserProfile.PrimaryAddress;
            }
        }
    }
}