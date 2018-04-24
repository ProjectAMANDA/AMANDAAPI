using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AMANDAPI.Models;
using AMANDAPI.Data;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.AspNetCore.Routing;
using AMANDAPI.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Xunit;
using AMANDAPI;
using System.Collections;
using Microsoft.Extensions.Options;

namespace XUnitTestProject1
{
  
    public class AnalyticsControllerXunitTest
    {
        // This tests the Analyze method hits the GetText ; and part of the analytics.Analyze
    
        [Fact]
        public async void AnalyticsControllerXunitGetText()
        {
            var options = new DbContextOptionsBuilder<ImagesContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            var configuration = builder.Build();

            using (var context = new ImagesContext(options))
            {
                var controller = new AnalyticsController( configuration);

                //Act
                var results = controller.GetText("true");

                //Assert
                Assert.IsNotType<Analytics>(results);
            }
        }

        [Fact]
        public void TestingCanAnalyze()
        {
            var options = new DbContextOptionsBuilder<ImagesContext>()
               .UseInMemoryDatabase(databaseName: "testDb")
               .Options;

            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            var configuration = builder.Build();
            using (var context = new ImagesContext(options))

            {
                var controller = new AnalyticsController( configuration);

                //Act
                var results = controller.Analyze("hello from the past this is me");

                //Assert
                Assert.IsType<Analytics>(results);
            }

        }

      /*
        [Fact]
        public void GetContextnumber()
        {
            var options = new DbContextOptionsBuilder<ImagesContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            var configuration = builder.Build();

            using (ImagesContext _context = new ImagesContext(options))
            {
                AnalyticsController controller = new AnalyticsController(_context);
                int tableCount = controller.GetHashCode().Count();
                Assert.Equal(6, tableCount);
            }
        }
        */

    }
}
