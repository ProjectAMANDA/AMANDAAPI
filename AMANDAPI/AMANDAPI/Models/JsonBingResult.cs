using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMANDAPI.Models
{
    public class JsonBingResult
    {
        public class Instrumentation
{
    public string _type { get; set; }
}

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

public class Thumbnail2
{
    public string thumbnailUrl { get; set; }
}

public class QueryExpansion
{
    public string text { get; set; }
    public string displayText { get; set; }
    public string webSearchUrl { get; set; }
    public string searchLink { get; set; }
    public Thumbnail2 thumbnail { get; set; }
}

public class Thumbnail3
{
    public string thumbnailUrl { get; set; }
}

public class Suggestion
{
    public string text { get; set; }
    public string displayText { get; set; }
    public string webSearchUrl { get; set; }
    public string searchLink { get; set; }
    public Thumbnail3 thumbnail { get; set; }
}

public class PivotSuggestion
{
    public string pivot { get; set; }
    public List<Suggestion> suggestions { get; set; }
}

public class Thumbnail4
{
    public string url { get; set; }
}

public class SimilarTerm
{
    public string text { get; set; }
    public string displayText { get; set; }
    public string webSearchUrl { get; set; }
    public Thumbnail4 thumbnail { get; set; }
}

public class Thumbnail5
{
    public string thumbnailUrl { get; set; }
}

public class RelatedSearch
{
    public string text { get; set; }
    public string displayText { get; set; }
    public string webSearchUrl { get; set; }
    public string searchLink { get; set; }
    public Thumbnail5 thumbnail { get; set; }
}

public class RootObject
{
    public string _type { get; set; }
    public Instrumentation instrumentation { get; set; }
    public string readLink { get; set; }
    public string webSearchUrl { get; set; }
    public int totalEstimatedMatches { get; set; }
    public int nextOffset { get; set; }
    public List<Value> value { get; set; }
    public List<QueryExpansion> queryExpansions { get; set; }
    public List<PivotSuggestion> pivotSuggestions { get; set; }
    public List<SimilarTerm> similarTerms { get; set; }
    public List<RelatedSearch> relatedSearches { get; set; }
}
    }
}
