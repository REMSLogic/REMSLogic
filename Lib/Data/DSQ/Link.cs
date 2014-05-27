using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data.DSQ
{
	[Table( DatabaseName = "FDARems", TableName = "DSQ_Links", PrimaryKeyColumn = "ID" )]
	public class Link : ActiveRecord
	{
		public static IList<Link> FindByDrug(Drug d, Question q)
		{
			if (d == null || d.ID == null || q == null || q.ID == null)
				return null;

			return FindByDrug(d.ID.Value, q.ID.Value);
		}

		public static IList<Link> FindByDrug(long did, long qid)
		{
			return FindAllBy<Link>( new Dictionary<string, object> {
				{ "DrugID", did },
				{ "QuestionID", qid }
			} );
		}

		[Column]
		public long DrugID;
		[Column]
		public long QuestionID;
		[Column]
		public string Label;
		[Column]
		public string Value;
		[Column]
		public string HelpText;
		[Column]
		public DateTime? Date;
        [Column]
        public long EocId;
        [Column]
        public bool IsRequired;
        [Column]
        public string LinkType;

        public bool HasEoc
        {
            get {return EocId > 0;}
        }

		public Drug Drug
		{ get { return new Drug( DrugID ); } }

		public Link(long? id = null) : base(id)
		{ }

		public Link(IDataRecord row) : base(row)
		{ }
	}
}
