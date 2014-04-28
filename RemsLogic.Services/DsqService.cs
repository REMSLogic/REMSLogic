using System;
using RemsLogic.Model;
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

        public void UpdateLink(DsqLink link)
        {
            throw new NotImplementedException();
        }
    }
}
