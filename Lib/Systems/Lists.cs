using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Systems
{
	public static class Lists
	{
		public static bool IsValidListType(string type)
		{
			return Data.UserList.DataTypes.Contains( type );
		}

		public static IList<Lib.Data.UserList> GetMyLists()
		{
			var profile = Security.GetCurrentProfile();

			var lists = Lib.Data.UserList.FindByUserProfile( profile );

			return lists;
		}

		public static IList<Lib.Data.UserList> GetMyUserLists()
		{
			var profile = Security.GetCurrentProfile();

			var lists = Lib.Data.UserList.FindByUserProfile( profile, null, false );
			return lists;
		}

		public static IList<Lib.Data.UserList> GetMyDrugLists()
		{
			var profile = Security.GetCurrentProfile();

			var lists = Lib.Data.UserList.FindByUserProfile( profile, "drug" );

			return lists;
		}

		public static Lib.Data.UserList GetMyDrugList()
		{
			var profile = Security.GetCurrentProfile();

			var lists = Lib.Data.UserList.FindByUserProfile( profile, "drug", true );

			if( lists.Count > 0 )
			{
				for( int i = 0; i < lists.Count; i++ )
					if( lists[i].Name == "My Drugs" )
						return lists[i];
			}

			var ret = new Data.UserList();

			ret.DataType = "drug";
			ret.DateCreated = DateTime.Now;
			ret.DateModified = DateTime.Now;
			ret.Name = "My Drugs";
			ret.System = true;
			ret.UserProfileID = profile.ID;
			ret.Save();

			return ret;
		}

		public static IList<Lib.Data.Drug> GetMyDrugs()
		{
			return GetMyDrugList().GetItems<Lib.Data.Drug>();
		}

		public static Lib.Data.UserList GetUsersDrugList( long profile_id )
		{
			var profile = new Data.UserProfile( profile_id );

			var lists = Lib.Data.UserList.FindByUserProfile( profile, "drug", true );

			if( lists.Count > 0 )
			{
				for( int i = 0; i < lists.Count; i++ )
					if( lists[i].Name == "My Drugs" )
						return lists[i];
			}

			var ret = new Data.UserList();

			ret.DataType = "drug";
			ret.DateCreated = DateTime.Now;
			ret.DateModified = DateTime.Now;
			ret.Name = "My Drugs";
			ret.System = true;
			ret.UserProfileID = profile.ID;
			ret.Save();

			return ret;
		}

		public static IList<Lib.Data.Drug> GetUsersDrugs( long profile_id )
		{
			return GetUsersDrugList( profile_id ).GetItems<Lib.Data.Drug>();
		}

		public static Lib.Data.UserList GetMyFormsAndDocumentsList()
		{
			var profile = Security.GetCurrentProfile();

			var lists = Lib.Data.UserList.FindByUserProfile( profile, "drug-link", true );

			if( lists.Count > 0 )
			{
				for( int i = 0; i < lists.Count; i++ )
					if( lists[i].Name == "Forms and Documents" )
						return lists[i];
			}

			var ret = new Data.UserList();

			ret.DataType = "drug-link";
			ret.DateCreated = DateTime.Now;
			ret.DateModified = DateTime.Now;
			ret.Name = "Forms and Documents";
			ret.System = true;
			ret.UserProfileID = profile.ID;
			ret.Save();

			return ret;
		}

		public static Lib.Data.UserList GetSystemList( string name, string data_type )
		{
			var rows = Lib.Data.UserList.FindByUserProfile( null, data_type, true, name );

			if( rows == null || rows.Count <= 0 )
			{
				var ret = new Lib.Data.UserList();
				ret.Name = name;
				ret.UserProfileID = null;
				ret.DataType = data_type;
				ret.DateCreated = DateTime.Now;
				ret.DateModified = DateTime.Now;
				ret.System = true;
				ret.Save();

				return ret;
			}

			return rows[0];
		}

		public static IList<Lib.Data.Drug> GetDrugsWithPharmacyRequirements()
		{
			var list = GetSystemList( "Drugs with Pharmacy Requirements", "drug" );

			return list.GetItems<Lib.Data.Drug>();
		}

		public static IList<Lib.Data.Drug> GetDrugsWithEducationTraining()
		{
			var list = GetSystemList( "Drugs with Education/Training", "drug" );

			return list.GetItems<Lib.Data.Drug>();
		}

		public static void UpdateDrugLists( Lib.Data.Drug item )
		{
			item.DetermineEOC();

			var ul_pr = Lib.Systems.Lists.GetSystemList( "Drugs with Pharmacy Requirements", "drug" );

			if( item.HasEoc( "pharmacy-requirements" ) )
				ul_pr.AddItem( item.ID.Value );
			else
				ul_pr.RemoveItem( item.ID.Value );

			var ul_et = Lib.Systems.Lists.GetSystemList( "Drugs with Education/Training", "drug" );

			if( item.HasEoc( "education-training" ) )
				ul_et.AddItem( item.ID.Value );
			else
				ul_et.RemoveItem( item.ID.Value );
		}

		public static void UpdateDrugLists()
		{
			var drugs = Lib.Data.Drug.FindAll();

			foreach( var d in drugs )
				UpdateDrugLists( d );
		}
	}
}
