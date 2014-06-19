using System;
using System.Collections.Generic;
using System.Linq;
using Lib.Web;
using Lib.Data;

namespace Site.App.Views.prescriber
{
    public partial class edit : AppControlPage
    {
		public edit() : base()
		{
			IgnoreDrugSelection = true;
		}

        #region Properties
        public IList<Drug> Drugs {get; set;}
        #endregion

        #region Page Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            var user = Framework.Security.Manager.GetUser();

            IList<Drug> noSelections = Lib.Systems.Drugs.GetDrugsWithNoSelection(user);
            IList<Drug> updates = Lib.Systems.Drugs.GetDrugsWithUpdates(user);

            // combine and sort the two lists
            Drugs = (noSelections.Union(updates)).OrderBy(x => x.GenericName).ToList();
        }
        #endregion
    }
}