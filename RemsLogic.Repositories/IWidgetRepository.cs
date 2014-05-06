using System.Collections.Generic;
using RemsLogic.Model.UI;

namespace RemsLogic.Repositories
{
    public interface IWidgetRepository : IRepository<Widget>
    {
        void Save(WidgetSettings settings);
        WidgetSettings FindSettingsByUserId(long userId);
        

        IEnumerable<Widget> FindByRoles(IEnumerable<string> roles);
    }
}
