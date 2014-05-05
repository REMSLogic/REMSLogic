using System;
using System.Collections.Generic;
using RemsLogic.Model.Compliance;

namespace RemsLogic.Repositories
{
    public interface IComplianceRepository : IRepository<PrescriberEoc>
    {
        void LogEocComplianceEntry(long prescriberEocId, DateTime recordedAt);

        PrescriberEoc Find(long profileId, long drugId, long eocId);

        Eoc GetEoc(long eocId);
        Eoc GetEoc(long drugId, long questionId);
        
        IEnumerable<Eoc> GetEocs();
        IEnumerable<Eoc> GetByDrug(long drugId);
        IEnumerable<Eoc> GetByDrugAndRole(long drugId, string p);
        IEnumerable<PrescriberEoc> GetByPrescriberProfile(long profileId);

        IEnumerable<PrescriberEocLogEntry> GetComplianceLog(long prescriberEocID);
    }
}
