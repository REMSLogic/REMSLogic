using System;
using System.Collections.Generic;
using Lib.Data;
using RemsLogic.Model.Ecommerce;
using RemsLogic.Services;
using StructureMap;

namespace Site.Views.admin.security.ecommerce
{
    public partial class list_prescribers : Lib.Web.AppControlPage
    {
        #region Member Variables
        private IAccountService _accountSvc;
        #endregion

        #region Properties
        public IList<PrescriberProfile> PrescriberProfiles {get; set;}
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            _accountSvc = ObjectFactory.GetInstance<IAccountService>();

            PrescriberProfiles = PrescriberProfile.FindEcommerce();
        }

        protected Account GetAccountByPrescriberPrfile(PrescriberProfile prescriberProfile)
        {
            UserProfile userProfile = prescriberProfile.Prescriber.Profile;

            return _accountSvc.GetByUserProfileId(userProfile.ID ?? 0);
        }
    }
}