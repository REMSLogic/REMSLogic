using System;
using System.Collections.Generic;

namespace Site.App.Views.admin.security.prescribers
{
    public partial class list : Lib.Web.AppControlPage
    {
        public IList<Lib.Data.PrescriberProfile> PrescriberProfiles {get; set;}
        
        protected void Page_Init(object sender, EventArgs e)
        {
            PrescriberProfiles = Lib.Data.PrescriberProfile.FindAll();
        }
    }
}