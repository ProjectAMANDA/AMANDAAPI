
# Project AMANDA API
-----
**Authors**:
Kevin Farrow <br>
Brent Williams <br>
Tiger Hsu <br>
**Version**: 1.0.0

## Overview
-----
The custom API will handle the calls to the Azure language analysis API that will return the sentiment and key words of a blog post. It will also store a series of sanitized images that can be used to match both keywords and sentiment of the blog posts from the web app. The custom API can be scaled to utilize the Bing search API to return a series of more random but possible less safe images. These images can be used with the set already stored to provider a wider range of matches for blog posts.


## Use
There are two endpoints for the user to hit with http get requests. To utilize our full service hit the analysis endpoint:
```
/api/analytics/{useSentiment}/{numberOfResponses}
```
You also will need to put a 'text' key/value pair in the header of your request. The text that holds will be the text analyzed.
#### Parameters
All parameters at theis point have default values if excluded.
- useSentiment: Default: `true`. If `true`, your reccomendations will be based on sentiment. For any other value, your reccomendations will be based on keywords found by our analytics.
- numberOfResponses: Default: `3`. This is a numeric integer specifying the number of reccomended images you would like to recieve. Maximum of 6, minmum 1.

Alternatively, if you have done your own analytics and just want to consume our image recommendation, you can hit that directly.
```
/api/analytics/{data}/{numberOfResponses}
```
#### Parameters
- data: this is the item your image recommendations will be based upon. If you want one based on sentiment, input something like `0.4321` - a number between 0 and 1. Otherwise, if you have a keyphrase or word for which you would like a reccommeded image, you can simply put that in instead. If your input cannot be converted to a floating point number, we will use it as a key phrase search instead. Be aware that we will attempt to filter for safe images, but we are not perfect.
- numberOfResponses: Default: `3`. This is a numeric integer specifying the number of reccomended images you would like to recieve. Maximum of 6, minmum 1.

## Json Example
`/api/analytics/rue/2`
-----
```
{
    "rec": [
        {
            "id": 0,
            "url": "https://tse4.mm.bing.net/th?id=OIP.XpkOCKKcJd-KjGQ8c5QEEAHaEW&pid=Api",
            "sentiment": 0
        },
        {
            "id": 0,
            "url": "https://tse1.mm.bing.net/th?id=OIP.wkVL8_SNmrryRZdHQnYOLwHaE6&pid=Api",
            "sentiment": 0
        }
    ],
    "bySeniment": false,
    "sentim": -1,
    "keyword": "high code coverage"
}
```

## Frameworks & Dependencies
-----
- Entity Framework Core
- ASP.NET Core
- Swashbuckle
- Xunit
- C# ASP.NET Core MVC Application
- Microsoft Azure Services

## Resources
-----
- JSON Editor https://jsoneditoronline.org/
- API Documentation
https://azure.microsoft.com/en-us/services/cognitive-services/bing-image-search-api/
https://dev.cognitive.microsoft.com/docs/services/8336afba49a84475ba401758c0dbf749/operations/56b4433fcf5ff8098cef380c
https://azure.microsoft.com/en-us/services/cognitive-services/text-analytics/
- Additional Resources
https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=visual-studio
https://blogs.msdn.microsoft.com/mihansen/2017/09/10/managing-secrets-in-net-core-2-0-apps/
