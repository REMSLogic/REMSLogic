using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Framework.Data.SqlServer
{
	public class Database : Framework.Data.Database
	{
		public Database(string cs) : base(cs)
		{
		}

		public override IList<Table> GetTables()
		{
			var ret = new List<Table>();

			var sql = "SELECT [name] FROM [sys].[objects] WHERE [type] = 'U' ORDER BY [name]";
			var rows = this.ExecuteQuery( sql );

			foreach( var row in rows )
				ret.Add( Table.Get( this, (string)row["name"] ) );

			return ret;
		}

		public override IList<Data.Database.ColumnInfo> GetColumns(string tbl_name)
		{
			var ret = new List<ColumnInfo>();

			var sql = "SELECT c.name 'name', t.name 'type', c.max_length 'length', c.precision, c.scale, c.is_nullable " +
						"FROM sys.columns c " +
						"	INNER JOIN sys.types t " +
						"		ON c.user_type_id = t.user_type_id " +
						"WHERE c.object_id = OBJECT_ID('"+tbl_name+"')";
			var rows = this.ExecuteQuery( sql );

			foreach( var row in rows )
			{
				var ci = new ColumnInfo() {
					Name = (string)row["name"],
					DataType = (string)row["type"],
					MaxLength = (short)row["length"],
					Precision = (byte)row["precision"],
					Scale = (byte)row["scale"],
					IsNullable = (bool)row["is_nullable"]
				};

				ret.Add( ci );
			}

			return ret;
		}

		public override IList<Data.Database.PrimaryKeyInfo> GetPrimaryKeys(string tbl_name)
		{
			var ret = new List<PrimaryKeyInfo>();

			var sql = "SELECT c.name " +
						"FROM sys.columns c " +
						"	LEFT JOIN sys.index_columns ic " +
						"		ON ic.object_id = c.object_id AND ic.column_id = c.column_id " +
						"	LEFT JOIN sys.indexes i " +
						"		ON ic.object_id = i.object_id AND ic.index_id = i.index_id " +
						"WHERE c.object_id = OBJECT_ID('"+tbl_name+"') AND i.is_primary_key = 1";
			var rows = this.ExecuteQuery(sql);

			foreach (var row in rows)
			{
				var ci = new PrimaryKeyInfo()
				{
					Column = (string)row["name"]
				};

				ret.Add(ci);
			}

			return ret;
		}

		public override IList<Data.Database.ForeignKeyInfo> GetForeignKeys(string tbl_name)
		{
			var ret = new List<ForeignKeyInfo>();

			var sql = "SELECT [c].[name] AS [column], [c2].[name] AS [foreign_column], [o].[name] AS [foreign_table] " +
						"FROM [sys].[columns] AS c " +
						"	LEFT JOIN [sys].[foreign_key_columns] fkc " +
						"		ON c.[object_id] = fkc.[parent_object_id] AND c.[column_id] = fkc.[parent_column_id] " +
						"	LEFT JOIN [sys].[columns] c2 " +
						"		ON fkc.[referenced_object_id] = c2.[object_id] AND fkc.[referenced_column_id] = c2.[column_id] " +
						"	LEFT JOIN [sys].[objects] o " +
						"		ON c2.[object_id] = o.[object_id] " +
						"WHERE c.[object_id] = OBJECT_ID('" + tbl_name + "') AND fkc.parent_object_id IS NOT NULL";
			var rows = this.ExecuteQuery(sql);

			foreach (var row in rows)
			{
				var ci = new ForeignKeyInfo()
				{
					Column = (string)row["column"],
					ForeignTable = (string)row["foreign_table"],
					ForeignColumn = (string)row["foreign_column"],
				};

				ret.Add(ci);
			}

			return ret;
		}

        public override string Delim(string input, DelimType type)
        {
			switch(type)
			{
			case DelimType.Database:
			case DelimType.Schema:
			case DelimType.Table:
			case DelimType.Column:
				return '[' + input + ']';
			case DelimType.Parameter:
				return '@' + input;
			case DelimType.String:
				return "'" + input.Replace("'", "\\'") + "'";
			default:
				throw new Exception("Unrecognized [DelimType].[" + type.ToString() + "].");
			}
        }

        protected override DbConnection GetConnection(string cs)
        {
            return new SqlConnection(cs);
        }

        protected override DbCommand GetCommand(string sql, DbConnection conn)
        {
            return new SqlCommand(sql, (SqlConnection)conn);
        }

        protected override DbParameter GetParameter(Parameter p)
        {
            var ret = new SqlParameter('@'+p.Name, p.Value);

			if( p.DataType != null )
			{
				switch( p.DataType.FullName )
				{
				case "System.String":
					ret.SqlDbType = System.Data.SqlDbType.NVarChar;
					break;
				case "System.DateTime":
					ret.SqlDbType = System.Data.SqlDbType.DateTime;
					break;
				case "System.Int16":
					ret.SqlDbType = System.Data.SqlDbType.SmallInt;
					break;
				case "System.Int32":
					ret.SqlDbType = System.Data.SqlDbType.Int;
					break;
				case "System.Int64":
					ret.SqlDbType = System.Data.SqlDbType.BigInt;
					break;
				}
			}

			return ret;
        }

        protected override DbTransaction BeginTransaction(string name)
        {
            return ((SqlConnection)conn).BeginTransaction(name);
        }
	}
}
