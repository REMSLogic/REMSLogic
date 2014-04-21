using System.Data.SqlClient;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public class CompliancyRepository : Repository<EocRequirement>, IRepository<EocRequirement>
    {
        #region Constructor
        public CompliancyRepository(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        #region IRepository Implementation
        public override void Save(EocRequirement model)
        {
            string sql;

            if(model.Id == 0)
            {
                sql = @"
                    INSERT INTO UserEoc
                        (ProfileId, DrugId, EocId, DateCompleted, Deleted)
                    VALUES
                        (@ProfileId, @DrugId, @EoczId, @DateCompleted, @Deleted)";
            }
            else
            {
                sql = @"
                    UPDATE UserEoc
                        SET 
                            ProfileId = @ProfileId,
                            DrugId = @DrugId,
                            EocId = @EocId,
                            DateCompleted = @DateCompleted,
                            Deleted = @Deleted;";
            }

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProfileId", model.ProfileId);
                    command.Parameters.AddWithValue("@DrugId", model.DrugId);
                    command.Parameters.AddWithValue("@EocId", model.EocId);
                    command.Parameters.AddWithValue("@DateCompleted", model.CompletedAt);
                    command.Parameters.AddWithValue("@Deleted", model.Deleted);

                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }
}
