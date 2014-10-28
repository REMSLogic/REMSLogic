using RemsLogic.Model.Ecommerce;
using RemsLogic.Repositories;

namespace RemsLogic.Services
{
    public class AccountService : IAccountService
    {
        #region Member Variables
        public readonly IAccountRepository _accountRepo;
        #endregion

        #region Constructor
        public AccountService(
            IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }
        #endregion

        #region IAccountService Implementation
        public void Save(Account account)
        {
            _accountRepo.Save(account);
        }

        public Account GetByUserProfileId(long userProfileId)
        {
            return _accountRepo.GetByUserProfileId(userProfileId);
        }
        #endregion
    }
}
