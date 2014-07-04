using System;
using RemsLogic.Model;
using RemsLogic.Repositories;
using StructureMap;

namespace Site
{
    public partial class Video : System.Web.UI.Page
    {
        private readonly IRestrictedLinkRepository _linkRepo;

        public bool ShowVideo {get; set;}

        public Video()
        {
            _linkRepo = ObjectFactory.GetInstance<IRestrictedLinkRepository>();

            ShowVideo = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["token"] == null)
                return;

            Guid token = Guid.Empty;

            if(!Guid.TryParse(Request.QueryString["token"], out token))
                return;

            RestrictedLink link = _linkRepo.GetByToken(token);

            if(link.ExpirationDate.Date <= DateTime.UtcNow)
                return;

            ShowVideo = true;
        }
    }
}