using System;
using System.Linq;
using RemsLogic.Model;
using RemsLogic.Repositories;
using System.Configuration;
using RemsLogic.Services;

namespace Site.App.Controls.Widgets
{
    public partial class MyDrugList : System.Web.UI.UserControl
    {
        private readonly IDrugListRepository _drugListRepo;
        private readonly IDrugRepository _drugRepo;
        private readonly IComplianceRepository _complianceRepo;
        private readonly IComplianceService _complianceSvc;

        public MyDrugList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString;

            _drugRepo = new DrugRepository(connectionString);
            _drugListRepo = new DrugListRepository(connectionString);
            _complianceRepo = new ComplianceRepository(connectionString);

            _complianceSvc = new ComplianceService(_drugRepo, _complianceRepo);
        }

        public DrugList GetDrugList()
        {
            // 20140424 MJL - Changed this to sit on top of my compliance code.
            // once the favorites list is added a new method should be added
            // to the service to pull the same information using the favorites
            // list instead of the drug list (which means adding a method to the
            // compliance repository as well.
            long profileId = Lib.Systems.Security.GetCurrentProfile().ID.Value;
            //var eocs = _complianceSvc.GetEocsByPrescriberProfile(profileId);

            //return new DrugList
            //{
            //    Id = 0,
            //    ListName = String.Empty,
            //    UserProfileId = profileId,
            //    Drugs = 
            //        (from eoc in eocs
            //         select new DrugListItem
            //         {
            //             Id = eoc.Key.Id,
            //             DrugName = eoc.Key.GenericName,
            //             DateAdded = DateTime.Now,
            //             DrugEocsCount = eoc.Value.Count(),
            //             UserEocsCount = eoc.Value.Count(x => x.CompletedAt.HasValue)
            //         }).ToList()
            //};

            return _drugListRepo.GetFavoritesListByProfileId(profileId);
            
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