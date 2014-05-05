using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Framework.API;
using RemsLogic.Model;
using RemsLogic.Services;
using StructureMap;

namespace Lib.API.Dev.DSQ
{
	public class Link : Base
	{
        [SecurityRole( "view_admin" )]
        [Method( "Dev/DSQ/Link/Edit" )]
        public static ReturnObject Edit( HttpContext context, long drug_id, long question_id, long eoc_id, string label, string value, DateTime date, string help_text = "", long? id = null, string required = "No" )
        {
            Data.DSQ.Link item;

            if( help_text != null && help_text.Length >= 450 )
            {
                return new ReturnObject {
                    Error = true,
                    Message = "The help text must be less than 450 characters."
                };
            }

            var link = new DsqLink
            {
                Id = id ?? 0,
                DrugId = drug_id,
                QuestionId = question_id,
                EocId = eoc_id,
                Label = label,
                Value = value,
                Date = date,
                HelpText = help_text,
                IsRequired = required.ToLowerInvariant() == "yes"
            };

            // a nice and testable method call
            ObjectFactory.GetInstance<IDsqService>().SaveLink(link);

            return new ReturnObject()
            {
                Result = link,
                Redirect = new ReturnRedirectObject()
                {
                    Hash = "admin/dsq/edit?id=" + drug_id + "&section-id=" + link.Question.SectionId
                },
                Growl = new ReturnGrowlObject()
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject()
                    {
                        text = "You have successfully saved this link.", title = "Link Saved"
                    }
                }
            };
        }

		[SecurityRole( "view_admin" )]
		[Method( "Dev/DSQ/Link/Delete" )]
		public static ReturnObject Delete( HttpContext context, long id )
		{
			if( id <= 0 || id == 1 ) // id == 1 is to protect the General info section which is special
				return new ReturnObject() { Error = true, Message = "Invalid Section." };

			var item = new Lib.Data.DSQ.Link( id );
			var question_id = item.QuestionID;
			item.Delete();

			return new ReturnObject() {
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "You have successfully deleted a link.",
						title = "Link Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#form-q-"+question_id+" tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}

		[SecurityRole( "view_admin" )]
		[Method( "Dev/DSQ/Link/Upload" )]
		public static ReturnObject Upload( HttpContext context, long drug_id )
		{
			var file = context.Request.Files["file"];
			string path = context.Server.MapPath( "~/upload/drugs/" + drug_id.ToString() + "/" );

			if( !Directory.Exists( path ) )
				Directory.CreateDirectory( path );

			string ext = Path.GetExtension( file.FileName );

			string fn = Guid.NewGuid().ToString() + ext;

			while( File.Exists( Path.Combine( path, fn ) ) )
				fn = Guid.NewGuid().ToString() + ext;

			fn = Path.Combine( path, fn );

			file.SaveAs( fn );

			var ret = new ReturnObject() {
				Result = Admin.Files.MapPathReverse( fn ),
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "You have successfully uploaded a file.",
						title = "File Uploaded"
					}
				}
			};

			return ret;
		}
	}
}
