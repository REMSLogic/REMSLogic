using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.API;

namespace Lib.API.Dev
{
	public class Eoc : Base
	{
		[SecurityRole( "view_dev" )]
		[Method( "Dev/Eoc/Edit" )]
		public static ReturnObject Edit( HttpContext context, string name, string display_name, List<long> user_types, long? id = null )
		{
			Data.Eoc item;
			if( id == null || id <= 0 )
				item = new Data.Eoc();
			else
				item = new Data.Eoc( id );

			item.Name = name;
			item.DisplayName = display_name;
			item.Save();

			item.ClearUserTypes();

			foreach( var ut_id in user_types )
				item.AddUserType( ut_id );

			return new ReturnObject {
				Result = item,
				Growl = new ReturnGrowlObject {
					Type = "default",
					Vars = new ReturnGrowlVarsObject {
						text = "Eoc '" + item.DisplayName + "' has been saved.",
						title = "EOC saved"
					}
				},
				Redirect = new ReturnRedirectObject {
					Hash = "dev/eocs/edit?id=" + item.ID.Value.ToString()
				}
			};
		}

		[SecurityRole( "view_dev" )]
		[Method( "Dev/Eoc/Delete" )]
		public static ReturnObject Delete( HttpContext context, long id )
		{
			if( id <= 0 )
				return new ReturnObject() { Error = true, Message = "Invalid EOC." };

			var item = new Lib.Data.Eoc( id );
			item.Delete();

			return new ReturnObject() {
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "You have successfully deleted this eoc.",
						title = "Report deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#eocs-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
