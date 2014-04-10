using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Framework.Config
{
	public static class Manager
	{
		private static Configuration config = null;
		public static FrameworkSectionGroup Framework
		{
			get
			{
				GetConfig();
				return GetSectionGroup<FrameworkSectionGroup>( "framework" );
			}
		}

		public static T GetSectionGroup<T>(string name) where T : ConfigurationSectionGroup
		{
			GetConfig();
			return (T)config.GetSectionGroup( name );
		}

		public static T GetSection<T>(string name) where T : ConfigurationSection
		{
			GetConfig();
			return (T)config.GetSection( name );
		}

		private static void GetConfig()
		{
			if( config == null )
				config = WebConfigurationManager.OpenWebConfiguration( HttpContext.Current.Request.ApplicationPath );
		}
	}
}
