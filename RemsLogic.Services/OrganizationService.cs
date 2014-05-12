using System.Collections.Generic;
using RemsLogic.Model;
using RemsLogic.Repositories;

namespace RemsLogic.Services
{
    public class OrganizationService : IOrganizationService
    {
        #region Member Variables
        private readonly IOrganizationRepository _orgRepo;
        #endregion

        #region Constructors
        public OrganizationService(
            IOrganizationRepository orgRepo)
        {
            _orgRepo = orgRepo;
        }
        #endregion

        #region IOrganizationRepository Implementation
        public void Save(Organization organization)
        {
            _orgRepo.Save(organization);
        }

        public void Delete(long id)
        {
            _orgRepo.Delete(id);
        }

        public Organization Get(long id)
        {
            return _orgRepo.Get(id);
        }

        public IEnumerable<Organization> GetAll()
        {
            return _orgRepo.GetAll();
        }

        public Facility GetFacility(long facilityId)
        {
            return _orgRepo.GetFacility(facilityId);
        }

        public void SaveFacility(Facility facility)
        {
            _orgRepo.SaveFacility(facility);
        }
        #endregion
    }
}
