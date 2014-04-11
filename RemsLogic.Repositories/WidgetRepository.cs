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
