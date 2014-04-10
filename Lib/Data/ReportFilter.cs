using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "ReportFilters", PrimaryKeyColumn = "ID" )]
	public class ReportFilter : ActiveRecord
	{
		public enum FilterTypes
		{
			Invalid = 0,
			String,
			Integer,
			DateTime
		}

		public static IList<ReportFilter> FindAll()
		{
			return FindAll<ReportFilter>();
		}

		public static IList<ReportFilter> FindByReport( Report r )
		{
			if( r == null || r.ID == null )
				return new List<ReportFilter>();

			return FindByReport( r.ID.Value );
		}

		public static IList<ReportFilter> FindByReport( long report_id )
		{
			return FindAllBy<ReportFilter>( new Dictionary<string, object> {
				{ "ReportID", report_id }
			} );
		}

		[Column]
		public long ReportID;
		[Column]
		public string DisplayName;
		[Column]
		public string PlaceholderText;
		[Column]
		public string HelpText;
		[Column]
		public string ParameterName;
		[Column]
		public FilterTypes Type;
		[Column]
		public string Parameters;

		public ReportFilter(long? id = null) : base( id )
		{ }

		public ReportFilter( IDataRecord row ) : base( row )
		{ }
	}
}
