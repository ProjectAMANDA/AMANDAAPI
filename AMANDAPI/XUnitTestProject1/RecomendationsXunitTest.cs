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

        // Testing Models.Recomndations.UseSentiment(true)
        [Fact]
        public void ImgesSetReturnsBoolTrue()
        {
            //arrage
            Reccommendations fc = new Reccommendations
            {
                UseSentiment = true
            };

            //assert
            Assert.True(fc.UseSentiment);
        }

        // Testing Models.Recomndations.UseSentiment(false)
       [Fact]
        public void ImgesSetReturnsBoolFalse()
        {
            //arrage
            Reccommendations fc = new Reccommendations
            {
                UseSentiment = false
            };

            //assert
            Assert.False(fc.UseSentiment);
        }

        // Testing Models.Recomndations.Sentiment(float)
        [Fact]
        public void UseSentiment()
        {
            //arrange
            Reccommendations Img = new Reccommendations
            {
                Sentiment = .01f
            };

            //assert
            Assert.Equal(.01f, Img.Sentiment);
        }

        [Fact]
        public void UseKeyPhrase()
        {
            //arrange
            Reccommendations testPhrase = new Reccommendations
            {
                KeyPhrase = "Happy"
            };

            //assert
            Assert.Equal("Happy", testPhrase.KeyPhrase);
        }




        












    }
}
