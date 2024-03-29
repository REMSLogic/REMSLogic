﻿using System;
using System.Collections.Generic;
using System.Security;
using System.Web;
using Framework.API;
using Lib.Systems.Tasks;
using RemsLogic.Model.Compliance;
using RemsLogic.Services;
using StructureMap;
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

            AddEocs(profile.ID.Value, d.ID.Value);
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

            RemoveEocs(profile.ID.Value, d.ID.Value);

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

            Data.UserEoc cert = new Data.UserEoc()
            {
                DateCompleted = DateTime.Now,
                DrugID = d.ID.Value,
                EocID = eoc.ID.Value,
                ProfileID = p.ProfileID.Value
            };

            RecordCompliance(cert);

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

        private static void AddEocs(long profileId, long drugId)
        {
            IComplianceService complianceService = ObjectFactory.GetInstance<IComplianceService>();

            List<string> roles = new List<string>();

            if(Framework.Security.Manager.HasRole("view_prescriber"))
                roles.Add("view_prescriber");

            if(Framework.Security.Manager.HasRole("view_provider"))
                roles.Add("view_provider");

            // this adds the entries into the UserEoc table for all possible eocs for the drug.
            // the date completed is left null
            complianceService.AddEocsToProfile(profileId, roles, drugId);
        }

        private static void RecordCompliance(Data.UserEoc cert)
        {
            IComplianceService complianceService = ObjectFactory.GetInstance<IComplianceService>();

            PrescriberEoc eoc = complianceService.Find(cert.ProfileID, cert.DrugID, cert.EocID) ?? new PrescriberEoc
                {
                    PrescriberProfileId = cert.ProfileID,
                    DrugId = cert.DrugID,
                    EocId = cert.EocID,
                    Deleted = false,
                };

            eoc.CompletedAt = cert.DateCompleted;
            complianceService.RecordCompliance(eoc);
        }

        private static void RemoveEocs(long profileId, long drugId)
        {
            IComplianceService complianceService = ObjectFactory.GetInstance<IComplianceService>();
            
            complianceService.RemoveEocsFromPrescriberProfile(profileId, drugId);
        }
	}
}
