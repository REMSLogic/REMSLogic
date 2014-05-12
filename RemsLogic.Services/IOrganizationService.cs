using System.Collections.Generic;
using RemsLogic.Model;

namespace RemsLogic.Services
{
    public interface IOrganizationService
    {
        // organizations
        void Save(Organization organization);
        void Delete(long id);
        Organization Get(long id);
        IEnumerable<Organization> GetAll();


        // facilities
        Facility GetFacility(long facilityId);
        void SaveFacility(Facility facility);
    }
}
