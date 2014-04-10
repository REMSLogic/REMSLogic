using System;
using System.Data;
using System.Text;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "Addresses", PrimaryKeyColumn = "ID" )]
	public class Address : ActiveRecord
	{
		[Column]
		public string Street1;
		[Column]
		public string Street2;
		[Column]
		public string City;
		[Column]
		public string State;
		[Column]
		public string Zip;
		[Column]
		public string Country;

		public Address(long? id = null) : base( id )
		{ }

		public Address(IDataRecord row) : base( row )
		{ }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Street1);

            if(!String.IsNullOrEmpty(Street2))
                sb.Append(" "+Street2);

            sb.Append(", ");
            sb.Append(City);
            sb.Append(", ");
            sb.Append(State);
            sb.Append(" ");
            sb.Append(Zip);

            return sb.ToString();
        }
	}
}
