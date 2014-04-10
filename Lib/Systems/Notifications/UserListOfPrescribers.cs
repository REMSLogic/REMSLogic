using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Security;
using Lib.Data;

namespace Lib.Systems.Notifications
{
    public class UserListOfPrescribers : IDistributionListGenerator
    {
        private readonly string _settings;

        public UserListOfPrescribers(string settings)
        {
            _settings = settings;
        }

        public IList<User> GetUsers()
        {
            long listId;

            if(!long.TryParse(_settings, out listId))
                throw new ApplicationException("Settings do not match the expected format.");

            var temp = UserListItem.FindByList<Prescriber>(listId);

            return (from item in UserListItem.FindByList<Prescriber>(listId)
                    select item.Profile.User).ToList();
        }
    }
}
