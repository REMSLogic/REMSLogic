using System.Collections.Generic;
using System.Data;
using Framework.Data;
using RemsLogic.Model;
using RemsLogic.Services;
using StructureMap;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "ProviderUsers", PrimaryKeyColumn = "ID" )]
	public class ProviderUser : ActiveRecord
	{
		public static IList<ProviderUser> FindAll()
		{
			return FindAll<ProviderUser>();
		}

		public static IList<ProviderUser> FindByProvider(Provider p)
		{
			if (p == null || !p.ID.HasValue)
				return new List<ProviderUser>();

			return FindByProvider(p.ID.Value);
		}

		public static IList<ProviderUser> FindByProvider(long provider_id)
		{
			return FindAllBy<ProviderUser>( new Dictionary<string, object> {
				{ "ProviderID", provider_id }
			}, new[] { "+ProviderUserType", "+ProfileID" } );
		}

		public static IList<ProviderUser> FindByOrganization(long organization_id)
		{
			return FindAllBy<ProviderUser>( new Dictionary<string, object> {
				{ "OrganizationId", organization_id }
			}, new[] { "+ProviderUserType", "+ProfileID" } );
		}

		public static IList<ProviderUser> FindByFacility(ProviderFacility p)
		{
			if (p == null || !p.ID.HasValue)
				return new List<ProviderUser>();

			return FindByFacility(p.ID.Value);
		}

		public static IList<ProviderUser> FindByFacility(long facility_id)
		{
			var db = Database.Get("FDARems");
			string sql = "SELECT " + db.DelimTable("ProviderUsers") + ".* " +
							" FROM " + db.DelimTable("ProviderUserFacilities") +
								" RIGHT JOIN " + db.DelimTable("ProviderUsers") +
									" ON " + db.DelimTable("ProviderUserFacilities") + "." + db.DelimColumn("UserID") + " = " + db.DelimTable("ProviderUsers") + "." + db.DelimColumn("ID") +
							" WHERE " + db.DelimTable("ProviderUserFacilities") + "." + db.DelimColumn("FacilityID") + " = @id";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("id", facility_id));

			return db.ExecuteQuery<ProviderUser>(sql, ps.ToArray());
		}

		public static ProviderUser FindByProfile(UserProfile p)
		{
			if (p == null || !p.ID.HasValue)
				return null;

			return FindByProfile(p.ID.Value);
		}

		public static ProviderUser FindByProfile(long profile_id)
		{
			return FindFirstBy<ProviderUser>( new Dictionary<string, object> {
				{ "ProfileID", profile_id }
			});
		}

		[Column]
		public long ProfileID;
		[Column]
		public long ProviderID;
        [Column]
        public long OrganizationID;
		[Column]
		public string ProviderUserType;
		[Column]
		public long? PrimaryFacilityID;

		private UserProfile _cacheUserProfile = null;
		public UserProfile Profile
		{
			get
			{
				if (this._cacheUserProfile == null)
					this._cacheUserProfile = new UserProfile(this.ProfileID);

				return this._cacheUserProfile;
			}
		}
		private Provider _cacheProvider = null;
		public Provider Provider
		{
			get
			{
				if (this._cacheProvider == null)
					this._cacheProvider = new Provider(this.ProviderID);

				return this._cacheProvider;
			}
		}
        private Facility _facility;
		public Facility Facility
		{
            get{return _facility ?? (_facility = LoadFacility(PrimaryFacilityID.Value));}
            set{_facility = value;}
		}

		public ProviderUser(long? id = null) : base(id)
		{ }

		public ProviderUser(IDataRecord row) : base(row)
		{ }

		public IList<ProviderFacility> GetFacilities()
		{
			if (this.ID == null)
				return new List<ProviderFacility>();

			string sql = "SELECT [t].* " +
						" FROM [ProviderUserFacilities] AS [l] " +
							" LEFT JOIN [ProviderFacilities] AS [t] " +
								" ON [l].[FacilityID] = [t].[ID] " +
						"WHERE [l].[UserID] = @id";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("id", this.ID));

			return this.table.DB.ExecuteQuery<ProviderFacility>(sql, ps.ToArray());
		}

		public void ClearFacilities()
		{
			var tbl = Table.Get("FDARems", "ProviderUserFacilities");

			tbl.Delete(this.table.DB.Delim("UserID", DelimType.Column) + " = @id", new Parameter[] { new Parameter("id", this.ID.Value) });
		}

		public void AddFacility(ProviderFacility item)
		{
			var tbl = Table.Get("FDARems", "ProviderUserFacilities");
			var data = new Dictionary<string, object>();

			data.Add("UserID", this.ID.Value);
			data.Add("FacilityID", item.ID.Value);

			tbl.Insert(data);
		}

		public void RemoveFacility(ProviderFacility item)
		{
			var tbl = Table.Get("FDARems", "ProviderUserFacilities");

			tbl.Delete(this.table.DB.Delim("UserID", DelimType.Column) + " = @uid AND " + this.table.DB.Delim("FacilityID", DelimType.Column) + " = @fid", new Parameter[] { new Parameter("uid", this.ID.Value), new Parameter("fid", item.ID.Value) });
		}

        private Facility LoadFacility(long facilityId)
        {
            return ObjectFactory
                .GetInstance<IOrganizationService>()
                .GetFacility(facilityId);
        }
	}
}
