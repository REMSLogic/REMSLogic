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
        #endregion
    }
}
