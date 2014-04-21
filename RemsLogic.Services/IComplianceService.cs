using System.Collections.Generic;
using RemsLogic.Model;

namespace RemsLogic.Services
{
    public interface IComplianceService
    {
        PrescriberEoc Find(long profileId, long drugId, long eocId);
        void Save(PrescriberEoc prescriberEoc);

        void AddEocsToPrescriberProfile(long profileId, long drugId);
        void RemoveEocsFromPrescriberProfile(long profileId, long drugId);

        Dictionary<Drug, List<PrescriberEoc>> GetEocsByPrescriberProfile(int profileId);
    }
}
