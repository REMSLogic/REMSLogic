using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "DrugCompanyUsers", PrimaryKeyColumn = "ID" )]
	public class DrugCompanyUser : ActiveRecord
	{
		public static IList<DrugCompanyUser> FindAll()
		{
			return FindAll<DrugCompanyUser>();
		}

		public static IList<DrugCompanyUser> FindByDrugCompany(DrugCompany dc)
		{
			if (dc == null || dc.ID == null)
				return new List<DrugCompanyUser>();

			return FindByDrugCompany(dc.ID.Value);
		}

		public static IList<DrugCompanyUser> FindByDrugCompany(long dcid)
		{
			return FindAllBy<DrugCompanyUser>( new Dictionary<string, object> {
				{ "DrugCompanyID", dcid }
			} );
		}

		public static DrugCompanyUser FindByProfile(UserProfile p)
		{
			if (p == null || !p.ID.HasValue)
				return null;

			return FindByProfile(p.ID.Value);
		}

		public static DrugCompanyUser FindByProfile(long profile_id)
		{
			return FindFirstBy<DrugCompanyUser>( new Dictionary<string, object> {
				{ "ProfileID", profile_id }
			} );
		}

		[Column]
		public long ProfileID;
		[Column]
		public long DrugCompanyID;

		private UserProfile _cacheProfile = null;
		public UserProfile Profile
		{
			get
			{
				if (this._cacheProfile == null)
					this._cacheProfile = new UserProfile(this.ProfileID);

				return this._cacheProfile;
			}
		}
		private DrugCompany _cacheDrugCompany = null;
		public DrugCompany DrugCompany
		{
			get
			{
				if (this._cacheDrugCompany == null)
					this._cacheDrugCompany = new DrugCompany(this.DrugCompanyID);

				return this._cacheDrugCompany;
			}
		}

		public DrugCompanyUser(long? id = null) : base(id)
		{ }

		public DrugCompanyUser(IDataRecord row) : base(row)
		{ }
	}
}
