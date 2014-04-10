using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;
using Lib.Queries;
using Lib.Systems.Activity;

namespace Lib.API.App
{
    public class Stats : Base
    {
        [SecurityRole("view_app")]
        [Method("App/Stats/SumActivityByName")]
        public static ReturnObject SumActivityByName(HttpContext context, string name, DateTime min_date, DateTime max_date)
        {
            StringBuilder json = new StringBuilder();

            SetupResponseForJson(context);

            var totals = ActivityService.SumByDate(name, min_date, max_date);

            json.Append("[");

            for(int i = 0; i < totals.Count; i++)
            {
                var total = totals[i];

                json.Append("[\""+total.Key+"\","+total.Value+"]");

                if(i != totals.Count-1)
                    json.Append(",");
            }

            json.Append("]");

            context.Response.Write(json.ToString());
            context.Response.End();
            return null;
        }

        [SecurityRole("view_app")]
        [Method("App/Stats/Compliance")]
        public static ReturnObject Compliance(HttpContext context, long providerId, DateTime min_date, DateTime max_date)
        {
            Data.Provider provider = new Data.Provider(providerId);
            StringBuilder json = new StringBuilder();

            SetupResponseForJson(context);

            int prescriberCount = Data.PrescriberProfile.FindByProvider(provider).Count();

            // Prescriber Enrollment
            int ncPrescriber = new NonCompliantPrescriberEnrollment().Run(providerId);
            float ncPrescriberPct = ((float)ncPrescriber/(float)prescriberCount)*100.0F;

            if(ncPrescriberPct > 100)
                ncPrescriberPct = 100F;

            float cPrescriberPct = 100F-ncPrescriberPct;

            // Patient Enrollment
            int ncPatient = new NonCompliantPatientEnrollment().Run(providerId);
            float ncPatientPct = ((float)ncPatient/(float)prescriberCount)*100.0F;

            if(ncPatientPct > 100F)
                ncPatientPct = 100F;

            float cPatientPct = 100F-ncPatientPct;

            // Education
            int ncEnrollment = new NonCompliantEducation().Run(providerId);
            float ncEducationPct = ((float)ncEnrollment/(float)prescriberCount)*100F;

            if(ncEducationPct > 100F)
                ncEducationPct = 100F;

            float cEducation = 100F-ncEducationPct;

            // the json response
            json.Append("{");
            json.Append("\"compliant\":["+cPrescriberPct+","+cPatientPct+","+cEducation+"]");
            json.Append(",");
            json.Append("\"nonCompliant\":["+ncPrescriberPct+","+ncPatientPct+","+ncEducationPct+"]");
            json.Append("}");

            context.Response.Write(json.ToString());
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
