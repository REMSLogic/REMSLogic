using System.Collections.Generic;
using RemsLogic.Model;
using RemsLogic.Repositories;
using System;
using System.Linq;

namespace RemsLogic.Services
{
    public class DrugListService : IDrugListService
    {
        private readonly IDrugListRepository _drugListRepo;
        private readonly IDrugRepository _drugRepo;
        private readonly IComplianceRepository _complianceRepo;
        private readonly IComplianceService _complianceSvc;

        public DrugListService( IDrugListRepository drugListRepo, IDrugRepository drugRepo, IComplianceRepository compRepo)
        {
            _drugListRepo = drugListRepo;
            _drugRepo = drugRepo;
            _complianceRepo = compRepo;
            _complianceSvc = new ComplianceService(_drugRepo, _complianceRepo);
        }

        public DrugList GetDrugListByProfileId(long profileId, string listType)
        {
            if(listType == DrugListType.Undefined)
                throw new ArgumentException("ListType.Undefined is not currently supported.");

            DrugList retList;

            var eocs = _complianceSvc.GetEocsStatus(profileId, listType);

            if(eocs == null)
                return null;

            // set IsFav = true assuming it's the favorites list
            retList = new DrugList
            {
                Id = _drugListRepo.GetDrugListId(profileId, listType),
                ListName = listType,
                UserProfileId = profileId,
                Drugs =
                    (from eoc in eocs
                        select new DrugListItem
                        {
                            Id = eoc.Key.Id,
                            DrugName = eoc.Key.GenericName,
                            DateAdded = DateTime.Now,
                            DrugEocsCount = eoc.Value.Count(),
                            UserEocsCount = eoc.Value.Count(x => x.CompletedAt.HasValue),
                            IsFav = true
                        }).ToList()
            };

            // if it isn't the favorites list then we actaully need to do some work
            // to determin what the IsFav value realy should be.
            if(listType != DrugListType.Favorites)
            {
                IEnumerable<long> favList = _drugListRepo.GetFavList(profileId);
                retList.Drugs.ForEach(x => x.IsFav = favList.Contains(x.Id));
            }

            return retList;
        }

        public void AddDrugToDrugListByProfileId(long profileId, long drugId, string listType)
        {
            switch (listType)
            {
                case DrugListType.MyDrugs:
                    _drugListRepo.AddDrugToDrugListByProfileId(profileId, drugId);
                    break;
                case DrugListType.Favorites:
                    _drugListRepo.AddDrugToFavoritesByProfileId(profileId, drugId);
                    break;
            }
        }

        public void RemoveDrugFromDrugListByProfileId(long profileId, long drugId, string listType)
        {
            switch (listType)
            {
                case DrugListType.MyDrugs:
                    _drugListRepo.RemoveDrugFromDrugListByProfileId(profileId, drugId);
                    break;
                case DrugListType.Favorites:
                    _drugListRepo.RemoveDrugFromFavoritesByProfileId(profileId, drugId);
                    break;
            }
        }
    }
}
