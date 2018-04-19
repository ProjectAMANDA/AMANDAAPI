using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AMANDAPI.Controllers;
using AMANDAPI.Models;
using AMANDAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Options;

namespace XUnitTestProject1
{
    public class xunitTestDelete
    {

        [Fact]
        public void TestImageCollector()
        {
            var Option = new DbContextOptionsBuilder<ImagesContext>()
                .UseInMemoryDatabase(databaseName: "testingDb")
                .Options;


            using (var context = new ImagesContext(Option))
            {
                //arrange

                var controller = new ImageController(context);
                Image image = new Image();
                image.Sentiment = .1333f;
                image.URL = "https://assets.pokemon.com/assets//cms2/img/play-games/_tiles/alolan_volcanic_panic/alolan-volcanic-panic-169.jpg";

                context.Images.Add(image);
                context.SaveChanges();

                var result = controller.GenerateRecs(".1333", 1).Images;
                Image temp = new Image()
                {
                    Id = 1,
                    URL = "",
                    Sentiment = 1,
                };

                foreach (var item in result)
                {
                    temp = item;                    
                }


                //Not removing at this point, need to movie id =1 
               // var DeleteResult = controller.Delete(temp.Id);
                var tc = result;

                var RemoveResult = controller.Delete(1);

                /*var result2 = controller.GenerateRecs(".1333", 1).Images;

                foreach (var item in result2)
                {
                    temp = item;
                }
                */

                var DeleteResult = controller.Delete(temp.Id);

                Assert.NotEqual(temp, image);
            }




        }
    }
}
