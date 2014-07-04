using System;
using System.Collections.Generic;
using System.Linq;
using RemsLogic.Model;
using RemsLogic.Repositories;
using StructureMap;

namespace Site.Views.admin.video_links
{
    public partial class list : System.Web.UI.UserControl
    {
        public IList< RestrictedLink> Links {get; set;}

        protected void Page_Init(object sender, EventArgs e)
        {
            IRestrictedLinkRepository linkRepo = ObjectFactory.GetInstance<IRestrictedLinkRepository>();

            Links = linkRepo.GetAll().ToList();
        }
    }
}