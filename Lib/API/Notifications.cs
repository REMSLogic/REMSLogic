using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Framework.API;
using Framework.Security;
using Lib.Data;
using Lib.Systems.Notifications;

namespace Lib.API
{
    public class Notifications : Base
    {
        private class UserComparer : IEqualityComparer<User>
        {
            public bool Equals(User x, User y)
            {
                return x.ID == y.ID;
            }

            public int GetHashCode(User obj)
            {
                return obj.ID.GetHashCode();
            }
        }

        /// <summary>
        /// This method creates an instance of a notification and then sends it
        /// to the correct users
        /// </summary>
        /// <param name="context"></param>
        /// <param name="back_hash"></param>
        /// <param name="send_to_source">The DistributionList.ID that the User.ID came from</param>
        /// <param name="send_to_id">User.ID or 0 for all users</param>
        /// <param name="notification_body">Message to send</param>
        /// <param name="is_important"></param>
        /// <param name="link">
        ///     For external links this is a URL.  For internal links this is simply
        ///     the user friendly name of the drug of drug company.
        /// </param>
        /// <param name="link_type">
        ///     This is used to tell the system how to interpret the link_valu
        ///     coming in from the view.
        /// </param>
        /// <param name="link_value">
        ///     This is used for drugs and drug companies.  It contains the id
        ///     for the selected object.  For external links this is null.
        /// </param>
        /// <returns></returns>
        [Method("Notifications/Create")]
        public static ReturnObject Create(HttpContext context, string back_hash, string send_to_id, string notification_body, string is_important = null, string link = null, string link_type = null, long? link_value = null )
        {
            List<NotificationRecipient> recipients = Newtonsoft.Json.JsonConvert.DeserializeObject<List<NotificationRecipient>>(send_to_id);
            List<User> users = new List<User>();

            // MJL - 2014-01-21 - Groups are just specialized distribution lists, so the code
            // below accounts for sending a notification to a single user, a type of user, a group,
            // or a specified distrubiton list.  It also allows for multiple recipients.
            foreach(NotificationRecipient nr in recipients)
            {
                switch(nr.RecipientType)
                {
                    case NotificationRecipient.Type.User:
                        users.Add(new Framework.Security.User(nr.Id));
                        break;

                    case NotificationRecipient.Type.DistributionList:
                        users.AddRange((new DistributionList(nr.Id)).Users);
                        break;

                    case NotificationRecipient.Type.Facility:
                        IList<PrescriberProfile> prescriberProfiles = PrescriberProfile.FindByFacility(nr.Id);
                        IList<ProviderUser> providerUsers = ProviderUser.FindByFacility(nr.Id);

                        foreach(PrescriberProfile profile in prescriberProfiles)
                        {
                            if(profile.PrescriberID == null || profile.PrescriberID == 0)
                                continue;

                            Data.Prescriber prescriber = new Data.Prescriber(profile.PrescriberID);
                            
                            if(prescriber.Profile == null || prescriber.Profile.User == null)
                                continue;

                            users.Add(prescriber.Profile.User);
                        }

                        foreach(ProviderUser providerUser in providerUsers)
                        {
                            if(providerUser.Profile == null || providerUser.Profile.User == null)
                                continue;

                            users.Add(providerUser.Profile.User);
                        }
                        break;
                }
            }

            // make sure we don't send a message to the same user twice
            users = (from u in users select u).Distinct(new UserComparer()).ToList();

            Notification n;
            bool important_flag = (is_important == "yes");

            // MJL - 2013-11-06 - This logic is necissary because the link is built
            // inside of Systems.Notifications.Create.  In each case below the only
            // real difference is the way the link is put together.  Handling that
            // logic in the system specific API would cut down on the number of Create
            // overloads needed.
            switch(link_type)
            {
                case "drug":
                    if(link_value == null)
                        throw new ApplicationException("Invalid link type and value combination.");

                    n = NotificationService.Create("Drug notification", notification_body,important_flag, NotificationService.DataType.Drug, link_value.Value);
                    break;

                case "company":
                    if(link_value == null)
                        throw new ApplicationException("Invalid link type and value combination.");

                    n = NotificationService.Create("Drug company notification", notification_body,important_flag, NotificationService.DataType.DrugCompany, link_value.Value);
                    break;

                case "external":
                    n = NotificationService.Create("Basic notification", notification_body,important_flag, link);
                    break;

                default:
                    n = NotificationService.Create("Basic notification", notification_body, important_flag);
                    break;
            }

            NotificationService.Send(n, users);

            return new ReturnObject
            {
                Result = n,
                Redirect = new ReturnRedirectObject
                {
                    Hash = back_hash
                },
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject
                    {
                        text = "Your notification has been sent.",
                        title = "Notification Sent"
                    }
                }
            };
        }

        //[SecurityRole("view_notifications")]
        [Method("Notifications/MarkRead")]
        public static ReturnObject MarkRead(HttpContext context, long id)
        {
            bool success = NotificationService.MarkAsRead(id);

            return new ReturnObject
            {
                Result = null,
                Error = !success,
                Message = (success)
                    ? String.Empty
                    : "Invalid Notification"
            };
        }

        [Method("Notifications/Archive")]
        public static ReturnObject Archive(HttpContext context, long id)
        {
            bool success = NotificationService.Archive(id);

            return new ReturnObject
            {
                Result = null,
                Error = !success,
                Message = (success)
                    ? String.Empty
                    : "Invalid Notification"
            };
        }

        [Method("Notifications/Delete")]
        public static ReturnObject Delete(HttpContext context, long id)
        {
            bool success = NotificationService.Delete(id);

            if(!success)
            {
                return new ReturnObject
                {
                    Result = null,
                    Error = true,
                    Message = "Failed to delete notification."
                };
            }

            return new ReturnObject
            {
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject
                    {
                        text = "You have successfully deleted a notification.",
                        title = "Notification Removed"
                    }
                },
                Actions = new List<ReturnActionObject>
                {
                    new ReturnActionObject
                    {
                        Ele = "#notifications-table tr[data-id=\""+id+"\"]",
                        Type = "remove"
                    }
                },

            };
        }

        #region Auto-complete support methods
        [Method("Notifications/LinkAutocomplete")]
        public static ReturnObject LinkAutocomplete(HttpContext context)
        {
            string link_type = context.Request.QueryString["link_type"];
            string term = context.Request.QueryString["term"];

            SetupResponseForJson(context);

            switch(link_type)
            {
                case "external":
                    context.Response.Write("[]");
                    break;

                case "drug":
                    ListDrugs(context.Response, term);
                    break;

                case "company":
                    ListDrugCompanies(context.Response, term);
                    break;
            }
            
            context.Response.End();
            return null;
        }

        [Method("Notifications/SendToAutocomplete")]
        public static ReturnObject SendToAutocomplete(HttpContext context, string term)
        {
            SetupResponseForJson(context);
            context.Response.Write("[");

            User user = Framework.Security.Manager.GetUser();
            UserProfile userProfile = UserProfile.FindByUser(user);
            ProviderUser providerUser = ProviderUser.FindByProfile(userProfile);

            IList<DistributionList> distLists = (from dl in NotificationService.GetDistributionLists(Framework.Security.Manager.GetUser())
                                                 where dl.Name.ToUpper().Contains(term.ToUpper())

                                                 select dl).ToList();
            IList<ProviderFacility> facilities = (from f in ProviderFacility.FindAllByProviderUser(providerUser)
                                                  orderby f.Name
                                                  select f).ToList();

            IList<UserProfile> matchList = (from up in UserProfile.FindAll()
                                            where up.PrimaryContact.Name.ToUpper().Contains(term.ToUpper())
                                            select up).Take(10).ToList();

            foreach(DistributionList distList in distLists)
            {
                context.Response.Write(String.Format("{{\"label\": \"{0} (dist. list)\", \"value\": {{\"type\":{1},\"id\":{2}}}}}",
                    distList.Name,
                    (int)NotificationRecipient.Type.DistributionList,
                    distList.ID));

                if(matchList.Any() || facilities.Any())
                    context.Response.Write(",");
            }

            foreach(ProviderFacility facility in facilities)
            {
                context.Response.Write(String.Format("{{\"label\": \"{0} (facility)\", \"value\": {{\"type\":{1},\"id\":{2}}}}}", 
                    facility.Name, 
                    (int)NotificationRecipient.Type.Facility,
                    facility.ID));

                if(matchList.Any())
                    context.Response.Write(",");
            }

            foreach(UserProfile userProf in matchList)
            {
                context.Response.Write(String.Format("{{\"label\": \"{0} (user)\", \"value\": {{\"type\":{1},\"id\":{2}}}}}", 
                    userProf.PrimaryContact.Name, 
                    (int)NotificationRecipient.Type.User,
                    userProf.UserID));

                if(userProf != matchList.Last())
                    context.Response.Write(",");
            }

            context.Response.Write("]");
            context.Response.End();
            return null;
        }

        [SecurityRole("view_provider")]
        [Method("Notifications/CreatePrescriberDistributionList")]
        public static ReturnObject CreatePrescriberDistributionList(HttpContext context, string list_name, long[] prescriber_ids = null)
        {
            if(String.IsNullOrEmpty(list_name))
                return Error("You must give the list a name.");

            if(prescriber_ids == null || prescriber_ids.Count() == 0)
                return Error("You must select at least one prescriber.");

            User currentUser = Framework.Security.Manager.GetUser();
            UserProfile profile = UserProfile.FindByUser(currentUser.ID ?? 0);

            // Create the user list
            UserList userList = new UserList()
            {
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                DataType = UserList.Types.Prescriber,
                Name = list_name,
                System = false,
                UserProfileID = profile.ID
            };
            userList.Save();

            if(userList.ID == 0)
                return Error("Failed to save the user list.");

            // add the prescribers to the user list
            for(int i = 0; i < prescriber_ids.Count(); i++)
            {
                UserListItem item = new UserListItem()
                {
                    ItemID = prescriber_ids[i],
                    ListID = userList.ID ?? 0,
                    Order = i
                };
                item.Save();
            }

            // now save the distribution list
            DistributionList distList = new DistributionList
            {
                ListGeneratorType = typeof(UserListOfPrescribers).FullName,
                Name = userList.Name,
                UserProfileId = profile.ID,
                Settings = (userList.ID ?? 0).ToString(CultureInfo.InvariantCulture),
            };
            distList.Save();

            if(distList.ID == 0)
                return Error("Failed to save the distribution list.");

            return new ReturnObject
            {
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject
                    {
                        text = "Your distribution list has been created.",
                        title = "List Created."
                    }
                }
            };
        }

        [SecurityRole("view_provider")]
        [Method("Notifications/AddPrescribersToDistributionList")]
        public static ReturnObject AddPrescribersToDistributionList(HttpContext context, long? list_id = null, long[] prescriber_ids = null)
        {
            if(list_id == null || list_id == 0)
                return Error("You must select a valid distribution list");

            if(prescriber_ids == null || prescriber_ids.Count() == 0)
                return Error("You must select at least one prescriber.");

            DistributionList distList = new DistributionList(list_id);
            UserList userList = new UserList(long.Parse(distList.Settings));
            IList<Data.Prescriber> prescribers = userList.GetItems<Data.Prescriber>();

            int startIndex = prescribers.Count;

            // add the prescribers to the user list
            for(int i = 0; i < prescriber_ids.Count(); i++)
            {
                if(prescribers.Any(x => x.ID == prescriber_ids[i]))
                    continue;

                UserListItem item = new UserListItem
                {
                    ItemID = prescriber_ids[i],
                    ListID = userList.ID ?? 0,
                    Order = i+startIndex
                };
                item.Save();
            }

            return new ReturnObject
            {
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject
                    {
                        text = "The prescriber(s) were added to the selected list.",
                        title = "List Updated."
                    }
                }
            };
        }

        [SecurityRole("view_provider")]
        [Method("Notifications/UpdateDistributionList")]
        public static ReturnObject UpdateDistributionList(HttpContext context, long? list_id = null, long[] prescriber_ids = null)
        {
            if(list_id == null || list_id == 0)
                return Error("You must select a valid distribution list");

            if(prescriber_ids == null || prescriber_ids.Count() == 0)
                return Error("You must select at least one prescriber.");

            DistributionList distList = new DistributionList(list_id);
            UserList userList = new UserList(long.Parse(distList.Settings));

            userList.ClearItems();

            int startIndex = 1;

            // add the prescribers to the user list
            for(int i = 0; i < prescriber_ids.Count(); i++)
            {
                UserListItem item = new UserListItem
                {
                    ItemID = prescriber_ids[i],
                    ListID = userList.ID ?? 0,
                    Order = i+startIndex
                };
                item.Save();
            }

            return new ReturnObject
            {
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject
                    {
                        text = "The selected list has been updated.",
                        title = "List Updated."
                    }
                }
            };
        }
        #endregion

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

        private static void ListDrugs(HttpResponse response, string term)
        {
            IEnumerable<Drug> drugs = (from d in Drug.FindAll()
                                       where d.GenericName.Contains(term)
                                       select d).Take(10);

            response.Write("[");

            foreach(Drug d in drugs)
            {
                response.Write("{\"label\":\""+d.GenericName+"\",\"value\":"+d.ID+"}");

                if(d != drugs.Last())
                    response.Write(",");
            }

            response.Write("]");
        }

        private static void ListDrugCompanies(HttpResponse response, string term)
        {
            IEnumerable<DrugCompany> companies = (from c in DrugCompany.FindAll()
                                                  where c.Name.Contains(term)
                                                  select c).Take(10);

            response.Write("[");

            foreach(DrugCompany company in companies)
            {
                response.Write("{\"label\":\""+company.Name+"\",\"value\":"+company.ID+"}");

                if(company != companies.Last())
                   response.Write(",");
            }

            response.Write("]");
        }

        private static ReturnObject Error(string errorMessage)
        {
            return new ReturnObject
            {
                Error = true,
                Message = errorMessage
            };
        }
        #endregion
    }
}
