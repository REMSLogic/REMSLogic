using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Admin
{
	public class Files : Base
	{
		[SecurityRole( "view_admin" )]
		[Method( "Admin/Files/Upload" )]
		public static ReturnObject Upload(HttpContext context, string parent_type, long parent_id)
		{
			var file = context.Request.Files["file"];
			string path = context.Server.MapPath("~/upload/" + parent_type + "/" + parent_id.ToString() + "/");

			if( !Directory.Exists(path) )
				Directory.CreateDirectory(path);

			string ext = Path.GetExtension(file.FileName);

			string fn = Guid.NewGuid().ToString()+ext;

			while( File.Exists(Path.Combine(path,fn)) )
				fn = Guid.NewGuid().ToString() + ext;

			fn = Path.Combine(path,fn);

			file.SaveAs( fn );

			var item = new Data.File();
			item.ParentType = parent_type;
			item.ParentID = parent_id;
			item.Path = fn;
			item.Name = file.FileName;
			item.ContentType = file.ContentType;
			item.Save();

			var ret = new ReturnObject() {
				Result = MapPathReverse(fn),
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

		public static string MapPathReverse(string fullServerPath)
		{
			return "http://"+HttpContext.Current.Request.Url.Host+"/" + fullServerPath.Replace( HttpContext.Current.Request.PhysicalApplicationPath, String.Empty ).Replace("\\","/");
		}
	}
}
