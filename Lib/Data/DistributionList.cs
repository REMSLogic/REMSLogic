using System.Collections.Generic;
using System.Data;
using System.Text;
using Framework.Data;
using Framework.Security;
using Lib.Systems.Notifications;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "DistributionLists", PrimaryKeyColumn = "ID" )]
    public class DistributionList : ActiveRecord
    {
        #region Member Variables
        [Column]
        public long? UserProfileId;
        [Column]
        public string Name;
        [Column]
        public string ListGeneratorType;
        [Column]
        public string Settings;

        private IDistributionListGenerator _listGenerator;
        private IList<User> _users;
        #endregion

        #region Properties
        public string DisplayName {get{return Name;}}
        public long ListId {get{return ID ?? 0;}}
        #endregion

        #region Navigation Properties
        public IList<User> Users
        {
            get{return _users ?? (_users = LoadUsers());}
        }
        #endregion

        #region Constructors
        public DistributionList(long? id = null)
            : base(id)
        {
        }

        public DistributionList(IDataRecord row)
            : base(row)
        {
        }
        #endregion

        #region Utility Methods
        private IList<User> LoadUsers()
        {
            DistributionListGeneratorFactory factory = new DistributionListGeneratorFactory();

            if(_listGenerator == null)
                _listGenerator = factory.GetGenerator(ListGeneratorType, Settings);

            return _listGenerator.GetUsers();
        }
        #endregion

        #region Factory Methods
        public static IList<DistributionList> FindByUserProfile(UserProfile profile)
        {
            Database db = Database.Get("FDARems");
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * ");
            sql.Append("FROM "+db.DelimTable("DistributionLists")+" ");
            sql.Append("WHERE "+db.DelimColumn("UserProfileId")+" = "+db.DelimParameter("ProfileId")+" ");
            sql.Append("ORDER BY "+db.DelimColumn("Name")+" ASC;");

            List<Parameter> ps = new List<Parameter>
            {
                new Parameter("ProfileId", profile.ID)
            };

            return db.ExecuteQuery<DistributionList>(sql.ToString(), ps.ToArray());
        }
        #endregion
    }
}
