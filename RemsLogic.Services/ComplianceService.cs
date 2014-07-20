using System;
using System.Collections.Generic;
using System.Linq;
using RemsLogic.Model;
using RemsLogic.Model.Compliance;
using RemsLogic.Model.Dsq;
using RemsLogic.Repositories;

namespace RemsLogic.Services
{
    public class ComplianceService : IComplianceService
    {
        private readonly IDrugRepository _drugRepo;
        private readonly IComplianceRepository _complianceRepo;
        private readonly IDsqRepository _dsqRepo;

        public ComplianceService(
            IDrugRepository drugRepo,
            IComplianceRepository complianceRepo,
            IDsqRepository dsqRep)
        {
            _drugRepo = drugRepo;
            _complianceRepo = complianceRepo;
            _dsqRepo = dsqRep;
        }

        public PrescriberEoc Find(long profileId, long drugId, long eocId)
        {
            return _complianceRepo.Find(profileId, drugId, eocId);
        }

        public PrescriberEoc FindByLinkId(long profileId, long linkId)
        {
            return _complianceRepo.FindByLinkId(profileId, linkId);
        }

        public IEnumerable<PrescriberEoc> GetPrescriberEocs(long drugId, long questionId, long userProfileId)
        {
            return _complianceRepo.GetPrescriberEocs(drugId, questionId, userProfileId);
        }

        public void RecordCompliance(PrescriberEoc prescriberEoc)
        {
            _complianceRepo.Save(prescriberEoc);

            // TODO: Optimize this.  This approach is terrible
            PrescriberEoc eoc = _complianceRepo.FindByLinkId(prescriberEoc.PrescriberProfileId, prescriberEoc.LinkId);

            if(eoc.Id > 0 && eoc.CompletedAt != null)
                LogEocComplianceEntry(eoc.Id, eoc.CompletedAt.Value);
        }

        public void LogEocComplianceEntry(long prescriberEocId, DateTime recordedAt)
        {
            _complianceRepo.LogEocComplianceEntry(prescriberEocId, recordedAt);
        }

        public void AddEocsToProfile(long profileId, List<string> roles, long drugId)
        {
            // get a list of eoc ids that apply to the roles
            List<long> eocIds = (
                from e in _complianceRepo.GetEocs()
                where roles.Any(r => e.AppliesTo.Contains(r))
                select e.Id).ToList();

            // get a list of links that have eocs that apply
            // to the list of roles and are required
            List<DsqLink> links = (
                from l in _dsqRepo.GetLinks(drugId)
                where 
                    l.HasPrereq &&
                    eocIds.Contains(l.EocId)
                select l).ToList();

            foreach(DsqLink link in links)
            {
                // try and load an existing eoc.  if one isn't found, create a new one
                PrescriberEoc prescriberEoc = _complianceRepo.FindByLinkId(profileId, link.Id) 
                    ?? new PrescriberEoc
                    {
                        PrescriberProfileId = profileId,
                        EocId = link.EocId,
                        DrugId = drugId,
                        LinkId = link.Id,
                        QuestionId = link.QuestionId,
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
            _complianceRepo.RemovePrescriberEocs(profileId, drugId);
        }

        public void RebuildEocs(long profileId, List<string> roles, long drugId)
        {
            RemoveEocsFromPrescriberProfile(profileId, drugId);
            AddEocsToProfile(profileId, roles, drugId);
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

        public Eoc GetEoc(long eocId)
        {
            return _complianceRepo.GetEoc(eocId);
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