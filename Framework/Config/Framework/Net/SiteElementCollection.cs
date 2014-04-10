using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Framework.Config.Framework.Net
{
	[ConfigurationCollection( typeof( SiteElement ), CollectionType = ConfigurationElementCollectionType.BasicMap )]
	public class SiteElementCollection : ConfigurationElementCollection
	{
		#region Constructors
		static SiteElementCollection()
		{
			m_properties = new ConfigurationPropertyCollection();
		}

		public SiteElementCollection()
		{
		}
		#endregion

#region Fields
		private static ConfigurationPropertyCollection m_properties;
#endregion

#region Properties
		protected override ConfigurationPropertyCollection Properties
		{
			get { return m_properties; }
		}
    
		public override ConfigurationElementCollectionType CollectionType
		{
			get { return ConfigurationElementCollectionType.BasicMap; }
		}

		protected override string ElementName
		{
			get { return "site"; }
		}

		public SiteElement CurrentSite
		{
			get
			{
				for( int i = 0; i < this.Count; i++ )
				{
					if( this[i].Current )
					{
						return this[i];
					}
				}

				return this[0];
			}
		}
#endregion

#region Indexers
		public SiteElement this[int index]
		{
			get { return (SiteElement)base.BaseGet( index ); }
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				base.BaseAdd(index, value);
			}
		}

		public new SiteElement this[string name]
		{
			get { return (SiteElement)base.BaseGet( name ); }
		}
#endregion

		protected override ConfigurationElement CreateNewElement()
		{
			return new SiteElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as SiteElement).Name;
		}
	}
}
