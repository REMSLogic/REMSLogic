using System;

namespace Site.App.Controls.Widgets
{
    public partial class FacilityDrugList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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