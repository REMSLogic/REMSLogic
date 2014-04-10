using System;
using Lib.Data;

namespace Site.App.Views.provider.prescribers
{
    public partial class edit : System.Web.UI.UserControl
    {
        #region Properties
        public Prescriber Prescriber {get; set;}

        public string BackHash {get; set;}
        public string ParentType {get; set;}
        public string ParentId {get; set;}
        #endregion

        #region Page Life Cycle
        protected void Page_Init(object sender, EventArgs e)
        {
            BackHash = Request.QueryString["back-hash"];
            ParentType = Request.QueryString["parent-type"];
            ParentId = Request.QueryString["parent-id"];

            string strId = Request.QueryString["id"];
            long id;

            if (string.IsNullOrEmpty(strId) || !long.TryParse(strId, out id))
                Prescriber = new Prescriber();
            else
                Prescriber = new Prescriber(id);
        }
        #endregion
    }
}