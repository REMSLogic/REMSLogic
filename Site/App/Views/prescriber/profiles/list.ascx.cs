using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Security;
using Lib.Data;

namespace Site.App.Views.prescriber.profiles
{
    public partial class list : Lib.Web.AppControlPage
    {
        public Prescriber Prescriber {get; set;}
        public IList<PrescriberProfile> PrescriberProfiles {get; set;}
        public List<Address> Addresses {get; set;}
        public List<Contact> Contacts {get; set;}
        public IList<State> States {get; set;}
        public IList<Speciality> Specialities {get; set;}
        public long SpecialityId {get; set;}

        protected void Page_Init(object sender, EventArgs e)
        {
            RequireRole( "view_prescriber" );

            User user = Manager.GetUser();
            UserProfile userProfile = UserProfile.FindByUser(user);

            Prescriber = Prescriber.FindByProfile(userProfile);
            PrescriberProfiles = PrescriberProfile.FindByPrescriber(Prescriber);

            Addresses = (from pp in PrescriberProfiles
                         select pp.Address).ToList();

            Contacts = (from pp in PrescriberProfiles
                        select pp.Contact).ToList();

            States = State.FindAll();
            Specialities = Speciality.FindAll();
            SpecialityId = Prescriber.SpecialityID ?? 0;
        }
    }
}