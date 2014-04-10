using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.App
{
	public class Reports : Base
	{
		// TODO: Get Filters
		[Method( "App/Reports/Get" )]
		public static ReturnObject Get( HttpContext context, long id, long output_id )
		{
			var report = new Lib.Data.Report( id );

			if( !Lib.Systems.Reports.CanViewReport(report) )
				return new ReturnObject() {
					Error = true,
					Message = "Invalid report requested."
				};

			var output = new Lib.Data.ReportOutput( output_id );

			if( output.ReportID != report.ID )
				return new ReturnObject() {
					Error = true,
					Message = "Invalid output requested."
				};

			var filters = report.GetFilters();
			var data = new Dictionary<string, object>();

			foreach( var f in filters )
			{
				string name = "filter-" + f.ID.Value.ToString();

				if( string.IsNullOrWhiteSpace( context.Request.Form[name] ) )
					continue;

				string val = context.Request.Form[name];

				object r = null;
				switch( f.Type )
				{
				case Data.ReportFilter.FilterTypes.String:
					r = val;
					break;
				case Data.ReportFilter.FilterTypes.Integer:
					int t;
					if( int.TryParse(val, out t) )
						r = t;
					break;
				case Data.ReportFilter.FilterTypes.DateTime:
					DateTime dt;
					if( DateTime.TryParse( val, out dt ) )
						r = dt;
					break;
				}

				if( r == null )
					return new ReturnObject() {
						Error = true,
						Message = "Invalid filter value \""+val+"\" for filter ["+f.DisplayName+"]."
					};

				data[f.ParameterName] = r;
			}

			return new ReturnObject() {
				Error = false,
				Result = Lib.Systems.Reports.RunOutput(report, output, data)
			};
		}
	}
}
