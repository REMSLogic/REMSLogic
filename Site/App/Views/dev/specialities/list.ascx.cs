using System;
using System.Collections.Generic;
using Lib.Data;

namespace Site.App.Views.dev.specialities
{
    public partial class list : System.Web.UI.UserControl
    {
        public IList<Speciality> Items;

        protected void Page_Init(object sender, EventArgs e)
        {
            Items = Speciality.FindAll();
        }
    }
}