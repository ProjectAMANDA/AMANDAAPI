using AMANDAPI;
using AMANDAPI.Controllers;
using AMANDAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace ImageControllerTest
{
    public class UnitTest1
    {
        [Fact]
        public async void CanReturnJsonObjectAsync()
        {
            var options = new DbContextOptionsBuilder<ImagesContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<Startup>();
            var Configuration = builder.Build();
            using (var context = new ImagesContext(options))
            {
                var controller = new ImageController(context, Configuration);

                //Act
                var results = await controller.BingSearch("cats");

                //Assert
                Assert.IsAssignableFrom<IEnumerable>(results);
            }
        }

        [Fact]
        public void CanConsumeBingSearch()
        {
            var options = new DbContextOptionsBuilder<ImagesContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<Startup>();
            var Configuration = builder.Build();
            using (var context = new ImagesContext(options))
            {
                var controller = new ImageController(context, Configuration);

                //Act
                //var results = controller.GetUrls("cats", "False", "3");

                //Assert
               // Assert.IsAssignableFrom<IEnumerable>(results);
            }
        }
    }
}
