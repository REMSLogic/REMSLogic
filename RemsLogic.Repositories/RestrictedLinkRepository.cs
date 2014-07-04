using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public class RestrictedLinkRepository : Repository<RestrictedLink>, IRestrictedLinkRepository
    {
        #region Constructor
        public RestrictedLinkRepository(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        #region IRestrictedLinkRepository Implementation
        public override IEnumerable<RestrictedLink> GetAll()
        {
            String sql = @"
                SELECT *
                FROM RestrictedLinks
                ORDER BY ExpirationDate DESC;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            yield return ReadRestrictedLink(reader);
                        }
                    }
                }
            }
        }

        public RestrictedLink GetByToken(Guid token)
        {
            String sql = @"
                SELECT *
                FROM RestrictedLinks
                WHERE Token = @Token;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new []
                    {
                        new SqlParameter("Token", token)
                    });

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read()
                            ? ReadRestrictedLink(reader)
                            : null;
                    }
                }
            }
        }

        public override void Save(RestrictedLink model)
        {
            String sql = @"
                INSERT INTO RestrictedLinks
                    (Url, Token, ExpirationDate, CreatedFor)
                VALUES
                    (@Url, @Token, @ExpirationDate, @CreatedFor);";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new []
                    {
                        new SqlParameter("Url", model.Url),
                        new SqlParameter("Token", model.Token),
                        new SqlParameter("ExpirationDate", model.ExpirationDate),
                        new SqlParameter("CreatedFor", model.CreatedFor)
                    });

                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Utilty Methods
        private RestrictedLink ReadRestrictedLink(SqlDataReader reader)
        {
            return new RestrictedLink
            {
                Url = (string)reader["Url"],
                Token = (Guid)reader["Token"],
                ExpirationDate = (DateTime)reader["ExpirationDate"],
                CreatedFor = reader["CreatedFor"].ToString()
            };
        }
        #endregion
    }
}
