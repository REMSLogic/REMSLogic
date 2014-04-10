using System;
using System.Collections.Generic;
using Framework.Security;
using Lib.Data;

namespace Lib.Systems.Notifications
{
    public class NotificationServices
    {
        #region Enums
        public enum DataType
        {
            Drug,
            DrugCompany
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
			switch (t)
			{
			case DataType.Drug:
				return "Default.aspx#common/drugs/detail?id=" + id.ToString();

			case DataType.DrugCompany:
				return "Default.aspx#admin/drugs/companies/edit?id=" + id.ToString();

			default:
				throw new ArgumentOutOfRangeException("Unrecognized value for data_type");
			}
		}

		public static void Send(Data.Notification n, User u)
		{
			var ni = new Data.NotificationInstance() {
				Archived = false,
				NotificationID = n.ID.Value,
				Read = null,
				UserID = u.ID.Value
			};
			ni.Save();

			Email.SendNotification(n, u);
		}

		public static void Send(Data.Notification n, IEnumerable<User> us)
		{
			foreach( var u in us )
				Send(n, u);
		}

		public static void Send(Data.Notification n, Group g)
		{
			var us = User.FindByGroup(g);
			foreach (var u in us)
				Send(n, u);
		}

		public static void Send(Data.Notification n, IEnumerable<Group> gs)
		{
			foreach (var g in gs)
				Send(n, g);
		}

		public static void SendAll(Data.Notification n)
		{
			var us = User.FindAll();
			foreach (var u in us)
				Send(n, u);
        }
        #endregion

        #region Distribution List Management
        public static List<DistributionList> GetDistributionLists(User user)
        {
            // TODO: Fix this

            // Right now the distribution lists are hard coded. They should be pulled from the database.
            // There needs to be a table called GroupDistributionLists that can be used to determine
            // which distribution lists each group has access to.

            Group admin = new Group(1);
            Group prescribers = new Group(3);
            Group providers = new Group(4);
            Group dev = new Group(6);

            List<DistributionList> ret = new List<DistributionList>();

            if(user.IsInGroup(providers))
                ret.Add(new DistributionList(1));

            if(user.IsInGroup(admin) || user.IsInGroup(dev))
            {
                ret.Add(new DistributionList(2));
                ret.Add(new DistributionList(3));
            }

            return ret;
        }

        public static DistributionList GetDistributionList(long id)
        {
            return new DistributionList(id);
        }
        #endregion
    }
}
