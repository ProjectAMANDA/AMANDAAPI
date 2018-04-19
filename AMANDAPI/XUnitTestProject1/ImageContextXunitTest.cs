using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AMANDAPI.Data;
using AMANDAPI.Models;
using AMANDAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AMANDAPI;

namespace XUnitTestProject1
{
    public class ImageContextXunitTest
    {
        [Fact]
        public void TestImageCollector()
        {
            var Option = new DbContextOptionsBuilder<ImagesContext>()
                .UseInMemoryDatabase(databaseName: "testingDb")
                .Options;
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<Startup>();
            var Configuration = builder.Build();

            using (var context = new ImagesContext(Option))
            {
                var controller = new ImageController(context, Configuration);
                Image image = new Image();
                image.Sentiment = .1333f;
                image.URL = "https://assets.pokemon.com/assets//cms2/img/play-games/_tiles/alolan_volcanic_panic/alolan-volcanic-panic-169.jpg";

                context.Images.Add(image);
                context.SaveChanges();

                var result = controller.GenerateRecs(".1333", 1).Images;

                Image temp = new Image()
                {
                    URL = "",
                    Sentiment = 1
                };

                foreach (var item in result)
                {
                    temp = item;
                }

                var tc = result;

                Assert.Equal(image.URL, temp.URL);
            }













        }
    }
}
