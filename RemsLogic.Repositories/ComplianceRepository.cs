using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public class ComplianceRepository : Repository<PrescriberEoc>, IComplianceRepository
    {
        #region Constructor
        public ComplianceRepository(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        #region IRepository Implementation
        public override void Save(PrescriberEoc model)
        {
            string sql;

            if(model.Id == 0)
            {
                sql = @"
                    INSERT INTO UserEoc
                        (ProfileId, DrugId, EocId, DateCompleted, Deleted)
                    VALUES
                        (@ProfileId, @DrugId, @EoczId, @DateCompleted, @Deleted)";
            }
            else
            {
                sql = @"
                    UPDATE UserEoc
                        SET 
                            ProfileId = @ProfileId,
                            DrugId = @DrugId,
                            EocId = @EocId,
                            DateCompleted = @DateCompleted,
                            Deleted = @Deleted;";
            }

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("ProfileId", model.PrescriberProfileId);
                    command.Parameters.AddWithValue("DrugId", model.DrugId);
                    command.Parameters.AddWithValue("EocId", model.EocId);
                    command.Parameters.AddWithValue("DateCompleted", model.CompletedAt);
                    command.Parameters.AddWithValue("Deleted", model.Deleted);

                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region IComplianceRepository Implementaion
        public IEnumerable<Eoc> GetByDrugId(long drugId)
        {
            string sql = @"
                SELECT *
                FROM Eocs
                    INNER JOIN DrugEocs ON DrugEocs.EocID = Eocs.ID
                WHERE
                    DrugEocs.DrugID = @DrugId;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("DrugId", drugId);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            yield return ReadEoc(reader);
                        }
                    }
                }
            }
        }

        public IEnumerable<PrescriberEoc> GetByPrescriberProfile(long profileId)
        {
            string sql = @"
                SELECT *
                FROM UserEocs
                WHERE
                    ProfileId = @ProfileId AND
                    Deleted = 0;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("ProfileId", profileId);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            yield return ReadPrescriberEoc(reader);
                        }
                    }
                }
            }
        }
        #endregion

        #region Utility Methods
        private Eoc ReadEoc(SqlDataReader reader)
        {
            return new Eoc
            {
                Id = (long)reader["ID"],
                Name = (string)reader["Name"],
                DisplayName = (string)reader["DisplayName"]
            };
        }

        private PrescriberEoc ReadPrescriberEoc(SqlDataReader reader)
        {
            return new PrescriberEoc
            {
                PrescriberProfileId = (long)reader["ProfileId"],
                DrugId = (long)reader["DrugId"],
                EocId = (long)reader["EocId"],
                CompletedAt = (DateTime)reader["DateCompleted"],
                Deleted = (bool)reader["Deleted"]
            };
        }
        #endregion
    }
}
