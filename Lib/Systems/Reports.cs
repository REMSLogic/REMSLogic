using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Systems
{
	public class Reports
	{
		public static bool CanViewReport(Lib.Data.Report report)
		{
			if( string.IsNullOrWhiteSpace( report.ForRoles ) )
				return Framework.Security.Manager.HasRole( "dev" );

			var roles = report.ForRoles.Split( new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries );

			foreach( var role in roles )
			{
				if( Framework.Security.Manager.HasRole( role ) )
				{
					return true;
				}
			}

			return false;
		}
		
		public static IList<Data.Report> GetMyReports()
		{
			var ret = new List<Data.Report>();
			var reports = Data.Report.FindAll();

			foreach(var report in reports)
			{
				if( CanViewReport( report ) )
					ret.Add( report );
			}

			return ret;
		}

		public struct ReportOutputReturn
		{
			public string OutputType;
			public string OutputParameters;
			public List<Dictionary<string,object>> Data;
		}

		public static ReportOutputReturn RunOutput( Lib.Data.Report report, Lib.Data.ReportOutput output, Dictionary<string, object> data )
		{
			var ret = new ReportOutputReturn {
				OutputType = output.Type.ToString(),
				OutputParameters = output.Parameters,
				Data = new List<Dictionary<string,object>>()
			};

			var ps = new List<Framework.Data.Parameter>();

			foreach( var k in data.Keys )
			{
				if( data[k] != null )
					ps.Add( new Framework.Data.Parameter( k, data[k] ) );
			}

			var filters = report.GetFilters();

			foreach( var filter in filters )
			{
				if( !data.ContainsKey( filter.ParameterName ) )
				{
					Type t = null;

					switch( filter.Type )
					{
					case Data.ReportFilter.FilterTypes.DateTime:
						t = typeof( DateTime );
						break;
					case Data.ReportFilter.FilterTypes.Integer:
						t = typeof( int );
						break;
					case Data.ReportFilter.FilterTypes.String:
						t = typeof( string );
						break;
					}

					ps.Add( new Framework.Data.Parameter( filter.ParameterName, null, t ) );
				}
			}

			var rows = output.Run( ps );

			foreach( var row in rows )
			{
				var dr = new Dictionary<string, object>();

				for( int i=0; i < row.FieldCount; i++ )
				{
					string name = row.GetName( i );
					object value = row.GetValue( i );

					if( value == DBNull.Value )
						value = null;

					dr[name] = value;
				}

				ret.Data.Add( dr );
			}

			return ret;
		}
	}
}
