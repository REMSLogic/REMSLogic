using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Web;

namespace Lib.Web
{
    public class AppControlPage : BaseControl
    {
        protected bool IgnoreDrugSelection;
        protected Lib.Data.UserProfile Profile;

        public AppControlPage() : base()
        {
            IgnoreDrugSelection = false;
            if( User != null )
                Profile = Lib.Data.UserProfile.FindByUser( User );
        }

        protected override void OnInit(EventArgs e)
        {
            RequireRole( "view_app" );

            base.OnInit( e );

            if( !IgnoreDrugSelection && Profile.UserType.Name == "prescriber" && Lib.Systems.Drugs.SelectionsUpdateRequired() )
            {
                RedirectHash( "prescriber/selections/edit" );
                return;
            }
        }
    }
}
