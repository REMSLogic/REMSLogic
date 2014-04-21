using System.Collections.Generic;
using RemsLogic.Model;

namespace RemsLogic.Services
{
    public interface IComplianceService
    {
        void AddEocRequirmentsForPrescriberProfile(long profileId, long drugId);
        Dictionary<Drug, List<PrescriberEoc>> GetEocRequirementsByPrescriberProfile(int profileId);
    }
}
