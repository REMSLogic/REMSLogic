using System;
using System.Collections.Generic;
using Lib.Data;

namespace Site.Views.admin.security.ecommerce
{
    public partial class list : Lib.Web.AppControlPage
    {
        public IList<ProviderUser> ProviderUsers {get; set;}

        protected void Page_Load(object sender, EventArgs e)
        {
            ProviderUsers = ProviderUser.FindByClass(ProviderUser.ProviderClass.Ecommerce);
        }
    }
}