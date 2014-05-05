using System;
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

        public PrescriberEoc Find(long profileId, long drugId, long eocId)
        {
            return _complianceRepo.Find(profileId, drugId, eocId);
        }

        public void RecordCompliance(PrescriberEoc prescriberEoc)
        {
            _complianceRepo.Save(prescriberEoc);

            // TODO: Optimize this.  This approach is terrible
            PrescriberEoc eoc = _complianceRepo.Find(prescriberEoc.PrescriberProfileId, prescriberEoc.DrugId, prescriberEoc.EocId);

            if(eoc.Id > 0 && eoc.CompletedAt != null)
                LogEocComplianceEntry(eoc.Id, eoc.CompletedAt.Value);
        }

        public void LogEocComplianceEntry(long prescriberEocId, DateTime recordedAt)
        {
            _complianceRepo.LogEocComplianceEntry(prescriberEocId, recordedAt);
        }

        public void AddEocsToPrescriberProfile(long profileId, long drugId)
        {
            // first, load all of the EOCs for the given drug
            List<Eoc> eocs = _complianceRepo.GetByDrugAndRole(drugId, "view_prescriber").ToList();

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

        public void RemoveEocsFromPrescriberProfile(long profileId, long drugId)
        {
            // first, load all of hte eocs for the given drug
            List<Eoc> eocs = _complianceRepo.GetByDrug(drugId).ToList();

            // now "delete" each eoc from the user's profile.  the entries are
            // only marked as deleted.  they are not actually deleted
            foreach(Eoc eoc in eocs)
            {
                PrescriberEoc prescriberEoc = _complianceRepo.Find(profileId, drugId, eoc.Id);

                if(prescriberEoc == null)
                    continue;

                prescriberEoc.Deleted = true;
                _complianceRepo.Save(prescriberEoc);
            }
        }

        public Dictionary<Drug, List<PrescriberEoc>> GetEocsStatus(long profileId, string listType)
        {
            // first, load all of the prescriber profile drugs
            List<Drug> drugs = _drugRepo.GetByList(profileId, listType).ToList();
            
            // second, load all of the eoc requirements
            List<PrescriberEoc> reqs = _complianceRepo.GetByPrescriberProfile(profileId).ToList();

            // now build the dictionary
            return drugs.ToDictionary(
                d => d, 
                d => (from eoc in reqs 
                        where eoc.DrugId == d.Id 
                        select eoc).ToList());
        }

        public IEnumerable<Eoc> GetEocs()
        {
            return _complianceRepo.GetEocs();
        }

        public IEnumerable<Eoc> GetByDrug(long drugId)
        {
            return _complianceRepo.GetByDrug(drugId);
        }
    }
}
