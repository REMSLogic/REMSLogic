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

        bool CreateNewFavoritesList(long userId);

        bool CreateNewDrugList(long userId);

        void AddDrugToFavorites(long userId, long drugId);

        void RemoveDrugFromFavorites(long userId, long drugId);

        void AddDrugToDrugList(long userId, long drugId);

        void RemoveDrugFromDrugList(long userId, long drugId);
    }
}
