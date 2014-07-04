using System;
using RemsLogic.Model;

namespace Site.Views.admin.video_links
{
    public partial class edit : Lib.Web.AppControlPage
    {
        public RestrictedLink Link {get; set;}

        protected void Page_Init(object sender, EventArgs e)
        {
            Link = new RestrictedLink();
        }
    }
}