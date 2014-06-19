using System;
using Lib.Data;

namespace Site.App.Views.prescriber.wizards
{
    public partial class landing_page : System.Web.UI.UserControl
    {
        #region Properties
        public string Message {get; set;}

        public string Token {get; set;}
        public string Reset {get; set;}
        public PrescriberProfile PrescriberProfile {get; set;}
        #endregion

        #region Page Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            Token = Request.QueryString["token"];
            Reset = Request.QueryString["reset"];
            PrescriberProfile = PrescriberProfile.FindByToken(Token);
        }
        #endregion
    }
}