using System.Collections.Generic;
using Framework.Security;

namespace Lib.Systems.Notifications
{
    public class AllUsers : IDistributionListGenerator
    {
        public AllUsers(string settings)
        {
        }

        public IList<User> GetUsers()
        {
            return User.FindAll();
        }
    }
}
