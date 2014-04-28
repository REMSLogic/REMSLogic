using System;
using System.Data.SqlClient;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public class DsqRepository : Repository<Dsq>, IDsqRepository
    {
        #region Constructor
        public DsqRepository(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        #region IDsqRepository Implementation
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

        public void SaveLink(DsqLink link)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Utility Methods
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
                    : (DateTime?)null
            };
        }
        #endregion
    }
}
