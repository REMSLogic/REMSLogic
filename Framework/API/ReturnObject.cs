using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.API
{
	public class ReturnObject
	{
		public int StatusCode;
		public bool Error = false;
		public string Message;
		public object Result;
		public ReturnGrowlObject Growl;
		public ReturnRedirectObject Redirect;
		public List<ReturnActionObject> Actions;
	}

	public class ReturnGrowlObject
	{
		public ReturnGrowlOptsObject Opts;
		public ReturnGrowlVarsObject Vars;
		public string Type = "default";
	}

	public class ReturnGrowlVarsObject
	{
		public string title;
		public string text;
	}

	public class ReturnGrowlOptsObject
	{
		public int? speed = 500;
		public int? expires = null;
	}

	public class ReturnRedirectObject
	{
		public string Hash;
		public string Url;
	}

	public class ReturnActionObject
	{
		public string Type;
		public string Ele;
	}
}
