using RemsLogic.Model;
using RemsLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public DrugList GetDrugListByProfileId(long profileId, ListType listType)
        {
            DrugList retList = null;
            var eocs = _complianceSvc.GetEocsByPrescriberProfile(profileId, listType);

            if (eocs != null)
            {
                long listId = 0;
                switch(listType)
                {
                    case ListType.MYDRUGLIST:
                        listId = _drugListRepo.GetDrugListId(profileId);
                        List<long> favList = _drugListRepo.GetFavList(profileId);
                        retList = new DrugList
                        {
                            Id = listId,
                            ListName = "My Drugs",
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
                                     IsFav = favList.Contains(eoc.Key.Id)
                                 }).ToList()
                        };
                        break;
                    case ListType.FAVDRUGLIST:
                        listId = _drugListRepo.GetFavListId(profileId);
                        retList = new DrugList
                        {
                            Id = listId,
                            ListName = "Fav Drugs",
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
                        break;
                    default:
                        retList = new DrugList
                        {
                            Id = 0,
                            ListName = String.Empty,
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
                                     IsFav = false
                                 }).ToList()
                        };
                        break;
                }
            }

            return retList;
        }

        public void AddDrugToDrugListByProfileId(long profileId, long drugId, ListType listType)
        {
            switch (listType)
            {
                case ListType.MYDRUGLIST:
                    _drugListRepo.AddDrugToDrugListByProfileId(profileId, drugId);
                    break;
                case ListType.FAVDRUGLIST:
                    _drugListRepo.AddDrugToFavoritesByProfileId(profileId, drugId);
                    break;
            }
        }

        public void RemoveDrugFromDrugListByProfileId(long profileId, long drugId, ListType listType)
        {
            switch (listType)
            {
                case ListType.MYDRUGLIST:
                    _drugListRepo.RemoveDrugFromDrugListByProfileId(profileId, drugId);
                    break;
                case ListType.FAVDRUGLIST:
                    _drugListRepo.RemoveDrugFromFavoritesByProfileId(profileId, drugId);
                    break;
            }
        }
    }
}
