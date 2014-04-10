using System.Collections.Generic;
using Framework.Security;

namespace Lib.Systems.Notifications
{
    public interface IDistributionListGenerator
    {
        IList<User> GetUsers();
    }
}
