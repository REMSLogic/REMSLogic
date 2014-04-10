using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Security;
using Lib.Data;

namespace Site.App.Views.account
{
    public partial class profile : Lib.Web.AppControlPage
    {
        public User UserInfo {get; set;}
        public UserProfile UserProfile {get; set;}
        public Address PrimaryAddress {get; set;}
        public Contact PrimaryContact {get; set;}

        public Prescriber Prescriber {get; set;}
        public IList<PrescriberProfile> PrescriberProfiles {get; set;}
        public List<Address> Addresses {get; set;}
        public List<Contact> Contacts {get; set;}
        public IList<State> States {get; set;}
        public IList<Speciality> Specialities {get; set;}
        public long SpecialityId {get; set;}

        protected void Page_Init(object sender, EventArgs e)
        {
            UserInfo = Manager.GetUser();
            UserProfile = UserProfile.FindByUser(UserInfo);

            if(UserProfile.PrimaryAddressID.HasValue)
                PrimaryAddress = UserProfile.PrimaryAddress;

            if(UserProfile.PrimaryContactID.HasValue)
                PrimaryContact = UserProfile.PrimaryContact;

            if(Manager.HasRole("view_prescriber", true))
            {
                Prescriber = Prescriber.FindByProfile(UserProfile);
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
}