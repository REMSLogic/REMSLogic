using System.Collections.Generic;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public interface IComplianceRepository
    {
        IEnumerable<Eoc> GetByDrugId(long drugId);
        IEnumerable<PrescriberEoc> GetByPrescriberProfile(long profileId);
    }
}
