﻿using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RemsLogic.Model.UI;
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
            results.Count.Should().BeGreaterThan(0);
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
            results.Count.Should().BeGreaterThan(0);
        }
    }
}
