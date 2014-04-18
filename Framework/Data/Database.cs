using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Framework.Data
{
	public enum DelimType
	{
		Database,
		Schema,
		Table,
		Column,
		Parameter,
		String
	}

	public abstract class Database
	{
		private static Dictionary<string, Database> instances;
		
		static Database()
		{
			instances = new Dictionary<string, Database>();
		}

		public static Database Get(string name)
		{
			if (instances.ContainsKey(name))
				return instances[name];

            var inst = Manager.Create("SqlServer", ConfigurationManager.ConnectionStrings[name].ConnectionString);
			instances.Add(name, inst);

			return inst;
		}

        public static void CloseConnections(string id)
        {
			if( string.IsNullOrEmpty( id ) )
				return;

            foreach (var db_key in instances.Keys)
            {
                var db = instances[db_key];

                if (db.connections.ContainsKey(id))
                {
                    var c = db.connections[id];

                    if (c.State != ConnectionState.Closed)
                        c.Close();

					c.Dispose();
					db.connections.Remove( id );
                }
            }
        }

        protected Dictionary<string, DbConnection> connections;
        protected string connectionString;
        protected DbConnection conn
        {
            get
            {
				string id = System.Web.HttpContext.Current.Request.Headers["DBUNIQUEID"];

                if (!connections.ContainsKey(id))
                    connections[id] = GetConnection(connectionString);

                return connections[id];
            }
            set
            {
				string id = System.Web.HttpContext.Current.Request.Headers["DBUNIQUEID"];
                connections[id] = value;
            }
        }
        protected DbTransaction transaction;

		protected Database(string cs)
		{
            connectionString = cs;
            connections = new Dictionary<string, DbConnection>();
            transaction = null;
		}

		public IList<IDataRecord> ExecuteQuery(string sql, Parameter[] parameters = null, CommandType type = CommandType.Text)
		{
			OpenConnection();
			var cmd = GetCommand(sql, conn);
			cmd.CommandType = type;
			if( transaction != null )
				cmd.Transaction = transaction;
            if (parameters != null)
            {
                foreach( var p in parameters )
                    cmd.Parameters.Add(GetParameter(p));
            }

			List<IDataRecord> ret = new List<IDataRecord>();

			using (var rdr = cmd.ExecuteReader())
			{
				var i = rdr.GetEnumerator();
				while (i.MoveNext())
					ret.Add((IDataRecord)i.Current);
			}

			return ret;
		}

        public IList<T> ExecuteQuery<T>(string sql, Parameter[] parameters = null, CommandType type = CommandType.Text) where T : RowView
		{
			OpenConnection();
			var cmd = GetCommand(sql, conn);
			cmd.CommandType = type;
			if( transaction != null )
				cmd.Transaction = transaction;
			if (parameters != null)
            {
                foreach (var p in parameters)
                    cmd.Parameters.Add(GetParameter(p));
            }

			var ret = new List<T>();

			using (var rdr = cmd.ExecuteReader())
			{
				var i = rdr.GetEnumerator();

				while (i.MoveNext())
				{
					IDataRecord r = (IDataRecord)i.Current;
					var temp = Manager.Create( typeof( T ), r );
					ret.Add((T)temp);
				}
			}

			return ret;
		}

        public T ExecuteScalar<T>(string sql, Parameter[] parameters = null, CommandType type = CommandType.Text)
		{
			OpenConnection();
			var cmd = GetCommand(sql, conn);
			cmd.CommandType = type;
			if( transaction != null )
				cmd.Transaction = transaction;
			if (parameters != null)
            {
                foreach (var p in parameters)
                    cmd.Parameters.Add(GetParameter(p));
            }

			object o = cmd.ExecuteScalar();
			T ret = default(T);

			if (o != DBNull.Value)
				ret = (T)o;

			return ret;
		}

        public int ExecuteNonQuery(string sql, Parameter[] parameters = null, CommandType type = CommandType.Text)
		{
			OpenConnection();
			var cmd = GetCommand(sql, conn);
			cmd.CommandType = type;
			if( transaction != null )
				cmd.Transaction = transaction;
			if (parameters != null)
            {
                foreach (var p in parameters)
                    cmd.Parameters.Add(GetParameter(p));
            }

			int ret = cmd.ExecuteNonQuery();

			return ret;
		}

		protected void OpenConnection()
		{
			if (conn.State != ConnectionState.Open)
				conn.Open();
		}

		protected void CloseConnection()
		{
			if (conn.State != ConnectionState.Closed)
				conn.Close();
		}

        public bool StartTransaction(string name)
        {
			OpenConnection();
            transaction = BeginTransaction(name);

            return (transaction != null);
        }

        public void EndTransaction(bool commit)
        {
            if (transaction == null)
                return;

			OpenConnection();

            if (commit)
                transaction.Commit();
            else
                transaction.Rollback();

			transaction = null;
        }

		public IList<ColumnInfo> GetColumns(Table tbl)
		{
			return GetColumns(tbl.Name);
		}

		public IList<PrimaryKeyInfo> GetPrimaryKeys(Table tbl)
		{
			return GetPrimaryKeys(tbl.Name);
		}

		public IList<ForeignKeyInfo> GetForeignKeys(Table tbl)
		{
			return GetForeignKeys(tbl.Name);
		}

		public string DelimDatabase(string input) { return this.Delim(input, DelimType.Database); }
		public string DelimSchema(string input) { return this.Delim(input, DelimType.Schema); }
		public string DelimTable(string input) { return this.Delim(input, DelimType.Table); }
		public string DelimTable( string input, string as_name ) { return this.Delim( input, DelimType.Table )+" AS "+this.DelimTable(as_name); }
		public string DelimColumn(string input) { return this.Delim(input, DelimType.Column); }
		public string DelimColumn(string table, string column) { return this.DelimTable(table)+"."+this.DelimColumn( column ); }
		public string DelimColumn( string table, string column, string as_name ) { return this.DelimTable( table ) + "." + this.DelimColumn( column )+" AS "+this.DelimColumn(as_name); }
		public string DelimParameter(string input) { return this.Delim(input, DelimType.Parameter); }
		public string DelimString(string input) { return this.Delim(input, DelimType.String); }

		public abstract IList<Table> GetTables();
		public abstract IList<ColumnInfo> GetColumns( string tbl_name );
		public abstract IList<PrimaryKeyInfo> GetPrimaryKeys(string tbl_name);
		public abstract IList<ForeignKeyInfo> GetForeignKeys(string tbl_name);
        public abstract string Delim(string input, DelimType type);

        protected abstract DbConnection GetConnection(string cs);
        protected abstract DbCommand GetCommand(string sql, DbConnection conn);
        protected abstract DbParameter GetParameter(Parameter p);
        protected abstract DbTransaction BeginTransaction(string name);

		public struct ColumnInfo
		{
			public string Name;
			public string DataType;
			public short MaxLength;
			public byte Precision;
			public byte Scale;
			public bool IsNullable;
		}

		public struct PrimaryKeyInfo
		{
			public string Column;
		}

		public struct ForeignKeyInfo
		{
			public string Column;
			public string ForeignTable;
			public string ForeignColumn;
		}
	}
}
