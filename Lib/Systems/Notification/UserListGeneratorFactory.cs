using System;

namespace Lib.Systems.Notifications
{
    public class UserListGeneratorFactory
    {
        public IUserListGenerator GetGenerator(string type_name, string settings)
        {
            Type type = Type.GetType(type_name);

            if(type == null)
                throw new ApplicationException("Failed to find type "+type_name);

            return (IUserListGenerator)Activator.CreateInstance(type, new object[]{settings});
        }
    }
}
