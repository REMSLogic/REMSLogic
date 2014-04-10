using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;

namespace Framework.Data
{
	public class Manager : Framework.LoadTimeInitializer
	{
		internal struct DataFieldInfo
		{
			public string Name;
			public FieldInfo FieldInfo;
			public PropertyInfo PropertyInfo;
		}
		internal struct RowInfo
		{
			public Type Type;
			public TableAttribute Attr;
			public Func<long?, ActiveRecord> LongCreator;
			public Func<System.Data.IDataRecord, RowView> DataRecordCreator;
			public Action<RowView, System.Data.IDataRecord> LoadColumnsFunction;
			public Func<ActiveRecord, Dictionary<string, object>> GetDataFunction;
		}
		internal struct DatabaseInfo
		{
			public Type Type;
			public string Name;
			public Func<string, Database> DatabaseCreator;
		}

		private static Dictionary<string, DatabaseInfo> dbCreators = null;
		private static Dictionary<Type,RowInfo> rowCreators = null;
		private static Dictionary<string, Type> tableToTypeLookup = null;

		private static bool Initialized = false;
		private static object InitLock = new object();

		public static void Init()
		{
			if( !Initialized )
			{
				lock( InitLock )
				{
					if( !Initialized )
					{
						dbCreators = new Dictionary<string, DatabaseInfo>();
						rowCreators = new Dictionary<Type, RowInfo>();
						tableToTypeLookup = new Dictionary<string, Type>();

						InitRowViews();
						InitActiveRecords();
						InitDBs();

						Initialized = true;
					}
				}
			}
		}

		private static void InitRowViews()
		{
			var rowTypes = Framework.Manager.FindSubclassesOf( typeof( RowView ) );

			foreach( var t in rowTypes )
			{
				if( t == typeof( ConcreteActiveRecord ) || t == typeof( ActiveRecord ) )
					continue;

				var ci = new RowInfo() { Type = t };

				ConstructorInfo dataCtor = t.GetConstructor( new Type[] { typeof( System.Data.IDataRecord ) } );

				if( dataCtor == null )
					throw new Exception( "Type [" + t.FullName + "] inherits from Framework.Data.RowView but does not offer a constructor accepting a System.Data.IDataRecord." );

				var p1 = Expression.Parameter( typeof( System.Data.IDataRecord ), "p2" );
				var expr1 = Expression.Lambda<Func<System.Data.IDataRecord, RowView>>(
							   Expression.TypeAs(
								   Expression.New( dataCtor, p1 ),
								   typeof( RowView ) ),
							   p1 );

				ci.DataRecordCreator = expr1.Compile();

				var fields = t.GetFields( BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly );
				var props = t.GetProperties( BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly );
				var fieldsToUse = new List<DataFieldInfo>();

				foreach( var field in fields )
				{
					var attrs = field.GetCustomAttributes( typeof( ColumnAttribute ), true );

					foreach( var attr in attrs )
					{
						var ca = attr as ColumnAttribute;

						if( ca == null )
							continue;

						fieldsToUse.Add( new DataFieldInfo() {
							Name = (string.IsNullOrEmpty( ca.Name )) ? field.Name : ca.Name,
							FieldInfo = field
						} );

						break;
					}
				}

				foreach( var prop in props )
				{
					var attrs = prop.GetCustomAttributes( typeof( ColumnAttribute ), true );

					foreach( var attr in attrs )
					{
						var ca = attr as ColumnAttribute;

						if( ca == null )
							continue;

						fieldsToUse.Add( new DataFieldInfo() {
							Name = (string.IsNullOrEmpty( ca.Name )) ? prop.Name : ca.Name,
							PropertyInfo = prop
						} );

						break;
					}
				}

				if( fieldsToUse.Count > 0 )
				{
					var dh_t = typeof( ActiveRecord.DataHelper );
					var dh_get_mi = dh_t.GetMethod( "Get", new Type[] { typeof( System.Data.IDataRecord ), typeof( string ) } );
					var inst_p = Expression.Parameter( typeof( RowView ), "inst" );
					var row_p = Expression.Parameter( typeof( System.Data.IDataRecord ), "row" );

					var load_ps = new List<ParameterExpression>();
					load_ps.Add( inst_p );
					load_ps.Add( row_p );

					var load_stmts = new List<Expression>();

					foreach( var field in fieldsToUse )
					{
						if( field.FieldInfo != null )
						{
							load_stmts.Add(
								Expression.Assign(
									Expression.Field( Expression.TypeAs( inst_p, t ), field.FieldInfo ),
									Expression.Call(
										dh_get_mi.MakeGenericMethod( new Type[] { field.FieldInfo.FieldType } ),
										row_p,
										Expression.Constant( field.Name ) )
								)
							);
						}
						else
						{
							load_stmts.Add(
								Expression.Call(
									inst_p,
									field.PropertyInfo.GetSetMethod(),
									Expression.Call(
										dh_get_mi.MakeGenericMethod( new Type[] { field.PropertyInfo.PropertyType } ),
										row_p,
										Expression.Constant( field.Name )
									)
								)
							);
						}
					}

					var load_expr = Expression.Lambda<Action<RowView, System.Data.IDataRecord>>( Expression.Block( load_stmts ), load_ps );

					ci.LoadColumnsFunction = load_expr.Compile();
				}
				rowCreators.Add( t, ci );
			}
		}

		private static void InitActiveRecords()
		{
			var rowTypes = Framework.Manager.FindSubclassesOf( typeof( ActiveRecord ) );

			foreach( var t in rowTypes )
			{
				if( t == typeof( ConcreteActiveRecord ) )
					continue;
				var ci = rowCreators[t];

				var tableattrs = t.GetCustomAttributes( typeof( TableAttribute ), true );
				if( tableattrs != null && tableattrs.Length > 0 )
				{
					var tableAttr = (TableAttribute)tableattrs[0];

					if( string.IsNullOrWhiteSpace( tableAttr.TableName ) || string.IsNullOrWhiteSpace( tableAttr.DatabaseName ) || string.IsNullOrWhiteSpace( tableAttr.PrimaryKeyColumn ) )
						throw new Exception( "An Attribute of Type TableAttribute must have a database, table, and primary key column name specified for Type [" + t.FullName + "]" );

					ci.Attr = tableAttr;
				}

				ConstructorInfo longCtor = t.GetConstructor( new Type[] { typeof( long? ) } );

				if( longCtor == null )
					throw new Exception( "Type [" + t.FullName + "] inherits from Framework.Data.ActiveRecord but does not offer a constructor accepting a Nullable<long>." );

				var p1 = Expression.Parameter( typeof( long? ), "p1" );
				var expr1 = Expression.Lambda<Func<long?, ActiveRecord>>(
							   Expression.TypeAs(
								   Expression.New( longCtor, p1 ),
								   typeof( ActiveRecord ) ),
							   p1 );

				ci.LongCreator = expr1.Compile();

				var fields = t.GetFields( BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly );
				var props = t.GetProperties( BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly );
				var fieldsToUse = new List<DataFieldInfo>();

				foreach( var field in fields )
				{
					var attrs = field.GetCustomAttributes( typeof( ColumnAttribute ), true );

					foreach( var attr in attrs )
					{
						var ca = attr as ColumnAttribute;

						if( ca == null )
							continue;

						fieldsToUse.Add( new DataFieldInfo() {
							Name = (string.IsNullOrEmpty( ca.Name )) ? field.Name : ca.Name,
							FieldInfo = field
						} );

						break;
					}
				}

				foreach( var prop in props )
				{
					var attrs = prop.GetCustomAttributes( typeof( ColumnAttribute ), true );

					foreach( var attr in attrs )
					{
						var ca = attr as ColumnAttribute;

						if( ca == null )
							continue;

						fieldsToUse.Add( new DataFieldInfo() {
							Name = (string.IsNullOrEmpty( ca.Name )) ? prop.Name : ca.Name,
							PropertyInfo = prop
						} );

						break;
					}
				}

				if( fieldsToUse.Count > 0 )
				{
					var inst_p = Expression.Parameter( typeof( ActiveRecord ), "inst" );
					var ret_p = Expression.Parameter( typeof( Dictionary<string, object> ), "ret" );
					var ret_init_mi = typeof( Dictionary<string, object> ).GetConstructor( new Type[] { } );
					var ret_add_mi = typeof( Dictionary<string, object> ).GetMethod( "Add", BindingFlags.Public | BindingFlags.Instance );

					var save_ps_outer = new List<ParameterExpression>();
					save_ps_outer.Add( inst_p );

					var save_ps = new List<ParameterExpression>();
					save_ps.Add( ret_p );

					var save_stmts = new List<Expression>();
					save_stmts.Add( Expression.Assign( ret_p, Expression.New( ret_init_mi ) ) );

					foreach( var field in fieldsToUse )
					{
						if( field.FieldInfo != null )
						{
							save_stmts.Add(
								Expression.Call(
									ret_p,
									ret_add_mi,
									Expression.Constant( field.Name ),
									Expression.TypeAs(
										Expression.Field( Expression.TypeAs( inst_p, t ), field.FieldInfo ),
										typeof( object )
									)
								)
							);
						}
						else
						{
							save_stmts.Add(
								Expression.Call(
									ret_p,
									ret_add_mi,
									Expression.Constant( field.Name ),
									Expression.TypeAs(
										Expression.Call( Expression.TypeAs( inst_p, t ), field.PropertyInfo.GetGetMethod() ),
										typeof( object )
									)
								)
							);
						}
					}

					save_stmts.Add( ret_p );

					var save_expr = Expression.Lambda<Func<ActiveRecord, Dictionary<string, object>>>( Expression.Block( save_ps, save_stmts ), save_ps_outer );

					ci.GetDataFunction = save_expr.Compile();
				}
				rowCreators[t] = ci;

				if( ci.Attr != null && !string.IsNullOrWhiteSpace( ci.Attr.TableName ) )
				{
					tableToTypeLookup.Add( ci.Attr.TableName, t );
				}
			}
		}

		private static void InitDBs()
		{
			var dbTypes = Framework.Manager.FindSubclassesOf( typeof( Database ) );

			foreach( var t in dbTypes )
			{
				if( t == typeof( Database ) || t.Name != "Database" )
					continue;
				var ci = new DatabaseInfo() { Type = t };

				ci.Name = t.Namespace.Replace( "Framework.Data.", "" );

				ConstructorInfo ctor = t.GetConstructor( new Type[] { typeof( string ) } );

				if( ctor == null )
					throw new Exception( "Type [" + t.FullName + "] inherits from Framework.Data.Database but does not offer a constructor accepting a string." );

				var p1 = Expression.Parameter( typeof( string ), "p1" );
				var expr1 = Expression.Lambda<Func<string, Database>>(
							   Expression.TypeAs(
								   Expression.New( ctor, p1 ),
								   typeof( Database ) ),
							   p1 );

				ci.DatabaseCreator = expr1.Compile();

				dbCreators.Add( ci.Name, ci );
			}
		}

		public static ActiveRecord Create(Type t, long id)
		{
            if (!rowCreators.ContainsKey(t))
				throw new Exception("No creator has been added for Type ["+t.FullName+"]");

			var rc = rowCreators[t];

			if( rc.LongCreator == null )
				throw new Exception( "No {long} creator has been added for Type [" + t.FullName + "]" );

            return rowCreators[t].LongCreator(new long?(id));
		}

		public static RowView Create( Type t, System.Data.IDataRecord r )
		{
            if (!rowCreators.ContainsKey(t))
				throw new Exception("No creator has been added for Type [" + t.FullName + "]");

            return rowCreators[t].DataRecordCreator(r);
		}

        public static Database Create(string name, string cs)
        {
            if (!dbCreators.ContainsKey(name))
                throw new Exception("No creator has been added for Type [" + name + "]");

            return dbCreators[name].DatabaseCreator(cs);
        }

		public static void LoadColumns(RowView inst, System.Data.IDataRecord r)
		{
			var t = inst.GetType();
			if (!rowCreators.ContainsKey(t))
				throw new Exception("No creator has been added for Type [" + t.FullName + "]");

			rowCreators[t].LoadColumnsFunction(inst, r);
		}

		public static Dictionary<string, object> GetData(ActiveRecord inst)
		{
			var t = inst.GetType();
			if (!rowCreators.ContainsKey(t))
				throw new Exception("No creator has been added for Type [" + t.FullName + "]");

			var rc = rowCreators[t];

			if( rc.GetDataFunction == null )
				throw new Exception( "No {GetData} Delegate has been added for Type [" + t.FullName + "]" );

			return rowCreators[t].GetDataFunction(inst);
		}

		#region Type <-> Table Lookups

		public static Type GetTypeFromTableName(string tbl_name)
		{
			if( string.IsNullOrWhiteSpace(tbl_name) )
				throw new ArgumentNullException("tbl_name");

			string tn = tbl_name.ToLower();

			if( !tableToTypeLookup.ContainsKey( tn ) )
				throw new Exception("No table has been added by the name of ["+tbl_name+"]");

			return tableToTypeLookup[tn];
		}

		public static string GetTableNameFromType(Type type)
		{
			return GetInfoForType( type ).Attr.TableName;
		}

		public static string GetDatabaseNameFromType( Type type )
		{
			return GetInfoForType( type ).Attr.DatabaseName;
		}

		public static string GetPrimaryKeyColumnFromType( Type type )
		{
			return GetInfoForType(type).Attr.PrimaryKeyColumn;
		}

		internal static RowInfo GetInfoForType( Type type )
		{
			if( type == null )
				throw new ArgumentNullException( "type" );

			if( !rowCreators.ContainsKey( type ) )
				throw new Exception( "No table has been registered to type [" + type.FullName + "]" );

			return rowCreators[type];
		}

		#endregion
	}
}
