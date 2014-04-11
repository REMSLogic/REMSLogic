using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public class WidgetRepository : Repository<Widget>, IWidgetRepository
    {
        #region Constructor
        public WidgetRepository(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        #region IWidgetRepository Implementation
        public void Save(WidgetSettings settings)
        {

        }

        public WidgetSettings FindSettingsByUserId(long userId)
        {
            string sql = @"
                SELECT *
                FROM UserWidgetSettings
                WHERE UserId = "+userId;

            using(SqlConnection connection = new SqlConnection(ConnectinString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            return new WidgetSettings
                            {
                                Id = (long)reader["Id"],
                                Userid = (long)reader["UserId"],
                                Column1 = (string)reader["Column1"],
                                Column2 = (string)reader["Column2"]
                            };
                        }
                        
                        return null;
                    }
                }
            }
        }

        public IEnumerable<Widget> FindByRoles(IEnumerable<string> roles)
        {
            string sql = @"
                SELECT *
                FROM Widgets
                WHERE ";

            string query = String.Format("{0}{1}", 
                sql, 
                roles
                    .Select(r => String.Format(" Roles LIKE '%{0}%'", r) )
                    .Aggregate((i,j) => i+" OR "+j));

            using(SqlConnection connection = new SqlConnection(ConnectinString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            yield return new Widget
                            {
                                Id = (long)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Location = reader["Location"].ToString()
                            };
                        }
                    }
                }
            }
        }
        #endregion
    }
}
