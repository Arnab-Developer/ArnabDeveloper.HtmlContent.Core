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

- `GetContent()` to get HTML responses synchronously
- `GetContentAsync()` to get HTML responses asynchronously
- `GetContentParallelAsync` to get HTML responses asynchronously in parallel
- `GetContentParallelAsyncV2()` to get HTML responses asynchronously in parallel 
with `Parallel.ForEach()`
- `GetContentParallelAsyncV2WithProgress()` to get HTML responses asynchronously 
in parallel with `Parallel.ForEach()` with progress notification
- `GetContentParallelAsyncV2WithAsyncStream()` use of async stream

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
