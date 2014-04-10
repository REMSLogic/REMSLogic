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

				var provider = profile.Provider;
				
				var name = prescriber.Profile.PrimaryContact.Name;
				var drug_name = drug.GenericName;
				string msg = name + " has added the generic drug "+drug_name+" to their list of prescribed drugs.";
                string type = "drug-added";

				var pu = new Data.PrescriberUpdate();
				pu.PrescriberID = prescriber.ID.Value;
                pu.ProviderID = provider.ID;
				pu.DrugID = drug.ID.Value;
                pu.Message = msg;
                pu.Type = type;
				pu.DateCreated = DateTime.Now;
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

                var provider = profile.Provider;

                var name = prescriber.Profile.PrimaryContact.Name;
                var drug_name = drug.GenericName;
                string msg = name + " has removed the generic drug " + drug_name + " from their list of prescribed drugs.";
                string type = "drug-removed";

                var pu = new Data.PrescriberUpdate();
                pu.PrescriberID = prescriber.ID.Value;
                pu.ProviderID = provider.ID;
                pu.DrugID = drug.ID.Value;
                pu.Message = msg;
                pu.DateCreated = DateTime.Now;
                pu.Type = type;
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

                var provider = profile.Provider;

                var name = prescriber.Profile.PrimaryContact.Name;
                var drug_name = drug.GenericName;
                string msg = name + " has marked that they are certified to prescribe the generic drug " + drug_name + ".";
                string type = "drug-certified";

                var pu = new Data.PrescriberUpdate();
                pu.PrescriberID = prescriber.ID.Value;
                pu.ProviderID = provider.ID;
                pu.DrugID = drug.ID.Value;
                pu.Message = msg;
                pu.DateCreated = DateTime.Now;
                pu.Type = type;
                pu.Save();
            }
        }
    }
}
