using System;
using RemsLogic.Model;
using RemsLogic.Services;
using StructureMap;

namespace Site.App.Views.hcos.facilities
{
    public partial class edit : Lib.Web.AppControlPage
    {
        private readonly IOrganizationService _orgSvc;

        public long OrganizationId {get; set;}
        public Facility Facility {get; set;}

        public edit()
        {
            _orgSvc = ObjectFactory.GetInstance<IOrganizationService>();
        }

        protected void Page_Init(object sender, EventArgs e)
        {

            long facilityId = long.Parse(Request.QueryString["id"]);
            long orgId = long.Parse(Request.QueryString["provider-id"]);

            OrganizationId = orgId;
            Facility = _orgSvc.GetFacility(facilityId) ??
                new Facility
                {
                    OrganizationId = orgId,
                    Address = new Address()
                };
        }
    }
}