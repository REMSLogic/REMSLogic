using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Framework.Data
{
	public class Table
	{
		private static Dictionary<Database, Dictionary<string, Table>> instances;
		static Table()
		{
			instances = new Dictionary<Database, Dictionary<string, Table>>();
		}

		public static Table Get(Database db, string name)
		{
			if (instances.ContainsKey(db))
			{
				if (instances[db].ContainsKey(name))
					return instances[db][name];
				else
				{
					var inst = new Table(db, name);
					instances[db].Add(name, inst);
					return inst;
				}
			}
			else
			{
				var inst = new Table(db,name);
				instances.Add(db, new Dictionary<string,Table>());
				instances[db].Add(name,inst);

				return inst;
			}
		}

		public static Table Get(string db_name, string name)
		{
			var db = Database.Get(db_name);
			if (instances.ContainsKey(db))
			{
				if (instances[db].ContainsKey(name))
					return instances[db][name];
				else
				{
					var inst = new Table(db, name);
					instances[db].Add(name, inst);
					return inst;
				}
			}
			else
			{
				var inst = new Table(db, name);
				instances.Add(db, new Dictionary<string, Table>());
				instances[db].Add(name, inst);

				return inst;
			}
		}

		public static Table Get( Type type )
		{
			var info = Manager.GetInfoForType( type );
			var db_name = info.Attr.DatabaseName;
			var name = info.Attr.TableName;

			var db = Database.Get( db_name );
			if( instances.ContainsKey( db ) )
			{
				if( instances[db].ContainsKey( name ) )
					return instances[db][name];
				else
				{
					var inst = new Table( db, name );
					instances[db].Add( name, inst );
					return inst;
				}
			}
			else
			{
				var inst = new Table( db, name );
				instances.Add( db, new Dictionary<string, Table>() );
				instances[db].Add( name, inst );

				return inst;
			}
		}

		public Database DB;
		protected string name;
		public string Name { get { return name; } }

		private IList<Database.ColumnInfo> _cacheColumns = null;
		public IList<Database.ColumnInfo> Columns
		{
			get
			{
				if (_cacheColumns == null)
					_cacheColumns = this.DB.GetColumns(this.name);

				return _cacheColumns;
			}
		}

		private IList<Database.PrimaryKeyInfo> _cachePrimaryKeys = null;
		public IList<Database.PrimaryKeyInfo> PrimaryKeys
		{
			get
			{
				if (_cachePrimaryKeys == null)
					_cachePrimaryKeys = this.DB.GetPrimaryKeys(this.name);

				return _cachePrimaryKeys;
			}
		}

		private IList<Database.ForeignKeyInfo> _cacheForeignKeys = null;
		public IList<Database.ForeignKeyInfo> ForeignKeys
		{
			get
			{
				if (_cacheForeignKeys == null)
					_cacheForeignKeys = this.DB.GetForeignKeys(this.name);

				return _cacheForeignKeys;
			}
		}

		private Table(Database db, string name)
		{
			this.DB = db;
			this.name = name;
		}

        public IList<System.Data.IDataRecord> Select(string[] columns = null, string where = null, Framework.Data.Parameter[] parameters = null)
		{
			string sql = "";
			sql += "SELECT ";

			if (columns == null)
				sql += "*";
			else
			{
				bool first = true;

				foreach (var c in columns)
				{
					if (!first)
						sql += ", ";

					first = false;
					sql += DB.Delim(c, DelimType.Column);
				}
			}

			sql += " FROM " + DB.Delim(name, DelimType.Table);

			if (!string.IsNullOrEmpty(where))
				sql += " WHERE " + where;

			return DB.ExecuteQuery(sql, parameters);
		}

		public long Insert(Dictionary<string,object> record)
		{
			var list = new List<Dictionary<string, object>>();
			list.Add(record);
			return Insert(list);
		}

		public long Insert(IList<Dictionary<string, object>> records)
		{
			string sql = "INSERT INTO " + DB.Delim(name, DelimType.Table) + "(";
			List<string> cols = new List<string>();

			if (records.Count == 0)
				return 0;

			foreach (var k in records[0].Keys)
			{
				if( cols.Count > 0 )
					sql += ", ";
				sql += DB.Delim(k, DelimType.Column);
				cols.Add(k);
				
			}

			sql += ") VALUES ";

            List<Framework.Data.Parameter> parameters = new List<Framework.Data.Parameter>();

			for (int i = 0, len = records.Count; i < len; i++)
			{
				if( records[i].Keys.Count != cols.Count )
					throw new Exception("All rows must have the same number of columns.");

				if (i > 0)
					sql += ", ";
				sql += "(";

				for (int j = 0; j < cols.Count; j++)
				{
					if (j > 0)
						sql += ", ";

					if( records[i][cols[j]] == null )
					{
						sql += "NULL";
						continue;
					}

					string pname = cols[j]+i.ToString();

					sql += "@"+pname;

                    parameters.Add(new Framework.Data.Parameter(pname, records[i][cols[j]]));
				}

				sql += ")";
			}

			sql += "; SELECT SCOPE_IDENTITY();";

			return Convert.ToInt64(DB.ExecuteScalar<object>(sql, parameters.ToArray()));
		}

        public int Update(Dictionary<string, object> record, string where = null, Framework.Data.Parameter[] parameters = null)
		{
			string sql = "UPDATE " + DB.Delim(name, DelimType.Table) + " SET ";
			List<string> cols = new List<string>();
            var lstParams = new List<Framework.Data.Parameter>();

			if (parameters != null)
				lstParams.AddRange(parameters);

			foreach (var k in record.Keys)
			{
				if (cols.Count > 0)
					sql += ", ";

				cols.Add( k );

				if( record[k] == null )
				{
					sql += DB.Delim( k, DelimType.Column ) + " = NULL" ;
					continue;
				}

				string pname = "upd" + k;
				sql += DB.Delim(k, DelimType.Column) + "=@" + pname;
                lstParams.Add(new Framework.Data.Parameter(pname, record[k]));
			}

			if( !string.IsNullOrEmpty(where) )
				sql += " WHERE "+where;

			return DB.ExecuteNonQuery(sql, lstParams.ToArray());
		}

        public int Delete(string where = null, Framework.Data.Parameter[] parameters = null)
		{
			string sql = "";
			sql += "DELETE FROM " + DB.Delim(this.name, DelimType.Table);
			if (!string.IsNullOrEmpty(where))
				sql += " WHERE " + where;

			return DB.ExecuteNonQuery(sql, parameters);
		}
	}
}
