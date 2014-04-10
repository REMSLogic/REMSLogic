using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
	public class Manager
	{
		public static void Init()
		{
			System.Web.HttpContext.Current.Application["Lib_Init"] = true;
		}
	}
}
