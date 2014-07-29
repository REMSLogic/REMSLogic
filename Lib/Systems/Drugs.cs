using System;
using System.Collections.Generic;
using Framework.Security;
using Lib.Data;

namespace Lib.Systems
{
	public class Drugs
	{
		public static bool HasPendingChanges(Lib.Data.Drug drug)
		{
			if( drug == null || !drug.ID.HasValue )
				return false;

			return HasPendingChanges(drug.ID);
		}

		public static bool HasPendingChanges(long? drug_id)
		{
			if( drug_id == null || !drug_id.HasValue || drug_id <= 0 )
				return false;

			var ver = Lib.Data.DrugVersion.FindLatestByDrug(drug_id);

			if( ver != null && ver.Status == "Pending" )
				return true;

			return false;
		}

		/// <summary>
		/// Looks up how frequently a Drug needs to have its certification renewed.
		/// </summary>
		/// <param name="drug">The Drug to look-up.  Only the Drug.ID is needed.</param>
		/// <returns>
		/// Returns a TimeSpan indicating the renewal period.
		/// </returns>
		public static TimeSpan GetRenewalPeriod(Data.Drug drug)
		{
			// MJL 2013-11-05 - I like the idea of simply looking up the renewal period and
			// then having the caller do the math; however, there are some issues with
			// accuracy when it comes to TimeSpan that may make this a bad idea.  For now
			// this method is not used anywhere

			// TODO: Implement the method... maybe...
			return new TimeSpan(180, 0, 0, 0);
		}

		/// <summary>
		/// Based on the current date, calculates the next time a certification will need
		/// to be renewed.
		/// </summary>
		/// <param name="drug">The Drug to look-up.  Only the Drug.ID is needed.</param>
		/// <param name="base_date">The DateTime to base the calculate off of.</param>
		/// <returns>DateTime reflecting the base_date plus the Drugs renewal period.</returns>
		public static DateTime CalculateRenewalDate(Lib.Data.Drug drug, DateTime base_date)
		{
			// MJL 2013-11-05

			// TODO: Implement the method
			return (base_date + GetRenewalPeriod(drug));
		}

        // MJL 2013-11-06
        public static IList<Lib.Data.Drug> GetDrugsWithNoSelection(User user)
        {
            var userProfile = UserProfile.FindByUser(user);
            if(userProfile == null)
                return new List<Lib.Data.Drug>();

            var prescriber = Prescriber.FindByProfile(userProfile);
            if(prescriber == null)
                return new List<Lib.Data.Drug>();

            return DrugSelection.GetDrugsWithNoSelection(prescriber);
        }

        public static IList<Data.Drug> GetDrugsWithUpdates(User user)
        {
            var userProfile = UserProfile.FindByUser(user);
            if(userProfile == null)
                return new List<Lib.Data.Drug>();

            var prescriber = Prescriber.FindByProfile(userProfile);
            if(prescriber == null)
                return new List<Lib.Data.Drug>();

            return DrugSelection.GetDrugsWithUpdates(prescriber);
        }

		public static bool SelectionsUpdateRequired()
		{
			//return SelectionsUpdateRequired( Lib.Systems.Security.GetCurrentUser() );
            return false;
		}

        // MJL 2013-11-06 - Technically the method above could be used to accomplish the same
        // goal, but this allows for a more efficient query.
        public static bool SelectionsUpdateRequired(User user)
        {
            var userProfile = UserProfile.FindByUser(user);
            var prescriber = Prescriber.FindByProfile(userProfile);

            if(DrugSelection.HasDrugsWithNoSelection(prescriber))
                return true;

            if(DrugSelection.HasDrugsWithUpdates(prescriber))
                return true;

            return false;
        }

        public static IList<Data.Drug> GetETASUDrugs()
        {
            return Lib.Data.Drug.FindByClass(1);
        }

        public static IList<Data.Drug> GetNonETASUDrugs()
        {
            return Lib.Data.Drug.FindByClass(0);
        }

        internal static void ClearSelections(Prescriber prescriber)
        {
            Lib.Data.Drug.ClearSelections(prescriber);
        }
    }
}
