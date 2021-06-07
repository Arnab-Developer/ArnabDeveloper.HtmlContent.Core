using ArnabDeveloper.HtmlContent.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArnabDeveloper.HtmlContent.Core.Services
{
    public interface IHtmlContentService
    {
        IList<string> Urls { get; set; }

        IEnumerable<WebSiteDataModel> GetContent();

        Task<IEnumerable<WebSiteDataModel>> GetContentAsync();

        IAsyncEnumerable<ProgressDataModel> GetContentAsyncStream();

        Task<IEnumerable<WebSiteDataModel>> GetContentParallelAsync();

        Task<IEnumerable<WebSiteDataModel>> GetContentParallelForEachAsync();

        Task<IEnumerable<WebSiteDataModel>> GetContentParallelForEachProgressAsync(
            IProgress<ProgressDataModel> progress);        
    }
}