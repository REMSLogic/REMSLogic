using System;
using Lib.Systems;

namespace Site.App.Controls.Widgets
{
    public partial class ComplianceGraph : System.Web.UI.UserControl
    {
        public long ProviderId {get; set;}

        protected void Page_Init(object sender, EventArgs e)
        {
			var provider = Security.GetCurrentProvider();
			if( provider == null )
				ProviderId = 0;
			else
				ProviderId = provider.ID ?? 0;
        }
    }
}