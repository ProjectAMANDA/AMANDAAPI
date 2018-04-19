using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AMANDAPI.Controllers;
using AMANDAPI.Data;
using AMANDAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestProject1
{
   public class RecomendationsXunitTest
   {
        [Fact]
        public void ImgesSet()
        {
            //arrage
            Reccommendations fc = new Reccommendations
            {
                UseSentiment = true
            };

            //assert
            Assert.True(fc.UseSentiment);
        }




        ImagesContext _context;
        /*
        [Fact]
        public void ModelSentimentTest()
        {
            DbContextOptions<ImagesContext> options = new DbContextOptionsBuilder<ImagesContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString())
              .Options;
            _context = new ImagesContext(options);
        }*/

        /*
        [Fact]
        public void GetSentimentFromRecs()
        {
            Image testingSentiment = new Reccommendations()
            {
                UseSentiment = true,
                Sentiment = 0.65f,
                KeyPhrase = "happy",
            };

            Assert.Equal(0.65f)


        }
        */

           









        



    }
}
