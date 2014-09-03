using System;
using Lib.Data;
using RemsLogic.Model;
using RemsLogic.Model.Ecommerce;
using RemsLogic.Services;
using StructureMap;

namespace Site.Views.admin.security.ecommerce
{
    public partial class edit : Lib.Web.AdminControlPage
    {
        private readonly IAccountService _accountSvc;
        private readonly IOrganizationService _orgSvc;

        public Organization Organization {get; set;}
        public ProviderUser ProviderUser {get; set;}
        public UserProfile UserProfile {get; set;}
        public Contact Contact {get; set;}
        public Lib.Data.Address Address {get; set;}
        public Framework.Security.User User {get; set;}
        public Account Account {get; set;}

        public edit()
        {
            _accountSvc = ObjectFactory.GetInstance<IAccountService>();
            _orgSvc = ObjectFactory.GetInstance<IOrganizationService>();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            long providerUserId = long.Parse(Request.QueryString["provider-user-id"]);

            if(providerUserId <= 0)
            {
                ProviderUser = new ProviderUser();
                UserProfile = new UserProfile();
                Contact = new Contact();
                Address = new Lib.Data.Address();
                User = new Framework.Security.User();

                Account = new Account
                {
                    ExpiresOn = DateTime.Now
                };
            }
            else
            {
                ProviderUser = new ProviderUser(providerUserId);
                UserProfile = ProviderUser.Profile;
                User = UserProfile.User;
                Contact = UserProfile.PrimaryContact;
                Address = UserProfile.PrimaryAddress;
                Account = GetProviderUserAccount(ProviderUser);
            }
        }

        protected Account GetProviderUserAccount(ProviderUser providerUser)
        {
            return _accountSvc.GetByProviderUserId(providerUser.ID ?? 0);
        }
    }
}