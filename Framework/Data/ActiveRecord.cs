using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Framework.Data
{
	public abstract class ActiveRecord : RowView, IComparable<ActiveRecord>, IEquatable<ActiveRecord>
	{
		// Must be generic + protected because no late static binding
		protected static IList<T> FindAll<T>( IEnumerable<string> order_by = null ) where T : ActiveRecord
		{
			var db = Database.Get( "FDARems" );
			string sql = "SELECT * " +
							"FROM " + db.DelimTable( Manager.GetTableNameFromType(typeof(T)) );

			sql += " " + Command.ParseOrderBy( db, order_by );

			return db.ExecuteQuery<T>( sql );
		}

		protected static IList<T> FindAllBy<T>( Dictionary<string, object> paramereters, IEnumerable<string> order_by = null ) where T : ActiveRecord
		{
			var db = Database.Get( "FDARems" );
			var sql = "SELECT * " +
					" FROM " + db.DelimTable( Manager.GetTableNameFromType( typeof( T ) ) ) +
					" WHERE ";

			bool first = true;
			var ps = new List<Parameter>();

			foreach( var k in paramereters.Keys )
			{
				if( !first )
					sql += " AND ";

				first = false;

				sql += Command.HandleParameter( k, paramereters[k], db, ref ps );
			}

			sql += " " + Command.ParseOrderBy( db, order_by );

			return db.ExecuteQuery<T>( sql, ps.ToArray() );
		}

		protected static T FindFirstBy<T>( Dictionary<string, object> paramereters, IEnumerable<string> order_by = null ) where T : ActiveRecord
		{
			var db = Database.Get( "FDARems" );
			var sql = "SELECT * " +
					" FROM " + db.DelimTable( Manager.GetTableNameFromType( typeof( T ) ) ) +
					" WHERE ";

			bool first = true;
			var ps = new List<Parameter>();

			foreach( var k in paramereters.Keys )
			{
				if( !first )
					sql += " AND ";

				first = false;

				sql += Command.HandleParameter( k, paramereters[k], db, ref ps );
			}

			sql += " " + Command.ParseOrderBy( db, order_by );

			var rows = db.ExecuteQuery<T>( sql, ps.ToArray() );

			if( rows == null || rows.Count <= 0 )
				return null;

			return rows[0];
		}

		protected static T FindOnlyBy<T>( Dictionary<string, object> paramereters, IEnumerable<string> order_by = null ) where T : ActiveRecord
		{
			var db = Database.Get( "FDARems" );
			var sql = "SELECT * " +
					" FROM " + db.DelimTable( Manager.GetTableNameFromType( typeof( T ) ) ) +
					" WHERE ";

			bool first = true;
			var ps = new List<Parameter>();

			foreach( var k in paramereters.Keys )
			{
				if( !first )
					sql += " AND ";

				first = false;

				sql += Command.HandleParameter( k, paramereters[k], db, ref ps );
			}

			sql += " " + Command.ParseOrderBy( db, order_by );

			var rows = db.ExecuteQuery<T>( sql, ps.ToArray() );

			if( rows == null || rows.Count != 1 )
				return null;

			return rows[0];
		}

		protected Table table;
		protected string IDCol;
		public long? ID
		{ get; protected set; }

		protected enum RelationshipType
		{
			Unknown = 0,
			HasA = 0,
			HasMany,
			ManyToMany
		}
		protected class RelationshipInfo
		{
			public RelationshipType Type;
			public string Name;
			public Type ForeignType;
			public string LinkTable;
			public string IDCol;
			public string FKIDCol;

			public bool IsValid()
			{
				if( string.IsNullOrWhiteSpace( Name ) )
					return false;

				if( !Enum.IsDefined( typeof( RelationshipType ), this.Type ) || this.Type == RelationshipType.Unknown )
					return false;

				if( this.ForeignType == null || !Framework.Manager.IsSubclassOf(this.ForeignType, typeof(RowView) ) )
					return false;

				if( string.IsNullOrWhiteSpace(FKIDCol) )
					return false;

				if( this.Type == RelationshipType.ManyToMany )
				{
					if( string.IsNullOrWhiteSpace(this.LinkTable) )
						return false;

					if( string.IsNullOrWhiteSpace(IDCol) )
						return false;
				}

				return true;
			}
		}
		protected Dictionary<string, RelationshipInfo> relationships;

		public ActiveRecord( long? id = null ) : base()
		{
			this.table = Table.Get( this.GetType() );
			this.IDCol = Manager.GetPrimaryKeyColumnFromType(this.GetType());
			this.relationships = new Dictionary<string, RelationshipInfo>();

			DefineRelationships();

			if( id != null )
				Load( id.Value );
		}

		public ActiveRecord(string db, string t, string id_col, long? id = null) : base()
		{
			this.table = Table.Get(db,t);
			this.IDCol = id_col;
			this.relationships = new Dictionary<string, RelationshipInfo>();

			DefineRelationships();

			if (id != null)
				Load(id.Value);
		}

		public ActiveRecord(Table t, string id_col, long? id = null) : base()
		{
			this.table = t;
			this.IDCol = id_col;
			this.relationships = new Dictionary<string, RelationshipInfo>();

			DefineRelationships();

			if (id != null)
				Load(id.Value);
		}

		public ActiveRecord( IDataRecord row ) : base()
		{
			this.table = Table.Get( this.GetType() );
			this.IDCol = Manager.GetPrimaryKeyColumnFromType( this.GetType() );
			this.relationships = new Dictionary<string, RelationshipInfo>();

			DefineRelationships();
			LoadData( row );
		}

		public ActiveRecord(string db, string t, string id_col, IDataRecord row) : base()
		{
			this.table = Table.Get( db, t );
			this.IDCol = id_col;
			this.relationships = new Dictionary<string, RelationshipInfo>();

			DefineRelationships();
			LoadData( row );
		}

		public ActiveRecord(Table t, string id_col, IDataRecord row) : base()
		{
			this.table = t;
			this.IDCol = id_col;
			this.relationships = new Dictionary<string, RelationshipInfo>();

			DefineRelationships();
			LoadData( row );
		}

		protected virtual void DefineRelationships() {}

		protected void DefineRelationShip( RelationshipType type, string name, Type foreignType, string fkidCol = null, string linkTable = null, string idCol = null )
		{
			var ri = new RelationshipInfo {
				Type = type,
				Name = name,
				ForeignType = foreignType,
				LinkTable = linkTable,
				IDCol = idCol,
				FKIDCol = fkidCol
			};

			if( !ri.IsValid() )
				throw new InvalidConstraintException( "The relationship [" + name + "] is not valid." );

			relationships.Add( name, ri );
		}

		public bool Load(long id)
		{
            var rows = table.Select(null, table.DB.Delim(this.IDCol, DelimType.Column) + " = @id", new Framework.Data.Parameter[] { new Framework.Data.Parameter("id", id) });
			if (rows == null || rows.Count != 1)
				return false;

			var row = rows[0];
			if( row[this.IDCol] == DBNull.Value || ((long)row[this.IDCol]) != id )
				return false;

			LoadData( row );

			return true;
		}

		protected override void LoadData( IDataRecord row )
		{
			ID = (long)row[IDCol];
			Manager.LoadColumns( this, row );
		}

		protected virtual Dictionary<string, object> GetData()
		{
			return Manager.GetData( this );
		}

		public void Save()
		{
			Dictionary<string, object> data = GetData();

			if (ID == null || !ID.HasValue)
			{
				ID = table.Insert(data);
			}
			else
			{
				table.Update(
						data,
						table.DB.DelimColumn( this.IDCol) + " = " + table.DB.DelimParameter( "id" ),
                        new [] { new Framework.Data.Parameter("id", ID.Value) }
					);
			}
		}

		public void Delete()
		{
			if (ID == null || !ID.HasValue)
				return;

			this.table.Delete(
					table.DB.DelimColumn( this.IDCol ) + " = " + table.DB.DelimParameter( "id" ),
                    new [] { new Framework.Data.Parameter("id", ID.Value) }
				);
		}

		public int CompareTo( ActiveRecord other )
		{
			if( other == null )
				return 1;

			if( other.table.Name != this.table.Name )
				return this.table.Name.CompareTo( other.table.Name );

			if( this.ID == other.ID )
				return 0;
			if( this.ID == null )
				return -1;
			else if( other.ID == null )
				return 1;

			return this.ID.Value.CompareTo(other.ID.Value);
		}

		protected IList<T> GetRelatedItems<T>( string rel_name ) where T : ActiveRecord
		{
			if( !relationships.ContainsKey(rel_name) )
				throw new ArgumentOutOfRangeException("rel_name", "No relationship defined for type ["+this.GetType().FullName+"] matching name ["+rel_name+"].");

			var ri = relationships[rel_name];

			if( ri.Type != RelationshipType.HasMany && ri.Type != RelationshipType.ManyToMany )
				throw new InvalidOperationException( "Relationship [" + rel_name + "] does not return a list of items. Did you mean GetRelatedItem?" );

			if( ri.ForeignType != typeof( T ) )
				throw new InvalidOperationException( "Generic Type ["+typeof(T).FullName+"] does not match the expected type defined for relationship ["+rel_name+"]." );

			var db = this.table.DB;
			var foreign = Framework.Data.Manager.GetInfoForType(typeof(T));
			var sql = "";

			if( ri.Type == RelationshipType.HasMany )
			{
				sql = "SELECT * " +
						" FROM " + db.DelimTable(foreign.Attr.TableName) +
						" WHERE " + db.DelimColumn(ri.FKIDCol) + " = " + db.DelimParameter("id");
			}
			else // if( ri.Type == RelationshipType.ManyToMany )
			{
				sql = "SELECT " + db.DelimTable( foreign.Attr.TableName ) + ".* " +
						" FROM " + db.DelimTable( ri.LinkTable ) +
							" LEFT JOIN " + db.DelimTable( foreign.Attr.TableName ) +
								" ON " + db.DelimColumn( ri.LinkTable, ri.FKIDCol ) + " = " + db.DelimColumn( foreign.Attr.TableName, foreign.Attr.PrimaryKeyColumn ) +
						" WHERE " + db.DelimColumn( ri.LinkTable, ri.IDCol ) + " = " + db.DelimParameter( "id" );
			}

			return db.ExecuteQuery<T>( sql, new[] { new Parameter( "id", this.ID.Value ) } );
		}

		protected T GetRelatedItem<T>( string rel_name ) where T : ActiveRecord
		{
			if( !relationships.ContainsKey( rel_name ) )
				throw new ArgumentOutOfRangeException( "rel_name", "No relationship defined for type [" + this.GetType().FullName + "] matching name [" + rel_name + "]." );

			var ri = relationships[rel_name];

			if( ri.Type != RelationshipType.HasA )
				throw new InvalidOperationException( "Relationship [" + rel_name + "] does not return a single items. Did you mean GetRelatedItems?" );

			if( ri.ForeignType != typeof( T ) )
				throw new InvalidOperationException( "Generic Type [" + typeof( T ).FullName + "] does not match the expected type defined for relationship [" + rel_name + "]." );

			var db = this.table.DB;
			var foreign = Framework.Data.Manager.GetInfoForType( typeof( T ) );
			var sql = "SELECT * " +
					" FROM " + db.DelimTable( foreign.Attr.TableName ) +
					" WHERE " + db.DelimColumn( ri.FKIDCol ) + " = " + db.DelimParameter( "id" );

			var rows = db.ExecuteQuery<T>( sql, new[] { new Parameter( "id", this.ID.Value ) } );

			if( rows == null || rows.Count <= 0 )
				return default( T );

			return rows[0];
		}

		protected void AddRelatedItem( string rel_name, ActiveRecord foreign_record )
		{
			AddRelatedItem( rel_name, foreign_record.ID.Value );
		}

		protected void AddRelatedItem( string rel_name, long fkid )
		{
			if( !relationships.ContainsKey( rel_name ) )
				throw new ArgumentOutOfRangeException( "rel_name", "No relationship defined for type [" + this.GetType().FullName + "] matching name [" + rel_name + "]." );

			var ri = relationships[rel_name];

			if( ri.Type != RelationshipType.ManyToMany )
				throw new InvalidOperationException( "Relationship [" + rel_name + "] does not return a single items. Did you mean GetRelatedItems?" );

			var db = this.table.DB;
			var sql = "INSERT INTO " + db.DelimTable( ri.LinkTable ) +
					"(" + db.DelimColumn( ri.IDCol ) + ", " + db.DelimColumn( ri.FKIDCol ) + ") VALUES (" + db.DelimParameter( "pkid" ) + ", " + db.DelimParameter( "fkid" ) + ")";

			db.ExecuteNonQuery( sql, new[] { new Parameter( "pkid", this.ID.Value ), new Parameter( "fkid", fkid ) } );
		}

		protected void RemoveRelatedItem( string rel_name, ActiveRecord foreign_record )
		{
			RemoveRelatedItem( rel_name, foreign_record.ID.Value );
		}

		protected void RemoveRelatedItem( string rel_name, long fkid )
		{
			if( !relationships.ContainsKey( rel_name ) )
				throw new ArgumentOutOfRangeException( "rel_name", "No relationship defined for type [" + this.GetType().FullName + "] matching name [" + rel_name + "]." );

			var ri = relationships[rel_name];

			if( ri.Type != RelationshipType.ManyToMany )
				throw new InvalidOperationException( "Relationship [" + rel_name + "] does not return a single items. Did you mean GetRelatedItems?" );

			var db = this.table.DB;
			var sql = "DELETE FROM " + db.DelimTable( ri.LinkTable ) +
					" WHERE " + db.DelimColumn( ri.IDCol ) + " = " + db.DelimParameter( "pkid" ) +
					" AND " + db.DelimColumn( ri.FKIDCol ) + " = " + db.DelimParameter( "fkid" );

			db.ExecuteNonQuery( sql, new[] { new Parameter( "pkid", this.ID.Value ), new Parameter( "fkid", fkid ) } );
		}

		protected void ClearRelatedItems( string rel_name )
		{
			if( !relationships.ContainsKey( rel_name ) )
				throw new ArgumentOutOfRangeException( "rel_name", "No relationship defined for type [" + this.GetType().FullName + "] matching name [" + rel_name + "]." );

			var ri = relationships[rel_name];

			if( ri.Type != RelationshipType.ManyToMany )
				throw new InvalidOperationException( "Relationship [" + rel_name + "] does not return a single items. Did you mean GetRelatedItems?" );

			var db = this.table.DB;
			var sql = "DELETE FROM " + db.DelimTable( ri.LinkTable ) +
					" WHERE " + db.DelimColumn( ri.IDCol ) + " = " + db.DelimParameter( "pkid" );

			db.ExecuteNonQuery( sql, new[] { new Parameter( "pkid", this.ID.Value ) } );
		}

		protected bool HasRelatedItem<T>( string rel_name, ActiveRecord foreign_record ) where T : ActiveRecord
		{
			if( foreign_record == null || foreign_record.ID == null )
				return false;

			return HasRelatedItem<T>( rel_name, foreign_record.ID.Value );
		}

		protected bool HasRelatedItem<T>( string rel_name, long fkid ) where T : ActiveRecord
		{
			var items = GetRelatedItems<T>(rel_name);

			foreach( var item in items )
			{
				if( item.ID.Value == fkid )
					return true;
			}

			return false;
		}

		public class DataHelper
		{
			public static T Get<T>( IDataRecord row, string name )
			{
                try
                {
				    if( row[name] == DBNull.Value )
					    return default( T );

				    return (T)row[name];
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
			}

			public static T Get<T>( IDataRecord row, string name, T def )
			{
				if( row[name] == DBNull.Value )
					return def;

				return (T)row[name];
			}
		}

		public bool Equals( ActiveRecord other )
		{
			if( other == null || other.ID == null || this.ID == null )
				return false;

			if( this.table.DB == other.table.DB && this.table.Name == other.table.Name && this.ID == other.ID )
				return true;

			return false;
		}
	}
}
