using System.Collections.Generic;
using RemsLogic.Model;

namespace RemsLogic.Services
{
    public interface IComplianceService
    {
        void AddEocRequirmentsForPrescriber(long prescriberid, long drugId);
        Dictionary<Drug, List<PrescriberEoc>> GetEocRequirementsByPrescriberProfile(int profileId);
    }
}
