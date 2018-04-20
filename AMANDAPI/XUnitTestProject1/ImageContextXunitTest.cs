using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AMANDAPI;
using AMANDAPI.Data;
using AMANDAPI.Models;
using AMANDAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace XUnitTestProject1
{
    /*This is testing the */

    public class ImageContextXunitTest
    {
        [Fact]
        public async void CanReturnJsonObjectAsync()
        {
            var options = new DbContextOptionsBuilder<ImagesContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            var configuration = builder.Build();

            using (var context = new ImagesContext(options))
            {
                var controller = new ImageController(context, configuration);

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

            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            var configuration = builder.Build();

            using (var context = new ImagesContext(options))
            {
                var controller = new ImageController(context, configuration);

                //Act
                var results = controller.GetUrls("cats", 3);

                //Assert
                Assert.IsType<OkObjectResult>(results);
            }
        }

        [Fact]
        public void CanCreateDBEntry()
        {
            var options = new DbContextOptionsBuilder<ImagesContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            var configuration = builder.Build();

            using (var context = new ImagesContext(options))
            {
                var controller = new ImageController(context, configuration);

                //Act
                controller.Create(new Image() { URL = "test Url" });
                Image testImage = context.Images.
                                FirstOrDefaultAsync(test => test.URL == "test Url").Result;

                //Assert
                Assert.Equal("test Url", testImage.URL);
            }
        }

        [Fact]
        public void CanDeleteDBEntry()
        {
            var options = new DbContextOptionsBuilder<ImagesContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            var configuration = builder.Build();

            using (var context = new ImagesContext(options))
            {
                var controller = new ImageController(context, configuration);

                //Act
                controller.Create(new Image() { URL = "test Url" });
                Image testImage = context.Images.
                                FirstOrDefaultAsync(test => test.URL == "test Url").Result;

                //Assert
                Assert.IsType<NoContentResult>(controller.Delete(testImage.Id));
            }
        }
    }
}
