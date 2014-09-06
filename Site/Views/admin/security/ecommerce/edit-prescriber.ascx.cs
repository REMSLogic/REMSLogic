using System;
using System.Collections.Generic;
using Lib.Data;
using RemsLogic.Model.Ecommerce;
using RemsLogic.Services;
using StructureMap;

namespace Site.Views.admin.security.ecommerce
{
    public partial class edit_prescriber :  Lib.Web.AdminControlPage
    {
        #region Member Variables
        private readonly IAccountService _accountSvc;
        #endregion

        #region Properties
        public IList<State> States {get; set;}
        public IList<Speciality> Specialities {get; set;}
        public IList<PrescriberType> PrescriberTypes {get; set;}

        public Prescriber Prescriber {get; set;}
        public PrescriberProfile PrescriberProfile {get; set;}
        public long SpecialityId {get; set;}
        public long TypeId {get; set;}
        public Framework.Security.User User {get; set;}
        public Account Account {get; set;}
        #endregion

        #region Constructor
        public edit_prescriber()
        {
            _accountSvc = ObjectFactory.GetInstance<IAccountService>();
        }
        #endregion

        #region Page Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            long prescriberProfileId = long.Parse(Request.QueryString["prescriber-profile-id"]);

            States = State.FindAll();
            Specialities = Speciality.FindAll();
            PrescriberTypes = PrescriberType.FindAll();

            if(prescriberProfileId <= 0)
            {
                PrescriberProfile = new PrescriberProfile();
                Prescriber = new Lib.Data.Prescriber();
                SpecialityId = 0;
                TypeId = 0;
                User = new Framework.Security.User();

                Account = new Account
                {
                    ExpiresOn = DateTime.Now
                };
            }
            else
            {
                PrescriberProfile = new PrescriberProfile(prescriberProfileId);
                Prescriber = PrescriberProfile.Prescriber;
                SpecialityId = Prescriber.SpecialityID ?? 0;
                TypeId = PrescriberProfile.PrescriberTypeID ?? 0;
                UserProfile userProfile = new UserProfile(Prescriber.ProfileID);
                User = userProfile.User;
                Account = _accountSvc.GetByUserProfileId(userProfile.ID ?? 0);
            }

        }
        #endregion
    }
}