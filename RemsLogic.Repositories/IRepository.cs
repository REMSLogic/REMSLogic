using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public interface IRepository<TModel>
        where TModel : IEntity
    {
        TModel Get(long id);

        void Save(TModel model);
        void Delete(TModel model);

        IEnumerable<TModel> GetAll();

        IQueryable<TModel> Get(
            Expression<Func<TModel, bool>> filter = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderby = null,
            string includeProperties = "");
    }
}
