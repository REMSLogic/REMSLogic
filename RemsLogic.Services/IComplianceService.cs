using System.Collections.Generic;
using RemsLogic.Model;

namespace RemsLogic.Services
{
    public interface IComplianceService
    {
        void AddEocsToPrescriberProfile(long profileId, long drugId);
        Dictionary<Drug, List<PrescriberEoc>> GetEocsByPrescriberProfile(int profileId);
    }
}
