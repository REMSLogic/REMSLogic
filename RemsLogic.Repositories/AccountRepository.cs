using System;
using System.Data.SqlClient;
using RemsLogic.Model.Ecommerce;

namespace RemsLogic.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        #region Constructor
        public AccountRepository(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        #region Base Class Implementation
        public override void Save(Account model)
        {
            const string insertSql = @"
                INSERT INTO Accounts
                    (UserProfileId, CreatedAt, ExpiresOn, Enabled)
                OUTPUT INSERTED.Id
                VALUES
                    (@UserProfileId, @CreatedAt, @ExpiresOn, @Enabled)";

            const string updateSql = @"
                UPDATE Accounts SET
                    UserProfileId = @UserProfileId,
                    ExpiresOn = @ExpiresOn,
                    CreatedAt = @CreatedAt,
                    Enabled = @Enabled
                WHERE
                    Id = @Id;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(model.Id > 0? updateSql : insertSql, connection))
                {
                    command.Parameters.AddRange(new []
                        {
                            new SqlParameter("UserProfileId", model.UserProifleId),
                            new SqlParameter("ExpiresOn", model.ExpiresOn),
                            new SqlParameter("CreatedAt", model.CreatedAt),
                            new SqlParameter("Enabled", model.IsEnabled)
                        });

                    if(model.Id != 0)
                    {
                        command.Parameters.AddWithValue("Id", model.Id);
                        command.ExecuteNonQuery();
                    }
                    else
                        model.Id = (long)command.ExecuteScalar();
                }
            }
        }
        #endregion

        #region IAccountRepository Implementation
        public Account GetByUserProfileId(long userProfileId)
        {
            Account ret = null;

            const string sql = @"
                SELECT *
                FROM Accounts
                WHERE UserProfileId = @UserProfileId;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(new []
                    {
                        new SqlParameter("UserProfileId", userProfileId)
                    });

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            ret = new Account
                            {
                                Id = (long)reader["Id"],
                                UserProifleId = userProfileId,
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                ExpiresOn = (DateTime)reader["ExpiresOn"],
                                IsEnabled = (bool)reader["Enabled"]
                            };
                        }
                    }
                }
            }

            return ret;
        }
        #endregion
    }
}
