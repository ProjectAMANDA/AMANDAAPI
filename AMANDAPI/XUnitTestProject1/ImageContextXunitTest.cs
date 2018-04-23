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
    public class ImageContextXunitTest
    {
        [Fact]
        public async void CanReturnBingSearch()
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

        [Theory]
        [InlineData("cats", 3)]
        [InlineData("0.5", 3)]
        [InlineData("cats", 9)]
        [InlineData("0.5", -1)]
        [InlineData("1.4", 3)]
        public void CanGenerateURLs( string query, int numResults)
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
                var results = controller.GetUrls(query, numResults);

                //Assert
                Assert.IsType<OkObjectResult>(results);
            }
        }

        // Create tests
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
        public void FailDBEntry()
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

                //Assert
                Assert.IsType<BadRequestResult>(controller.Create(null));
            }
        }
        // Delete tests
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

        [Fact]
        public void FailToDelete()
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
                Assert.IsType<NotFoundResult>(controller.Delete(-1));
            }
        }
    }
}
