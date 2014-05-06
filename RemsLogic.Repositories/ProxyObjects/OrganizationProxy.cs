using System.Linq;
using RemsLogic.Model;

namespace RemsLogic.Repositories.ProxyObjects
{
    public class OrganizationProxy : Organization
    {
        private readonly IOrganizationRepository _orgRepo;

        public OrganizationProxy(IOrganizationRepository orgRepo)
        {
            _orgRepo = orgRepo;
        }

        public override Facility PrimaryFacility
        {
            get{return base.PrimaryFacility ?? (base.PrimaryFacility = _orgRepo.GetFacility(PrimaryFacilityId));}
            set{base.PrimaryFacility = value;}
        }

        public override System.Collections.Generic.List<Facility> Facilities
        {
            get{return base.Facilities ?? (base.Facilities = _orgRepo.GetOrganizationFacilities(Id).ToList());}
            set{base.Facilities = value;}
        }
    }
}
