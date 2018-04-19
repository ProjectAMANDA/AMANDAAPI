using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AMANDAPI;
using Microsoft.EntityFrameworkCore;
using AMANDAPI.Controllers;
using AMANDAPI.Models;


namespace XUnitTestProjectModels
{
    //Testing Models.Analytics
    public class AnalyticsModel
    {
        [Fact]
        public void canFloatSentimentAnalytics()
        {
            //arrrange
            Analytics pc = new Analytics
            {
                Sentiment = .01f
            };

            //assert
            Assert.Equal(.01f, pc.Sentiment);
        }

        [Fact]
        public void AnalyticsKeywords()
        {

            //arrange
            Analytics pc = new Analytics
            {
                 Keywords = new List<string> {"hello", "blue"}
            };

            //assert
            Assert.Contains("hello", pc.Keywords);

        }
    }
}
