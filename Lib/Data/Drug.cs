using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "Drugs", PrimaryKeyColumn = "ID" )]
	public class Drug : ActiveRecord
	{
		public static IList<Drug> FindAll(bool active_only = true, long? drug_company_id = null)
		{
			var db = Database.Get("FDARems");
			var ps = new List<Parameter>();
			string sql = "SELECT * " +
							" FROM " + db.DelimTable("Drugs");

			if ( active_only || drug_company_id != null )
				sql += " WHERE ";

			if( active_only )
				sql += db.DelimColumn("Active") + " = 1 " + ((drug_company_id != null) ? " AND " : "");

			if( drug_company_id != null )
			{
				sql += db.DelimColumn("ID") + " IN (SELECT DISTINCT [DrugID] FROM [DrugFormulations] WHERE [DrugCompanyID] = @company_id)";
				ps.Add(new Parameter("company_id", drug_company_id.Value));
			}

			sql += " ORDER BY " + db.DelimColumn("ClassID") + " DESC, " + db.DelimColumn("BrandName") + " ASC";

			return db.ExecuteQuery<Drug>(sql, ps.ToArray());
		}

		public static IList<Drug> FindPending(long? drug_company_id = null)
		{
			var db = Database.Get("FDARems");
			var ps = new List<Parameter>();
			string sql = "SELECT " + db.DelimTable("d") + ".* " +
							" FROM " + db.DelimTable("Drugs") + " AS " + db.DelimTable("d") +
								" LEFT JOIN " + db.DelimTable("DrugVersions") + " AS " + db.DelimTable("dv") +
									" ON " + db.DelimTable("d") + "." + db.DelimColumn("ID") + " = " + db.DelimTable("dv") + "." + db.DelimColumn("DrugID") +
							" WHERE " + db.DelimTable("dv") + "." + db.DelimColumn("Status") + " = " + db.DelimString("Pending");

			if (drug_company_id != null)
			{
				sql += " AND " + db.DelimTable("Drugs") + "." + db.DelimColumn("ID") + " IN (SELECT DISTINCT [DrugID] FROM [DrugFormulations] WHERE [DrugCompanyID] = @company_id)";
				ps.Add(new Parameter("company_id", drug_company_id.Value));
			}

			sql += " ORDER BY " + db.DelimColumn("ClassID") + " DESC, " + db.DelimColumn("BrandName") + " ASC";

			return db.ExecuteQuery<Drug>(sql, ps.ToArray());
		}

		public static IList<Drug> FindByEoc(string eoc)
		{
			if( string.IsNullOrWhiteSpace(eoc) )
				return new List<Drug>();

			var eocs = new List<string>(new [] {
				"etasu","facility-pharmacy-enrollment","patient-enrollment",
				"prescriber-enrollment","education-training","monitoring-management",
				"informed-consent", "medication-guide", "forms-documents",
				"pharmacy-requirements"
			});

			if (eocs.Contains(eoc.ToLower()))
				return new List<Drug>();

			var db = Database.Get("FDARems");
			var ps = new List<Parameter>();
			string sql = "SELECT * " +
							" FROM " + db.DelimTable("Drugs") +
							" WHERE " + db.DelimColumn("EocIcons") + " LIKE " + db.DelimString("%"+eoc+"%");

			sql += " ORDER BY " + db.DelimColumn("ClassID") + " DESC, " + db.DelimColumn("BrandName") + " ASC";

			return db.ExecuteQuery<Drug>(sql, ps.ToArray());
		}

        public static IList<Drug> FindByClass(int drugClass)
        {
            if(drugClass < 0 || drugClass > 1)
                return new List<Drug>();

            var db = Database.Get("FDARems");
            string sql = "SELECT * " +
                            " FROM " + db.DelimTable("Drugs") +
                            " WHERE " + db.DelimColumn("ClassID") + " = " + db.DelimParameter("drugClass")+" AND " +
                                " "+db.DelimColumn("Active")+" = 1";

            sql += " ORDER BY " + db.DelimColumn("ClassID") + " DESC, " + db.DelimColumn("GenericName") + " ASC";

            var ps = new List<Parameter>();
            ps.Add(new Parameter("drugClass", drugClass));

            return db.ExecuteQuery<Drug>(sql, ps.ToArray());
        }

		[Column]
		public string GenericName;
		[Column]
		public string RemsReason;
		[Column]
		public string Indication;
		[Column]
		public long ClassID;
		[Column]
		public long? SystemID;
		[Column]
		public string RemsProgramUrl;
		[Column]
		public string FdaApplicationNumber;
		[Column]
		public DateTime? RemsApproved;
		[Column]
		public DateTime? RemsUpdated;
		[Column]
		public DateTime Updated;
		[Column]
		public long UpdatedByID;
		[Column]
		public bool Active;
		[Column]
		public string EocIcons;

		private DrugSystem _cacheSystem = null;
		public DrugSystem System
		{
			get
			{
				if (this._cacheSystem == null)
					this._cacheSystem = new DrugSystem(this.SystemID);

				return this._cacheSystem;
			}
		}

		public DateTime LatestVersionDate
		{
			get
			{
				var version = DrugVersion.FindLatestByDrug( this );
				DateTime ret = DateTime.MinValue;

				if( version != null && version.Status == "Approved" )
					ret = version.Updated;
				else
				{
					var temp = this.RemsUpdated;

					if( temp.HasValue )
						ret = temp.Value;
					else
					{
						temp = this.RemsApproved;

						if( temp.HasValue )
							ret = temp.Value;
						else
							ret = DateTime.MinValue;
					}
				}

				return ret;
			}
		}

		public Drug(long? id = null) : base(id)
		{ }

		public Drug(IDataRecord row) : base(row)
		{ }

		public IList<DrugLink> GetLinks()
		{
			if (this.ID == null)
				return new List<DrugLink>();

			var db = Database.Get("FDARems");
			string sql = "SELECT * " +
							" FROM " + db.DelimTable("DrugLinks") +
							" WHERE " + db.DelimColumn("DrugID") + " = " + db.DelimParameter("did") +
							" ORDER BY " + db.DelimColumn("Order") + " ASC";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("did", this.ID.Value));

			return db.ExecuteQuery<DrugLink>(sql, ps.ToArray());
		}

		public IList<DrugLink> GetLinksByType(string type)
		{
			if (this.ID == null)
				return new List<DrugLink>();

			var db = Database.Get("FDARems");
			string sql = "SELECT * " +
							" FROM " + db.DelimTable("DrugLinks") +
							" WHERE " + db.DelimColumn("DrugID") + " = " + db.DelimParameter("did") + " AND " + db.DelimColumn("Type") + " = " + db.DelimParameter("type") +
							" ORDER BY " + db.DelimColumn("Order") + " ASC";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("did", this.ID.Value));
			ps.Add(new Parameter("type", type));

			return db.ExecuteQuery<DrugLink>(sql, ps.ToArray());
		}

		public int GetNumFormulations()
		{
			if (this.ID == null)
				return 0;

			var db = Database.Get("FDARems");
			var sql = "SELECT COUNT(1) " +
						" FROM " + db.DelimTable("DrugFormulations") +
						" WHERE " + db.DelimColumn("DrugID") + " = " + db.DelimParameter("did");

			var ps = new List<Parameter>();
			ps.Add(new Parameter("did", this.ID.Value));

			return db.ExecuteScalar<int>(sql, ps.ToArray());
		}

		public string GetAnswer(string question_dev_name)
		{
			if (string.IsNullOrEmpty(question_dev_name))
				return null;

			var q = DSQ.Question.FindByDevName(question_dev_name);
			if (q == null)
				return null;

			var a = DSQ.Answer.FindByDrug(this, q);
			if (a == null)
				return null;

			return a.Value;
		}

		public void DetermineEOC()
		{
			if( this.ID == null )
				return;

			var eocs = new List<Eoc>();

			if( ClassID == 1 || (SystemID != null && SystemID > 0) )
				eocs.Add( Eoc.FindByName( "etasu" ) );

			if (GetAnswer("facility-enrollment") == "Yes" || GetAnswer("pharmacy-enrollment") == "Yes")
				eocs.Add( Eoc.FindByName( "facility-pharmacy-enrollment" ) );
			if (GetAnswer("patient-enrollment") == "Yes")
				eocs.Add( Eoc.FindByName( "patient-enrollment" ) );
			if (GetAnswer("prescriber-enrollment") == "Yes")
				eocs.Add( Eoc.FindByName( "prescriber-enrollment" ) );
			if (GetAnswer("education-training") == "Yes")
				eocs.Add( Eoc.FindByName( "education-training" ) );
			if (GetAnswer("monitoring-management") == "Yes")
				eocs.Add( Eoc.FindByName( "monitoring-management" ) );
			if (GetAnswer("informed-consent") == "Yes")
				eocs.Add( Eoc.FindByName( "informed-consent" ) );
			if( GetAnswer( "pharmacy-requirements" ) == "Yes" )
				eocs.Add( Eoc.FindByName( "pharmacy-requirements" ) );

			//TODO: Add "medication-guide", "forms-documents"

			var data = new List<Dictionary<string, object>>();
			foreach( var eoc in eocs )
			{
				data.Add( new Dictionary<string, object> {
					{"DrugID", this.ID.Value},
					{"EocID", eoc.ID.Value}
				});
			}

			var tbl = Table.Get( this.table.DB, "DrugEocs" );
			tbl.Delete( "[DrugID] = @drug_id", new[] { new Parameter( "drug_id", this.ID.Value ) } );
			tbl.Insert( data );
		}

		public bool HasEoc(string name)
		{
            if (string.IsNullOrEmpty(name))
				return false;

            if(name == "medication-guide")
                return HasMedicationGuide();

			var eoc = Eoc.FindByName( name );

			if( eoc == null )
				return false;

			var sql = "SELECT COUNT(1) FROM [DrugEocs] WHERE [DrugID] = @drug_id AND [EocID] = @eoc_id";
			var db = this.table.DB;

			var count = db.ExecuteScalar<int>( sql, new[] { new Parameter( "drug_id", this.ID.Value ), new Parameter( "eoc_id", eoc.ID.Value ) } );

			return (count >= 1);
		}

        public bool HasMedicationGuide()
        {
            return (GetAnswer("medication-guide") == "Yes");
        }

		public int GetNumberOfEocs()
		{
			var sql = "SELECT COUNT(1) FROM [DrugEocs] WHERE [DrugID] = @drug_id";
			var db = this.table.DB;

			return db.ExecuteScalar<int>( sql, new[] { new Parameter( "drug_id", this.ID.Value ) } );
		}

        internal static void ClearSelections(Prescriber prescriber)
        {
            if(prescriber == null)
                return;

            var db = Database.Get("FDARems");
            var sql = "DELETE FROM "+db.DelimTable("DrugSelections")+" "+
                        " WHERE " + db.DelimColumn("PrescriberID") + " = " + db.DelimParameter("prescriberId");

            var ps = new List<Parameter>();
            ps.Add(new Parameter("prescriberId", prescriber.ID));

            db.ExecuteQuery(sql, ps.ToArray());
        }
    }
}
