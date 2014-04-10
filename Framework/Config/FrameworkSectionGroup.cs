using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Framework.Config
{
	public class FrameworkSectionGroup : ConfigurationSectionGroup
	{
		[ConfigurationProperty("data")]
		public Framework.DataSection Data
		{
			get
			{
				return (Framework.DataSection)this.Sections["data"];
			}
		}

		[ConfigurationProperty( "email" )]
		public Framework.EmailSection Email
		{
			get
			{
				return (Framework.EmailSection)this.Sections["email"];
			}
		}

		[ConfigurationProperty( "security" )]
		public Framework.SecuritySectionGroup Security
		{
			get
			{
				return (Framework.SecuritySectionGroup)this.SectionGroups["security"];
			}
		}

		[ConfigurationProperty( "net" )]
		public Framework.NetSection Net
		{
			get
			{
				return (Framework.NetSection)this.Sections["net"];
			}
		}

		[ConfigurationProperty("encryption")]
		public Framework.EncryptionSection Encryption
		{
			get
			{
				return (Framework.EncryptionSection)this.Sections["encryption"];
			}
		}

		[ConfigurationProperty( "hash" )]
		public Framework.HashSection Hash
		{
			get
			{
				return (Framework.HashSection)this.Sections["hash"];
			}
		}

		[ConfigurationProperty("log")]
		public Framework.LogSection Log
		{
			get
			{
				return (Framework.LogSection)this.Sections["log"];
			}
		}
	}
}
