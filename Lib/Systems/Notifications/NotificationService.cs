using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using Framework.Security;
using Lib.Data;

namespace Lib.Systems.Notifications
{
    public class NotificationService
    {
        #region Enums
        public enum DataType
        {
            Drug,
            DrugCompany
        }
        #endregion

        #region Constants
        // MJL 2013-01-09 - I went with constants because enums can't be strings and
        // the email templating system wants a string for the tempalte name.  This
        // saves a lot of switching or if-ing.
        //
        // Using the nested class is just syntatical sugar.  I like to group my constants
        // so that I can do NotificationServices.Templates.HCOReminder rather than putting
        // the word "Template" in the name of each constant.
        public static class Templates
        {
            public const String Generic = "Generic";
            public const String NewsAndUpdates = "NewsAndUpdates";
            public const String HCOReminder = "HCOReminder";
        }
        #endregion

        #region Notification Management
        public static bool MarkAsRead(long id)
        {
            User u = Framework.Security.Manager.GetUser();

            if(u == null || u.ID == null)
                return false;

            NotificationInstance ni = NotificationInstance.Find(u.ID.Value, id);

            ni.Read = DateTime.Now;
            ni.Save();
            return true;
        }

        public static bool Archive(long id)
        {
            User u = Framework.Security.Manager.GetUser();

            if(u == null || u.ID == null)
                return false;

            NotificationInstance ni = NotificationInstance.Find(u.ID.Value, id);

            ni.Archived = true;
            ni.Save();
            return true;
        }

        public static bool ArchiveAllInstancesForUser(long id)
        {
            User u = Framework.Security.Manager.GetUser();

            if(u == null || u.ID == null)
                return false;

            IList<NotificationInstance> nis = NotificationInstance.FindAllInstancesForUser(u.ID.Value, id);

            foreach(var ni in nis)
            {
                ni.Archived = true;
                ni.Save();
            }
            return true;
        }

        public static bool Delete(long id)
        {
            User u = Framework.Security.Manager.GetUser();

            if(u == null || u.ID == null)
                return false;

            NotificationInstance ni = NotificationInstance.Find(u.ID.Value, id);

            ni.Archived = true;
            ni.Deleted = true;
            ni.Save();
            return true;
        }

		public static Data.Notification Create(string title, string message, bool important, DataType data_type, long data_id)
		{
			var dt = "";
			switch(data_type)
			{
			case DataType.Drug:
				dt = "drug";
				break;
			case DataType.DrugCompany:
				dt = "drug-company";
				break;
			default:
				throw new ArgumentOutOfRangeException("Unrecognized value for data_type");
			}

			var ret = new Data.Notification() {
				DataID = data_id,
				DataType = dt,
				Important = important ? "yes" : "no",
				Link = BuildNotificationUrl(data_type, data_id),
				Message = message,
				Sent = DateTime.Now,
				Title = title,
			};

			ret.Save();

			return ret;
		}

		public static Data.Notification Create(string title, string message, bool important, string link)
		{
			var ret = new Data.Notification()
			{
				DataType = "link",
				Important = important ? "yes" : "no",
				Link = link,
				LinkType = "link",
				Message = message,
				Sent = DateTime.Now,
				Title = title,
			};

			ret.Save();

			return ret;
		}

		public static Data.Notification Create(string title, string message, bool important)
		{
			var ret = new Data.Notification()
			{
				DataType = "link",
				Important = important ? "yes" : "no",
				Link = null,
				LinkType = null,
				Message = message,
				Sent = DateTime.Now,
				Title = title,
			};

			ret.Save();

			return ret;
		}

		private static string BuildNotificationUrl(DataType t, long id)
		{
            /*
			switch (t)
			{
			case DataType.Drug:
				return "Default.aspx#common/drugs/detail?id=" + id.ToString();

			case DataType.DrugCompany:
				return "Default.aspx#admin/drugs/companies/view?id=" + id.ToString();

			default:
				throw new ArgumentOutOfRangeException("Unrecognized value for data_type");
			}
            */

            // MJL - this code taks into account role and produces a link accordingly.
            return Links.View(t, id);
		}

		public static void Send(Data.Notification n, User u, string template = null)
		{

			if( n == null || n.ID == null || u == null || u.ID == null )
				return;

			var ni = new Data.NotificationInstance() {
				Archived = false,
				NotificationID = n.ID.Value,
				Read = null,
				UserID = u.ID.Value
			};
			ni.Save();

            UserPreferences pref = UserPreferences.FindByUser(u);

            /*
            if((pref != null && pref.EmailNotifications) && !String.IsNullOrEmpty(u.Email))
            {
                Dictionary<string, object> data = new Dictionary<string,object>();

                data.Add("Message", n.Message);
                data.Add("Year", DateTime.Now.Year.ToString(CultureInfo.InvariantCulture));
                data.Add("EmailAddress", u.Email ?? "");

                Framework.Email.SendTemplate(template ?? Templates.Generic, data, new Framework.Email.TemplateOverrides
                    {
                        To = new List<MailAddress>{new MailAddress(u.Email, "place holder")}
                        //To = new List<MailAddress>{new MailAddress("mike.lindegarde@gmail.com", "Mike Lindegarde")}
                    });
            }
            */
		}

		public static void Send(Data.Notification n, IEnumerable<User> us, string template = null)
		{
			foreach( var u in us )
				Send(n, u, template);
		}

		public static void Send(Data.Notification n, Group g, string template = null)
		{
			var us = User.FindByGroup(g);
			foreach (var u in us)
				Send(n, u, template);
		}

		public static void Send(Data.Notification n, IEnumerable<Group> gs, string template = null)
		{
			foreach (var g in gs)
				Send(n, g, template);
		}

		public static void SendAll(Data.Notification n, string template = null)
		{
			var us = User.FindAll();
			foreach (var u in us)
				Send(n, u, template);
        }
        #endregion

        #region Distribution List Management
        public static List<DistributionList> GetDistributionLists(User user)
        {
            // TODO: Fix this

            // Right now the distribution lists are hard coded. They should be pulled from the database.
            // There needs to be a table called GroupDistributionLists that can be used to determine
            // which distribution lists each group has access to.

            User u = Framework.Security.Manager.GetUser();
            UserProfile profile = UserProfile.FindByUser(u);

            Group admin = new Group(1);
            Group prescribers = new Group(3);
            Group providers = new Group(4);
            Group dev = new Group(6);

            List<DistributionList> ret = new List<DistributionList>();

            // first setup the system default lists
            if(user.IsInGroup(providers))
            {
                ret.Add(new DistributionList(1));
            }

            if(user.IsInGroup(admin) || user.IsInGroup(dev))
            {
                ret.Add(new DistributionList(2));
                ret.Add(new DistributionList(3));
            }

            // now add any user created distribution lists
            ret.AddRange(DistributionList.FindByUserProfile(profile));
            return ret;
        }

        public static DistributionList GetDistributionList(long id)
        {
            return new DistributionList(id);
        }
        #endregion
    }
}
