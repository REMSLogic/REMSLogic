using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Framework.Data;
using Framework.Security;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "ReportOutputs", PrimaryKeyColumn = "ID" )]
	public class ReportOutput : ActiveRecord
	{
		public enum OutputTypes
		{
			Invalid = 0,
			Table,
			PieChart
		}

		public static IList<ReportOutput> FindAll()
		{
			return FindAll<ReportOutput>();
		}

		public static IList<ReportOutput> FindByReport(Report r)
		{
			if( r == null || r.ID == null )
				return new List<ReportOutput>();

			return FindByReport( r.ID.Value );
		}

		public static IList<ReportOutput> FindByReport( long report_id )
		{
			return FindAllBy<ReportOutput>( new Dictionary<string, object> {
				{ "ReportID", report_id }
			} );
		}

		[Column]
		public long ReportID;
		[Column]
		public string Name;
		[Column]
		public string SqlText;
		[Column]
		public string SqlType;
		[Column]
		public OutputTypes Type;
		[Column]
		public string Parameters;

		public ReportOutput(long? id = null) : base( id )
		{ }

		public ReportOutput( IDataRecord row ) : base( row )
		{ }

		public IList<IDataRecord> Run( List<Framework.Data.Parameter> parameters )
		{
			var db = this.table.DB;

            // hack to get the currenly logged in HCO id into a query
            if(SqlText.Contains("{{HCO-ID}}") || SqlText.Contains("{{FACILITY-ID}}"))
            {
                User user = Framework.Security.Manager.GetUser();
                UserProfile userProfile = UserProfile.FindByUser(user);
                ProviderUser providerUser = ProviderUser.FindByProfile(userProfile);

                SqlText = SqlText.Replace("{{HCO-ID}}", providerUser != null 
                    ? providerUser.ProviderID.ToString(CultureInfo.InvariantCulture)
                    : "0");

                SqlText = SqlText.Replace("{{FACILITY-ID}}", providerUser != null
                    ? (providerUser.PrimaryFacilityID ?? 0).ToString(CultureInfo.InvariantCulture)
                    : "0");
            }

			return db.ExecuteQuery( this.SqlText, parameters.ToArray(), ((this.SqlType == "text") ? CommandType.Text : CommandType.StoredProcedure) );
		}
	}
}
