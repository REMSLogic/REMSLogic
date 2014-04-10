using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Framework.Config.Framework.Data
{
	[ConfigurationCollection( typeof( ConnectionElement ), CollectionType = ConfigurationElementCollectionType.BasicMap )]
	public class ConnectionElementCollection : ConfigurationElementCollection
	{
#region Constructors
		static ConnectionElementCollection()
		{
			m_properties = new ConfigurationPropertyCollection();
		}

		public ConnectionElementCollection()
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
			get { return "connection"; }
		}
#endregion

#region Indexers
		public ConnectionElement this[int index]
		{
			get { return (ConnectionElement)base.BaseGet( index ); }
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				base.BaseAdd(index, value);
			}
		}

		public new ConnectionElement this[string name]
		{
			get { return (ConnectionElement)base.BaseGet( name ); }
		}
#endregion

		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as ConnectionElement).Name;
		}
	}
}
