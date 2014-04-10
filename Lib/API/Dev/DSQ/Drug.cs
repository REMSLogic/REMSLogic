using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;
using Framework.Security;
using Lib.Data;
using Lib.Systems.Notifications;
using Utilities;

namespace Lib.API.Dev.DSQ
{
	public class Drug : Base
	{
		[SecurityRole("drug_edit")]
		[Method("Dev/DSQ/Drug/Edit")]
		public static ReturnObject Edit(HttpContext context, string generic_name, long class_id, long? id = null, string rems_reason = null, string indication = null, long? system_id = null, string rems_website = null, string fda_number = null, DateTime? rems_approved = null, DateTime? rems_updated = null, bool active = false, bool new_version = false, string message = null, Newtonsoft.Json.Linq.JArray questions = null)
		{
			Lib.Data.Drug item = null;
			if (id > 0)
				item = new Lib.Data.Drug(id);
			else
				item = new Lib.Data.Drug();

			var u = Framework.Security.Manager.GetUser();
			bool approved = true;

			if( u.IsInGroup(Framework.Security.Group.FindByName("drugcompany")) )
			{
				approved = false;
				new_version = true;
			}

			if (new_version && string.IsNullOrEmpty(message))
				return new ReturnObject { Error = true, Message = "You must enter a message when you save a new version." };

			if( Lib.Systems.Drugs.HasPendingChanges(item) )
				return new ReturnObject { Error = true, Message = "You can not modify a drug that has pending changes." };

			item.GenericName = generic_name;
			item.RemsReason = rems_reason;
			item.Indication = indication;
			item.ClassID = class_id;
			item.SystemID = system_id;
			item.RemsProgramUrl = rems_website;
			item.FdaApplicationNumber = fda_number;
			item.RemsApproved = rems_approved;
			item.RemsUpdated = rems_updated;
			item.Active = active;
			item.UpdatedByID = u.ID.Value;
			item.Updated = DateTime.Now;
			
			item.Save();

			if (id <= 0)
			{
				new_version = true;
				message = "New Drug Created.";
			}

			Data.DrugVersion ver = null;
			if (new_version)
			{
				var last_version = Data.DrugVersion.FindLatestByDrug(item);

				ver = new Data.DrugVersion();
				ver.DrugID = item.ID.Value;
				ver.Message = message;
				ver.Version = ((last_version == null) ? 1 : last_version.Version + 1);
				ver.Updated = DateTime.Now;
				ver.UpdatedBy = u.ID.Value;
				ver.Status = (approved ? "Approved" : "Pending");

				if( approved )
				{
					ver.Reviewed = DateTime.Now;
					ver.ReviewedBy = u.ID.Value;
				}

                // MJL - changed to always send notification no matter what (even if
                // it was auto approved.
                StringBuilder notificationMsg = new StringBuilder();

                notificationMsg.Append(item.GenericName);
                notificationMsg.Append(" has new pending changes for you to review (version ");
                notificationMsg.Append(ver.Version);
                notificationMsg.Append(" - ");
                notificationMsg.Append(ver.Updated.ToShortDateString());
                notificationMsg.Append(")");

                // MJL - if the version was auto-approved (because user is admin), note
                // that in the message.
                if(approved)
                    notificationMsg.Append(". This version was automatically approved.");

                var n = NotificationService.Create(
                    item.GenericName + " Update", 
                    notificationMsg.ToString(), 
                    true, 
                    NotificationService.DataType.Drug, 
                    item.ID.Value);
				NotificationService.Send(n, Group.FindByName("admin"), NotificationService.Templates.NewsAndUpdates);

				ver.Save();
			}

			if (questions != null)
			{
				var ids = new List<long>();

				foreach (var o in questions)
				{
					var oid = o.Value<string>("id");
					var ovalue = o.Value<object>("value");

					long lid;
					if (!long.TryParse(oid, out lid))
						continue;

					var question = new Lib.Data.DSQ.Question(lid);
					if (!question.ID.HasValue || question.ID.Value != lid)
						continue;

					ids.Add(lid);

					string v;

					#region Parse Value from Request
					if (ovalue == null)
					{
						v = null;
					}
					else if (ovalue.GetType() == typeof(string))
					{
						v = (string)ovalue;
					}
					else if (ovalue.GetType() == typeof(Newtonsoft.Json.Linq.JValue))
					{
						var t = (Newtonsoft.Json.Linq.JValue)ovalue;

						v = (string)t.Value;
					}
					else if (ovalue.GetType() == typeof(Newtonsoft.Json.Linq.JArray))
					{
						var t = (Newtonsoft.Json.Linq.JArray)ovalue;

						v = "";

						for (int i = 0; i < t.Count; i++)
						{
							if (!string.IsNullOrEmpty(v))
								v += "\n";

							v += (string)t[i];
						}
					}
					else
					{
						return new ReturnObject() { Result = ovalue, Error = true, Message = "Invalid value for question [" + oid + "]. {" + question.Text + "} {" + ovalue.GetType().FullName + "}" };
					}

					if (v != null && v.Trim() == "")
						v = null;
					#endregion

					var answer = Lib.Data.DSQ.Answer.FindByDrug(item, question);

                    /*
                    // if it's a text area, process any Markdown that might be present
                    if(question.FieldType == "TextArea")
                        v = markdownSvc.ToHtml(v);
                    */

					if (answer == null || answer.DrugID != item.ID.Value || answer.QuestionID != lid)
					{
						answer = new Data.DSQ.Answer();
						answer.DrugID = item.ID.Value;
						answer.QuestionID = lid;
						answer.Value = null;
						answer.Save();
					}

					if( new_version && answer.Value != v )
					{
						var av = new Lib.Data.DSQ.AnswerVersion();
						av.DSQAnswerID = answer.ID.Value;
						av.Value = v;
						av.Version = ver.Version;
						av.Save();
					}

					if( approved )
					{
						answer.Value = v;
						answer.Save();
					}
				}

				if( approved )
				{
					var lqs = Lib.Data.DSQ.Question.FindAll();

					foreach (var lq in lqs)
					{
						if (lq == null || lq.ID == null || ids.Contains(lq.ID.Value))
							continue;

						var a = Lib.Data.DSQ.Answer.FindByDrug(item, lq);

						if (a == null)
							continue;

						a.Delete();
					}
				}
			}

			if( approved )
			{
				item.DetermineEOC();
                Lib.Systems.Lists.UpdateDrugLists( item );

                SendNotification(item);
			}

			return new ReturnObject()
			{
				Result = item,
				Redirect = new ReturnRedirectObject
				{
					Hash = "admin/dsq/edit?id=" + item.ID.Value.ToString(CultureInfo.InvariantCulture)
				},
				Growl = new ReturnGrowlObject
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject
					{
						text = "You have successfully saved this drug.",
						title = "Drug Saved"
					}
				},
				Actions = new List<ReturnActionObject>
				{
					new ReturnActionObject
					{
						Type = "reset-unsaved"
					}
				}
			};
		}

		[SecurityRole("drug_version_approve", "You do not have permission to approve pending drug changes.")]
		[Method("Dev/DSQ/Drug/ApproveVersion")]
		public static ReturnObject ApproveVersion(HttpContext context, long drug_id, long drug_version_id, string message = null)
		{
			var drug = new Lib.Data.Drug(drug_id);
			var drug_version = new Lib.Data.DrugVersion(drug_version_id);

			if( !drug.ID.HasValue || drug.ID.Value != drug_id || drug.ID.Value != drug_version.DrugID )
				return new ReturnObject { Error = true, Message = "Invalid Drug Version." };

			if( drug_version.Status != "Pending" )
				return new ReturnObject { Error = true, Message = "Can not approve versions that are not pending." };

			var u = Framework.Security.Manager.GetUser();

			var changes = Lib.Data.DSQ.AnswerVersion.FindByDrugVersion(drug.ID.Value, drug_version.Version);

			foreach( var change in changes )
			{
				var ans = new Lib.Data.DSQ.Answer(change.DSQAnswerID);
				ans.Value = change.Value;
				ans.Save();
			}

			drug_version.Status = "Approved";
			drug_version.ReviewedBy = u.ID;
			drug_version.Reviewed = DateTime.Now;
			drug_version.Save();

			drug.DetermineEOC();

			Lib.Systems.Lists.UpdateDrugLists( drug );
            SendNotification(drug);

			if( !string.IsNullOrWhiteSpace( message ) )
			{
				var n = NotificationService.Create(
						drug.GenericName + " Approved",
						"You changes were approved, <br /><br />" + message,
						true,
						NotificationService.DataType.Drug,
						drug.ID.Value );
				NotificationService.Send( n, new Framework.Security.User( drug_version.UpdatedBy ), NotificationService.Templates.NewsAndUpdates );
			}

			return new ReturnObject()
			{
				Result = null,
				Growl = new ReturnGrowlObject
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject
					{
						text = "You have successfully approved changes to this drug.",
						title = "Changes Approved"
					}
				},
				Redirect = new ReturnRedirectObject
				{
					Hash = "admin/drugs/drugs/list?pending=true"
				}
			};
		}

		[SecurityRole("drug_version_approve", "You do not have permission to deny pending drug changes.")]
		[Method("Dev/DSQ/Drug/DenyVersion")]
		public static ReturnObject DenyVersion(HttpContext context, long drug_id, long drug_version_id, string message)
		{
			var drug = new Lib.Data.Drug(drug_id);
			var drug_version = new Lib.Data.DrugVersion(drug_version_id);

			if (!drug.ID.HasValue || drug.ID.Value != drug_id || drug.ID.Value != drug_version.DrugID)
				return new ReturnObject { Error = true, Message = "Invalid Drug Version." };

			if (drug_version.Status != "Pending")
				return new ReturnObject { Error = true, Message = "Can not deny versions that are not pending." };

			var u = Framework.Security.Manager.GetUser();
			
			drug_version.Status = "Denied";
			drug_version.ReviewedBy = u.ID;
			drug_version.Reviewed = DateTime.Now;
			drug_version.DenyReason = message;
			drug_version.Save();

			var n = NotificationService.Create(
						drug.GenericName + " Denied",
						"You changes were denied, please see below.<br /><br />" + message,
						true,
						NotificationService.DataType.Drug,
						drug.ID.Value );
			NotificationService.Send( n, new Framework.Security.User( drug_version.UpdatedBy ), NotificationService.Templates.NewsAndUpdates );

			return new ReturnObject
			{
				Result = null,
				Growl = new ReturnGrowlObject
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject
					{
						text = "You have denied the pending changes to this drug.",
						title = "Changes Denied"
					}
				},
				Redirect = new ReturnRedirectObject
				{
					Hash = "admin/drugs/drugs/list?pending=true"
				}
			};
		}

        private static void SendNotification(Lib.Data.Drug drug)
        {
            StringBuilder notifcationMsg = new StringBuilder();

            notifcationMsg.Append(drug.GenericName);
            notifcationMsg.Append(" has been updated. Please log into the system and update your drug selections.");

            Notification notification = NotificationService.Create("Drug Updated", notifcationMsg.ToString(), true);

			var lists = UserList.FindByItemAndType( drug.ID.Value, UserList.Types.Drug );
            List<User> users = (from l in lists
                                select new UserProfile(l.UserProfileID).User).ToList();

			var hco_ut = Lib.Data.UserType.FindByName( "provider" );
			var hco_users = Lib.Data.UserProfile.FindByUserType( hco_ut.ID.Value );

			users.AddRange( from up in hco_users
                            select up.User );

			var formulations = Lib.Data.DrugFormulation.FindByDrug( drug );
			var comp_ids = new List<long>();

			foreach( var f in formulations )
			{
				if( comp_ids.Contains( f.DrugCompanyID ) )
					continue;

				comp_ids.Add( f.DrugCompanyID );

				var comp_users = Lib.Data.DrugCompanyUser.FindByDrugCompany( f.DrugCompanyID );

				users.AddRange( from compuser in comp_users
								select compuser.Profile.User );
			}

            NotificationService.Send(notification, users.Distinct(), NotificationService.Templates.NewsAndUpdates);
        }
	}
}
