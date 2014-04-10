using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "DrugFormulations", PrimaryKeyColumn = "ID" )]
	public class DrugFormulation : ActiveRecord
	{
		public static IList<DrugFormulation> FindByDrug(Drug d)
		{
			if( d == null || d.ID == null )
				return new List<DrugFormulation>();

			return FindByDrug(d.ID.Value);
		}

		public static IList<DrugFormulation> FindByDrug(long did)
		{
			return FindAllBy<DrugFormulation>( new Dictionary<string, object> {
				{ "DrugID", did }
			}, new [] { "+BrandName", "+DrugCompanyID" } );
		}

		[Column]
		public long DrugID;
		[Column]
		public long FormulationID;
		[Column]
		public string BrandName;
		[Column]
		public long DrugCompanyID;
		[Column]
		public string DrugCompanyURL;
		[Column]
		public string DrugCompanyEmail;
		[Column]
		public string DrugCompanyPhone;
		[Column]
		public string DrugCompanyFax;

		public Drug Drug { get { return new Drug(DrugID); } }
		public Formulation Formulation { get { return new Formulation(FormulationID); } }
		public DrugCompany DrugCompany { get { return new DrugCompany(DrugCompanyID); } }

		public DrugFormulation(long? id = null) : base(id)
		{ }

		public DrugFormulation(IDataRecord row) : base(row)
		{ }
	}
}
