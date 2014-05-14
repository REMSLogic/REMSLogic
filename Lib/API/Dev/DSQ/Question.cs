using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Dev.DSQ
{
	public class Question : Base
	{
		[SecurityRole("view_dev")]
		[Method("Dev/DSQ/Question/Edit")]
		public static ReturnObject Edit(HttpContext context, long section_id, string text, string field_type, int order, string viewtext = null, string subtext = null, string help_text = null, string dev_name = "", int required = 0, long? parent_id = null, string parent_checks = null, long? id = null, string eoc = null, string answers = null, string hide_answers = null, string show_children_answers = null, string hide_roles = null, string show_roles = null)
		{
			var item = new Lib.Data.DSQ.Question(id);
			item.SectionID = section_id;
			item.ParentID = parent_id;
			item.ParentChecks = parent_checks;
			item.Text = text;
			item.ViewText = viewtext;
			item.SubText = subtext;
			item.HelpText = help_text;
			item.DevName = dev_name;
			item.Required = (required == 1);
			item.FieldType = field_type;
			item.Order = order;
			item.Answers = (field_type == "EOC" ? eoc : answers);
			item.HideForAnswers = hide_answers;
			item.ShowChildrenForAnswers = show_children_answers;
			item.HideForRoles = hide_roles;
			item.ShowForRoles = show_roles;
			item.Save();

			return new ReturnObject() { Result = item, Redirect = new ReturnRedirectObject() { Hash = "dev/dsq/questions/list" }, Growl = new ReturnGrowlObject() { Type = "default", Vars = new ReturnGrowlVarsObject() { text = "You have successfully saved this question.", title = "Question Saved" } } };
		}

        [Method("Dev/DSQ/Question/Update")]
        public static ReturnObject Update(HttpContext context, long id, string text)
        {
            var item = new Data.DSQ.Question(id);
    
            item.Text = text;
            item.Save();

            SetupResponseForJson(context);

            context.Response.Write(String.Format("{{\"id\":{0},\"text\":\"{1}\"}}", id, text));
            context.Response.End();
            return null;

            //return new ReturnObject() { Result = item, Redirect = new ReturnRedirectObject() { Hash = "dev/dsq/questions/list" }, Growl = new ReturnGrowlObject() { Type = "default", Vars = new ReturnGrowlVarsObject() { text = "You have successfully saved this question.", title = "Question Saved" } } };
        }

		[SecurityRole("view_dev")]
		[Method("Dev/DSQ/Question/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Question." };

			var item = new Lib.Data.DSQ.Question(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a question.",
						title = "Question Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#questions-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}

        #region Utility Methods
        private static void SetupResponseForJson(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "application/json";

            context.Response.ClearHeaders();
            context.Response.AppendHeader("Cache-Control", "no-cache"); //HTTP 1.1
            context.Response.AppendHeader("Cache-Control", "private"); // HTTP 1.1
            context.Response.AppendHeader("Cache-Control", "no-store"); // HTTP 1.1
            context.Response.AppendHeader("Cache-Control", "must-revalidate"); // HTTP 1.1
            context.Response.AppendHeader("Cache-Control", "max-stale=0"); // HTTP 1.1 
            context.Response.AppendHeader("Cache-Control", "post-check=0"); // HTTP 1.1 
            context.Response.AppendHeader("Cache-Control", "pre-check=0"); // HTTP 1.1 
            context.Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.1 
            context.Response.AppendHeader("Keep-Alive", "timeout=3, max=993"); // HTTP 1.1 
            context.Response.AppendHeader("Expires", "Mon, 26 Jul 1997 05:00:00 GMT"); // HTTP 1.1
        }
        #endregion
    }
}
