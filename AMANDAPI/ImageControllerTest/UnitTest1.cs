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
        public async System.Threading.Tasks.Task CanReturnJsonObjectAsync()
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
    }
}