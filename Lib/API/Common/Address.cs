using System.Web;
using Framework.API;

namespace Lib.API.Common
{
    public class Address : Base
    {
        [Method("Common/Address/EditPrimary")]
        public static ReturnObject EditPrimary(HttpContext context, long id, string street_1, string street_2, string city, string state, string zip)
        {
            Data.Address item = new Data.Address(id);

            item.Street1 = street_1;
            item.Street2 = street_2;
            item.City = city;
            item.State = state;
            item.Zip = zip;
            item.Save();

            return new ReturnObject()
            {
                Result = item,
                Growl = new ReturnGrowlObject()
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject()
                    {
                        text = "You have successfully updated your primary address.",
                        title = "Address Updated"
                    }
                }
            };
        }
    }
}
