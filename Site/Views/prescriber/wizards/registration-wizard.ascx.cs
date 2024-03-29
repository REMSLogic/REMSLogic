﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using Lib.Data;
using RemsLogic.Model;
using RemsLogic.Repositories;
using StructureMap;
using Address = Lib.Data.Address;
using Prescriber = Lib.Data.Prescriber;
using User = Framework.Security.User;

namespace Site.App.Views.prescriber.wizards
{
    public partial class RegistrationWizard : UserControl
    {
        #region Member Variables
        private readonly IOrganizationRepository _orgRepo;
        #endregion

        #region Properties
        public PrescriberProfile PrescriberProfile {get; set;}
        public Address Address {get; set;}
        public Contact Contact {get; set;}

        //public ProviderFacility Facility {get; set;}
        public IList<PrescriberType> PrescriberTypes {get; set;}
        public IList<Speciality> Specialities {get; set;}
        public IList<State> States {get; set;}
        
        public IEnumerable<Facility> Facilities {get; set;}
        #endregion

        #region Constructors
        public RegistrationWizard()
        {
            _orgRepo = ObjectFactory.GetInstance<IOrganizationRepository>();
        }
        #endregion

        #region Page Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            bool reset = Request.QueryString["reset"] != null;

            PrescriberProfile profile = PrescriberProfile.FindByToken(token);

            /*
            if(profile.PrimaryFacilityID != null && profile.PrimaryFacilityID > 0)
                Facility = profile.Facility;
            */

            Facilities = _orgRepo.GetOrganizationFacilities(profile.OrganizationId);

            // allow the user to go through the process again.
            if(reset)
            {
                // load prescriber
                Prescriber prescriber = new Prescriber(profile.PrescriberID);

                if(prescriber.ID > 0)
                {
                    IList<PrescriberProfile> linkedProfiles = PrescriberProfile.FindByPrescriber(prescriber);

                    // if it's the only profile, we need to delte the user and prescriber
                    // otherwise just disasociate the profile.
                    if(linkedProfiles.Count == 1)
                    {
                        UserProfile userProfile = prescriber.Profile;
                        User user = userProfile.User;

                        user.ClearGroups();

                        userProfile.Delete();
                        user.Delete();
                        prescriber.Delete();
                    }
                }

                // reset the profile
                profile.PrescriberID = null;
                profile.Save();
            }

            PrescriberProfile = profile;
            Address = profile.Address;
            Contact = profile.Contact;
            PrescriberTypes = PrescriberType.FindAll();
            Specialities = Speciality.FindAll();
            States = State.FindAll();
        }
        #endregion
    }
}
