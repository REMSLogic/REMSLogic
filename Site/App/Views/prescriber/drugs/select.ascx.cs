using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.prescriber.drugs
{
	public partial class select : Lib.Web.AppControlPage
	{
		public IList<Lib.Data.Drug> Drugs;
		public IList<Lib.Data.Drug> AvailableDrugs;
		public IList<Lib.Data.Drug> SelectedDrugs;

		protected void Page_Init(object sender, EventArgs e)
		{
			Drugs = Lib.Data.Drug.FindAll();
			SelectedDrugs = Lib.Systems.Lists.GetMyDrugs();
			AvailableDrugs = new List<Lib.Data.Drug>();

			foreach (var d in Drugs)
			{
				bool found = false;
				for (int i = 0; i < SelectedDrugs.Count; i++)
				{
					if (d.ID.Value == SelectedDrugs[i].ID.Value)
					{
						found = true;
						break;
					}
				}

				if (found)
					continue;

				AvailableDrugs.Add(d);
			}
		}

		public string GetEOCData(Lib.Data.Drug d)
		{
			var ret = "";

			if( d.HasEoc("etasu") ) ret += " data-etasu=\"1\"";
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