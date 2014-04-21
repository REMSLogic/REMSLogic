using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public class DrugRepository : Repository<Drug>, IDrugRepository
    {
        public DrugRepository(string connectionString)
            : base(connectionString)
        {
        }

        public override Drug Get(long id)
        {
            const string sql = @"
                SELECT *
                FROM Drugs
                WHERE
                    ID = @DrugId;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("DrugId", id);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read() 
                            ? ReadDrug(reader) 
                            : null;
                    }
                }
            }
        }

        public IEnumerable<Drug> GetByPrescriberProfile(long profileId)
        {
            const string sql = @"
                SELECT *
                FROM Drugs
                    INNER JOIN DrugSelections ON DrugSelections.DrugID = Drugs.ID
                    INNER JOIN PrescriberProfiles ON DrugSelections.PrescriberId = PrescriberProfiles.PrescriberID
                WHERE
                    PrescriberProfiles.ID = @ProfileId AND
                    Prescribes = 1;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("ProfileId", profileId);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                            yield return ReadDrug(reader);
                    }
                }
            }
        }

        #region Utilty Methods
        private Drug ReadDrug(SqlDataReader reader)
        {
            return new Drug
            {
                Id = (long)reader["ID"],
                ClassId = (long)reader["ClassID"],
                SystemId = reader["SystemId"] != DBNull.Value
                    ? (long)reader["SystemID"] 
                    : (long?)null,
                UpdatedById=  (long)reader["UpdatedByID"], 
                GenericName = (string)reader["GenericName"],
                RemsReason = reader["RemsReason"] != DBNull.Value
                    ? (string)reader["RemsReason"]
                    : null,
                Indication = reader["Indication"] != DBNull.Value
                    ? (string)reader["Indication"]
                    : null,
                RemsProgramUrl = reader["RemsProgramUrl"] != DBNull.Value
                    ? (string)reader["RemsProgramUrl"]
                    : null,
                FdaApplicationNumber = reader["FdaApplicationNumber"] != DBNull.Value
                    ? (string)reader["FdaApplicationNumber"]
                    : null,
                RemsApproved = reader["RemsApproved"] != DBNull.Value
                    ? (DateTime)reader["RemsApproved"]
                    : (DateTime?)null,
                RemsUpdated = reader["RemsUpdated"] != DBNull.Value
                    ? (DateTime)reader["RemsUpdated"]
                    : (DateTime?)null,
                Updated = (DateTime)reader["Updated"],
                Active = (bool)reader["Active"],
                EocIcons = reader["EocIcons"] != DBNull.Value
                    ? (string)reader["EocIcons"]
                    : null
            };
        }
        #endregion
    }
}
