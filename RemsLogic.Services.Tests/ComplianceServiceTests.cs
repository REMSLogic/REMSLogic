using System;
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
                new Eoc{Id = 1,Name = "Eoc 1"},
                new Eoc{Id = 2,Name = "Eoc 2"}
            };

            List<PrescriberEoc> savedPrescriberEocs = new List<PrescriberEoc>();

            _complianceRepo.Stub(x => x.GetByDrug(Arg<long>.Is.Anything)).Return(expectedEocs);
            _complianceRepo.Stub(x => x.Find(0,0,0)).IgnoreArguments().Return(null);
            _complianceRepo.Stub(x => x.Save(Arg<PrescriberEoc>.Is.Anything))
                .WhenCalled(x => savedPrescriberEocs.Add((PrescriberEoc)x.Arguments.First()));

            // act
            _complianceSvc.AddEocsToPrescriberProfile(-1, -1);

            // assert
            savedPrescriberEocs.Count.Should().Be(2);
            savedPrescriberEocs[1].EocId.Should().Be(2);
        }

        [Test]
        public void should_call_LogEocComplianceEntry()
        {
            // arrange
            PrescriberEoc expectedEoc = new PrescriberEoc
            {
                Id = 1,
                PrescriberProfileId = 1,
                DrugId = 1,
                EocId = 1,
                CompletedAt = DateTime.Now
            };

            PrescriberEocLogEntry logEntry = new PrescriberEocLogEntry();

            _complianceRepo.Stub(x => x.Find(0,0,0)).IgnoreArguments().Return(expectedEoc);
            _complianceRepo.Stub(x => x.Save(null)).IgnoreArguments();
            _complianceRepo.Stub(x => x.LogEocComplianceEntry(0, DateTime.Now))
                .IgnoreArguments()
                .WhenCalled(x =>
                    {
                        logEntry.PrescriberEocId = (long)x.Arguments[0];
                        logEntry.RecordedAt = (DateTime)x.Arguments[1];
                    });

            // act
            _complianceSvc.RecordCompliance(expectedEoc);

            // assert
            logEntry.PrescriberEocId.Should().Be(expectedEoc.Id);
            logEntry.RecordedAt.Should().Be(expectedEoc.CompletedAt.Value);
        }
    }
}
