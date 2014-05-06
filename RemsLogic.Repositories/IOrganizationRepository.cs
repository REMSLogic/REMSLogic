using System.Collections.Generic;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        // Address
        void SaveAddress(Address model);
        Address GetAddress(long addressId);

        // facilities
        void SaveFacility(Facility model);
        Facility GetFacility(long facilityId);
        IEnumerable<Facility> GetOrganizationFacilities(long organizationId);
    }
}
