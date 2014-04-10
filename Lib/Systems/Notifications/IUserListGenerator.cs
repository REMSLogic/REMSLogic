using System.Collections.Generic;
using Framework.Security;

namespace Lib.Systems.Notifications
{
    public interface IUserListGenerator
    {
        IList<User> GetUsers();
    }
}
