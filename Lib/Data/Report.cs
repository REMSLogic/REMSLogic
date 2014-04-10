using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "Reports", PrimaryKeyColumn = "ID" )]
	public class Report : ActiveRecord
	{
		public static IList<Report> FindAll()
		{
			return FindAll<Report>();
		}

		[Column]
		public string Name;
		[Column]
		public string Description;
		[Column]
		public string ForRoles;

		public Report(long? id = null) : base(id)
		{ }

		public Report(IDataRecord row) : base(row)
		{ }

		public IList<ReportFilter> GetFilters()
		{
			return ReportFilter.FindByReport( this );
		}

		public IList<ReportOutput> GetOutputs()
		{
			return ReportOutput.FindByReport( this );
		}
	}
}
