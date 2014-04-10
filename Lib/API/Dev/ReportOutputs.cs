using System;
using System.Collections.Generic;
using System.Security;
using System.Web;
using Framework.API;
using Framework.Data;

namespace Lib.API.Dev
{
	public class ReportOutputs : Base
	{
		[SecurityRole( "view_dev" )]
		[Method( "Dev/ReportOutputs/Edit" )]
		public static ReturnObject Edit( HttpContext context, long report_id, string name, string sql_text, string sql_type, int type, string parameters = null, long? id = null )
		{
			Data.ReportOutput item;
			if( id == null || id <= 0 )
				item = new Data.ReportOutput();
			else
				item = new Data.ReportOutput( id );

			if( !Enum.IsDefined( typeof( Data.ReportOutput.OutputTypes ), type ) )
				return new ReturnObject() { Error = true, Message = "Invalid Output Type." };

			item.ReportID = report_id;
			item.Name = name;
			item.SqlText = sql_text;
			item.SqlType = sql_type;
			item.Type = (Data.ReportOutput.OutputTypes)type;
			item.Parameters = parameters;
			item.Save();

			return new ReturnObject {
				Result = item,
				Growl = new ReturnGrowlObject {
					Type = "default",
					Vars = new ReturnGrowlVarsObject {
						text = "Output '" + item.Name + "' has been saved.",
						title = "Output saved"
					}
				},
				Actions = new List<ReturnActionObject> {
					new ReturnActionObject {
						Type = "back"
					}
				}
			};
		}

		[SecurityRole("view_dev")]
		[Method( "Dev/ReportOutputs/Delete" )]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if( id <= 0 )
				return new ReturnObject() { Error = true, Message = "Invalid Output." };

			var item = new Lib.Data.ReportOutput( id );
			item.Delete();

			return new ReturnObject() {
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "You have successfully deleted this output.",
						title = "Output deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#outputs-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
