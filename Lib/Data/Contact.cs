using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "Contacts", PrimaryKeyColumn = "ID" )]
	public class Contact : ActiveRecord
	{
		[Column]
		public string Prefix;
		[Column]
		public string FirstName;
		[Column]
		public string LastName;
		[Column]
		public string Postfix;
		[Column]
		public string Title;
		[Column]
		public string Email;
		[Column]
		public string Phone;
		[Column]
		public string Fax;

        public string ContactType;
        public string ParentType;

		public string Name
		{ get { return FirstName + " " + LastName; } }

		public Contact(long? id = null) : base(id)
		{ }

		public Contact(IDataRecord row) : base(row)
		{ }
	}
}
