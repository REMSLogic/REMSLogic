using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Data.CodeGen
{
	public class Manager
	{
		public static void BuildActiveRecordClasses(string path, Dictionary<string, string> class_name_overrides)
		{
			var db = Framework.Data.Database.Get("Site");
			var tables = db.GetTables();

			var class_lookup = new Dictionary<string, Framework.Data.CodeGen.ActiveRecord.ClassInfo>();

			foreach (var tbl in tables)
			{

				var cols = tbl.Columns;
				var pks = tbl.PrimaryKeys;

				if (pks.Count != 1 && pks.Count != cols.Count)
					continue;

				if (pks.Count == cols.Count)
					continue;

				class_lookup[tbl.Name] = new Framework.Data.CodeGen.ActiveRecord.ClassInfo()
				{
					Name = tbl.Name,
					Table = tbl
				};
			}

			foreach( var k in class_name_overrides.Keys )
			{
				class_lookup[k].Name = class_name_overrides[k];
			}

			foreach (var k in class_lookup.Keys)
			{
				Framework.Data.CodeGen.ActiveRecord.Create(path, class_lookup[k].Table, class_lookup);
			}
		}
	}
}
