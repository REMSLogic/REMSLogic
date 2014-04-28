using RemsLogic.Model;
using System.Collections.Generic;

namespace RemsLogic.Repositories
{
    public interface IDrugListRepository : IRepository<DrugList>
    {
        long GetDrugListId(long profileId, string listType);

        List<long> GetFavList(long profileId);

        void AddDrugToFavoritesByProfileId(long profileId, long drugId);
        void RemoveDrugFromFavoritesByProfileId(long profileId, long drugId);
        void AddDrugToDrugListByProfileId(long profileId, long drugId);
        void RemoveDrugFromDrugListByProfileId(long profileId, long drugId);
    }
}
