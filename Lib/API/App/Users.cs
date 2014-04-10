using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;
using Lib.Systems.Tasks;
using CertificationReminder = Lib.Systems.Tasks.CertificationReminder;

namespace Lib.API.App
{
	public class Users : Base
	{
		[SecurityRole( "view_prescriber" )]
		[Method( "App/Users/Certified" )]
		public static ReturnObject Certified( HttpContext context, long drug_id, string eoc_name )
		{
			var d = new Data.Drug( drug_id );
			if( d.ID == null || d.ID.Value != drug_id || !d.Active )
			{
				return new ReturnObject {
					Error = true,
					StatusCode = 404,
					Message = "That is not a valid drug."
				};
			}

			var eoc = Data.Eoc.FindByName( eoc_name );
			if( eoc == null || eoc.ID == null )
			{
				return new ReturnObject {
					Error = true,
					StatusCode = 404,
					Message = "That is not a valid EOC."
				};
			}

			var p = Systems.Security.GetCurrentProfile();

			if( p == null || p.ID == null )
			{
				return new ReturnObject {
					Error = true,
					StatusCode = 403,
					Message = "You must be logged in to do that."
				};
			}

			var certs = Data.UserEoc.FindByUserandDrug( p.ID.Value, d.ID.Value );
			var cert = (from c in certs
						where c.EocID == eoc.ID.Value
						select c).FirstOrDefault();

			if( cert != null )
			{
				return new ReturnObject {
					Error = true,
					StatusCode = 400,
					Message = "You are already certified in that EOC."
				};
			}

			cert = new Data.UserEoc();
			cert.DateCompleted = DateTime.Now;
			cert.DrugID = d.ID.Value;
			cert.EocID = eoc.ID.Value;
			cert.ProfileID = p.ID.Value;
			cert.Save();

			if( p.UserType.Name == "prescriber" )
				Systems.PrescriberUpdate.DrugCertified( Lib.Systems.Security.GetCurrentPrescriber(), d );

			TaskService.ScheduleTask( new CertificationReminder {
				DrugId = d.ID.Value,
				PrescriberId = p.ID.Value
			}, DateTime.Now.Add( Lib.Systems.Drugs.GetRenewalPeriod( d ) ) );

			return new ReturnObject {
				Error = false,
				StatusCode = 200,
				Growl = new ReturnGrowlObject {
					Type = "default",
					Vars = new ReturnGrowlVarsObject {
						text = "You have successfully certified \"" + eoc.DisplayName + "\" for \"" + d.GenericName + "\".",
						title = "EOC Certified"
					}
				},
				Actions = new List<ReturnActionObject> {
					new ReturnActionObject {
						Ele = ".eoc-icon-"+eoc_name,
						Type = "enable"
					}
				}
			};
		}
	}
}
