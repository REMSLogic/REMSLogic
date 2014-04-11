using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RemsLogic.Model;
using RemsLogic.Repositories;

namespace RemsLogic.Respositories.Tests
{
    [TestFixture]
    public class WidgetRepositoryTests
    {
        private IWidgetRepository _widgetRepo;
        private string _connectionString;

        [SetUp]
        public void InitializeValues()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["FDARems"].ConnectionString;
            _widgetRepo = new WidgetRepository(_connectionString);
        }

        [Test]
        public void should_load_all_widgets_for_providers()
        {
            // arrange
            List<Widget> results = null;
            List<string> roles = new List<string>{"view_provider"};

            // act
            results = _widgetRepo.FindByRoles(roles).ToList();

            // assert
            results.Should().NotBeNull();
            results.Count.Should().Be(6);
        }

        [Test]
        public void should_load_all_widgets_for_providers_and_prescribers()
        {
            // arrange
            List<Widget> results = null;
            List<string> roles = new List<string>{"view_provider", "view_prescriber"};

            // act
            results = _widgetRepo.FindByRoles(roles).ToList();

            // assert
            results.Should().NotBeNull();
            results.Count.Should().Be(9);
        }

        [Test]
        public void should_load_widget_settings_for_user_1()
        {
            // arrange
            
            WidgetSettings settings = null;
            const long userId = 1;

            // we need to make sure the record exists.  This is very pport test design.
            // It could be the save that fails.  I really just want to make sure that
            // the communication with the actual database is working.  This is more of
            // an integration test than anything else.
            if(_widgetRepo.FindSettingsByUserId(userId) == null)
                _widgetRepo.Save(new WidgetSettings
                {
                    UserId = 1,
                    Column1 = "3|2",
                    Column2 = "4|4|3"
                });

            // act
            settings = _widgetRepo.FindSettingsByUserId(userId);

            // assert
            settings.Should().NotBeNull();
        }
    }
}
