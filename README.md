# .NET async await demo

A demo WPF application for async await with .NET

## Use cases

This application send request to http web addresses and download the response as 
string. There are five use cases.

- Download string synchronously (normal)
- Download string asynchronously
- Download string asynchronously in parallel
- Download string asynchronously in parallel with `Parallel.ForEach()`
- Download string asynchronously in parallel with `Parallel.ForEach()` with
progressbar and show result as they are available

![Screenshot](https://github.com/Arnab-Developer/AsyncAwaitDemo/blob/main/Assets/Screenshot.png)

## Detail tutorial

[C# Advanced Async by TimCorey](https://www.youtube.com/watch?v=ZTKGRJy5P2M)