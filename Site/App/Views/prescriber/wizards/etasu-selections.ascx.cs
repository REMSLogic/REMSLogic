using System;
using System.Collections.Generic;
using System.Web.UI;
using Lib.Data;

namespace Site.App.Views.prescriber.wizards
{
    public partial class etasu_selections : UserControl
    {
        #region Properties
        public IList<Drug> Drugs {get; set;}
        #endregion

        #region Page Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            Drugs = Lib.Systems.Drugs.GetETASUDrugs();
        }
        #endregion
    }
}