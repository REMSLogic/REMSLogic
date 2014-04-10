using System;
using System.Collections.Generic;
using System.Security;
using System.Web;
using Framework.API;
using Framework.Data;

namespace Lib.API.Dev
{
	public class ReportFilters : Base
	{
		[SecurityRole( "view_dev" )]
		[Method( "Dev/ReportFilters/Edit" )]
		public static ReturnObject Edit( HttpContext context, long report_id, string display_name, string parameter_name, int type, string placeholder_text = null, string help_text = null, string parameters = null, long? id = null )
		{
			Data.ReportFilter item;
			if( id == null || id <= 0 )
				item = new Data.ReportFilter();
			else
				item = new Data.ReportFilter( id );

			if( !Enum.IsDefined(typeof(Data.ReportFilter.FilterTypes), type) )
				return new ReturnObject() { Error = true, Message = "Invalid Filter Type." };

			item.ReportID = report_id;
			item.DisplayName = display_name;
			item.PlaceholderText = placeholder_text;
			item.HelpText = help_text;
			item.ParameterName = parameter_name;
			item.Type = (Data.ReportFilter.FilterTypes)type;
			item.Parameters = parameters;
			item.Save();

			return new ReturnObject {
				Result = item,
				Growl = new ReturnGrowlObject {
					Type = "default",
					Vars = new ReturnGrowlVarsObject {
						text = "Filter '" + item.DisplayName + "' has been saved.",
						title = "Filter saved"
					}
				},
				Actions = new List<ReturnActionObject> {
					new ReturnActionObject {
						Type = "back"
					}
				}
			};
		}

		[SecurityRole( "view_dev" )]
		[Method( "Dev/ReportFilters/Delete" )]
		public static ReturnObject Delete( HttpContext context, long id )
		{
			if( id <= 0 )
				return new ReturnObject() { Error = true, Message = "Invalid Filter." };

			var item = new Lib.Data.ReportFilter( id );
			item.Delete();

			return new ReturnObject() {
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "You have successfully deleted this filter.",
						title = "Filter deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#filters-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
