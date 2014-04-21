using System.Collections.Generic;
using System.Linq;
using RemsLogic.Model;
using RemsLogic.Repositories;

namespace RemsLogic.Services
{
    public class ComplianceService : IComplianceService
    {
        private readonly IDrugRepository _drugRepo;
        private readonly IComplianceRepository _complianceRepo;

        public ComplianceService(
            IDrugRepository drugRepo,
            IComplianceRepository complianceRepo)
        {
            _drugRepo = drugRepo;
            _complianceRepo = complianceRepo;
        }

        public void AddEocRequirmentsForPrescriber(long prescriberid, long drugId)
        {

        }

        public Dictionary<Drug, List<PrescriberEoc>> GetEocRequirementsByPrescriberProfile(int profileId)
        {
            // first, load all of the prescriber profile drugs
            List<Drug> drugs = _drugRepo.GetByPrescriberProfile(profileId).ToList();

            // second, load all of the eoc requirements
            List<PrescriberEoc> reqs = _complianceRepo.GetByPrescriberProfile(profileId).ToList();

            // now build the dictionary
            return drugs.ToDictionary(
                d => d, 
                d => (from eoc in reqs 
                        where eoc.DrugId == d.Id 
                        select eoc).ToList());
        }
    }
}
