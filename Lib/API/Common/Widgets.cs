using System.Web;
using Framework.API;

namespace Lib.API.Common
{
    public class Widgets : Base
    {
        [Method("Common/Widgets/UpdateLayout")]
        public static ReturnObject UpdateLayout(HttpContext context, string[] widget)
        {
            return new ReturnObject()
            {
                Growl = new ReturnGrowlObject()
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject()
                    {
                        text = "Your changes have been saved.",
                        title = "Layout Saved"
                    }
                }
            };
        }
    }
}
