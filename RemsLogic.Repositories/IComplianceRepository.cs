using System.Collections.Generic;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public interface IComplianceRepository : IRepository<PrescriberEoc>
    {
        PrescriberEoc Find(long profileId, long drugId, long eocId);

        IEnumerable<Eoc> GetByDrugId(long drugId);
        IEnumerable<PrescriberEoc> GetByPrescriberProfile(long profileId);
    }
}
