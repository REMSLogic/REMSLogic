using System;
using System.Collections.Generic;
using System.Linq;
using RemsLogic.Model;
using RemsLogic.Services;
using StructureMap;

namespace Site.App.Views.admin.security.providers
{
    public partial class list : Lib.Web.AppControlPage
    {
        public List<Organization> Organizations {get; set;}

        protected void Page_Init(object sender, EventArgs e)
        {
            IOrganizationService orgSvc = ObjectFactory.GetInstance<IOrganizationService>();

            Organizations = orgSvc.GetAll().ToList();
        }
    }
}