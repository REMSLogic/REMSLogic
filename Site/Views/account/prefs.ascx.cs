using System;
using Framework.Security;
using Lib.Data;

namespace Site.App.Views.account
{
    public partial class prefs : Lib.Web.AppControlPage
    {
        #region Properties
        public UserPreferences UserPreferences {get; set;}
        #endregion

        #region Page Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            User user = Framework.Security.Manager.GetUser();
            UserPreferences = UserPreferences.FindByUser(user) ?? new UserPreferences();
        }
        #endregion
    }
}