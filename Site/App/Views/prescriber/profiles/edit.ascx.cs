using System;
using System.Collections.Generic;
using Lib.Data;

namespace Site.App.Views.prescriber.profiles
{
    public partial class edit : Lib.Web.AppControlPage
    {
        public Framework.Security.User UserInfo {get; set;}
        public UserProfile UserProfile {get; set;}
        public PrescriberProfile PrescriberProfile {get; set;}
        public Address Address {get; set;}
        public Contact Contact {get; set;}
        public Provider Provider {get; set;}
        public ProviderFacility Facility {get; set;}
        public IList<PrescriberType> PrescriberTypes {get; set;}
        public long TypeId {get; set;}

        protected void Page_Init(object sender, EventArgs e)
        {
            RequireRole( "view_prescriber" );

            string input = Request.QueryString["id"];
            long prescriberId;

            if(String.IsNullOrEmpty(input) || !long.TryParse(input, out prescriberId))
                RedirectHash( "prescriber/profiles/list", true, "Invalid Prescriber Profile" );
            else
                PrescriberProfile = new Lib.Data.PrescriberProfile(prescriberId);

            UserInfo = Framework.Security.Manager.GetUser();
            UserProfile = UserProfile.FindByUser(UserInfo);

            Address = PrescriberProfile.Address;
            Contact = PrescriberProfile.Contact;
            Provider = PrescriberProfile.Provider;
            Facility = PrescriberProfile.Facility;
            PrescriberTypes = PrescriberType.FindAll();

            TypeId = PrescriberProfile.PrescriberTypeID ?? 0;
        }
    }
}