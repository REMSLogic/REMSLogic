using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data.DSQ
{
    [Table(DatabaseName= "FDARems", TableName = "DSQ_SectionSettings", PrimaryKeyColumn ="Id")]
    public class SectionSettings : ActiveRecord
    {
        [Column]
        public long SectionId;
        [Column]
        public long DrugId;
        [Column]
        public bool IsEnabled;

        public SectionSettings(long? id = null) 
            : base(id)
        { 
        }

        public SectionSettings(IDataRecord row)
            : base(row)
        {
        }

        public static SectionSettings Get(long sectionId, long drugId)
        {
            return FindFirstBy<SectionSettings>(new Dictionary<string, object>
            {
                {"SectionId", sectionId},
                {"DrugId", drugId}
            });
        }
    }
}
