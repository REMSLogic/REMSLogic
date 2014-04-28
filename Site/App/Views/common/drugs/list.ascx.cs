using RemsLogic.Model;
using RemsLogic.Repositories;
using RemsLogic.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.common.drugs
{
    public partial class list : Lib.Web.AppControlPage
    {
        private readonly IDrugListRepository _drugListRepo;
        private readonly IDrugRepository _drugRepo;
        private readonly IComplianceRepository _complianceRepo;
        private readonly IDrugListService _drugListSvc;

        public DrugList Drugs;

        public list()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString;

            _drugRepo = new DrugRepository(connectionString);
            _drugListRepo = new DrugListRepository(connectionString);
            _complianceRepo = new ComplianceRepository(connectionString);

            _drugListSvc = new DrugListService(_drugListRepo, _drugRepo, _complianceRepo);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //Drugs = Lib.Data.Drug.FindAll();
            long profileId = Lib.Systems.Security.GetCurrentProfile().ID.Value;
            Drugs = _drugListSvc.GetDrugListByProfileId(profileId, DrugListType.MyDrugs);
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