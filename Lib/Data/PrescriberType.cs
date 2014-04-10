using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "PrescriberTypes", PrimaryKeyColumn = "ID" )]
    public class PrescriberType : ActiveRecord
    {
		public static IList<PrescriberType> FindAll()
		{
			return FindAll<PrescriberType>( new[] {
				"+DisplayName"
			} );
		}

		public static PrescriberType FindByName( string name )
		{
			return FindFirstBy<PrescriberType>( new Dictionary<string, object> {
				{ "Name", name }
			} );
		}

		public static PrescriberType FindByDisplayName( string displayName )
		{
			return FindFirstBy<PrescriberType>( new Dictionary<string, object> {
				{ "DisplayName", displayName }
			} );
		}

        [Column]
        public string DisplayName;
        [Column]
        public string Name;

        public PrescriberType(long? id = null) : base(id)
        {
        }

        public PrescriberType(IDataRecord row) : base(row)
        {
        }
    }
}
