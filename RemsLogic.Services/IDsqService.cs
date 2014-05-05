using RemsLogic.Model.Dsq;

namespace RemsLogic.Services
{
    public interface IDsqService
    {
        DsqLink GetLink(long id);
        void SaveLink(DsqLink link);
    }
}
