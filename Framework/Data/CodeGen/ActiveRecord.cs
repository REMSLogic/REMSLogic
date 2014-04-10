using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Data.CodeGen
{
	public static class ActiveRecord
	{
		public class ClassInfo
		{
			public string Name;
			public Table Table;
		}
		public static void Create(string file_path, Table tbl, Dictionary<string, ClassInfo> class_lookup)
		{
			var cols = tbl.Columns;
			var pks = tbl.PrimaryKeys;
			var fks = tbl.ForeignKeys;

			if( pks.Count != 1 )
				throw new Exception("Can not build a class deriving from ActiveRecord with more than one primary key column. ["+tbl.Name+"] has ["+pks.Count+"] key columns.");

			var pk = pks[0];
			var ci = class_lookup[tbl.Name];

			var file = new Framework.CodeGen.File() {
				Usings = new List<string>() {
					"System",
					"System.Collections.Generic",
					"System.Data",
					"Framework.Data"
				},
				Namespace = new Framework.CodeGen.Namespace() {
					Name = "Lib.Data",
					Class = new Framework.CodeGen.Class() {
						Name = ci.Name,
						Inherits = "ActiveRecord",
						StaticMethods = new List<Framework.CodeGen.Method>() {
							new Framework.CodeGen.Method() {
								Accessor = Framework.CodeGen.Accessor.Public,
								IsStatic = true,
								ReturnType = "IList<"+ci.Name+">",
								Name = "FindAll",
								Body = "" +
									"var db = Database.Get(\"Site\");\n" +
									"string sql = \"SELECT * FROM \"+db.Delim(\""+tbl.Name+"\", DelimType.Table);\n" +
									"\n" +
									"return db.ExecuteQuery<"+ci.Name+">(sql);\n"
							}
						},
						Fields = new List<Framework.CodeGen.Field>(),
						Constructors = new List<Framework.CodeGen.Method>() {
							new Framework.CodeGen.Method() {
								Accessor = Framework.CodeGen.Accessor.Public,
								Name = ci.Name,
								Parameters = new List<Framework.CodeGen.Parameter>() {
									new Framework.CodeGen.Parameter() {
										Type = "long?",
										Name = "id",
										DefaultValue = "null"
									}
								},
								InitializerList = "base(\"Site\", \""+tbl.Name+"\", \""+pk.Column+"\", id)"
							},
							new Framework.CodeGen.Method() {
								Accessor = Framework.CodeGen.Accessor.Public,
								Name = ci.Name,
								Parameters = new List<Framework.CodeGen.Parameter>() {
									new Framework.CodeGen.Parameter() {
										Type = "IDataRecord",
										Name = "row"
									}
								},
								InitializerList = "base(\"Site\", \"" + tbl.Name + "\", \""+pk.Column+"\", row)"
							}
						}
					}
				}
			};

			foreach( var col in cols )
			{
				if( pk.Column == col.Name )
					continue;

				file.Namespace.Class.Fields.Add(new Framework.CodeGen.Field() {
					Attributes = new List<Framework.CodeGen.Attribute>() {
						new Framework.CodeGen.Attribute() {
							Name = "Column"
						}
					},
					Accessor = Framework.CodeGen.Accessor.Public,
					Type = MapDataType(col),
					Name = col.Name
				});
			}

			foreach(var fk in fks)
			{
				var fkc = class_lookup[fk.ForeignTable];

				file.Namespace.Class.StaticMethods.Add(new Framework.CodeGen.Method() {
					Accessor = Framework.CodeGen.Accessor.Public,
					IsStatic = true,
					ReturnType = "IList<" + ci.Name + ">",
					Name = "FindBy" + fkc.Name,
					Parameters = new List<Framework.CodeGen.Parameter>() {
						new Framework.CodeGen.Parameter() {
							Type = fkc.Name,
							Name = "o",
							DefaultValue = "null"
						}
					},
					Body = "" +
						"if( o == null )\n" +
						"\treturn new List<" + ci.Name + ">();\n" +
						"\n" +
						"return FindBy" + fkc.Name + "(o.ID);\n"
				});

				file.Namespace.Class.StaticMethods.Add(new Framework.CodeGen.Method()
				{
					Accessor = Framework.CodeGen.Accessor.Public,
					IsStatic = true,
					ReturnType = "IList<" + ci.Name + ">",
					Name = "FindBy" + fkc.Name,
					Parameters = new List<Framework.CodeGen.Parameter>() {
						new Framework.CodeGen.Parameter() {
							Type = "long?",
							Name = "id",
							DefaultValue = "null"
						}
					},
					Body = "" +
						"if( id == null )\n" +
						"\treturn new List<" + ci.Name + ">();\n" +
						"\n" +
						"var db = Database.Get(\"Site\");\n" +
						"string sql = \"SELECT * FROM \" + db.Delim(\"" + tbl.Name + "\", DelimType.Table) + \" WHERE \" + db.Delim(\""+fk.Column+"\", DelimType.Column) + \" = \" + db.Delim(\"id\", DelimType.Parameter);\n" +
						"var ps = new List<Parameter>();\n" +
						"ps.Add(new Parameter(\"id\", id.Value));\n" +
						"\n" +
						"return db.ExecuteQuery<" + ci.Name + ">(sql);\n"
				});
			}

			Framework.CodeGen.Writer.Write(System.IO.Path.Combine(file_path,ci.Name+".cs"), file);
		}

		private static string MapDataType(Framework.Data.Database.ColumnInfo col)
		{
			string sql_data_type = col.DataType.ToLower();

			switch (sql_data_type)
			{
			case "bit":
				return "bool" + (col.IsNullable ? "?" : "");
			case "date":
			case "datetime":
				return "DateTime" + (col.IsNullable ? "?" : "");
			case "tinyint":
				return "byte" + (col.IsNullable ? "?" : "");
			case "int":
				return "int" + (col.IsNullable ? "?" : "");
			case "bigint":
				return "long" + (col.IsNullable ? "?" : "");
			case "float":
				return "double" + (col.IsNullable ? "?" : "");
			case "decimal":
			case "money":
				return "decimal" + (col.IsNullable ? "?" : "");
			case "sysname":
			case "char":
			case "nchar":
			case "varchar":
			case "nvarchar":
			case "text":
			case "ntext":
				return "string";
			default:
				return null;
			}
		}
	}
}
