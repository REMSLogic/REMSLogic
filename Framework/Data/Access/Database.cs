using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Text;

namespace Framework.Data.Access
{
	public class Database : Framework.Data.Database
	{
		public Database(string cs) : base(cs)
		{
		}

		public override IList<Table> GetTables()
		{
			throw new NotImplementedException();
		}

		public override IList<Data.Database.ColumnInfo> GetColumns( string tbl_name )
		{
			throw new NotImplementedException();
		}

		public override IList<Data.Database.PrimaryKeyInfo> GetPrimaryKeys(string tbl_name)
		{
			throw new NotImplementedException();
		}

		public override IList<Data.Database.ForeignKeyInfo> GetForeignKeys(string tbl_name)
		{
			throw new NotImplementedException();
		}

        public override string Delim(string input, DelimType type)
        {
			switch (type)
			{
			case DelimType.Database:
			case DelimType.Schema:
			case DelimType.Table:
			case DelimType.Column:
				return '[' + input + ']';
			case DelimType.Parameter:
				return '@' + input;
			case DelimType.String:
				return '"' + input.Replace("\"", "\\\"") + '"';
			default:
				throw new Exception("Unrecognized [DelimType].[" + type.ToString() + "].");
			}
        }

        protected override System.Data.Common.DbConnection GetConnection(string cs)
        {
            return new OleDbConnection(cs);
        }

        protected override System.Data.Common.DbCommand GetCommand(string sql, System.Data.Common.DbConnection conn)
        {
            return new OleDbCommand(sql, (OleDbConnection)conn);
        }

        protected override System.Data.Common.DbParameter GetParameter(Parameter p)
        {
            return new OleDbParameter('@'+p.Name, p.Value);
        }

        protected override DbTransaction BeginTransaction(string name)
        {
            return null;
        }
    }
}
