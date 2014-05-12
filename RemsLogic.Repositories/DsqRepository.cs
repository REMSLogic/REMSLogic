using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RemsLogic.Model.Dsq;

namespace RemsLogic.Repositories
{
    public class DsqRepository : Repository<Questionnaire>, IDsqRepository
    {
        #region Constructor
        public DsqRepository(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        #region IDsqRepository Implementation
        public void AddEoc(DsqEoc eoc)
        {
            const string sql = @"
                INSERT INTO DSQ_Eocs
                    (DrugId, EocId, QuestionId, IsRequired)
                OUTPUT INSERTED.Id
                VALUES 
                    (@DrugId, @EocId, @QuestionId, @IsRequired);";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new []
                        {
                            new SqlParameter("DrugId", eoc.DrugId),
                            new SqlParameter("QuestionId", eoc.QuestionId),
                            new SqlParameter("EocId", eoc.EocId),
                            new SqlParameter("IsRequired", eoc.IsRequired)
                        });

                    eoc.Id = (long)command.ExecuteScalar();
                }
            }
        }

        public void DeleteEoc(long drugId, long questionId)
        {
            const string sql = @"
                DELETE FROM DSQ_Eocs
                WHERE
                    DrugId = @DrugId AND
                    QuestionId = @QuestionId;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("DrugId", drugId);
                    command.Parameters.AddWithValue("QuestionId", questionId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public DsqLink GetLink(long id)
        {
            const string sql = @"
                SELECT *
                FROM DSQ_Links
                WHERE
                    ID = @LinkId;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("LinkId", id);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                            return ReadLink(reader);
                    }
                }
            }

            return null;
        }

        public IEnumerable<DsqLink> GetLinks(long drugId, long questionid)
        {
            const string sql = @"
                SELECT *
                FROM DSQ_Links
                WHERE
                    DrugId = @DrugId AND
                    QuestionId = @QuestionId;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("DrugId", drugId);
                    command.Parameters.AddWithValue("QuestionId", questionid);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                            yield return ReadLink(reader);
                    }
                }
            }
        }

        public void SaveLink(DsqLink link)
        {
            const string insertSql = @"
                INSERT INTO DSQ_Links
                    (DrugID, QuestionID, Label, Value, HelpText, Date, EocId, IsRequired)
                OUTPUT INSERTED.Id
                VALUES
                    (@DrugId, @QuestionId, @Label, @Value, @HelpText, @Date, @EocId, @IsRequired);";

            const string updateSql = @"
                UPDATE DSQ_Links SET
                    DrugId = @DrugId,
                    QuestionId = @QuestionId,
                    Label = @Label,
                    Value = @Value,
                    HelpText = @HelpText,
                    Date = @Date,
                    EocId = @EocId,
                    IsRequired = @IsRequired
                WHERE
                    Id = @Id;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(link.Id > 0? updateSql : insertSql, connection))
                {
                    command.Parameters.AddRange(new []
                        {
                            new SqlParameter("DrugId", link.DrugId),
                            new SqlParameter("QuestionId", link.QuestionId),
                            new SqlParameter("Label", link.Label),
                            new SqlParameter("Value", link.Value),
                            new SqlParameter("HelpText", link.HelpText),
                            new SqlParameter("Date", link.Date),
                            new SqlParameter("EocId", link.EocId),
                            new SqlParameter("IsRequired", link.IsRequired)
                        });

                    if(link.Id != 0)
                    {
                        command.Parameters.AddWithValue("Id", link.Id);
                        command.ExecuteNonQuery();
                    }
                    else
                        link.Id = (long)command.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Utility Methods
        private DsqEoc ReadEoc(SqlDataReader reader)
        {
            return new DsqEoc
            {
                Id = (long)reader["Id"],
                DrugId = (long)reader["DrugId"],
                QuestionId = (long)reader["QuestionId"],
                EocId = (long)reader["EocId"],
                IsRequired = (bool)reader["IsRequired"]
            };
        }

        private DsqLink ReadLink(SqlDataReader reader)
        {
            return new DsqLink
            {
                Id = (long)reader["ID"],
                DrugId = (long)reader["DrugId"],
                QuestionId = (long)reader["QuestionId"],
                Label = (string)reader["Label"],
                Value = (string)reader["Value"],
                HelpText = reader["HelpText"] != DBNull.Value
                    ? (string)reader["HelpText"]
                    : null,
                Date = reader["Date"] != DBNull.Value
                    ? (DateTime)reader["Date"]
                    : (DateTime?)null,
                IsRequired = (bool)reader["IsRequired"],
                EocId = (long)reader["EocId"]
            };
        }
        #endregion
    }
}
