using System;
using System.Collections.Generic;
using System.Text;
using Framework.Data;
using Lib.Data;

namespace Lib.Systems.Activity
{
    public class ActivityService
    {
        public const String StandardLogin = "Standard log in";
        public const String RedirectToUpdateDrugs = "Redirect to update drugs";

        public static void Record(long? userId, string sessionId, string action, object data)
        {
            // serialize the data to JSON
            string jsonData = (data != null)
                ? Newtonsoft.Json.JsonConvert.SerializeObject(data)
                : null;

            // now record the activity
            ActivityLogEntry entry = new ActivityLogEntry
            {
                UserID = userId,
                SessionID = sessionId,
                Action = action,
                Data = jsonData,
                RecordedAt = DateTime.Now
            };

            entry.Save();
        }

        public static List<KeyValuePair<DateTime,int>> SumByDate(string activityName, DateTime min_date, DateTime max_date)
        {
            List<KeyValuePair<DateTime,int>> ret = new List<KeyValuePair<DateTime,int>>();
            DateTime curDate = min_date.Date;
            Database db = Database.Get("FDARems");

            while(curDate <= max_date.Date)
            {
                StringBuilder sql = new StringBuilder();

                sql.Append("SELECT COUNT(*) FROM ActivityLog WHERE DATEDIFF(day, ActivityLog.RecordedAt, '"+curDate.ToString("yyyy-MM-dd")+"') = 0;");
                int count = db.ExecuteScalar<int>(sql.ToString());

                ret.Add(new KeyValuePair<DateTime,int>(curDate, count));
                curDate = curDate.AddDays(1);
            }

            return ret;
        }
    }
}
