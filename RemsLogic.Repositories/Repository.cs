using System;
using System.Linq;
using System.Linq.Expressions;

using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public abstract class Repository<TModel> : IRepository<TModel>
        where TModel : IEntity
    {
        #region Properties
        protected string ConnectinString {get; private set;}
        #endregion

        #region Constructor
        public Repository(string connectionString)
        {
            ConnectinString = connectionString;
        }
        #endregion

        #region IRespository Implementation
        public TModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(TModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(TModel model)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<TModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TModel> Get(
            Expression<Func<TModel, bool>> filter = null, 
            Func<IQueryable<TModel>, 
                IOrderedQueryable<TModel>> orderby = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
