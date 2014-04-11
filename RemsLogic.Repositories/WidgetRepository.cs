using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
            StringBuilder sql = new StringBuilder();

            if(settings.Id == 0)
            {
                sql.Append("INSERT INTO UserWidgetSettings (UserId, Column1, Column2) ");
                sql.Append("VALUES ("+settings.UserId+",'"+settings.Column1+"','"+settings.Column2+"');");
            }
            else
            {
                sql.Append("UPDATE UserWidgetSettings ");
                sql.Append("SET Column1='"+settings.Column1+"', Column2='"+settings.Column2+"' ");
                sql.Append("WHERE UserId="+settings.UserId);
            }

            using(SqlConnection connection = new SqlConnection(ConnectinString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql.ToString(), connection))
                {
                    command.ExecuteNonQuery();
                }
            }
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
                                UserId = (long)reader["UserId"],
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
