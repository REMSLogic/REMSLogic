using System;

namespace Site.App.Views.provider.prescribers
{
    public partial class delete : System.Web.UI.UserControl
    {
        #region Properties
        public Lib.Data.Prescriber Prescriber {get; private set;}
        #endregion

        #region Page Life Cycle
        protected void Page_Init(object sender, EventArgs e)
        {
            string strId = Request.QueryString["id"];
            long id;

            if (string.IsNullOrEmpty(strId) || !long.TryParse(strId, out id))
                Prescriber = new Lib.Data.Prescriber();
            else
                Prescriber = new Lib.Data.Prescriber(id);
        }
        #endregion
    }
}