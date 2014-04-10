using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.App.List
{
	public class FormsAndDocuments : Base
	{
		[Method( "App/List/FormsAndDocuments/AddItem" )]
		public static ReturnObject AddItem( HttpContext context, long id )
		{
			var item = new Lib.Data.DSQ.Link( id );

			if( item.ID != id )
				return new ReturnObject() {
					Error = true,
					Message = "Invalid item."
				};

			var list = Lib.Systems.Lists.GetMyFormsAndDocumentsList();

			list.AddItem( id );

			return new ReturnObject() {
				Error = false,
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "You have successfully added \""+item.Label+"\" to your list.",
						title = "Item Added"
					}
				}
			};
		}

		[Method( "App/List/FormsAndDocuments/RemoveItem" )]
		public static ReturnObject RemoveItem( HttpContext context, long id )
		{
			var item = new Lib.Data.DSQ.Link( id );

			if( item.ID != id )
				return new ReturnObject() {
					Error = true,
					Message = "Invalid item."
				};

			var list = Lib.Systems.Lists.GetMyFormsAndDocumentsList();

			list.RemoveItem( id );

			return new ReturnObject() {
				Error = false,
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "You have successfully removed \"" + item.Label + "\" from your list.",
						title = "Item Removes"
					}
				}
			};
		}
	}
}
