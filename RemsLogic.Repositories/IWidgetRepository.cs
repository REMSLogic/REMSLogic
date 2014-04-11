using System.Collections.Generic;
using RemsLogic.Model;

namespace RemsLogic.Repositories
{
    public interface IWidgetRepository : IRepository<Widget>
    {
        IEnumerable<Widget> FindByRoles(IEnumerable<string> roles);
    }
}
