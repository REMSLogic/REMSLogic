using System.Collections.Generic;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public interface IDrugRepository : IRepository<Drug>
    {
        IEnumerable<Drug> GetByList(long profileId, string listType);
    }
}
