using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMANDAPI.Models
{
    /*This is creating a Image Class which has the properties of Id(num), URL(bing.com), and Sentiment(.09f)
     * 
     */

    public class Image
    {
        public Image()
        {
            URL = "";
            Sentiment = 0;
        }
        public Image (string url)
        {
            URL = url;
            Sentiment = 0;
        }
        public int Id { get; set; }
        public string URL { get; set; }
        public float Sentiment { get; set; }
    }
}
