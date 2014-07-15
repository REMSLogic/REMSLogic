using System;
using System.Collections.Generic;
using System.Web;
using Framework.API;
using Lib.Systems.Tasks;
using RemsLogic.Model.Compliance;
using RemsLogic.Services;
using StructureMap;
using CertificationReminder = Lib.Systems.Tasks.CertificationReminder;

namespace Lib.API.App
{
	public class Users : Base
	{
        /*
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
            var prescriber = Data.Prescriber.FindByProfile(p);

			if( p == null || p.ID == null )
			{
				return new ReturnObject {
					Error = true,
					StatusCode = 403,
					Message = "You must be logged in to do that."
				};
			}

            Data.UserEoc cert = new Data.UserEoc()
            {
                DateCompleted = DateTime.Now,
                DrugID = d.ID.Value,
                EocID = eoc.ID.Value,
                ProfileID = p.ID.Value
            };

            RecordCompliance(cert);

			if( p.UserType.Name == "prescriber" )
				Systems.PrescriberUpdate.DrugCertified( Lib.Systems.Security.GetCurrentPrescriber(), d );

			TaskService.ScheduleTask( new CertificationReminder {
				DrugId = d.ID.Value,
				PrescriberId = p.ID.Value
			}, DateTime.Now.Add( Lib.Systems.Drugs.GetRenewalPeriod( d ) ) );

            if(prescriber != null)
                Systems.PrescriberUpdate.DrugCertified(prescriber, d);

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
        */

		[Method( "App/Users/Certified" )]
		public static ReturnObject Certified( HttpContext context, long link_id )
		{
            Data.DSQ.Link link = new Data.DSQ.Link(link_id);

			if( link.ID == null || link.ID != link_id )
			{
				return new ReturnObject {
					Error = true,
					StatusCode = 404,
					Message = "That is not a valid EOC."
				};
			}

            Data.Drug d = new Data.Drug(link.DrugID);

			if( d.ID == null || d.ID != link.DrugID || !d.Active )
			{
				return new ReturnObject {
					Error = true,
					StatusCode = 404,
					Message = "That is not a valid drug."
				};
			}

			var eoc = new Data.Eoc(link.EocId);
			if( eoc.ID == null || eoc.ID != link.EocId)
			{
				return new ReturnObject {
					Error = true,
					StatusCode = 404,
					Message = "That is not a valid EOC."
				};
			}

			var p = Systems.Security.GetCurrentProfile();
            var prescriber = Data.Prescriber.FindByProfile(p);

			if( p == null || p.ID == null )
			{
				return new ReturnObject {
					Error = true,
					StatusCode = 403,
					Message = "You must be logged in to do that."
				};
			}

            Data.UserEoc cert = new Data.UserEoc()
            {
                DateCompleted = DateTime.Now,
                DrugID = 0,
                EocID = 0,
                LinkId = link_id,
                ProfileID = p.ID.Value
            };

            RecordCompliance(cert);

			if( p.UserType.Name == "prescriber" )
            {
				Systems.PrescriberUpdate.DrugCertified( Lib.Systems.Security.GetCurrentPrescriber(), d );

			    TaskService.ScheduleTask( new CertificationReminder {
				    DrugId = d.ID.Value,
				    PrescriberId = p.ID.Value
			    }, DateTime.Now.Add( Lib.Systems.Drugs.GetRenewalPeriod( d ) ) );

                if(prescriber != null)
                    Systems.PrescriberUpdate.DrugCertified(prescriber, d);
            }

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
						Ele = ".eoc-icon-"+eoc.Name,
						Type = "enable"
					}
				}
			};
		}

        private static void RecordCompliance(Data.UserEoc cert)
        {
            IComplianceService complianceService = ObjectFactory.GetInstance<IComplianceService>();

            PrescriberEoc eoc = complianceService.FindByLinkId(cert.ProfileID, cert.LinkId) ?? new PrescriberEoc
                {
                    PrescriberProfileId = cert.ProfileID,
                    DrugId = cert.DrugID,
                    EocId = cert.EocID,
                    LinkId = cert.LinkId,
                    Deleted = false,
                };

            eoc.CompletedAt = cert.DateCompleted;
            complianceService.RecordCompliance(eoc);
        }
	}
}
