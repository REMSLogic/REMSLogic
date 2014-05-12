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
        protected string ConnectionString {get; private set;}
        #endregion

        #region Constructor
        public Repository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion

        #region IRespository Implementation
        public virtual TModel Get(long id)
        {
            throw new NotImplementedException();
        }

        public virtual void Save(TModel model)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public virtual System.Collections.Generic.IEnumerable<TModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TModel> Get(
            Expression<Func<TModel, bool>> filter = null, 
            Func<IQueryable<TModel>, 
                IOrderedQueryable<TModel>> orderby = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
