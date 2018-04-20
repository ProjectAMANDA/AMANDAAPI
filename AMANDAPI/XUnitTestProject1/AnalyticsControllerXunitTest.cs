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

namespace XUnitTestProject1
{
    //needs to touch Analyze string
    // needs to touch Anaytics controller
    // needs to touch get text 
    public class AnalyticsControllerXunitTest
    {
        [Fact]
        public async void AnalyticsControllerXunitTest01()
        {
            var options = new DbContextOptionsBuilder<ImagesContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            var configuration = builder.Build();

            using (var context = new ImagesContext(options))
            {
                var controller = new AnalyticsController(context, configuration);

                //Act
                var results = controller.Analyze("Little Baby foo foo");


                //Assert
                Assert.IsType<Analytics>(results);

            }


        }
    }
}
