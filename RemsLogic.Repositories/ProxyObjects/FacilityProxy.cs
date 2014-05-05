using RemsLogic.Model;

namespace RemsLogic.Repositories.ProxyObjects
{
    public class FacilityProxy : Facility
    {
        private readonly IOrganizationRepository _orgRepo;

        public FacilityProxy(IOrganizationRepository orgRepo)
        {
            _orgRepo = orgRepo;
        }

        public override Address Address
        {
            get{return base.Address ?? (base.Address = _orgRepo.GetAddress(AddressId));}
            set{base.Address = value;}
        }

        public override Organization Organization
        {
            get{return base.Organization ?? (base.Organization = _orgRepo.Get(OrganizationId));}
            set{base.Organization = value;}
        }
    }
}
