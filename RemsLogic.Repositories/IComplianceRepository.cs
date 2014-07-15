using System;
using System.Collections.Generic;
using RemsLogic.Model.Compliance;

namespace RemsLogic.Repositories
{
    public interface IComplianceRepository : IRepository<PrescriberEoc>
    {
        void LogEocComplianceEntry(long prescriberEocId, DateTime recordedAt);

        PrescriberEoc Find(long profileId, long drugId, long eocId);
        PrescriberEoc FindByLinkId(long profileId, long linkId);

        Eoc GetEoc(long eocId);
        Eoc GetEoc(long drugId, long questionId);
        
        IEnumerable<Eoc> GetEocs();
        IEnumerable<Eoc> GetByDrug(long drugId, bool requiredOnly = false);
        IEnumerable<Eoc> GetByDrugAndRole(long drugId, string role, bool requiredOnly = false);
        IEnumerable<PrescriberEoc> GetByPrescriberProfile(long profileId);

        IEnumerable<PrescriberEocLogEntry> GetComplianceLog(long prescriberEocID);

        void RemovePrescriberEocs(long profileId, long drugId);
    }
}
