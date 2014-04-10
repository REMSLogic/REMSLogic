using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;
using Framework.Security;
using Lib.Data;

namespace Lib.API.Common
{
	public class Account : Base
	{
		[SecurityRole("view_app")]
		[Method("Common/Account/Profile")]
		public static ReturnObject Profile(HttpContext context, long id, string email, string current_password = null, string new_password = null, string confirm_password = null)
		{
			var item = new Framework.Security.User( id );

			if( id != Framework.Security.Manager.GetUser().ID.Value )
				return new ReturnObject() { Error = true, Message = "Invalid user specified." };

			item.Email = email;
			item.Save();

			if( !string.IsNullOrEmpty( current_password ) || !string.IsNullOrEmpty( new_password ) || !string.IsNullOrEmpty( confirm_password ) )
			{
				if( string.IsNullOrEmpty(current_password) )
					return new ReturnObject() { Error = true, Message = "You must enter your current password to change your password." };

				if( string.IsNullOrEmpty( new_password ) || string.IsNullOrEmpty( confirm_password ) )
					return new ReturnObject() { Error = true, Message = "You must enter a new password and confirm it to change your password." };

				if( new_password != confirm_password )
					return new ReturnObject() { Error = true, Message = "Your new passwords do not match." };

				if( !Framework.Security.Manager.ChangePassword(item,current_password,new_password) )
					return new ReturnObject() { Error = true, Message = "You did not enter your current password correctly." };
			}

			var ret = new ReturnObject()
			{
				Result = item,
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully updated your profile.",
						title = "Profile Saved"
					}
				}
			};

			return ret;
		}

		[SecurityRole( "view_app" )]
		[Method("Common/Account/Prefs")]
		public static ReturnObject Prefs(HttpContext context, long? id = null, /*int email_frequency,*/ bool email_notifications = false)
		{
            var item = new UserPreferences(id);

            User currentUser = Framework.Security.Manager.GetUser();

            if(currentUser == null || currentUser.ID == null)
                return new ReturnObject { Error = true, Message = "Invalid user specified." };

            item.UserId = currentUser.ID.Value;
            item.EmailNotifications = email_notifications;
            item.Save();

			var ret = new ReturnObject()
			{
				Result = null,
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully updated your preferences.",
						title = "Preferences Saved"
					}
				}
			};

			return ret;
		}
	}
}
