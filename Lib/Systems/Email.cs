using System;
using System.Collections.Generic;

namespace Lib.Systems
{
    public class Email
    {
        public static void SendPrescriberRegistration(string email_address, Guid guid)
        {
            // MJL 2013-10-25 - This is just a stub for now.
            // TODO: Implement this method
        }

        public static void SendNotification(Data.Notification n, Framework.Security.User u)
        {
            // MJL 2013-10-25 - This is just a stub for now.
            // TODO: Implement this method.  Check user settings and send email if enabled
        }
    }
}
