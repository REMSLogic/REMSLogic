using System;
using System.Collections.Generic;
using Lib.Data;
using RemsLogic.Model;
using RemsLogic.Services;
using StructureMap;
using User = Framework.Security.User;

namespace Site.App.Views.hcos.facilities
{
    public partial class list : Lib.Web.AppControlPage
    {
        private readonly IOrganizationService _orgSvc;
        public Provider Provider;

        public List<Facility> Facilities {get; set;}
        public long OrganizationId {get; set;}

        public list()
        {
            _orgSvc = ObjectFactory.GetInstance<IOrganizationService>();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Provider = Lib.Systems.Security.GetCurrentProvider();

            User user = Framework.Security.Manager.GetUser();
            UserProfile userProfile = UserProfile.FindByUser(user);
            ProviderUser providerUser = ProviderUser.FindByProfile(userProfile);
            Organization org = _orgSvc.Get(providerUser.OrganizationID);

            OrganizationId = org.Id;
            Facilities = org.Facilities;
        }
    }
}