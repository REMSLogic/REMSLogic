using System.Collections.Generic;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public interface IDrugRepository : IRepository<Drug>
    {
        IEnumerable<Drug> GetByPrescriberProfile(long profileId);

        IEnumerable<Drug> GetFavByPrescriberProfile(long profileId);
    }
}
