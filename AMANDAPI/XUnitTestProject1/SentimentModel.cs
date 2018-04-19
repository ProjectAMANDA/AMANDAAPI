using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AMANDAPI.Data;
using AMANDAPI.Models;
using AMANDAPI.Controllers;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestProject1
{
  public  class SentimentModel
    {
        ImagesContext _context;

        public SentimentModel()
        {
            DbContextOptions<ImagesContext> options = new DbContextOptionsBuilder<ImagesContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString())
              .Options;
            _context = new ImagesContext(options);
        }

        [Fact]
        public void GetSentimentValue()
        {
            Image testSentiment = new Image()
            {
                URL ="https://www.purina.com/sites/g/files/auxxlc196/files/HOUND_Beagle-%2813inch%29.jpg",
                Sentiment = 0f
            };

            Assert.Equal(0f, testSentiment.Sentiment);
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

    }
}
