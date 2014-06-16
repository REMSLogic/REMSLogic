using System;
using System.Web;
using Framework.API;

namespace Lib.API.Dev.DSQ
{
    public class SectionSettings : Base
    {
        [SecurityRole("view_dev")]
        [Method("Dev/DSQ/SectionSettings/Update")]
        public static ReturnObject Update(HttpContext context, long sectionId, long drugId, bool isEnabled)
        {
            Lib.Data.DSQ.SectionSettings settings = Lib.Data.DSQ.SectionSettings.Get(sectionId, drugId) ?? new Lib.Data.DSQ.SectionSettings
                {
                    SectionId = sectionId,
                    DrugId = drugId
                };

            settings.IsEnabled = isEnabled;
            settings.Save();

            SetupResponseForJson(context);

            context.Response.Write(String.Format("{{\"sectionId\":{0},\"drugId\":{1},\"isEnabled\":{2}}}", 
                settings.SectionId, 
                settings.DrugId, 
                settings.IsEnabled ? "true" : "false"));
            context.Response.End();
            return null;
        }

        #region Utility Methods
        private static void SetupResponseForJson(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "application/json";

            context.Response.ClearHeaders();
            context.Response.AppendHeader("Cache-Control", "no-cache"); //HTTP 1.1
            context.Response.AppendHeader("Cache-Control", "private"); // HTTP 1.1
            context.Response.AppendHeader("Cache-Control", "no-store"); // HTTP 1.1
            context.Response.AppendHeader("Cache-Control", "must-revalidate"); // HTTP 1.1
            context.Response.AppendHeader("Cache-Control", "max-stale=0"); // HTTP 1.1 
            context.Response.AppendHeader("Cache-Control", "post-check=0"); // HTTP 1.1 
            context.Response.AppendHeader("Cache-Control", "pre-check=0"); // HTTP 1.1 
            context.Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.1 
            context.Response.AppendHeader("Keep-Alive", "timeout=3, max=993"); // HTTP 1.1 
            context.Response.AppendHeader("Expires", "Mon, 26 Jul 1997 05:00:00 GMT"); // HTTP 1.1
        }
        #endregion
    }
}
