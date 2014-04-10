using System.Web;
using Framework.API;
using Lib.Data;

namespace Lib.API.Prescriber
{
    public class Profile : Base
    {
        // I don't believe this method is used anywhere
        [Method("Prescriber/Profile/Update")]
        public static ReturnObject Update(HttpContext context, string agreeToTerms, string currentPassword, string newPassword)
        {
            return new ReturnObject
            {

                Result = null,
                Redirect = new ReturnRedirectObject
                {

                },
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject 
                    {
                        text = "Your profile has been updated.",
                        title = "Profile Updated"
                    }
                }
            };
        }

        [SecurityRole("view_prescriber")]
        [Method("Prescriber/Profile/Edit")]
        public static ReturnObject Edit(HttpContext context, long id, long prescriber_type)
        {
            PrescriberProfile profile = new PrescriberProfile(id);

            if(profile.ID == null)
                return new ReturnObject{Error = true, Message = "Invalid Request."};

            profile.PrescriberTypeID = prescriber_type;
            profile.Save();

            return new ReturnObject
            {
                Result = profile,
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject
                    {
                        text = "Your information has been updated.",
                        title = "Prescriber Profile Updated"
                    }
                }
            };
        }
    }
}
