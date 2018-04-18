using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMANDAPI.Models
{
    public class CleanVAlue
    {
        public class Thumbnail
        {
            public int width { get; set; }
            public int height { get; set; }
        }

        public class BestRepresentativeQuery
        {
            public string text { get; set; }
            public string displayText { get; set; }
            public string webSearchUrl { get; set; }
        }

        public class InsightsMetadata
        {
            public int recipeSourcesCount { get; set; }
            public BestRepresentativeQuery bestRepresentativeQuery { get; set; }
            public int pagesIncludingCount { get; set; }
            public int availableSizesCount { get; set; }
        }

        public class Value
        {
            public string webSearchUrl { get; set; }
            public string name { get; set; }
            public string thumbnailUrl { get; set; }
            public DateTime datePublished { get; set; }
            public string contentUrl { get; set; }
            public string hostPageUrl { get; set; }
            public string contentSize { get; set; }
            public string encodingFormat { get; set; }
            public string hostPageDisplayUrl { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public Thumbnail thumbnail { get; set; }
            public string imageInsightsToken { get; set; }
            public InsightsMetadata insightsMetadata { get; set; }
            public string imageId { get; set; }
            public string accentColor { get; set; }
        }
    }
}
