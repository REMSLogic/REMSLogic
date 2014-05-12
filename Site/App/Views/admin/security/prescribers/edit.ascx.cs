using System;
using System.Collections.Generic;
using Lib.Data;
using RemsLogic.Model;
using RemsLogic.Services;
using StructureMap;
using Prescriber = Lib.Data.Prescriber;

namespace Site.App.Views.admin.security.prescribers
{
    public partial class edit : Lib.Web.AdminControlPage
    {
        #region Member Variables
        private readonly IOrganizationService _orgSvc;
        #endregion

        #region Properties
        public List<Facility> Facilities {get; set;}
        public IList<State> States {get; set;}
        public IList<Speciality> Specialities {get; set;}
        public IList<PrescriberType> PrescriberTypes {get; set;}

        public long ProviderId {get ;set;}
        public Prescriber Prescriber {get; set;}
        public PrescriberProfile PrescriberProfile {get; set;}
        public long SpecialityId {get; set;}
        public long TypeId {get; set;}
        public Framework.Security.User User {get; set;}
        #endregion

        #region Constructor
        public edit()
        {
            _orgSvc = ObjectFactory.GetInstance<IOrganizationService>();
        }
        #endregion

        #region Page Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            long prescriberProfileId = long.Parse(Request.QueryString["prescriber-profile-id"]);
            ProviderId = long.Parse(Request.QueryString["provider-id"]);

            Organization org = _orgSvc.Get(ProviderId);
            Facilities = org.Facilities;
            States = State.FindAll();
            Specialities = Speciality.FindAll();
            PrescriberTypes = PrescriberType.FindAll();

            PrescriberProfile = new PrescriberProfile(prescriberProfileId);
            Prescriber = PrescriberProfile.Prescriber;
            SpecialityId = Prescriber.SpecialityID ?? 0;
            TypeId = PrescriberProfile.PrescriberTypeID ?? 0;

            UserProfile userProfile = new UserProfile(Prescriber.ProfileID);
            User = userProfile.User;
        }
        #endregion
    }
}