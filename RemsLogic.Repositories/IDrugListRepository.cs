using RemsLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemsLogic.Repositories
{
    public interface IDrugListRepository : IRepository<DrugList>
    {
        long GetDrugListId(long profileId);

        long GetFavListId(long profileId);

        List<long> GetFavList(long profileId);

        void AddDrugToFavoritesByProfileId(long profileId, long drugId);

        void RemoveDrugFromFavoritesByProfileId(long profileId, long drugId);

        void AddDrugToDrugListByProfileId(long profileId, long drugId);

        void RemoveDrugFromDrugListByProfileId(long profileId, long drugId);
    }
}
