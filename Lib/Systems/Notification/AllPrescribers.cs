using System.Collections.Generic;
using Framework.Security;
using Lib.Data;

namespace Lib.Systems.Notifications
{
    public class AllPrescribers : IUserListGenerator
    {
        public AllPrescribers(string settings)
        {
        }

        public IList<User> GetUsers()
        {
            // TODO: Move to a UserProfile Systems class that handles all this back and forth from business objects and security objects
            var us = new List<User>();

            User currentUser = Framework.Security.Manager.GetUser();
            UserProfile userProfile = Lib.Data.UserProfile.FindByUser(currentUser);
            ProviderUser providerUser = ProviderUser.FindByProfile(userProfile);
            IList<PrescriberProfile> sendTo = PrescriberProfile.FindByProvider(providerUser.Provider);

            foreach (PrescriberProfile prescriberProf in sendTo)
            {
                if (prescriberProf == null || prescriberProf.Prescriber == null || prescriberProf.Prescriber.Profile == null)
                    continue;

                us.Add(prescriberProf.Prescriber.Profile.User);
            }

            return us;
        }
    }
}
