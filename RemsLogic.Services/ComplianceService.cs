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

        public void AddEocsToPrescriberProfile(long profileId, long drugId)
        {
            // first, load all of the EOCs for the given drug
            List<Eoc> eocs = _complianceRepo.GetByDrugId(drugId).ToList();

            // now make sure that the presriber has the requirment setup correctly
            foreach(Eoc eoc in eocs)
            {
                // try and load an existing eoc.  if one isn't found, create a new one
                PrescriberEoc prescriberEoc = _complianceRepo.Find(profileId, drugId, eoc.Id) 
                    ?? new PrescriberEoc
                    {
                        PrescriberProfileId = profileId,
                        EocId = eoc.Id,
                        DrugId = drugId,
                        CompletedAt = null
                    };

                // set deleted to false (there may have been one that previously
                // existed but was deleted
                prescriberEoc.Deleted = false;

                // save the prescriber eoc
                _complianceRepo.Save(prescriberEoc);
            }
        }

        public Dictionary<Drug, List<PrescriberEoc>> GetEocsByPrescriberProfile(int profileId)
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
