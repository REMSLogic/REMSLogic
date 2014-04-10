using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Framework.Data
{
	public class ConcreteActiveRecord : ActiveRecord
	{
		public static IList<ConcreteActiveRecord> FindAll(string db_name, string tbl_name, string id_col)
		{
			var db=Database.Get(db_name);
			var t = Table.Get(db, tbl_name);

			return FindAll(t, id_col);
		}

		public static IList<ConcreteActiveRecord> FindAll(Table t, string id_col)
		{
			var db = t.DB;
			var sql = "SELECT * FROM "+db.Delim(t.Name, DelimType.Table);
			var rows = db.ExecuteQuery(sql);

			var ret = new List<ConcreteActiveRecord>();
			foreach( var row in rows )
			{
				ret.Add( new ConcreteActiveRecord(t, id_col, row));
			}

			return ret;
		}

		protected Dictionary<string, object> m_aValues;

		public object this[string name]
		{
			get { if( m_aValues == null ) return null; return m_aValues[name]; }
			set { if( m_aValues == null ) return; m_aValues[name] = value; }
		}

		public ConcreteActiveRecord(Table t, string id_col, long? id = null)
			: base( t, id_col, id )
		{
		}

		public ConcreteActiveRecord(string db, string t, string id_col, long? id = null)
			: base( db, t, id_col, id )
		{
		}

		public ConcreteActiveRecord(Table t, string id_col, IDataRecord row)
			: base( t, id_col, row )
		{
		}

		public ConcreteActiveRecord(string db, string t, string id_col, IDataRecord row)
			: base( db, t, id_col, row )
		{
		}

		protected override void LoadData(System.Data.IDataRecord row)
		{
			ID = (long)row[IDCol];
			m_aValues = new Dictionary<string, object>();

			for( int i = 0; i < row.FieldCount; i++ )
				if( row.GetName( i ) != this.IDCol )
					m_aValues.Add( row.GetName( i ), row.GetValue( i ) );
		}

		protected override Dictionary<string, object> GetData()
		{
			return m_aValues ?? new Dictionary<string, object>();
		}
	}
}
