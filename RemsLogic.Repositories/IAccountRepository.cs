using RemsLogic.Model.Ecommerce;

namespace RemsLogic.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetByProviderUserId(long providerUserId);
    }
}
