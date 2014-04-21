using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Web;
using Framework.API;
using Lib.Systems.Tasks;
using RemsLogic.Model;
using RemsLogic.Repositories;
using RemsLogic.Services;
using CertificationReminder = Lib.Systems.Tasks.CertificationReminder;

namespace Lib.API.Prescriber
{
	public class Drug : Base
	{
		[SecurityRole("view_prescriber")]
		[Method("Prescriber/Drug/Add")]
		public static ReturnObject Add(HttpContext context, long id, int order)
		{
            try
            {
                AddDrugToPrescriber(id, order);
            }
            catch(ArgumentException ex)
            {
                return new ReturnObject
                {
                    Error = true,
                    StatusCode = 404,
                    Message = ex.Message
                };
            }
            catch(SecurityException ex)
            {
                return new ReturnObject
                {
                    Error = true,
                    StatusCode = 403,
                    Message = ex.Message
                };
            }

            return new ReturnObject
            {
                Error = false,
                StatusCode = 200
            };
        }

        /// <summary>
        /// This method is used by the Drug system and durring the selections process.
        /// Durring the selection process the method cannot directly return a ReturnObject
        /// because it is just one of a few steps.
        /// </summary>
        /// <param name="id">Drug to be added</param>
        /// <param name="order">Where to insert the drug</param>
        internal static void AddDrugToPrescriber(long id, int order)
        {
            var d = new Data.Drug(id);

            if (d.ID == null || d.ID.Value != id || !d.Active)
                throw new ArgumentException("That is not a valid drug", "id");

            var profile = Data.UserProfile.FindByUser(Framework.Security.Manager.GetUser());
            var p = Data.Prescriber.FindByProfile(profile);

            if (p == null || p.ID == null)
                throw new SecurityException("You must be logged in to do that");

			var drug_list = Systems.Lists.GetMyDrugList();
			drug_list.AddItem( d.ID.Value, order );

            Systems.PrescriberUpdate.DrugAdded(p, d);

            // typically i would have an IoC container setup to take care of all of this
            string connectionString = ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString;
            IDrugRepository drugRepo = new DrugRepository(connectionString);
            IComplianceRepository complianceRepo = new ComplianceRepository(connectionString);

            ComplianceService complianceService = new ComplianceService(drugRepo, complianceRepo);

            // this adds the entries into the UserEoc table for all possible eocs for the drug.
            // the date completed is left null
            complianceService.AddEocsToPrescriberProfile(profile.ID.Value, d.ID.Value);
        }

		[SecurityRole("view_prescriber")]
		[Method("Prescriber/Drug/Remove")]
		public static ReturnObject Remove(HttpContext context, long id)
		{
			var d = new Data.Drug(id);
			if (d.ID == null || d.ID.Value != id || !d.Active)
			{
				return new ReturnObject
				{
					Error = true,
					StatusCode = 404,
					Message = "That is not a valid drug."
				};
			}

			var profile = Data.UserProfile.FindByUser(Framework.Security.Manager.GetUser());
			var p = Data.Prescriber.FindByProfile(profile);

			if (p == null || p.ID == null)
			{
				return new ReturnObject
				{
					Error = true,
					StatusCode = 403,
					Message = "You must be logged in to do that."
				};
			}

			var drug_list = Systems.Lists.GetMyDrugList();
			drug_list.RemoveItem( d.ID.Value );

            Systems.PrescriberUpdate.DrugRemoved(p, d);

			return new ReturnObject
			{
				Error = false,
				StatusCode = 200
			};
		}

		[SecurityRole("view_prescriber")]
		[Method("Prescriber/Drug/Reorder")]
		public static ReturnObject Reorder(HttpContext context, long id, int fromPosition, int toPosition)
		{
			if (id <= 0)
				return new ReturnObject { Error = true, Message = "Invalid Drug specified.", StatusCode=404 };

			var profile = Data.UserProfile.FindByUser(Framework.Security.Manager.GetUser());
			var p = Data.Prescriber.FindByProfile(profile);

			if (p == null || p.ID == null)
			{
				return new ReturnObject
				{
					Error = true,
					StatusCode = 403,
					Message = "You must be logged in to do that."
				};
			}

			var drug_list = Systems.Lists.GetMyDrugList();
			drug_list.ReorderItem( id, fromPosition, toPosition );

			return new ReturnObject { Error = false };
		}

        [SecurityRole("view_prescriber")]
        [Method("Prescriber/Drug/Certified")]
        public static ReturnObject Certified(HttpContext context, long id, string eoc_name)
        {
            var d = new Data.Drug(id);
            if (d.ID == null || d.ID.Value != id || !d.Active)
            {
                return new ReturnObject
                {
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

			var p = Systems.Security.GetCurrentPrescriber();

            if (p == null || p.ID == null || p.ProfileID == null)
            {
                return new ReturnObject
                {
                    Error = true,
                    StatusCode = 403,
                    Message = "You must be logged in to do that."
                };
            }

			var certs = Data.UserEoc.FindByUserandDrug( p.ProfileID.Value, d.ID.Value );
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
			cert.ProfileID = p.ProfileID.Value;

            // this is handle differently now.  there will already be an existing
            // entry for the eoc. we just need to update the date comleted. sie note,
            // this method is to big.

			//cert.Save();

            UpdatePrescriberEoc(cert);

            Systems.PrescriberUpdate.DrugCertified(p, d);

            TaskService.ScheduleTask(new CertificationReminder
            {
                DrugId = d.ID.Value,
                PrescriberId = p.ID.Value
            }, DateTime.Now.Add(Lib.Systems.Drugs.GetRenewalPeriod(d)));

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

        private static void UpdatePrescriberEoc(Data.UserEoc cert)
        {
            
            // typically i would have an IoC container setup to take care of all of this
            string connectionString = ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString;
            IDrugRepository drugRepo = new DrugRepository(connectionString);
            IComplianceRepository complianceRepo = new ComplianceRepository(connectionString);

            ComplianceService complianceService = new ComplianceService(drugRepo, complianceRepo);

            PrescriberEoc eoc = complianceService.Find(cert.ProfileID, cert.DrugID, cert.EocID) ?? new PrescriberEoc
            {
                PrescriberProfileId = cert.ProfileID,
                DrugId = cert.DrugID,
                EocId = cert.EocID,
                Deleted = false,
            };

            eoc.CompletedAt = cert.DateCompleted;
            complianceService.Save(eoc);
        }
	}
}
