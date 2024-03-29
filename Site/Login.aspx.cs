﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Security;
using Lib.Data;
using Lib.Systems;
using Lib.Systems.Activity;
using Lib.Systems.Notifications;
using RemsLogic.Model.Ecommerce;
using RemsLogic.Services;
using StructureMap;

namespace Site.App
{
	public partial class Login : System.Web.UI.Page
	{
        private readonly IAccountService _accountSvc;

		public string msg;

        public Login()
        {
            _accountSvc = ObjectFactory.GetInstance<IAccountService>();
        }

		protected void Page_Load(object sender, EventArgs e)
		{
			if( !string.IsNullOrEmpty( Request["username"] ) )
				CheckLogin();
            else if(!string.IsNullOrEmpty(Request["login-token"]))
                CheckToken();
			else
				CheckCookie();
		}

		protected void CheckLogin()
		{
            // With the return inside of each "if", the "else" isn't needed.
			if( string.IsNullOrEmpty( Request["username"] ) || string.IsNullOrEmpty( Request["password"] ) )
			{
				msg = "You must enter your username and password.";
				return;
			}
			else if (string.IsNullOrEmpty(Request["tos"]) || Request["tos"] != "1")
			{
				msg = "You must agree to the Terms and Conditions.";
				return;
			}
			else if (!Framework.Security.Manager.Login(Request["username"], Request["password"]))
			{
				msg = "Invalid Username/Password.";
				return;
			}
			else if (!Framework.Security.Manager.HasRole("view_app"))
			{
				Framework.Security.Manager.Logout();
				msg = "You don't have permissions for that.";
				return;
			}

			if( Request["remember"] == "1" )
				Framework.Security.Manager.GenerateLoginCookie();

			User user = Manager.GetUser();
            UserProfile userProfile = Security.GetCurrentProfile();

            // if it's an ecommerce user we need to ensure the account is enabled and not expired
            Account account = _accountSvc.GetByUserProfileId(userProfile.ID ?? 0);

            if(account != null)
            {
                if(!account.IsEnabled)
                {
				    Framework.Security.Manager.Logout();
				    msg = "Your account has been disabled.";
				    return;
                }

                if(account.ExpiresOn < DateTime.Now)
                {
				    Framework.Security.Manager.Logout();
				    msg = "Your account has exired.";
				    return;
                }

                if(!userProfile.IsWizardComplete)
                {
                    if(userProfile.UserType.Name == "provider")
                    {
                        Lib.API.Common.DrugList.AddDrugToList(Context, 137);
                        Lib.API.Common.DrugList.AddDrugToFavorites(Context, 137);
                        
                        Lib.API.Common.DrugList.AddDrugToList(Context, 27);
                        Lib.API.Common.DrugList.AddDrugToFavorites(Context, 27);

                        Lib.API.Common.DrugList.AddDrugToList(Context, 88);
                        Lib.API.Common.DrugList.AddDrugToFavorites(Context, 88);

                        Lib.API.Common.DrugList.AddDrugToList(Context, 85);
                        Lib.API.Common.DrugList.AddDrugToFavorites(Context, 85);

                        userProfile.IsWizardComplete = true;
                        userProfile.Save();
                    }
                    else
                        Response.Redirect("Ecommerce.aspx#ecommerce/wizards/registration-wizard?token=925bcbf9-4ca5-4b24-b9d5-f14eb4cd75c4");
                }
            }

			string hash = "";

			if( !string.IsNullOrEmpty(Request["frag"]) )
				hash = "#"+Request["frag"];

            // MJL 2013-11-02 - Check to see if the user needs to say yes or no to any
            // new drugs
			// TJM 2014-02-03 - Check moved to AppControlPage, exception added to prescriber/selections/edit.ascx.cs
            /*if(u.IsInGroup(Group.FindByName("prescribers")) && Lib.Systems.Drugs.SelectionsUpdateRequired(u))
            {
                ActivityService.Record(u.ID, Session.SessionID, ActivityService.RedirectToUpdateDrugs, null);
                Response.Redirect("~/Locked.aspx#prescriber/selections/edit");
                return;
            }*/

            ActivityService.Record(user.ID, Session.SessionID, ActivityService.StandardLogin, null);

            // MJL 2014-01-13 - Monk, I'm using this for testing.  Please don't remove
            // this.  I will remove it once we are confident the notification emails are
            // working as expected.

            // Notification n = NotificationService.Create("Test Notification", "Test Notification (User Login)", false);
            // NotificationService.Send(n, u, NotificationService.Templates.Generic);

			Response.Redirect( "~/Default.aspx"+hash, true );
		}

        /// <summary>
        /// This method is used to accomidate users coming to the website from the link
        /// emailed to them when a provider adds a prescriber to the system.  Effectively
        /// this is an alternative login method.
        /// 
        /// There are some security concerns here.
        /// </summary>
        protected void CheckToken()
        {
            // With the return inside of each "if", the "else" isn't needed.
			if( string.IsNullOrEmpty( Request["username"] ) || string.IsNullOrEmpty( Request["password"] ) )
			{
				msg = "You must enter your username and password.";
				return;
			}
			else if (string.IsNullOrEmpty(Request["tos"]) || Request["tos"] != "1")
			{
				msg = "You must agree to the Terms and Conditions.";
				return;
			}
			else if (!Framework.Security.Manager.Login(Request["username"], Request["password"]))
			{
				msg = "Invalid Username/Password.";
				return;
			}
			else if (!Framework.Security.Manager.HasRole("view_app"))
			{
				Framework.Security.Manager.Logout();
				msg = "You don't have permissions for that.";
				return;
			}

			if( Request["remember"] == "1" )
				Framework.Security.Manager.GenerateLoginCookie();

			var u = Framework.Security.Manager.GetUser();

			string hash = "";

			if( !string.IsNullOrEmpty(Request["frag"]) )
				hash = "#"+Request["frag"];

            // MJL 2013-11-02 - Check to see if the user needs to say yes or no to any
            // new drugs
			// TJM 2014-02-03 - Check moved to AppControlPage, exception added to prescriber/selections/edit.ascx.cs
            /*if(u.IsInGroup(Group.FindByName("prescribers")) && Lib.Systems.Drugs.SelectionsUpdateRequired(u))
            {
                Response.Redirect("~/Default.aspx#prescriber/selections");
                return;
            }*/

			Response.Redirect( "~/Default.aspx"+hash, true );
        }

		protected void CheckCookie()
		{
			if( Framework.Security.Manager.Login() )
				Response.Redirect( "~/Default.aspx", true );
		}
	}
}