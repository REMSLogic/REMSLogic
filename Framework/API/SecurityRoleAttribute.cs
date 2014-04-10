using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.API
{
	public class SecurityRoleAttribute : Attribute
	{
		public string Role
		{ get; protected set; }

		public string ErrorMessage
		{ get; protected set; }

		public SecurityRoleAttribute(string r)
		{
			Role = r;
			ErrorMessage = null;
		}

		public SecurityRoleAttribute(string r, string message)
		{
			Role = r;
			ErrorMessage = message;
		}
	}
}
