using RemsLogic.Model;
using System.Collections.Generic;

namespace RemsLogic.Repositories
{
    public interface IDrugListRepository : IRepository<DrugList>
    {
        long GetDrugListId(long profileId, string listType);

        List<long> GetFavList(long profileId);

        void AddDrugToList(long profileId, long drugId, string listType);
        void RemoveDrugFromList(long profileId, long drugId, string listType);
    }
}
