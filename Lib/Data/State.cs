using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
    [Table( DatabaseName = "FDARems", TableName = "States", PrimaryKeyColumn = "ID" )]
    public class State : ActiveRecord
    {
        #region Member Variables
        [Column] public string Name;
        [Column] public string Status;
        [Column] public string ISO;
        [Column] public string ANSI_LETTER;
        [Column] public string ANSI_DIGIT;
        [Column] public string USPS;
        [Column] public string OldGPO;
        [Column] public string AP;
        [Column] public string Other;
        #endregion

        #region Constructors
        public State(long? id = null)
            : base(id)
        {
        }

        public State(IDataRecord row)
            : base(row)
        {
        }
        #endregion

        #region Utility Methods
        public static IList<State> FindAll()
        {
            return FindAll<State>(new[]{"+Name","+USPS"});
        }
        #endregion
    }
}
