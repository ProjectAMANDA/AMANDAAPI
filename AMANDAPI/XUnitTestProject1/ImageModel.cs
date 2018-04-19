using System;
using Xunit;
using AMANDAPI.Data;
using AMANDAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestProject1
{
    public class ImageModelXunitTesting
    {

        // Testing Models.ImageModel(true)

        ImagesContext _context;

        public ImageModelXunitTesting()
        {
            DbContextOptions<ImagesContext> options = new DbContextOptionsBuilder<ImagesContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString())
              .Options;
            _context = new ImagesContext(options);
        }

        //Testing Models.ImageModel Sentiment Property

        [Fact]
        public void GetSentiment()
        {
            Image testSentiment = new Image()
            {
                Sentiment = 0.94f
            };

            Assert.Equal(0.94f, testSentiment.Sentiment);
        }

        //Testing Models.ImageModel Sentiment ID
        [Fact]
        public void GetIDofSentiment()
        {
            Image testId = new Image()
            {
                Id = 1
            };

            Assert.Equal(1, testId.Id);
        }

        //Testing Models.ImageModel Property URL

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
