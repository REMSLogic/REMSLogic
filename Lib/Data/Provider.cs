using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "Providers", PrimaryKeyColumn = "ID" )]
	public class Provider : ActiveRecord
	{
		public static IList<Provider> FindAll()
		{
			return FindAll<Provider>(new [] { "+Name" });
		}

		public static Provider FindByUser(ProviderUser o)
		{
			if (o == null || !o.ID.HasValue)
				return null;

			return o.Provider;
		}

		public static int GetRecentCount()
		{
			var db = Framework.Data.Database.Get( "FDARems" );
			string sql = "SELECT COUNT(1)" +
						" FROM " + db.DelimTable("Providers") +
						" WHERE [Created] > @dt";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("dt", DateTime.Now.AddDays(-1)));
			return Convert.ToInt32(db.ExecuteScalar<object>( sql, ps.ToArray() ));
		}

		public static int GetTotalCount()
		{
			var db = Framework.Data.Database.Get( "FDARems" );
			string sql = "SELECT COUNT(1) FROM " + db.DelimTable("Providers");

			return Convert.ToInt32( db.ExecuteScalar<object>( sql ) );
		}

		[Column]
		public long? PrimaryContactID;
		[Column]
		public string Name;
		[Column]
		public string FacilitySize;
		[Column]
		public long? AddressID;
		[Column]
		public DateTime Created;

		private Contact _cachePrimaryContact = null;
		public Contact PrimaryContact
		{
			get
			{
				if (this._cachePrimaryContact == null)
					this._cachePrimaryContact = new Contact(this.PrimaryContactID);

				return this._cachePrimaryContact;
			}
		}
		private Address _cacheAddress = null;
		public Address Address
		{
			get
			{
				if (this._cacheAddress == null)
					this._cacheAddress = new Address(this.AddressID);

				return this._cacheAddress;
			}
		}

		public Provider(long? id = null) : base(id )
		{ }

		public Provider(IDataRecord row) : base(row )
		{ }

        public IList<Prescriber> GetPrescribers()
        {
            Database db = Database.Get("FDARems");
            ProviderUser providerUser = ProviderUser.FindByProvider(ID.Value).First();

            const string sql =  @"
                SELECT Prescribers.*
                FROM PrescriberProfiles
                    LEFT JOIN Prescribers ON [PrescriberProfiles].[PrescriberID] = [Prescribers].[ID]
                WHERE 
                    [PrescriberProfiles].[ProviderID] = @OrganizationId AND 
                    [PrescriberProfiles].[PrescriberID] IS NOT NULL
                ORDER BY 
                    [Prescribers].[ID] DESC;";

            return db.ExecuteQuery<Prescriber>(sql, new []
            {
                new Parameter("OrganizationId", providerUser.OrganizationID)
            });
        }

		public int GetNumPrescribers()
		{
			var db = Framework.Data.Database.Get("FDARems");
			string sql = "SELECT COUNT(1) " +
							" FROM " + db.DelimTable("PrescriberProfiles") +
							" WHERE [PrescriberProfiles].[ProviderID] = @id";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("id", this.ID.Value));

			return db.ExecuteScalar<int>(sql, ps.ToArray());
		}

		public int GetNumFacilities()
		{
			var db = Framework.Data.Database.Get( "FDARems" );
			string sql = "SELECT COUNT(1) " +
							" FROM " + db.DelimTable( "ProviderFacilities" ) +
							" WHERE [ProviderID] = @id";

			var ps = new List<Parameter>();
			ps.Add( new Parameter( "id", this.ID.Value ) );

			return db.ExecuteScalar<int>( sql, ps.ToArray() );
		}
	}
}
