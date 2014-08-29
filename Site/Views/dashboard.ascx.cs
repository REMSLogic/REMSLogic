using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Framework.Security;
using Lib.Data;
using Lib.Systems;
using RemsLogic.Model.UI;
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
            Framework.Security.User user = Manager.GetUser();

            IEnumerable<Widget> widgets = LoadWidgetsByRole().ToList();
            WidgetSettings settings = _widgetRepo.FindSettingsByUserId(user.ID ?? 0);
            
            if(settings == null)
            {
                InitializeWidgetSettings(widgets, ref settings);
                _widgetRepo.Save(settings);
            }

            DisplayWidgets(widgets, settings);
        }

        private IEnumerable<Widget> LoadWidgetsByRole()
        {
            if(Manager.HasRole("view_admin"))
                return _widgetRepo.FindByRoles(new[]{"view_admin"});

            if(Manager.HasRole("dashboard_drugcompany_view"))
                return _widgetRepo.FindByRoles(new[]{"dashboard_drugcompany_view"});

            if(Manager.HasRole("view_provider"))
                return _widgetRepo.FindByRoles(new[]{"view_provider"});

            if(Manager.HasRole("view_prescriber"))
                return _widgetRepo.FindByRoles(new[]{"view_prescriber"});

            return new List<Widget>();
        }

        private void InitializeWidgetSettings(IEnumerable<Widget> widgets, ref WidgetSettings settings)
        {
            settings = new WidgetSettings()
            {
                UserId = Manager.GetUser().ID ?? 0,
                Column1 = String.Empty,
                Column2 = String.Empty
            };

            List<string> widgetsToRemoveForEcomm = new List<string>
            {
                "Compliance Graph", 
                "Organization Summary", 
                "Prescriber Updates", 
                "User Activity Graph", 
                "Reports"
            };

            List<Widget> widgetList = widgets.ToList();
            ProviderUser providerUser = Security.GetCurrentProviderUser();

            // if it's an ecommerce user, filter out the widgets that class of user should
            // not see.
            if(providerUser != null && providerUser.Class == ProviderUser.ProviderClass.Ecommerce)
            {
                for(int i = 0; i < widgetList.Count; i++)
                {
                    if(widgetsToRemoveForEcomm.Contains(widgetList[i].Name))
                        widgetList.RemoveAt(i--);
                }
            }

            for(int i = 0; i < widgetList.Count(); i++)
            {
                Widget widget = widgetList[i];

                if((i+1)%2 != 0)
                    settings.Column1 += widget.Id+"|";
                else
                    settings.Column2 += widget.Id+"|";
            }
        }

        private void DisplayWidgets(IEnumerable<Widget> widgets, WidgetSettings settings)
        {
            List<int> column1 = settings.Column1
                .Split(new []{'|'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();

            List<int> column2 = settings.Column2
                .Split(new []{'|'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();

            foreach(int id in column1)
            {
                var widget = (from w in widgets where w.Id == id select w).Single();
                pnlColumn1.Controls.Add(WrapWidget(LoadControl(widget.Location), "widget_"+widget.Id));
            }

            foreach(int id in column2)
            {
                var widget = (from w in widgets where w.Id == id select w).Single();
                pnlColumn2.Controls.Add(WrapWidget(LoadControl(widget.Location), "widget_"+widget.Id));
            }
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