using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public interface IDsqRepository : IRepository<Dsq>
    {
        DsqLink GetLink(long id);

        void SaveLink(DsqLink link);
    }
}
