using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RemsLogic.Model;
using RemsLogic.Repositories;
using Rhino.Mocks;

namespace RemsLogic.Services.Tests
{
    [TestFixture]
    public class ComplianceServiceTests
    {
        private MockRepository _mock;
        private IComplianceRepository _complianceRepo;
        private IDrugRepository _drugRepo;
        private IComplianceService _complianceSvc;

        [SetUp]
        public void SetupValues()
        {
            _complianceRepo = MockRepository.GenerateStub<IComplianceRepository>();
            _drugRepo = MockRepository.GenerateStub<IDrugRepository>();

            _complianceSvc = new ComplianceService(_drugRepo, _complianceRepo);
        }

        [Test]
        public void should_save_eocs_for_prescriber()
        {
            // arrange
            List<Eoc> expectedEocs = new List<Eoc>
            {
                new Eoc{Id = 1,Name = "Eoc 1",},
                new Eoc{Id = 2,Name = "Eoc 2"}
            };

            List<PrescriberEoc> savedPrescriberEocs = new List<PrescriberEoc>();

            _complianceRepo.Stub(x => x.GetByDrugId(Arg<long>.Is.Anything)).Return(expectedEocs);
            _complianceRepo.Stub(x => x.Find(0,0,0)).IgnoreArguments().Return(null);
            _complianceRepo.Stub(x => x.Save(Arg<PrescriberEoc>.Is.Anything))
                .WhenCalled(x => savedPrescriberEocs.Add((PrescriberEoc)x.Arguments.First()));

            // act
            _complianceSvc.AddEocsToPrescriberProfile(-1, -1);

            // assert
            savedPrescriberEocs.Count.Should().Be(2);
            savedPrescriberEocs[1].EocId.Should().Be(2);
        }
    }
}
