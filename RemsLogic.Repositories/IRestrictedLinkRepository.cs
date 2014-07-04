using System;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public interface IRestrictedLinkRepository : IRepository<RestrictedLink>
    {
        RestrictedLink GetByToken(Guid token);
    }
}
