using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using RemsLogic.Model.Compliance;
using RemsLogic.Repositories.ProxyObjects;

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
                        (ProfileId, DrugId, EocId, LinkId, QuestionId, DateCompleted, Deleted)
                    VALUES
                        (@ProfileId, @DrugId, @EocId, @LinkId, @QuestionId, @DateCompleted, @Deleted)";
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
                            LinkId = @LinkId,
                            QuestionId = @QuestionId,
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
                    command.Parameters.AddWithValue("LinkId", model.LinkId);
                    command.Parameters.AddWithValue("QuestionId", model.QuestionId);
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
        public void LogEocComplianceEntry(long prescriberEocId, DateTime recordedAt)
        {
            const string sql = @"
                INSERT INTO UserEocsLog
                    (UserEocsId, RecordedAt)
                VALUES
                    (@UserEocsId, @RecordedAt);";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("UserEocsId", prescriberEocId);
                    command.Parameters.AddWithValue("RecordedAt", recordedAt);

                    command.ExecuteNonQuery();
                }
            }
        }

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

        public PrescriberEoc FindByLinkId(long profileId, long linkId)
        {
            const string sql = @"
                SELECT *
                FROM UserEocs
                WHERE
                    LinkId = @LinkId AND
                    ProfileId = @ProfileId;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("LinkId", linkId);
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

        public Eoc GetEoc(long id)
        {
            const string sql = @"
                SELECT *
                FROM Eocs
                WHERE
                    Eocs.ID = @EocId;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("EocId", id);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            return ReadEoc(reader);
                        }
                    }
                }
            }

            return null;
        }

        public Eoc GetEoc(long drugId, long questionId)
        {
            const string sql = @"
                SELECT 
                    Eocs.*
                FROM DSQ_Eocs
                    INNER JOIN Eocs ON Eocs.ID = DSQ_Eocs.EocId
    
                WHERE
                    DrugID = @DrugId AND
                    QuestionId = @QuestionId;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("DrugId", drugId);
                    command.Parameters.AddWithValue("QuestionId", questionId);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                            return ReadEoc(reader);
                    }
                }
            }

            return null;
        }

        public IEnumerable<Eoc> GetEocs()
        {
            const string sql = @"
                SELECT 
                    *
                FROM Eocs
                ORDER BY DisplayOrder ASC;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
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

        public IEnumerable<Eoc> GetByDrug(long drugId, bool prereqOnly = false)
        {
            string sql = @"
                SELECT DISTINCT
                    DSQ_Links.IsRequired, DSQ_Links.HasPrereq, Eocs.*
                FROM DSQ_Links
                    INNER JOIN Eocs ON Eocs.ID = DSQ_Links.EocId
                WHERE
                    DrugID = @DrugId ";

            sql += prereqOnly? "AND DSQ_Links.HasPrereq = 1;" : ";";

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

        public IEnumerable<Eoc> GetByDrugAndRole(long drugId, string role, bool prereqOnly = false)
        {
            string sql = @"
                SELECT DISTINCT
                    DSQ_Links.IsRequired, DSQ_Links.HasPrereq, Eocs.*
                FROM DSQ_Links
                    INNER JOIN Eocs ON Eocs.ID = DSQ_Links.EocId
                WHERE
                    DrugID = @DrugId AND
                    Eocs.Roles LIKE @Role ";

            sql += prereqOnly? "AND DSQ_Links.HasPrereq = 1;" : ";";

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

        public IEnumerable<PrescriberEoc> GetPrescriberEocs(long drugId, long questionId, long userProfileId)
        {
            const string sql = @"
                SELECT *
                FROM UserEocs
                WHERE
                    ProfileId = @ProfileId AND
                    DrugId = @DrugId AND
                    QuestionId = @QuestionId AND
                    Deleted = 0;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("ProfileId", userProfileId);
                    command.Parameters.AddWithValue("DrugId", drugId);
                    command.Parameters.AddWithValue("QuestionId", questionId);

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

        public IEnumerable<PrescriberEocLogEntry> GetComplianceLog(long prescriberEocId)
        {
            const string sql = @"
                SELECT *
                FROM UserEocsLog
                WHERE
                    UserEocsId = @PrescriberEocId
                ORDER BY
                    RecordedAt ASC;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("PrescriberEocId", prescriberEocId);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            yield return ReadPrescriberEocLogEntry(reader);
                        }
                    }
                }
            }
        }

        public void RemovePrescriberEocs(long profileId, long drugId)
        {
            const string sql = @"
                UPDATE UserEocs
                    SET
                        Deleted = 1
                WHERE
                    ProfileId = @ProfileId AND
                    DrugId = @DrugId;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("ProfileId", profileId);
                    command.Parameters.AddWithValue("DrugId", drugId);

                    command.ExecuteNonQuery();
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
                ShortDisplayName = (string)reader["ShortDisplayName"],
                LargeIcon = (string)reader["LargeIcon"],
                SmallIcon = (string)reader["SmallIcon"],
                AppliesTo = reader["Roles"] != DBNull.Value
                    ? ((string)reader["Roles"]).Split(new []{'|'}, StringSplitOptions.RemoveEmptyEntries)
                        .ToList()
                    : new List<string>(),
                DisplayFor = reader["DisplayForRoles"] != DBNull.Value
                    ? ((string)reader["DisplayForRoles"]).Split(new []{'|'}, StringSplitOptions.RemoveEmptyEntries)
                        .ToList()
                    : new List<string>(),
            };
        }

        private PrescriberEoc ReadPrescriberEoc(SqlDataReader reader)
        {
            return new PrescriberEocProxy(this)
            {
                Id = (long)reader["ID"],
                PrescriberProfileId = (long)reader["ProfileId"],
                DrugId = (long)reader["DrugId"],
                EocId = (long)reader["EocId"],
                LinkId = (long)reader["LinkId"],
                QuestionId = (long)reader["QuestionId"],
                CompletedAt = reader["DateCompleted"] != DBNull.Value
                    ? (DateTime)reader["DateCompleted"]
                    : (DateTime?)null,
                Deleted = (bool)reader["Deleted"]
            };
        }

        private PrescriberEocLogEntry ReadPrescriberEocLogEntry(SqlDataReader reader)
        {
            return new PrescriberEocLogEntry
            {
                Id = (long)reader["Id"],
                PrescriberEocId = (long)reader["UserEocsId"],
                RecordedAt = (DateTime)reader["RecordedAt"]
            };
        }
        #endregion
    }
}
