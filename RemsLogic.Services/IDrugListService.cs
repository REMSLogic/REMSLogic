﻿using System.Collections.Generic;
using RemsLogic.Model;

namespace RemsLogic.Services
{
    public interface IDrugListService
    {
        DrugList GetDrugListByProfileId(long profileId, string listType);

        void AddDrugToDrugListByProfileId(long profileId, List<string> roles, long drugId, string listType);
        void RemoveDrugFromDrugListByProfileId(long profileId, long drugId, string listType);
    }
}
