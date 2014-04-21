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

        bool AddDrugToFavorites(long userId, long drugId);

        bool RemoveDrugFromFavorites(long userId, long drugId);

        bool AddDrugToDrugList(long userId, long drugId);

        bool RemoveDrugFromDrugList(long userId, long drugId);
    }
}
