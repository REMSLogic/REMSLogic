using System.Configuration;
using System.Linq;
using System.Web;
using Framework.API;
using RemsLogic.Model.UI;
using RemsLogic.Repositories;

namespace Lib.API.Common
{
    public class Widgets : Base
    {
        [Method("Common/Widgets/UpdateLayout")]
        public static ReturnObject UpdateLayout(HttpContext context, int containerId, string[] widget)
        {
            IWidgetRepository widgetRepo = new WidgetRepository(ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString);

            Framework.Security.User user = Framework.Security.Manager.GetUser();
            WidgetSettings settings = widgetRepo.FindSettingsByUserId(user.ID ?? 0);

            if(containerId == 1)
                settings.Column1 = widget.Aggregate((i,j) => i+"|"+j);
            else
                settings.Column2 = widget.Aggregate((i,j) => i+"|"+j);

            widgetRepo.Save(settings);

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
