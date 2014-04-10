using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "FDAUsers", PrimaryKeyColumn = "ID" )]
	public class FDAUser : ActiveRecord
	{
		public static IList<FDAUser> FindAll()
		{
			return FindAll<FDAUser>(new[] { "-ID" });
		}

		[Column]
		public long ProfileID;
		[Column]
		public bool Active;

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

		public FDAUser(long? id = null) : base(id)
		{ }

		public FDAUser(IDataRecord row) : base(row)
		{ }
	}
}
