using System.Web;
using Framework.API;

namespace Lib.API.Prescriber
{
    public class Prescribers : Base
    {
        [SecurityRole("view_prescriber")]
        [Method("Prescribers/Edit")]
        public static ReturnObject Edit(HttpContext context, long id, string npi, string state_id, long issuer, long speciality, long primary_contact_id, long primary_address_id)
        {
            Data.Prescriber prescriber = new Data.Prescriber(id);

            if(prescriber.ID == null)
                return new ReturnObject{Error = true, Message = "Invalid Request."};

            prescriber.NpiId = npi;
            prescriber.StateId = state_id;
            prescriber.StateIdIssuer = issuer;
            prescriber.SpecialityID = speciality;
            prescriber.Save();

            prescriber.Profile.PrimaryAddressID = primary_address_id;
            prescriber.Profile.PrimaryContactID = primary_contact_id;
            prescriber.Profile.Save();

            return new ReturnObject
            {
                Result = prescriber,
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject
                    {
                        text = "Your information has been updated.",
                        title = "Prescriber Updated"
                    }
                }
            };
        }
    }
}
