using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
    [Table( DatabaseName = "FDARems", TableName = "Specialities", PrimaryKeyColumn = "ID" )]
    public class Speciality : ActiveRecord
    {
        public static IList<Speciality> FindAll()
        {
            return FindAll<Speciality>( new[] { "+Order", "+Name" } );
        }

        [Column]
        public string Name;
        [Column]
        public int Order;
        [Column]
        public string Code;

        public Speciality(long? id = null) : base(id)
        { }

        public Speciality(IDataRecord row) : base(row)
        { }
    }
}