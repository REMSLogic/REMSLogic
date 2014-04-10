using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Data
{
    public class Parameter
    {
        public string Name { get; protected set; }
        public object Value { get; protected set; }
		public Type DataType { get; protected set; }

        public Parameter(string name, object value, Type data_type = null)
        {
            this.Name = name;
            this.Value = value ?? DBNull.Value;
			this.DataType = data_type;
        }
    }
}
