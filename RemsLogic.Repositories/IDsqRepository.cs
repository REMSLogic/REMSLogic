using System.Collections.Generic;
using RemsLogic.Model.Dsq;

namespace RemsLogic.Repositories
{
    public interface IDsqRepository : IRepository<Questionnaire>
    {
        // DSQ_Eocs
        void AddEoc(DsqEoc eoc);
        void DeleteEoc(long drugId, long questionId);
        
        // DSQ_Links
        DsqLink GetLink(long id);
        IEnumerable<DsqLink> GetLinks(long drugId, long questionid);

        void SaveLink(DsqLink link);
    }
}
