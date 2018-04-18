using System;
using Xunit;
using AMANDAPI.Data;
using AMANDAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using AMANDAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;

namespace XUnitTestProject1
{
   public class StatusCodeXunitTest
    {
        [Fact]
        public void TestStatusCode()
        {
            var Options = new DbContextOptionsBuilder<ImagesContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            using (var context = new ImagesContext(Options))
            {
                var controller = new ImageController (context);

                Image image = new Image();
                image.Sentiment = ".301";
                image.URL = "https://www.purina.com/sites/g/files/auxxlc196/files/HOUND_Beagle-%2813inch%29.jpg";

                context.Images.Add(image);
                context.SaveChanges();
                //act
                  var result = controller.GetUrls(".314", "true", "1");
                string temp = "";
                foreach (var item in result)
                {
                    temp = item;
                }
                 var sc = result;




                Assert.Equal(image.URL, temp );
               //Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)sc.StatusCode.Value);

            }

            //create two status code ojbects  to each other 
        }

    }
}
