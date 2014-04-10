using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.App
{
	public class Lists : Base
	{
		[Method("App/Lists/AddList")]
		public ReturnObject AddList(string name, string data_type)
		{
			var profile = Lib.Systems.Security.GetCurrentProfile();

			if( !Lib.Data.UserList.DataTypes.Contains( data_type ) )
				return new ReturnObject() {
					Error = true,
					Message = "Invalid data type."
				};

			var list = new Lib.Data.UserList() {
				DataType = data_type,
				DateCreated = DateTime.Now,
				DateModified = DateTime.Now,
				Name = name,
				System = false,
				UserProfileID = profile.ID.Value
			};
			list.Save();

			return new ReturnObject() {
				Error = false,
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully created a new list.",
						title = "List Created"
					}
				}
			};
		}
	}
}
