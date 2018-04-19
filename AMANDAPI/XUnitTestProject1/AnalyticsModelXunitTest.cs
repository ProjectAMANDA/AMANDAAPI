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

        /*This really tests the image model */
        ImagesContext _context;

        public ModelSentimentTest()
        {
            DbContextOptions<ImagesContext> options = new DbContextOptionsBuilder<ImagesContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString())
              .Options;
            _context = new ImagesContext(options);
        }

        [Fact]
        public void GetSentiment()
        {
            Image testSentiment = new Image()
            {
                Sentiment = 0.94f
            };

            Assert.Equal(0.94f, testSentiment.Sentiment);
        }


        [Fact]
        public void GetIDofSentiment()
        {
            Image testId = new Image()
            {
                Id = 1
            };

            Assert.Equal(1, testId.Id);
        }

        [Fact]
        public void ImageTestUrl()
        {
            Image testId = new Image()
            {
                URL = "https://upload.wikimedia.org/wikipedia/commons/3/3b/Coca-cat.jpg"
            };

            Assert.Equal("https://upload.wikimedia.org/wikipedia/commons/3/3b/Coca-cat.jpg", testId.URL);
        }


    }
}
