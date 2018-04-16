using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMANDAPI.Models
{
    /*
     * 
     */

    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Tags { get; set; }
        public string Sentiment { get; set; }
        public string Source { get; set; }
    }
}
