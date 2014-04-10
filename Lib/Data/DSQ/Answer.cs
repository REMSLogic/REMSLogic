using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data.DSQ
{
	[Table( DatabaseName = "FDARems", TableName = "DSQ_Answers", PrimaryKeyColumn = "ID" )]
	public class Answer : ActiveRecord
	{
		public static Answer FindByDrug(Drug d, Question q)
		{
			if (d == null || !d.ID.HasValue || q == null || !q.ID.HasValue)
				return null;

			return FindByDrug(d.ID.Value, q.ID.Value);
		}

		public static Answer FindByDrug(long did, long qid)
		{
			return FindFirstBy<Answer>( new Dictionary<string, object> {
				{ "DrugID", did },
				{ "QuestionID", qid }
			} );
		}

		[Column]
		public long DrugID;
		[Column]
		public long QuestionID;
		[Column]
		public string Value;

		public Answer(long? id = null) : base(id)
		{ }

		public Answer(IDataRecord row) : base(row)
		{ }
	}
}
