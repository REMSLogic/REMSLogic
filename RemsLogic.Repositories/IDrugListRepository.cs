using RemsLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemsLogic.Repositories
{
    public interface IDrugListRepository : IRepository<DrugList>
    {
        DrugList GetFavoritesListByUserId(long userId);

        DrugList GetDrugListByUserId(long userId);

        DrugList GetFavoritesListByProfileId(long profileId);

        DrugList GetDrugListByProfileId(long profileId);

        long GetOrCreateNewFavoritesListByUserId(long userId);

        long GetOrCreateNewDrugListByUserId(long userId);

        long GetOrCreateNewFavoritesListByProfileId(long profileId);

        long GetOrCreateNewDrugListByProfileId(long profileId);

        void AddDrugToFavoritesByUserId(long userId, long drugId);

        void RemoveDrugFromFavoritesByUserId(long userId, long drugId);

        void AddDrugToFavoritesByProfileId(long profileId, long drugId);

        void RemoveDrugFromFavoritesByProfileId(long profileId, long drugId);

        void AddDrugToDrugListByUserId(long userId, long drugId);

        void RemoveDrugFromDrugListByUserId(long userId, long drugId);

        void AddDrugToDrugListByProfileId(long profileId, long drugId);

        void RemoveDrugFromDrugListByProfileId(long profileId, long drugId);
    }
}
