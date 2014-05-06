using System.Linq;
using RemsLogic.Model.Dsq;
using RemsLogic.Repositories;

namespace RemsLogic.Services
{
    public class DsqService : IDsqService
    {
        private readonly IDsqRepository _dsqRepo;

        public DsqService(
            IDsqRepository dsqRepo)
        {
            _dsqRepo = dsqRepo;
        }

        public DsqLink GetLink(long id)
        {
            return _dsqRepo.GetLink(id);
        }

        public void SaveLink(DsqLink link)
        {
            // save the link
            _dsqRepo.SaveLink(link);

            // now we need to update the eoc status of the question.  first, we just
            // clear any existing value.
            _dsqRepo.DeleteEoc(link.DrugId, link.QuestionId);

            // now we'll see if there is a need to set value by loading all of the links
            // for the question and seeing if any of them have a required eoc.
            long eocId = (
                from l in _dsqRepo.GetLinks(link.DrugId, link.QuestionId)
                where l.IsRequired && l.EocId > 0
                select l.EocId).FirstOrDefault();

            // if eocId > 0, then we need to indicate that this question has an EOC.
            _dsqRepo.AddEoc(new DsqEoc
            {
                DrugId = link.DrugId,
                QuestionId = link.QuestionId,
                EocId = eocId,
            });
        }
    }
}
