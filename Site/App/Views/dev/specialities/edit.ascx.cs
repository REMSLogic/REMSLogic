using System;
using Lib.Data;

namespace Site.App.Views.dev.specialities
{
    public partial class edit : System.Web.UI.UserControl
    {
        public Speciality Item {get; set;}

        protected void Page_Init(object sender, EventArgs e)
        {
            string strID = Request.QueryString["id"];
            long id;

            if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
                Item = new Speciality();
            else
                Item = new Speciality(id);
        }
    }
}