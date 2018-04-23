using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMANDAPI.Models
{
    /*This is creating a class called Analytics with properties for Keywords("happy") and Sentiment (0.25f) 
     * 
     */

    public class Analytics
    {
        public List<string> Keywords { get; set; }
        public float Sentiment { get; set; }
    }
}
