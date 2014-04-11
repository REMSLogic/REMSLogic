using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Framework.Security;
using Lib.Systems;
using RemsLogic.Model;
using RemsLogic.Repositories;

namespace Site.App.Views
{
    public partial class dashboard : Lib.Web.AppControlPage
    {
        private readonly IWidgetRepository _widgetRepo;

        public dashboard()
        {
            _widgetRepo = new WidgetRepository(ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // Try to find settings for the user
            User user = Manager.GetUser();
            WidgetSettings settings = _widgetRepo.FindSettingsByUserId(user.ID ?? 0);

            if(settings == null)
            {
                // no settings, so load the default and save the layout.

                // This logic is pretty ugly.  The HasRole method does some stuff behind
                // the scenes that makes calling something like:
                //    LoadWidget(_widgetRepo.FindByRoles(CurrentUser.Roles)
                //
                // more involved than it should be.

                if(Manager.HasRole("view_admin"))
                    settings = LoadWidgets(_widgetRepo.FindByRoles(new[]{"view_admin"}));

                else if(Manager.HasRole("dashboard_drugcompany_view"))
                    settings = LoadWidgets(_widgetRepo.FindByRoles(new[]{"dashboard_drugcompany_view"}));

                else if(Manager.HasRole("view_provider"))
                    settings = LoadWidgets(_widgetRepo.FindByRoles(new[]{"view_provider"}));

                else if(Manager.HasRole("view_prescriber"))
                    settings = LoadWidgets(_widgetRepo.FindByRoles(new[]{"view_prescriber"}));

                _widgetRepo.Save(settings);
            }
        }

        private WidgetSettings LoadWidgets(IEnumerable<Widget> widgets)
        {
            WidgetSettings settings = new WidgetSettings()
            {
                Userid = Manager.GetUser().ID ?? 0,
                Column1 = String.Empty,
                Column2 = String.Empty
            };

            List<Widget> widgetList = widgets.ToList();

            for(int i = 0; i < widgetList.Count(); i++)
            {
                Widget widget = widgetList[i];

                Control control = LoadControl(widget.Location);
                Control container;

                if((i+1)%2 != 0)
                {
                    container = pnlColumn1;
                    settings.Column1 += widget.Id+"|";
                }
                else
                {
                    container = pnlColumn1;
                    settings.Column2 += widget.Id+"|";
                }

                container.Controls.Add(WrapWidget(control, "widget_"+widget.Id));
            }

            return settings;
        }

        private Control WrapWidget(Control control, string id)
        {
            HtmlGenericControl wrapper = new HtmlGenericControl("section");

            wrapper.Attributes["class"] = "portlet grid_6 bottom-marg";
            wrapper.Attributes["id"] = id;
            wrapper.Controls.Add(control);
            return wrapper;
        }
    }
}