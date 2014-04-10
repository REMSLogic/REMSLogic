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
    }
}
