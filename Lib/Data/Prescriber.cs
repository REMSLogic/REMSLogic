using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Framework.Data;
using RemsLogic.Model;
using RemsLogic.Repositories;
using RemsLogic.Services;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "Prescribers", PrimaryKeyColumn = "ID" )]
	public class Prescriber : ActiveRecord
	{
		public static IList<Prescriber> FindAll()
		{
			return FindAll<Prescriber>(new [] { "-ID" });
		}

		public static Prescriber FindByProfile(UserProfile profile)
		{
			if (!profile.ID.HasValue)
				return null;

			return FindByProfile(profile.ID.Value);
		}

		public static Prescriber FindByProfile(long pid)
		{
			return FindFirstBy<Prescriber>( new Dictionary<string, object> {
				{ "ProfileID", pid }
			}, new[] { "-ID" } );
		}

		public static Prescriber FindByNPI(string npi)
		{
			return FindFirstBy<Prescriber>( new Dictionary<string, object> {
				{ "NpiId", npi }
			}, new[] { "-ID" } );
		}

        public static Prescriber FindByStateId(long issuerId, string stateId)
        {
            return FindFirstBy<Prescriber>(new Dictionary<string,object>{
                {"StateIdIssuer", issuerId},
                {"StateId", stateId}
            }, new[] {"-ID"});
        }

		public static int GetRecentCount()
		{
			var db = Database.Get( "FDARems" );
			string sql = "SELECT COUNT(1)" +
						" FROM " + db.DelimTable("UserProfiles") +
						" WHERE " + db.DelimColumn("Created") + " > " + db.DelimParameter("dt") + " AND " + db.DelimColumn("UserTypeID") + " = " + db.DelimParameter("utid");

			var ps = new List<Parameter>();
			ps.Add(new Parameter("dt", DateTime.Now.AddDays(-1)));
			ps.Add(new Parameter("utid", UserType.FindByName("prescriber").ID.Value));
			return Convert.ToInt32(db.ExecuteScalar<object>( sql, ps.ToArray() ));
		}

		public static int GetTotalCount()
		{
			var db = Database.Get( "FDARems" );
			string sql = "SELECT COUNT(1)" +
						" FROM " + db.DelimTable("Prescribers");

			return Convert.ToInt32( db.ExecuteScalar<object>( sql ) );
		}

        [Column]
		public long? ProfileID;
		[Column]
		public long? SpecialityID;
		[Column]
		public string NpiId;
        [Column]
        public string StateId;
        [Column]
        public long? StateIdIssuer;

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
		private Speciality _cacheSpeciality = null;
		public Speciality Speciality
		{
			get
			{
				if (!this.SpecialityID.HasValue)
					return null;

				if (this._cacheSpeciality == null)
					this._cacheSpeciality = new Speciality(this.SpecialityID.Value);

				return this._cacheSpeciality;
			}
		}

		public Prescriber(long? id = null) : base( id )
		{ }

		public Prescriber(IDataRecord row) : base( row )
		{ }

		/*public int GetNumSelectedDrugs()
		{
			if (this.ID == null || !this.ID.HasValue)
				return 0;

			var db = Database.Get("FDARems");
			string sql = "SELECT COUNT(1) " +
						" FROM " + db.DelimTable("PrescriberDrugs") + 
						" WHERE " + db.DelimColumn("PrescriberID") + " = " + db.DelimParameter("id");

			var ps = new List<Parameter>();
			ps.Add(new Parameter("id", this.ID.Value));
			return db.ExecuteScalar<int>(sql, ps.ToArray());
		}

		public int GetNumCertifiedDrugs()
		{
			if (this.ID == null || !this.ID.HasValue)
				return 0;

			var db = Database.Get("FDARems");
			string sql = "SELECT COUNT(1) " +
						" FROM " + db.DelimTable("PrescriberDrugs") +
						" WHERE " + db.DelimColumn("PrescriberID") + " = " + db.DelimParameter("id") + " AND " + db.DelimColumn("DateCertified") + " IS NOT NULL";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("id", this.ID.Value));
			return db.ExecuteScalar<int>(sql, ps.ToArray());
		}

		public int GetNumUncertifiedDrugs()
		{
			if (this.ID == null || !this.ID.HasValue)
				return 0;

			var db = Database.Get("FDARems");
			string sql = "SELECT COUNT(1) " +
						" FROM " + db.DelimTable("PrescriberDrugs") +
						" WHERE " + db.DelimColumn("PrescriberID") + " = " + db.DelimParameter("id") + " AND " + db.DelimColumn("DateCertified") + " IS NULL";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("id", this.ID.Value));
			return db.ExecuteScalar<int>(sql, ps.ToArray());
		}*/

		public DateTime GetCurrentExpirationDate()
		{
			var db = Database.Get("FDARems");
			string sql = "SELECT " + db.DelimColumn("Expires") +
						" FROM " + db.DelimTable("PrescriberProfiles") + 
						" WHERE " + db.DelimColumn("PrescriberID") + " = " + db.DelimParameter("id") +
						" ORDER BY " + db.DelimColumn("Expires") + " DESC";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("id", this.ID.Value));
			return db.ExecuteScalar<DateTime>(sql, ps.ToArray());
		}

		public void AddSpeciality(long SpecialityID)
		{
			// Prevent Duplicate Entry
			RemoveSpeciality(SpecialityID);

			var db = Database.Get("FDARems");
			var tbl = Table.Get(db, "PrescriberSpecialities");

			var vals = new Dictionary<string, object>();
			vals["PrescriberID"] = this.ID.Value;
			vals["SpecialityID"] = SpecialityID;

			tbl.Insert(vals);
		}

		public void RemoveSpeciality(long SpecialityID)
		{
			var db = Database.Get("FDARems");
			var tbl = Table.Get(db, "PrescriberSpecialities");

			var where = db.DelimColumn("PrescriberID") + " = " + db.DelimParameter("pid") + " AND " +
						db.DelimColumn("SpecialityID") + " = " + db.DelimParameter("sid");

			var ps = new List<Parameter>();
			ps.Add(new Parameter("pid", this.ID.Value));
			ps.Add(new Parameter("sid", SpecialityID));

			tbl.Delete(where, ps.ToArray());

		}

		public class PresciberDrugInfo
		{
			[Column]
			public long DrugID;
			[Column]
			public string DrugName;
			[Column]
			public DateTime DateAdded;
			[Column]
			public int DrugEocs;
			[Column]
			public int UserEocs;

			public float PercentComplete
			{
				get
				{
					if( DrugEocs <= 0 )
						return 100.0f;

					if( UserEocs <= 0 )
						return 0.0f;

					return ((float)UserEocs/(float)DrugEocs)*100.0F;
				}
			}
		}

		public IList<PresciberDrugInfo> GetDrugInfo()
		{
            string connectionString = ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString;
            long profileId = Systems.Security.GetCurrentProfile().ID.Value;

            IDrugRepository drugRepo = new DrugRepository(connectionString);
            IDsqRepository dsqRepo = new DsqRepository(connectionString);
            IComplianceRepository complianceRepo = new ComplianceRepository(connectionString);

            IComplianceService complianceSvc = new ComplianceService(drugRepo, complianceRepo, dsqRepo);

            var eocs = complianceSvc.GetEocsStatus(profileId, DrugListType.MyDrugs);

            return (from eoc in eocs
                    select new PresciberDrugInfo
                    {
                        DrugID = eoc.Key.Id,
                        DrugName = eoc.Key.GenericName,
                        DateAdded = DateTime.Now,
                        DrugEocs = eoc.Value.Count,
                        UserEocs = eoc.Value.Count(x => x.CompletedAt != null)
                    }).ToList();
		}
	}
}