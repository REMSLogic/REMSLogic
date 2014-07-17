using System;
using System.Collections.Generic;
using RemsLogic.Model;
using RemsLogic.Model.Compliance;

namespace RemsLogic.Services
{
    public interface IComplianceService
    {
        PrescriberEoc Find(long profileId, long drugId, long eocId);
        void RecordCompliance(PrescriberEoc prescriberEoc);
        void LogEocComplianceEntry(long precriberEocId, DateTime recordedAt);

        void AddEocsToProfile(long profileId, List<string> roles, long drugId);
        void RemoveEocsFromPrescriberProfile(long profileId, long drugId);
        void RebuildEocs(long profileId, List<string> roles, long drugId);

        Dictionary<Drug, List<PrescriberEoc>> GetEocsStatus(long profileId, string listType);

        Eoc GetEoc(long eocId);
        IEnumerable<Eoc> GetEocs();
        IEnumerable<Eoc> GetByDrug(long drugId);
    }
}
