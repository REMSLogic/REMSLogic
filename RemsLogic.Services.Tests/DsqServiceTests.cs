using System;
using System.Collections.Generic;
using NUnit.Framework;
using RemsLogic.Model.Dsq;
using RemsLogic.Repositories;
using Rhino.Mocks;

namespace RemsLogic.Services.Tests
{
    [TestFixture]
    public class DsqServiceTests
    {
        private IDsqRepository _dsqRepo;
        private IDsqService _dsqService;

        private DsqLink _link;

        [SetUp]
        public void SetupTestObjects()
        {
            _dsqRepo = MockRepository.GenerateMock<IDsqRepository>();
            _dsqService = new DsqService(_dsqRepo);

            _link = new DsqLink
            {
                Date = DateTime.Now,
                DrugId = 1,
                EocId = 1,
                HelpText = "Help Text",
                Label = "Label",
                QuestionId = 1,
                Value = "Test"
            };
        }

        [Test]
        public void should_call_repository_save_method()
        {
            // Arrange
            
            _dsqRepo.Expect(x => x.SaveLink(_link));
            _dsqRepo.Expect(x => x.GetLinks(0,0)).IgnoreArguments().Return(new List<DsqLink>());

            // Act
            _dsqService.SaveLink(_link);

            // Assert
            _dsqRepo.VerifyAllExpectations();
        }

        [Test]
        public void should_get_link_by_id_from_repo()
        {
            // Arrange
            _dsqRepo.Expect(x => x.GetLink(Arg<int>.Is.Anything));

            // Act
            _dsqService.GetLink(1);

            // Assert
            _dsqRepo.VerifyAllExpectations();
        }
    }
}
