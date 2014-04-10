using System.Collections.Generic;
using System.Linq;
using Framework.Security;
using Lib.Data;

namespace Lib.Systems.Notifications
{
    public class AllProviders : IUserListGenerator
    {
        public AllProviders(string settings)
        {
        }

        public IList<User> GetUsers()
        {
            return (from pu in ProviderUser.FindAll()
                    select pu.Profile.User).ToList();
        }
    }
}
