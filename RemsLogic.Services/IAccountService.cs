using RemsLogic.Model.Ecommerce;

namespace RemsLogic.Services
{
    public interface IAccountService
    {
        void Save(Account account);
        Account GetByUserProfileId(long userProfileId);
    }
}
