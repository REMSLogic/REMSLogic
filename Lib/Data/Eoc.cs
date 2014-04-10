using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "Eocs", PrimaryKeyColumn = "ID" )]
	public class Eoc : ActiveRecord
	{
		public static IList<Eoc> FindAll()
		{
			return FindAll<Eoc>( new[] {
				"+DisplayName"
			} );
		}

		public static Eoc FindByName( string name )
		{
			return FindFirstBy<Eoc>( new Dictionary<string, object> {
				{ "Name", name }
			} );
		}

		[Column]
		public string Name;
		[Column]
		public string DisplayName;

		public Eoc(long? id = null) : base( id )
		{ }

		public Eoc(IDataRecord row) : base( row )
		{ }

		protected override void DefineRelationships()
		{
			DefineRelationShip( RelationshipType.ManyToMany, "eoc-to-user-types", typeof( UserType ), "UserTypeID", "EocUserTypes", "EocID" );
		}

		public IList<UserType> GetUserTypes()
		{
			return GetRelatedItems<UserType>( "eoc-to-user-types" );
		}

		public void AddUserType( UserType ut )
		{
			AddRelatedItem( "eoc-to-user-types", ut );
		}

		public void AddUserType( long ut_id )
		{
			AddRelatedItem( "eoc-to-user-types", ut_id );
		}

		public void RemoveUserType( UserType ut )
		{
			RemoveRelatedItem( "eoc-to-user-types", ut );
		}

		public void RemoveUserType( long ut_id )
		{
			RemoveRelatedItem( "eoc-to-user-types", ut_id );
		}

		public void ClearUserTypes()
		{
			ClearRelatedItems( "eoc-to-user-types" );
		}

		public bool HasUserType( UserType ut )
		{
			return HasRelatedItem<UserType>( "eoc-to-user-types", ut );
		}

		public bool HasUserType( long ut_id )
		{
			return HasRelatedItem<UserType>( "eoc-to-user-types", ut_id );
		}
	}
}
