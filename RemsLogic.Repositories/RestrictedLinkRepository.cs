using System;
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
        #endregion

        #region Utilty Methods
        private RestrictedLink ReadRestrictedLink(SqlDataReader reader)
        {
            return new RestrictedLink
            {
                Url = (string)reader["Url"],
                Token = (Guid)reader["Token"],
                ExpirationDate = (DateTime)reader["ExpirationDate"]
            };
        }
        #endregion
    }
}
