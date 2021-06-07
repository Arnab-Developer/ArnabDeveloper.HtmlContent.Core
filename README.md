# HTML content

[![CI CD](https://github.com/Arnab-Developer/ArnabDeveloper.HtmlContent.Core/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/ArnabDeveloper.HtmlContent.Core/actions/workflows/ci-cd.yml)
![Nuget](https://img.shields.io/nuget/v/ArnabDeveloper.HtmlContent.Core)

It takes a collection of HTTP URLs and returns HTML responses of them. Internally
it used `System.Net.WebClient` to get the HTML response of the URLs.

This is influenced by
[C# Advanced Async by TimCorey](https://www.youtube.com/watch?v=ZTKGRJy5P2M)

## How to install

This library has been hosted in 
[NuGet](https://www.nuget.org/packages/ArnabDeveloper.HtmlContent.Core). 
Use below command to install this in your .NET application.

```
dotnet add package ArnabDeveloper.HtmlContent.Core
```

## How to use

There are five methods to get the HTML responses.

- `GetContent()` get html contents synchronously
- `GetContentAsync()` get html contents asynchronously
- `GetContentAsyncStream()` get html contents asynchronously but start to return contents as they are ready before all are complete
- `GetContentParallelAsync()` get html contents asynchronously in parallel
- `GetContentParallelForEachAsync()` get html contents asynchronously in parallel using `Parallel.ForEach()`
- `GetContentParallelForEachProgressAsync()` get html contents asynchronously in parallel using 'Parallel.ForEach()' and start to return contents as they are ready before all are complete with progress data.

## Tech stack

- Visual Studio 2019
- .NET 5
- xUnit for unit test
- NuGet for package hosting

## License

[MIT License](https://github.com/Arnab-Developer/ArnabDeveloper.HtmlContent.Core/blob/main/LICENSE)

## Contributing

Please read [this](https://github.com/Arnab-Developer/ArnabDeveloper.HtmlContent.Core/blob/main/CONTRIBUTING.md) 
for contribution related guidelines.
