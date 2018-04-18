using System;
using Xunit;
using AMANDAPI.Data;
using AMANDAPI.Models;
using AMANDAPI.Controllers;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestProject1
{
    public class ModelSentimentTest
    {
        ImagesContext _context;

        public ModelSentimentTest()
        {
            DbContextOptions<ImagesContext> options = new DbContextOptionsBuilder<ImagesContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString())
              .Options;
            _context = new ImagesContext(options);
        }


        [Fact]
        public void GetId()
        {
            Image testsntiment = new Image()
            {
                Sentiment = "0.943"
            };

            Assert.Equal("0.943", testsntiment.Sentiment);

        }




    }
}
