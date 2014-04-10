using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Data
{
	public class TableAttribute : Attribute
	{
		public string DatabaseName;
		public string TableName;
		public string PrimaryKeyColumn;

		public TableAttribute()
		{
			DatabaseName = null;
			TableName = null;
			PrimaryKeyColumn = null;
		}

		public TableAttribute(string db, string name)
		{
			DatabaseName = db;
			TableName = name;
			PrimaryKeyColumn = null;
		}
	}
}
