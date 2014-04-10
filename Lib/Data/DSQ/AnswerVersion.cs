using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Framework.Data;

namespace Lib.Data.DSQ
{
	[Table( DatabaseName = "FDARems", TableName = "DSQ_AnswerVersions", PrimaryKeyColumn = "ID" )]
	public class AnswerVersion : ActiveRecord
	{
		public static IList<AnswerVersion> FindAll()
		{
			return FindAll<AnswerVersion>( new[] { "-DSQAnswerID", "-Version" } );
		}

		public static IList<AnswerVersion> FindByAnswer(Answer o)
		{
			if (o == null || !o.ID.HasValue)
				return new List<AnswerVersion>();

			return FindByAnswer(o.ID.Value);
		}

		public static IList<AnswerVersion> FindByAnswer(long id)
		{
			return FindAllBy<AnswerVersion>( new Dictionary<string, object> {
				{ "DSQAnswerID", id }
			}, new[] { "-Version" } );
		}

		public static IList<AnswerVersion> FindByDrugVersion(long drug_id, int version)
		{
			var db = Database.Get("FDARems");
			string sql = "SELECT " + db.DelimTable("dav") + ".* " +
							" FROM " + db.DelimTable("DSQ_AnswerVersions") + " AS " + db.DelimTable("dav") +
								" LEFT JOIN " + db.DelimTable("DSQ_Answers") + " AS " + db.DelimTable("da") +
									" ON " + db.DelimTable("dav") + "." + db.DelimColumn("DSQAnswerID") + " = " + db.DelimTable("da") + "." + db.DelimColumn("ID") +
							" WHERE [da].[DrugID] = @id AND [dav].[Version] = @version" +
							" ORDER BY [da].[DrugID] ASC";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("id", drug_id));
			ps.Add(new Parameter("version", version));

			return db.ExecuteQuery<AnswerVersion>(sql, ps.ToArray());
		}

		public static AnswerVersion FindByDrugVersion(long drug_id, int version, long answer_id)
		{
			var db = Database.Get("FDARems");
			string sql = "SELECT " + db.DelimTable("dav") + ".* " +
							" FROM " + db.DelimTable("DSQ_AnswerVersions") + " AS " + db.DelimTable("dav") +
								" LEFT JOIN " + db.DelimTable("DSQ_Answers") + " AS " + db.DelimTable("da") +
									" ON " + db.DelimTable("dav") + "." + db.DelimColumn("DSQAnswerID") + " = " + db.DelimTable("da") + "." + db.DelimColumn("ID") +
							" WHERE [da].[DrugID] = @id AND [dav].[Version] = @version AND [da].[ID] = @answer_id" +
							" ORDER BY [da].[DrugID] ASC";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("id", drug_id));
			ps.Add(new Parameter("version", version));
			ps.Add(new Parameter("answer_id", answer_id));

			var rows = db.ExecuteQuery<AnswerVersion>(sql, ps.ToArray());

			if( rows.Count > 0 )
				return rows[0];

			return null;
		}

		[Column]
		public long DSQAnswerID;
		[Column]
		public int Version;
		[Column]
		public string Value;

		public AnswerVersion(long? id = null) : base(id)
		{ }

		public AnswerVersion(IDataRecord row) : base(row)
		{ }
	}
}
