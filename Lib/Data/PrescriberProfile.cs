﻿using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "PrescriberProfiles", PrimaryKeyColumn = "ID" )]
	public class PrescriberProfile : ActiveRecord
	{
		public static IList<PrescriberProfile> FindAll()
		{
			return FindAll<PrescriberProfile>();
		}

		public static IList<PrescriberProfile> FindByProvider(Provider p)
		{
			return FindAllBy<PrescriberProfile>( new Dictionary<string, object> {
				{ "ProviderID", p.ID.Value },
				{ "PrescriberID", SpecialValue.IsNotNull },
                { "Deleted", false },
			}, new[] { "-Expires" } );
		}

		public static PrescriberProfile FindByPrescriberAndProvider( Prescriber p, Provider pro )
		{
			return FindFirstBy<PrescriberProfile>( new Dictionary<string, object> {
				{ "ProviderID", pro.ID.Value },
				{ "PrescriberID", p.ID.Value }
			}, new[] { "-Expires" } );
		}

        public static IList<PrescriberProfile> FindPendingInvitesByProvider(Provider provider)
        {
            return FindAllBy<PrescriberProfile>( new Dictionary<string, object> {
                { "ProviderID", provider.ID.Value },
                { "PrescriberId", SpecialValue.IsNull },
            }, new[] { "-Expires" } );
        }

		public static IList<PrescriberProfile> FindByPrescriber(Prescriber p)
		{
			return FindAllBy<PrescriberProfile>( new Dictionary<string, object> {
				{ "PrescriberID", p.ID.Value }
			}, new[] { "-Expires" } );
		}

		public static IList<PrescriberProfile> FindByFacility(ProviderFacility p)
		{
			if (p == null || !p.ID.HasValue)
				return new List<PrescriberProfile>();

			return FindByFacility(p.ID.Value);
		}

		public static IList<PrescriberProfile> FindByFacility(long facility_id)
		{
			var db = Database.Get("FDARems");
			string sql = "SELECT " + db.DelimTable("PrescriberProfiles") + ".* " +
							" FROM " + db.DelimTable("PrescriberProfileFacilities") +
								" RIGHT JOIN " + db.DelimTable("PrescriberProfiles") +
									" ON " + db.DelimTable("PrescriberProfileFacilities") + "." + db.DelimColumn("ProfileID") + " = " + db.DelimTable("PrescriberProfiles") + "." + db.DelimColumn("ID") +
							" WHERE " + db.DelimTable("PrescriberProfileFacilities") + "." + db.DelimColumn("FacilityID") + " = @id";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("id", facility_id));

			return db.ExecuteQuery<PrescriberProfile>(sql, ps.ToArray());
		}

        public static PrescriberProfile FindByToken(string token)
        {
			return FindFirstBy<PrescriberProfile>( new Dictionary<string, object> {
				{ "Guid", token }
			}, new[] { "-Expires" } );
        }

        // MJL 2013-10-25 - Added GUID to AR Object.
        [Column]
        public Guid Guid;

        // MJL 2013-10-25 - PrescriberID changed to nullable.  When a provider
        // creates a prescriber, an entry with a GUID is added to the PrescriberProfile
        // table.  It is then up to the prescriber to come back and finish
        // filling out thier profile.
		[Column]
		public long? PrescriberID;
		[Column]
		public long? ProviderID;
		[Column]
		public long? PrimaryFacilityID;
		[Column]
		public long ContactID;
		[Column]
		public long AddressID;
        [Column]
        public long? PrescriberTypeID;
		[Column]
		public DateTime Expires;
		[Column]
		public bool Deleted;

		private Prescriber _cachePrescriber = null;
		public Prescriber Prescriber
		{
			get
			{
				if (this._cachePrescriber == null)
					this._cachePrescriber = new Prescriber(this.PrescriberID);

				return this._cachePrescriber;
			}
		}
		private Provider _cacheProvider = null;
		public Provider Provider
		{
			get
			{
				if (!this.ProviderID.HasValue)
					return null;

				if (this._cacheProvider == null)
					this._cacheProvider = new Provider(this.ProviderID.Value);

				return this._cacheProvider;
			}
		}
		private ProviderFacility _cacheFacility = null;
		public ProviderFacility Facility
		{
			get
			{
				if( this.PrimaryFacilityID == null )
					return null;

				if (this._cacheFacility == null)
					this._cacheFacility = new ProviderFacility(this.PrimaryFacilityID);

				return this._cacheFacility;
			}
		}
		private Contact _cacheContact = null;
		public Contact Contact
		{
			get
			{
				if (this._cacheContact == null)
					this._cacheContact = new Contact(this.ContactID);

				return this._cacheContact;
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

        private PrescriberType _cachePrescriberType;
        public PrescriberType PrescriberType
        {
            get{return _cachePrescriberType ?? (_cachePrescriberType = new PrescriberType(PrescriberTypeID));}
        }

		public bool Expired
		{ get { return Expires < DateTime.Now; } }

		public PrescriberProfile(long? id = null) : base(id)
		{ }

		public PrescriberProfile(IDataRecord row) : base(row)
		{ }

		public IList<ProviderFacility> GetFacilities()
		{
			if (this.ID == null)
				return new List<ProviderFacility>();

			string sql = "SELECT [t].* " +
						" FROM [PrescriberProfileFacilities] AS [l] " +
							" LEFT JOIN [ProviderFacilities] AS [t] " +
								" ON [l].[FacilityID] = [t].[ID] " +
						"WHERE [l].[ProfileID] = @id";
			
			var ps = new List<Parameter>();
			ps.Add(new Parameter("id", this.ID));

			return this.table.DB.ExecuteQuery<ProviderFacility>(sql, ps.ToArray());
		}

		public void ClearFacilities()
		{
			var tbl = Table.Get("FDARems", "PrescriberProfileFacilities");

			tbl.Delete(this.table.DB.Delim("ProfileID", DelimType.Column) + " = @id", new Parameter[] { new Parameter("id", this.ID.Value) });
		}

		public void AddFacility(ProviderFacility item)
		{
			var tbl = Table.Get("FDARems", "PrescriberProfileFacilities");
			var data = new Dictionary<string, object>();

			data.Add("ProfileID", this.ID.Value);
			data.Add("FacilityID", item.ID.Value);

			tbl.Insert(data);
		}

		public void RemoveFacility(ProviderFacility item)
		{
			var tbl = Table.Get("FDARems", "PrescriberProfileFacilities");

			tbl.Delete(this.table.DB.Delim("ProfileID", DelimType.Column) + " = @pid AND " + this.table.DB.Delim("FacilityID", DelimType.Column) + " = @fid", new Parameter[] { new Parameter("pid", this.ID.Value), new Parameter("fid", item.ID.Value) });
		}
    }
}
