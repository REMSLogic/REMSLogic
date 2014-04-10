namespace RemsLogic.Repositories
{
    public class WidgetRepository : IWidgetRepository
    {
        #region IWidgetRepository Implementation
        public Model.Widget Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save(Model.Widget model)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Model.Widget model)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<Model.Widget> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public System.Linq.IQueryable<Model.Widget> Get(System.Linq.Expressions.Expression<System.Func<Model.Widget, bool>> filter = null, System.Func<System.Linq.IQueryable<Model.Widget>, System.Linq.IOrderedQueryable<Model.Widget>> orderby = null, string includeProperties = "")
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
