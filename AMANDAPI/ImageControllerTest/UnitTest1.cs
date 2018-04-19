using AMANDAPI.Controllers;
using AMANDAPI.Data;
using Microsoft.EntityFrameworkCore;
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
            using (var context = new ImagesContext(options))
            {
                var controller = new ImageController(context);

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
            using (var context = new ImagesContext(options))
            {
                var controller = new ImageController(context);

                //Act
                //var results = controller.GetUrls("cats", "False", "3");

                //Assert
               // Assert.IsAssignableFrom<IEnumerable>(results);
            }
        }
    }
}