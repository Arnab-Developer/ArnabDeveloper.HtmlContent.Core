# HTML content

This is a library which takes a collection of HTTP URLs and returns HTML responses of them. Internally
it used `System.Net.WebClient` to get the HTML response of the URLs.

Install from [NuGet](https://www.nuget.org/packages/ArnabDeveloper.HtmlContent.Core).

```
dotnet add package ArnabDeveloper.HtmlContent.Core
```

There are five methods to get the HTML responses.

- `GetContent()` get html contents synchronously
- `GetContentAsync()` get html contents asynchronously
- `GetContentAsyncStream()` get html contents asynchronously but start to return 
contents as they are ready before all are complete
- `GetContentParallelAsync()` get html contents asynchronously in parallel
- `GetContentParallelForEachAsync()` get html contents asynchronously in parallel 
using `Parallel.ForEach()`
- `GetContentParallelForEachProgressAsync()` get html contents asynchronously in 
parallel using `Parallel.ForEach()` and start to return contents as they are ready 
before all are complete with progress data.

Example code to use the library:

```csharp
IHtmlContentService _htmlContentService = new HtmlContentService();

_htmlContentService.Urls.Add("http://google.com");
_htmlContentService.Urls.Add("http://microsoft.com");

IEnumerable<WebSiteDataModel> webSiteDataModels = _htmlContentService.GetContent();

foreach (WebSiteDataModel webSiteDataModel in webSiteDataModels)
{
    Console.WriteLine(webSiteDataModel.WebsiteUrl);
    Console.WriteLine(webSiteDataModel.WebsiteData);
}
```

This is influenced by
[C# Advanced Async by TimCorey](https://www.youtube.com/watch?v=ZTKGRJy5P2M)