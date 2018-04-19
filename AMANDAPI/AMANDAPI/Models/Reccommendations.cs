using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AMANDAPI.Models
{
    public class Reccommendations
    {
        public IEnumerable<Image> Images { get; set; }
        public bool UseSentiment { get; set; }
        public float Sentiment { get; set; }
        public string KeyPhrase { get; set; }
    }
}
