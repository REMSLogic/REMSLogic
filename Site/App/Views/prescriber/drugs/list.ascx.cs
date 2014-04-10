using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.prescriber.drugs
{
	public partial class list : Lib.Web.AppControlPage
	{
		public IList<Lib.Data.Drug> Drugs;
		protected void Page_Init(object sender, EventArgs e)
		{
			Drugs = Lib.Systems.Lists.GetMyDrugs();
		}

        public string GetEOCData(Lib.Data.Drug d)
        {
            var ret = "";

            if (d.HasEoc("etasu")) ret += " data-etasu=\"1\"";
            if (d.HasEoc("facility-pharmacy-enrollment")) ret += " data-facility-pharmacy-enrollment=\"1\"";
            if (d.HasEoc("patient-enrollment")) ret += " data-patient-enrollment=\"1\"";
            if (d.HasEoc("prescriber-enrollment")) ret += " data-prescriber-enrollment=\"1\"";
            if (d.HasEoc("education-training")) ret += " data-education-training=\"1\"";
            if (d.HasEoc("monitoring-management")) ret += " data-monitoring-management=\"1\"";
            if (d.HasEoc("medication-guide")) ret += " data-medication-guide=\"1\"";
            if (d.HasEoc("informed-consent")) ret += " data-informed-consent=\"1\"";
            if (d.HasEoc("forms-documents")) ret += " data-forms-documents=\"1\"";
            if (d.HasEoc("pharmacy-requirements")) ret += " data-pharmacy-requirements=\"1\"";

            return ret;
        }
	}
}