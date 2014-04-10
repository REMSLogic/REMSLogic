using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Site.App.Views
{
    public partial class dashboard : Lib.Web.AppControlPage
    {
        private const string WIDGET_PATH = "~/App/Controls/Widgets/";
        private const string WIDGET_EXT = ".ascx";

        protected void Page_Init(object sender, EventArgs e)
        {
            if(Framework.Security.Manager.HasRole("view_admin"))
                LoadAdminDashboard();

            else if(Framework.Security.Manager.HasRole("dashboard_drugcompany_view"))
                LoadDrugCompanyDashboard();

            else if(Framework.Security.Manager.HasRole("view_provider"))
                LoadProviderDashboard();

            else if(Framework.Security.Manager.HasRole("view_prescriber"))
                LoadPrescriberDashboard();

            Control reports = LoadControl("~/App/Controls/Widgets/Reports.ascx");
            pnlColumn1.Controls.Add(WrapWidget(reports));
        }

        private void LoadAdminDashboard()
        {
            LoadWidgets(new List<string>
            {
                "AccountStatistics",
                "UserActivityGraph",
                "PendingDrugChanges"
            });
        }

        private void LoadDrugCompanyDashboard()
        {
            LoadWidgets(new List<string>
            {
                "MyDrugs"
            });
        }

        private void LoadProviderDashboard()
        {
            List<string> widgets = new List<string>
            {
                "FacilityDrugList",
                "PrescriberUpdates",
                "OrganizationSummary",
                "UserActivityGraph"
            };

            // this one is kind of a speicial case.  apparently it is possible
            // to have both view_admin and the provider roles.  in that case
            // we don't want that admin to see the graph.
            if(!Framework.Security.Manager.HasRole("view_admin"))
                widgets.Insert(0, "ComplianceGraph");

            LoadWidgets(widgets);
        }

        private void LoadPrescriberDashboard()
        {
            LoadWidgets(new List<string>
            {
                "MyDrugList",
                "QuickLinks",
                "ComplianceStatus"
            });
        }

        private void LoadWidgets(List<string> widgets)
        {
            for(int i = 0; i < widgets.Count(); i++)
            {
                string widget = widgets[i];

                Control control = LoadControl(String.Format("{0}{1}{2}", WIDGET_PATH, widget, WIDGET_EXT));
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