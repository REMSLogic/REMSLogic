using RemsLogic.Model.Ecommerce;

namespace RemsLogic.Services
{
    public interface IAccountService
    {
        void Save(Account account);
        Account GetByProviderUserId(long providerUserId);
    }
}
