using Lib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Systems
{
    public class PrescriberUpdate
    {
        public IList<Data.PrescriberUpdate> FindAll()
        {
            return Data.PrescriberUpdate.FindAll();
        }
        
        public IList<Data.PrescriberUpdate> FindByProvider(Data.Provider provider)
        {
            return Data.PrescriberUpdate.FindByProvider(provider);
        }
        
        public static void DrugAdded(Data.Prescriber prescriber, Data.Drug drug)
        {
			var profiles = Data.PrescriberProfile.FindByPrescriber(prescriber);

			foreach( var profile in profiles )
			{
				if( profile.ProviderID == null )
					continue;

				var name = prescriber.Profile.PrimaryContact.Name;
				var drug_name = drug.GenericName;
				string msg = "<b>" + name + "</b> has added <b>"+drug_name+"</b> to their list of prescribed drugs.";
                string type = "drug-added";

				var pu = new Data.PrescriberUpdate();
				pu.PrescriberID = prescriber.ID.Value;
                pu.ProviderID = profile.ProviderID;
				pu.DrugID = drug.ID.Value;
                pu.Message = msg;
                pu.Type = type;
				pu.DateCreated = DateTime.Now;
                pu.FacilityId = profile.PrimaryFacilityID ?? 0;
                pu.OrganizationId = profile.OrganizationId;
				pu.Save();
			}
        }

		public static void DrugRemoved(Data.Prescriber prescriber, Data.Drug drug)
        {
            var profiles = Data.PrescriberProfile.FindByPrescriber(prescriber);

            foreach (var profile in profiles)
            {
                if (profile.ProviderID == null)
                    continue;

                var name = prescriber.Profile.PrimaryContact.Name;
                var drug_name = drug.GenericName;
                string msg = "<b>" + name + "</b> has removed <b>" + drug_name + "</b> from their list of prescribed drugs.";
                string type = "drug-removed";

                var pu = new Data.PrescriberUpdate();
                pu.PrescriberID = prescriber.ID.Value;
                pu.ProviderID = profile.ProviderID;
                pu.DrugID = drug.ID.Value;
                pu.Message = msg;
                pu.DateCreated = DateTime.Now;
                pu.Type = type;
                pu.FacilityId = profile.PrimaryFacilityID ?? 0;
                pu.OrganizationId = profile.OrganizationId;
                pu.Save();
            }
        }

		public static void DrugCertified(Data.Prescriber prescriber, Data.Drug drug)
        {
            var profiles = Data.PrescriberProfile.FindByPrescriber(prescriber);

            foreach (var profile in profiles)
            {
                if (profile.ProviderID == null)
                    continue;

                var name = prescriber.Profile.PrimaryContact.Name;
                var drug_name = drug.GenericName;
                string msg = "<b>" + name + "</b> has marked a prerequisite for <b>" + drug_name + "</b> as complete.";
                string type = "drug-certified";

                var pu = new Data.PrescriberUpdate();
                pu.PrescriberID = prescriber.ID.Value;
                pu.ProviderID = profile.ProviderID;
                pu.DrugID = drug.ID.Value;
                pu.Message = msg;
                pu.DateCreated = DateTime.Now;
                pu.Type = type;
                pu.FacilityId = profile.PrimaryFacilityID ?? 0;
                pu.OrganizationId = profile.OrganizationId;
                pu.Save();
            }
        }
    }
}
