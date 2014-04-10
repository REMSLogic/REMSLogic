using System.Collections.Generic;
using Framework.Security;

namespace Lib.Systems.Notifications
{
    public class AllUsers : IUserListGenerator
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
