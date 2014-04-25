using RemsLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemsLogic.Services
{
    public interface IDrugListService
    {
        //DrugList GetDrugListByUserId(long userId, ListType listType);

        DrugList GetDrugListByProfileId(long profileId, ListType listType);

        //void AddDrugToDrugListByUserId(long userId, long drugId, ListType listType);

        //void RemoveDrugFromDrugListByUserId(long userId, long drugId, ListType listType);

        void AddDrugToDrugListByProfileId(long profileId, long drugId, ListType listType);

        void RemoveDrugFromDrugListByProfileId(long profileId, long drugId, ListType listType);
    }
}
