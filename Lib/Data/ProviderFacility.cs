using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "ProviderFacilities", PrimaryKeyColumn = "ID" )]
	public class ProviderFacility : ActiveRecord
	{
		public static IList<ProviderFacility> FindAll()
		{
			return FindAllBy<ProviderFacility>( new Dictionary<string, object> {
				{ "Deleted", 0 }
			}, new [] { "+Name" } );
		}

		public static IList<ProviderFacility> FindByProvider(Provider provider)
		{
			if (provider == null || provider.ID == null)
				return new List<ProviderFacility>();

			return FindByProvider(provider.ID.Value);
		}

		public static IList<ProviderFacility> FindByProvider(long provider_id)
		{
			return FindAllBy<ProviderFacility>( new Dictionary<string, object> {
				{ "ProviderID", provider_id },
				{ "Deleted", 0 }
			}, new[] { "+Name" } );
		}

		public static IList<ProviderFacility> FindAllByProviderUser(ProviderUser user)
		{
			if (user == null || user.ID == null)
				return new List<ProviderFacility>();

			return FindAllByProviderUser(user.ID.Value);
		}

		public static IList<ProviderFacility> FindAllByProviderUser(long user_id)
		{
			var db = Database.Get("FDARems");
			string sql = "SELECT " + db.DelimTable("ProviderFacilities") + ".* " +
							" FROM " + db.DelimTable("ProviderUserFacilities") +
								" RIGHT JOIN " + db.DelimTable("ProviderFacilities") +
									" ON " + db.DelimTable("ProviderUserFacilities") + "." + db.DelimColumn("FacilityID") + " = " + db.DelimTable("ProviderFacilities") + "." + db.DelimTable("ID") +
							" WHERE " + db.DelimTable("ProviderUserFacilities") + "." + db.DelimColumn("UserID") + " = " + db.DelimParameter("id") +
								" AND " + db.DelimTable("ProviderFacilities") + "." + db.DelimColumn("Deleted") + " = 0 " +
							" ORDER BY  " + db.DelimTable("ProviderFacilities") + "." + db.DelimColumn("Name") + " ASC";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("id", user_id));

			return db.ExecuteQuery<ProviderFacility>(sql, ps.ToArray());
		}

		[Column]
		public long ProviderID;
		[Column]
		public string Name;
		[Column]
		public long AddressID;
		[Column]
		public bool Deleted;

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
		private Address _cachePrimaryAddress = null;
		public Address PrimaryAddress
		{
			get
			{
				if (this._cachePrimaryAddress == null)
					this._cachePrimaryAddress = new Address(this.AddressID);

				return this._cachePrimaryAddress;
			}
		}

		public ProviderFacility(long? id = null) : base(id)
		{ }

		public ProviderFacility(IDataRecord row) : base(row)
		{ }
	}
}
