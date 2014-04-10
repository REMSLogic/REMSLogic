using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Framework.Config.Framework.Data
{
	public class ConnectionElement : ConfigurationElement
	{
#region Constructors
        /// <summary>
        /// Predefines the valid properties and prepares
        /// the property collection.
        /// </summary>
		static ConnectionElement()
        {
            // Predefine properties here
            s_propName = new ConfigurationProperty(
                "name",
                typeof(string),
                null,
                ConfigurationPropertyOptions.IsRequired
            );

            s_propCS = new ConfigurationProperty(
                "connectionString",
                typeof(string),
                null,
                ConfigurationPropertyOptions.IsRequired
            );

            s_propType = new ConfigurationProperty(
                "type",
                typeof(string),
                "SqlServer",
                ConfigurationPropertyOptions.None
            );

            s_properties = new ConfigurationPropertyCollection();
            
            s_properties.Add(s_propName);
            s_properties.Add(s_propCS);
            s_properties.Add(s_propType);
        }
        #endregion

        #region Static Fields
        private static ConfigurationProperty s_propName;
        private static ConfigurationProperty s_propCS;
        private static ConfigurationProperty s_propType;

        private static ConfigurationPropertyCollection s_properties;
#endregion

#region Properties
        /// <summary>
        /// Gets the Name setting.
        /// </summary>
        [ConfigurationProperty("name", IsRequired=true)]
        public string Name
        {
            get { return (string)base[s_propName]; }
        }

        /// <summary>
        /// Gets the Type setting.
        /// </summary>
		[ConfigurationProperty( "connectionString", IsRequired=true )]
        public string ConnectionString
        {
            get { return (string)base[s_propCS]; }
        }

        /// <summary>
        /// Gets the Type setting.
        /// </summary>
        [ConfigurationProperty("type")]
        public string Type
        {
            get { return (string)base[s_propType]; }
        }

        /// <summary>
        /// Override the Properties collection and return our custom one.
        /// </summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get { return s_properties; }
        }
#endregion
	}
}
