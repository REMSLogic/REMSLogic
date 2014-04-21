using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
                    INSERT INTO UserEocs
                        (ProfileId, DrugId, EocId, DateCompleted, Deleted)
                    VALUES
                        (@ProfileId, @DrugId, @EocId, @DateCompleted, @Deleted)";
            }
            else
            {
                sql = @"
                    UPDATE UserEocs
                        SET 
                            ProfileId = @ProfileId,
                            DrugId = @DrugId,
                            EocId = @EocId,
                            DateCompleted = @DateCompleted,
                            Deleted = @Deleted
                    WHERE ID = @UserEocId;";
            }

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("ProfileId", model.PrescriberProfileId);
                    command.Parameters.AddWithValue("DrugId", model.DrugId);
                    command.Parameters.AddWithValue("EocId", model.EocId);
                    command.Parameters.AddWithValue("DateCompleted", model.CompletedAt != null
                        ? (object)model.CompletedAt.Value 
                        : DBNull.Value);
                    command.Parameters.AddWithValue("Deleted", model.Deleted);

                    if(model.Id > 0)
                        command.Parameters.AddWithValue("UserEocId", model.Id);

                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region IComplianceRepository Implementaion
        public PrescriberEoc Find(long profileId, long drugId, long eocId)
        {
            const string sql = @"
                SELECT *
                FROM UserEocs
                WHERE
                    DrugID = @DrugId AND
                    EocID = @EocId AND
                    ProfileId = @ProfileId;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("DrugId", drugId);
                    command.Parameters.AddWithValue("EocId", eocId);
                    command.Parameters.AddWithValue("ProfileId", profileId);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            return ReadPrescriberEoc(reader);
                        }
                    }
                }
            }

            return null;
        }

        public IEnumerable<Eoc> GetByDrug(long drugId)
        {
            const string sql = @"
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

        public IEnumerable<Eoc> GetByDrugAndRole(long drugId, string role)
        {
            const string sql = @"
                SELECT *
                FROM Eocs
                    INNER JOIN DrugEocs ON DrugEocs.EocID = Eocs.ID
                WHERE
                    DrugEocs.DrugID = @DrugId AND
                    Eocs.Roles LIKE @Role;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("DrugId", drugId);
                    command.Parameters.AddWithValue("Role", String.Format("%{0}%", role));

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
            const string sql = @"
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
                DisplayName = (string)reader["DisplayName"],
                AppliesTo = reader["Roles"] != DBNull.Value
                    ? ((string)reader["Roles"]).Split(new []{'|'}, StringSplitOptions.RemoveEmptyEntries)
                        .ToList()
                    : new List<string>()
            };
        }

        private PrescriberEoc ReadPrescriberEoc(SqlDataReader reader)
        {
            return new PrescriberEoc
            {
                Id = (long)reader["ID"],
                PrescriberProfileId = (long)reader["ProfileId"],
                DrugId = (long)reader["DrugId"],
                EocId = (long)reader["EocId"],
                CompletedAt = reader["DateCompleted"] != DBNull.Value
                    ? (DateTime)reader["DateCompleted"]
                    : (DateTime?)null,
                Deleted = (bool)reader["Deleted"]
            };
        }
        #endregion
    }
}
