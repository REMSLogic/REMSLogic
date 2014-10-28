using System;
using System.Collections.Generic;
using Lib.Data;

namespace Site.Views.ecommerce.wizards
{
    public partial class non_etasu_selection : System.Web.UI.UserControl
    {
        #region Properties
        public IList<Drug> Drugs {get; set;}
        #endregion

        #region Page Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            Drugs = Lib.Systems.Drugs.GetNonETASUDrugs();
        }
        #endregion
    }
}