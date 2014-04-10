using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace Framework
{
	public class Email
	{
		public static void SendEmail(string to, string subject, string message, string from_email = null, string from_name = null, bool isHtml = true)
		{
            return;

			var msg = new MailMessage();
			msg.To.Add(to);

			if( !string.IsNullOrEmpty(from_email) )
			{
				if( !string.IsNullOrEmpty(from_name) )
					msg.From = new MailAddress(from_email, from_name);
				else
					msg.From = new MailAddress(from_email);
			}

			msg.IsBodyHtml = isHtml;
			msg.Subject = subject;
			msg.Body = message;

			var client = new SmtpClient();
			//client.Send(msg);
		}

		public class TemplateOverrides
		{
			public IEnumerable<MailAddress> To;
			public MailAddress From;
			public string Subject;
		}

		public static void SendTemplate( string template_name, Dictionary<string, object> data, TemplateOverrides overrides = null )
		{
            return;

			var template_path = System.Web.HttpContext.Current.Server.MapPath(Config.Manager.Framework.Email.Template.Path);
			var base_path = Path.Combine( template_path, template_name );

			if( !File.Exists( Path.Combine(base_path,"config.json") ) )
				throw new FileNotFoundException( "Could not find the specified config file." );

			var sr_config = new StreamReader( Path.Combine(base_path,"config.json") );
			var config = Newtonsoft.Json.Linq.JObject.Parse( sr_config.ReadToEnd() );
			sr_config.Close();

			if( !File.Exists(Path.Combine( base_path, (string)config["file"] )) )
				throw new FileNotFoundException( "The file [" + (string)config["file"] + "] referenced in the template config can not be found in [" + base_path + "]." );

			if( overrides != null )
			{
				if( !string.IsNullOrEmpty( overrides.Subject ) )
					config["subject"] = overrides.Subject;

				if( overrides.To != null )
				{
					var arr = new Newtonsoft.Json.Linq.JArray();

					foreach( var to in overrides.To )
					{
						arr.Add(new Newtonsoft.Json.Linq.JObject(new Newtonsoft.Json.Linq.JProperty("name", to.DisplayName), new Newtonsoft.Json.Linq.JProperty("email", to.Address)));
					}

					config["to"] = arr;
				}

				if( overrides.From != null )
				{
					config["from"] = new Newtonsoft.Json.Linq.JObject( new Newtonsoft.Json.Linq.JProperty( "name", overrides.From.DisplayName ), new Newtonsoft.Json.Linq.JProperty( "email", overrides.From.Address ) );
				}
			}

			if( config["vars"] != null )
			{
				foreach( Newtonsoft.Json.Linq.JObject o in config["vars"] )
				{
					string name = (string)o["name"];
					string file = (string)o["file"];
					string loop_over = (string)o["loop_over"];
					string condition = (string)o["condition"];

					if( !File.Exists( Path.Combine( base_path, file ) ) )
						throw new FileNotFoundException( "The file ["+file+"] referenced in the template config can not be found in ["+base_path+"]." );

					var sr_inner = new StreamReader( Path.Combine( base_path, file ) );
					string inner_content = sr_inner.ReadToEnd();
					sr_inner.Close();
					data[name] = "";

					if( !string.IsNullOrEmpty( condition ) )
					{
						// test condition, if true allow execution to continue, else continue
					}

					if( !string.IsNullOrEmpty( loop_over ) )
					{
						if( !data.ContainsKey( loop_over ) )
							throw new ArgumentException( "The data dictionary is missing a required value called ["+loop_over+"].", "data" );

						var list = data[loop_over] as IEnumerable;

						if( list == null )
							throw new ArgumentException( "The value called [" + loop_over + "] in the data dictionary must be castable to an IEnumerable.", "data" );

						var regex_inner = new System.Text.RegularExpressions.Regex( @"\{\{([a-zA-Z0-9_\-\.]+)\}\}", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.CultureInvariant );
						var matches_inner = regex_inner.Matches( inner_content );

						var builder = new StringBuilder();
						foreach( var item in list )
						{
							string t_content = inner_content;

							foreach( System.Text.RegularExpressions.Match m in matches_inner )
							{
								var orig_match = m.Value;
								var group = m.Groups[1];
								var parts = group.Value.Split( '.' );

								if( parts[0] == "Item" )
									t_content = t_content.Replace( orig_match, GetValue( item, parts, orig_match, 1 ) );
								else
									t_content = t_content.Replace( orig_match, GetValue( data[parts[0]], parts, orig_match, 1 ) );
							}

							builder.AppendLine( t_content );
						}

						inner_content = builder.ToString();
					}

					data[name] = inner_content;
				}
			}

			var sr = new StreamReader( Path.Combine( base_path, (string)config["file"] ) );
			string content = sr.ReadToEnd();
			sr.Close();

			var regex = new System.Text.RegularExpressions.Regex( @"\{\{([a-zA-Z0-9_\-\.]+)\}\}", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.CultureInvariant );
			var matches = regex.Matches( content );

			foreach( System.Text.RegularExpressions.Match m in matches )
			{
				var orig_match = m.Value;
				var group = m.Groups[1];
				var parts = group.Value.Split( '.' );

				content = content.Replace( orig_match, GetValue(data[parts[0]], parts, orig_match, 1) );
			}

            // MJL 2014-01-13 - If you use the "To" field in the overrides, then the config["to"] is an array
            // the original code seems to assume it's not a collection.  I've added a simple check to see
            // if the object is a collection, of so, loop.
            if(!(config["to"] is ICollection))
                SendEmail((string)(config["to"]["email"]), (string)(config["subject"]), content, (string)(config["from"]["email"]), (string)(config["from"]["name"]));
            else
                foreach(var item in config["to"])
                    SendEmail((string)(item["email"]), (string)(config["subject"]), content, (string)(config["from"]["email"]), (string)(config["from"]["name"]));
		}

		private static string GetValue( object o, string[] parts, string orig_match, int startIndex = 0 )
		{
			for( int i = startIndex; i < parts.Length; i++ )
			{
				var pi = o.GetType().GetProperty( parts[i] );

				if( pi == null )
					throw new Exception( "Missing value in data dictionary for [" + orig_match + "]." );

				o = pi.GetGetMethod().Invoke( o, new object[] { } );

				if( o == null )
					throw new Exception( "Missing value in data dictionary for [" + orig_match + "]." );
			}

			return o.ToString();
		}
	}
}
