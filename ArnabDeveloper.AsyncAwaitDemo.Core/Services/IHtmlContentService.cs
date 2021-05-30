using ArnabDeveloper.AsyncAwaitDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArnabDeveloper.AsyncAwaitDemo.Core.Services
{
    public interface IHtmlContentService
    {
        IList<string> Urls { get; set; }

        IEnumerable<WebSiteDataModel> GetContent();

        Task<IEnumerable<WebSiteDataModel>> GetContentAsync();

        Task<IEnumerable<WebSiteDataModel>> GetContentParallelAsync();

        Task<IEnumerable<WebSiteDataModel>> GetContentParallelAsyncV2();

        Task<IEnumerable<WebSiteDataModel>> GetContentParallelAsyncV2WithProgress(
            IProgress<ProgressDataModel> progress);
    }
}