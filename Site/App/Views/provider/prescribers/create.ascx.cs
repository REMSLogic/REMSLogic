using System;

namespace Site.App.Views.provider.prescribers
{
    public partial class create : Lib.Web.AppControlPage
    {
        #region Properties
        public string BackHash {get; set;}
        #endregion

        #region Page Life Cycle
        protected void Page_Init(object sender, EventArgs e)
        {
            BackHash = Request.QueryString["back-hash"];
        }
        #endregion
    }
}