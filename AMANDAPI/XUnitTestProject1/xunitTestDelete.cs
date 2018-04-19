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
        /*
        [Fact]
        public void TestDeleteMethod()
        {
            var Options = new DbContextOptionsBuilder<ImagesContext>()
                .UseInMemoryDatabase(databaseName: "testing_database")
                .Options;

            using (var context = new ImagesContext(Option))
            {
                //
                var controller = new ImageController(context);
                Image image = new Image();
                image.Id = 1;
                image.Sentiment = .40f;
                image.URL = "https://www.purina.com/sites/g/files/auxxlc196/files/HOUND_Beagle-%2813inch%29.jpg";

                context.Images.Add(image);
                context.SaveChanges();

                //act
                var results = controller.Delete("https://www.purina.com/sites/g/files/auxxlc196/files/HOUND_Beagle-%2813inch%29.jpg");
            

            }

        }
        */



    }
}
