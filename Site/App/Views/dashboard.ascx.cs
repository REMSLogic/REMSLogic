using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
            // This logic is pretty ugly.  The HasRole method does some stuff behind
            // the scenes that makes calling something like:
            //    LoadWidget(_widgetRepo.FindByRoles(CurrentUser.Roles)
            //
            // more involved than it should be.

            if(Framework.Security.Manager.HasRole("view_admin"))
                LoadWidgets(_widgetRepo.FindByRoles(new[]{"view_admin"}));

            else if(Framework.Security.Manager.HasRole("dashboard_drugcompany_view"))
                LoadWidgets(_widgetRepo.FindByRoles(new[]{"dashboard_drugcompany_view"}));

            else if(Framework.Security.Manager.HasRole("view_provider"))
                LoadWidgets(_widgetRepo.FindByRoles(new[]{"view_provider"}));

            else if(Framework.Security.Manager.HasRole("view_prescriber"))
                LoadWidgets(_widgetRepo.FindByRoles(new[]{"view_prescriber"}));
        }

        private void LoadWidgets(IEnumerable<Widget> widgets)
        {
            List<Widget> widgetList = widgets.ToList();

            for(int i = 0; i < widgetList.Count(); i++)
            {
                Widget widget = widgetList[i];

                Control control = LoadControl(widget.Location);
                Control container = ((i+1)%2 == 0)? pnlColumn1 : pnlColumn2;

                container.Controls.Add(WrapWidget(control));
            }
        }

        private Control WrapWidget(Control widget)
        {
            HtmlGenericControl wrapper = new HtmlGenericControl("section");

            wrapper.Attributes["class"] = "portlet grid_6 bottom-marg";
            wrapper.Controls.Add(widget);
            return wrapper;
        }
    }
}