using ArnabDeveloper.AsyncAwaitDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArnabDeveloper.AsyncAwaitDemo.Core.Services
{
    public interface IDemoMethods
    {
        IList<string> Urls { get; set; }

        IEnumerable<WebSiteDataModel> RunDownloadNormal();

        Task<IEnumerable<WebSiteDataModel>> RunDownloadAsync();

        Task<IEnumerable<WebSiteDataModel>> RunDownloadParallelAsync();

        Task<IEnumerable<WebSiteDataModel>> RunDownloadParallelV2Async();

        Task<IEnumerable<WebSiteDataModel>> RunDownloadParallelV2WithProgressBarAsync(
            IProgress<ProgressDataModel> progress);
    }
}