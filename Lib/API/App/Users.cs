using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.API;
using Lib.Data;
using Lib.Systems;
using Lib.Systems.Tasks;
using RemsLogic.Model.Compliance;
using RemsLogic.Services;
using StructureMap;
using CertificationReminder = Lib.Systems.Tasks.CertificationReminder;

namespace Lib.API.App
{
	public class Users : Base
	{
		[Method( "App/Users/Certified" )]
		public static ReturnObject Certified( HttpContext context, long drug_id, long question_id )
		{
            IComplianceService complianceSvc = ObjectFactory.GetInstance<IComplianceService>();

            UserProfile p = Security.GetCurrentProfile();
            Data.Prescriber prescriber = Data.Prescriber.FindByProfile(p);
            List<PrescriberEoc> eocs = complianceSvc.GetPrescriberEocs(drug_id, question_id, p.ID ?? 0).ToList();

            /*
            Data.DSQ.Link link = new Data.DSQ.Link(link_id);

			if( link.ID == null || link.ID != link_id )
			{
				return new ReturnObject {
					Error = true,
					StatusCode = 404,
					Message = "That is not a valid EOC."
				};
			}
            */

            Data.Drug d = new Data.Drug(drug_id);

			if( d.ID == null || d.ID != drug_id || !d.Active )
			{
				return new ReturnObject {
					Error = true,
					StatusCode = 404,
					Message = "That is not a valid drug."
				};
			}

            /*
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
            */

            if(eocs.Count == 0)
            {
                return new ReturnObject {
                    Error = true,
                    StatusCode = 404,
                    Message = "No matching requirement found."
                };
            }

			if( p == null || p.ID == null )
			{
				return new ReturnObject {
					Error = true,
					StatusCode = 403,
					Message = "You must be logged in to do that."
				};
			}

            RecordCompliance(complianceSvc, eocs);

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
						text = "Certification recorded for \"" + d.GenericName + "\".",
						title = "EOC Certified"
					}
				}
			};
		}

        private static void RecordCompliance(IComplianceService complianceSvc, List<PrescriberEoc> eocs)
        {
            foreach(PrescriberEoc eoc in eocs)
            {
                eoc.CompletedAt = DateTime.Now;
                complianceSvc.RecordCompliance(eoc);
            }
        }
	}
}
