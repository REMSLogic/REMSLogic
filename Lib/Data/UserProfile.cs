using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "UserProfiles", PrimaryKeyColumn = "ID" )]
	public class UserProfile : ActiveRecord
	{
		public static IList<UserProfile> FindAll()
		{
			return FindAll<UserProfile>( new[] { "-Created" } );
		}

		public static UserProfile FindByUser(Framework.Security.User u)
		{
			if (u == null || u.ID == null)
				return null;

			return FindByUser(u.ID.Value);
		}

		public static UserProfile FindByUser(long uid)
		{
			return FindFirstBy<UserProfile>( new Dictionary<string, object> {
				{ "UserID", uid }
			}, new[] { "-Created" } );
		}

		public static IList<UserProfile> FindByUserType(UserType ut)
		{
			if (!ut.ID.HasValue)
				return new List<UserProfile>();

			return FindByUserType(ut.ID.Value);
		}

		public static IList<UserProfile> FindByUserType(long utid)
		{
			return FindAllBy<UserProfile>( new Dictionary<string, object> {
				{ "UserTypeID", utid }
			}, new[] { "-Created" } );
		}

		public static IList<UserProfile> FindByUserTypes(List<UserType> uts)
		{
			if (uts == null || uts.Count <= 0)
				return new List<UserProfile>();

			var utids = new List<long>();

			foreach (var ut in uts)
				if (ut.ID.HasValue)
					utids.Add(ut.ID.Value);

			if( utids.Count <= 0 )
				return new List<UserProfile>();

			return FindByUserTypes(utids);
		}

		public static IList<UserProfile> FindByUserTypes(List<long> utids)
		{
			if (utids == null || utids.Count <= 0)
				return new List<UserProfile>();

			return FindByUserTypes(utids.ToArray());
		}

		public static IList<UserProfile> FindByUserTypes(long[] utids)
		{
			string ids = "";
			bool first = true;

			foreach (var utid in utids)
			{
				if (!first)
					ids += ",";

				first = false;

				ids += utid.ToString();
			}
			

			var db = Database.Get("FDARems");
			string sql = "SELECT * " +
							" FROM " + db.DelimTable("UserProfiles") +
							" WHERE [UserTypeID] IN ("+ids+")" +
							" ORDER BY [Created] DESC";

			return db.ExecuteQuery<UserProfile>(sql);
		}

        public static UserProfile FindByPrimaryContact(long id)
        {
			return FindFirstBy<UserProfile>( new Dictionary<string, object> {
				{ "PrimaryContactID", id }
			}, new[] { "-Created" } );
        }

		[Column]
		public long UserID;
		[Column]
		public long UserTypeID;
		[Column]
		public long? PrimaryContactID;
		[Column]
		public long? PrimaryAddressID;
		[Column]
		public DateTime Created;
        [Column]
        public bool IsEcommerce;

		private Framework.Security.User _cacheUser = null;
		public Framework.Security.User User
		{
			get
			{
				if( this._cacheUser == null )
					this._cacheUser = new Framework.Security.User(this.UserID);

				return this._cacheUser;
			}
		}
		private UserType _cacheUserType = null;
		public UserType UserType
		{
			get
			{
				if (this._cacheUserType == null)
					this._cacheUserType = new UserType(this.UserTypeID);

				return this._cacheUserType;
			}
		}
		private Contact _cachePrimaryContact = null;
		public Contact PrimaryContact
		{
			get
			{
				if (!this.PrimaryContactID.HasValue)
					return null;

				if (this._cachePrimaryContact == null)
					this._cachePrimaryContact = new Contact(this.PrimaryContactID.Value);

				return this._cachePrimaryContact;
			}
		}
		private Address _cachePrimaryAddress = null;
		public Address PrimaryAddress
		{
			get
			{
				if (!this.PrimaryAddressID.HasValue)
					return null;

				if (this._cachePrimaryAddress == null)
					this._cachePrimaryAddress = new Address(this.PrimaryAddressID.Value);

				return this._cachePrimaryAddress;
			}
		}

		public UserProfile(long? id = null) : base(id)
		{ }

		public UserProfile(IDataRecord row) : base( row )
		{ }
	}
}
